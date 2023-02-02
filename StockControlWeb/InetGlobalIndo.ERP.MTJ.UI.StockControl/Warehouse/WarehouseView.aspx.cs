using System;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.Warehouse
{
    public partial class WarehouseView : WarehouseBase
    {
        private WarehouseBL _warehouse = new WarehouseBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.WarehouseGroupTextBox.Attributes.Add("ReadOnly", "True");
                this.WarehouseAreaTextBox.Attributes.Add("ReadOnly", "True");

                this.ShowData();
            }
        }

        public void ShowData()
        {
            MsWarehouse _msWarehouse = this._warehouse.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.WarehouseCodeTextBox.Text = _msWarehouse.WrhsCode;
            this.WarehouseNameTextBox.Text = _msWarehouse.WrhsName;
            this.WarehouseGroupTextBox.Text = _msWarehouse.WrhsGroup;
            this.WarehouseGroupNameTextBox.Text = _warehouse.GetWrhsGroupNameByCode(_msWarehouse.WrhsGroup);
            this.WarehouseAreaTextBox.Text = _msWarehouse.WrhsArea;
            this.WarehouseAreaNameTextBox.Text = _warehouse.GetWrhsAreaNameByCode(_msWarehouse.WrhsArea);
            this.IsActiveCheckBox.Checked = WarehouseDataMapper.IsActive(_msWarehouse.FgActive);
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + this._nvcExtractor.GetValue(this._codeKey));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }
    }
}