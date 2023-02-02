using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.Linq.SqlClient;
using System.Net.Mail;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using System.Web;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Billing
{
    public sealed partial class NotificationBL : Base
    {
        public NotificationBL()
        {
        }

        #region Notification
        #region BA
        public double RowsCountBANotYetApproved(DateTime _prmDate, String _prmStatus)
        {
            String _pattern1 = "";
            int _result = 0;

            if (_prmStatus.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else if (_prmStatus.Trim() == "0")
            {
                _pattern1 = "false";
            }
            else if (_prmStatus.Trim() == "1")
            {
                _pattern1 = "true";
            }

            try
            {
                _result = (
                                 from _sc in this.db.BILTrSalesConfirmations
                                 where !(
                                            from _ba in this.db.BILTrBeritaAcaras
                                            where _ba.SalesConfirmationNoRef == _sc.TransNmbr
                                                && (_ba.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Approved) || _ba.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Posted))
                                            select _ba.SalesConfirmationNoRef
                                        ).Contains(_sc.TransNmbr)
                                    && ((DateTime)_sc.ApprovedDate).AddDays((int)_sc.TargetInstallationDay) <= new DateTime(_prmDate.Year, _prmDate.Month, _prmDate.Day, 23, 59, 59)
                                    && (SqlMethods.Like(_sc.FgPending.ToString().Trim(), _pattern1.ToString().Trim()))
                                 orderby ((DateTime)_sc.ApprovedDate).AddDays((int)_sc.TargetInstallationDay) descending
                                 select _sc.TransNmbr
                            ).Count();
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public double RowsCountBASoftBlock(DateTime _prmDate, String _prmStatusBlock)
        {
            String _pattern1 = "";
            int _result = 0;

            if (_prmStatusBlock.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else if (_prmStatusBlock.Trim() == "0")
            {
                _pattern1 = "false";
            }
            else if (_prmStatusBlock.Trim() == "1")
            {
                _pattern1 = "true";
            }

            try
            {
                int _dueDay = new EmailNotificationSetupBL().GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.BASoftBlock)).Days;

                _result = (
                                 from _sc in this.db.BILTrSalesConfirmations
                                 where !(
                                            from _ba in this.db.BILTrBeritaAcaras
                                            where _ba.SalesConfirmationNoRef == _sc.TransNmbr
                                                && (_ba.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Approved) || _ba.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Posted))
                                            select _ba.SalesConfirmationNoRef
                                        ).Contains(_sc.TransNmbr)
                                    && ((DateTime)_sc.ApprovedDate).AddDays((int)_sc.TargetInstallationDay + _dueDay) <= new DateTime(_prmDate.Year, _prmDate.Month, _prmDate.Day, 23, 59, 59)
                                    && (SqlMethods.Like(_sc.FgSoftBlockExec.ToString().Trim(), _pattern1.ToString().Trim()))
                                 orderby ((DateTime)_sc.ApprovedDate).AddDays((int)_sc.TargetInstallationDay + _dueDay) descending
                                 select _sc.TransNmbr
                            ).Count();
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String GetListBANotYetApproved(DateTime _prmDate, String _prmStatus)
        {
            String _pattern1 = "";
            if (_prmStatus.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else if (_prmStatus.Trim() == "0")
            {
                _pattern1 = "false";
            }
            else if (_prmStatus.Trim() == "1")
            {
                _pattern1 = "true";
            }

            String _result = "";

            try
            {
                var _query = (
                                 from _sc in this.db.BILTrSalesConfirmations
                                 where !(
                                            from _ba in this.db.BILTrBeritaAcaras
                                            where _ba.SalesConfirmationNoRef == _sc.TransNmbr
                                                && (_ba.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Approved) || _ba.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Posted))
                                            select _ba.SalesConfirmationNoRef
                                        ).Contains(_sc.TransNmbr)
                                    && ((DateTime)_sc.ApprovedDate).AddDays((int)_sc.TargetInstallationDay) <= new DateTime(_prmDate.Year, _prmDate.Month, _prmDate.Day, 23, 59, 59)
                                    && (SqlMethods.Like(_sc.FgPending.ToString().Trim(), _pattern1.ToString().Trim()))
                                 orderby ((DateTime)_sc.ApprovedDate).AddDays((int)_sc.TargetInstallationDay) descending
                                 select _sc.TransNmbr
                            );

                foreach (String _item in _query)
                {
                    if (_result == "")
                    {
                        _result = _item;
                    }
                    else
                    {
                        _result += "," + _item;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String GetListBASoftBlock(DateTime _prmDate, String _prmStatusBlock)
        {
            String _pattern1 = "";

            if (_prmStatusBlock.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else if (_prmStatusBlock.Trim() == "0")
            {
                _pattern1 = "false";
            }
            else if (_prmStatusBlock.Trim() == "1")
            {
                _pattern1 = "true";
            }

            String _result = "";

            try
            {
                int _dueDay = new EmailNotificationSetupBL().GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.BASoftBlock)).Days;

                var _query = (
                                 from _sc in this.db.BILTrSalesConfirmations
                                 where !(
                                            from _ba in this.db.BILTrBeritaAcaras
                                            where _ba.SalesConfirmationNoRef == _sc.TransNmbr
                                                && (_ba.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Approved) || _ba.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Posted))
                                            select _ba.SalesConfirmationNoRef
                                        ).Contains(_sc.TransNmbr)
                                    && ((DateTime)_sc.ApprovedDate).AddDays((int)_sc.TargetInstallationDay + _dueDay) <= new DateTime(_prmDate.Year, _prmDate.Month, _prmDate.Day, 23, 59, 59)
                                    && (SqlMethods.Like(_sc.FgSoftBlockExec.ToString().Trim(), _pattern1.ToString().Trim()))
                                 orderby ((DateTime)_sc.ApprovedDate).AddDays((int)_sc.TargetInstallationDay + _dueDay) descending
                                 select _sc.TransNmbr
                            );

                foreach (String _item in _query)
                {
                    if (_result == "")
                    {
                        _result = _item;
                    }
                    else
                    {
                        _result += "," + _item;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<BILTrSalesConfirmation> GetListBANotYetApproved(int _prmReqPage, int _prmPageSize, DateTime _prmDate, String _prmStatus)
        {
            String _pattern1 = "";

            if (_prmStatus.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else if (_prmStatus.Trim() == "0")
            {
                _pattern1 = "false";
            }
            else if (_prmStatus.Trim() == "1")
            {
                _pattern1 = "true";
            }

            List<BILTrSalesConfirmation> _result = new List<BILTrSalesConfirmation>();

            try
            {
                var _query = (
                                 from _sc in this.db.BILTrSalesConfirmations
                                 where !(
                                            from _ba in this.db.BILTrBeritaAcaras
                                            where _ba.SalesConfirmationNoRef == _sc.TransNmbr
                                                && (_ba.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Approved) || _ba.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Posted))
                                            select _ba.SalesConfirmationNoRef
                                        ).Contains(_sc.TransNmbr)
                                    && ((DateTime)_sc.ApprovedDate).AddDays((int)_sc.TargetInstallationDay) <= new DateTime(_prmDate.Year, _prmDate.Month, _prmDate.Day, 23, 59, 59)
                                    && (SqlMethods.Like(_sc.FgPending.ToString().Trim(), _pattern1.ToString().Trim()))
                                 orderby ((DateTime)_sc.ApprovedDate).AddDays((int)_sc.TargetInstallationDay) descending
                                 select new
                                 {
                                     TransNmbr = _sc.TransNmbr,
                                     FileNmbr = _sc.FileNmbr,
                                     TransDate = _sc.TransDate,
                                     CompanyName = _sc.CompanyName,
                                     Remark = _sc.Remark,
                                     FgSoftBlockExec = _sc.FgSoftBlockExec,
                                     StatusPending = _sc.FgPending,
                                     FgNotify = _sc.FgNotifiedBA
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILTrSalesConfirmation(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CompanyName, _row.Remark, _row.FgSoftBlockExec, _row.StatusPending, _row.FgNotify));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<BILTrSalesConfirmation> GetListBASoftBlock(int _prmReqPage, int _prmPageSize, DateTime _prmDate, String _prmStatusBlock)
        {
            String _pattern1 = "";

            if (_prmStatusBlock.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else if (_prmStatusBlock.Trim() == "0")
            {
                _pattern1 = "false";
            }
            else if (_prmStatusBlock.Trim() == "1")
            {
                _pattern1 = "true";
            }

            List<BILTrSalesConfirmation> _result = new List<BILTrSalesConfirmation>();

            try
            {
                int _dueDay = new EmailNotificationSetupBL().GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.BASoftBlock)).Days;

                var _query = (
                                 from _sc in this.db.BILTrSalesConfirmations
                                 where !(
                                            from _ba in this.db.BILTrBeritaAcaras
                                            where _ba.SalesConfirmationNoRef == _sc.TransNmbr
                                                && (_ba.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Approved) || _ba.Status == BeritaAcaraDataMapper.GetStatus(TransStatus.Posted))
                                            select _ba.SalesConfirmationNoRef
                                        ).Contains(_sc.TransNmbr)
                                    && ((DateTime)_sc.ApprovedDate).AddDays((int)_sc.TargetInstallationDay + _dueDay) <= new DateTime(_prmDate.Year, _prmDate.Month, _prmDate.Day, 23, 59, 59)
                                    && (SqlMethods.Like(_sc.FgSoftBlockExec.ToString().Trim(), _pattern1.ToString().Trim()))
                                 orderby ((DateTime)_sc.ApprovedDate).AddDays((int)_sc.TargetInstallationDay + _dueDay) descending
                                 select new
                                 {
                                     TransNmbr = _sc.TransNmbr,
                                     FileNmbr = _sc.FileNmbr,
                                     TransDate = _sc.TransDate,
                                     CompanyName = _sc.CompanyName,
                                     Remark = _sc.Remark,
                                     FgSoftBlockExec = _sc.FgSoftBlockExec,
                                     FgPending = _sc.FgPending,
                                     FgNotify = _sc.FgNotifiedSoftBlock
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILTrSalesConfirmation(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CompanyName, _row.Remark, _row.FgSoftBlockExec, _row.FgPending, _row.FgNotify));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        #endregion BA

        #region CL
        public double RowsCountCLNotYetApproved(DateTime _prmDate, String _prmStatus, EmailNotificationID _prmCLType)
        {
            String _pattern1 = "";
            int _result = 0;

            if (_prmStatus.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else if (_prmStatus.Trim() == "0")
            {
                _pattern1 = "false";
            }
            else if (_prmStatus.Trim() == "1")
            {
                _pattern1 = "true";
            }

            try
            {
                int _dueDay = new EmailNotificationSetupBL().GetSingle(EmailNotificationDataMapper.GetID(_prmCLType)).Days;

                _result = (
                                 from _sc in this.db.BILTrSalesConfirmations
                                 join _ba in this.db.BILTrBeritaAcaras
                                    on _sc.TransNmbr equals _ba.SalesConfirmationNoRef
                                 where _ba.Status >= BeritaAcaraDataMapper.GetStatus(TransStatus.Approved)
                                    && ((DateTime)_ba.ApprovedDate).AddDays(_dueDay) <= new DateTime(_prmDate.Year, _prmDate.Month, _prmDate.Day, 23, 59, 59)
                                    && (SqlMethods.Like(_sc.FgPending.ToString().Trim(), _pattern1.ToString().Trim()))
                                    && !(
                                            from _con in this.db.BILTrContracts
                                            where _con.SalesConfirmationNoRef == _sc.TransNmbr
                                                && (_con.Status == ContractDataMapper.GetStatusByte(TransStatus.Approved))
                                            select _con.SalesConfirmationNoRef
                                        ).Contains(_sc.TransNmbr)
                                 orderby ((DateTime)_ba.ApprovedDate).AddDays(_dueDay) descending
                                 select _sc.TransNmbr
                            ).Count();
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public double RowsCountCLSoftBlock(DateTime _prmDate, String _prmStatusBlock)
        {
            String _pattern1 = "";
            int _result = 0;

            if (_prmStatusBlock.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else if (_prmStatusBlock.Trim() == "0")
            {
                _pattern1 = "false";
            }
            else if (_prmStatusBlock.Trim() == "1")
            {
                _pattern1 = "true";
            }

            try
            {
                int _dueDay = new EmailNotificationSetupBL().GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.CLSoftBlock)).Days;

                _result = (
                                 from _sc in this.db.BILTrSalesConfirmations
                                 join _ba in this.db.BILTrBeritaAcaras
                                    on _sc.TransNmbr equals _ba.SalesConfirmationNoRef
                                 join _con in this.db.BILTrContracts
                                    on _sc.TransNmbr equals _con.SalesConfirmationNoRef
                                 where _ba.Status >= BeritaAcaraDataMapper.GetStatus(TransStatus.Approved)
                                    && ((DateTime)_ba.ApprovedDate).AddDays(_dueDay) <= new DateTime(_prmDate.Year, _prmDate.Month, _prmDate.Day, 23, 59, 59)
                                    && (SqlMethods.Like(_sc.FgSoftBlockExec.ToString().Trim(), _pattern1.ToString().Trim()))
                                    && _con.Status == ContractDataMapper.GetStatus(TransStatus.Approved)
                                 orderby ((DateTime)_ba.ApprovedDate).AddDays(_dueDay) descending
                                 select _sc.TransNmbr
                            ).Count();
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String GetListCLNotYetApproved(DateTime _prmDate, String _prmStatus, EmailNotificationID _prmCLType)
        {
            String _pattern1 = "";
            if (_prmStatus.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else if (_prmStatus.Trim() == "0")
            {
                _pattern1 = "false";
            }
            else if (_prmStatus.Trim() == "1")
            {
                _pattern1 = "true";
            }

            String _result = "";

            try
            {
                int _dueDay = new EmailNotificationSetupBL().GetSingle(EmailNotificationDataMapper.GetID(_prmCLType)).Days;

                var _query = (
                                 from _sc in this.db.BILTrSalesConfirmations
                                 join _ba in this.db.BILTrBeritaAcaras
                                    on _sc.TransNmbr equals _ba.SalesConfirmationNoRef
                                 where _ba.Status >= BeritaAcaraDataMapper.GetStatus(TransStatus.Approved)
                                    && ((DateTime)_ba.ApprovedDate).AddDays(_dueDay) <= new DateTime(_prmDate.Year, _prmDate.Month, _prmDate.Day, 23, 59, 59)
                                    && (SqlMethods.Like(_sc.FgPending.ToString().Trim(), _pattern1.ToString().Trim()))
                                    && !(
                                            from _con in this.db.BILTrContracts
                                            where _con.SalesConfirmationNoRef == _sc.TransNmbr
                                                && (_con.Status == ContractDataMapper.GetStatus(TransStatus.Approved))
                                            select _con.SalesConfirmationNoRef
                                        ).Contains(_sc.TransNmbr)
                                 orderby ((DateTime)_ba.ApprovedDate).AddDays(_dueDay) descending
                                 select _sc.TransNmbr
                            );

                foreach (String _item in _query)
                {
                    if (_result == "")
                    {
                        _result = _item;
                    }
                    else
                    {
                        _result += "," + _item;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String GetListCLSoftBlock(DateTime _prmDate, String _prmStatusBlock)
        {
            String _pattern1 = "";

            if (_prmStatusBlock.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else if (_prmStatusBlock.Trim() == "0")
            {
                _pattern1 = "false";
            }
            else if (_prmStatusBlock.Trim() == "1")
            {
                _pattern1 = "true";
            }

            String _result = "";

            try
            {
                int _dueDay = new EmailNotificationSetupBL().GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.CLSoftBlock)).Days;

                var _query = (
                                 from _sc in this.db.BILTrSalesConfirmations
                                 join _ba in this.db.BILTrBeritaAcaras
                                    on _sc.TransNmbr equals _ba.SalesConfirmationNoRef
                                 join _con in this.db.BILTrContracts
                                    on _sc.TransNmbr equals _con.SalesConfirmationNoRef
                                 where _ba.Status >= BeritaAcaraDataMapper.GetStatus(TransStatus.Approved)
                                    && ((DateTime)_ba.ApprovedDate).AddDays(_dueDay) <= new DateTime(_prmDate.Year, _prmDate.Month, _prmDate.Day, 23, 59, 59)
                                    && (SqlMethods.Like(_sc.FgSoftBlockExec.ToString().Trim(), _pattern1.ToString().Trim()))
                                    && _con.Status == ContractDataMapper.GetStatus(TransStatus.Approved)
                                 orderby ((DateTime)_ba.ApprovedDate).AddDays(_dueDay) descending
                                 select _sc.TransNmbr
                            );

                foreach (String _item in _query)
                {
                    if (_result == "")
                    {
                        _result = _item;
                    }
                    else
                    {
                        _result += "," + _item;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<BILTrSalesConfirmation> GetListCLNotYetApproved(int _prmReqPage, int _prmPageSize, DateTime _prmDate, String _prmStatus, EmailNotificationID _prmCLType)
        {
            String _pattern1 = "";

            if (_prmStatus.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else if (_prmStatus.Trim() == "0")
            {
                _pattern1 = "false";
            }
            else if (_prmStatus.Trim() == "1")
            {
                _pattern1 = "true";
            }

            List<BILTrSalesConfirmation> _result = new List<BILTrSalesConfirmation>();

            try
            {
                int _dueDay = new EmailNotificationSetupBL().GetSingle(EmailNotificationDataMapper.GetID(_prmCLType)).Days;

                var _query = (
                                 from _sc in this.db.BILTrSalesConfirmations
                                 join _ba in this.db.BILTrBeritaAcaras
                                    on _sc.TransNmbr equals _ba.SalesConfirmationNoRef
                                 where _ba.Status >= BeritaAcaraDataMapper.GetStatus(TransStatus.Approved)
                                    && ((DateTime)_ba.ApprovedDate).AddDays(_dueDay) <= new DateTime(_prmDate.Year, _prmDate.Month, _prmDate.Day, 23, 59, 59)
                                    && (SqlMethods.Like(_sc.FgPending.ToString().Trim(), _pattern1.ToString().Trim()))
                                    && !(
                                            from _con in this.db.BILTrContracts
                                            where _con.SalesConfirmationNoRef == _sc.TransNmbr
                                                && (_con.Status == ContractDataMapper.GetStatus(TransStatus.Approved))
                                            select _con.SalesConfirmationNoRef
                                        ).Contains(_sc.TransNmbr)
                                 orderby ((DateTime)_ba.ApprovedDate).AddDays(_dueDay) descending
                                 select new
                                 {
                                     TransNmbr = _sc.TransNmbr,
                                     FileNmbr = _sc.FileNmbr,
                                     TransDate = _sc.TransDate,
                                     BAFileNmbr = _ba.FileNmbr,
                                     BATransDate = (DateTime)_ba.TransDate,
                                     CompanyName = _sc.CompanyName,
                                     StatusPending = _sc.FgPending,
                                     FgNotify = _sc.FgNotifiedCL
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILTrSalesConfirmation(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.BAFileNmbr, _row.BATransDate, _row.CompanyName, _row.StatusPending, _row.FgNotify));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<BILTrSalesConfirmation> GetListCLSoftBlock(int _prmReqPage, int _prmPageSize, DateTime _prmDate, String _prmStatusBlock)
        {
            String _pattern1 = "";

            if (_prmStatusBlock.Trim() == "99")
            {
                _pattern1 = "%";
            }
            else if (_prmStatusBlock.Trim() == "0")
            {
                _pattern1 = "false";
            }
            else if (_prmStatusBlock.Trim() == "1")
            {
                _pattern1 = "true";
            }

            List<BILTrSalesConfirmation> _result = new List<BILTrSalesConfirmation>();

            try
            {
                int _dueDay = new EmailNotificationSetupBL().GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.CLSoftBlock)).Days;

                var _query = (
                                 from _sc in this.db.BILTrSalesConfirmations
                                 join _ba in this.db.BILTrBeritaAcaras
                                    on _sc.TransNmbr equals _ba.SalesConfirmationNoRef
                                 join _con in this.db.BILTrContracts
                                    on _sc.TransNmbr equals _con.SalesConfirmationNoRef
                                 where _ba.Status >= BeritaAcaraDataMapper.GetStatus(TransStatus.Approved)
                                    && ((DateTime)_ba.ApprovedDate).AddDays(_dueDay) <= new DateTime(_prmDate.Year, _prmDate.Month, _prmDate.Day, 23, 59, 59)
                                    && (SqlMethods.Like(_sc.FgSoftBlockExec.ToString().Trim(), _pattern1.ToString().Trim()))
                                    && _con.Status == ContractDataMapper.GetStatus(TransStatus.Approved)
                                 orderby ((DateTime)_ba.ApprovedDate).AddDays(_dueDay) descending
                                 select new
                                 {
                                     TransNmbr = _sc.TransNmbr,
                                     FileNmbr = _sc.FileNmbr,
                                     TransDate = _sc.TransDate,
                                     CompanyName = _sc.CompanyName,
                                     Remark = _sc.Remark,
                                     FgSoftBlockExec = _sc.FgSoftBlockExec,
                                     FgPending = _sc.FgPending,
                                     FgNotify = _sc.FgNotifiedBA
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new BILTrSalesConfirmation(_row.TransNmbr, _row.FileNmbr, _row.TransDate, _row.CompanyName, _row.Remark, _row.FgSoftBlockExec, _row.FgPending, _row.FgNotify));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        #endregion CL

        #region Pembayaran
        public double RowCountNotYetPay(int _prmPeriod, int _prmYear, DateTime _prmDueDate, String _prmCustName, String _prmCustType)
        {
            int _result = 0;
            String _pattern1 = "";
            String _pattern2 = "";

            if (_prmCustName.Trim() == "")
            {
                _pattern1 = "%";
            }
            else
            {
                _pattern1 = "%" + _prmCustName + "%";
            }

            if (_prmCustType.Trim() == "null")
            {
                _pattern2 = "%";
            }
            else
            {
                _pattern2 = "%" + _prmCustType + "%";
            }

            try
            {
                _result = (
                             from _msCust in this.db.MsCustomers
                             join _msCustBillAcc in this.db.Master_CustBillAccounts
                                on _msCust.CustCode equals _msCustBillAcc.CustCode
                             where (
                                        from _ar in this.db.FINARPostings
                                        where (
                                                (
                                                    (
                                                        from _billInvHd in this.db.Billing_InvoiceHds
                                                        where _billInvHd.DueDate < DateTime.Now
                                                        select _billInvHd.TransNmbr
                                                    ).Union(
                                                        from _billCustInvHd in this.db.Billing_CustomerInvoiceHds
                                                        where _billCustInvHd.DueDate < DateTime.Now
                                                        select _billCustInvHd.TransNmbr
                                                    )
                                                ).Distinct()
                                              ).Contains(_ar.InvoiceNo)
                                            && _ar.Amount > _ar.Balance
                                        select _ar.CustCode
                                    ).Contains(_msCust.CustCode)
                                && _msCust.FgActive == 'Y'
                                && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_msCust.CustType, _pattern2))
                                && (_msCustBillAcc.FgSoftBlock ?? false) == false
                             select _msCust.CustCode
                        ).Count();
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String GetListNotYetPay(int _prmPeriod, int _prmYear, DateTime _prmDueDate, String _prmCustName, String _prmCustType)
        {
            String _pattern1 = "";
            String _pattern2 = "";

            if (_prmCustName.Trim() == "")
            {
                _pattern1 = "%";
            }
            else
            {
                _pattern1 = "%" + _prmCustName + "%";
            }

            if (_prmCustType.Trim() == "null")
            {
                _pattern2 = "%";
            }

            String _result = "";

            try
            {
                var _query = (
                                 from _msCust in this.db.MsCustomers
                                 join _msCustBillAcc in this.db.Master_CustBillAccounts
                                    on _msCust.CustCode equals _msCustBillAcc.CustCode
                                 where (
                                            from _ar in this.db.FINARPostings
                                            where (
                                                    (
                                                        (
                                                            from _billInvHd in this.db.Billing_InvoiceHds
                                                            where _billInvHd.DueDate < DateTime.Now
                                                            select _billInvHd.TransNmbr
                                                        ).Union(
                                                            from _billCustInvHd in this.db.Billing_CustomerInvoiceHds
                                                            where _billCustInvHd.DueDate < DateTime.Now
                                                            select _billCustInvHd.TransNmbr
                                                        )
                                                    ).Distinct()
                                                  ).Contains(_ar.InvoiceNo)
                                                && _ar.Amount > _ar.Balance
                                            select _ar.CustCode
                                        ).Contains(_msCust.CustCode)
                                    && _msCust.FgActive == 'Y'
                                    && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msCust.CustType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (_msCustBillAcc.FgSoftBlock ?? false) == false
                                 select _msCust.CustCode
                            );

                foreach (String _item in _query)
                {
                    if (_result == "")
                    {
                        _result = _item;
                    }
                    else
                    {
                        _result += "," + _item;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsCustomer> GetListNotYetPay(int _prmReqPage, int _prmPageSize, int _prmPeriod, int _prmYear, DateTime _prmDueDate, String _prmCustName, String _prmCustType)
        {
            String _pattern1 = "";
            String _pattern2 = "";

            if (_prmCustName.Trim() == "")
            {
                _pattern1 = "%";
            }
            else
            {
                _pattern1 = "%" + _prmCustName + "%";
            }

            if (_prmCustType.Trim() == "null")
            {
                _pattern2 = "%";
            }
            else
            {
                _pattern2 = "%" + _prmCustType + "%";
            }

            List<MsCustomer> _result = new List<MsCustomer>();

            try
            {
                int _dueDay = new EmailNotificationSetupBL().GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.CLSoftBlock)).Days;

                var _query = (
                                 from _msCust in this.db.MsCustomers
                                 join _msCustBillAcc in this.db.Master_CustBillAccounts
                                    on _msCust.CustCode equals _msCustBillAcc.CustCode
                                 orderby _msCust.CustName
                                 where (
                                            from _ar in this.db.FINARPostings
                                            where (
                                                    (
                                                        (
                                                            from _billInvHd in this.db.Billing_InvoiceHds
                                                            where _billInvHd.DueDate < DateTime.Now
                                                            select _billInvHd.TransNmbr
                                                        ).Union(
                                                            from _billCustInvHd in this.db.Billing_CustomerInvoiceHds
                                                            where _billCustInvHd.DueDate < DateTime.Now
                                                            select _billCustInvHd.TransNmbr
                                                        )
                                                    ).Distinct()
                                                  ).Contains(_ar.InvoiceNo)
                                                && _ar.Amount > _ar.Balance
                                            select _ar.CustCode
                                        ).Contains(_msCust.CustCode)
                                    && _msCust.FgActive == 'Y'
                                    && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msCust.CustType, _pattern2))
                                    && (_msCustBillAcc.FgSoftBlock ?? false) == false
                                 select new
                                 {
                                     CustCode = _msCust.CustCode,
                                     CustName = _msCust.CustName,
                                     CustType = _msCust.CustType,
                                     CustBillAccount = _msCustBillAcc.CustBillAccount,
                                     ContactName = _msCust.ContactName,
                                     CustomerEmail = _msCust.Email,
                                     CustBillDescription = _msCustBillAcc.CustBillDescription
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustomer(_row.CustCode, _row.CustName, _row.CustType, _row.CustBillAccount, _row.ContactName, _row.CustomerEmail, _row.CustBillDescription));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public double RowCountNotYetPayOpen(int _prmPeriod, int _prmYear, DateTime _prmDueDate, String _prmCustName, String _prmCustType)
        {
            int _result = 0;
            String _pattern1 = "";
            String _pattern2 = "";

            if (_prmCustName.Trim() == "")
            {
                _pattern1 = "%";
            }
            else
            {
                _pattern1 = "%" + _prmCustName + "%";
            }

            if (_prmCustType.Trim() == "null")
            {
                _pattern2 = "%";
            }

            try
            {
                _result = (
                             from _msCust in this.db.MsCustomers
                             join _msCustBillAcc in this.db.Master_CustBillAccounts
                                on _msCust.CustCode equals _msCustBillAcc.CustCode
                             where (
                                        from _ar in this.db.FINARPostings
                                        where (
                                                (
                                                    (
                                                        from _billInvHd in this.db.Billing_InvoiceHds
                                                        where _billInvHd.DueDate < DateTime.Now
                                                        select _billInvHd.TransNmbr
                                                    ).Union(
                                                        from _billCustInvHd in this.db.Billing_CustomerInvoiceHds
                                                        where _billCustInvHd.DueDate < DateTime.Now
                                                        select _billCustInvHd.TransNmbr
                                                    )
                                                ).Distinct()
                                              ).Contains(_ar.InvoiceNo)
                                            && _ar.Amount > _ar.Balance
                                        select _ar.CustCode
                                    ).Contains(_msCust.CustCode)
                                && _msCust.FgActive == 'Y'
                                && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_msCust.CustType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && (_msCustBillAcc.FgSoftBlock ?? false) == true
                             select _msCust.CustCode
                        ).Count();
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String GetListNotYetPayOpen(int _prmPeriod, int _prmYear, DateTime _prmDueDate, String _prmCustName, String _prmCustType)
        {
            String _pattern1 = "";
            String _pattern2 = "";

            if (_prmCustName.Trim() == "")
            {
                _pattern1 = "%";
            }
            else
            {
                _pattern1 = "%" + _prmCustName + "%";
            }

            if (_prmCustType.Trim() == "null")
            {
                _pattern2 = "%";
            }

            String _result = "";

            try
            {
                var _query = (
                                 from _msCust in this.db.MsCustomers
                                 join _msCustBillAcc in this.db.Master_CustBillAccounts
                                    on _msCust.CustCode equals _msCustBillAcc.CustCode
                                 where (
                                            from _ar in this.db.FINARPostings
                                            where (
                                                    (
                                                        (
                                                            from _billInvHd in this.db.Billing_InvoiceHds
                                                            where _billInvHd.DueDate < DateTime.Now
                                                            select _billInvHd.TransNmbr
                                                        ).Union(
                                                            from _billCustInvHd in this.db.Billing_CustomerInvoiceHds
                                                            where _billCustInvHd.DueDate < DateTime.Now
                                                            select _billCustInvHd.TransNmbr
                                                        )
                                                    ).Distinct()
                                                  ).Contains(_ar.InvoiceNo)
                                                && _ar.Amount > _ar.Balance
                                            select _ar.CustCode
                                        ).Contains(_msCust.CustCode)
                                    && _msCust.FgActive == 'Y'
                                    && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msCust.CustType.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                    && (_msCustBillAcc.FgSoftBlock ?? false) == true
                                 select _msCust.CustCode
                            );

                foreach (String _item in _query)
                {
                    if (_result == "")
                    {
                        _result = _item;
                    }
                    else
                    {
                        _result += "," + _item;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<MsCustomer> GetListNotYetPayOpen(int _prmReqPage, int _prmPageSize, int _prmPeriod, int _prmYear, DateTime _prmDueDate, String _prmCustName, String _prmCustType)
        {
            String _pattern1 = "";
            String _pattern2 = "";

            if (_prmCustName.Trim() == "")
            {
                _pattern1 = "%";
            }
            else
            {
                _pattern1 = "%" + _prmCustName + "%";
            }

            if (_prmCustType.Trim() == "null")
            {
                _pattern2 = "%";
            }
            else
            {
                _pattern2 = "%" + _prmCustType + "%";
            }

            List<MsCustomer> _result = new List<MsCustomer>();

            try
            {
                int _dueDay = new EmailNotificationSetupBL().GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.CLSoftBlock)).Days;

                var _query = (
                                 from _msCust in this.db.MsCustomers
                                 join _msCustBillAcc in this.db.Master_CustBillAccounts
                                    on _msCust.CustCode equals _msCustBillAcc.CustCode
                                 orderby _msCust.CustName
                                 where (
                                            from _ar in this.db.FINARPostings
                                            where (
                                                    (
                                                        (
                                                            from _billInvHd in this.db.Billing_InvoiceHds
                                                            where _billInvHd.DueDate < DateTime.Now
                                                            select _billInvHd.TransNmbr
                                                        ).Union(
                                                            from _billCustInvHd in this.db.Billing_CustomerInvoiceHds
                                                            where _billCustInvHd.DueDate < DateTime.Now
                                                            select _billCustInvHd.TransNmbr
                                                        )
                                                    ).Distinct()
                                                  ).Contains(_ar.InvoiceNo)
                                                && _ar.Amount > _ar.Balance
                                            select _ar.CustCode
                                        ).Contains(_msCust.CustCode)
                                    && _msCust.FgActive == 'Y'
                                    && (SqlMethods.Like(_msCust.CustName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                    && (SqlMethods.Like(_msCust.CustType, _pattern2))
                                    && (_msCustBillAcc.FgSoftBlock ?? false) == true
                                 select new
                                 {
                                     CustCode = _msCust.CustCode,
                                     CustName = _msCust.CustName,
                                     CustType = _msCust.CustType,
                                     CustBillAccount = _msCustBillAcc.CustBillAccount,
                                     ContactName = _msCust.ContactName,
                                     CustomerEmail = _msCust.Email,
                                     CustBillDescription = _msCustBillAcc.CustBillDescription
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsCustomer(_row.CustCode, _row.CustName, _row.CustType, _row.CustBillAccount, _row.ContactName, _row.CustomerEmail, _row.CustBillDescription));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        #endregion Pembayaran

        #region BillingInvoiceEmailNotYetSend
        public double RowCountBilInvEmailNYSend(int _prmPeriod, int _prmYear, String _prmInvoiceNo, String _prmCustName)
        {
            int _result = 0;
            String _pattern1 = "";
            String _pattern2 = "";
            String _pattern3 = "";
            String _pattern4 = "";

            if (_prmCustName.Trim() == "")
            {
                _pattern1 = "%";
            }
            else
            {
                _pattern1 = "%" + _prmCustName + "%";
            }
            if (_prmInvoiceNo.Trim() == "")
            {
                _pattern2 = "%";
            }
            else
            {
                _pattern2 = "%" + _prmInvoiceNo + "%";
            }
            if (_prmPeriod.ToString().Trim() == "")
            {
                _pattern3 = "%";
            }
            else
            {
                _pattern3 = "%" + _prmPeriod + "%";
            }
            if (_prmYear.ToString().Trim() == "")
            {
                _pattern4 = "%";
            }
            else
            {
                _pattern4 = "%" + _prmYear + "%";
            }

            try
            {
                _result = (
                             from _bilInvHd in this.db.Billing_InvoiceHds
                             join _msCust in this.db.MsCustomers
                                on _bilInvHd.CustCode equals _msCust.CustCode
                             where (SqlMethods.Like(_msCust.CustName.Trim(), _pattern1.Trim()))
                                && (SqlMethods.Like(_bilInvHd.TransNmbr.ToString().Trim(), _pattern2.Trim()))
                                && (SqlMethods.Like(_bilInvHd.Period.ToString().Trim(), _pattern3.Trim()))
                                && (SqlMethods.Like(_bilInvHd.Year.ToString().Trim(), _pattern4.Trim()))
                                && _bilInvHd.FgSendEmail == false
                             select _bilInvHd.InvoiceHd
                        ).Count();
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public String GetListBilInvEmailNYSend(int _prmPeriod, int _prmYear, String _prmInvoiceNo, String _prmCustName)
        {
            String _result = "";
            String _pattern1 = "";
            String _pattern2 = "";
            String _pattern3 = "";
            String _pattern4 = "";

            if (_prmCustName.Trim() == "")
            {
                _pattern1 = "%";
            }
            else
            {
                _pattern1 = "%" + _prmCustName + "%";
            }
            if (_prmInvoiceNo.Trim() == "")
            {
                _pattern2 = "%";
            }
            else
            {
                _pattern2 = "%" + _prmInvoiceNo + "%";
            }
            if (_prmPeriod.ToString().Trim() == "")
            {
                _pattern3 = "%";
            }
            else
            {
                _pattern3 = "%" + _prmPeriod + "%";
            }
            if (_prmYear.ToString().Trim() == "")
            {
                _pattern4 = "%";
            }
            else
            {
                _pattern4 = "%" + _prmYear + "%";
            }

            try
            {
                var _query = (
                                 from _bilInvHd in this.db.Billing_InvoiceHds
                                 join _msCust in this.db.MsCustomers
                                    on _bilInvHd.CustCode equals _msCust.CustCode
                                 where (SqlMethods.Like(_msCust.CustName.Trim(), _pattern1.Trim()))
                                    && (SqlMethods.Like(_bilInvHd.TransNmbr.ToString().Trim(), _pattern2.Trim()))
                                    && (SqlMethods.Like(_bilInvHd.Period.ToString().Trim(), _pattern3.Trim()))
                                    && (SqlMethods.Like(_bilInvHd.Year.ToString().Trim(), _pattern4.Trim()))
                                 select _bilInvHd.InvoiceHd.ToString()
                            );

                foreach (String _item in _query)
                {
                    if (_result == "")
                    {
                        _result = _item;
                    }
                    else
                    {
                        _result += "," + _item;
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }

        public List<Billing_InvoiceHd> GetListBilInvEmailNYSend(int _prmReqPage, int _prmPageSize, int _prmPeriod, int _prmYear, String _prmInvoiceNo, String _prmCustName)
        {
            List<Billing_InvoiceHd> _result = new List<Billing_InvoiceHd>();
            String _pattern1 = "";
            String _pattern2 = "";
            String _pattern3 = "";
            String _pattern4 = "";

            if (_prmCustName.Trim() == "")
            {
                _pattern1 = "%";
            }
            else
            {
                _pattern1 = "%" + _prmCustName + "%";
            }
            if (_prmInvoiceNo.Trim() == "")
            {
                _pattern2 = "%";
            }
            else
            {
                _pattern2 = "%" + _prmInvoiceNo + "%";
            }
            if (_prmPeriod.ToString().Trim() == "")
            {
                _pattern3 = "%";
            }
            else
            {
                _pattern3 = "%" + _prmPeriod + "%";
            }
            if (_prmYear.ToString().Trim() == "")
            {
                _pattern4 = "%";
            }
            else
            {
                _pattern4 = "%" + _prmYear + "%";
            }

            try
            {
                var _query = (
                                 from _bilInvHd in this.db.Billing_InvoiceHds
                                 join _msCust in this.db.MsCustomers
                                    on _bilInvHd.CustCode equals _msCust.CustCode
                                 where (SqlMethods.Like(_msCust.CustName.Trim(), _pattern1.Trim()))
                                    && (SqlMethods.Like(_bilInvHd.TransNmbr.ToString().Trim(), _pattern2.Trim()))
                                    && (SqlMethods.Like(_bilInvHd.Period.ToString().Trim(), _pattern3.Trim()))
                                    && (SqlMethods.Like(_bilInvHd.Year.ToString().Trim(), _pattern4.Trim()))
                                    && _bilInvHd.FgSendEmail == false
                                 select new
                                 {
                                     InvoiceHd = _bilInvHd.InvoiceHd,
                                     TransNmbr = _bilInvHd.TransNmbr,
                                     Period = _bilInvHd.Period,
                                     Year = _bilInvHd.Year,
                                     CustName = _msCust.CustName,
                                     CustomerEmail = _msCust.Email
                                 }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Billing_InvoiceHd(_row.InvoiceHd, _row.TransNmbr, _row.Period, _row.Year, _row.CustName, _row.CustomerEmail));
                }
            }
            catch (Exception ex)
            {
            }

            return _result;
        }
        #endregion

        public String SendEmail(String _prmCode, EmailNotificationID _prmSubType)
        {
            String _result = "";
            Boolean _errorExist = false;
            String _errCodes = "";
            String _exErr = "";
            String _exStackErr = "";

            EmailNotificationSetupBL _emailSetupBL = new EmailNotificationSetupBL();
            SalesConfirmationBL _scBL = new SalesConfirmationBL();
            BeritaAcaraBL _baBL = new BeritaAcaraBL();
            ContractBL _contractBL = new ContractBL();
            CustomerBL _custBL = new CustomerBL();
            BillingInvoiceBL _bilInvBL = new BillingInvoiceBL();
            
            String[] _split = _prmCode.Split(',');

            for (int i = 0; i < _split.Length; i++)
            {
                String[] _codes = _split[i].Split('=');

                String _code = _codes[0];

                if (_prmSubType == EmailNotificationID.SPKInstallasi)
                {
                    #region SPK Installasi
                    #endregion SPK Installasi
                }
                else if (_prmSubType == EmailNotificationID.BANotYetApproved)
                {
                    #region BAApproved
                    BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.BANotYetApproved));
                    BILMsEmailNotificationSetup _emailSetupInternal = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.BANotYetApprovedInternal));

                    try
                    {
                        BILTrSalesConfirmation _sc = _scBL.GetSingleSalesConfirmation(_code);
                        //send mail to external
                        MailMessage _msgMail = new MailMessage();
                        _msgMail.To.Add(new MailAddress(_sc.ResponsibleEmailAddress));
                        _msgMail.To.Add(new MailAddress(_sc.TechnicalEmail));
                        _msgMail.To.Add(new MailAddress(_sc.TechnicalEmail2));
                        _msgMail.From = new MailAddress(_emailSetup.EmailFrom);
                        _msgMail.Subject = _emailSetup.Subject;
                        _msgMail.IsBodyHtml = true;

                        BILMsEmailNotificationSetup _emailSetupBlock = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.BASoftBlock));
                        DateTime _tglBlock = new DateTime(_sc.TransDate.Year, _sc.TransDate.Month, _sc.TransDate.Day, 0, 0, 0).AddDays((int)_sc.TargetInstallationDay + _emailSetupBlock.Days);
                        String _oldBody = _emailSetup.BodyMessage ?? "";
                        String _newBody = _oldBody.Replace("--CUSTOMERNAME--", _sc.CompanyName);
                        _newBody = _newBody.Replace("--TechnicalResponsibleName--", _sc.TechnicalName);
                        _newBody = _newBody.Replace("--SCNO--", _sc.FileNmbr);
                        _newBody = _newBody.Replace("--TGLBLOCK--", _tglBlock.ToString("dd/MM/yyyy"));
                        _msgMail.Body = _newBody;

                        SmtpClient _smtp = new SmtpClient();
                        _smtp.Send(_msgMail);

                        //send mail to internal
                        MailMessage _msgMailInternal = new MailMessage();
                        _msgMailInternal.To.Add(new MailAddress(_emailSetupInternal.EmailTo));
                        _msgMailInternal.From = new MailAddress(_emailSetupInternal.EmailFrom);
                        _msgMailInternal.Subject = _emailSetupInternal.Subject;
                        _msgMailInternal.IsBodyHtml = true;

                        BILTrSalesConfirmation _scInternal = _scBL.GetSingleSalesConfirmation(_code);
                        DateTime _tglTargetSC = new DateTime(_sc.TransDate.Year, _sc.TransDate.Month, _sc.TransDate.Day, 0, 0, 0).AddDays((int)_sc.TargetInstallationDay);
                        String _oldBodyInternal = _emailSetupInternal.BodyMessage ?? "";
                        String _newBodyInternal = _oldBodyInternal.Replace("--CUSTOMERNAME--", _scInternal.CompanyName);
                        _newBodyInternal = _newBodyInternal.Replace("--SCNO--", _sc.FileNmbr);
                        _newBodyInternal = _newBodyInternal.Replace("--TGLTARGETSC--", _tglTargetSC.ToString("dd/MM/yyyy"));
                        _newBodyInternal = _newBodyInternal.Replace("--DAYSSCBLOCK--", _emailSetupBlock.Days.ToString());
                        _msgMailInternal.Body = _newBodyInternal;

                        SmtpClient _smtpInternal = new SmtpClient();
                        _smtpInternal.Send(_msgMailInternal);

                        //update sc already notified
                        _scBL.UpdateFgNotifiedBA(_code, true);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;
                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    #endregion BAApproved
                }
                else if (_prmSubType == EmailNotificationID.BASoftBlock)
                {
                    #region BASoftBlock
                    BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.BASoftBlock));
                    BILMsEmailNotificationSetup _emailSetupInternal = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.SPKBASoftBlock));

                    try
                    {
                        BILTrSalesConfirmation _sc = _scBL.GetSingleSalesConfirmation(_code);
                        //send mail to external
                        MailMessage _msgMail = new MailMessage();
                        _msgMail.To.Add(new MailAddress(_sc.ResponsibleEmailAddress));
                        _msgMail.To.Add(new MailAddress(_sc.TechnicalEmail));
                        _msgMail.To.Add(new MailAddress(_sc.TechnicalEmail2));
                        _msgMail.From = new MailAddress(_emailSetup.EmailFrom);
                        _msgMail.Subject = _emailSetup.Subject;
                        _msgMail.IsBodyHtml = true;

                        DateTime _tglBlock = new DateTime(_sc.TransDate.Year, _sc.TransDate.Month, _sc.TransDate.Day, 0, 0, 0).AddDays((int)_sc.TargetInstallationDay + _emailSetup.Days);
                        String _oldBody = _emailSetup.BodyMessage ?? "";
                        String _newBody = _oldBody.Replace("--CUSTOMERNAME--", _sc.CompanyName);
                        _newBody = _newBody.Replace("--TechnicalResponsibleName--", _sc.TechnicalName);
                        _newBody = _newBody.Replace("--SCNO--", _sc.FileNmbr);
                        _newBody = _newBody.Replace("--TGLBLOCK--", _tglBlock.ToString("dd/MM/yyyy"));
                        _msgMail.Body = _newBody;

                        SmtpClient _smtp = new SmtpClient();
                        _smtp.Send(_msgMail);

                        //send mail to internal
                        MailMessage _msgMailInternal = new MailMessage();
                        _msgMailInternal.To.Add(new MailAddress(_emailSetupInternal.EmailTo));
                        _msgMailInternal.From = new MailAddress(_emailSetupInternal.EmailFrom);
                        _msgMailInternal.Subject = _emailSetupInternal.Subject;
                        _msgMailInternal.IsBodyHtml = true;

                        BILTrSalesConfirmation _scInternal = _scBL.GetSingleSalesConfirmation(_code);
                        //DateTime _tglBlock = new DateTime(_sc.TransDate.Year, _sc.TransDate.Month, _sc.TransDate.Day, 0, 0, 0).AddDays((int)_sc.TargetInstallationDay + _emailSetup.Days);
                        String _oldBodyInternal = _emailSetupInternal.BodyMessage ?? "";
                        String _newBodyInternal = _oldBodyInternal.Replace("--CUSTOMERNAME--", _scInternal.CompanyName);
                        _newBodyInternal = _newBodyInternal.Replace("--SCNO--", _sc.FileNmbr);
                        _newBodyInternal = _newBodyInternal.Replace("--TGLBLOCK--", _tglBlock.ToString("dd/MM/yyyy"));
                        _newBodyInternal = _newBodyInternal.Replace("--TGLBLOCK+1--", (_tglBlock.AddDays(1)).ToString("dd/MM/yyyy"));
                        _msgMailInternal.Body = _newBodyInternal;

                        SmtpClient _smtpInternal = new SmtpClient();
                        _smtpInternal.Send(_msgMailInternal);

                        //update sc already notified
                        _scBL.UpdateFgNotifiedSoftBlock(_code, true);
                        _scBL.UpdateFgSoftBlockExec(_code, true);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;
                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    #endregion BASoftBlock
                }
                else if (_prmSubType == EmailNotificationID.BAOpenSoftBlock)
                {
                    #region BAOpenSoftBlock
                    BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.BAOpenSoftBlock));
                    BILMsEmailNotificationSetup _emailSetupInternal = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.SPKBAOpenSoftBlock));

                    try
                    {
                        BILTrSalesConfirmation _sc = _scBL.GetSingleSalesConfirmation(_code);
                        //send mail to external
                        MailMessage _msgMail = new MailMessage();
                        _msgMail.To.Add(new MailAddress(_sc.ResponsibleEmailAddress));
                        _msgMail.To.Add(new MailAddress(_sc.TechnicalEmail));
                        _msgMail.To.Add(new MailAddress(_sc.TechnicalEmail2));
                        _msgMail.From = new MailAddress(_emailSetup.EmailFrom);
                        _msgMail.Subject = _emailSetup.Subject;
                        _msgMail.IsBodyHtml = true;

                        BILTrBeritaAcara _ba = _baBL.GetBeritaAcaraBySCCode(_code);
                        String _oldBody = _emailSetup.BodyMessage ?? "";
                        String _newBody = _oldBody.Replace("--CUSTOMERNAME--", _sc.CompanyName);
                        _newBody = _newBody.Replace("--TechnicalResponsibleName--", _sc.TechnicalName);
                        _newBody = _newBody.Replace("--BANo--", _ba.FileNmbr);
                        _msgMail.Body = _newBody;

                        SmtpClient _smtp = new SmtpClient();
                        _smtp.Send(_msgMail);

                        //send mail to internal
                        MailMessage _msgMailInternal = new MailMessage();
                        _msgMailInternal.To.Add(new MailAddress(_emailSetupInternal.EmailTo));
                        _msgMailInternal.From = new MailAddress(_emailSetupInternal.EmailFrom);
                        _msgMailInternal.Subject = _emailSetupInternal.Subject;
                        _msgMailInternal.IsBodyHtml = true;

                        BILTrSalesConfirmation _scInternal = _scBL.GetSingleSalesConfirmation(_code);
                        String _oldBodyInternal = _emailSetupInternal.BodyMessage ?? "";
                        String _newBodyInternal = _oldBodyInternal.Replace("--CUSTOMERNAME--", _scInternal.CompanyName);
                        _newBodyInternal = _newBodyInternal.Replace("--SCNO--", _sc.FileNmbr);
                        _newBodyInternal = _newBodyInternal.Replace("--TGLOPENBA--", ((DateTime)_ba.ApprovedDate).AddDays(1).ToString("dd/MM/yyyy"));
                        _msgMailInternal.Body = _newBodyInternal;

                        SmtpClient _smtpInternal = new SmtpClient();
                        _smtpInternal.Send(_msgMailInternal);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;
                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    #endregion BAOpenSoftBlock
                }
                else if (_prmSubType == EmailNotificationID.ContractLetterNotYetApproved)
                {
                    #region ContractApproved
                    BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.ContractLetterNotYetApproved));
                    BILMsEmailNotificationSetup _emailSetupBlock = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.BASoftBlock));

                    try
                    {
                        BILTrSalesConfirmation _sc = _scBL.GetSingleSalesConfirmation(_code);
                        //send mail to external
                        MailMessage _msgMail = new MailMessage();
                        _msgMail.To.Add(new MailAddress(_sc.ResponsibleEmailAddress));
                        _msgMail.To.Add(new MailAddress(_sc.TechnicalEmail));
                        _msgMail.To.Add(new MailAddress(_sc.TechnicalEmail2));
                        _msgMail.From = new MailAddress(_emailSetup.EmailFrom);
                        _msgMail.Subject = _emailSetup.Subject;
                        _msgMail.IsBodyHtml = true;

                        BILTrBeritaAcara _ba = _baBL.GetBeritaAcaraBySCCode(_code);
                        String _oldBody = _emailSetup.BodyMessage ?? "";
                        String _newBody = _oldBody.Replace("--CUSTOMERNAME--", _sc.CompanyName);
                        _newBody = _newBody.Replace("--TechnicalResponsibleName--", _sc.TechnicalName);
                        _newBody = _newBody.Replace("--SCNO--", _sc.FileNmbr);
                        _newBody = _newBody.Replace("--BANo--", _ba.FileNmbr);
                        _newBody = _newBody.Replace("--TGLTARGETBA--", (((DateTime)_ba.ApprovedDate).AddDays(_emailSetup.Days)).ToString("dd/MM/yyyy"));
                        _newBody = _newBody.Replace("--TGLBLOCKBA--", (((DateTime)_ba.ApprovedDate).AddDays(_emailSetupBlock.Days)).ToString("dd/MM/yyyy"));
                        _msgMail.Body = _newBody;

                        SmtpClient _smtp = new SmtpClient();
                        _smtp.Send(_msgMail);

                        //update sc already notified
                        _scBL.UpdateFgNotifiedCL(_code, true);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;
                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    #endregion ContractApproved
                }
                else if (_prmSubType == EmailNotificationID.ContractLetterNotYetApprovedInternal)
                {
                    #region ContractApprovedInternal
                    BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.ContractLetterNotYetApprovedInternal));

                    try
                    {
                        //send mail to internal
                        MailMessage _msgMail = new MailMessage();
                        _msgMail.To.Add(new MailAddress(_emailSetup.EmailTo));
                        _msgMail.From = new MailAddress(_emailSetup.EmailFrom);
                        _msgMail.Subject = _emailSetup.Subject;
                        _msgMail.IsBodyHtml = true;

                        BILTrSalesConfirmation _sc = _scBL.GetSingleSalesConfirmation(_code);
                        BILTrBeritaAcara _ba = _baBL.GetBeritaAcaraBySCCode(_code);
                        String _oldBody = _emailSetup.BodyMessage ?? "";
                        String _newBody = _oldBody.Replace("--CUSTOMERNAME--", _sc.CompanyName);
                        _newBody = _newBody.Replace("--SCNO--", _sc.FileNmbr);
                        _newBody = _newBody.Replace("--BANo--", _ba.FileNmbr);
                        _msgMail.Body = _newBody;

                        SmtpClient _smtp = new SmtpClient();
                        _smtp.Send(_msgMail);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;
                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    #endregion ContractApprovedInternal
                }
                else if (_prmSubType == EmailNotificationID.CLSoftBlock)
                {
                    #region ContractSoftBlock
                    BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.CLSoftBlock));
                    BILMsEmailNotificationSetup _emailSetupInternal = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.SPKCLSoftBlock));
                    BILMsEmailNotificationSetup _emailSetupBA = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.BANotYetApproved));

                    try
                    {
                        BILTrSalesConfirmation _sc = _scBL.GetSingleSalesConfirmation(_code);
                        //send mail to external
                        MailMessage _msgMail = new MailMessage();
                        _msgMail.To.Add(new MailAddress(_sc.ResponsibleEmailAddress));
                        _msgMail.To.Add(new MailAddress(_sc.TechnicalEmail));
                        _msgMail.To.Add(new MailAddress(_sc.TechnicalEmail2));
                        _msgMail.From = new MailAddress(_emailSetup.EmailFrom);
                        _msgMail.Subject = _emailSetup.Subject;
                        _msgMail.IsBodyHtml = true;

                        BILTrBeritaAcara _ba = _baBL.GetBeritaAcaraBySCCode(_code);
                        String _oldBody = _emailSetup.BodyMessage ?? "";
                        String _newBody = _oldBody.Replace("--CUSTOMERNAME--", _sc.CompanyName);
                        _newBody = _newBody.Replace("--TechnicalResponsibleName--", _sc.TechnicalName);
                        _newBody = _newBody.Replace("--SCNO--", _sc.FileNmbr);
                        _newBody = _newBody.Replace("--BANo--", _ba.FileNmbr);
                        _newBody = _newBody.Replace("--TGLTARGETBA--", (((DateTime)_ba.ApprovedDate).AddDays(_emailSetupBA.Days)).ToString("dd/MM/yyyy"));
                        _msgMail.Body = _newBody;

                        SmtpClient _smtp = new SmtpClient();
                        _smtp.Send(_msgMail);

                        //send mail to internal
                        MailMessage _msgMailInternal = new MailMessage();
                        _msgMailInternal.To.Add(new MailAddress(_emailSetupInternal.EmailTo));
                        _msgMailInternal.From = new MailAddress(_emailSetupInternal.EmailFrom);
                        _msgMailInternal.Subject = _emailSetupInternal.Subject;
                        _msgMailInternal.IsBodyHtml = true;

                        BILTrSalesConfirmation _scInternal = _scBL.GetSingleSalesConfirmation(_code);
                        String _oldBodyInternal = _emailSetupInternal.BodyMessage ?? "";
                        String _newBodyInternal = _oldBodyInternal.Replace("--CUSTOMERNAME--", _scInternal.CompanyName);
                        _newBodyInternal = _newBodyInternal.Replace("--TGLBLOCKCONTRACT--", (((DateTime)_ba.ApprovedDate).AddDays(_emailSetup.Days)).ToString("dd/MM/yyyy"));
                        _newBodyInternal = _newBodyInternal.Replace("--TGLBLOCKCONTRACT+1--", (((DateTime)_ba.ApprovedDate).AddDays(_emailSetup.Days + 1)).ToString("dd/MM/yyyy"));
                        _msgMailInternal.Body = _newBodyInternal;

                        SmtpClient _smtpInternal = new SmtpClient();
                        _smtpInternal.Send(_msgMailInternal);

                        //update sc already notified
                        _scBL.UpdateFgNotifiedSoftBlock(_code, true);
                        _scBL.UpdateFgSoftBlockExec(_code, true);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;
                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    #endregion ContractSoftBlock
                }
                else if (_prmSubType == EmailNotificationID.CLOpenSoftBlock)
                {
                    #region ContractOpenSoftBlock
                    BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.CLOpenSoftBlock));
                    BILMsEmailNotificationSetup _emailSetupInternal = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.SPKCLOpenSoftBlock));

                    try
                    {
                        BILTrSalesConfirmation _sc = _scBL.GetSingleSalesConfirmation(_code);
                        //send mail to external
                        MailMessage _msgMail = new MailMessage();
                        _msgMail.To.Add(new MailAddress(_sc.ResponsibleEmailAddress));
                        _msgMail.To.Add(new MailAddress(_sc.TechnicalEmail));
                        _msgMail.To.Add(new MailAddress(_sc.TechnicalEmail2));
                        _msgMail.From = new MailAddress(_emailSetup.EmailFrom);
                        _msgMail.Subject = _emailSetup.Subject;
                        _msgMail.IsBodyHtml = true;

                        BILTrBeritaAcara _ba = _baBL.GetBeritaAcaraBySCCode(_code);
                        BILTrContract _contract = _contractBL.GetContractBySCCode(_code);
                        String _oldBody = _emailSetup.BodyMessage ?? "";
                        String _newBody = _oldBody.Replace("--CUSTOMERNAME--", _sc.CompanyName);
                        _newBody = _newBody.Replace("--TechnicalResponsibleName--", _sc.TechnicalName);
                        _newBody = _newBody.Replace("--BANo--", _ba.FileNmbr);
                        _msgMail.Body = _newBody;

                        SmtpClient _smtp = new SmtpClient();
                        _smtp.Send(_msgMail);

                        //send mail to internal
                        MailMessage _msgMailInternal = new MailMessage();
                        _msgMailInternal.To.Add(new MailAddress(_emailSetupInternal.EmailTo));
                        _msgMailInternal.From = new MailAddress(_emailSetupInternal.EmailFrom);
                        _msgMailInternal.Subject = _emailSetupInternal.Subject;
                        _msgMailInternal.IsBodyHtml = true;

                        BILTrSalesConfirmation _scInternal = _scBL.GetSingleSalesConfirmation(_code);
                        String _oldBodyInternal = _emailSetupInternal.BodyMessage ?? "";
                        String _newBodyInternal = _oldBodyInternal.Replace("--CUSTOMERNAME--", _scInternal.CompanyName);
                        _newBodyInternal = _newBodyInternal.Replace("--SCNO--", _sc.FileNmbr);
                        _newBodyInternal = _newBodyInternal.Replace("--TGLOPENCONTRACT--", ((DateTime)_contract.ApprovedDate).AddDays(1).ToString("dd/MM/yyyy"));
                        _msgMailInternal.Body = _newBodyInternal;

                        SmtpClient _smtpInternal = new SmtpClient();
                        _smtpInternal.Send(_msgMailInternal);

                        //update sc already notified
                        _scBL.UpdateFgSoftBlockExec(_code, false);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;
                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    #endregion ContractOpenSoftBlock
                }
                else if (_prmSubType == EmailNotificationID.BillingInvoiceEmailNotYetSent)
                {
                    #region BillingInvoiceEmailNotYetSent
                    BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.BillingInvoiceEmailNotYetSent));

                    try
                    {
                        Billing_InvoiceHd _bilInv = _bilInvBL.GetSingleBillingInvoiceHd(new Guid(_code));
                        MsCustomer _msCust = _custBL.GetSingleCust(_bilInv.CustCode);
                        //send mail to external
                        MailMessage _msgMail = new MailMessage();
                        _msgMail.To.Add(new MailAddress(_msCust.Email));
                        _msgMail.From = new MailAddress(_emailSetup.EmailFrom);
                        //Attachment _attach = new Attachment();




                        //_msgMail.Attachments.Add(_attach);
                        _msgMail.Subject = _emailSetup.Subject;
                        _msgMail.IsBodyHtml = true;

                        BILTrBeritaAcara _ba = _baBL.GetBeritaAcaraBySCCode(_code);
                        BILTrContract _contract = _contractBL.GetContractBySCCode(_code);
                        String _oldBody = _emailSetup.BodyMessage ?? "";
                        //String _newBody = _oldBody.Replace("--CUSTOMERNAME--", _sc.CompanyName);
                        //_newBody = _newBody.Replace("--TechnicalResponsibleName--", _sc.TechnicalName);
                        //_newBody = _newBody.Replace("--BANo--", _ba.FileNmbr);
                        _msgMail.Body = _oldBody;

                        SmtpClient _smtp = new SmtpClient();
                        _smtp.Send(_msgMail);

                        //update bill invoice already notified
                        _bilInvBL.UpdateFgSendEmail(_code, true);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;
                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    #endregion ContractOpenSoftBlock
                }
            }

            if (_errorExist == true)
            {
                _result = "Cannot send email(s) from transaction " + _errCodes + " due to technical error";
                new ErrorLogBL().CreateErrorLog(_exErr, _exStackErr, HttpContext.Current.User.Identity.Name, "Send Email", "BIL");
            }

            return _result;
        }

        public String SendEmail(String _prmCode, EmailNotificationID _prmSubType, DateTime _prmDueDate)
        {
            String _result = "";
            Boolean _errorExist = false;
            String _errCodes = "";
            String _exErr = "";
            String _exStackErr = "";

            EmailNotificationSetupBL _emailSetupBL = new EmailNotificationSetupBL();
            CustomerBL _custBL = new CustomerBL();

            String[] _split = _prmCode.Split(',');

            for (int i = 0; i < _split.Length; i++)
            {
                String[] _codes = _split[i].Split('=');

                String _code = _codes[0];

                if (_prmSubType == EmailNotificationID.NotPay)
                {
                    #region PembayaranNotPay
                    try
                    {
                        BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.NotPay));
                        MsCustomer _msCust = _custBL.GetSingleCust(_code);
                        //send mail to external
                        MailMessage _msgMail = new MailMessage();
                        _msgMail.To.Add(new MailAddress(_msCust.Email));
                        _msgMail.From = new MailAddress(_emailSetup.EmailFrom);
                        _msgMail.Subject = _emailSetup.Subject;
                        _msgMail.IsBodyHtml = true;

                        String _oldBody = _emailSetup.BodyMessage ?? "";
                        String _newBody = _oldBody.Replace("--CUSTOMERNAME--", _msCust.CustName);
                        _newBody = _newBody.Replace("--PERIODYEAR--", MonthMapper.GetMonthName(_prmDueDate.Month) + " " + _prmDueDate.Year);
                        _msgMail.Body = _newBody;

                        SmtpClient _smtp = new SmtpClient();
                        _smtp.Send(_msgMail);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;
                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    #endregion PembayaranNotPay
                }
                else if (_prmSubType == EmailNotificationID.NotPaySoftBlock)
                {
                    #region PembayaranNotPaySoftBlock
                    try
                    {
                        BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.NotPaySoftBlock));
                        BILMsEmailNotificationSetup _emailSetupInternal = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.SPKNotPaySoftBlock));
                        MsCustomer _msCust = _custBL.GetSingleCust(_code);
                        //send mail to external
                        MailMessage _msgMail = new MailMessage();
                        _msgMail.To.Add(new MailAddress(_msCust.Email));
                        _msgMail.From = new MailAddress(_emailSetup.EmailFrom);
                        _msgMail.Subject = _emailSetup.Subject;
                        _msgMail.IsBodyHtml = true;

                        String _oldBody = _emailSetup.BodyMessage ?? "";
                        String _newBody = _oldBody.Replace("--CUSTOMERNAME--", _msCust.CustName);
                        _newBody = _newBody.Replace("--PERIODYEAR--", MonthMapper.GetMonthName(_prmDueDate.Month) + " " + _prmDueDate.Year);
                        _msgMail.Body = _newBody;

                        SmtpClient _smtp = new SmtpClient();
                        _smtp.Send(_msgMail);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;

                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    #endregion PembayaranNotPaySoftBlock
                }
                else if (_prmSubType == EmailNotificationID.NotPaySoftBlockInternal)
                {
                    #region PembayaranNotPaySoftBlockInternal
                    try
                    {
                        BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.NotPaySoftBlock));
                        BILMsEmailNotificationSetup _emailSetupInternal = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.SPKNotPaySoftBlock));
                        MsCustomer _msCust = _custBL.GetSingleCust(_code);

                        //send mail to internal
                        MailMessage _msgMailInternal = new MailMessage();
                        _msgMailInternal.To.Add(new MailAddress(_emailSetupInternal.EmailTo));
                        _msgMailInternal.From = new MailAddress(_emailSetupInternal.EmailFrom);
                        _msgMailInternal.Subject = _emailSetup.Subject;
                        _msgMailInternal.IsBodyHtml = true;

                        String _oldBodyInternal = _emailSetupInternal.BodyMessage ?? "";
                        String _newBodyInternal = _oldBodyInternal.Replace("--CUSTOMERNAME--", _msCust.CustName);
                        _newBodyInternal = _newBodyInternal.Replace("--PERIODYEAR--", MonthMapper.GetMonthName(_prmDueDate.Month) + " " + _prmDueDate.Year);
                        _msgMailInternal.Body = _newBodyInternal;

                        SmtpClient _smtpInternal = new SmtpClient();
                        _smtpInternal.Send(_msgMailInternal);

                        CustBillAccountBL _custBilAccBL = new CustBillAccountBL();
                        Guid _custBillCode = _custBilAccBL.GetCustBillCode(_codes[1]);
                        Master_CustBillAccount _msCustBillAcc = new CustBillAccountBL().GetSingleCustBillAccount(_custBillCode);
                        _msCustBillAcc.FgSoftBlock = true;
                        _custBilAccBL.EditCustBillAccount(_msCustBillAcc);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;
                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    #endregion PembayaranNotPaySoftBlockInternal
                }
                else if (_prmSubType == EmailNotificationID.NotPayOpenSoftBlock)
                {
                    #region PembayaranNotPayOpenSoftBlock
                    try
                    {
                        BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.NotPayOpenSoftBlock));
                        BILMsEmailNotificationSetup _emailSetupInternal = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.SPKNotPayOpenSoftBlock));
                        MsCustomer _msCust = _custBL.GetSingleCust(_code);
                        //send mail to external
                        MailMessage _msgMail = new MailMessage();
                        _msgMail.To.Add(new MailAddress(_msCust.Email));
                        _msgMail.From = new MailAddress(_emailSetup.EmailFrom);
                        _msgMail.Subject = _emailSetup.Subject;
                        _msgMail.IsBodyHtml = true;

                        String _oldBody = _emailSetup.BodyMessage ?? "";
                        String _newBody = _oldBody.Replace("--CUSTOMERNAME--", _msCust.CustName);
                        _newBody = _newBody.Replace("--PERIODYEAR--", MonthMapper.GetMonthName(_prmDueDate.Month) + " " + _prmDueDate.Year);
                        _msgMail.Body = _newBody;

                        SmtpClient _smtp = new SmtpClient();
                        _smtp.Send(_msgMail);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;
                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    
                    #endregion PembayaranNotPayOpenSoftBlock
                }
                else if (_prmSubType == EmailNotificationID.NotPayOpenSoftBlockInternal)
                {
                    #region PembayaranNotPayOpenSoftBlockInternal

                    try
                    {
                        BILMsEmailNotificationSetup _emailSetup = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.NotPayOpenSoftBlock));
                        BILMsEmailNotificationSetup _emailSetupInternal = _emailSetupBL.GetSingle(EmailNotificationDataMapper.GetID(EmailNotificationID.SPKNotPayOpenSoftBlock));
                        MsCustomer _msCust = _custBL.GetSingleCust(_code);
                        //send mail to internal
                        MailMessage _msgMailInternal = new MailMessage();
                        _msgMailInternal.To.Add(new MailAddress(_emailSetupInternal.EmailTo));
                        _msgMailInternal.From = new MailAddress(_emailSetupInternal.EmailFrom);
                        _msgMailInternal.Subject = _emailSetup.Subject;
                        _msgMailInternal.IsBodyHtml = true;

                        String _oldBodyInternal = _emailSetupInternal.BodyMessage ?? "";
                        String _newBodyInternal = _oldBodyInternal.Replace("--CUSTOMERNAME--", _msCust.CustName);
                        _newBodyInternal = _newBodyInternal.Replace("--PERIODYEAR--", MonthMapper.GetMonthName(_prmDueDate.Month) + " " + _prmDueDate.Year);
                        _msgMailInternal.Body = _newBodyInternal;

                        SmtpClient _smtpInternal = new SmtpClient();
                        _smtpInternal.Send(_msgMailInternal);

                        CustBillAccountBL _custBilAccBL = new CustBillAccountBL();
                        Guid _custBillCode = _custBilAccBL.GetCustBillCode(_codes[1]);
                        Master_CustBillAccount _msCustBillAcc = new CustBillAccountBL().GetSingleCustBillAccount(_custBillCode);
                        _msCustBillAcc.FgSoftBlock = false;
                        _custBilAccBL.EditCustBillAccount(_msCustBillAcc);
                    }
                    catch (Exception ex)
                    {
                        _errorExist = true;
                        _exErr = ex.Message;
                        _exStackErr = ex.StackTrace;
                        if (_errCodes == "")
                        {
                            _errCodes = _code;
                        }
                        else
                        {
                            _errCodes = _errCodes + ", " + _code;
                        }
                    }
                    
                    #endregion PembayaranNotPayOpenSoftBlockInternal
                }
            }

            if (_errorExist == true)
            {
                _result = "Cannot send email(s) from transaction " + _errCodes + " due to technical error";
                new ErrorLogBL().CreateErrorLog(_exErr, _exStackErr, HttpContext.Current.User.Identity.Name, "Send Email", "BIL");
            }

            return _result;
        }
        #endregion

        ~NotificationBL()
        {
        }
    }
}
