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
    public sealed class ClassRoomBL : Base
    {
        public ClassRoomBL()
        {

        }

        #region ClassGroup

        public double RowsCountClassGroup(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";

            }
            var _query =
                (
                    from _classGroup in this.db.Master_ClassroomGroups
                    where (SqlMethods.Like(_classGroup.ClassroomGroupName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    select _classGroup.ClassroomGroupCode

                ).Count();

            _result = _query;
            return _result;
        }

        public Master_ClassroomGroup GetSingleClassGroup(string _prmClassGrpCode)
        {
            Master_ClassroomGroup _result = null;

            try
            {
                _result = this.db.Master_ClassroomGroups.Single(_courseGrp => _courseGrp.ClassroomGroupCode == new Guid(_prmClassGrpCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_ClassroomGroup> GetListClassGroup(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_ClassroomGroup> _result = new List<Master_ClassroomGroup>();
            string _pattern1 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";

            }

            try
            {
                var _query = (
                                from _classGrp in this.db.Master_ClassroomGroups
                                where (SqlMethods.Like(_classGrp.ClassroomGroupName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _classGrp.InsertDate descending

                                select new
                                {
                                    ClassroomGroupCode = _classGrp.ClassroomGroupCode,
                                    ClassroomGroupName = _classGrp.ClassroomGroupName,
                                    ClassroomGroupDescription = _classGrp.ClassroomGroupDescription

                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_ClassroomGroup(_row.ClassroomGroupCode, _row.ClassroomGroupName, _row.ClassroomGroupDescription));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditClassGroup(Master_ClassroomGroup _prmClassGrp)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsClassGrpName(_prmClassGrp.ClassroomGroupName, _prmClassGrp.ClassroomGroupCode) == false)
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

        public bool AddClassGroup(Master_ClassroomGroup _prmClassGroup)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsClassGrpName(_prmClassGroup.ClassroomGroupName, _prmClassGroup.ClassroomGroupCode) == false)
                {
                    this.db.Master_ClassroomGroups.InsertOnSubmit(_prmClassGroup);
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

        public bool DeleteMultiClassGrp(string[] _prmClassGrp)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmClassGrp.Length; i++)
                {
                    Master_ClassroomGroup _msClassGrp = this.db.Master_ClassroomGroups.Single(_classRoomGrp => _classRoomGrp.ClassroomGroupCode == new Guid(_prmClassGrp[i]));

                    this.db.Master_ClassroomGroups.DeleteOnSubmit(_msClassGrp);
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

        private bool IsExistsClassGrpName(String _prmClassGrpName, Guid _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = from _ClassGrp in this.db.Master_ClassroomGroups
                             where _ClassGrp.ClassroomGroupName == _prmClassGrpName && _ClassGrp.ClassroomGroupCode != _prmCode
                             select new
                             {
                                 _ClassGrp.ClassroomGroupCode
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

        public List<Master_ClassroomGroup> GetListClassGroupForDDL()
        {
            List<Master_ClassroomGroup> _result = new List<Master_ClassroomGroup>();

            try
            {
                var _query =
                            (
                                from _group in this.db.Master_ClassroomGroups
                                orderby _group.ClassroomGroupName ascending
                                select new
                                {
                                    ClassGroupCode = _group.ClassroomGroupCode,
                                    ClassGroupName = _group.ClassroomGroupName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_ClassroomGroup(_row.ClassGroupCode, _row.ClassGroupName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion


        #region ClassRoom

        public double RowsCountClass(string _prmCategory, string _prmKeyword)
        {
            double _result = 0;

            string _pattern1 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";

            }
            var _query =
                (
                    from _class in this.db.Master_Classrooms
                    join _classGrp in this.db.Master_ClassroomGroups
                            on _class.ClassroomGroupCode equals _classGrp.ClassroomGroupCode
                    where (SqlMethods.Like(_classGrp.ClassroomGroupName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                    select _class.ClassroomCode

                ).Count();

            _result = _query;
            return _result;
        }

        public Master_Classroom GetSingleClass(string _prmClassCode)
        {
            Master_Classroom _result = null;

            try
            {
                _result = this.db.Master_Classrooms.Single(_class => _class.ClassroomCode == new Guid(_prmClassCode));
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public List<Master_Classroom> GetListClass(int _prmReqPage, int _prmPageSize, string _prmCategory, string _prmKeyword)
        {
            List<Master_Classroom> _result = new List<Master_Classroom>();
            string _pattern1 = "%%";

            if (_prmCategory == "Code")
            {
                _pattern1 = "%" + _prmKeyword + "%";

            }

            try
            {
                var _query = (
                                from _class in this.db.Master_Classrooms
                                join _classGrp in this.db.Master_ClassroomGroups
                                        on _class.ClassroomGroupCode equals _classGrp.ClassroomGroupCode
                                join _location in this.db.Master_CourseLocations
                                        on _class.CourseLocationCode equals _location.CourseLocationCode
                                where (SqlMethods.Like(_class.ClassroomName.Trim().ToLower(), _pattern1.Trim().ToLower()))
                                orderby _class.InsertDate descending

                                select new
                                {
                                    ClassRoomCode = _class.ClassroomCode,
                                    ClassRoomName = _class.ClassroomName,
                                    ClassRoomGroupName = _classGrp.ClassroomGroupName,
                                    CourseLocationName = _location.CourseLocationName,
                                    MaxCapacity = _class.MaxCapacity
                                }
                            ).Skip(_prmReqPage * _prmPageSize).Take(_prmPageSize);

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Classroom(_row.ClassRoomCode, _row.ClassRoomName, _row.ClassRoomGroupName, _row.CourseLocationName, _row.MaxCapacity));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        public bool EditClass(Master_Classroom _prmClassRoom)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsClassName(_prmClassRoom.ClassroomName, _prmClassRoom.ClassroomCode) == false)
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

        public bool AddClass(Master_Classroom _prmClassRoom)
        {
            bool _result = false;

            try
            {
                if (this.IsExistsClassName(_prmClassRoom.ClassroomName, _prmClassRoom.ClassroomCode) == false)
                {
                    this.db.Master_Classrooms.InsertOnSubmit(_prmClassRoom);
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

        public bool DeleteMultiClass(string[] _prmClass)
        {
            bool _result = false;

            try
            {
                for (int i = 0; i < _prmClass.Length; i++)
                {
                    Master_Classroom _msClassRoom = this.db.Master_Classrooms.Single(_course => _course.ClassroomCode == new Guid(_prmClass[i]));

                    this.db.Master_Classrooms.DeleteOnSubmit(_msClassRoom);
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

        private bool IsExistsClassName(String _prmClassName, Guid _prmCode)
        {
            bool _result = false;

            try
            {
                var _query = from _class in this.db.Master_Classrooms
                             where _class.ClassroomName == _prmClassName && _class.ClassroomCode != _prmCode
                             select new
                             {
                                 _class.ClassroomCode
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

        public List<Master_Classroom> GetListCourseForDDL()
        {
            List<Master_Classroom> _result = new List<Master_Classroom>();

            try
            {
                var _query =
                            (
                                from _class in this.db.Master_Classrooms
                                orderby _class.ClassroomName ascending
                                select new
                                {
                                    ClassroomCode = _class.ClassroomCode,
                                    ClassroomName = _class.ClassroomName
                                }
                            );

                foreach (var _row in _query)
                {
                    _result.Add(new Master_Classroom(_row.ClassroomCode, _row.ClassroomName));
                }
            }
            catch (Exception ex)
            {
                ErrorHandler.Record(ex, EventLogEntryType.Error);
            }

            return _result;
        }

        #endregion

        ~ClassRoomBL()
        {

        }
    }
}
