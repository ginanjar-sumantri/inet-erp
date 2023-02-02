using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;

namespace Windows_App
{
    public partial class tempHyperterminal : Form
    {
        private SerialPort _port;
        private AutoResetEvent receiveNow;

        public tempHyperterminal()
        {
            InitializeComponent();
            _port = new SerialPort();
            if (!_port.IsOpen)
            {
                receiveNow = new AutoResetEvent(false);
                //_port.PortName = "COM1";
                GlobalVar.PortName = "COM1";
                _port.PortName = GlobalVar.PortName;                 //COM1
                _port.BaudRate = 115200;                   //9600
                _port.DataBits = 8;                   //8
                _port.StopBits = StopBits.One;                  //1
                _port.Parity = Parity.None;                     //None
                _port.ReadTimeout = 300;             //300
                _port.WriteTimeout = 300;           //300
                _port.Encoding = Encoding.GetEncoding("iso-8859-1");
                _port.DataReceived += new SerialDataReceivedEventHandler(port_DataReceived);
                _port.Open();
                _port.DtrEnable = true;
                _port.RtsEnable = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.label1.Text = this.ExecCommand(this._port, this.textBox1.Text, 5000, "Failed to read the messages.");
        }


        private void port_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                if (e.EventType == SerialData.Chars)
                    receiveNow.Set();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private String ReadResponse(SerialPort port, int timeout)
        {
            string buffer = string.Empty;
            try
            {
                do
                {
                    if (receiveNow.WaitOne(timeout, false))
                    {
                        string t = port.ReadExisting();
                        buffer += t;
                    }
                    else
                    {
                        return "Error";
                    }
                    if (buffer.EndsWith("\r\nERROR\r\n"))
                        return "Error";
                }
                while (!buffer.EndsWith("\r\nOK\r\n") && !buffer.EndsWith("\r\n> ") && !buffer.EndsWith("\r\nERROR\r\n"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return buffer;
        }

        private String ExecCommand(SerialPort port, string command, int responseTimeout, string errorMessage)
        {
            try
            {
                port.DiscardOutBuffer();
                port.DiscardInBuffer();
                receiveNow.Reset();
                port.Write(command + "\r");

                String input = ReadResponse(port, responseTimeout);
                return input;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(errorMessage, ex);
            }
        }

        private void tempHyperterminal_FormClosed(object sender, FormClosedEventArgs e)
        {
            _port.Close();
        }
    }
}
