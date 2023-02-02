using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SMSLibrary;
using Microsoft.Win32;

namespace Windows_App
{
    public partial class SMSGateway : Form
    {
        private dbConnector _dbconn;
        private SerialPortInterface _port;

        public SMSGateway()
        {
            InitializeComponent();

            this._port = new SerialPortInterface();
            this._dbconn = new dbConnector();
            this._dbconn.OpenConnection(ApplicationConfig.SMSPortalConnectionString);
            GlobalVar.ConnString = ApplicationConfig.SMSPortalConnectionString;

            String[] _portList = System.IO.Ports.SerialPort.GetPortNames();
            foreach (String _singlePort in _portList)
            {
                this.PortComboBox.Items.Add(_singlePort);
            }
            this.PortComboBox.SelectedIndex = 0;
        }

        private void SMSGateway_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                this.Hide();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            WindowState = FormWindowState.Normal;
        }

        private void SMSGateway_FormClosed(object sender, FormClosedEventArgs e)
        {
            this._port.ClosePort();
            this.timer1.Enabled = false;
            this._dbconn.executeNonQuery("UPDATE MsOrganization SET GatewayStatus = 0 WHERE OrganizationID=" + GlobalVar.OrgID);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            this.timer1.Enabled = false;
            this.timer1_junction();
        }

        private void timer1_junction()
        {
            String _errMsg = this._dbconn.isConnected();

            if (_errMsg == "")
            {
                this.SignalStrengthLabel.Text = this._port.cekSignal().Replace(',', '.');
                //Int16 signalStrength = Convert.ToInt16(Math.Round(Convert.ToDecimal(this.SignalStrengthLabel.Text.Replace(',', '.'))));
                decimal _signal = 0;
                if (Decimal.TryParse(this.SignalStrengthLabel.Text, out _signal) == false)
                {
                    this.SignalStrengthLabel.Text = "Signal Unstable, Please Restart Application";
                }

                Int16 signalStrength = Convert.ToInt16(_signal);
                if (signalStrength > 30) { signalStrength = 0; this.SignalStrengthLabel.Text = "No Signal"; }
                this.SignalProgressBar.Value = signalStrength;
                if (signalStrength > 5)
                {
                    this._dbconn.executeNonQuery("UPDATE MsOrganization SET GatewayStatus = 1, SignalStrength = " + signalStrength.ToString() + " WHERE OrganizationID=" + GlobalVar.OrgID);
                    this._port.PortingMessageToDB();
                    if (this._dbconn.snatchQuery("count(*)", "SMSGatewaySend", "flagSend = 0 AND COALESCE(DateSent,GetDate()) <= GetDate() AND OrganizationID=" + GlobalVar.OrgID) != "0")
                    {
                        this._port.SendingPushedMessages();
                    }
                    if (this._dbconn.snatchQuery("BalanceCheckRequest", "MsOrganization", "OrganizationID=" + GlobalVar.OrgID) == "True")
                    {
                        String _kodeCekPulsa = this._dbconn.snatchQuery("BalanceCheckCode", "MsOrganization", "OrganizationID=" + GlobalVar.OrgID);
                        String _lastBalance = this._port.cekPulsa(_kodeCekPulsa);
                        this._dbconn.executeNonQuery("UPDATE MsOrganization SET LastBalance='" + _lastBalance.Replace("\'", "\''") + "' WHERE OrganizationID=" + GlobalVar.OrgID);
                    }

                    //cek jam 6 - 7
                    if (DateTime.Now.Hour >= 6 && DateTime.Now.Hour <= 7)
                    {
                        List<String> _birthdayList = this._dbconn.executeQuery("SELECT id,BirthdayWishes,PhoneNumber FROM MsPhoneBook WHERE OrganizationID='" + GlobalVar.OrgID + "' AND MONTH(DateOfBirth) = " + DateTime.Now.Month + " AND DAY(DateOfBirth) = " + DateTime.Now.Day + " AND lastWishesSent <> '" + DateTime.Now.Date + "'");
                        if (_birthdayList.Count() > 0)
                            foreach (String _birthdayRow in _birthdayList)
                            {
                                String[] _fieldRow = _birthdayRow.Split('♀');
                                this._dbconn.executeNonQuery("INSERT INTO SMSGatewaySend (Category,DestinationPhoneNo,Message,flagSend,userID,OrganizationID) " +
                                    " VALUES ('Birthday','" + _fieldRow[2] + "', '" + _fieldRow[1] + "' , 0 , '" + GlobalVar.UserID + "', '" + GlobalVar.OrgID + "')");
                                this._dbconn.executeNonQuery("UPDATE MsPhoneBook SET lastWishesSent = '" + DateTime.Now.Date + "' WHERE id=" + _fieldRow[0]);
                            }

                        String _lastNotice = this._dbconn.snatchQuery("GatewayStatusNoticeLastSent", "MsOrganization", "OrganizationID=" + GlobalVar.OrgID);
                        if (_lastNotice != DateTime.Now.Date.ToString())
                        {
                            String _noticePhoneNumber = this._dbconn.snatchQuery("GatewayStatusNoticeNumber", "MsOrganization", "OrganizationID=" + GlobalVar.OrgID);
                            if (_noticePhoneNumber != "") this._port.sendMessage(_noticePhoneNumber, this._dbconn.snatchQuery("OrganizationName", "MsOrganization", "OrganizationID=" + GlobalVar.OrgID) + "Gateway Status " + DateTime.Now.Date.ToShortDateString() + " : Active");
                            this._dbconn.executeNonQuery("UPDATE MsOrganization SET GatewayStatusNoticeLastSent='" + DateTime.Now.Date.ToString() + "' WHERE OrganizationID=" + GlobalVar.OrgID);
                        }
                    }
                }
            }
            else
            {
                this._dbconn.CloseConnection();
                //this.label5.Text = "Connection To Server Failed."; 
                this.SignalStrengthLabel.Text = _errMsg;
            }
            this.timer1.Enabled = true;
        }

        private void OrganizationTextBox_Leave(object sender, EventArgs e)
        {
            RegistryKey _currRegKey = Registry.CurrentUser;
            _currRegKey = _currRegKey.OpenSubKey("GatewayAppsSubKey", true);
            if (_currRegKey != null)
            {
                RegistryKey _regKey = _currRegKey.OpenSubKey(this.OrganizationTextBox.Text);
                if (_regKey != null)
                {
                    if (Convert.ToBoolean(_regKey.GetValue("RememberValue")))
                    {
                        this.RememberSettingCheckBox.Checked = true;
                        this.UserNameTextBox.Text = (string)_regKey.GetValue("UserNameValue");
                        this.PasswordTextBox.Text = (string)_regKey.GetValue("PasswordValue");
                        this.IntervalTextBox.Text = (string)_regKey.GetValue("IntervalValue");
                        //this.comboBox1.SelectedItem = (string)_regKey.GetValue("PortValue");
                        this.PortComboBox.Items.Insert(0, (string)_regKey.GetValue("PortValue"));
                    }
                    _regKey.Close();
                }
                _currRegKey.Close();
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            if (this.StartButton.Text == "Start")
            {
                if (this.IntervalTextBox.Text != "") this.timer1.Interval = Convert.ToInt32(this.IntervalTextBox.Text);
                if (this.UserNameTextBox.Text != "" && this.PasswordTextBox.Text != "")
                {
                    String _errMsg = this._dbconn.isConnected();
                    if (_errMsg == "")
                    {
                        String _orgID = this._dbconn.snatchQuery("OrganizationID", "MsOrganization", "OrganizationName='" + this.OrganizationTextBox.Text + "'");
                        if (Convert.ToInt16(this._dbconn.snatchQuery("Count(*)", "MsUser", "OrganizationID=" + _orgID + " AND UserID='" + this.UserNameTextBox.Text + "' AND password='" + Rijndael.Encrypt(this.PasswordTextBox.Text, ApplicationConfig.EncryptionKey) + "' AND fgAdmin=1")) > 0)
                        {
                            GlobalVar.OrgID = Convert.ToInt32(_orgID);
                            GlobalVar.UserID = this.UserNameTextBox.Text;
                            GlobalVar.FgAdmin = Convert.ToBoolean(this._dbconn.snatchQuery("fgAdmin", "MsUser", "OrganizationID=" + _orgID + " AND UserID='" + this.UserNameTextBox.Text + "'"));
                            GlobalVar.PortName = this.PortComboBox.SelectedItem.ToString();
                            this.Text += " :: Port " + GlobalVar.PortName;

                            RegistryKey _currRegKey = Registry.CurrentUser;
                            _currRegKey = _currRegKey.OpenSubKey("GatewayAppsSubKey", true);
                            if (_currRegKey == null)
                            {
                                _currRegKey = Registry.CurrentUser;
                                _currRegKey.CreateSubKey("GatewayAppsSubKey");
                                _currRegKey = _currRegKey.OpenSubKey("GatewayAppsSubKey", true);
                            }
                            RegistryKey _regKey = _currRegKey.OpenSubKey(this.OrganizationTextBox.Text, true);
                            if (_regKey == null)
                            {
                                _currRegKey.CreateSubKey(this.OrganizationTextBox.Text);
                                _regKey = _currRegKey.OpenSubKey(this.OrganizationTextBox.Text, true);
                            }
                            if (this.RememberSettingCheckBox.Checked)
                            {
                                //_currRegKey.SetValue("CorporateValue", this.textBox3.Text);
                                _regKey.SetValue("UserNameValue", this.UserNameTextBox.Text);
                                _regKey.SetValue("PasswordValue", this.PasswordTextBox.Text);
                                _regKey.SetValue("PortValue", this.PortComboBox.SelectedItem);
                                _regKey.SetValue("IntervalValue", this.IntervalTextBox.Text);
                                _regKey.SetValue("RememberValue", "True");
                            }
                            else
                            {
                                _regKey.SetValue("UserNameValue", "");
                                _regKey.SetValue("PasswordValue", "");
                                _regKey.SetValue("PortValue", "");
                                _regKey.SetValue("IntervalValue", "");
                                _regKey.SetValue("RememberValue", "False");
                            }
                            _regKey.Close();
                            _currRegKey.Close();

                            this.RememberSettingCheckBox.Enabled = this.UserNameTextBox.Enabled = this.PasswordTextBox.Enabled = this.OrganizationTextBox.Enabled = this.PortComboBox.Enabled = this.IntervalTextBox.Enabled = false;
                            this.StartButton.Text = "Stop";

                            this._port.OpenPort();
                            this.timer1.Enabled = true;
                        }
                        else
                        {
                            MessageBox.Show("Organization, Username and password did not match or not exist");
                            this.UserNameTextBox.Text = "";
                            this.PasswordTextBox.Text = "";
                            this.UserNameTextBox.Select();
                        }
                    }
                    else
                    {
                        //this.label5.Text = "Connection To Server Failed.";
                        this.SignalStrengthLabel.Text = _errMsg;
                    }
                }
            }
            else
            {
                this.timer1.Enabled = false;
                this._port.ClosePort();
                this._dbconn.executeNonQuery("UPDATE MsOrganization SET GatewayStatus = 0 WHERE OrganizationID=" + GlobalVar.OrgID);

                this.RememberSettingCheckBox.Enabled = this.UserNameTextBox.Enabled = this.PasswordTextBox.Enabled = this.OrganizationTextBox.Enabled = this.PortComboBox.Enabled = this.IntervalTextBox.Enabled = true;
                this.StartButton.Text = "Start";
                this.SignalProgressBar.Value = 0;
                this.SignalStrengthLabel.Text = "Signal Strength";
            }
        }
    }
}
