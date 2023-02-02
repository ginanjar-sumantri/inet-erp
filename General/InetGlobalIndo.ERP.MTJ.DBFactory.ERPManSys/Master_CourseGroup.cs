using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_CourseGroup
    {
        public Master_CourseGroup(Guid _prmCourseGroupCode, string _prmCourseGroupName)
        {
            this.CourseGroupCode = _prmCourseGroupCode;
            this.CourseGroupName = _prmCourseGroupName;
        }

        public Master_CourseGroup(Guid _prmCourseGroupCode, string _prmCourseGroupName, string _prmCourseGroupDesc)
        {
            this.CourseGroupCode = _prmCourseGroupCode;
            this.CourseGroupName = _prmCourseGroupName;
            this.CourseGroupDescription = _prmCourseGroupDesc;
        }
    }
}
