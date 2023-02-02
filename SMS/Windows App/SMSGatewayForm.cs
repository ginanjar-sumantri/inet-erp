using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Windows_App
{
    public partial class SMSGatewayForm : Form
    {
        private dbConnector _dbconn;
        private SerialPortInterface _port;
        public String _kodeCekPulsa, _pesanBalasan;

        public SMSGatewayForm()
        {
            InitializeComponent();
            //this._kodeCekPulsa = "*888#";
            //this._pesanBalasan = "Terima Kasih";
            _dbconn = new dbConnector();
            _port = new SerialPortInterface(this);
            _dbconn.OpenConnection(GlobalVar.ConnString);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            timer1_junction();
            this.buttonStop.Enabled = true;
        }
        private void timer1_junction() {
            this.label1.Text = _port.cekSignal();
            Int16 signalStrength = Convert.ToInt16(Math.Round(Convert.ToDecimal(this.label1.Text.Replace(',', '.')))) ;
            if (signalStrength > 30) { signalStrength = 0; this.label1.Text = "No Signal"; }
            this.progressBar1.Value = signalStrength;
            if (signalStrength > 5) _dbconn.executeNonQuery("UPDATE MsOrganization SET GatewayStatus = 1 WHERE OrganizationID=" + GlobalVar.OrgID);
            this._port.PortingMessageToDB();
            //if (this._dbconn.cekExistingData("SMSGatewaySend", "flagSend", "0"))
            if (this._dbconn.snatchQuery("count(*)", "SMSGatewaySend", "flagSend = 0 AND COALESCE(DateSent,GetDate()) <= GetDate() AND OrganizationID=" + GlobalVar.OrgID) != "0")
                this._port.SendingPushedMessages();
            if (this._dbconn.snatchQuery("BalanceCheckRequest", "MsOrganization", "OrganizationID=" + GlobalVar.OrgID) == "True")
            {
                String _kodeCekPulsa = this._dbconn.snatchQuery("BalanceCheckCode", "MsOrganization", "OrganizationID=" + GlobalVar.OrgID);
                String _lastBalance = this._port.cekPulsa(_kodeCekPulsa);
                this._dbconn.executeNonQuery("UPDATE MsOrganization SET LastBalance='" + _lastBalance + "' WHERE OrganizationID=" + GlobalVar.OrgID );
            }
            ////cek jam 7 - 8
            //if (DateTime.Now.Hour >= 7 && DateTime.Now.Hour <= 8) { 
            //    List<String> _scheduleList = this._dbconn.executeQuery ( "SELECT id,Message,DestinationPhoneNumber FROM TrSchedule WHERE OrganizationID='" + GlobalVar.OrgID + "' AND ScheduleDate = '" + DateTime.Now.Date + "' AND fgQueuedSend = 0" );
            //    if ( _scheduleList.Count() > 0 )
            //        foreach (String _scheduleRow in _scheduleList) {
            //            String[] _fieldRow = _scheduleRow.Split('♀');
            //            this._dbconn.executeNonQuery("INSERT INTO SMSGatewaySend (Category,DestinationPhoneNo,Message,flagSend,userID,OrganizationID) " +
            //                " VALUES ('Secheduled','" + _fieldRow[2] + "', '" + _fieldRow[1] + "' , 0 , '" + GlobalVar.UserID + "', '" + GlobalVar.OrgID + "')");
            //            this._dbconn.executeNonQuery("UPDATE TrSchedule SET fgQueuedSend = 1 WHERE id=" + _fieldRow[0]);
            //        }
            //}

            //cek jam 6 - 7
            if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour <= 7) {
                List<String> _birthdayList = this._dbconn.executeQuery("SELECT id,BirthdayWishes,PhoneNumber FROM MsPhoneBook WHERE OrganizationID='" + GlobalVar.OrgID + "' AND DateOfBirth = '" + DateTime.Now.Date + "' AND lastWishesSent <> '" + DateTime.Now.Date + "'");
                if ( _birthdayList.Count() > 0 )
                    foreach (String _birthdayRow in _birthdayList) {
                        String[] _fieldRow = _birthdayRow.Split('♀');
                        this._dbconn.executeNonQuery("INSERT INTO SMSGatewaySend (Category,DestinationPhoneNo,Message,flagSend,userID,OrganizationID) " +
                            " VALUES ('Birthday','" + _fieldRow[2] + "', '" + _fieldRow[1] + "' , 0 , '" + GlobalVar.UserID + "', '" + GlobalVar.OrgID + "')");
                        this._dbconn.executeNonQuery("UPDATE MsPhoneBook SET lastWishesSent = '" + DateTime.Now.Date + "' WHERE id=" + _fieldRow[0]);
                    }

                String _lastNotice = this._dbconn.snatchQuery("GatewayStatusNoticeLastSent", "MsOrganization", "OrganizationID=" + GlobalVar.OrgID);
                if (_lastNotice != DateTime.Now.Date.ToString())
                {
                    String _noticePhoneNumber = this._dbconn.snatchQuery("GatewayStatusNoticeNumber", "MsOrganization", "OrganizationID=" + GlobalVar.OrgID);
                    if (_noticePhoneNumber != "") _port.sendMessage(_noticePhoneNumber, "Gateway Status : Active");
                    this._dbconn.executeNonQuery("UPDATE MsOrganization SET GatewayStatusNoticeLastSent='" + DateTime.Now.Date.ToString() + "' WHERE OrganizationID=" + GlobalVar.OrgID );
                }
            }

            this.timer1.Enabled = true;
        }

        private void buttonStart_Click(object sender, EventArgs e)
        {
            _port.OpenPort();
            this.timer1.Enabled = true;
            this.buttonStart.Enabled = false;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _port.ClosePort();
            this.timer1.Enabled = false;
            this.buttonStart.Enabled = true;
            this.buttonStop.Enabled = false;
            this.label1.Text = "Signal Strength";
            this.progressBar1.Value = 0;
            _dbconn.executeNonQuery("UPDATE MsOrganization SET GatewayStatus = 0 WHERE OrganizationID=" + GlobalVar.OrgID);
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void SMSGatewayForm_Resize(object sender, System.EventArgs e) {
            if (FormWindowState.Minimized == WindowState)
                this.Hide();
        }

    }
}
