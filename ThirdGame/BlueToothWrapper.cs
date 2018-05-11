using System;
using Android.Bluetooth;
using Java.Util;
using Android.Util;
using System.Threading.Tasks;
using Android.Content;
using Android.App;
using System.IO;
using System.Text;

namespace ThirdGame
{
    public class BlueToothWrapper
    {
        public static readonly UUID MY_UUID_INSECURE =
            UUID.FromString("ff724081-fe5d-4fb2-8745-af149cc2c0de");
        public const string TAG = "BLUETOOTH";
        public const string APP_NAME = "AndroidCoder";
        public readonly Context context;
        public readonly BluetoothAdapter bluetoothAdapter;
        private AcceptThread insecureAcceptThread;
        private ConnectThread connectThread;
        private ConnectedThread ConnectedThread;
        public BluetoothDevice blueToothDevice;
        public UUID deviceUUID;
        private ProgressDialog progressDialog;

        public BlueToothWrapper(Context context)
        {
            this.context = context;
            this.bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
            insecureAcceptThread = new AcceptThread(this);
            connectThread = new ConnectThread(this);
            ConnectedThread = new ConnectedThread();
        }

        public void Connected(BluetoothSocket socket, BluetoothDevice remoteDevice)
        {
            ConnectedThread.Run(socket);
        }

        public void Start()
        {
            insecureAcceptThread.Run();
        }

        public void StartClient(BluetoothDevice device, UUID uuid)
        {
            connectThread.Run(device, uuid);
        }

        public void Write(string message)
        {
            ConnectedThread.Write(message);
        }
    }

    public class ConnectedThread
    {
        private BluetoothSocket socket;
        private Stream input;
        private Stream output;

        public void Run(BluetoothSocket socket)
        {
            this.socket = socket;
            try
            {

                this.input = socket.InputStream;
                this.output = socket.OutputStream;
            }
            catch (Exception ex)
            {
                //??????????????????????????
                Log.Error(BlueToothWrapper.TAG, ex.ToString());
            }


            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    try
                    {
                        using (var reader = new StreamReader(input))
                        {
                            string line = await reader.ReadToEndAsync();
                            //??????????????????
                        }
                    }
                    catch (Exception ex)
                    {
                        break;
                    }
                }
            });
        }

        public void Write(string message)
        {
            try
            {
                using (var writer = new StreamWriter(output))
                {
                    //await writer.WriteAsync(message);
                    writer.Write(message);
                }
            }
            catch (Exception ex)
            {
                Log.Error(BlueToothWrapper.TAG, ex.ToString());
            }
        }

        public void Cancel()
        {
            try
            {
                if (socket != null)
                    socket.Close();
            }
            catch (Exception ex)
            {
                Log.Error(BlueToothWrapper.TAG, ex.ToString());
            }
        }
    }

    public class ConnectThread
    {
        private readonly BlueToothWrapper parent;
        private BluetoothSocket socket;

        public ConnectThread(BlueToothWrapper parent)
        {
            this.parent = parent;
        }

        public void Run(BluetoothDevice device, UUID uuid)
        {
            TryToCloseSocket();
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    socket = parent.blueToothDevice.CreateInsecureRfcommSocketToServiceRecord(uuid);

                    parent.bluetoothAdapter.CancelDiscovery();

                    await socket.ConnectAsync();

                    Log.Debug(BlueToothWrapper.TAG, "ConnectThread: Connected!");

                    parent.Connected(socket, device);
                }
                catch (Exception ex)
                {
                    Log.Error(BlueToothWrapper.TAG, ex.ToString());

                    TryToCloseSocket();
                }
            });
        }

        public void Cancel()
        {
            TryToCloseSocket();
        }

        private void TryToCloseSocket()
        {
            try
            {
                if (socket != null)
                    socket.Close();
            }
            catch (Exception ex)
            {
                Log.Error(BlueToothWrapper.TAG, ex.ToString());
            }
        }
    }


    public class AcceptThread
    {
        private BluetoothServerSocket serverSocket;
        private readonly BlueToothWrapper parent;

        public AcceptThread(BlueToothWrapper parent)
        {
            this.parent = parent;
        }

        public void Run()
        {
            TryToCloseSocket();
            Task.Factory.StartNew(async () =>
            {
                try
                {
                    serverSocket = parent.bluetoothAdapter
                            .ListenUsingInsecureRfcommWithServiceRecord(BlueToothWrapper.APP_NAME, BlueToothWrapper.MY_UUID_INSECURE);

                    Log.Debug(BlueToothWrapper.TAG, "AcceptThread: setting up server!");

                    var socket = await serverSocket.AcceptAsync();

                    //assim mesmo que pega o device????????
                    parent.Connected(socket, socket.RemoteDevice);
                }
                catch (Exception ex)
                {
                    Log.Error(BlueToothWrapper.TAG, ex.ToString());
                    TryToCloseSocket();
                }
            });
        }

        public void Cancel()
        {
            TryToCloseSocket();
        }

        private void TryToCloseSocket()
        {
            try
            {
                if (serverSocket != null)
                    serverSocket.Close();
            }
            catch (Exception ex)
            {
                Log.Error(BlueToothWrapper.TAG, ex.ToString());
            }
        }
    }


}