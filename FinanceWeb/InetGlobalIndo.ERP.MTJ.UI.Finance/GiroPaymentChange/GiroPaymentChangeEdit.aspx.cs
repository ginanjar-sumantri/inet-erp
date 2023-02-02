using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.GiroPaymentChange
{
    public partial class GiroPaymentChangeEdit : GiroPaymentChangeBase
    {
        private FINChangeGiroOutBL _finChangeGiroOutBL = new FINChangeGiroOutBL();
        private SupplierBL _suppBL = new SupplierBL();
        private CustomerBL _custBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.ViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view_detail.jpg";
                this.SaveAndViewDetailButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save_and_view_detail.jpg";

                this.SetAttribute(); 
                this.ClearLabel();
                this.ShowData();
            }
        }

        private void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly","True");

            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void TypeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.TypeDropDownList.SelectedValue == "Cust")
            {
                this.ShowCust();
            }
            else if (this.TypeDropDownList.SelectedValue == "Supp")
            {
                this.ShowSupp();
            }
        }

        private void ShowCust()
        {
            this.CodeDropDownList.Items.Clear();
            this.CodeDropDownList.DataTextField = "CustName";
            this.CodeDropDownList.DataValueField = "CustCode";
            this.CodeDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.CodeDropDownList.DataBind();
            this.CodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowSupp()
        {
            this.CodeDropDownList.Items.Clear();
            this.CodeDropDownList.DataTextField = "SuppName";
            this.CodeDropDownList.DataValueField = "SuppCode";
            this.CodeDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.CodeDropDownList.DataBind();
            this.CodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowData()
        {
            FINChangeGiroOutHd _finChangeGiroOutHd = this._finChangeGiroOutBL.GetSingleFINChangeGiroOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.TransNoTextBox.Text = _finChangeGiroOutHd.TransNmbr;
            this.FileNmbrTextBox.Text = _finChangeGiroOutHd.FileNmbr;
            this.DateTextBox.Text = DateFormMapper.GetValue(_finChangeGiroOutHd.TransDate);

            if (_finChangeGiroOutHd.SuppCode != null && _finChangeGiroOutHd.SuppCode.Trim() != "")
            {
                this.TypeDropDownList.SelectedValue = "Supp";
                this.ShowSupp();
                this.CodeDropDownList.Text = _finChangeGiroOutHd.SuppCode;
            }
            else if (_finChangeGiroOutHd.CustCode != null && _finChangeGiroOutHd.CustCode.Trim() != "")
            {
                this.TypeDropDownList.SelectedValue = "Cust";
                this.ShowCust();
                this.CodeDropDownList.Text = _finChangeGiroOutHd.CustCode;
            }

            this.RemarkTextBox.Text = _finChangeGiroOutHd.Remark;
        }


        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINChangeGiroOutHd _finChangeGiroOutHd = this._finChangeGiroOutBL.GetSingleFINChangeGiroOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finChangeGiroOutHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            if (this.TypeDropDownList.SelectedValue == "Cust")
            {
                _finChangeGiroOutHd.CustCode = this.CodeDropDownList.SelectedValue;
                _finChangeGiroOutHd.SuppCode = "";
            }
            else if (this.TypeDropDownList.SelectedValue == "Supp")
            {
                _finChangeGiroOutHd.SuppCode = this.CodeDropDownList.SelectedValue;
                _finChangeGiroOutHd.CustCode = "";
            }

            _finChangeGiroOutHd.Remark = this.RemarkTextBox.Text;
            _finChangeGiroOutHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finChangeGiroOutHd.DatePrep = DateTime.Now;

            bool _result = this._finChangeGiroOutBL.EditFINChangeGiroOutHd(_finChangeGiroOutHd);

            if (_result == true)
            {
                Response.Redirect(this._homePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ShowData();
        }

        protected void ViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void SaveAndViewDetailButton_Click(object sender, ImageClickEventArgs e)
        {
            FINChangeGiroOutHd _finChangeGiroOutHd = this._finChangeGiroOutBL.GetSingleFINChangeGiroOutHd(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            _finChangeGiroOutHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);

            if (this.TypeDropDownList.SelectedValue == "Cust")
            {
                _finChangeGiroOutHd.CustCode = this.CodeDropDownList.SelectedValue;
                _finChangeGiroOutHd.SuppCode = "";
            }
            else if (this.TypeDropDownList.SelectedValue == "Supp")
            {
                _finChangeGiroOutHd.SuppCode = this.CodeDropDownList.SelectedValue;
                _finChangeGiroOutHd.CustCode = "";
            }

            _finChangeGiroOutHd.Remark = this.RemarkTextBox.Text;
            _finChangeGiroOutHd.UserPrep = HttpContext.Current.User.Identity.Name;
            _finChangeGiroOutHd.DatePrep = DateTime.Now;

            bool _result = this._finChangeGiroOutBL.EditFINChangeGiroOutHd(_finChangeGiroOutHd);

            if (_result == true)
            {
                Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Edit Data";
            }
        }
    }
}