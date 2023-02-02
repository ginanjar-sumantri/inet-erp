using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.Common.MethodExtension;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common;
using System.Diagnostics;
using System.Data.Linq.SqlClient;
using System.Web;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using System.IO;
using System.Drawing;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class LoanBL : Base
    {
        public LoanBL()
        {

        }

        #region Loan
        public double RowsCountLoan(string _prmCategory, string _prmKeyword)
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
                    from _msLoan in this.db.HRMMsLoans
                    where (SqlMethods.Like(_msLoan.LoanCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msLoan.LoanName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msLoan.LoanCode

                ).Count();

            _result = _query;
            return _result;
        }

        public HRMMsLoan GetSingleLoan(string _prmLoanCode)
        {
            HRMMsLoan _result = null;

            try
            {
                _result = this.db.HRMMsLoans.Single(_emp => _emp.LoanCode == _prmLoanCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetLoanNameByCode(string _prmCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _msLoan in this.db.HRMMsLoans
                                where _msLoan.LoanCode == _prmCode
                                select new
                                {
                                    LoanName = _msLoan.LoanName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.LoanName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsLoan> GetListLoan(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<HRMMsLoan> _result = new List<HRMMsLoan>();
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
                                from _msLoan in this.db.HRMMsLoans
                                where (SqlMethods.Like(_msLoan.LoanCode.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                       && (SqlMethods.Like(_msLoan.LoanName.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _msLoan.EditDate descending
                                select new
                                {
                                    LoanCode = _msLoan.LoanCode,
                                    LoanName = _msLoan.LoanName,
                                    PayrollCode = _msLoan.PayrollCode,
                                    PayrollName = (
                                                        from _msPayrollItem in this.db.PAYMsPayrollItems
                                                        where _msLoan.PayrollCode == _msPayrollItem.PayrollCode
                                                        select _msPayrollItem.PayrollName
                                                    ).FirstOrDefault(),
                                    FgMandatory = _msLoan.FgMandatory
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsLoan(_row.LoanCode, _row.LoanName, _row.PayrollCode, _row.PayrollName, _row.FgMandatory));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<HRMMsLoan> GetListLoanForDDL()
        {
            List<HRMMsLoan> _result = new List<HRMMsLoan>();

            try
            {
                var _query = (
                                from _msLoan in this.db.HRMMsLoans
                                orderby _msLoan.LoanName ascending
                                select new
                                {
                                    LoanCode = _msLoan.LoanCode,
                                    LoanName = _msLoan.LoanName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsLoan(_row.LoanCode, _row.LoanName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiLoan(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    HRMMsLoan _msLoan = this.db.HRMMsLoans.Single(_temp => _temp.LoanCode.Trim().ToLower() == _prmCode[i].Trim().ToLower());

                    this.db.HRMMsLoans.DeleteOnSubmit(_msLoan);
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

        public bool AddLoan(HRMMsLoan _prmMsLoan)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsLoans.InsertOnSubmit(_prmMsLoan);
                this.db.SubmitChanges();

                _result = true;
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditLoan(HRMMsLoan _prmMsLoan)
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

        #region LoanDt
        public double RowsCountLoanDt(string _prmCode)
        {
            double _result = 0;

            var _query =
             (
                from _msLoanDt in this.db.HRMMsLoanDts
                where _msLoanDt.LoanCode == _prmCode
                select _msLoanDt
             ).Count();

            _result = _query;

            return _result;
        }

        public List<HRMMsLoanDt> GetListLoanDt(int _prmReqPage, int _prmPageSize, string _prmCode)
        {
            List<HRMMsLoanDt> _result = new List<HRMMsLoanDt>();

            try
            {
                var _query = (
                                from _msLoanDt in this.db.HRMMsLoanDts
                                where _msLoanDt.LoanCode == _prmCode
                                select new
                                {
                                    LoanCode = _msLoanDt.LoanCode,
                                    JobLevelCode = _msLoanDt.JobLevel,
                                    JobLevelName = (
                                                from _jobLevel in this.db.MsJobLevels
                                                where _jobLevel.JobLvlCode == _msLoanDt.JobLevel
                                                select _jobLevel.JobLvlName
                                              ).FirstOrDefault(),
                                    Amount = _msLoanDt.Amount
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new HRMMsLoanDt(_row.LoanCode, _row.JobLevelCode, _row.JobLevelName, _row.Amount));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public HRMMsLoanDt GetSingleLoanDt(string _prmLoanCode, string _prmJobLevel)
        {
            HRMMsLoanDt _result = null;

            try
            {
                _result = this.db.HRMMsLoanDts.Single(_temp => _temp.LoanCode == _prmLoanCode && _temp.JobLevel == _prmJobLevel);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiLoanDt(string[] _prmCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCode.Length; i++)
                {
                    string[] _code = _prmCode[i].Split('=');

                    HRMMsLoanDt _msLoanDt = this.db.HRMMsLoanDts.Single(_temp => _temp.LoanCode == _code[0] && _temp.JobLevel == _code[1]);
                    this.db.HRMMsLoanDts.DeleteOnSubmit(_msLoanDt);
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

        public bool AddLoanDt(HRMMsLoanDt _prmLoanDt)
        {
            bool _result = false;

            try
            {
                this.db.HRMMsLoanDts.InsertOnSubmit(_prmLoanDt);
                this.db.SubmitChanges();

                _result = true;

            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditLoanDt(HRMMsLoanDt _prmLoanDt)
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

        ~LoanBL()
        {

        }
    }
}
