using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SMSLibrary;

namespace Windows_App
{
    public partial class LoginForm : Form
    {
        private dbConnector _dbconn;
        public LoginForm()
        {
            InitializeComponent();
            _dbconn = new dbConnector();
            _dbconn.OpenConnection(ApplicationConfig.SMSPortalConnectionString);
            GlobalVar.ConnString = ApplicationConfig.SMSPortalConnectionString;
            /*
            List<String> _organizationList = _dbconn.executeQuery ( "SELECT OrganizationName FROM MsOrganization" );
            foreach (String _rowOrganization in _organizationList)
                this.comboBox1.Items.Add(_rowOrganization);
            this.comboBox1.SelectedIndex = 0;
             */
            String [] _portList = System.IO.Ports.SerialPort.GetPortNames();
            foreach (String _singlePort in _portList) {
                this.comboBox1.Items.Add(_singlePort);
            }
            this.comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "" && this.textBox2.Text != "")
            {
                String _orgID = _dbconn.snatchQuery("OrganizationID", "MsOrganization", "OrganizationName='" + this.textBox3.Text + "'");
                if (Convert.ToInt16(_dbconn.snatchQuery("Count(*)", "MsUser", "OrganizationID=" + _orgID + " AND UserID='" + this.textBox1.Text + "' AND password='" + Rijndael.Encrypt ( this.textBox2.Text , ApplicationConfig.EncryptionKey ) + "'")) > 0)
                {
                    GlobalVar.OrgID = Convert.ToInt32(_orgID);
                    GlobalVar.UserID = this.textBox1.Text;
                    GlobalVar.FgAdmin = Convert.ToBoolean(_dbconn.snatchQuery("fgAdmin", "MsUser", "OrganizationID=" + _orgID + " AND UserID='" + this.textBox1.Text + "'"));
                    GlobalVar.PortName = this.comboBox1.SelectedItem.ToString();
                    SMSGatewayForm _smsGatewayForm = new SMSGatewayForm();
                    _smsGatewayForm.Text += " :: " + GlobalVar.PortName;
                    _smsGatewayForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Organization, Username and password did not match.");
                    this.textBox1.Text = "";
                    this.textBox2.Text = "";
                    this.textBox1.Select();
                }
            }
        }
    }
}
