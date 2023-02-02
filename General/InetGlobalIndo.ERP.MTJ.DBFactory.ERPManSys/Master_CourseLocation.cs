using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_CourseLocation
    {
        public Master_CourseLocation(Guid _prmCourseLocationCode, string _prmCourseLocationName, string _prmCouseLocationDesc, string _prmCity, string _prmContactName)
        {
            this.CourseLocationCode = _prmCourseLocationCode;
            this.CourseLocationName = _prmCourseLocationName;
            this.CourseLocationDescription = _prmCouseLocationDesc;
            this.City = _prmCity;
            this.ContactName = _prmContactName;
        }

        public Master_CourseLocation(Guid _prmCourseLocationCode, string _prmCourseLocationName)
        {
            this.CourseLocationCode = _prmCourseLocationCode;
            this.CourseLocationName = _prmCourseLocationName;

        }

    }
}
