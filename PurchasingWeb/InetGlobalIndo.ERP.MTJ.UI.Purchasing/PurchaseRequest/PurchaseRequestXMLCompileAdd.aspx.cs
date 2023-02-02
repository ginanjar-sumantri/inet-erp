using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.HumanResource;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseRequest
{
    public partial class PurchaseRequestXMLCompileAdd : PurchaseRequestBase
    {
        private PurchaseRequestBL _purchaseRequestBL = new PurchaseRequestBL();
        private PermissionBL _permBL = new PermissionBL();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuIDCompileXML, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permAdd = this._permBL.PermissionValidation1(this._menuIDCompileXML, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Add);

            if (this._permAdd == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageCompileTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";

                this.SetAttribute();
                this.ShowXmlFileDDL();
                this.ShowCustomerDDL();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ShowXmlFileDDL()
        {
            try
            {
                this.XmlFileDDL.Items.Clear();
                foreach (string file in System.IO.Directory.GetFiles(ApplicationConfig.UploadXMLPurchaseRequestHQPath))
                {
                    if (!this._purchaseRequestBL.isXMLFileHQUsed(System.IO.Path.GetFileName(file)))
                    {
                        String xmlFileName = System.IO.Path.GetFileName(file);
                        this.XmlFileDDL.Items.Add(new ListItem(xmlFileName, xmlFileName));
                    }
                }
                this.XmlFileDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void ShowCustomerDDL() { 
            CustomerBL _customerBL = new CustomerBL();
            this.CustomerDropDownList.DataSource = _customerBL.GetListCustForDDLForReport();
            this.CustomerDropDownList.DataValueField = "CustCode";
            this.CustomerDropDownList.DataTextField = "CustName";
            this.CustomerDropDownList.DataBind();
        }

        public void SetAttribute()
        {
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        private void ClearData()
        {
            this.ClearLabel();
            this.XmlFileDDL.SelectedValue = "null";
            this.RemarkTextBox.Text = "";
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            PRCPRxmlList _saveData = new PRCPRxmlList();
            //_saveData.SenderCode = this.CustomerCodeTextBox.Text;
            _saveData.SenderCode = this.CustomerDropDownList.SelectedValue;
            _saveData.Status = 2;
            _saveData.XMLFileName = this.XmlFileDDL.SelectedValue;
            _saveData.Remark = this.RemarkTextBox.Text;

            if (this._purchaseRequestBL.AddXMLListHQ(_saveData))
            {
                Response.Redirect(this._compilePage);
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_compilePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}