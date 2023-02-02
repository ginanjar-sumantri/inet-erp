using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace SMS.SMSWeb.Message
{
    public partial class ContactsUpload : MessageBase
    {
        private SMSMessageBL _smsMessageBL = new SMSMessageBL();
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
                this.SubPageTitleLiteral.Text = "Contacts Upload";

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

                    String _contactName, _contactPhoneNumber, _contactCompany, _contactDateOfBirth, _contactReligion, _contactEmail, _contactCity, _contactGroup;
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
                        _contactName = myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _contactPhoneNumber = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _contactCompany = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _contactDateOfBirth = (myRange.Cells.Value2 == null) ? "" : DateTime.FromOADate(Convert.ToDouble(myRange.Cells.Value2)).ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _contactReligion = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _contactEmail = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _contactCity = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        _currX += 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                        _contactGroup = (myRange.Cells.Value2 == null) ? "" : myRange.Cells.Value2.ToString();

                        MsPhoneBook _newData = new MsPhoneBook();
                        _newData.OrganizationID = Convert.ToInt32(this._orgID);
                        _newData.UserID = this._userID;
                        _newData.Name = _contactName;
                        _newData.PhoneNumber = _contactPhoneNumber;
                        _newData.Company = _contactCompany;
                        if (_contactDateOfBirth == "")
                            _newData.DateOfBirth = null;
                        else
                            _newData.DateOfBirth = Convert.ToDateTime(_contactDateOfBirth);
                        _newData.Religion = _contactReligion;
                        _newData.Email = _contactEmail;
                        _newData.City = _contactCity;
                        _newData.PhoneBookGroup = _contactGroup;

                        _smsMessageBL.AddPhoneBook(_newData);

                        _currY += 1; _currX = 1;
                        myRange = (Excel.Range)xlWorkSheet.Cells[_currY, _currX];
                    }

                    xlWorkBook.Close(false, Type.Missing, Type.Missing);

                    Response.Redirect(this._contactsPage);
                }
                catch (Exception ex)
                {
                    WarningLabel.Text = "Upload status: The file could not be uploaded. The following error occured: " + ex.Message;
                }
            }

        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(_contactsPage);
        }

    }
}