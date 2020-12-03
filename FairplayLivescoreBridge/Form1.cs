using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FairplayLivescoreBridge
{
    public partial class Form1 : Form
    {
        private TcpListener listener;
        bool keepListening = false;
        HeavyTask hvtask;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            serialPort1.Open();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e) {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            txtComRcvd.Text = indata;

            //callmyfunction(indata);
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            keepListening = true;

            hvtask = new HeavyTask();
            hvtask.Callback1 += CallbackChangeFirstButton;
            hvtask.Start();
        }

        private void CallbackChangeFirstButton(object sender, HeavyTaskResponse response)
        {
            Receive(response.Message);
        }

        private void Receive(string strReceived, bool newLine = true)
        {
            if ((newLine) && (!strReceived.EndsWith(Environment.NewLine)))
            {
                strReceived += Environment.NewLine;
            }

            Console.WriteLine(strReceived);
            txtResponse.Text += strReceived;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            keepListening = false;
            hvtask.Stop();
        }
    }
}
