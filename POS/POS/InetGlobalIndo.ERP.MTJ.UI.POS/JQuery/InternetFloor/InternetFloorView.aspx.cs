using System;
using System.Web;
using System.Web.UI;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common.Encryption;
using InetGlobalIndo.ERP.MTJ.BusinessRule.Settings;
using InetGlobalIndo.ERP.MTJ.Common.Enum;
using BusinessRule.POS;
using InetGlobalIndo.ERP.MTJ.DataMapping;
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.InternetFloor
{
    public partial class InternetFloorView : InternetFloorBase
    {
        private InternetFloorBL _internetFloorBL = new InternetFloorBL();
        private PermissionBL _permBL = new PermissionBL();

        private int _no = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {
                this.PageTitleLiteral.Text = this._pageTitleLiteral;

                this.EditButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit2.jpg";
                this.CancelButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/cancel.jpg";

                this.GenerateImageButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/generate.jpg";

                this.FloorTypeTextBox.Attributes.Add("ReadOnly", "True");
                this.FloorNmbrTextBox.Attributes.Add("ReadOnly", "True");
                this.FloorNameTextBox.Attributes.Add("ReadOnly", "True");
                this.RoomCodeTextBox.Attributes.Add("ReadOnly", "True");
                this.DescriptionTextBox.Attributes.Add("ReadOnly", "True");

                this.SetButtonPermission();

                this.ShowData();
            }
        }

        private void SetButtonPermission()
        {
            this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

            if (this._permEdit == PermissionLevel.NoAccess)
            {
                this.EditButton.Visible = false;
            }
        }

        public void ShowData()
        {
            POSMsInternetFloor _posMsInternetFloor = this._internetFloorBL.GetSingle(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));

            this.FloorTypeTextBox.Text = _posMsInternetFloor.FloorType;
            this.FloorNmbrTextBox.Text = _posMsInternetFloor.FloorNmbr.ToString();
            this.FloorNameTextBox.Text = _posMsInternetFloor.FloorName;
            this.RoomCodeTextBox.Text = _posMsInternetFloor.roomCode;
            this.DescriptionTextBox.Text = _posMsInternetFloor.Description;

            ShowDataDetail1();
        }

        protected void EditButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._editPage + "?" + _codeKey + "=" + HttpUtility.UrlEncode(this._nvcExtractor.GetValue(this._codeKey)));
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

        #region Detail

        public void ShowDataDetail1()
        {
            NameValueCollectionExtractor _nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this._permView = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.View);

            if (this._permView == PermissionLevel.NoAccess)
            {
                this.ListRepeater.DataSource = null;
            }
            else
            {
                this.ListRepeater.DataSource = this._internetFloorBL.GetListTableInternet(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            }
            this.ListRepeater.DataBind();
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                POSMsInternetTable _temp = (POSMsInternetTable)e.Item.DataItem;

                string _code = Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey) + "|" + _temp.TableIDPerRoom.ToString();

                Literal _noLiteral = (Literal)e.Item.FindControl("NoLiteral");
                _no += 1;
                _noLiteral.Text = _no.ToString();

                ImageButton _editButton = (ImageButton)e.Item.FindControl("EditButton");
                this._permEdit = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Edit);

                if (this._permEdit == PermissionLevel.NoAccess)
                {
                    _editButton.Visible = false;
                }
                else
                {
                    _editButton.PostBackUrl = this._editDetailPage + "?" + this._tableAndFloorKey + "=" + HttpUtility.UrlEncode( Rijndael.Encrypt( _code, ApplicationConfig.EncryptionKey ) );
                    _editButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/edit.jpg";
                }

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

                Literal _TableIDPerRoomLiteral = (Literal)e.Item.FindControl("TableIDPerRoomLiteral");
                _TableIDPerRoomLiteral.Text = HttpUtility.HtmlEncode(_temp.TableIDPerRoom.ToString());

                Literal _TableNumberLiteral = (Literal)e.Item.FindControl("TableNumberLiteral");
                _TableNumberLiteral.Text = HttpUtility.HtmlEncode(_temp.TableNmbr.ToString());

                Literal _StatusLiteral = (Literal)e.Item.FindControl("StatusLiteral");
                _StatusLiteral.Text = HttpUtility.HtmlEncode(_temp.Status.ToString());                
            }
        }

        protected void GenerateImageButton_Click(object sender, ImageClickEventArgs e)
        {
            _internetFloorBL.GenerateTable(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey) );
            ShowDataDetail1();
        }

        #endregion

    }
}