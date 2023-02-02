using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.DirectBillOfLading
{
    public partial class DirectBillOfLadingAdd : DirectBillOfLadingBase
    {
        private DirectBillOfLadingBL _directDirectBillOfLadingBL = new DirectBillOfLadingBL();
        private CustomerBL _custBL = new CustomerBL();
        private PermissionBL _permBL = new PermissionBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();

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
            if (!this.Page.IsPostBack == true)
            {
                this.TransDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.TransDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.BtnSearchCustomer.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findCustomer&configCode=customer','_popSearch','width=1200,height=1000,toolbar=0,location=0,status=0,scrollbars=1')";
                String spawnJS = "<script language='JavaScript'>\n";
                ////////////////////DECLARE FUNCTION FOR CATCHING CUSTOMER SEARCH
                spawnJS += "function findCustomer(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.CustCodeTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.CustomerNameTextBox.ClientID + "').value = dataArray[1];\n";
                spawnJS += "}\n";
                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.ShowWarehouse();
                this.ShowLocation();

                this.SetAttribute();
                this.ClearData();
            }
        }

        private void SetAttribute()
        {
            this.TransDateTextBox.Attributes.Add("ReadOnly", "True");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        private void ShowWarehouse()
        {
            this.WarehouseCodeDropDownList.DataTextField = "WrhsName";
            this.WarehouseCodeDropDownList.DataValueField = "WrhsCode";
            this.WarehouseCodeDropDownList.DataSource = this._warehouseBL.GetListForDDLActive();
            this.WarehouseCodeDropDownList.DataBind();
            this.WarehouseCodeDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowLocation()
        {
            this.WrhsLocationDropDownList.Items.Clear();
            this.WrhsLocationDropDownList.DataTextField = "WLocationName";
            this.WrhsLocationDropDownList.DataValueField = "WLocationCode";
            this.WrhsLocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(this.WarehouseCodeDropDownList.SelectedValue);
            this.WrhsLocationDropDownList.DataBind();
            this.WrhsLocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();
            this.TransDateTextBox.Text = DateFormMapper.GetValue(now);
            this.CustCodeTextBox.Text = "";
            this.CustomerNameTextBox.Text = "";
            this.RemarkTextBox.Text = "";
            this.CounterTextBox.Text = "500";
            this.WrhsSubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            this.WrhsSubledDropDownList.SelectedValue = "null";
            this.FgLocationCheckBox.Checked = false;
            this.WrhsLocationDropDownList.Enabled = false;
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTrDirectSJHd _STCTrDirectSJHd = new STCTrDirectSJHd();

            _STCTrDirectSJHd.Status = DirectBillOfLadingDataMapper.GetStatusByte(TransStatus.OnHold);
            _STCTrDirectSJHd.TransDate = new DateTime(DateFormMapper.GetValue(this.TransDateTextBox.Text).Year, DateFormMapper.GetValue(this.TransDateTextBox.Text).Month, DateFormMapper.GetValue(this.TransDateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _STCTrDirectSJHd.CustCode = this.CustCodeTextBox.Text;
            _STCTrDirectSJHd.Remark = this.RemarkTextBox.Text;
            _STCTrDirectSJHd.CreatedBy = HttpContext.Current.User.Identity.Name;
            _STCTrDirectSJHd.CreatedDate = DateTime.Now;
            _STCTrDirectSJHd.EditBy = HttpContext.Current.User.Identity.Name;
            _STCTrDirectSJHd.EditDate = DateTime.Now;

            _STCTrDirectSJHd.WrhsCode = this.WarehouseCodeDropDownList.SelectedValue;

            _STCTrDirectSJHd.WrhsFgSubLed = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);
            if (this.WrhsSubledDropDownList.SelectedValue != "null")
            {
                _STCTrDirectSJHd.WrhsSubLed = this.WrhsSubledDropDownList.SelectedValue;
            }
            else
            {
                _STCTrDirectSJHd.WrhsSubLed = "";
            }

            _STCTrDirectSJHd.FgLocation = this.FgLocationCheckBox.Checked;
            _STCTrDirectSJHd.LocationCode = this.WrhsLocationDropDownList.SelectedValue;


            string _result = this._directDirectBillOfLadingBL.AddSTCTrDirectSJHd (_STCTrDirectSJHd);

            if (_result != "")
            {
                Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
            }
            else
            {
                this.ClearLabel();
                this.WarningLabel.Text = "Your Failed Add Data";
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }


        protected void WarehouseCodeDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowLocation();

            char _fgSubled = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseCodeDropDownList.SelectedValue);

            if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.WrhsSubledDropDownList.Enabled = false;
                this.WrhsSubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.WrhsSubledDropDownList.Enabled = true;
                if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    //this.ShowCust();
                }
                else if (_fgSubled == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    //this.ShowSupp();
                }
            }
        }



        protected void FgLocationCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            this.WrhsLocationDropDownList.Enabled = this.FgLocationCheckBox.Checked;
        }
}
}