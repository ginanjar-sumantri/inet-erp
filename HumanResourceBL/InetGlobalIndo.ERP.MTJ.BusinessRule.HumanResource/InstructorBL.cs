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

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class InstructorBL : Base
    {
        public InstructorBL()
        {

        }

        #region Instructor
        public double RowsCountInstructor(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";


            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            var _query =
                (
                    from _master_Instructor in this.db.Master_Instructors
                    where (SqlMethods.Like(_master_Instructor.InstructorsName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    select _master_Instructor.InstructorsCode
                ).Count();

            _result = _query;
            return _result;

        }

        public Master_Instructor GetSingleInstructor(Guid _prmInstructorCode)
        {
            Master_Instructor _result = null;

            try
            {
                _result = this.db.Master_Instructors.Single(_temp => _temp.InstructorsCode == _prmInstructorCode);
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public string GetInstructorNameByCode(Guid _prmInstructorCode)
        {
            string _result = "";

            try
            {
                var _query = (
                                from _master_Instructor in this.db.Master_Instructors
                                where _master_Instructor.InstructorsCode == _prmInstructorCode
                                select new
                                {
                                    InstructorName = _master_Instructor.InstructorsName
                                }
                              );

                foreach (var _obj in _query)
                {
                    _result = _obj.InstructorName;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Instructor> GetListInstructor(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_Instructor> _result = new List<Master_Instructor>();

            string _pattern1 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
            }

            try
            {
                var _query = (
                                from _master_Instructor in this.db.Master_Instructors
                                where (SqlMethods.Like(_master_Instructor.InstructorsName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _master_Instructor.InstructorsName ascending
                                select new
                                {
                                    InstructorCode = _master_Instructor.InstructorsCode,
                                    InstructorName = _master_Instructor.InstructorsName,
                                    InstructorDescription = _master_Instructor.InstructorsDescription,
                                    InstructorAddress1 = _master_Instructor.InstructorsAddress1
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Instructor(_row.InstructorCode, _row.InstructorName, _row.InstructorDescription, _row.InstructorAddress1));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Instructor> GetListInstructorForDDL()
        {
            List<Master_Instructor> _result = new List<Master_Instructor>();

            try
            {
                var _query = (
                                from _master_Instructor in this.db.Master_Instructors
                                orderby _master_Instructor.InstructorsName ascending
                                select new
                                {
                                    InstructorCode = _master_Instructor.InstructorsCode,
                                    InstructorName = _master_Instructor.InstructorsName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Instructor(_row.InstructorCode, _row.InstructorName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditInstructor(Master_Instructor _prmMaster_Instructor)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsInstructorName(_prmMaster_Instructor.InstructorsName, _prmMaster_Instructor.InstructorsCode) == false)
                {
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        private bool IsExistsInstructorName(String _prmInstructorName, Guid _prmInstructorCode)
        {
            bool _result = false;

            try
            {
                var _query = from _master_Instructor in this.db.Master_Instructors
                             where _master_Instructor.InstructorsName == _prmInstructorName && _master_Instructor.InstructorsCode != _prmInstructorCode
                             select new
                             {
                                 _master_Instructor.InstructorsCode
                             };

                if (_query.Count() > 0)
                {
                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool AddInstructor(Master_Instructor _prmMaster_Instructor)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsInstructorName(_prmMaster_Instructor.InstructorsName, _prmMaster_Instructor.InstructorsCode) == false)
                {
                    this.db.Master_Instructors.InsertOnSubmit(_prmMaster_Instructor);
                    this.db.SubmitChanges();

                    _result = true;
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool DeleteMultiInstructor(string[] _prmInstructorCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmInstructorCode.Length; i++)
                {
                    Master_Instructor _master_Instructor = this.db.Master_Instructors.Single(_temp => _temp.InstructorsCode == new Guid(_prmInstructorCode[i]));

                    this.db.Master_Instructors.DeleteOnSubmit(_master_Instructor);
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
        #endregion

        ~InstructorBL()
        {

        }
    }
}
