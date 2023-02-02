using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing
{
    public sealed class DeliveryBL : Base
    {
        public DeliveryBL()
        {

        }

        #region DeliveryType

        public int RowsCountDelivery
        {
            get
            {
                return this.db.MsDeliveries.Count();
            }
        }

        public List<MsDelivery> GetListDelivery(int _prmReqPage, int _prmPageSize)
        {
            List<MsDelivery> _result = new List<MsDelivery>();

            try
            {
                var _query = (
                                from _msDelivery in this.db.MsDeliveries
                                orderby _msDelivery.UserDate descending
                                select new
                                {
                                    DeliveryCode = _msDelivery.DeliveryCode,
                                    DeliveryName = _msDelivery.DeliveryName,
                                    Address1 = _msDelivery.Address1
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new MsDelivery(_row.DeliveryCode, _row.DeliveryName, _row.Address1));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsDelivery> GetListDeliveryForDDL()
        {
            List<MsDelivery> _result = new List<MsDelivery>();

            try
            {
                var _query = (
                                from _msDelivery in this.db.MsDeliveries
                                orderby _msDelivery.DeliveryName ascending
                                select new
                                {
                                    DeliveryCode = _msDelivery.DeliveryCode,
                                    DeliveryName = _msDelivery.DeliveryName
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { DeliveryCode = this._string, DeliveryName = this._string });

                    _result.Add(new MsDelivery(_row.DeliveryCode, _row.DeliveryName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiDelivery(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    MsDelivery _msDelivery = this.db.MsDeliveries.Single(_temp => _temp.DeliveryCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.MsDeliveries.DeleteOnSubmit(_msDelivery);
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

        public MsDelivery GetSingleDelivery(string _prmCode)
        {
            MsDelivery _result = null;

            try
            {
                _result = this.db.MsDeliveries.Single(_temp => _temp.DeliveryCode == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetDeliveryNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msDelivery in this.db.MsDeliveries
                                where _msDelivery.DeliveryCode == _prmCode
                                select new
                                {
                                    DeliveryName = _msDelivery.DeliveryName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.DeliveryName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddDelivery(MsDelivery _prmMsDelivery)
        {
            bool _result = false;

            try
            {
                this.db.MsDeliveries.InsertOnSubmit(_prmMsDelivery);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditDelivery(MsDelivery _prmMsDelivery)
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

        #endregion

        ~DeliveryBL()
        {

        }
    }
}
