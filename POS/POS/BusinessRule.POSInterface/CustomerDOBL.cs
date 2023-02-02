using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using BusinessRule.POS;

namespace BusinessRule.POSInterface
{
    public sealed class CustomerDOBL : Base
    {
        public CustomerDOBL()
        {
        }

        #region CustomerDO

        public double RowsCount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                        (
                             from _posMsCustomerDOs in this.db.POSMsCustomerDOs
                             where (SqlMethods.Like(_posMsCustomerDOs.CustDOCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                && (SqlMethods.Like(_posMsCustomerDOs.Name.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsCustomerDOs
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<POSMsCustomerDO> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsCustomerDO> _result = new List<POSMsCustomerDO>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Name")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            try
            {
                var _query = (
                                from _posMsCustomerDO in this.db.POSMsCustomerDOs
                                where (SqlMethods.Like(_posMsCustomerDO.CustDOCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsCustomerDO.Name.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsCustomerDO.Name descending
                                select _posMsCustomerDO
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<POSMsCustomerDO> GetList()
        {
            List<POSMsCustomerDO> _result = new List<POSMsCustomerDO>();

            try
            {
                var _query = (
                                from _posMsCustomerDO in this.db.POSMsCustomerDOs
                                orderby _posMsCustomerDO.Name ascending
                                select _posMsCustomerDO
                            );

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    POSMsCustomerDO _posMsCustomerDO = this.db.POSMsCustomerDOs.Single(_temp => _temp.CustDOCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.POSMsCustomerDOs.DeleteOnSubmit(_posMsCustomerDO);
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

        public POSMsCustomerDO GetSingle(string _prmCode)
        {
            POSMsCustomerDO _result = null;

            try
            {
                _result = this.db.POSMsCustomerDOs.Single(_temp => _temp.CustDOCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetMemberNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _posMsCustomerDOs in this.db.POSMsCustomerDOs
                                where _posMsCustomerDOs.CustDOCode == _prmCode
                                select new
                                {
                                    Name = _posMsCustomerDOs.Name
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.Name;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxCustDOCode()
        {
            int _result = 0;
            try
            {
                var _query = (
                                from _msCustomerDO in this.db.POSMsCustomerDOs
                                select _msCustomerDO.CustDOCode
                              ).Count();
                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public int GetMaxReferenceNo()
        {
            int _result = 0;
            try
            {
                var _query = (
                                from _trDeliveryOrder in this.db.POSTrDeliveryOrders
                                select _trDeliveryOrder.ReferenceNo
                              ).Count();
                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddTRDOWithMsCustomerDO(POSMsCustomerDO _prmPOSMsCustomerDO, POSTrDeliveryOrder _prmPOSTrDeliveryOrder, POSTrDeliveryOrderLog _prmPOSTrDeliveryOrderLog)
        {
            bool _result = false;

            try
            {
                if (_prmPOSMsCustomerDO != null)
                {
                    this.db.POSMsCustomerDOs.InsertOnSubmit(_prmPOSMsCustomerDO);
                }
                this.db.POSTrDeliveryOrders.InsertOnSubmit(_prmPOSTrDeliveryOrder);
                this.db.POSTrDeliveryOrderLogs.InsertOnSubmit(_prmPOSTrDeliveryOrderLog);
                this.db.SubmitChanges();
                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddTRDOWithoutMsCustomerDO(POSTrDeliveryOrder _prmPOSTrDeliveryOrder, POSTrDeliveryOrderLog _prmPOSTrDeliveryOrderLog)
        {
            bool _result = false;

            try
            {
                this.db.POSTrDeliveryOrders.InsertOnSubmit(_prmPOSTrDeliveryOrder);
                this.db.POSTrDeliveryOrderLogs.InsertOnSubmit(_prmPOSTrDeliveryOrderLog);
                this.db.SubmitChanges();
                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(POSMsCustomerDO _prmPOSMsCustomerDO)
        {
            bool _result = false;

            try
            {
                this.db.POSMsCustomerDOs.InsertOnSubmit(_prmPOSMsCustomerDO);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(POSMsCustomerDO _prmPOSMsCustomerDO)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public POSMsCustomerDO GetSingleForSearch(string _prmTelephone)
        {
            POSMsCustomerDO _result = null;
            try
            {
                var _query = (
                               from _msCustomerDO in this.db.POSMsCustomerDOs
                               where _msCustomerDO.Phone.Trim().ToLower() == _prmTelephone.Trim().ToLower()
                               | _msCustomerDO.HP.Trim().ToLower() == _prmTelephone.Trim().ToLower()
                               select _msCustomerDO
                               );
                if (_query.Count() > 0)
                {
                    _result = _query.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public POSMsCustomerDO GetSingleTelephone(string _prmTelephone)
        {
            POSMsCustomerDO _result = null;

            try
            {
                _result = this.db.POSMsCustomerDOs.Single(_temp => _temp.Phone == _prmTelephone | _temp.HP == _prmTelephone);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        //public List<POSMsCustomerDO> GetCustomerDOListDDL()
        //{
        //    List<POSMsCustomerDO> _result = new List<POSMsCustomerDO>();

        //    try
        //    {
        //        var _query = (
        //                        from _msCustomerDO in this.db.POSMsCustomerDOs
        //                        //where _msCustomer.CompanyID == _prmCompanyID
        //                        select new
        //                        {
        //                            CustDOCode = _msCustomerDO.CustDOCode,
        //                            Name  = _msCustomerDO.Name 
        //                        }
        //                    ).Distinct();

        //        foreach (var _row in _query)
        //        {
        //            _result.Add(new POSMsCustomerDO(_row.CustDOCode, _row.Name ));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //    }

        //    return _result;
        //}

        #endregion

        #region POSTrDeliveryOrder

        public List<V_POSListDeliveryOrder> GetListDeliveryOrderRef(string _prmReferenceNo)
        {
            String _pattern1 = "%%";
            if (_prmReferenceNo != "")
            {
                _pattern1 = "%" + _prmReferenceNo + "%";
            }

            List<V_POSListDeliveryOrder> _result = new List<V_POSListDeliveryOrder>();

            try
            {
                var _query =
                    (
                    from _posListDeliveryOrder in this.db.V_POSListDeliveryOrders
                    where (SqlMethods.Like(_posListDeliveryOrder.ReferenceNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    && _posListDeliveryOrder.IsVoid == false
                    orderby _posListDeliveryOrder.ReferenceNo descending
                    select _posListDeliveryOrder

                    );
                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception Ex)
            {
                _result = null;
            }
            return _result;
        }

        public List<POSTrDeliveryOrderRef> GetListPOSTrDeliveryOrderRef(string _prmReferenceNo)
        {
            String _pattern1 = "%%";
            if (_prmReferenceNo != "")
            {
                _pattern1 = "%" + _prmReferenceNo + "%";
            }

            List<POSTrDeliveryOrderRef> _result = new List<POSTrDeliveryOrderRef>();

            try
            {
                var _query =
                    (
                    from _pOSTrDeliveryOrderRef in this.db.POSTrDeliveryOrderRefs
                    where (SqlMethods.Like(_pOSTrDeliveryOrderRef.ReferenceNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    orderby _pOSTrDeliveryOrderRef.TransType descending
                    select new
                    {
                        ReferenceNo = _pOSTrDeliveryOrderRef.ReferenceNo,
                        TransType = _pOSTrDeliveryOrderRef.TransType,
                        TransNmbr = _pOSTrDeliveryOrderRef.TransNmbr
                    }
                    );
                foreach (var _row in _query)
                {
                    _result.Add(new POSTrDeliveryOrderRef(_row.ReferenceNo, _row.TransType, _row.TransNmbr));
                }
            }
            catch (Exception Ex)
            {
                _result = null;
            }
            return _result;
        }

        public List<POSTrDeliveryOrder> GetListPOSTrDeliveryOrder(string _prmReferenceNo, byte _prmStatus)
        {
            String _pattern1 = "%%";
            if (_prmReferenceNo != "")
            {
                _pattern1 = "%" + _prmReferenceNo + "%";
            }

            byte _pattern2 = 0;
            if (_prmStatus != 0)
            {
                _pattern2 = _prmStatus;
            }
            //if (_referenceNo != "" && Convert.ToString(_prmStatus )!= "")
            //{
            //    _pattern1 = "%" + _referenceNo + "%";
            //    _pattern2 = "%" + _prmStatus + "%";
            //}
            //if (_referenceNo != "" && Convert.ToString(_prmStatus) == "")
            //{
            //    _pattern1 = "%" + _referenceNo + "%";
            //}

            List<POSTrDeliveryOrder> _result = new List<POSTrDeliveryOrder>();

            try
            {
                var _query =
                    (
                    from _posTrDeliveryOrder in this.db.POSTrDeliveryOrders
                    join _posTrDeliveryOrderRef in this.db.POSTrDeliveryOrderRefs
                        on _posTrDeliveryOrder.ReferenceNo equals _posTrDeliveryOrderRef.ReferenceNo
                    where (SqlMethods.Like(_posTrDeliveryOrder.ReferenceNo.Trim().ToLower(), _pattern1.Trim().ToLower()))
                        //&& (SqlMethods.Like(Convert.ToString(_posTrDeliveryOrder.Status), _pattern2.Trim().ToLower()))
                    && _posTrDeliveryOrder.Status == _pattern2
                    && _posTrDeliveryOrder.IsVoid != true
                    orderby _posTrDeliveryOrder.ReferenceNo descending
                    select _posTrDeliveryOrder
                    ).Distinct();
                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception Ex)
            {
                _result = null;
            }
            return _result;
        }

        public POSTrDeliveryOrder GetSingleTrDeliveryOrder(string _prmReferenceNo)
        {
            POSTrDeliveryOrder _result = null;
            try
            {
                _result = this.db.POSTrDeliveryOrders.FirstOrDefault(_temp => _temp.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<POSTrDeliveryOrder> GetTrDeliveryOrder(string _prmReferenceNo)
        {
            List<POSTrDeliveryOrder> _result = new List<POSTrDeliveryOrder>();
            try
            {
                var _query =
                    (
                    from _posTrDeliveryOrder in this.db.POSTrDeliveryOrders
                    where _posTrDeliveryOrder.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                    && _posTrDeliveryOrder.IsVoid != true
                    select _posTrDeliveryOrder
                    ).Distinct();
                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public bool UpdatePOSTrDeliveryOrder(POSTrDeliveryOrder _prmPOSTrDeliveryOrder)
        {
            bool _result = false;

            try
            {
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public POSTrDeliveryOrderLog GetSingleTrDeliveryOrderLog(string _prmReferenceNo)
        {
            POSTrDeliveryOrderLog _result = null;
            try
            {
                _result = this.db.POSTrDeliveryOrderLogs.FirstOrDefault(_temp => _temp.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public POSTrDeliveryOrderLog GetSingleTrDeliveryOrderLogByDriver(string _prmReferenceNo, byte _prmstatus)
        {
            POSTrDeliveryOrderLog _result = null;
            try
            {
                _result = this.db.POSTrDeliveryOrderLogs.FirstOrDefault(_temp => _temp.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower() && _temp.Status == _prmstatus);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public bool DriverAvailable(string _prmUserName)
        {
            bool _result = true;
            try
            {
                var _query =
                    (
                    from _posTrDeliveryOrder in this.db.POSTrDeliveryOrders
                    join _posTrDeliveryOrderLog in this.db.POSTrDeliveryOrderLogs
                        on _posTrDeliveryOrder.ReferenceNo equals _posTrDeliveryOrderLog.ReferenceNo
                    where _posTrDeliveryOrder.Status == POSTrDeliveryOrderDataMapper.GetStatus(POSDeliveryOrderStatus.Delivering)
                    && _posTrDeliveryOrderLog.UserName == _prmUserName
                    select _posTrDeliveryOrderLog
                    ).Distinct();
                foreach (var _row in _query)
                {
                    _result = false;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public bool InsertPOSTrDeliveryOrderLog(POSTrDeliveryOrderLog _prmPOSTrDeliveryOrderLog)
        {
            bool _result = false;

            try
            {
                this.db.POSTrDeliveryOrderLogs.InsertOnSubmit(_prmPOSTrDeliveryOrderLog);
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public POSTrDeliveryOrderRef GetSingleTrDeliveryOrderRef(string _prmReferenceNo)
        {
            POSTrDeliveryOrderRef _result = null;
            try
            {
                _result = this.db.POSTrDeliveryOrderRefs.Single(_temp => _temp.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public POSTrDeliveryOrderRef GetSingleTrDeliveryOrderRefByReferenceNoTransType(string _prmReferenceNo, string _prmTransType)
        {
            POSTrDeliveryOrderRef _result = null;
            try
            {
                _result = this.db.POSTrDeliveryOrderRefs.Single(_temp => _temp.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower() && _temp.TransType.Trim().ToLower() == _prmTransType.Trim().ToLower());
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<POSTrDeliveryOrderLog> GetListTrDeliveryOrderLog(string _prmReferenceNo)
        {
            List<POSTrDeliveryOrderLog> _result = new List<POSTrDeliveryOrderLog>();

            try
            {
                var _query =
                    (
                    from _posTrDeliveryOrderLog in this.db.POSTrDeliveryOrderLogs
                    where _posTrDeliveryOrderLog.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                    select _posTrDeliveryOrderLog
                    );
                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception Ex)
            {
                _result = null;
            }
            return _result;
        }

        public List<POSTrAll> GetListTrDeliveryOrderAll(string _prmReferenceNo)
        {
            List<POSTrAll> _result = new List<POSTrAll>();

            this.db.spPOS_TrAll(_prmReferenceNo);
            var _query = (
                            from _temp in db.General_TemporaryTables
                            where _temp.TableName == "spPOS_TrAllResult"
                            && _temp.StoreProcedure == "spPOS_TrAll"
                            && _temp.PrimaryKey1 == _prmReferenceNo
                            select _temp
                         );
            foreach (var _row in _query)
            {
                _result.Add(new POSTrAll(_row.Field1, _row.Field2, _row.Field3, _row.Field4, Convert.ToInt32(_row.Field5)));
            }
            
            //var _query = (
            //            from _trAll in db.spPOS_TrAll(_prmReferenceNo)
            //            select new
            //            {
            //                TransType = _trAll.TransType,
            //                TransNmbr = _trAll.TransNmbr,
            //                ProductCode = _trAll.ProductCode,
            //                ProductName = _trAll.ProductName,
            //                Qty = _trAll.Qty
            //            }
            //        );
            //foreach (var _row in _query)
            //{
            //    _result.Add(new POSTrAll(_row.TransType, _row.TransNmbr, _row.ProductCode, _row.ProductName, _row.Qty));
            //}
            return _result;
        }

        //public POSTrDeliveryOrder GetSingleReferenceNo(string _prmReferenceNo)
        //{
        //    POSTrDeliveryOrder _result = null;
        //    try
        //    {
        //        var _query =
        //            (
        //               from _TrDeliveryOrder in this.db.POSTrDeliveryOrders
        //               where _TrDeliveryOrder.ReferenceNo = _prmReferenceNo
        //               select _TrDeliveryOrder
        //              );
        //        if (_query.Count() > 0)
        //        {
        //            _result = _query.FirstOrDefault();
        //        }
        //    }

        //    catch (Exception ex)
        //    {
        //        ErrorHandler.Record(ex, EventLogEntryType.Error);
        //    }
        //    return _result;
        //}

        public Boolean SetVOID(String _prmReferenceNo, String _prmReasonCode, Boolean _prmVOIDValue)
        {
            Boolean _result = false;
            POSReasonBL _reasonBL = new POSReasonBL();

            try
            {
                POSTrDeliveryOrder _posTrDeliveryOrder = this.db.POSTrDeliveryOrders.FirstOrDefault(_temp => _temp.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower());
                _posTrDeliveryOrder.Reason = Convert.ToInt32(_prmReasonCode);
                _posTrDeliveryOrder.IsVoid = _prmVOIDValue;

                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception Ex)
            {
            }

            return _result;
        }

        public Boolean CheckIsVoidDO(String _prmReferenceNo)
        {
            Boolean _result = false;
            var _query =
                    (
                    from _posTrDeliveryOrderRef in this.db.POSTrDeliveryOrderRefs
                    join _vPOSTrDetailAll in this.db.V_POSTrDetailAlls
                        on _posTrDeliveryOrderRef.ReferenceNo equals _vPOSTrDetailAll.ReferenceNo
                    where _posTrDeliveryOrderRef.ReferenceNo.Trim().ToLower() == _prmReferenceNo.Trim().ToLower()
                    && _posTrDeliveryOrderRef.TransType == _vPOSTrDetailAll.TransType
                    select _posTrDeliveryOrderRef
                    );
            foreach (var _row in _query)
            {
                _result = true;
            }
            return _result;
        }
        #endregion

        ~CustomerDOBL()
        {
        }
    }
}
