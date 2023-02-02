using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTSWeb.BusinessRule;
using System.Web.UI.WebControls;
using VTSWeb.DataMapping;
using VTSWeb.Enum;
using VTSWeb.Database;
using System.IO;

namespace VTSWeb.Foto
{
    public sealed class FotoAdd
    {
        private MsVisitorExtensionBL _VisitorExtensionBL = null;

        public String Edit(master_CustContactExtension _prmCustContactExtension, FileUpload _prmFileUpload, String _imagePath)
        {
            String _result = "";

            this._VisitorExtensionBL = new MsVisitorExtensionBL();

            try
            {
                _result = this._VisitorExtensionBL.Edit(_prmCustContactExtension, _prmFileUpload,_imagePath);
            }
            catch (Exception)
            {
                //throw;
            }

            return _result;
        }

        public String Add(master_CustContactExtension _prmCustContactExtension, FileUpload _prmFileUpload,String _imagePath)
        {
            String _result = "";

            this._VisitorExtensionBL = new MsVisitorExtensionBL();

            try
            {
                _result = this._VisitorExtensionBL.Add(_prmCustContactExtension, _prmFileUpload,_imagePath);
            }
            catch (Exception)
            {
                //throw;
            }

            return _result;
        }

    }
}
