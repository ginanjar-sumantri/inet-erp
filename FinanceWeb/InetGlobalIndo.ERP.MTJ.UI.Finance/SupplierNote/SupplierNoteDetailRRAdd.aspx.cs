using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Finance;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web.UI.HtmlControls;

namespace InetGlobalIndo.ERP.MTJ.UI.Finance.SupplierNote
{
    public partial class SupplierNoteDetailRRAdd : SupplierNoteBase
    {
        private SupplierNoteBL _supplierNoteBL = new SupplierNoteBL();
        private UnitBL _unitBL = new UnitBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _no2 = 0;

        private string _awal2 = "ctl00_DefaultBodyContentPlaceHolder_ListRepeater2_ctl";
        private string _akhir2 = "_ListCheckBox2";
        private string _cbox2 = "ctl00_DefaultBodyContentPlaceHolder_AllCheckBox2";
        private string _tempHidden2 = "ctl00$DefaultBodyContentPlaceHolder$TempHidden2";

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

                this.btnSearchRRNo.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findRRNo&configCode=rrno_for_suppnote','_popSearch','width=750,height=300,toolbar=0,location=0,status=0,scrollbars=1')";
                String spawnJS = "<script language='JavaScript'>\n";
                ////////////////////DECLARE FUNCTION FOR CATCHING RR NO SEARCH
                spawnJS += "function findRRNo(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.RRNoHIddenField.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.getElementById('" + this.RRFileNoTextBox.ClientID + "').value = dataArray [1];\n";                
                spawnJS += "document.getElementById('" + this.RemarkTextBox.ClientID + "').value = dataArray [6];\n";
                spawnJS += "}\n";
                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";

                //this.ShowProduct();

                //this.SetAttribute();
                this.ClearData();
                this.ShowDataDetail2();
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ClearData()
        {
            this.ClearLabel();
            this.RRNoHIddenField.Value = "";
            this.RRFileNoTextBox.Text = "";
            this.RemarkTextBox.Text = "";
        }

        public void ShowDataDetail2()
        {
            this.TempHidden2.Value = "";

            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater2.DataSource = null;
            }
            else
            {
                this.ListRepeater2.DataSource = this._supplierNoteBL.GetListFINSuppInvRRList(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater2.DataBind();

            this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "' );");

            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            FINSuppInvRRList _temp = (FINSuppInvRRList)e.Item.DataItem;

            string _transNmbr = _temp.TransNmbr;
            string _rrNo = _temp.RRNo;
            string _all = _transNmbr + "-" + _rrNo;

            if (this.TempHidden2.Value == "")
            {
                this.TempHidden2.Value = _all;
            }
            else
            {
                this.TempHidden2.Value += "," + _all;
            }

            Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral2");
            //_no = 0;
            _no2 += 1;
            //_no = _nomor + _no;
            _noLiteral.Text = _no2.ToString();
            //_nomor += 1;

            CheckBox _listCheckbox;
            _listCheckbox = (CheckBox)e.Item.FindControl("ListCheckBox2");
            _listCheckbox.Attributes.Add("OnClick", "CheckBox_Click(" + this.CheckHidden2.ClientID + ", " + _listCheckbox.ClientID + ", '" + _all + "', '" + _awal2 + "', '" + _akhir2 + "', '" + _cbox2 + "')");

            HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
            _tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");
            if (e.Item.ItemType == ListItemType.Item)
            {
                _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
                _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
            }
            if (e.Item.ItemType == ListItemType.AlternatingItem)
            {
                _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
                _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
            }

            Literal _rrNoLiteral = (Literal)e.Item.FindControl("RRNoLiteral");
            _rrNoLiteral.Text = _temp.RRNoFileNmbr;

            Literal _remarkLiteral = (Literal)e.Item.FindControl("RemarkLiteral");
            _remarkLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);
        }

        protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden2.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._supplierNoteBL.DeleteMultiFINSuppInvRRList(_tempSplit);

            if (_result == true)
            {
                this.WarningLabel2.Text = "Delete Success";
            }
            else
            {
                this.WarningLabel2.Text = "Delete Failed";
            }

            this.CheckHidden2.Value = "";
            this.AllCheckBox2.Checked = false;

            this.ShowDataDetail2();
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            FINSuppInvRRList _finSuppInvRRList = new FINSuppInvRRList();

            string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);

            _finSuppInvRRList.TransNmbr = _transNo;
            _finSuppInvRRList.RRNo = this.RRNoHIddenField.Value;
            _finSuppInvRRList.Remark = this.RemarkTextBox.Text;

            String _result = "";

            if (this.RRNoHIddenField.Value.Trim() != "")
            {
                _result = this._supplierNoteBL.AddFINSuppInvRRList(_transNo, _finSuppInvRRList);
            }

            if (_result == "")
            {
                this.ClearData();
                this.WarningLabel.Text = "Your Success Add Data";
                this.ShowDataDetail2();
            }
            else
            {
                this.ClearData();
                this.WarningLabel.Text = "Your Failed Add Data" + ", " + _result;
            }
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}