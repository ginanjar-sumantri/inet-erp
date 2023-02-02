using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.Common;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.MenuServiceType
{
    public partial class MenuServiceTypeDtEdit : MenuServiceTypeBase
    {
        private MenuServiceTypeBL _menuServiceTypeBL = new MenuServiceTypeBL();
        private PermissionBL _permBL = new PermissionBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);
            
            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                //this.MenuServiceTypeCodeTextBox.Attributes.Add("OnKeyUp", "HarusAngka(this);");
                //this.DiscountAmountTextBox.Attributes.Add("OnChange", "HarusAngka(this);");
                this.MenuServiceTypeCodeTextBox.Attributes.Add("ReadOnly", "True");

                DDLProductSubGroup();
                this.ShowData();

                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void DDLProductSubGroup()
        {
            this.ProductSubGroupDropDownList.Items.Clear();
            this.ProductSubGroupDropDownList.DataTextField = "ProductSubGrpName";
            this.ProductSubGroupDropDownList.DataValueField = "ProductSubGrpCode";
            this.ProductSubGroupDropDownList.DataSource = _menuServiceTypeBL.GetListDDL();
            this.ProductSubGroupDropDownList.DataBind();
        }

        private void ClearData()
        {
            this.ClearLabel();
        }
        private void ShowData()
        {
            String _subProduct = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
            String _menutype = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey2), ApplicationConfig.EncryptionKey);
            POSMsMenuServiceTypeDt _msMenuServiceType = this._menuServiceTypeBL.GetSinglePOSMsMenuServiceTypeDt(_subProduct, _menutype);
                        
            this.MenuServiceTypeCodeTextBox.Text = _msMenuServiceType.MenuServiceTypeCode;
            this.ProductSubGroupDropDownList.SelectedValue = _msMenuServiceType.ProductSubGroup;
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            POSMsMenuServiceTypeDt _posMsMenuServiceTypeDt = new POSMsMenuServiceTypeDt();

            _posMsMenuServiceTypeDt.MenuServiceTypeCode = this.MenuServiceTypeCodeTextBox.Text;
            _posMsMenuServiceTypeDt.ProductSubGroup = this.ProductSubGroupDropDownList.SelectedValue;

            bool _result = this._menuServiceTypeBL.EditPOSMsMenuServiceTypeDt(_posMsMenuServiceTypeDt);

            if (_result == true)
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "You Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey2))); //+ "&" + this._codeKey2 + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey2)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}