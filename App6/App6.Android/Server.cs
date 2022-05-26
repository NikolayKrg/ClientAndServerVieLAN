using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Java.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace App6.Droid
{
    public class Server
    {

        public void D()
        {
            try
            {
                Boolean end = false;
                ServerSocket ss = new ServerSocket(6764);
                while (!end)
                {
                    //Server is waiting for client here, if needed
                    Socket s = ss.Accept();
                    BufferedReader input = new BufferedReader(new InputStreamReader(s.OutputStream));
                    PrintWriter output = new PrintWriter(s.OutputStream, true); //Autoflush
                    String st = input.ReadLine();
                    //Log.d("Tcp Example", "From client: " + st);
                    //output.println("Good bye and thanks for all the fish :)");
                    s.Close();
                    //    if (STOPPING conditions){ end = true; }
                    //}
                    //ss.close();

                }
            }
            catch (UnknownHostException e)
            {
                // TODO Auto-generated catch block
                // e.printStackTrace();
            }
            catch (Java.IO.IOException e)
            {
                // TODO Auto-generated catch block
                //e.printStackTrace();
            }
        }
    }
}