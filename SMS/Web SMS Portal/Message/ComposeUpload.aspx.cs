using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;


namespace SMS.SMSWeb.Message
{
    public partial class Message_ComposeUpload : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
        private LoginBL _loginBL = new LoginBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            this.CekSession();

            //if (Session["Organization"] != null)
            //{
            //    this._orgID = Session["Organization"].ToString();
            //    this._fgAdmin = Session["FgWebAdmin"].ToString();
            //    this._userID = Session["UserID"].ToString();
            //}

            if (!this.Page.IsPostBack == true)
            {
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



                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    List<SMSGatewaySend> _newDataBundle = new List<SMSGatewaySend>();
                    int _maskingCtr = 0;
                    String _category, _destinationPhoneNo, _maskingSetting, _messageSMS;
                    Int32 _currX, _currY;
                    Excel.Range myRange;

                    xlApp = new Excel.ApplicationClass();
                    xlWorkBook = xlApp.Workbooks.Open(Server.MapPath("../UploadFiles/") + filename, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing,
                        Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    _currX = _currY = 1;
                    myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                    while (myRange.Cells.Value2 != null)
                    {
                        _category = myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _destinationPhoneNo = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _maskingSetting = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _messageSMS = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        SMSGatewaySend _newData = new SMSGatewaySend();
                        _newData.Category = _category;
                        _newData.DestinationPhoneNo = _destinationPhoneNo;
                        _newData.Message = _messageSMS;
                        _newData.flagSend = 0;
                        _newData.userID = this._userID;
                        _newData.OrganizationID = Convert.ToInt32(this._orgID);
                        _newData.fgMasking = Convert.ToBoolean(_maskingSetting);
                        if (Convert.ToBoolean(_newData.fgMasking)) _maskingCtr++;

                        _newDataBundle.Add(_newData);

                        _currY += 1; _currX = 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                    }
                    xlWorkBook.Close(false, Type.Missing, Type.Missing);

                    Int32 _smsLimit = _smsMessageBL.GetSMSLimit(this._userID, Convert.ToInt32(this._orgID));
                    if (_smsLimit > _newDataBundle.Count)
                    {
                        if (_smsMessageBL.decreaseMaskingBalance(Convert.ToInt32(this._orgID), _maskingCtr))
                        {
                            _smsMessageBL.AddSMSGatewaySend(_newDataBundle);
                            _smsMessageBL.DecreaseSMSLimit(this._orgID, this._userID, _newDataBundle.Count);
                            Response.Redirect(this._outboxPage);
                        }
                        else
                        {
                            WarningLabel.Text = "Sending Mass SMS Failed because Your Masking Balance is not enough.";
                        }
                    }
                    else
                    {
                        WarningLabel.Text = "Sending Mass SMS Failed because you have reach your SMS Limit for today.";
                    }
                }
                catch (Exception ex)
                {
                    WarningLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }
        }
        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._composePage);
        }
    }
}