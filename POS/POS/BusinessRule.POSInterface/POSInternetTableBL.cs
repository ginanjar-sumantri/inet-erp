using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data.Linq.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using System.Transactions;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.Common;
using BusinessRule.POS;

namespace BusinessRule.POSInterface
{
    public sealed class POSInternetTableBL : Base
    {
        public POSInternetTableBL()
        {
        }

        #region Member

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
                             from _posMsInternetTable in this.db.POSMsInternetTables
                             where (SqlMethods.Like(_posMsInternetTable.TableNmbr.ToString(), _pattern1))
                                && (SqlMethods.Like(_posMsInternetTable.FloorNmbr.ToString(), _pattern2))
                             select _posMsInternetTable
                        ).Count();

            _result = _query;

            return _result;
        }
        
        public List<POSMsInternetTable> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsInternetTable> _result = new List<POSMsInternetTable>();

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
                                from _posMsInternetTable in this.db.POSMsInternetTables
                                where (SqlMethods.Like(_posMsInternetTable.TableNmbr.ToString(), _pattern1))
                                   && (SqlMethods.Like(_posMsInternetTable.FloorNmbr.ToString(), _pattern2))
                                orderby _posMsInternetTable.TableNmbr descending
                                select _posMsInternetTable
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

        public List<POSMsInternetTable> GetList()
        {
            List<POSMsInternetTable> _result = new List<POSMsInternetTable>();

            try
            {
                var _query = (
                                from _posMsInternetTable in this.db.POSMsInternetTables
                                orderby _posMsInternetTable.TableNmbr ascending
                                select _posMsInternetTable
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
                    POSMsInternetTable _posMsInternetTable = this.db.POSMsInternetTables.Single(_temp => _temp.TableNmbr.ToString() == _prmCode[i]);

                    this.db.POSMsInternetTables.DeleteOnSubmit(_posMsInternetTable);
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

        public POSMsInternetTable GetSingle(string _prmCode)
        {
            POSMsInternetTable _result = null;

            try
            {                
                 string[] _tempSplit = _prmCode.Split('-');
                 _result = this.db.POSMsInternetTables.Single(_temp => _temp.TableNmbr.ToString() == _tempSplit[0].Trim().ToLower() && _temp.FloorNmbr.ToString() == _tempSplit[1].Trim().ToLower());
                
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
                                from _posMsInternetTable in this.db.POSMsInternetTables
                                where _posMsInternetTable.TableNmbr.ToString() == _prmCode
                                select new
                                {
                                    FloorNumber = _posMsInternetTable.FloorNmbr
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.FloorNumber.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Add(POSMsInternetTable _prmPOSMsInternetTable)
        {
            bool _result = false;

            try
            {
                this.db.POSMsInternetTables.InsertOnSubmit(_prmPOSMsInternetTable);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(POSMsInternetTable _prmPOSMsInternetTable)
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

        public POSMsInternetTable GetSingleUpdate(string _prmFloorNmbr, string _prmTableID, string _prmFlooType)
        {
            POSMsInternetTable _result = null;

            try
            {
                //string[] _tempSplit = _prmCode.Split('-');
                _result = this.db.POSMsInternetTables.Single(_temp => _temp.FloorNmbr.ToString() == _prmFloorNmbr.Trim().ToLower() && _temp.TableIDPerRoom.ToString() == _prmTableID.Trim().ToLower()&& _temp.fgActive == true && _temp.FloorType == _prmFlooType.Trim().ToLower());

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public POSMsInternetTable GetSingleUpdateInternet(string _prmFloorNmbr, string _prmTableID, string _prmFlooType)
        {
            POSMsInternetTable _result = null;

            try
            {
                //string[] _tempSplit = _prmCode.Split('-');
                _result = this.db.POSMsInternetTables.Single(_temp => _temp.FloorNmbr.ToString() == _prmFloorNmbr.Trim().ToLower() && _temp.TableIDPerRoom.ToString() == _prmTableID.Trim().ToLower() && _temp.fgActive == true && _temp.FloorType == _prmFlooType.Trim().ToLower());

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean UpdateTables(string _prmFloorType)
        {
            Boolean _result = false;

            try
            {
                this.db.spPOS_UpdateTableStatus(_prmFloorType);
                _result = true;
            }
            catch (Exception)
            {
            }

            return _result;
        }

        #endregion

        ~POSInternetTableBL()
        {
        }
    }
}
