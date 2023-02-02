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
using InetGlobalIndo.ERP.MTJ.UI.POS.Member;
using InetGlobalIndo.ERP.MTJ.BusinessRule;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Linq;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Drawing.Printing;
using InetGlobalIndo.ERP.MTJ.DBFactory.Membership;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Text;

namespace InetGlobalIndo.ERP.MTJ.UI.POS.Synchronize
{
    public partial class Synchronize : SynchronizeBase
    {
        private PermissionBL _permBL = new PermissionBL();
        private SynchronizeBL _synchronizeBL = new SynchronizeBL();
        private POSConfigurationBL _posConfigurationBL = new POSConfigurationBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this._permAccess = this._permBL.PermissionValidation1(this._menuID, HttpContext.Current.User.Identity.Name, InetGlobalIndo.ERP.MTJ.Common.Enum.Action.Access);

            if (this._permAccess == PermissionLevel.NoAccess)
            {
                Response.Redirect(this._errorPermissionPage);
            }

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            if (!this.Page.IsPostBack == true)
            {

                this.PageTitleLiteral.Text = this._pageTitleLiteral;
                this.SyncButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/sync.jpg";
                this.ViewButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/view.jpg";
                this.PrintLogButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/printlog.jpg";
                this.PrintManualButton.ImageUrl = ApplicationConfig.HomeWebAppURL + "images/printmansync.jpg";
                this.ShowServer();
                this.TimerRefresher.Enabled = false;
                this.StartDateTextBox.Attributes.Add("ReadOnly", "True");
                this.EndDateTextBox.Attributes.Add("ReadOnly", "True");
                this.StartDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
                this.EndDateTextBox.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
        }

        private void ClearLabel()
        {
            if (this.WarningLabel.Text != "Finished" | this.WarningLabel.Text != "Failed")
                this.WarningLabel.Text = "";
        }

        private void ClearLabelData()
        {
            this.MemberLabel.Text = "";
            this.ProductLabel.Text = "";
            this.SupplierLabel.Text = "";
            this.AirLineLabel.Text = "";
            this.HotelLabel.Text = "";
            this.ShippingLabel.Text = "";
        }

        public void ShowServer()
        {
            this.ServerSourceDDL.Items.Clear();
            this.ServerSourceDDL.DataTextField = "Server_Name";
            this.ServerSourceDDL.DataValueField = "Server_IP";
            this.ServerSourceDDL.DataSource = this._synchronizeBL.GetList(0, 1000, "", "");
            this.ServerSourceDDL.DataBind();
            this.ServerSourceDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));

            this.ServerDestinationDDL.Items.Clear();
            this.ServerDestinationDDL.DataTextField = "Server_Name";
            this.ServerDestinationDDL.DataValueField = "Server_IP";
            this.ServerDestinationDDL.DataSource = this._synchronizeBL.GetList(0, 1000, "", "");
            this.ServerDestinationDDL.DataBind();
            this.ServerDestinationDDL.Items.Insert(0, new ListItem("[Choose One]", "null"));
        }

        protected void SyncButton_Click(object sender, ImageClickEventArgs e)
        {
            bool _result = false;
            if (this.MemberCheckBox.Checked == false & this.ProductCheckBox.Checked == false & this.SupplierCheckBox.Checked == false & this.AirLineCheckBox.Checked == false & this.HotelCheckBox.Checked == false & this.ShippingCheckBox.Checked == false)
            {
                this.TimerRefresher.Enabled = false;
                if (this.WarningLabel.Text == "Finished" | this.WarningLabel.Text == "Failed")
                {
                    this.Print("Log");
                    this.Print("Manual");
                }
            }
            else
            {
                this.CheckValidData();
                if (this.WarningLabel.Text == "")
                {
                    this.ShowProgress();
                    this.TimerRefresher.Enabled = true;
                    this.TimerRefresher.Interval = 1;
                    if (this.MemberCheckBox.Checked == true)
                    {
                        _result = this._synchronizeBL.Synchronize(this.ServerSourceDDL.SelectedValue, this.ServerDestinationDDL.SelectedValue, this.MemberCheckBox.Text);
                        if (_result == true)
                            this.WarningLabel.Text = "Finished";
                        else
                            this.WarningLabel.Text = "Failed";
                        this.MemberCheckBox.Checked = false;
                    }
                    else if (this.ProductCheckBox.Checked == true)
                    {
                        _result = this._synchronizeBL.Synchronize(this.ServerSourceDDL.SelectedValue, this.ServerDestinationDDL.SelectedValue, this.ProductCheckBox.Text);
                        if (_result == true)
                            this.WarningLabel.Text = "Finished";
                        else
                            this.WarningLabel.Text = "Failed";
                        this.ProductCheckBox.Checked = false;
                    }
                    else if (this.SupplierCheckBox.Checked == true)
                    {
                        _result = this._synchronizeBL.Synchronize(this.ServerSourceDDL.SelectedValue, this.ServerDestinationDDL.SelectedValue, this.SupplierCheckBox.Text);
                        if (_result == true)
                            this.WarningLabel.Text = "Finished";
                        else
                            this.WarningLabel.Text = "Failed";
                        this.SupplierCheckBox.Checked = false;
                    }
                    else if (this.AirLineCheckBox.Checked == true)
                    {
                        _result = this._synchronizeBL.Synchronize(this.ServerSourceDDL.SelectedValue, this.ServerDestinationDDL.SelectedValue, this.AirLineCheckBox.Text);
                        if (_result == true)
                            this.WarningLabel.Text = "Finished";
                        else
                            this.WarningLabel.Text = "Failed";
                        this.AirLineCheckBox.Checked = false;
                    }
                    else if (this.HotelCheckBox.Checked == true)
                    {
                        _result = this._synchronizeBL.Synchronize(this.ServerSourceDDL.SelectedValue, this.ServerDestinationDDL.SelectedValue, this.HotelCheckBox.Text);
                        if (_result == true)
                            this.WarningLabel.Text = "Finished";
                        else
                            this.WarningLabel.Text = "Failed";
                        this.HotelCheckBox.Checked = false;
                    }
                    else if (this.ShippingCheckBox.Checked == true)
                    {
                        _result = this._synchronizeBL.Synchronize(this.ServerSourceDDL.SelectedValue, this.ServerDestinationDDL.SelectedValue, this.ShippingCheckBox.Text);
                        if (_result == true)
                            this.WarningLabel.Text = "Finished";
                        else
                            this.WarningLabel.Text = "Failed";
                        this.ShippingCheckBox.Checked = false;
                    }
                    this.ShowProgress();
                    this.ListRepeater.DataSource = this._synchronizeBL.GetListTransaction_SyncLog(DateTime.Now, DateTime.Now, this.ServerSourceDDL.SelectedValue,this.ServerDestinationDDL.SelectedValue);
                    this.ListRepeater.DataBind();

                    this.ListRepeaterManual.DataSource = this._synchronizeBL.GetListTransaction_SyncManual(DateTime.Now, DateTime.Now, this.ServerDestinationDDL.SelectedValue);
                    this.ListRepeaterManual.DataBind();
                }
            }
        }

        public void ShowProgress()
        {
            if (this.MemberCheckBox.Checked == true & this.MemberLabel.Text == "")
                this.MemberLabel.Text = "in Progress...";
            else if (this.MemberCheckBox.Checked == false & this.MemberLabel.Text == "in Progress...")
            {
                if (this.WarningLabel.Text == "Finished")
                    this.MemberLabel.Text = "Success...";
                else
                    this.MemberLabel.Text = "Failed";
            }

            if (this.ProductCheckBox.Checked == true & this.ProductLabel.Text == "")
                this.ProductLabel.Text = "in Progress...";
            else if (this.ProductCheckBox.Checked == false & this.ProductLabel.Text == "in Progress...")
            {
                if (this.WarningLabel.Text == "Finished")
                    this.ProductLabel.Text = "Success...";
                else
                    this.ProductLabel.Text = "Failed";
            }

            if (this.SupplierCheckBox.Checked == true & this.SupplierLabel.Text == "")
                this.SupplierLabel.Text = "in Progress...";
            else if (this.SupplierCheckBox.Checked == false & this.SupplierLabel.Text == "in Progress...")
            {
                if (this.WarningLabel.Text == "Finished")
                    this.SupplierLabel.Text = "Success...";
                else
                    this.SupplierLabel.Text = "Failed";
            }

            if (this.AirLineCheckBox.Checked == true & this.AirLineLabel.Text == "")
                this.AirLineLabel.Text = "in Progress...";
            else if (this.AirLineCheckBox.Checked == false & this.AirLineLabel.Text == "in Progress...")
            {
                if (this.WarningLabel.Text == "Finished")
                    this.AirLineLabel.Text = "Success...";
                else
                    this.AirLineLabel.Text = "Failed";
            }

            if (this.HotelCheckBox.Checked == true & this.HotelLabel.Text == "")
                this.HotelLabel.Text = "in Progress...";
            else if (this.HotelCheckBox.Checked == false & this.HotelLabel.Text == "in Progress...")
            {
                if (this.WarningLabel.Text == "Finished")
                    this.HotelLabel.Text = "Success...";
                else
                    this.HotelLabel.Text = "Failed";
            }

            if (this.ShippingCheckBox.Checked == true & this.ShippingLabel.Text == "")
                this.ShippingLabel.Text = "in Progress...";
            else if (this.ShippingCheckBox.Checked == false & this.ShippingLabel.Text == "in Progress...")
            {
                if (this.WarningLabel.Text == "Finished")
                    this.ShippingLabel.Text = "Success...";
                else
                    this.ShippingLabel.Text = "Failed";
            }
        }

        protected void ListRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Transaction_SyncLog _temp = (Transaction_SyncLog)e.Item.DataItem;

            Literal _dataLiteral = (Literal)e.Item.FindControl("DataLiteral");
            _dataLiteral.Text = HttpUtility.HtmlEncode(_temp.Sync_Table);

            Literal _startTimeLiteral = (Literal)e.Item.FindControl("StartTimeLiteral");
            _startTimeLiteral.Text = HttpUtility.HtmlEncode((_temp.Sync_StartDate).ToString());

            Literal _endTimeLiteral = (Literal)e.Item.FindControl("EndTimeLiteral");
            _endTimeLiteral.Text = HttpUtility.HtmlEncode((_temp.Sync_EndDate).ToString());

            Literal _statusLiteral = (Literal)e.Item.FindControl("StatusLiteral");
            _statusLiteral.Text = HttpUtility.HtmlEncode(_temp.Sync_Status);

            Literal _remarkLiteral = (Literal)e.Item.FindControl("RemarkLiteral");
            _remarkLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

            //HtmlTableRow _tableRow = (HtmlTableRow)e.Item.FindControl("RepeaterItemTemplate");
            //_tableRow.Attributes.Add("OnMouseOver", "this.style.backgroundColor='" + this._rowColorHover + "';");

            //if (e.Item.ItemType == ListItemType.Item)
            //{
            //    _tableRow.Attributes.Add("style", "background-color:" + this._rowColor);
            //    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColor + "';");
            //}

            //if (e.Item.ItemType == ListItemType.AlternatingItem)
            //{
            //    _tableRow.Attributes.Add("style", "background-color:" + this._rowColorAlternate);
            //    _tableRow.Attributes.Add("OnMouseOut", "this.style.backgroundColor='" + this._rowColorAlternate + "';");
            //}
        }

        protected void ListRepeaterManual_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Transaction_SyncManual _temp = (Transaction_SyncManual)e.Item.DataItem;

            Literal _relationLiteral = (Literal)e.Item.FindControl("RelationLiteral");
            _relationLiteral.Text = HttpUtility.HtmlEncode(_temp.Table_Relation);

            Literal _nameLiteral = (Literal)e.Item.FindControl("NameLiteral");
            _nameLiteral.Text = HttpUtility.HtmlEncode((_temp.Table_Name).ToString());

            Literal _valueLiteral = (Literal)e.Item.FindControl("ValueLiteral");
            _valueLiteral.Text = HttpUtility.HtmlEncode((_temp.Table_Value).ToString());

            Literal _insertDateLiteral = (Literal)e.Item.FindControl("InsertDateLiteral");
            _insertDateLiteral.Text = HttpUtility.HtmlEncode((_temp.Insert_Date).ToString());

            Literal _updateDateLiteral = (Literal)e.Item.FindControl("UpdateDateLiteral");
            _updateDateLiteral.Text = HttpUtility.HtmlEncode((_temp.Update_Date).ToString());

            Literal _statusLiteral = (Literal)e.Item.FindControl("StatusLiteral");
            _statusLiteral.Text = HttpUtility.HtmlEncode(_temp.Status.ToString());

            Literal _remarkLiteral = (Literal)e.Item.FindControl("RemarkLiteral");
            _remarkLiteral.Text = HttpUtility.HtmlEncode(_temp.Remark);

        }

        protected void ServerSourceDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabelData();
            if (this.ServerSourceDDL.SelectedValue != "null")
            {
                this.ServerSourceLocationTextBox.Text = this._synchronizeBL.GetSingle(this.ServerSourceDDL.SelectedValue).Server_Location;
                if (this._synchronizeBL.CheckHO(this.ServerSourceDDL.SelectedValue) == false)
                {
                    this.ServerDestinationDDL.SelectedValue = this._synchronizeBL.GetIPServerHO();
                    this.ServerDestinationDDL.Enabled = false;
                    this.ServerDestinationDDL_SelectedIndexChanged(null, null);

                    this.MemberCheckBox.Checked = true;

                    this.ProductCheckBox.Checked = false;
                    this.ProductCheckBox.Enabled = false;
                    this.SupplierCheckBox.Checked = false;
                    this.SupplierCheckBox.Enabled = false;
                    this.AirLineCheckBox.Checked = false;
                    this.AirLineCheckBox.Enabled = false;
                    this.HotelCheckBox.Checked = false;
                    this.HotelCheckBox.Enabled = false;
                    this.ShippingCheckBox.Checked = false;
                    this.ShippingCheckBox.Enabled = false;

                }
                else
                {
                    this.ServerDestinationDDL.Enabled = true;
                    this.MemberCheckBox.Checked = true;
                    this.ProductCheckBox.Enabled = true;
                    this.ProductCheckBox.Checked = true;
                    this.SupplierCheckBox.Enabled = true;
                    this.SupplierCheckBox.Checked = true;
                    this.AirLineCheckBox.Enabled = true;
                    this.AirLineCheckBox.Checked = true;
                    this.HotelCheckBox.Enabled = true;
                    this.HotelCheckBox.Checked = true;
                    this.ShippingCheckBox.Enabled = true;
                    this.ShippingCheckBox.Checked = true;
                }
            }
            else
            {
                this.ServerSourceLocationTextBox.Text = "";
            }
        }

        protected void CheckValidData()
        {
            this.ClearLabel();
            if (this.ServerSourceDDL.SelectedValue == this.ServerDestinationDDL.SelectedValue)
                this.WarningLabel.Text = " Process can run only in different Server.";
            if (this.ServerSourceDDL.SelectedValue == "null" | this.ServerDestinationDDL.SelectedValue == "null")
                this.WarningLabel.Text = this.WarningLabel.Text + " You must choose Server Source and Destination.";
            if (this.MemberCheckBox.Checked == false & this.ProductCheckBox.Checked == false & this.SupplierCheckBox.Checked == false & this.AirLineCheckBox.Checked == false & this.HotelCheckBox.Checked == false & this.ShippingCheckBox.Checked == false)
                this.WarningLabel.Text = this.WarningLabel.Text + " You must check Synchronize Data.";
            if (this._synchronizeBL.CheckServer(this.ServerDestinationDDL.SelectedValue) == false)
                this.WarningLabel.Text = this.WarningLabel.Text + " Unable to Connect Server.";
        }

        protected void ServerDestinationDDL_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearLabelData();
            if (this.ServerDestinationDDL.SelectedValue != "null")
                this.ServerDestinationLocationTextBox.Text = this._synchronizeBL.GetSingle(this.ServerDestinationDDL.SelectedValue).Server_Location;
            else
                this.ServerDestinationLocationTextBox.Text = "";
        }

        protected void TimerRefresher_Tick(object sender, EventArgs e)
        {
            this.SyncButton_Click(null, null);
        }

        private IList<Stream> m_streams;
        private int m_currentPageIndex;
        private bool _prmDO = false;

        private Stream CreateStream(string name, string fileNameExtension, Encoding encoding, string mimeType, bool willSeek)
        {
            Stream stream = null;
            try
            {
                stream = new MemoryStream();
                m_streams.Add(stream);
            }
            catch (Exception Ex)
            {
            }
            return stream;
        }

        private void PrintPage(object sender, PrintPageEventArgs ev)
        {
            Metafile pageImage = new
               Metafile(m_streams[m_currentPageIndex]);
            ev.Graphics.DrawImage(pageImage, ev.PageBounds);
            m_currentPageIndex++;
            ev.HasMorePages = (m_currentPageIndex < m_streams.Count);
        }

        protected void Print(String _prmType)
        {
            try
            {
                ReportDataSource _reportDataSource1 = new ReportDataSource();
                String _reportPath = "";
                LocalReport report = new LocalReport();
                String _serverSourceInstance = "";
                String _sourceLocation = "";
                String _serverDestinationInstance = "";
                String _destinationLocation = "";

                MsLinkServer _msLinkServer = this._synchronizeBL.GetSingle(this.ServerSourceDDL.SelectedValue);
                MsLinkServer _msLinkServer2 = this._synchronizeBL.GetSingle(this.ServerDestinationDDL.SelectedValue);
                String _database = _msLinkServer2.Server_Database;

                _sourceLocation = _msLinkServer.Server_Location;
                _destinationLocation = _msLinkServer2.Server_Location;
                        
                if (_prmType == "Log")
                {
                    _serverSourceInstance = _msLinkServer.Server_IP.Trim() + "\\" + _msLinkServer.Server_Instance.Trim();
                    _serverDestinationInstance = _msLinkServer2.Server_IP.Trim() + "\\" + _msLinkServer2.Server_Instance.Trim();
                    _reportPath = "Synchronize/ReportSyncLog.rdlc";
                }
                else
                {
                    _serverSourceInstance = "[" + _msLinkServer.Server_IP.Trim() + "\\" + _msLinkServer.Server_Instance.Trim() + "]";
                    _serverDestinationInstance = "[" + _msLinkServer2.Server_IP.Trim() + "\\" + _msLinkServer2.Server_Instance.Trim() + "]";
                    _reportPath = "Synchronize/ReportSyncManual.rdlc";
                }

                _reportDataSource1 = this._synchronizeBL.ReportSynchronize(_serverSourceInstance, _sourceLocation, _serverDestinationInstance, _destinationLocation, _database, Convert.ToDateTime(this.StartDateTextBox.Text), Convert.ToDateTime(this.EndDateTextBox.Text), _prmType);

                this.ReportViewer1.LocalReport.DataSources.Clear();
                this.ReportViewer1.LocalReport.DataSources.Add(_reportDataSource1);
                this.ReportViewer1.LocalReport.ReportPath = Request.ServerVariables["APPL_PHYSICAL_PATH"] + _reportPath;

                ReportParameter[] _reportParam = new ReportParameter[7];
                _reportParam[0] = new ReportParameter("serverSource", _serverSourceInstance, true);
                _reportParam[1] = new ReportParameter("sourceLocation", _sourceLocation, true);
                _reportParam[2] = new ReportParameter("serverDestination", _serverDestinationInstance, true);
                _reportParam[3] = new ReportParameter("destinationLocation", _destinationLocation, true);
                _reportParam[4] = new ReportParameter("database", _database, true);
                _reportParam[5] = new ReportParameter("startDate", this.StartDateTextBox.Text, true);
                _reportParam[6] = new ReportParameter("endDate", this.EndDateTextBox.Text, true);
                
                this.ReportViewer1.LocalReport.SetParameters(_reportParam);
                this.ReportViewer1.LocalReport.Refresh();

                report = this.ReportViewer1.LocalReport;

                try
                {
                    string deviceInfo =
                          "<DeviceInfo>" +
                          "  <OutputFormat>EMF</OutputFormat>" +
                          "  <PageWidth>4in</PageWidth>" +
                          "  <PageHeight>7.5in</PageHeight>" +
                          "  <MarginTop>0.0in</MarginTop>" +
                          "  <MarginLeft>0.0in</MarginLeft>" +
                          "  <MarginRight>0.0in</MarginRight>" +
                          "  <MarginBottom>0.0in</MarginBottom>" +
                          "</DeviceInfo>";

                    Warning[] warnings;
                    m_streams = new List<Stream>();

                    report.Render("Image", deviceInfo, CreateStream, out warnings);
                    foreach (Stream stream in m_streams)
                        stream.Position = 0;

                    m_currentPageIndex = 0;

                    String hostIP = Request.ServerVariables["REMOTE_ADDR"];
                    String printerName = "";

                    POSMsCashierPrinter _vPOSMsCashierPrinter = this._synchronizeBL.GetDefaultPrinter(hostIP);
                    if (_vPOSMsCashierPrinter != null)
                    {
                        printerName = "\\\\" + _vPOSMsCashierPrinter.IPAddress + "\\" + _vPOSMsCashierPrinter.PrinterName;
                    }

                    if (m_streams == null || m_streams.Count == 0) return;

                    PrintDocument printDoc = new PrintDocument();
                    printDoc.PrinterSettings.PrinterName = printerName;

                    PaperSize _paperSize = new PaperSize();
                    _paperSize.Width = 400;
                    _paperSize.Height = 750;

                    printDoc.DefaultPageSettings.PaperSize = _paperSize;

                    if (!printDoc.PrinterSettings.IsValid)
                    {
                        this.WarningLabel.Text = String.Format("Can't find printer \"{0}\".", printerName);
                        return;
                    }

                    printDoc.PrintPage += new PrintPageEventHandler(PrintPage);
                    printDoc.Print();

                    if (m_streams != null)
                    {
                        foreach (Stream stream in m_streams)
                            stream.Close();
                        m_streams = null;
                    }
                }
                catch (Exception ex)
                {
                    foreach (Stream stream in m_streams)
                        stream.Close();
                    m_streams = null;
                    this.WarningLabel.Text = "Sorry, Printer Not Include. ";
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void PrintLogButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Print("Log");
        }

        protected void PrintManualButton_Click(object sender, ImageClickEventArgs e)
        {
            this.Print("Manual");
        }

        protected void ViewButton_Click(object sender, ImageClickEventArgs e)
        {
            this.ListRepeater.DataSource = this._synchronizeBL.GetListTransaction_SyncLog(Convert.ToDateTime(this.StartDateTextBox.Text), Convert.ToDateTime(this.EndDateTextBox.Text), this.ServerSourceDDL.SelectedValue, this.ServerDestinationDDL.SelectedValue);
            this.ListRepeater.DataBind();

            this.ListRepeaterManual.DataSource = this._synchronizeBL.GetListTransaction_SyncManual(Convert.ToDateTime(this.StartDateTextBox.Text), Convert.ToDateTime(this.EndDateTextBox.Text), this.ServerDestinationDDL.SelectedValue);
            this.ListRepeaterManual.DataBind();
        }
}
}