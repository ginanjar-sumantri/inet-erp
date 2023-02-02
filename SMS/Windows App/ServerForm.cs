using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Windows_App
{
    public partial class ServerForm : Form
    {
        public ServerForm()
        {
            InitializeComponent();
            StreamReader SR = File.OpenText ("ServerList.txt") ;
            String S;
            S = SR.ReadLine();
            while (S != null) {
                this.comboBox1.Items.Add(S);
                S = SR.ReadLine();
                S = SR.ReadLine();
            }
            SR.Close();
            this.comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            StreamReader SR = File.OpenText("ServerList.txt");
            String S;            
            GlobalVar.ConnString = "";
            S = SR.ReadLine();
            while (S != this.comboBox1.SelectedItem.ToString())
                S = SR.ReadLine();
            GlobalVar.ConnString = SR.ReadLine();
            SR.Close();
            dbConnector _dbConn = new dbConnector();
            if (!_dbConn.OpenConnection(GlobalVar.ConnString))
                this.label1.Text = "Failed to connect server.";
            else
            {
                LoginForm _loginForm = new LoginForm();
                _loginForm.Show();
                this.Hide();
            }
        }

    }
}
