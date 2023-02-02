using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessRule.POSInterface;
using System.IO;
using InetGlobalIndo.ERP.MTJ.SystemConfig;
using InetGlobalIndo.ERP.MTJ.Common;
using InetGlobalIndo.ERP.MTJ.DBFactory.ERPManSys;


namespace InetGlobalIndo.ERP.MTJ.UI.POS.ManageTable
{
    public partial class ManageTablePosition : ManageTableBase
    {
        private BoxLayout _boxLayoutBL = new BoxLayout();
        protected BoxLayout _boxLayout;
        protected String _lebarPanelLayout,
            _tinggiPanelLayout,
            _posisiXPanelLayout,
            _posisiYPanelLayout,
            _gridSpace,
            _tinggiBox,
            _lebarBox,
            _boxCount;

        protected void ReadPosition(String _prmRoomCode)
        {
            this._boxLayout = new BoxLayout(_prmRoomCode);
            this.hiddenBoxPosition.Value = _boxLayout.StringPosition;
            this._boxCount = _boxLayout.BoxCount.ToString();
            this.hiddenBoxCount.Value = _boxCount;
        }

        protected void ComposeLayout(String _prmRoomCode)
        {
            this._boxLayout = new BoxLayout(_prmRoomCode);
            this._lebarPanelLayout = _boxLayout.LebarPanelLayout.ToString();
            this._tinggiPanelLayout = _boxLayout.TinggiPanelLayout.ToString();
            this._posisiXPanelLayout = _boxLayout.PosisiXPanelLayout.ToString();
            this._posisiYPanelLayout = _boxLayout.PosisiYPanelLayout.ToString();
            this._gridSpace = _boxLayout.GridSpace.ToString();
            this._tinggiBox = _boxLayout.TinggiBox.ToString();
            this._lebarBox = _boxLayout.LebarBox.ToString();
            this.sizeLabel.Text = _tinggiPanelLayout + " X " + _lebarPanelLayout;
            this.CSSLiteral.Text = "<style type='text/css'>" +
                ".divPanel {float:left;width:" + _lebarPanelLayout + "px;" +
                "height:" + _tinggiPanelLayout + "px;border:inset 5px #AAA;padding: 0px 0px 0px 0px;" +
                "margin: 0px 0px 0px 0px;}" +
                ".divBox  {position:absolute; height:" + _tinggiBox + "px;width:" + _lebarBox + "px;" +
                "background:#ABF;cursor:move;text-align:center;font-family:Arial Rounded MT Bold;" +
                "font-size:16px;border:solid 1px #CDE;}" +
                "</style>";
            String _imgpath = ApplicationConfig.POSImportVirDirPath + _boxLayout.BackgroundImage.ToString();
            this.Panel.Attributes.Add("style", "background:url(\"" + _imgpath.Replace(" ", "%20") + "\") no-repeat; background-position :center");
        }

        protected void Table()
        {
            this.CSSLiteral.Text = "<style type='text/css'>" +
            ".divPanel {float:left;background:#AAA;width:500px;" +
            "height:400px;border:inset 5px #AAA;padding: 0px 0px 0px 0px;" +
            "margin: 0px 0px 0px 0px;}";
        }

        protected void RenderLayout()
        {
            String _strDivBoxes = "", _jsPositionSetter = "<script language='javascript'>";
            String[] _splitedPosition = this.hiddenBoxPosition.Value.Split('|');

            for (int i = 1; i <= Convert.ToInt16(_boxCount); i++)
            {
                if (_splitedPosition.Length < i || this.hiddenBoxPosition.Value == "")
                    _strDivBoxes += "<div id=\"divBox" + i.ToString() + "\" class=\"divBox\">" + i.ToString() + "</div>";
                else
                {
                    String[] _splitedXY = _splitedPosition[i - 1].Split(',');
                    _strDivBoxes += "<div id=\"divBox" + i.ToString() + "\" class=\"divBox\" style=\"top:" + _splitedXY[1] + "px;left:" + _splitedXY[0] + "px\">" + i.ToString() + "</div>";
                    _jsPositionSetter += "posXBox[" + i + "] = " + _splitedXY[0] + ";\n";
                    _jsPositionSetter += "posYBox[" + i + "] = " + _splitedXY[1] + ";\n";
                }
            }

            _jsPositionSetter += "</script>\n";
            this.literalBoxes.Text = _strDivBoxes + _jsPositionSetter;
        }

        protected void Page_PreLoad(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //this.ReadPosition("internetfloor1");

                this.btnSaveLayout.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.btnDeleteLayout.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.btnAddBox.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";
                this.btnDecreaseBox.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/delete.jpg";
                this.Savebutton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/save.jpg";
                this.NewLayoutButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/add.jpg";

                this.btnAddBox.OnClientClick = this.hiddenBoxCount.ClientID + ".value = " + this.hiddenBoxCount.ClientID + ".value * 1 + 1 ;";
                this.btnDecreaseBox.OnClientClick = this.hiddenBoxCount.ClientID + ".value = " + this.hiddenBoxCount.ClientID + ".value * 1 - 1 ;";

                this.FloorDropDownList.DataSource = new BoxLayout().GetListBoxLayout();
                this.FloorDropDownList.DataTextField = this.FloorDropDownList.DataValueField = "roomCode";
                this.FloorDropDownList.DataBind();
                if (this.FloorDropDownList.SelectedValue != null)
                    if (this.FloorDropDownList.SelectedValue != "")
                        this.ReadPosition(this.FloorDropDownList.SelectedValue);
                    else
                        this.Table();
            }

            if (this.FloorDropDownList.SelectedValue != null)
                if (this.FloorDropDownList.SelectedValue != "")
                    this.ComposeLayout(this.FloorDropDownList.SelectedValue);

            _boxCount = this.hiddenBoxCount.Value;
            String[] _countChecker = this.hiddenBoxPosition.Value.Split('|');
            String _decreasedPosition = "";
            if (_countChecker.Length > Convert.ToInt16(_boxCount) && Convert.ToInt16(_boxCount) > 0)
            {
                for (int i = 0; i < Convert.ToInt16(_boxCount); i++)
                    _decreasedPosition += "|" + _countChecker[i];
                this.hiddenBoxPosition.Value = _decreasedPosition.Substring(1);
            }

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RenderLayout();
        }

        protected void btnSaveLayout_Click(object sender, EventArgs e)
        {
            _boxLayout = new BoxLayout();
            this._boxLayout.UpdatePositionRoom(this.FloorDropDownList.SelectedValue, this.hiddenBoxPosition.Value);
        }

        protected void FloorDropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ReadPosition(this.FloorDropDownList.SelectedValue);
            RenderLayout();
        }

        protected void btnDeleteLayout_Click(object sender, EventArgs e)
        {
            new BoxLayout().DeleteLayout(this.FloorDropDownList.SelectedValue);
            Response.Redirect(this._homePage);
        }
        protected void NewLayoutButton_Click(object sender, EventArgs e)
        {
            if (new BoxLayout().NewLayout(this.NewLayoutTextBox.Text, this.HeightDDL.SelectedValue, this.WidthDDL.SelectedValue))
            {
                this.FloorDropDownList.Items.Add(this.NewLayoutTextBox.Text);
                this.FloorDropDownList.SelectedValue = this.NewLayoutTextBox.Text;
                this.NewLayoutTextBox.Text = "";
                this.ReadPosition(this.FloorDropDownList.SelectedValue);
                this.ComposeLayout(this.FloorDropDownList.SelectedValue);
                RenderLayout();
            }
            else { Response.Redirect(this._homePage); }
        }

        protected void Savebutton_Click(object sender, EventArgs e)
        {
            if (this.FileUpload.PostedFile.FileName.Trim() != "" && this.FloorDropDownList.SelectedValue != "")
            {
                if (Directory.Exists("C:\\Inetpub\\wwwroot\\ERP Content")) //C:\\Inetpub\\wwwroot\\ERP Content\\POS\\Import Sketch
                {
                    if (Directory.Exists("C:\\Inetpub\\wwwroot\\ERP Content\\POS"))
                    {
                        if (Directory.Exists("C:\\Inetpub\\wwwroot\\ERP Content\\POS\\Import Sketch"))
                        {
                        }
                        else
                        {
                            Directory.CreateDirectory("C:\\Inetpub\\wwwroot\\ERP Content\\POS\\Import Sketch");
                        }
                    }
                    else
                    {
                        Directory.CreateDirectory("C:\\Inetpub\\wwwroot\\ERP Content\\POS");
                        Directory.CreateDirectory("C:\\Inetpub\\wwwroot\\ERP Content\\POS\\Import Sketch");
                    }
                }
                else
                {
                    Directory.CreateDirectory("C:\\Inetpub\\wwwroot\\ERP Content");
                    Directory.CreateDirectory("C:\\Inetpub\\wwwroot\\ERP Content\\POS");
                    Directory.CreateDirectory("C:\\Inetpub\\wwwroot\\ERP Content\\POS\\Import Sketch");
                }

                //FileInfo _fileInfo = new FileInfo(this.FileUpload.PostedFile.FileName);
                String _path = this.FileUpload.PostedFile.FileName;
                FileInfo _file = new FileInfo(_path);
                DateTime _datetime = DateTime.Now;
                String _imagepath = ApplicationConfig.POSImportPath + this.FloorDropDownList.SelectedValue + _datetime.Year.ToString() + _datetime.Month.ToString() + _datetime.Day.ToString() + _datetime.Hour.ToString() + _datetime.Minute.ToString() + _datetime.Second.ToString() + _datetime.Millisecond.ToString() + _file.Extension;

                if (PictureHandler.IsPictureFile(_path, ApplicationConfig.ImageExtension) == true)
                {
                    System.Drawing.Image _uploadedImage = System.Drawing.Image.FromStream(this.FileUpload.PostedFile.InputStream);

                    Decimal _width = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Width);
                    Decimal _height = Convert.ToDecimal(_uploadedImage.PhysicalDimension.Height);

                    if (Convert.ToDecimal(_width) > Convert.ToDecimal(_lebarPanelLayout) || Convert.ToDecimal(_height) > Convert.ToDecimal(_tinggiPanelLayout))
                    {
                        this.WarningLabel.Text = "This image is too big - please resize it!";
                    }
                    else
                    {
                        POSMsBoxLayout _msBoxLayout = _boxLayoutBL.GetSinglePOSMsBoxLayout(this.FloorDropDownList.SelectedValue);

                        if (File.Exists(ApplicationConfig.POSImportPath + _msBoxLayout.backgroundImage) == true)
                        {
                            File.Delete(ApplicationConfig.POSImportPath + _msBoxLayout.backgroundImage);
                        }

                        //_file.CopyTo(_imagepath, true);
                        this.FileUpload.PostedFile.SaveAs(_imagepath);

                        _msBoxLayout.backgroundImage = this.FloorDropDownList.SelectedValue + _datetime.Year.ToString() + _datetime.Month.ToString() + _datetime.Day.ToString() + _datetime.Hour.ToString() + _datetime.Minute.ToString() + _datetime.Second.ToString() + _datetime.Millisecond.ToString() + _file.Extension;

                        Boolean _save = _boxLayoutBL.SavePOSMsBoxLayout(_msBoxLayout);

                        _file.Refresh();
                        this.ComposeLayout(this.FloorDropDownList.SelectedValue);

                        this.WarningLabel.Text = "File uploaded successfully";
                    }
                }

            }
        }
    }
}