using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FairplayLivescoreBridge
{
    class HeavyTask
    {
        private bool keepListening = false;

        // Boolean that indicates wheter the process is running or has been stopped
        private bool HeavyProcessStopped;

        // Expose the SynchronizationContext on the entire class
        private readonly SynchronizationContext SyncContext;

        // Create the 2 Callbacks containers
        public event EventHandler<HeavyTaskResponse> Callback1;
        public event EventHandler<HeavyTaskResponse> Callback2;

        // Constructor of your heavy task
        public HeavyTask()
        {
            // Important to update the value of SyncContext in the constructor with
            // the SynchronizationContext of the AsyncOperationManager
            SyncContext = AsyncOperationManager.SynchronizationContext;
        }

        // Method to start the thread
        public void Start()
        {
            Thread thread = new Thread(Run);
            thread.IsBackground = true;
            thread.Start();
        }

        // Method to stop the thread
        public void Stop()
        {
            keepListening = false;
            HeavyProcessStopped = true;
        }

        // Method where the main logic of your heavy task happens
        private void Run()
        {
            string strOutput = string.Empty;
            IPAddress localAddress = IPAddress.Any;
            TcpListener listener = new TcpListener(localAddress, 8080);
            keepListening = true;

            listener.Start();

            using (Socket socketForClient = listener.AcceptSocket())
            {
                if (socketForClient.Connected)
                {
                    Console.WriteLine("Client:" + socketForClient.RemoteEndPoint + " now connected to server.");

                    using (NetworkStream networkStream = new NetworkStream(socketForClient))
                    using (System.IO.StreamReader streamReader = new System.IO.StreamReader(networkStream))
                    {
                        try
                        {
                            while (keepListening)
                            {
                                string theString = streamReader.ReadLine();
                                if (string.IsNullOrEmpty(theString) == false)
                                {
                                    SyncContext.Post(e => triggerCallback1(
                                        new HeavyTaskResponse(theString)
                                    ), null);
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("** ERROR: " + ex.Message);
                        }
                    }


                }
            }
        }

        private void SendMessage(string message)
        {
            SyncContext.Post(e => triggerCallback1(
                new HeavyTaskResponse(message)
            ), null);

            Console.WriteLine(message);
        }

        // Methods that executes the callbacks only if they were set during the instantiation of
        // the HeavyTask class !
        private void triggerCallback1(HeavyTaskResponse response)
        {
            // If the callback 1 was set use it and send the given data (HeavyTaskResponse)
            Callback1?.Invoke(this, response);
        }

        private void triggerCallback2(HeavyTaskResponse response)
        {
            // If the callback 2 was set use it and send the given data (HeavyTaskResponse)
            Callback2?.Invoke(this, response);
        }
    }
}
