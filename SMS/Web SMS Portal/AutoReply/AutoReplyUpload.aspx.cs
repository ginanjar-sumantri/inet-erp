using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace SMS.SMSWeb.Message
{
    public partial class AutoReplyUpload : AutoReplyBase
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
                this.SubPageTitleLiteral.Text = "Auto Reply Upload";

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
                    if (!filename.Contains(".xls")) {
                        this.WarningLabel.Text = "It wasn't a valid Excell file.";
                        return;
                    }
                    UploadFile.SaveAs(Server.MapPath("../UploadFiles/") + filename);
                    WarningLabel.Text = "Upload status: File uploaded!";


                    
                    Excel.Application xlApp;
                    Excel.Workbook xlWorkBook;
                    Excel.Worksheet xlWorkSheet;
                    object misValue = System.Reflection.Missing.Value;

                    String _autoReplyKeyWord, _autoReplyPhoneNumber, _autoReplyReplyMessage;
                    Int32 _currX, _currY;
                    Excel.Range myRange;

                    xlApp = new Excel.ApplicationClass();
                    xlWorkBook = xlApp.Workbooks.Open(Server.MapPath("../UploadFiles/") + filename,Type.Missing , 
                        Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,
                        Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing );
                    xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);

                    _currX = _currY = 1;
                    myRange = (Excel.Range)xlWorkSheet.Cells[_currY ,_currX];
                    while (myRange.Cells.Value2 != null)
                    {
                        _autoReplyKeyWord = myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _autoReplyPhoneNumber = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _autoReplyReplyMessage = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        if (_smsMessageBL.isAutoReplyExist(_autoReplyKeyWord, _autoReplyPhoneNumber))
                        {
                            TrAutoReply _updateData = _smsMessageBL.GetSingleTrAutoReply(_autoReplyKeyWord, _autoReplyPhoneNumber);
                            _updateData.ReplyMessage = _autoReplyReplyMessage;
                            _smsMessageBL.EditSubmit();
                        }
                        else
                        {
                            TrAutoReply _newData = new TrAutoReply();
                            _newData.OrganizationID = Convert.ToInt32(this._orgID);
                            _newData.KeyWord = _autoReplyKeyWord;
                            _newData.PhoneNumber = _autoReplyPhoneNumber;
                            _newData.ReplyMessage = _autoReplyReplyMessage;

                            _smsMessageBL.AddAutoReply(_newData);
                        }                        

                        _currY += 1; _currX = 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                    }

                    xlWorkBook.Close(false, Type.Missing, Type.Missing );

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