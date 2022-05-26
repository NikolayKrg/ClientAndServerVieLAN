using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace App6.Droid
{
    public class Server3
    {
        private MainActivity _mainActivity;

        public Server3(MainActivity mainActivity)
        {
            _mainActivity = mainActivity;
        }
        public  async Task Listen()
        {
            string address = null;
            foreach (IPAddress adress in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                address = adress.ToString();

                // break;
            }
            HttpListener listener = new HttpListener();
            var str = $"http://{address}:8888/";
            listener.Prefixes.Add(str);
            listener.Start();
            Console.WriteLine("Ожидание подключений...");

            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
                HttpListenerRequest request = context.Request;
                HttpListenerResponse response = context.Response;


                string responseString = "<html><head><meta charset='utf8'></head><body>dgd</body></html>";
                string body2 = null;
                if (context.Request.HasEntityBody)
                {
                    using (System.IO.Stream body = request.InputStream) // here we have data
                    {
                        using (var reader = new System.IO.StreamReader(body, request.ContentEncoding))
                        {
                            body2= reader.ReadToEnd();
                            responseString = $"<html><head><meta charset='utf8'></head><body>{body2}</body></html>";
                        }
                    }
                }

                _mainActivity.RunOnUiThread(() => { Toast.MakeText(Application.Context, responseString, ToastLength.Long).Show(); });
                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
            }
        }
    }
}