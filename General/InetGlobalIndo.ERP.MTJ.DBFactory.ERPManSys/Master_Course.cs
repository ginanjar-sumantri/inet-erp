using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_Course
    {
        private string _courseGrpName = "";

        public Master_Course(Guid _prmCourseCode, string _prmCourseName)
        {
            this.CourseCode = _prmCourseCode;
            this.CourseName = _prmCourseName;
        }

        public Master_Course(Guid _prmCourseCode, string _prmCourseName, string _prmCouseDesc, string _prmCourseGrpName, int _prmMinPart)
        {
            this.CourseCode = _prmCourseCode;
            this.CourseName = _prmCourseName;
            this.CourseDescription = _prmCouseDesc;
            this.CourseGroupName = _prmCourseGrpName;
            this.MinParticipant = _prmMinPart;
        }

        public string CourseGroupName
        {
            get
            {
                return this._courseGrpName;
            }
            set
            {
                this._courseGrpName = value;
            }
        }

    }
}
