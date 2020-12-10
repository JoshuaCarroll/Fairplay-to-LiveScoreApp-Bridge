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
        DateTime LastSentToServer;
        TimeSpan SendToServerInterval;
        DateTime LastConnectionAttempt;
        TimeSpan ReconnectAttemptInterval;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopulateComPortList();
            InitLocalObjects();
            ConnectToLiveScoreApp();
        }

        private void InitLocalObjects()
        {
            LastSentToServer = DateTime.Now.Subtract(new TimeSpan(1, 0, 0));
            SendToServerInterval = new TimeSpan(0, 0, 0, 1, 0);
            LastConnectionAttempt = DateTime.Now.Subtract(new TimeSpan(1, 0, 0));
            ReconnectAttemptInterval = new TimeSpan(0, 0, 0, 5, 0);
            ocrScoreboardClient = new TcpClient();
            data = new Fairplay.Mp70Rs232Data();
            scoreboardOCRData = new ScoreboardOCRData();
        }

        private void PopulateComPortList()
        {
            string[] ports = SerialPort.GetPortNames();
            ddlComPort.Items.AddRange(ports);
            if (ddlComPort.Items.Count > 0)
            {
                ddlComPort.SelectedIndex = 0;
            }
        }

        private void OpenComPort()
        {
            if (ddlComPort.SelectedItem != null)
            {
                serialPort1.PortName = ddlComPort.SelectedItem.ToString();
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                serialPort1.Open();
            }
        }

        private void ddlComPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (serialPort1.IsOpen)
            {
                serialPort1.Close();
            }
            OpenComPort();
        }

        private void ConnectToLiveScoreApp()
        {
            if (LastConnectionAttempt.Add(ReconnectAttemptInterval) < DateTime.Now)
            {
                try
                {
                    ocrScoreboardClient.Connect("10.0.0.42", 8080);
                    ocrScoreboardStream = ocrScoreboardClient.GetStream();
                }
                catch (System.Net.Sockets.SocketException)
                {
                    ocrScoreboardClient.Close();
                    ocrScoreboardClient = new TcpClient();
                    statusStrip1.Text = "Unable to connect to Live Score App. Retrying...";
                }
                catch (Exception)
                {
                    statusStrip1.Text = "Unable to connect to Live Score App. Retrying...";
                    // throw;
                }
                finally
                {
                    LastConnectionAttempt = DateTime.Now;
                }
            }
        }

        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e) {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            txtComRcvd.Invoke(new ComReceiverDelegate(ComReceiver), indata);
        }

        private delegate void ComReceiverDelegate(string i);

        private void ComReceiver(string input)
        {
            if (txtComRcvd.Text.Length > 2000) {
                txtComRcvd.Text = txtComRcvd.Text.Remove(0, 10); 
            }

            txtComRcvd.AppendText(input.Replace("\x0003", Environment.NewLine));

            data.Receive(input);

            if (LastSentToServer.Add(SendToServerInterval) < DateTime.Now)
            {
                SendDataToServer();

                LastSentToServer = DateTime.Now;
            }
        }

        private void SendDataToServer()
        {
            string json = scoreboardOCRData.Parse(data).ToJson();

            if (json != txtOutput.Text)
            {
                txtOutput.Text = json;
                byte[] jsonByteArr = Encoding.ASCII.GetBytes(json);

                try
                {
                    if (ocrScoreboardClient.Connected)
                    {
                        ocrScoreboardStream.Write(jsonByteArr, 0, jsonByteArr.Length);
                    }
                    else
                    {
                        ConnectToLiveScoreApp();
                    }
                }
                catch (System.IO.IOException)
                {
                    ConnectToLiveScoreApp();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
            ocrScoreboardClient.Close();
            Application.Exit();
        }
    }
}
