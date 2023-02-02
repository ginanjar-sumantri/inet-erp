using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.Registrasi
{
    public partial class ActivationPage : System.Web.UI.Page
    {
        protected NameValueCollectionExtractor _nvcExtractor;

        protected void Page_Load(object sender, EventArgs e)
        {            
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
            RegistrationBL _regBL = new RegistrationBL();

            if (!this.Page.IsPostBack == true)
            {
                string _org = Rijndael.Decrypt(this._nvcExtractor.GetValue("org"), ApplicationConfig.EncryptionKey);
                string _userID = Rijndael.Decrypt(this._nvcExtractor.GetValue("user"), ApplicationConfig.EncryptionKey);
                string _code = Rijndael.Decrypt(this._nvcExtractor.GetValue("code"), ApplicationConfig.EncryptionKey);

                if (_regBL.CheckActivationEmail(_org, _userID, _code))
                {
                    bool _result = this.Approve(_org, _userID, _code);
                    if (_result == true)
                    {
                        this.ActivationResultLiteral.Text = "Terima kasih. Anda telah berhasil mengaktivasi account Anda. <br> This page will automatically redirect in 5 seconds ...";

                        Response.AppendHeader("REFRESH", "5;URL=" + "../Login/Login.aspx");
                    }
                }
                else
                {
                    this.ActivationResultLiteral.Text = "Gagal mengaktivasi account. Silahkan klik link berikut untuk kirim ulang email";
                }
            }
        }

        private bool Approve(String _prmOrg, String _prmUserID, String _prmCode)
        {
            RegistrationBL _regBL = new RegistrationBL();
            bool _result = false;

            _result = _regBL.ActivateUser(_prmOrg, _prmUserID);

            return _result;
        }
    }
}