using System;
using System.Collections.Generic;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Collections;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using System.Data.Linq;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public sealed class ReportListBL : Base
    {
        public ReportListBL()
        {
        }

        #region ReportList

        public SortedList GetReportListDDL(string _prmReportGroupID)
        {
            SortedList _result = new SortedList();
            try
            {
                var _query = (
                                from _msReportList in this.dbMembership.master_ReportLists
                                where _msReportList.ReportGroupID == _prmReportGroupID
                                    && _msReportList.ReportType == ReportTypeDataMapper.GetReportType(ReportType.Report)
                                select _msReportList
                             );
                foreach (var item in _query)
                {
                    _result.Add(item.ReportName, item.ReportPath);
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public List<master_ReportList> GetReportListDDL(string _prmReportGroupID, Guid _prmCompanyID)
        {
            List<master_ReportList> _result = new List<master_ReportList>();

            try
            {
                var _query = (
                                from _msReportList in this.dbMembership.master_ReportLists
                                where _msReportList.ReportGroupID == _prmReportGroupID
                                    && _msReportList.ReportType == ReportTypeDataMapper.GetReportType(ReportType.Report)
                                    && _msReportList.CompanyID == _prmCompanyID
                                    && _msReportList.fgActive == true
                                orderby _msReportList.SortNo ascending
                                select _msReportList
                             ).OrderBy(_temp => _temp.SortNo);
               
                foreach (var item in _query)
                {
                    _result.Add(new master_ReportList(item.ReportType, item.ReportGroupID, item.SortNo, item.ReportName, item.ReportPath, item.CompanyID, item.fgActive));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public String GetSinglePath(string _prmReportGroupID, Guid _prmCompanyID)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msReportList in this.dbMembership.master_ReportLists
                                where _msReportList.ReportGroupID == _prmReportGroupID
                                    && _msReportList.ReportType == ReportTypeDataMapper.GetReportType(ReportType.Report)
                                    && _msReportList.CompanyID == _prmCompanyID
                                    && _msReportList.fgActive == true
                                orderby _msReportList.SortNo ascending
                                select _msReportList.ReportPath
                             ).FirstOrDefault();

                _result = _query;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }
            return _result;
        }

        public String GetPathPrintPreviewSC(string _prmReportGroupID, Guid _prmCompanyID, int _prmSortID)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msReportList in this.dbMembership.master_ReportLists
                                where _msReportList.ReportGroupID == _prmReportGroupID
                                       && _msReportList.ReportType == ReportTypeDataMapper.GetReportType(ReportType.PrintPreview)
                                       && _msReportList.fgActive == true
                                       && _msReportList.CompanyID == _prmCompanyID
                                       && _msReportList.SortNo == _prmSortID
                                select _msReportList.ReportPath
                             );

                _result = _query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetPathForReport(string _prmReportGroupID, Guid _prmCompanyID, int _prmSortID)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msReportList in this.dbMembership.master_ReportLists
                                where _msReportList.ReportGroupID == _prmReportGroupID
                                       && _msReportList.ReportType == ReportTypeDataMapper.GetReportType(ReportType.Report)
                                       && _msReportList.fgActive == true
                                       && _msReportList.CompanyID == _prmCompanyID
                                       && _msReportList.SortNo == _prmSortID
                                select _msReportList.ReportPath
                             );

                _result = _query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public String GetPathPrintPreview(string _prmReportGroupID, Guid _prmCompanyID)
        {
            String _result = "";

            try
            {
                var _query = (
                                from _msReportList in this.dbMembership.master_ReportLists
                                where _msReportList.ReportGroupID == _prmReportGroupID
                                       && _msReportList.ReportType == ReportTypeDataMapper.GetReportType(ReportType.PrintPreview)
                                       && _msReportList.fgActive == true
                                       && _msReportList.CompanyID == _prmCompanyID
                                select _msReportList.ReportPath
                             );

                _result = _query.FirstOrDefault();
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public Boolean ActivateReportList(String _prmReportGroupID, String _prmReportName, Guid _prmCompanyID)
        {
            Boolean _result = false;
            try
            {
                master_ReportList _updateData = this.dbMembership.master_ReportLists.Single(a => (a.ReportGroupID == _prmReportGroupID && a.ReportName == _prmReportName && a.CompanyID == _prmCompanyID));
                _updateData.fgActive = !_updateData.fgActive;
                this.dbMembership.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public Boolean UpdateActivePrintPreview(String _prmReportGroupID, String _prmReportName, Guid _prmCompanyID)
        {
            Boolean _result = false;
            try
            {
                var _qry = (
                        from _masterReportList in this.dbMembership.master_ReportLists
                        where _masterReportList.ReportGroupID == _prmReportGroupID
                            && _masterReportList.CompanyID == _prmCompanyID
                        select _masterReportList
                    );
                foreach (var _rs in _qry)
                {
                    master_ReportList _updateData = this.dbMembership.master_ReportLists.Single(a => (a.ReportGroupID == _rs.ReportGroupID && a.ReportName == _rs.ReportName && a.CompanyID == _prmCompanyID));
                    _updateData.fgActive = false;
                }
                master_ReportList _updateData2 = this.dbMembership.master_ReportLists.Single(a => (a.ReportGroupID == _prmReportGroupID && a.ReportName == _prmReportName && a.CompanyID == _prmCompanyID));
                _updateData2.fgActive = true;

                this.dbMembership.SubmitChanges();
                _result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public List<master_ReportList> GetListPrintPreviewSelection(String _prmReportGroupId, Guid _prmCompanyID)
        {
            List<master_ReportList> _result = new List<master_ReportList>();
            try
            {
                var _qry = (
                        from _masterReportListSelection in this.dbMembership.master_ReportLists
                        where _masterReportListSelection.ReportGroupID == _prmReportGroupId
                            && _masterReportListSelection.ReportType == ReportTypeDataMapper.GetReportType(ReportType.PrintPreview)
                            && _masterReportListSelection.CompanyID == _prmCompanyID
                            && _masterReportListSelection.Enabled == true
                        select new
                        {
                            ReportType = _masterReportListSelection.ReportType,
                            ReportGroupID = _masterReportListSelection.ReportGroupID,
                            SortNo = _masterReportListSelection.SortNo,
                            ReportName = _masterReportListSelection.ReportName,
                            CompanyID = _masterReportListSelection.CompanyID,
                            ReportPath = _masterReportListSelection.ReportPath,
                            fgActive = _masterReportListSelection.fgActive
                        }
                    ).Distinct();
                foreach (var _rs in _qry)
                {
                    _result.Add(new master_ReportList(_rs.ReportType, _rs.ReportGroupID, _rs.SortNo, _rs.ReportName, _rs.ReportPath, _rs.CompanyID, _rs.fgActive));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        public String GetActivePrintPreview(String _prmReportGroupID, Guid _prmCompanyID)
        {
            String _result = "";
            try
            {
                _result = (
                        from _masterReportList in this.dbMembership.master_ReportLists
                        where _masterReportList.ReportGroupID == _prmReportGroupID
                            && _masterReportList.fgActive == true
                            && _masterReportList.CompanyID == _prmCompanyID
                        select _masterReportList.ReportName
                    ).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return _result;
        }

        #endregion

        ~ReportListBL()
        {
        }
    }
}
