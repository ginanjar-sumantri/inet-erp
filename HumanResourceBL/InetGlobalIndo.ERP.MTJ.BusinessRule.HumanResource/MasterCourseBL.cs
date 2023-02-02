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
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;

namespace InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource
{
    public sealed class MasterCourseBL : Base
    {

        #region MasterCourse

        public MasterCourseBL()
        {

        }

        public double RowsCountCourse(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";

            }
            var _query =
                (
                    from _course in this.db.Master_Courses
                    join _courseGrp in this.db.Master_CourseGroups
                            on _course.CourseGroupCode equals _courseGrp.CourseGroupCode
                    where (SqlMethods.Like(_courseGrp.CourseGroupName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    select _course.CourseCode

                ).Count();

            _result = _query;
            return _result;
        }

        public Master_Course GetSingleCourse(string _prmCourseCode)
        {
            Master_Course _result = null;

            try
            {
                _result = this.db.Master_Courses.Single(_courseGrp => _courseGrp.CourseCode == new Guid(_prmCourseCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Course> GetListCourse(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_Course> _result = new List<Master_Course>();
            string _pattern1 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";

            }

            try
            {
                var _query = (
                                from _course in this.db.Master_Courses
                                join _courseGrp in this.db.Master_CourseGroups
                                        on _course.CourseGroupCode equals _courseGrp.CourseGroupCode
                                where (SqlMethods.Like(_courseGrp.CourseGroupName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _courseGrp.InsertDate descending

                                select new
                                {
                                    CourseCode = _course.CourseCode,
                                    CourseName = _course.CourseName,
                                    CourseDescription = _course.CourseDescription,
                                    MinParticipant = _course.MinParticipant,
                                    CourseGroupName = _courseGrp.CourseGroupName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Course(_row.CourseCode, _row.CourseName, _row.CourseDescription, _row.CourseGroupName, _row.MinParticipant));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditCourse(Master_Course _prmMsCourse)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsCourseName(_prmMsCourse.CourseGroupName, _prmMsCourse.CourseCode) == false)
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

        public bool AddCourseGroup(Master_Course _prmMsCourse)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsCourseName(_prmMsCourse.CourseGroupName, _prmMsCourse.CourseCode) == false)
                {
                    this.db.Master_Courses.InsertOnSubmit(_prmMsCourse);
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

        public bool DeleteMultiCourse(string[] _prmCourse)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCourse.Length; i++)
                {
                    Master_Course _msCourse = this.db.Master_Courses.Single(_course => _course.CourseCode == new Guid(_prmCourse[i]));

                    this.db.Master_Courses.DeleteOnSubmit(_msCourse);
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

        private bool IsExistsCourseName(String _prmCourseName, Guid _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = from _Course in this.db.Master_Courses
                             where _Course.CourseName == _prmCourseName && _Course.CourseCode != _prmCode
                             select new
                             {
                                 _Course.CourseCode
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

        public List<Master_Course> GetListCourseForDDL()
        {
            List<Master_Course> _result = new List<Master_Course>();

            try
            {
                var _query =
                            (
                                from _group in this.db.Master_Courses
                                orderby _group.CourseName ascending
                                select new
                                {
                                    CourseCode = _group.CourseCode,
                                    CourseName = _group.CourseName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Course(_row.CourseCode, _row.CourseName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region Course Group
        public double RowsCountCourseGroup(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";
            string _pattern2 = "%%";

            if (_prmCategory == "Name")
            {
                _pattern1 = "%" + _prmKeyword + "%";
                _pattern2 = "%%";

            }
            else if (_prmCategory == "Description")
            {
                _pattern2 = "%" + _prmKeyword + "%";
                _pattern1 = "%%";
            }

            var _query =
                (
                    from _msCourseGrp in this.db.Master_CourseGroups
                    where (SqlMethods.Like(_msCourseGrp.CourseGroupName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                           && (SqlMethods.Like(_msCourseGrp.CourseGroupDescription.Trim().ToLower(), _pattern2.Trim().ToLower()))
                    select _msCourseGrp.CourseGroupCode

                ).Count();

            _result = _query;
            return _result;
        }

        public Master_CourseGroup GetSingleCourseGroup(string _prmCourseGrpCode)
        {
            Master_CourseGroup _result = null;

            try
            {
                _result = this.db.Master_CourseGroups.Single(_courseGrp => _courseGrp.CourseGroupCode == new Guid(_prmCourseGrpCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_CourseGroup> GetListCourseGroup(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_CourseGroup> _result = new List<Master_CourseGroup>();
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
                                from _courseGrp in this.db.Master_CourseGroups
                                where (SqlMethods.Like(_courseGrp.CourseGroupName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                        && (SqlMethods.Like(_courseGrp.CourseGroupDescription.Trim().ToLower(), _pattern2.Trim().ToLower()))
                                orderby _courseGrp.InsertDate descending

                                select new
                                {
                                    CourseGroupCode = _courseGrp.CourseGroupCode,
                                    CourseGroupName = _courseGrp.CourseGroupName,
                                    CourseGroupDescription = _courseGrp.CourseGroupDescription
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_CourseGroup(_row.CourseGroupCode, _row.CourseGroupName, _row.CourseGroupDescription));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditCourseGroup(Master_CourseGroup _prmMsCourseGroup)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsCourseGroupName(_prmMsCourseGroup.CourseGroupName, _prmMsCourseGroup.CourseGroupCode) == false)
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

        public bool AddCourseGroup(Master_CourseGroup _prmMsCourseGroup)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsCourseGroupName(_prmMsCourseGroup.CourseGroupName, _prmMsCourseGroup.CourseGroupCode) == false)
                {
                    this.db.Master_CourseGroups.InsertOnSubmit(_prmMsCourseGroup);
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

        public bool DeleteMultiCourseGroup(string[] _prmCourseGroupCode)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCourseGroupCode.Length; i++)
                {
                    Master_CourseGroup _msCourseGrp = this.db.Master_CourseGroups.Single(_courseGrp => _courseGrp.CourseGroupCode == new Guid(_prmCourseGroupCode[i]));

                    this.db.Master_CourseGroups.DeleteOnSubmit(_msCourseGrp);
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

        private bool IsExistsCourseGroupName(String _prmCourseGrpName, Guid _prmGrpCode)
        {
            bool _result = false;

            try
            {
                var _query = from _CourseGrp in this.db.Master_CourseGroups
                             where _CourseGrp.CourseGroupName == _prmCourseGrpName && _CourseGrp.CourseGroupCode != _prmGrpCode
                             select new
                             {
                                 _CourseGrp.CourseGroupCode
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

        public List<Master_CourseGroup> GetListForDDL()
        {
            List<Master_CourseGroup> _result = new List<Master_CourseGroup>();

            try
            {
                var _query =
                            (
                                from _group in this.db.Master_CourseGroups
                                orderby _group.CourseGroupName ascending
                                select new
                                {
                                    CourseGroupCode = _group.CourseGroupCode,
                                    CourseGroupName = _group.CourseGroupName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_CourseGroup(_row.CourseGroupCode, _row.CourseGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }
        #endregion

        ~MasterCourseBL()
        {

        }
    }
}
