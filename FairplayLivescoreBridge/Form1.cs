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
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FairplayLivescoreBridge
{
    public partial class Form1 : Form
    {
        HeavyTask hvtask;
        Fairplay.Mp70Rs232Data data;
        ScoreboardOCRData scoreboardOCRData;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            data = new Fairplay.Mp70Rs232Data();
            scoreboardOCRData = new ScoreboardOCRData();
            serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
            serialPort1.Open();
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e) {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            txtComRcvd.Invoke(new ComReceiverDelegate(ComReceiver), indata);
        }
        private delegate void ComReceiverDelegate(string i);
        private void ComReceiver(string input)
        {
            data.Receive(input);
            write();
            write("Game clock", data.GameClock);
            write("Shot clock ",data.ShotClock);
            write("Game period", data.GamePeriod);
            write("Home team", data.Home.TeamName);
            write("Home score", data.Home.Score.ToString());
            write("Away team", data.Away.TeamName);
            write("Away score", data.Away.Score.ToString());

            txtOutput.Text = scoreboardOCRData.Parse(data).ToJson();
        }

        private void write()
        {
            txtComRcvd.Text = "";
        }
        private void write(string label, string value)
        {
            txtComRcvd.Text += string.Format("{0}: {1}{2}",label,value, Environment.NewLine);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            serialPort1.Close();
        }
    }
}
