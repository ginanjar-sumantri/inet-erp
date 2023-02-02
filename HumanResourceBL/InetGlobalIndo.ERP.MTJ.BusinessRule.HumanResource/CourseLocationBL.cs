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
    public sealed class CourseLocationBL : Base
    {
        public CourseLocationBL()
        {

        }

        #region Course Location

        public double RowsCountCourseLocation(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";

            }
            var _query =
                (
                    from _courseLocation in this.db.Master_CourseLocations
                    where (SqlMethods.Like(_courseLocation.CourseLocationName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    select _courseLocation.CourseLocationCode

                ).Count();

            _result = _query;
            return _result;
        }

        public Master_CourseLocation GetSingleCourseLocation(string _prmCourseLocationCode)
        {
            Master_CourseLocation _result = null;

            try
            {
                _result = this.db.Master_CourseLocations.Single(_courseLocation => _courseLocation.CourseLocationCode == new Guid(_prmCourseLocationCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_CourseLocation> GetListCourseLocation(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_CourseLocation> _result = new List<Master_CourseLocation>();
            string _pattern1 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";

            }

            try
            {
                var _query = (
                                from _courseLocation in this.db.Master_CourseLocations
                                where (SqlMethods.Like(_courseLocation.CourseLocationName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _courseLocation.InsertDate descending

                                select new
                                {
                                    CourseLocationCode = _courseLocation.CourseLocationCode,
                                    CourseLocationName = _courseLocation.CourseLocationName,
                                    CourseLocationDescription = _courseLocation.CourseLocationDescription,
                                    City = _courseLocation.City,
                                    ContactName = _courseLocation.ContactName
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_CourseLocation(_row.CourseLocationCode, _row.CourseLocationName, _row.CourseLocationDescription, _row.City, _row.ContactName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditCourseLocation(Master_CourseLocation _prmMsCourseLocation)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsCourseLocationName(_prmMsCourseLocation.CourseLocationName, _prmMsCourseLocation.CourseLocationCode) == false)
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

        public bool AddCourseLocation(Master_CourseLocation _prmMsCourseLocation)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsCourseLocationName(_prmMsCourseLocation.CourseLocationName, _prmMsCourseLocation.CourseLocationCode) == false)
                {
                    this.db.Master_CourseLocations.InsertOnSubmit(_prmMsCourseLocation);
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

        public bool DeleteMultiCourseLocation(string[] _prmCourse)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmCourse.Length; i++)
                {
                    Master_CourseLocation _msCourseLocation = this.db.Master_CourseLocations.Single(_courseLocation => _courseLocation.CourseLocationCode == new Guid(_prmCourse[i]));

                    this.db.Master_CourseLocations.DeleteOnSubmit(_msCourseLocation);
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

        private bool IsExistsCourseLocationName(String _prmCourseLocationName, Guid _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = from _CourseLocation in this.db.Master_CourseLocations
                             where _CourseLocation.CourseLocationName == _prmCourseLocationName && _CourseLocation.CourseLocationCode != _prmCode
                             select new
                             {
                                 _CourseLocation.CourseLocationCode
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

        public List<Master_CourseLocation> GetListCourseLocationForDDL()
        {
            List<Master_CourseLocation> _result = new List<Master_CourseLocation>();

            try
            {
                var _query =
                            (
                                from _CourseLocation in this.db.Master_CourseLocations
                                orderby _CourseLocation.CourseLocationName ascending
                                select new
                                {
                                    CourseLocationCode = _CourseLocation.CourseLocationCode,
                                    CourseLocationName = _CourseLocation.CourseLocationName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_CourseLocation(_row.CourseLocationCode, _row.CourseLocationName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        #region Course Location Attach

        #endregion

        ~CourseLocationBL()
        {

        }
    }
}
