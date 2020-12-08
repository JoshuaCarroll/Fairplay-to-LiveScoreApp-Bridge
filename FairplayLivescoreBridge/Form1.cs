using System;
using System.IO.Ports;
using System.Net.Sockets;
using System.Text;
using System.Windows.Forms;

namespace FairplayLivescoreBridge
{
    public partial class Form1 : Form
    {
        Fairplay.Mp70Rs232Data data;
        ScoreboardOCRData scoreboardOCRData;
        TcpClient ocrScoreboardClient;
        NetworkStream ocrScoreboardStream;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            ocrScoreboardClient = new TcpClient("10.0.0.42", 8080); // Following validation on Port & IP
            ocrScoreboardStream = ocrScoreboardClient.GetStream();

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

            string json = scoreboardOCRData.Parse(data).ToJson(); ;
            txtOutput.Text = json;
            SendDataToServer(json);
        }

        private void SendDataToServer(String dataIn)
        {
            Byte[] StringToSend = Encoding.UTF8.GetBytes(dataIn);
            ocrScoreboardStream.Write(StringToSend, 0, StringToSend.Length);
        }

        private void write()
        {
            txtComRcvd.Text = "";
        }
        private void write(string label, string value)
        {
            txtComRcvd.Text += string.Format("{0}: {1}{2}",label,value, Environment.NewLine);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            Application.Exit();
        }
    }
}
