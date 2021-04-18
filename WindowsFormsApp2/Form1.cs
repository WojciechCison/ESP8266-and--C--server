using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {   
        //for reading button

        bool pom;
        public Form1()
        {
            InitializeComponent();
            dataGridView1.DataSource = SQLquerry.refreshview();
        }
        public void serverThread() //SERVER for ESP8266
        {


            int temp=0;
            int hum=0;
            char count='t';
            //setting up server
            UdpClient udpClient = new UdpClient(8080);
            IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 8080);
            while (pom)
            {

                Byte[] receiveBytes = udpClient.Receive(ref RemoteIpEndPoint);
                string returnData = Encoding.ASCII.GetString(receiveBytes);

                //reading and saving to database
               this.Invoke(new MethodInvoker(delegate ()
                  {
                      switch (count)
                      {
                          case 't':
                              temp = Int32.Parse(returnData);
                              count = 'h';
                              break;
                          case 'h':
                              hum = Int32.Parse(returnData);
                              count = 's';
                              break;
                          case 's':
                              SQLquerry.insertvalues(temp, hum);
                              dataGridView1.DataSource = SQLquerry.refreshview();
                              count = 't';
                              break;
                          default:
                              break;
                      }
                    

                  }));
               

            }
            udpClient.Close();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            this.ControlBox = false;
            Thread thdUDPServer = new Thread(new ThreadStart(serverThread));
            pom = true;

            thdUDPServer.Start();
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.ControlBox = true;
            pom = false;
        }
      
    }
}
