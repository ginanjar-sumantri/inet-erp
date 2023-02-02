using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Sales.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using WebAccessGlobalIndo.ERP.MTJ.BusinessRule.Purchasing.Master;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using System.Collections.Generic;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.ReceivingPO
{
    public partial class ReceivingPOAdd : ReceivingPOBase
    {
        private ReceivingPOBL _receivingPOBL = new ReceivingPOBL();
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
        private WarehouseBL _warehouseBL = new WarehouseBL();
        private CustomerBL _custBL = new CustomerBL();
        private SupplierBL _suppBL = new SupplierBL();
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

            this.btnSearchSupplier.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findSupplier&configCode=supplier','_popSearch','width=400,height=550,toolbar=0,location=0,status=0,scrollbars=1')";

            String spawnJS2 = "<script language='JavaScript'>\n";

            ////////////////////DECLARE FUNCTION FOR CATCHING SUPPLIER SEARCH
            spawnJS2 += "function findSupplier(x) {\n";
            spawnJS2 += "dataArray = x.split ('|') ;\n";
            spawnJS2 += "document.getElementById('" + this.SupplierTextBox.ClientID + "').value = dataArray[0];\n";
            spawnJS2 += "document.getElementById('" + this.SupplierLabel.ClientID + "').innerHTML = dataArray[1];\n";
            spawnJS2 += "document.forms[0].submit();\n";
            spawnJS2 += "}\n";

            spawnJS2 += "</script>\n";
            this.javascriptReceiver2.Text = spawnJS2;

            this.btnSearchSJNo.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findPONo&configCode=receiving_po_no&paramWhere=Supp_Codesamadenganpetik" + this.SupplierTextBox.Text + "petik','_popSearch','width=1200,height=700,toolbar=0,location=0,status=0,scrollbars=1')";
            if (!this.Page.IsPostBack == true)
            {
                this.SJSuppDateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.SJSuppDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                
                String spawnJS = "<script language='JavaScript'>\n";
                ////////////////////DECLARE FUNCTION FOR CATCHING SJ NO SEARCH
                spawnJS += "function findPONo(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.FileNoTextBox.ClientID + "').value = dataArray [1];\n";
                spawnJS += "document.getElementById('" + this.PONoHiddenField.ClientID + "').value = dataArray [0];\n";
                //spawnJS += "document.getElementById('" + this.RemarkTextBox.ClientID + "').value = dataArray [11];\n";
                spawnJS += "}\n";
                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.NextButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/next.jpg";
                this.ResetButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/reset.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";
                this.DateLiteral.Text = "<input id='button1' type='button' onclick='displayCalendar(" + this.DateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";
                this.SJSuppDateLiteral.Text = "<input id='button2' type='button' onclick='displayCalendar(" + this.SJSuppDateTextBox.ClientID + ",&#39" + DateFormMapper.GetFormat() + "&#39,this)' value='...' />";

                this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
                this.ShowWarehouse();
                this.ShowLocation();

                this.SetAttribute();
                this.ClearData();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void SetAttribute()
        {
            this.DateTextBox.Attributes.Add("ReadOnly", "True");
            this.SJSuppDateTextBox.Attributes.Add("ReadOnly", "True");
            this.FgLocationCheckBox.Attributes.Add("OnClick", "CheckUncheck(" + this.FgLocationCheckBox.ClientID + "," + this.WrhsLocationDropDownList.ClientID + ");");
            this.RemarkTextBox.Attributes.Add("OnKeyDown", "return textCounter(" + this.RemarkTextBox.ClientID + "," + this.CounterTextBox.ClientID + ",500" + ");");
        }

        public void ShowPONo()
        {
            //this.PONoDropDownList.Items.Clear();
            //this.PONoDropDownList.DataTextField = "FileNmbr";
            //this.PONoDropDownList.DataValueField = "TransNmbr";
            //this.PONoDropDownList.DataSource = this._purchaseOrderBL.GetPONoFromVPRPOForRR(this.SupplierDropDownList.SelectedValue);
            //this.PONoDropDownList.DataBind();
            //this.PONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowWarehouse()
        {
            this.WarehouseDropDownList.Items.Clear();
            this.WarehouseDropDownList.DataTextField = "WrhsName";
            this.WarehouseDropDownList.DataValueField = "WrhsCode";
            this.WarehouseDropDownList.DataSource = this._warehouseBL.GetListForDDLActive();
            this.WarehouseDropDownList.DataBind();
            this.WarehouseDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        private void ShowLocation()
        {
            this.WrhsLocationDropDownList.Items.Clear();
            this.WrhsLocationDropDownList.DataTextField = "WLocationName";
            this.WrhsLocationDropDownList.DataValueField = "WLocationCode";
            this.WrhsLocationDropDownList.DataSource = this._warehouseBL.GetListWrhsLocationByCodeForDDL(this.WarehouseDropDownList.SelectedValue);
            this.WrhsLocationDropDownList.DataBind();
            this.WrhsLocationDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowCust()
        {
            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.DataTextField = "CustName";
            this.SubledDropDownList.DataValueField = "CustCode";
            this.SubledDropDownList.DataSource = this._custBL.GetListCustomerForDDL();
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        public void ShowSupp()
        {
            this.SubledDropDownList.Items.Clear();
            this.SubledDropDownList.DataTextField = "SuppName";
            this.SubledDropDownList.DataValueField = "SuppCode";
            this.SubledDropDownList.DataSource = this._suppBL.GetListDDLSupp();
            this.SubledDropDownList.DataBind();
            this.SubledDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        //public void ShowSupplier()
        //{
        //    this.SupplierDropDownList.Items.Clear();
        //    this.SupplierDropDownList.DataTextField = "SuppName";
        //    this.SupplierDropDownList.DataValueField = "SuppCode";
        //    this.SupplierDropDownList.DataSource = this._suppBL.GetListDDLSupp();
        //    this.SupplierDropDownList.DataBind();
        //    this.SupplierDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //}

        public void ClearData()
        {
            DateTime now = DateTime.Now;

            this.ClearLabel();
            this.DateTextBox.Text = DateFormMapper.GetValue(now);
            this.SJSuppDateTextBox.Text = DateFormMapper.GetValue(now);
            this.RemarkTextBox.Text = "";
            this.SJSuppNoTextBox.Text = "";
            this.CarNoTextBox.Text = "";
            this.DriverTextBox.Text = "";
            this.SupplierTextBox.Text = "";
            this.SupplierLabel.Text = "";
            this.WarehouseDropDownList.SelectedValue = "null";
            this.SubledDropDownList.SelectedValue = "null";
            //this.PONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
            //this.PONoDropDownList.SelectedValue = "null";
            this.FileNoTextBox.Text = "";
            this.PONoHiddenField.Value = "";
            this.FgLocationCheckBox.Checked = false;
            this.WrhsLocationDropDownList.Enabled = false;
        }

        protected void WarehouseDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowLocation();

            if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.NoSubled))
            {
                this.SubledDropDownList.Enabled = false;
                this.SubledDropDownList.SelectedValue = "null";
            }
            else
            {
                this.SubledDropDownList.Enabled = true;
                if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Customer))
                {
                    this.ShowCust();
                }
                else if (_warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue) == WarehouseDataMapper.GetSubledStatus(WarehouseSubledStatus.Supplier))
                {
                    this.ShowSupp();
                }
            }
        }

        //protected void SupplierDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DateTime now = DateTime.Now;

        //    this.ClearLabel();
        //    //this.SJSuppDateTextBox.Text = DateFormMapper.GetValue(now);
        //    this.RemarkTextBox.Text = "";
        //    this.SJSuppNoTextBox.Text = "";
        //    this.CarNoTextBox.Text = "";
        //    this.DriverTextBox.Text = "";
        //    this.WarehouseDropDownList.SelectedValue = "null";
        //    this.SubledDropDownList.SelectedValue = "null";
        //    //this.PONoDropDownList.Items.Insert(0, new ListItem("[Choose One]", "null"));
        //    //this.PONoDropDownList.SelectedValue = "null";
        //    this.FileNoTextBox.Text = "";
        //    this.PONoHiddenField.Value = "";
        //    this.FgLocationCheckBox.Checked = false;
        //    this.WrhsLocationDropDownList.Enabled = false;

        //}

        protected void SupplierTextBox_TextChanged(object sender, EventArgs e)
        {
            if (this.SupplierTextBox.Text != "")
            {
                this.SupplierLabel.Text = _suppBL.GetSuppNameByCode(this.SupplierTextBox.Text);
            }
            else
            {
                this.SupplierLabel.Text = " ";
            }
            this.RemarkTextBox.Text = "";
            this.SJSuppNoTextBox.Text = "";
            this.CarNoTextBox.Text = "";
            this.DriverTextBox.Text = "";
            this.WarehouseDropDownList.SelectedValue = "null";
            this.SubledDropDownList.SelectedValue = "null";
            this.FileNoTextBox.Text = "";
            this.PONoHiddenField.Value = "";
            this.FgLocationCheckBox.Checked = false;
            this.WrhsLocationDropDownList.Enabled = false;
        }

        protected void NextButton_Click(object sender, ImageClickEventArgs e)
        {
            STCReceiveHd _stcReceiveHd = new STCReceiveHd();

            _stcReceiveHd.TransDate = new DateTime(DateFormMapper.GetValue(this.DateTextBox.Text).Year, DateFormMapper.GetValue(this.DateTextBox.Text).Month, DateFormMapper.GetValue(this.DateTextBox.Text).Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            _stcReceiveHd.Status = ReceivingPODataMapper.GetStatus(TransStatus.OnHold);
            _stcReceiveHd.SuppCode = this.SupplierTextBox.Text;
            _stcReceiveHd.PONo = this.PONoHiddenField.Value;
            //_stcReceiveHd.PONo = this.PONoDropDownList.SelectedValue;
            _stcReceiveHd.WrhsCode = this.WarehouseDropDownList.SelectedValue;
            _stcReceiveHd.WrhsFgSubLed = _warehouseBL.GetWarehouseFgSubledByCode(this.WarehouseDropDownList.SelectedValue);
            if (this.SubledDropDownList.SelectedValue != "null")
            {
                _stcReceiveHd.WrhsSubLed = this.SubledDropDownList.SelectedValue;
            }
            else
            {
                _stcReceiveHd.WrhsSubLed = "";
            }
            _stcReceiveHd.SJSupplierNo = this.SJSuppNoTextBox.Text;
            _stcReceiveHd.SJSupplierDate = DateFormMapper.GetValue(this.SJSuppDateTextBox.Text);
            _stcReceiveHd.CarNo = this.CarNoTextBox.Text;
            _stcReceiveHd.Driver = this.DriverTextBox.Text;
            _stcReceiveHd.Remark = this.RemarkTextBox.Text;
            _stcReceiveHd.FgProcess = YesNoDataMapper.GetYesNo(YesNo.No);
            _stcReceiveHd.DoneInvoice = YesNoDataMapper.GetYesNo(YesNo.No);
            _stcReceiveHd.DoneShipping = YesNoDataMapper.GetYesNo(YesNo.No);
            _stcReceiveHd.RRType = AppModule.GetValue(TransactionType.ReceivingPO);
            _stcReceiveHd.CreatedBy = HttpContext.Current.User.Identity.Name;
            _stcReceiveHd.CreatedDate = DateTime.Now;
            _stcReceiveHd.EditBy = HttpContext.Current.User.Identity.Name;
            _stcReceiveHd.EditDate = DateTime.Now;
            _stcReceiveHd.FgLocation = this.FgLocationCheckBox.Checked;
            _stcReceiveHd.LocationCode = this.WrhsLocationDropDownList.SelectedValue;

            //List<STCReceiveDt> _listDetail = new List<STCReceiveDt>();

            //if (this.FgLocationCheckBox.Checked == true)
            //{
            //    List<PRCPODt> _prcPODt = _purchaseOrderBL.GetListPRCPODtForStcRecvPO(this.PONoHiddenField.Value, _purchaseOrderBL.GetLastRevisiPRCPOHd(this.PONoHiddenField.Value));

            //    foreach (PRCPODt _item in _prcPODt)
            //    {
            //        STCReceiveDt _stcReceiveDt = new STCReceiveDt();

            //        _stcReceiveDt.ProductCode = _item.ProductCode;
            //        _stcReceiveDt.LocationCode = this.WrhsLocationDropDownList.SelectedValue;
            //        _stcReceiveDt.Unit = _item.Unit;
            //        _stcReceiveDt.Qty = _item.Qty - Convert.ToInt32(_item.QtyRR);
            //        _stcReceiveDt.Remark = "";

            //        _listDetail.Add(new STCReceiveDt("", _stcReceiveDt.ProductCode, _stcReceiveDt.LocationCode, _stcReceiveDt.Qty, _stcReceiveDt.Unit, _stcReceiveDt.Remark));
            //    }
            //}
            string _result = this._receivingPOBL.AddSTCReceiveHd(_stcReceiveHd);

            if (_result != "")
            {
                if (_result.Substring(0, 5) == ApplicationConfig.Error)
                {
                    this.WarningLabel.Text = _result.Substring(6, _result.Length - 6);
                }
                else
                {
                    Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(Rijndael.Encrypt(_result, ApplicationConfig.EncryptionKey)));
                }
            }
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}