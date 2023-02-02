using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.Authentication;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.Diagnostics;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.Settings
{
    public sealed class DatabaseBL : Base
    {
        public DatabaseBL()
        {
        }

        #region master_Database

        public int RowsCount
        {
            get
            {
                return this.db.master_Databases.Count();
            }
        }

        public List<master_Database> GetList(int _prmReqPage, int _prmPageSize)
        {
            List<master_Database> _result = new List<master_Database>();

            try
            {
                var _query = (
                                from _database in this.db.master_Databases
                                select new
                                {
                                    DatabaseId = _database.DatabaseID,
                                    Name = _database.Name,
                                    Server = _database.Server,
                                    UID = _database.UID,
                                    PWD = _database.PWD,
                                    Status = _database.Status
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { DatabaseId = this._guid, Name = this._string, Server = this._string, UID = this._string, PWD = this._string, Status = this._byte });

                    _result.Add(new master_Database(_row.DatabaseId, _row.Name, _row.Server, _row.UID, _row.PWD, _row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<master_Database> GetList()
        {
            List<master_Database> _result = new List<master_Database>();

            try
            {
                var _query = (
                                from _database in this.db.master_Databases
                                select new
                                {
                                    DatabaseId = _database.DatabaseID,
                                    Name = _database.Name,
                                    Server = _database.Server,
                                    UID = _database.UID,
                                    PWD = _database.PWD,
                                    Status = _database.Status
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { DatabaseId = this._guid, Name = this._string, Server = this._string, UID = this._string, PWD = this._string, Status = this._byte });

                    _result.Add(new master_Database(_row.DatabaseId, _row.Name, _row.Server, _row.UID, _row.PWD, _row.Status));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public master_Database GetSingle(string _prmCode)
        {
            master_Database _result = null;

            try
            {
                _result = this.db.master_Databases.Single(_temp => _temp.DatabaseID == new Guid(_prmCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _database in this.db.master_Databases
                                where _database.DatabaseID == new Guid(_prmCode)
                                select new
                                {
                                    Name = _database.Name
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

        public bool DeleteMulti(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    master_Database _database = this.db.master_Databases.Single(_temp => _temp.DatabaseID.ToString().Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.master_Databases.DeleteOnSubmit(_database);
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

        public bool Add(master_Database _prmMaster_Database)
        {
            bool _result = false;

            try
            {
                this.db.master_Databases.InsertOnSubmit(_prmMaster_Database);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool Edit(master_Database _prmMaster_Database)
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

        public List<master_Database> GetListForDDL()
        {
            List<master_Database> _result = new List<master_Database>();

            try
            {
                var _query = (
                                from _database in this.db.master_Databases
                                where !(
                                            from _companyDatabase in this.db.master_Company_master_Databases
                                            select _companyDatabase.DatabaseID
                                        ).Contains(_database.DatabaseID)
                                select new
                                {
                                    DatabaseID = _database.DatabaseID,
                                    Name = _database.Name
                                }
                            );
 
                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { DatabaseID = this._guid, Name = this._string });

                    _result.Add(new master_Database(_row.DatabaseID, _row.Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<master_Database> GetListForCheckBox(Guid _prmCompID)
        {
            List<master_Database> _result = new List<master_Database>();

            try
            {
                var _query = (
                                from _database in this.db.master_Databases
                                where (
                                            from _companyDatabase in this.db.master_Company_master_Databases
                                            where _companyDatabase.CompanyID == _prmCompID
                                            select _companyDatabase.DatabaseID
                                        ).Contains(_database.DatabaseID)
                                select new
                                {
                                    DatabaseID = _database.DatabaseID,
                                    Name = _database.Name
                                }
                            );

                foreach (object _obj in _query)
                {
                    var _row = _obj.Template(new { DatabaseID = this._guid, Name = this._string });

                    _result.Add(new master_Database(_row.DatabaseID, _row.Name));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        #region MsDbConsolidationList

        public List<MsDbConsolidationList> GetListMsDbConsolidationList() 
        {
            List<MsDbConsolidationList> _result = new List<MsDbConsolidationList>();

            try
            {
                var _query = (from _msDbConsolidationList in this.db.MsDbConsolidationLists
                              select _msDbConsolidationList
                                );

                foreach (var _row in _query)
                {
                    _result.Add(_row);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return _result;
        }

        #endregion

        ~DatabaseBL()
        {
        }
    }
}
