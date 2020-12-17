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
        DateTime simulatorCountdownTo;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "";
            PopulateComPortList();
            InitLocalObjects();
            //ConnectToLiveScoreApp();
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
            else
            {
                simulatorTimer.Enabled = true;
                chkSimulator.Checked = true;
            }
        }

        private void simulatorTimer_Tick(object sender, EventArgs e)
        {
            if ((simulatorCountdownTo == null) || (simulatorCountdownTo < DateTime.Now))
            {
                simulatorCountdownTo = DateTime.Now.AddMinutes(15);
            }
            TimeSpan timeLeft = simulatorCountdownTo.Subtract(DateTime.Now);
            string clock = timeLeft.Minutes.ToString().PadLeft(2) + timeLeft.Seconds.ToString().PadLeft(2, '0') + " ";

            ComReceiver(string.Format("\x0002C{0}2  0\x0003", clock));
            ComReceiver("\x0002BHHOME      010 02011     \x0003");
        }

        private void OpenComPort()
        {
            if (ddlComPort.SelectedItem != null)
            {
                serialPort1.PortName = ddlComPort.SelectedItem.ToString();
                serialPort1.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);
                serialPort1.Open();
                chkReceiving.Checked = true;
                chkSimulator.Checked = false;
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

        private void btnConnect_Click(object sender, EventArgs e)
        {
            LastConnectionAttempt = DateTime.Now.Subtract(new TimeSpan(1, 0, 0));
            ConnectToLiveScoreApp();
        }

        private void ConnectToLiveScoreApp()
        {
            if (LastConnectionAttempt.Add(ReconnectAttemptInterval) < DateTime.Now)
            {
                try
                {
                    int port;
                    if (int.TryParse(txtPort.Text, out port))
                    {
                        ocrScoreboardClient.Connect(txtIpAddress.Text, port);
                        ocrScoreboardStream = ocrScoreboardClient.GetStream();
                        toolStripStatusLabel1.Text = "";
                        chkSending.Checked = true;
                    }
                    else
                    {
                        toolStripStatusLabel1.Text = "Port entered is not valid.";
                    }
                }
                catch (System.Net.Sockets.SocketException)
                {
                    ocrScoreboardClient.Close();
                    ocrScoreboardClient = new TcpClient();
                    toolStripStatusLabel1.Text = "Unable to connect to Live Score App. Check address.";
                }
                catch (Exception)
                {
                    toolStripStatusLabel1.Text = "Unable to connect to Live Score App. Check address.";
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
            txtComRcvd.BeginInvoke(new ComReceiverDelegate(ComReceiver), indata);
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
                    //else
                    //{
                    //    ConnectToLiveScoreApp();
                    //}
                }
                catch (System.IO.IOException)
                {
                    //ConnectToLiveScoreApp();
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            txtOutput.Text = "Closing..." + Environment.NewLine;

            simulatorTimer.Enabled = false;
            txtOutput.AppendText("Simulator stopped." + Environment.NewLine);

            serialPort1.DataReceived -= new SerialDataReceivedEventHandler(DataReceivedHandler);
            txtOutput.AppendText("Serial data recieved handler removed." + Environment.NewLine);

            serialPort1.Close();
            txtOutput.AppendText("Serial port closed." + Environment.NewLine);
            
            ocrScoreboardClient.Close();
            txtOutput.AppendText("TCP client stopped." + Environment.NewLine);
            
            Application.Exit();
        }
    }
}
