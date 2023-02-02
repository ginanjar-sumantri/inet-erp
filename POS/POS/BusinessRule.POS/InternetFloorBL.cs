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

namespace BusinessRule.POS
{
    public sealed class InternetFloorBL : Base
    {
        public InternetFloorBL()
        {
        }

        #region InternetFloor

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
                             from _posMsInternetFloor in this.db.POSMsInternetFloors
                             where (SqlMethods.Like(_posMsInternetFloor.FloorNmbr.ToString(), _pattern1))
                                && (SqlMethods.Like(_posMsInternetFloor.FloorName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _posMsInternetFloor
                        ).Count();

            _result = _query;

            return _result;
        }

        public List<POSMsInternetFloor> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<POSMsInternetFloor> _result = new List<POSMsInternetFloor>();

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
                                from _posMsInternetFloor in this.db.POSMsInternetFloors
                                where (SqlMethods.Like(_posMsInternetFloor.FloorNmbr.ToString(), _pattern1.Trim().ToLower()))
                                   && (SqlMethods.Like(_posMsInternetFloor.FloorName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _posMsInternetFloor.FloorName descending
                                select _posMsInternetFloor
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

        public List<POSMsInternetFloor> GetList()
        {
            List<POSMsInternetFloor> _result = new List<POSMsInternetFloor>();

            try
            {
                var _query = (
                                from _posMsInternetFloor in this.db.POSMsInternetFloors
                                orderby _posMsInternetFloor.FloorName ascending
                                select _posMsInternetFloor
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
                    string[] _tempSplit = _prmCode[i].Split('-');
                    var _query = (from _internetTable in this.db.POSMsInternetTables
                                  where _internetTable.FloorType == _tempSplit[0]
                                  && _internetTable.FloorNmbr == Convert.ToInt16(_tempSplit[1].Trim())
                                  select _internetTable);

                    this.db.POSMsInternetTables.DeleteAllOnSubmit(_query);

                    POSMsInternetFloor _posMsInternetFloor = this.db.POSMsInternetFloors.Single(_temp => _temp.FloorType.ToString() == _tempSplit[0] && _temp.FloorNmbr.ToString() == _tempSplit[1].Trim());

                    this.db.POSMsInternetFloors.DeleteOnSubmit(_posMsInternetFloor);
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

        public POSMsInternetFloor GetSingle(string _prmCode)
        {
            POSMsInternetFloor _result = null;

            string[] _tempSplit = _prmCode.Split('-');

            try
            {
                _result = this.db.POSMsInternetFloors.Single(_temp => _temp.FloorType == _tempSplit[0] && _temp.FloorNmbr == Convert.ToInt32(_tempSplit[1]));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public POSMsInternetFloor GetSinglePOSMsInternetFloorByRoomCode(string _prmRoomCode)
        {
            POSMsInternetFloor _result = null;

            try
            {
                _result = this.db.POSMsInternetFloors.Single(_temp => _temp.roomCode == _prmRoomCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetMemberNameByCodeInternet(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _posMsInternetFloor in this.db.POSMsInternetFloors
                                where _posMsInternetFloor.FloorNmbr.ToString() == _prmCode
                                && _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Internet)
                                select new
                                {
                                    FloorName = _posMsInternetFloor.FloorName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.FloorName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetMemberNameByCodeCafe(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _posMsInternetFloor in this.db.POSMsInternetFloors
                                where _posMsInternetFloor.FloorNmbr.ToString() == _prmCode
                                && _posMsInternetFloor.FloorType == POSFloorTypeDataMapper.GetFloorType(POSFloorType.Cafe)
                                select new
                                {
                                    FloorName = _posMsInternetFloor.FloorName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.FloorName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }        

        public bool Add(POSMsInternetFloor _prmPOSMsInternetFloor)
        {
            bool _result = false;

            try
            {
                this.db.POSMsInternetFloors.InsertOnSubmit(_prmPOSMsInternetFloor);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(POSMsInternetFloor _prmPOSMsInternetFloor)
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

        public bool EditSubmit()
        {
            bool _result = false;
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

        public List<POSMsBoxLayout> GetListRoomCode()
        {
            List<POSMsBoxLayout> _result = new List<POSMsBoxLayout>();
            try
            {
                var _qry = (from _posMsBoxLayout in this.db.POSMsBoxLayouts
                            select _posMsBoxLayout);
                foreach (var _row in _qry)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }
        #endregion

        ~InternetFloorBL()
        {
        }

        public List<POSMsInternetTable> GetListTableInternet(String _prmCode)
        {
            List<POSMsInternetTable> _result = new List<POSMsInternetTable>();

            string[] _tempSplit = _prmCode.Split('-');

            try
            {
                var _qry = (
                        from _internetTable in this.db.POSMsInternetTables
                        where _internetTable.FloorType == _tempSplit[0]
                        && _internetTable.FloorNmbr == Convert.ToInt32(_tempSplit[1])
                        select _internetTable
                    );
                foreach (var _row in _qry)
                    _result.Add(_row);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public POSMsInternetTable GetSingleInternetTable(String _prmFloorNmbr, String _prmTableID)
        {
            POSMsInternetTable _result = new POSMsInternetTable();

            string[] _tempSplit = _prmFloorNmbr.Split('-');
            try
            {
                var _qry = (from _msInternetTable in this.db.POSMsInternetTables
                            where _msInternetTable.FloorType == _tempSplit[0]
                            && _msInternetTable.FloorNmbr == Convert.ToInt32(_tempSplit[1])
                            && _msInternetTable.TableIDPerRoom == Convert.ToInt32(_prmTableID)
                            select _msInternetTable);
                if (_qry.Count() > 0)
                    _result = this.db.POSMsInternetTables.Single(a => a.FloorType == _tempSplit[0] && a.FloorNmbr == Convert.ToInt32(_tempSplit[1]) && a.TableIDPerRoom == Convert.ToInt32(_prmTableID));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public void GenerateTable(String _prmFloorNmbr)
        {
            string[] _tempSplit = _prmFloorNmbr.Split('-');

            try
            {
                var _qryDelete = (from _tableInternet in this.db.POSMsInternetTables
                                  where _tableInternet.FloorType == _tempSplit[0]
                                  && _tableInternet.FloorNmbr == Convert.ToInt32(_tempSplit[1])
                                  select _tableInternet);
                foreach (POSMsInternetTable _row in _qryDelete)
                    this.db.POSMsInternetTables.DeleteOnSubmit(_row);

                String _positionString = (
                        from _boxLayout in this.db.POSMsBoxLayouts
                        join _floorInternet in this.db.POSMsInternetFloors
                        on _boxLayout.roomCode equals _floorInternet.roomCode
                        where _floorInternet.FloorType == _tempSplit[0]
                                  && _floorInternet.FloorNmbr == Convert.ToInt32(_tempSplit[1])
                        select _boxLayout.position
                    ).FirstOrDefault();

                String[] _layoutTables = _positionString.Split('|');
                for (int i = 1; i <= _layoutTables.Length; i++)
                {
                    POSMsInternetTable _newData = new POSMsInternetTable();
                    _newData.FloorType = _tempSplit[0];
                    _newData.FloorNmbr = Convert.ToInt32(_tempSplit[1]);
                    _newData.fgActive = true;
                    _newData.Status = 0;
                    _newData.TableIDPerRoom = i;
                    _newData.TableNmbr = i.ToString();
                    this.db.POSMsInternetTables.InsertOnSubmit(_newData);
                }

                this.db.SubmitChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetFloorByRoomCode(String _prmCurrRoomCode)
        {
            int _result = 0;

            var _query = (
                            from _inetFloor in this.db.POSMsInternetFloors
                            where _inetFloor.roomCode == _prmCurrRoomCode
                            select _inetFloor.FloorNmbr
                         );

            if (_query.Count() > 0)
            {
                _result = _query.FirstOrDefault();
            }

            return _result;
        }
    }
}
