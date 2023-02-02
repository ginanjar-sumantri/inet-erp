using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Data;
using System.Web;
using System.Transactions;
using System.Data.Linq.SqlClient;
using System.Data.SqlClient;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using Microsoft.Reporting.WebForms;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;

namespace BusinessRule.POS
{
    public sealed class SynchronizeBL : Base
    {
        public SynchronizeBL()
        {
        }

        public double RowsCount(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "IP")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "Location")
            {
                _pattern3 = "%" + _prmKeyword + "%";
            }

            var _query =
                        (
                             from _msLinkServer in this.dbMembership.MsLinkServers
                             where (SqlMethods.Like(_msLinkServer.Server_Name.ToString(), _pattern1))
                                && (SqlMethods.Like(_msLinkServer.Server_IP.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                && (SqlMethods.Like(_msLinkServer.Server_Location.Trim().ToLower(), _pattern2.Trim().ToLower()))
                             select _msLinkServer
                        ).Count();

            _result = _query;

            return _result;
        }

        public MsLinkServer GetSingle(string _prmCode)
        {
            MsLinkServer _result = null;

            try
            {
                _result = this.dbMembership.MsLinkServers.Single(_temp => _temp.Server_IP == _prmCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<MsLinkServer> GetList(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<MsLinkServer> _result = new List<MsLinkServer>();

            string _pattern1 = "%%";
            string _pattern2 = "%%";
            string _pattern3 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "IP")
            {
                _pattern2 = "%" + _prmKeyword + "%";
            }
            else if (_prmCategory == "Location")
            {
                _pattern3 = "%" + _prmKeyword + "%";
            }

            try
            {

                var _query = (
                               from _msLinkServer in this.dbMembership.MsLinkServers
                               where (SqlMethods.Like(_msLinkServer.Server_Name.ToString(), _pattern1))
                                  && (SqlMethods.Like(_msLinkServer.Server_IP.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                  && (SqlMethods.Like(_msLinkServer.Server_Location.Trim().ToLower(), _pattern3.Trim().ToLower()))
                               select _msLinkServer
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

        public bool Add(MsLinkServer _prmMsLinkServer)
        {
            bool _result = false;

            try
            {
                this.dbMembership.MsLinkServers.InsertOnSubmit(_prmMsLinkServer);
                this.dbMembership.SubmitChanges();

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
                this.dbMembership.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
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
                    var _query = (from _msLinkServer in this.dbMembership.MsLinkServers
                                  where _msLinkServer.Server_IP == _tempSplit[0]
                                  select _msLinkServer);

                    this.dbMembership.MsLinkServers.DeleteAllOnSubmit(_query);
                }

                this.dbMembership.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool CheckHO(string _prmServerIP)
        {
            bool _result = false;
            MsLinkServer _msLinkServer = this.dbMembership.MsLinkServers.FirstOrDefault(_temp => _temp.Server_IP == _prmServerIP && _temp.Server_HO == 'Y');
            if (_msLinkServer != null)
                _result = true;

            return _result;
        }

        public String GetIPServerHO()
        {
            String _result = this.dbMembership.MsLinkServers.FirstOrDefault(_temp => _temp.Server_HO == 'Y').Server_IP;
            return _result;
        }

        public List<MsTableSync> GetListMsTableSync(string _prmCategory, string _prmKeyword)
        {
            List<MsTableSync> _result = new List<MsTableSync>();

            string _pattern1 = "%%";

            if (_prmCategory == "Group")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {

                var _query = (
                               from _msTableSync in this.dbMembership.MsTableSyncs
                               where (SqlMethods.Like(_msTableSync.Table_Group.ToString(), _pattern1))
                               && _msTableSync.FgActive == 'Y'
                               select _msTableSync
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

        public bool Synchronize(string _prmServerIPSource, string _prmServerIPDestination, string _prmGroup)
        {
            bool _result = false;
            List<MsTableSync> _listMsTableSync = this.GetListMsTableSync("Group", _prmGroup);
            MsLinkServer _msLinkServer = this.GetSingle(_prmServerIPSource);
            String _serverSourceInstance = _msLinkServer.Server_IP.Trim() + "\\" + _msLinkServer.Server_Instance.Trim();

            _msLinkServer = this.GetSingle(_prmServerIPDestination);
            String _serverDestinationInstance = _msLinkServer.Server_IP.Trim() + "\\" + _msLinkServer.Server_Instance.Trim();

            foreach (var _item in _listMsTableSync)
            {
                this.db.sp_Synchronize(_serverSourceInstance, _serverDestinationInstance, _msLinkServer.Server_Database, _item.Table_Name, _item.Table_PrimaryKey);
                _result = true;
            }

            return _result;
        }

        public bool? CheckServer(string _prmServerIP)
        {
            bool? _result = false;
            try
            {
                MsLinkServer _msLinkServer = this.GetSingle(_prmServerIP);
                String _serverInstance = _msLinkServer.Server_Name.Trim() + "\\" + _msLinkServer.Server_Instance.Trim();

                this.db.sp_SynchronizeCheckServer(_serverInstance, ref _result);
                if (_result == false)
                {
                    _serverInstance = _msLinkServer.Server_IP.Trim() + "\\" + _msLinkServer.Server_Instance.Trim();
                    this.db.sp_SynchronizeCheckServer(_serverInstance, ref _result);
                }
            }
            catch
            {
            }
            return _result;

        }

        public List<Transaction_SyncLog> GetListTransaction_SyncLog(DateTime _prmStartDate, DateTime _prmEndDate, String _prmServerIPSource, String _prmServerIPDestination)
        {
            List<Transaction_SyncLog> _result = new List<Transaction_SyncLog>();

            _prmStartDate = new DateTime(_prmStartDate.Year, _prmStartDate.Month, _prmStartDate.Day, 0, 0, 0);
            _prmEndDate = new DateTime(_prmEndDate.Year, _prmEndDate.Month, _prmEndDate.Day, 23, 59, 59);

            MsLinkServer _msLinkServer = this.GetSingle(_prmServerIPSource);
            String _serverSourceInstance = _msLinkServer.Server_IP.Trim() + "\\" + _msLinkServer.Server_Instance.Trim();
            _msLinkServer = this.GetSingle(_prmServerIPDestination);
            String _serverDestinationInstance = _msLinkServer.Server_IP.Trim() + "\\" + _msLinkServer.Server_Instance.Trim();

            try
            {

                var _query = (
                               from _transactionSyncLog in this.db.Transaction_SyncLogs
                               where _transactionSyncLog.Sync_StartDate >= _prmStartDate
                               & _transactionSyncLog.Sync_StartDate <= _prmEndDate
                               & _transactionSyncLog.Sync_ServerSource == _serverSourceInstance
                               & _transactionSyncLog.Sync_ServerDestination == _serverDestinationInstance
                               orderby _transactionSyncLog.Sync_ID
                               select _transactionSyncLog
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

        public List<Transaction_SyncManual> GetListTransaction_SyncManual(DateTime _prmStartDate, DateTime _prmEndDate, String _prmServerIPDestination)
        {
            List<Transaction_SyncManual> _result = new List<Transaction_SyncManual>();
            _prmStartDate = new DateTime(_prmStartDate.Year, _prmStartDate.Month, _prmStartDate.Day, 0, 0, 0);
            _prmEndDate = new DateTime(_prmEndDate.Year, _prmEndDate.Month, _prmEndDate.Day, 23, 59, 59);
            
            MsLinkServer _msLinkServer = this.GetSingle(_prmServerIPDestination);
            String _serverDestinationInstance = "[" + _msLinkServer.Server_Name.Trim() + "\\" + _msLinkServer.Server_Instance.Trim() + "]";

            try
            {

                var _query = (
                               from _transactionSyncMan in this.db.Transaction_SyncManuals
                               where _transactionSyncMan.Insert_Date >= _prmStartDate
                               & _transactionSyncMan.Insert_Date <= _prmEndDate
                               & _transactionSyncMan.Server_Destination == _serverDestinationInstance
                               orderby _transactionSyncMan.ID
                               select _transactionSyncMan
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

        public ReportDataSource ReportSynchronize(String _serverSourceInstance, String _sourceLocation, String _serverDestinationInstance, String _destinationLocation, String _prmDatabase, DateTime _prmStartDate, DateTime _prmEndDate, String _prmType)
        {
            ReportDataSource _result = new ReportDataSource();
            DataTable _dataTable = new DataTable();
            //_prmStartDate = new DateTime(_prmStartDate.Year, _prmStartDate.Month, _prmStartDate.Day, 0, 0, 0);
            //_prmEndDate = new DateTime(_prmEndDate.Year, _prmEndDate.Month, _prmEndDate.Day, 23, 59, 59);
            try
            {

                SqlConnection _conn = new SqlConnection(new UserBL().ConnectionString(HttpContext.Current.User.Identity.Name));

                SqlCommand _cmd = new SqlCommand();

                _cmd.CommandType = CommandType.StoredProcedure;
                _cmd.Parameters.Clear();
                _cmd.Connection = _conn;
                if (_prmType == "Log")
                    _cmd.CommandText = "sp_SynchronizeReportLog";
                else
                    _cmd.CommandText = "sp_SynchronizeReportManual";

                _cmd.Parameters.AddWithValue("@serverSource", _serverSourceInstance);
                _cmd.Parameters.AddWithValue("@sourceLocation", _sourceLocation);
                _cmd.Parameters.AddWithValue("@serverDestination", _serverDestinationInstance);
                _cmd.Parameters.AddWithValue("@destinationLocation", _destinationLocation);
                _cmd.Parameters.AddWithValue("@database", _prmDatabase);
                _cmd.Parameters.AddWithValue("@startDate", _prmStartDate);
                _cmd.Parameters.AddWithValue("@endDate", _prmEndDate);

                SqlDataAdapter _da = new SqlDataAdapter();

                _da.SelectCommand = _cmd;
                _da.Fill(_dataTable);


                _result.Value = _dataTable;
                _result.Name = "DataSet1";
            }
            catch (Exception ex)
            {

            }

            return _result;
        }

        public POSMsCashierPrinter GetDefaultPrinter(String _prmHostIP)
        {
            POSMsCashierPrinter _result = null;
            try
            {
                _result = this.db.POSMsCashierPrinters.FirstOrDefault(_temp => _temp.IPAddress == _prmHostIP);
                if (_result == null)
                    _result = this.db.POSMsCashierPrinters.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        ~SynchronizeBL()
        {
        }
    }
}
