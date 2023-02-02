using System;
using System.Linq;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Transactions;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule
{
    public sealed class CompanyConfigReportParameterBL : Base
    {
        public CompanyConfigReportParameterBL()
        {
        }

        public CompanyConfigReportParameter GetSingleCompanyConfigParam(String _prmReportKey, ref String _errMessage)
        {
            CompanyConfigReportParameter _result = null;
            try
            {
                String[] _key = _prmReportKey.Split(',');
                _result = this.db.CompanyConfigReportParameters.Single(_temp => _temp.ReportID.ToLower() == _key[0].ToLower() && _temp.ReportParameter.ToLower() == _key[1].ToLower());
            }
            catch (Exception Ex)
            {
                _errMessage += Ex.Message;
            }

            return _result;
        }

        public List<CompanyConfigReportParameter> GetListCompanyConfigParamForDDL()
        {
            List<CompanyConfigReportParameter> _result = new List<CompanyConfigReportParameter>();

            var _query = (
                            from _compConfigParam in this.db.CompanyConfigReportParameters
                            group _compConfigParam by _compConfigParam.ReportID into _grp
                            select new
                            {
                                ReportID = _grp.Key,
                                ReportName = (
                                                from _compConfig2 in this.db.CompanyConfigReportParameters
                                                where _compConfig2.ReportID == _grp.Key
                                                select _compConfig2.ReportName
                                             ).FirstOrDefault()
                            }
                         );

            foreach (var _item in _query)
            {
                _result.Add(new CompanyConfigReportParameter(_item.ReportID, _item.ReportName));
            }

            return _result;
        }

        public List<CompanyConfigReportParameter> GetListCompanyConfigParamByReportID(String _prmReportID)
        {
            List<CompanyConfigReportParameter> _result = new List<CompanyConfigReportParameter>();

            var _query = (
                            from _compConfigParam in this.db.CompanyConfigReportParameters
                            where _compConfigParam.ReportID.ToLower() == _prmReportID.ToLower()
                            select new
                            {
                                ReportID = _compConfigParam.ReportID,
                                ReportParameter = _compConfigParam.ReportParameter,
                                ReportName = _compConfigParam.ReportName,
                                Value = _compConfigParam.Value,
                                Remark = _compConfigParam.Remark
                            }
                         );

            foreach (var _item in _query)
            {
                _result.Add(new CompanyConfigReportParameter(_item.ReportID, _item.ReportParameter, _item.ReportName, _item.Value, _item.Remark));
            }

            return _result;
        }

        public Boolean EditCompanyConfigReportParameter(CompanyConfigReportParameter _prmCompanyConfigReportParameter, ref String _errMessage)
        {
            Boolean _result = false;

            try
            {
                this.db.SubmitChanges();
                _result = true;
            }
            catch (Exception Ex)
            {
                _errMessage += Ex.Message;
            }

            return _result;
        }

        ~CompanyConfigReportParameterBL()
        {
        }
    }
}