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
    public partial class ConnStringGeneratorForm : Form
    {
        public ConnStringGeneratorForm()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.textBox2.Text = Rijndael.Encrypt(this.textBox1.Text, ApplicationConfig.EncryptionKey );
        }

    }
}
