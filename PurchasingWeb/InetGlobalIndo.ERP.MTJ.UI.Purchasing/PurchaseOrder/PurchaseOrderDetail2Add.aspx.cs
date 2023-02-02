using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Purchasing;
using InetGlobalIndo.ERP.MTJ.BusinessRule.StockControl;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using System.Web.UI.HtmlControls;

namespace InetGlobalIndo.ERP.MTJ.UI.Purchasing.PurchaseOrder
{
    public partial class PurchaseOrderDetail2Add : PurchaseOrderBase
    {
        private PurchaseOrderBL _purchaseOrderBL = new PurchaseOrderBL();
        private PurchaseRequestBL _purchaseRequestBL = new PurchaseRequestBL();
        private UnitBL _unitBL = new UnitBL();
        private ProductBL _productBL = new ProductBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _prmRevisi = 0;
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
                this.PageTitleLiteral.Text = this._pageTitleLiteral2;

                this.btnSearchPRNo.OnClientClick = "window.open('../Search/Search.aspx?valueCatcher=findRequestNo&configCode=prc_req_for_po','_popSearch','width=750,height=300,toolbar=0,location=0,status=0,scrollbars=1')";
                String spawnJS = "<script language='JavaScript'>\n";
                ////////////////////DECLARE FUNCTION FOR CATCHING REQUEST NO SEARCH
                spawnJS += "function findRequestNo(x) {\n";
                spawnJS += "dataArray = x.split ('|') ;\n";
                spawnJS += "document.getElementById('" + this.RequestNoHiddenField.ClientID + "').value = dataArray [0];\n";
                spawnJS += "document.getElementById('" + this.RequestFileNoTextBox.ClientID + "').value = dataArray [1];\n";
                spawnJS += "document.getElementById('" + this.RequestByTextBox.ClientID + "').value = dataArray [4];\n";
                spawnJS += "}\n";
                spawnJS += "</script>\n";
                this.javascriptReceiver.Text = spawnJS;

                this.SaveButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.BackButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/back.jpg";
                this.DeleteButton2.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";

                //this.ShowProduct();

                //this.SetAttribute();
                this.ClearData();
                this._prmRevisi = Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisi), ApplicationConfig.EncryptionKey));
                this.ShowDataDetail2(_prmRevisi);
            }
        }

        protected void ClearLabel()
        {
            this.WarningLabel.Text = "";
        }

        protected void ClearData()
        {
            this.ClearLabel();
            this.RequestFileNoTextBox.Text = "";
            this.RequestNoHiddenField.Value = "";
            this.RequestByTextBox.Text = "";
        }

        public void ShowDataDetail2(int _prmRevisiNo)
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
                this.ListRepeater2.DataSource = this._purchaseOrderBL.GetListDDLPRNoVPRRequestForPO(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey), _prmRevisiNo);
            }
            this.ListRepeater2.DataBind();

            this.AllCheckBox2.Attributes.Add("OnClick", "CheckBox_ClickAll(" + this.AllCheckBox2.ClientID + ", " + this.CheckHidden2.ClientID + ", '" + _awal2 + "', '" + _akhir2 + "', '" + _tempHidden2 + "' );");

            this.DeleteButton2.Attributes.Add("OnClick", "return AskYouFirst();");
        }

        protected void ListRepeater2_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            V_PRRequestForPO _temp = (V_PRRequestForPO)e.Item.DataItem;

            string _transNmbr = _temp.PR_No;
            string _all = _transNmbr;

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

            Literal _requestNoLiteral = (Literal)e.Item.FindControl("RequestNoLiteral");
            _requestNoLiteral.Text = _temp.FileNmbr;

            //Literal _qtyLiteral = (Literal)e.Item.FindControl("QtyOutstandingLiteral");
            //_qtyLiteral.Text = (_temp.Qty) == 0 ? "0" : _temp.Qty.ToString();

            //Literal _unitLiteral = (Literal)e.Item.FindControl("UnitLiteral");
            //_unitLiteral.Text = HttpUtility.HtmlEncode(_temp.UnitName);

            Literal _requestByLiteral = (Literal)e.Item.FindControl("RequestByLiteral");
            _requestByLiteral.Text = HttpUtility.HtmlEncode(_temp.RequestBy);
        }

        protected void DeleteButton2_Click(object sender, ImageClickEventArgs e)
        {
            string _temp = this.CheckHidden2.Value;
            string[] _tempSplit = _temp.Split(',');

            this.ClearLabel();

            bool _result = this._purchaseOrderBL.DeleteMultiPRCPODt2(_tempSplit);

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

            this._prmRevisi = Convert.ToInt32(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisi), ApplicationConfig.EncryptionKey));
            this.ShowDataDetail2(_prmRevisi);
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.RequestNoHiddenField.Value != "" && this.RequestNoHiddenField.Value != null)
            {
                PRCPODt2 _prcPODt2 = new PRCPODt2();

                string _transNo = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey);
                string _revisi = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeRevisi), ApplicationConfig.EncryptionKey);

                _prcPODt2.TransNmbr = _transNo;
                _prcPODt2.Revisi = Convert.ToInt32(_revisi);
                _prcPODt2.RequestNo = this.RequestNoHiddenField.Value;
                _prcPODt2.QtyRR = 0;

                _prcPODt2.CreatedBy = HttpContext.Current.User.Identity.Name;
                _prcPODt2.CreatedDate = DateTime.Now;
                _prcPODt2.EditBy = HttpContext.Current.User.Identity.Name;
                _prcPODt2.EditDate = DateTime.Now;

                bool _result = this._purchaseOrderBL.AddPRCPODt2(_prcPODt2);

                if (_result == true)
                {
                    this.ClearData();
                    this.WarningLabel.Text = "Your Success Add Data";
                    this.ShowDataDetail2(Convert.ToInt32(_revisi));
                    //Response.Redirect(this._addDetailPage2 + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisi)));
                }
                else
                {
                    this.ClearData();
                    this.WarningLabel.Text = "Your Failed Add Data";
                }
            }
        }

        protected void BackButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._detailPage + "?" + this._codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)) + "&" + this._codeRevisi + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeRevisi)));
        }

        protected void ResetButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ClearData();
        }
    }
}