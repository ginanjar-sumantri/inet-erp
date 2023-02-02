using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Authentication;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;

namespace InetGlobalIndo.ERP.MTJ.UI.Settings.User
{
    public partial class UserQuestionAndAnswer : UserBase
    {
        private UserBL _userBL = new UserBL();
        //private MembershipService _serviceBL = new MembershipService();

        private string _message = "";
        private string _userId = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveQuestionButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                if (this._nvcExtractor.GetValue(this._message) != "")
                {
                    this.WarningLabel.Text = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._message), ApplicationConfig.EncryptionKey);
                }

                this.ShowData();
                this.ClearLabel();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        public void ShowData()
        {
            aspnet_Membership _aspmembership = this._userBL.GetSingleMembership(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            aspnet_User _aspuser = this._userBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.UserNameLabel.Text = _aspuser.UserName;
            this._userId = _aspmembership.UserId.ToString();
            this.QuestionTextBox.Text = this._userBL.GetQuestionByUserId(_userId);

            this.AnswerTextBox.Text = "";
        }

        protected void SaveQuestionButton_Click(object sender, ImageClickEventArgs e)
        {
            //_serviceBL = new MembershipService();

            bool _resultPass = false;
            if (this.QuestionTextBox.Text != "" || this.AnswerTextBox.Text != "")
            {
                //string _oldAnswer = _userBL.GetAnswerByCode(this._userBL.GetUserIdByUserName(this.UserNameLabel.Text));
                //string _password = _serviceBL.GetPassword(this.UserNameLabel.Text, _oldAnswer);
                _resultPass = this._userBL.ChangeQuestionAndAnswer(this.UserNameLabel.Text, this.PassTextBox.Text, this.QuestionTextBox.Text, this.AnswerTextBox.Text);
            }

            this.ClearLabel();
            if (_resultPass == true)
            {
                //Response.Redirect(this._editPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                this.WarningLabel.Text = "You succesfully edited the security question";
            }
            else
            {
                this.WarningLabel.Text = "You Failed Change Security Question";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearLabel();
            this.ShowData();
        }
    }
}