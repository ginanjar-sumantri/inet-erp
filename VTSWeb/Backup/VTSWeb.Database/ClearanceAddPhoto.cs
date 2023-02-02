using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;

namespace VTSWeb.Database
{
    public partial class ClearanceAdditionalPhoto
    {
        public ClearanceAdditionalPhoto(Guid _prmClearanceAdditionalCode, string _prmClearanceCode, Binary _prmPhoto)
        {
            this.ClearanceAdditionalCode = _prmClearanceAdditionalCode;
            this.ClearanceCode = _prmClearanceCode;
            this.Photo = _prmPhoto;
        }
    }
}