using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SMSLibrary;

namespace SMS.SMSWeb.Schedule
{
    public partial class ScheduleEdit : ScheduleBase
    {
        protected ScheduleBL _schedule = new ScheduleBL();
        protected SMSMessageBL _smsMessageBL = new SMSMessageBL();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            this.CekSession();

            this._nvcExtractor = new NameValueCollectionExtractor(Request.QueryString);

            this.ScheduleDateTextBox.Attributes.Add("ReadOnly", "true");
            for (int i = 0; i < 24; i++)
                this.HourDropDownList.Items.Add(new ListItem(i.ToString("00")));
            for (int i = 0; i < 60; i++)
                this.MinuteDropDownList.Items.Add(new ListItem(i.ToString("00")));
            if (!Page.IsPostBack)
            {
                ShowData();
            }
        }
        
        protected void ShowData()
        {
            TrSchedule _editData = _schedule.GetSingleSchedule(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
            this.ScheduleDateTextBox.Text = Convert.ToDateTime(_editData.ScheduleDate).ToString("yyyy-MM-dd");
            this.HourDropDownList.SelectedValue = Convert.ToDateTime(_editData.ScheduleDate).Hour.ToString("00");
            this.MinuteDropDownList.SelectedValue = Convert.ToDateTime(_editData.ScheduleDate).Minute.ToString("00");
            this.MessageTextBox.Text = _editData.Message;
            this.DestinationPhoneNoTextBox.Text = _editData.DestinationPhoneNumber.ToString();
        }
        
        protected void SaveButton_Click(object sender, ImageClickEventArgs e)
        {
            if (this.ScheduleDateTextBox.Text != "")
                if (this.MessageTextBox.Text != "")
                    if (this.DestinationPhoneNoTextBox.Text != "")
                    {
                        TrSchedule _editData = _schedule.GetSingleSchedule(Rijndael.Decrypt(this._nvcExtractor.GetValue(this._codeKey), ApplicationConfig.EncryptionKey));
                        SMSGatewaySend _editSendData = _smsMessageBL.GetSingleSMSGatewaySend(Convert.ToDateTime(_editData.ScheduleDate),
                            _editData.Message, _editData.DestinationPhoneNumber);
                        _editSendData.DateSent = _editData.ScheduleDate = Convert.ToDateTime(this.ScheduleDateTextBox.Text + " " + this.HourDropDownList.Text + ":" + this.MinuteDropDownList.Text);
                        _editSendData.Message = _editData.Message = this.MessageTextBox.Text;
                        _editSendData.DestinationPhoneNo = _editData.DestinationPhoneNumber = this.DestinationPhoneNoTextBox.Text;
                        
                        if (_schedule.EditSubmit())
                        {
                            _smsMessageBL.EditSubmit();
                            Response.Redirect(this._homePage + "?result=Success Update Data.");
                        }
                        else
                            this.WarningLabel.Text = "Failed to update data.";
                    }
                    else this.WarningLabel.Text = "Destination Phone Number must be filled.";
                else this.WarningLabel.Text = "Message must be filled.";
            else this.WarningLabel.Text = "Schedule Date must be filled.";
        }
        
        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(this._homePage);
        }
    }
}