using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys
{
    public partial class Master_CourseLocationAttach
    {
        private string _courseLocationName = "";

        public Master_CourseLocationAttach(Guid _prmCourseLocationAttchCode, string _prmCourseLocationName)
        {
            this.CourseLocationAttachCode = _prmCourseLocationAttchCode;
            this.CourseLocationName = _prmCourseLocationName;
        }

        public string CourseLocationName
        {
            get
            {
                return this._courseLocationName;
            }
            set
            {
                this._courseLocationName = value;
            }
        }

    }
}
