using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using SMSLibrary;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace SMS.SMSWeb.Schedule
{
    public partial class ScheduleUpload : ScheduleBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
        private ScheduleBL _scheduleBL = new ScheduleBL();
        private LoginBL _loginBL = new LoginBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CekSession();

            if (Session["FgWebAdmin"] != null)
                if (Session["FgWebAdmin"].ToString() == "False" && _loginBL.getPackageName(Session["Organization"].ToString(), Session["UserID"].ToString()) == "PERSONAL") Response.Redirect("../Message/Compose.aspx");

            //if (Session["Organization"] != null)
            //{
            //    this._orgID = Session["Organization"].ToString();
            //    this._fgAdmin = Session["FgWebAdmin"].ToString();
            //    this._userID = Session["UserID"].ToString();
            //}

            if (!this.Page.IsPostBack == true)
            {
                this.SubPageTitleLiteral.Text = "Schedule Upload";

                this.SaveButton.ImageUrl = "../images/save.jpg";
                this.CancelButton.ImageUrl = "../images/cancel.jpg";
                this.WarningLabel.Text = "";
            }
        }

        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (UploadFile.HasFile)
            {
                try
                {
                    string filename = Path.GetFileName(UploadFile.FileName);
                    if (!filename.Contains(".xls"))
                    {
                        this.WarningLabel.Text = "It wasn't a valid Excell file.";
                        return;
                    }
                    UploadFile.SaveAs(Server.MapPath("../UploadFiles/") + filename);
                    WarningLabel.Text = "Upload status: File uploaded!";

                    List<SMSGatewaySend> _newDataCollection = new List<SMSGatewaySend>();
                    List<TrSchedule> _newScheduleCollection = new List<TrSchedule>();

                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    String _dateSend, _phoneNumber, _messageSMS, _fgMasking;
                    Int32 _currX, _currY;
                    Excel.Range myRange;

                    xlApp = new Excel.ApplicationClass();
                    xlWorkBook = xlApp.Workbooks.Open(Server.MapPath("../UploadFiles/") + filename, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    int _maskingCtr = 0;

                    _currX = _currY = 1;
                    myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                    while (myRange.Cells.Value2 != null)
                    {
                        _dateSend = (myRange.Cells.Value2 == null) ? "" : DateTime.FromOADate(Convert.ToDouble(myRange.Cells.Value2)).ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _phoneNumber = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _messageSMS = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _fgMasking = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        SMSGatewaySend _newData = new SMSGatewaySend();
                        TrSchedule _newSchedule = new TrSchedule();
                        _newData.Category = "Schedule";
                        _newData.DestinationPhoneNo = _phoneNumber;
                        _newSchedule.DestinationPhoneNumber = _phoneNumber;
                        _newData.Message = _messageSMS;
                        _newSchedule.Message = _messageSMS;
                        _newData.flagSend = 0;
                        if (_dateSend == "")
                        {
                            _newData.DateSent = null;
                            _newSchedule.ScheduleDate = null;
                        }
                        else
                        {
                            _newData.DateSent = Convert.ToDateTime(_dateSend);
                            _newSchedule.ScheduleDate = Convert.ToDateTime(_dateSend);
                        }
                        _newData.OrganizationID = Convert.ToInt32(this._orgID);
                        _newSchedule.OrganizationID = Convert.ToInt32(this._orgID);
                        _newData.userID = this._userID;
                        _newSchedule.UserID = this._userID;
                        _newData.fgMasking = Convert.ToBoolean(_fgMasking);
                        if (Convert.ToBoolean(_fgMasking)) _maskingCtr++;
                        _newSchedule.fgQueuedSend = true;

                        _newDataCollection.Add(_newData);
                        _newScheduleCollection.Add(_newSchedule);
                        //_scheduleBL.AddSchedule(_newSchedule, _newData);

                        _currY += 1; _currX = 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                    }

                    if (_smsMessageBL.decreaseMaskingBalance(Convert.ToInt32(this._orgID), _maskingCtr))
                    {
                        _smsMessageBL.EditSubmit();
                        //_smsMessageBL.AddSMSGatewaySend(_newDataCollection);
                        for (int i = 0; i < _newDataCollection.Count; i++)
                        {
                            _scheduleBL.AddSchedule(_newScheduleCollection[i], _newDataCollection[i]);
                        }
                    }

                    xlWorkBook.Close(false, Type.Missing, Type.Missing);

                    Response.Redirect(this._homePage);
                }
                catch (Exception ex)
                {
                    WarningLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }

        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_homePage);
        }

    }
}