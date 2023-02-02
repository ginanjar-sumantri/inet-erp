using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Collections;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed class BeritaAcaraBL : Base
    {
        public BeritaAcaraBL()
        {
        }

        #region Berita Acara

        public int RowCountBeritaAcara(string _prmCategory, string _prmKeyword)
        {
            int _result = 0;
            try
            {
                string _pattern1 = "%%";
                string _pattern2 = "%%";

                if (_prmCategory == "TransNmbr")
                {
                    _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
                    _pattern2 = "%%";
                }
                else if (_prmCategory == "SalesConfirmationNoRef")
                {
                    _pattern1 = "%%";
                    _pattern2 = "%" + _prmKeyword.Trim().ToLower() + "%";
                }

                _result = (
                    from _bilTrBeritaAcara in this.db.BILTrBeritaAcaras
                    where
                        (SqlMethods.Like(_bilTrBeritaAcara.TransNmbr.Trim().ToLower(), _pattern1))
                            && (SqlMethods.Like(_bilTrBeritaAcara.SalesConfirmationNoRef.Trim().ToLower(), _pattern2))
                    select _bilTrBeritaAcara
                           ).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public double RowCountBAList(string _prmCategory, string _prmKeyword, int _prmYear, int _prmPeriod)
        {
            int _result = 0;

            try
            {
                string _pattern1 = "%%";
                String _patternYear = "%%";
                String _patternPeriod = "%%";

                if (_prmCategory == "BANo")
                {
                    _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
                }
                if (_prmYear != 0)
                {
                    _patternYear = "%" + _prmYear.ToString().Trim().ToLower() + "%";
                }
                if (_prmPeriod != 0)
                {
                    _patternPeriod = "%" + _prmPeriod.ToString().Trim().ToLower() + "%";
                }

                _result = (
                               from _bilTrBeritaAcara in this.db.BILTrBeritaAcaras
                               join _bilTrSalesConfirmation in this.db.BILTrSalesConfirmations
                                   on _bilTrBeritaAcara.SalesConfirmationNoRef equals _bilTrSalesConfirmation.TransNmbr
                               where (SqlMethods.Like(_bilTrBeritaAcara.FileNmbr.Trim().ToLower(), _pattern1))
                                   && (SqlMethods.Like(Convert.ToDateTime(_bilTrBeritaAcara.TransDate).Year.ToString().Trim().ToLower(), _patternYear))
                                   && (SqlMethods.Like(Convert.ToDateTime(_bilTrBeritaAcara.TransDate).Month.ToString().Trim().ToLower(), _patternPeriod))
                                   && (_bilTrSalesConfirmation.CustBillAccount != "null" || _bilTrSalesConfirmation.CustBillAccount != null)
                               select _bilTrBeritaAcara
                           ).Count();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public bool DeleteMultiBeritaAcara(string[] _prmBeritaAcaraTransNmbr)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmBeritaAcaraTransNmbr.Length; i++)
                {
                    BILTrBeritaAcara _bilTrBeritaAcara = this.db.BILTrBeritaAcaras.Single(a => a.TransNmbr == _prmBeritaAcaraTransNmbr[i]);
                    if (_bilTrBeritaAcara.FileNmbr == "")
                        this.db.BILTrBeritaAcaras.DeleteOnSubmit(_bilTrBeritaAcara);
                }
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<BILTrBeritaAcara> GetListBeritaAcara(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<BILTrBeritaAcara> _result = new List<BILTrBeritaAcara>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "TransNmbr")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
                _pattern2 = "%%";
            }
            else if (_prmCategory == "SalesConfirmationNoRef")
            {
                _pattern1 = "%%";
                _pattern2 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }

            try
            {
                var _query =
                            (
                                from _bilTrBeritaAcara in this.db.BILTrBeritaAcaras
                                where
                                    (SqlMethods.Like(_bilTrBeritaAcara.TransNmbr.Trim().ToLower(), _pattern1))
                                    && (SqlMethods.Like(_bilTrBeritaAcara.SalesConfirmationNoRef.Trim().ToLower(), _pattern2))
                                orderby _bilTrBeritaAcara.TransDate descending
                                select _bilTrBeritaAcara
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    String _customerName = (
                            from _bilTrSalesConfirmation in this.db.BILTrSalesConfirmations
                            where _bilTrSalesConfirmation.TransNmbr == _row.SalesConfirmationNoRef
                            select _bilTrSalesConfirmation.CompanyName
                        ).FirstOrDefault();
                    _row.ApprovedByCustomerName = _customerName;
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<BILTrBeritaAcara> GetListBAList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword, int _prmYear, int _prmPeriod)
        {
            List<BILTrBeritaAcara> _result = new List<BILTrBeritaAcara>();

            string _pattern1 = "%%";
            String _patternYear = "%%";
            String _patternPeriod = "%%";

            if (_prmCategory == "BANo")
            {
                _pattern1 = "%" + _prmKeyword.Trim().ToLower() + "%";
            }
            if (_prmYear != 0)
            {
                _patternYear = "%" + _prmYear.ToString().Trim().ToLower() + "%";
            }
            if (_prmPeriod != 0)
            {
                _patternPeriod = "%" + _prmPeriod.ToString().Trim().ToLower() + "%";
            }

            try
            {
                var _query =
                            (
                                from _bilTrBeritaAcara in this.db.BILTrBeritaAcaras
                                join _bilTrSalesConfirmation in this.db.BILTrSalesConfirmations
                                    on _bilTrBeritaAcara.SalesConfirmationNoRef equals _bilTrSalesConfirmation.TransNmbr
                                where (SqlMethods.Like(_bilTrBeritaAcara.FileNmbr.Trim().ToLower(), _pattern1))
                                    && (SqlMethods.Like(Convert.ToDateTime(_bilTrBeritaAcara.TransDate).Year.ToString().Trim().ToLower(), _patternYear))
                                    && (SqlMethods.Like(Convert.ToDateTime(_bilTrBeritaAcara.TransDate).Month.ToString().Trim().ToLower(), _patternPeriod))
                                    && (_bilTrSalesConfirmation.CustBillAccount != "null" || _bilTrSalesConfirmation.CustBillAccount != null)
                                orderby _bilTrBeritaAcara.TransDate descending
                                select _bilTrBeritaAcara
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    String _customerName = (
                            from _bilTrSalesConfirmation in this.db.BILTrSalesConfirmations
                            where _bilTrSalesConfirmation.TransNmbr == _row.SalesConfirmationNoRef
                            select _bilTrSalesConfirmation.CompanyName
                        ).FirstOrDefault();
                    _row.ApprovedByCustomerName = _customerName;
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetTrialDayOfSalesConfirmation(String _prmSalesConfirmationID)
        {
            String _result = "";
            try
            {
                _result = (
                        from _salesConfirmation in this.db.BILTrSalesConfirmations
                        where _salesConfirmation.TransNmbr == _prmSalesConfirmationID
                        select _salesConfirmation.FreeTrialDays
                    ).FirstOrDefault().ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public SortedList GetListSalesConfirmationForDDL()
        {
            SortedList _result = new SortedList();
            try
            {
                var _query = (from _bilTrSalesConfirmation in this.db.BILTrSalesConfirmations
                              where !(from _beritaAcara in this.db.BILTrBeritaAcaras
                                      select _beritaAcara.SalesConfirmationNoRef).Contains(_bilTrSalesConfirmation.TransNmbr)
                                  && _bilTrSalesConfirmation.Status == 3
                              orderby _bilTrSalesConfirmation.TransDate descending
                              select _bilTrSalesConfirmation);
                foreach (var _obj in _query)
                {
                    _result.Add(_obj.TransNmbr, _obj.FileNmbr);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public String GetSalesConfirmationFileNumber(String _prmSalesCOnfirmationTransNmbr)
        {
            String _result = "";
            try
            {
                _result = (
                        from _bilTrSalesConfirmation in this.db.BILTrSalesConfirmations
                        where _bilTrSalesConfirmation.TransNmbr == _prmSalesCOnfirmationTransNmbr
                        select _bilTrSalesConfirmation.FileNmbr
                    ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String GetContractNumberBySalesConfirmation(String _prmSalesConfirmation)
        {
            String _result = "";
            try
            {
                var _query = (from _bilTrContract in this.db.BILTrContracts
                              where _bilTrContract.SalesConfirmationNoRef == _prmSalesConfirmation
                              select _bilTrContract.TransNmbr);
                if (_query.Count() > 0)
                    _result = _query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public Boolean AddBeritaAcara(String _prmCustomerApprovedBy,
            DateTime _prmTransDate, String _prmSalesConfirmationRef,
            String _prmNoKontrak, String _prmTechnicalAccountMRTG, String _prmTechnicalAccountMRTGPassword,
            String _prmTechnicalIPAllocation, String _prmTechnicalTransmision,
            String _prmTechnicalServiceSpesification, int _prmTechnicalBandwidthINT,
            int _prmTechnicalBandwidthLocalLoop, int _prmTechnicalBandwidthINTRatioUpstream,
            int _prmTechnicalBandwidthINTRatioDownstream, String _prmTechnicalTerminationPoint,
            String _prmMikrotikPPTPUserName, String _prmMikrotikPPPOEUserName,
            String _prmMikrotikHotspotUserName, String _prmMikrotikQueueNameDownLink,
            String _prmMikrotikQueueNameUpLink, String _prmCollocationRackNo,
            String _prmCollocationServerPositionNo, String _prmBGPASNumber,
            String _prmBGPIPAddressRouter, String _prmBGPAdvertiseIP, String _prmTypeReceivedRoutes,
            String _prmCreatedBy, int _prmTrialDay, String _prmRemark,
            ref String _prmTransNmbr)
        {
            Boolean _result = false;
            try
            {
                BILTrBeritaAcara _addDataBilTrBeritaAcara = new BILTrBeritaAcara();
                Temporary_TransactionNumber _transactionNumber = new Temporary_TransactionNumber();
                foreach (spERP_TransactionAutoNmbrResult _item in this.db.spERP_TransactionAutoNmbr(AppModule.GetValue(TransactionType.Transaction)))
                {
                    _addDataBilTrBeritaAcara.TransNmbr = _item.Number;
                    _transactionNumber.TempTransNmbr = _item.Number;
                    _prmTransNmbr = _item.Number;
                }
                this.db.Temporary_TransactionNumbers.InsertOnSubmit(_transactionNumber);

                var _query = (
                            from _temp in this.db.Temporary_TransactionNumbers
                            where _temp.TempTransNmbr != _transactionNumber.TempTransNmbr
                            select _temp
                          );
                this.db.Temporary_TransactionNumbers.DeleteAllOnSubmit(_query);

                _addDataBilTrBeritaAcara.TransDate = _prmTransDate;
                _addDataBilTrBeritaAcara.SalesConfirmationNoRef = _prmSalesConfirmationRef;
                _addDataBilTrBeritaAcara.NoKontrak = _prmNoKontrak;
                _addDataBilTrBeritaAcara.TechnicalAccountMRTG = _prmTechnicalAccountMRTG;
                _addDataBilTrBeritaAcara.TechnicalAccountMRTGPassword = _prmTechnicalAccountMRTGPassword;
                _addDataBilTrBeritaAcara.TechnicalIPAllocation = _prmTechnicalIPAllocation;
                _addDataBilTrBeritaAcara.TechnicalTransmision = _prmTechnicalTransmision;
                _addDataBilTrBeritaAcara.TechnicalServiceSpesification = _prmTechnicalServiceSpesification;
                _addDataBilTrBeritaAcara.TechnicalBandwidthINT = _prmTechnicalBandwidthINT;
                _addDataBilTrBeritaAcara.TechnicalBandwidthLocalLoop = _prmTechnicalBandwidthLocalLoop;
                _addDataBilTrBeritaAcara.TechnicalBandwidthINTRatioUpstream = _prmTechnicalBandwidthINTRatioUpstream;
                _addDataBilTrBeritaAcara.TechnicalBandwidthINTRatioDownstream = _prmTechnicalBandwidthINTRatioDownstream;
                _addDataBilTrBeritaAcara.TechnicalTerminationPoint = _prmTechnicalTerminationPoint;
                _addDataBilTrBeritaAcara.MikrotikPPTPUserName = _prmMikrotikPPTPUserName;
                _addDataBilTrBeritaAcara.MikrotikPPPOEUserName = _prmMikrotikPPPOEUserName;
                _addDataBilTrBeritaAcara.MikrotikHotspotUserName = _prmMikrotikHotspotUserName;
                _addDataBilTrBeritaAcara.MikrotikQueueNameDownLink = _prmMikrotikQueueNameDownLink;
                _addDataBilTrBeritaAcara.MikrotikQueueNameUpLink = _prmMikrotikQueueNameUpLink;
                _addDataBilTrBeritaAcara.CollocationRackNo = _prmCollocationRackNo;
                _addDataBilTrBeritaAcara.CollocationServerPositionNo = _prmCollocationServerPositionNo;
                _addDataBilTrBeritaAcara.BGPASNumber = _prmBGPASNumber;
                _addDataBilTrBeritaAcara.BGPIPAddressRouter = _prmBGPIPAddressRouter;
                _addDataBilTrBeritaAcara.BGPAdvertiseIP = _prmBGPAdvertiseIP;
                _addDataBilTrBeritaAcara.TypeReceivedRoutes = _prmTypeReceivedRoutes;
                _addDataBilTrBeritaAcara.CreatedBy = _prmCreatedBy;
                _addDataBilTrBeritaAcara.CreatedDate = DateTime.Now;
                _addDataBilTrBeritaAcara.Status = BeritaAcaraDataMapper.GetStatusByte(TransStatus.OnHold);
                _addDataBilTrBeritaAcara.TrialDay = _prmTrialDay;
                _addDataBilTrBeritaAcara.Remark = _prmRemark;
                _addDataBilTrBeritaAcara.ApprovedByCustomerName = _prmCustomerApprovedBy;

                this.db.BILTrBeritaAcaras.InsertOnSubmit(_addDataBilTrBeritaAcara);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean UpdateBeritaAcara(String _prmTransNmbr, DateTime _prmTransDate,
            String _prmSalesConfirmationRef,
    String _prmNoKontrak, String _prmTechnicalAccountMRTG, String _prmTechnicalAccountMRTGPassword,
    String _prmTechnicalIPAllocation, String _prmTechnicalTransmision,
    String _prmTechnicalServiceSpesification, int _prmTechnicalBandwidthINT,
    int _prmTechnicalBandwidthLocalLoop, int _prmTechnicalBandwidthINTRatioUpstream,
    int _prmTechnicalBandwidthINTRatioDownstream, String _prmTechnicalTerminationPoint,
    String _prmMikrotikPPTPUserName, String _prmMikrotikPPPOEUserName,
    String _prmMikrotikHotspotUserName, String _prmMikrotikQueueNameDownLink,
    String _prmMikrotikQueueNameUpLink, String _prmCollocationRackNo,
    String _prmCollocationServerPositionNo, String _prmBGPASNumber,
    String _prmBGPIPAddressRouter, String _prmBGPAdvertiseIP, String _prmTypeReceivedRoutes,
    String _prmAprovedByCustomerName, int _prmTrialDay, String _prmRemark, String _prmEditBy)
        {
            Boolean _result = false;
            try
            {
                BILTrBeritaAcara _updateDataBilTrBeritaAcara = this.db.BILTrBeritaAcaras.Single(a => a.TransNmbr == _prmTransNmbr);

                _updateDataBilTrBeritaAcara.TransDate = _prmTransDate;
                _updateDataBilTrBeritaAcara.SalesConfirmationNoRef = _prmSalesConfirmationRef;
                _updateDataBilTrBeritaAcara.NoKontrak = _prmNoKontrak;
                _updateDataBilTrBeritaAcara.TechnicalAccountMRTG = _prmTechnicalAccountMRTG;
                _updateDataBilTrBeritaAcara.TechnicalAccountMRTGPassword = _prmTechnicalAccountMRTGPassword;
                _updateDataBilTrBeritaAcara.TechnicalIPAllocation = _prmTechnicalIPAllocation;
                _updateDataBilTrBeritaAcara.TechnicalTransmision = _prmTechnicalTransmision;
                _updateDataBilTrBeritaAcara.TechnicalServiceSpesification = _prmTechnicalServiceSpesification;
                _updateDataBilTrBeritaAcara.TechnicalBandwidthINT = _prmTechnicalBandwidthINT;
                _updateDataBilTrBeritaAcara.TechnicalBandwidthLocalLoop = _prmTechnicalBandwidthLocalLoop;
                _updateDataBilTrBeritaAcara.TechnicalBandwidthINTRatioUpstream = _prmTechnicalBandwidthINTRatioUpstream;
                _updateDataBilTrBeritaAcara.TechnicalBandwidthINTRatioDownstream = _prmTechnicalBandwidthINTRatioDownstream;
                _updateDataBilTrBeritaAcara.TechnicalTerminationPoint = _prmTechnicalTerminationPoint;
                _updateDataBilTrBeritaAcara.MikrotikPPTPUserName = _prmMikrotikPPTPUserName;
                _updateDataBilTrBeritaAcara.MikrotikPPPOEUserName = _prmMikrotikPPPOEUserName;
                _updateDataBilTrBeritaAcara.MikrotikHotspotUserName = _prmMikrotikHotspotUserName;
                _updateDataBilTrBeritaAcara.MikrotikQueueNameDownLink = _prmMikrotikQueueNameDownLink;
                _updateDataBilTrBeritaAcara.MikrotikQueueNameUpLink = _prmMikrotikQueueNameUpLink;
                _updateDataBilTrBeritaAcara.CollocationRackNo = _prmCollocationRackNo;
                _updateDataBilTrBeritaAcara.CollocationServerPositionNo = _prmCollocationServerPositionNo;
                _updateDataBilTrBeritaAcara.BGPASNumber = _prmBGPASNumber;
                _updateDataBilTrBeritaAcara.BGPIPAddressRouter = _prmBGPIPAddressRouter;
                _updateDataBilTrBeritaAcara.BGPAdvertiseIP = _prmBGPAdvertiseIP;
                _updateDataBilTrBeritaAcara.TypeReceivedRoutes = _prmTypeReceivedRoutes;
                _updateDataBilTrBeritaAcara.EditBy = _prmEditBy;
                _updateDataBilTrBeritaAcara.EditDate = DateTime.Now;
                _updateDataBilTrBeritaAcara.ApprovedByCustomerName = _prmAprovedByCustomerName;
                _updateDataBilTrBeritaAcara.TrialDay = _prmTrialDay;
                _updateDataBilTrBeritaAcara.Remark = _prmRemark;

                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean EditBeritaAcara(BILTrBeritaAcara _prmBILTrBeritaAcara)
        {
            Boolean _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public BILTrBeritaAcara GetSingleBeritaAcara(String _prmTransNmbr)
        {
            BILTrBeritaAcara _result = new BILTrBeritaAcara();
            try
            {
                _result = (from _bilTrBeritaAcara in this.db.BILTrBeritaAcaras
                           where _bilTrBeritaAcara.TransNmbr == _prmTransNmbr
                           select _bilTrBeritaAcara).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public BILTrBeritaAcara GetBeritaAcaraBySCCode(String _prmSCCode)
        {
            BILTrBeritaAcara _result = null;

            try
            {
                _result = (
                               from _ba in this.db.BILTrBeritaAcaras
                               where _ba.SalesConfirmationNoRef == _prmSCCode
                               orderby _ba.TransDate descending
                               select _ba
                          ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetContractFileNmbrByContractTransNmbr(String _prmContractTransNmbr)
        {
            String _result = "";
            try
            {
                BILTrContract _dataContract = this.db.BILTrContracts.Single(a => a.TransNmbr == _prmContractTransNmbr);
                if (_dataContract != null)
                    _result = _dataContract.FileNmbr;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String ApproveBeritaAcara(String _prmTransNmbr, String _prmApprovedByCustomer, String _prmUser, Byte _prmStatus)
        {
            String _result = "";
            try
            {
                if (_prmStatus == BeritaAcaraDataMapper.GetStatus(TransStatus.OnHold))
                    this.db.spBIL_BeritaAcaraGetAppr(_prmTransNmbr, 0, 0, _prmUser, ref _result);
                else if (_prmStatus == BeritaAcaraDataMapper.GetStatus(TransStatus.WaitingForApproval))
                    this.db.spBIL_BeritaAcaraApprove(_prmTransNmbr, 0, 0, _prmUser, ref _result);
                else if (_prmStatus == BeritaAcaraDataMapper.GetStatus(TransStatus.Approved))
                    this.db.spBIL_BeritaAcaraPost(_prmTransNmbr, 0, 0, _prmUser, this._companyTag, ref _result);

                BILTrBeritaAcara _approveDataBeritaAcara = this.db.BILTrBeritaAcaras.Single(a => a.TransNmbr == _prmTransNmbr);
                if (_prmStatus == BeritaAcaraDataMapper.GetStatus(TransStatus.WaitingForApproval) && _result == "")
                {
                    foreach (S_SAAutoNmbrResult item in this.db.S_SAAutoNmbr(Convert.ToDateTime(_approveDataBeritaAcara.TransDate).Year, Convert.ToDateTime(_approveDataBeritaAcara.TransDate).Month, AppModule.GetValue(TransactionType.Retail), this._companyTag, ""))
                    {
                        _approveDataBeritaAcara.FileNmbr = item.Number;
                    }
                    _approveDataBeritaAcara.ApprovedByCustomerName = _prmApprovedByCustomer;
                    _approveDataBeritaAcara.ApprovedDate = DateTime.Now;

                    SalesConfirmationBL _salesConfirmastionBL = new SalesConfirmationBL();
                    if (_salesConfirmastionBL.GetFgSoftBlockExec(_approveDataBeritaAcara.SalesConfirmationNoRef))
                    {
                        NotificationBL _notificationBL = new NotificationBL();
                        _notificationBL.SendEmail(_approveDataBeritaAcara.SalesConfirmationNoRef, EmailNotificationID.BAOpenSoftBlock);
                    }
                }
                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Int16 GetBeritaAcaraStatus(String _prmTransNmbr)
        {
            Int16 _result = 0;
            try
            {
                _result = Convert.ToInt16(this.db.BILTrBeritaAcaras.Single(a => a.TransNmbr == _prmTransNmbr).Status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        #endregion

        ~BeritaAcaraBL()
        {
        }
    }
}
