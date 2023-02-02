using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Sales;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;

namespace InetGlobalIndo.ERP.MTJ.UI.StockControl.DirectBillOfLading
{
    public partial class DirectBillOfLadingDetailAdd : DirectBillOfLadingBase
    {
        private DirectBillOfLadingBL _directBillOfLadingBL = new DirectBillOfLadingBL();
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
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                STCTrDirectSJHd _headerDirectSJ = _directBillOfLadingBL.GetSingleSTCTrDirectSJHd ( Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                String _customerCodeHeader = _headerDirectSJ.CustCode;
                this.btnSearch.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findSoNumber&configCode=soNumberDirectBOL&paramWhere=CustCodesamadenganpetik" + _customerCodeHeader + "petik','_popSearch','width=800,height=600,toolbar=0,location=0,status=0,scrollbars=1')";
                String spawnJS = "<script language='JavaScript'>\n";
                ////////////////////DECLARE FUNCTION FOR CATCHING CUSTOMER SEARCH
                spawnJS += "function findSoNumber(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.SoNoTextBox.ClientID + "').value = dataArray[0];\n";
                spawnJS += "document.getElementById('" + this.transDateTextBox.ClientID + "').value = dataArray[2];\n";
                spawnJS += "}\n";
                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

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
        }

        private void ClearData()
        {
            this.ClearLabel();
            this.SoNoTextBox.Text = "";
            this.transDateTextBox.Text = "";
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            STCTrDirectSJSO _stcTrDirectSJSO = new STCTrDirectSJSO();

            /////////////////////VALIDASI APAKAH PUNYA DETAIL YANG QTYDO > QTY
            if (_directBillOfLadingBL.CekOutstandingQty(this.SoNoTextBox.Text))
            {
                _stcTrDirectSJSO.TransNmbr = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                _stcTrDirectSJSO.SONo = this.SoNoTextBox.Text;

                bool _result = this._directBillOfLadingBL.AddSTCTrDirectSJSO(_stcTrDirectSJSO);

                if (_result != false)
                {
                    Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
                }
                else
                {
                    this.ClearLabel();
                    this.WarningLabel.Text = "You Failed to Add Data";
                }
            }
            else {
                this.ClearLabel();
                this.WarningLabel.Text = "This Sales Order Has already been delivered.";
            }

        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._viewPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

    }
}