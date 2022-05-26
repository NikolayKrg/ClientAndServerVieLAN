using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace App6.Droid
{
    public class Server2
    {
        private MainActivity _mainActivity;

        public Server2(MainActivity mainActivity)
        {
            _mainActivity = mainActivity;
        }
        public async void D()
        {
            await Task.Run(()=> { 
            string address= null;
            foreach (IPAddress adress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                address= adress.ToString();
               
               // break;
            }

            IPAddress localAddr = IPAddress.Parse(address);
            int port = 8888;
            TcpListener tcpListener = new TcpListener(localAddr,port);
            while (true)
            {
                
                tcpListener.Start();
                //Program blocks on Accept() until a client connects.
                Socket soTcp = tcpListener.AcceptSocket();
                Console.WriteLine("SampleClient is connected through TCP.");
                Byte[] received = new Byte[1024];
                int bytesReceived = soTcp.Receive(received, received.Length, 0);
                String dataReceived = System.Text.Encoding.ASCII.GetString(received);
                Console.WriteLine(dataReceived);
                String returningString = "The Server got your message through TCP: " + dataReceived;
                Byte[] returningByte = System.Text.Encoding.ASCII.GetBytes(returningString.ToCharArray());
                //Returning a confirmation string back to the client.
                soTcp.Send(returningByte, returningByte.Length, 0);

                    _mainActivity.RunOnUiThread(()=> { Toast.MakeText(Application.Context, returningString, ToastLength.Long).Show(); });
                    
                    
                tcpListener.Stop();
            }

            });
        }



        //class IPAddressManager 
        //{
        //    public string GetIPAddress()
        //    {
        //        try
        //        {
        //            List<string> IpAddress = new List<string>();
        //            var Hosts = Networking.Connectivity.NetworkInformation.GetHostNames().ToList();
        //            foreach (var Host in Hosts)
        //            {
        //                string IP = Host.DisplayName;
        //                IpAddress.Add(IP);
        //            }

        //            IPAddress address = IPAddress.Parse(IpAddress.Last());

        //            return address.ToString();
        //        }
        //        catch (Exception ex)
        //        {
        //            return null;
        //        }
        //    }
        //}
    }
}