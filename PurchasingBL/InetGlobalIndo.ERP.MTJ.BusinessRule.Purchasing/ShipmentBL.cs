using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing
{
    public sealed class ShipmentBL : Base
    {
        public ShipmentBL()
        {

        }

        public int RowsCount
        {
            get
            {
                return this.db.MsShipments.Count();
            }
        }

        public bool Add(MsShipment _prmMsShipment)
        {
            bool _result = false;

            try
            {

                this.db.MsShipments.InsertOnSubmit(_prmMsShipment);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMulti(string[] _prmMsShipmentCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmMsShipmentCode.Length; i++)
                {
                    MsShipment _msShipment = this.db.MsShipments.Single(_temp => _temp.ShipmentCode == _prmMsShipmentCode[i]);

                    this.db.MsShipments.DeleteOnSubmit(_msShipment);
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

        public MsShipment GetSingle(string _prmMsShipmentCode)
        {
            MsShipment _result = null;

            try
            {
                _result = this.db.MsShipments.Single(_msShipment => _msShipment.ShipmentCode == _prmMsShipmentCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsShipment> GetListMasterShipment(int _prmReqPage, int _prmPageSize)
        {
            List<MsShipment> _result = new List<MsShipment>();

            try
            {
                var _query =
                            (
                                from _msShipment in this.db.MsShipments
                                select new
                                {
                                    ShipmentCode = _msShipment.ShipmentCode,
                                    ShipmentName = _msShipment.ShipmentName

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ShipmentCode = this._string, ShipmentName = this._string });

                    _result.Add(new MsShipment(_row.ShipmentCode, _row.ShipmentName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(MsShipment _prmMsShipment)
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

        public List<MsShipment> GetListDDLShipment()
        {
            List<MsShipment> _result = new List<MsShipment>();

            try
            {
                var _query =
                            (
                                from _msShipment in this.db.MsShipments
                                select new
                                {
                                    ShipmentCode = _msShipment.ShipmentCode,
                                    ShipmentName = _msShipment.ShipmentName

                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { ShipmentCode = this._string, ShipmentName = this._string });

                    _result.Add(new MsShipment(_row.ShipmentCode, _row.ShipmentName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetShipmentNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msShipment in this.db.MsShipments
                                where _msShipment.ShipmentCode.Trim().ToLower() == _prmCode.Trim().ToLower()
                                select new
                                {
                                    ShipmentName = _msShipment.ShipmentName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.ShipmentName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        ~ShipmentBL()
        {

        }

    }
}
