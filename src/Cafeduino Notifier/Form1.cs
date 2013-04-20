using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace Cafeduino_Notifier
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (FormWindowState.Minimized == WindowState)
                Hide();
        }

        private void notifyIcon1_DoubleClick(object sender, EventArgs e)
        {
            Show();
            WindowState = FormWindowState.Normal;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // get the cafeduino values
            WebRequest req = WebRequest.Create("http://" + textBox1.Text + "/");
            WebResponse resp = req.GetResponse();

            string measures = "";
            using (Stream stream = resp.GetResponseStream() )
            {
                byte[] buffer = new byte[ resp.ContentLength];
                stream.Read(buffer, 0, (int)resp.ContentLength);
                stream.Close();

                measures = UTF8Encoding.UTF8.GetString(buffer);
            }

            if ( String.IsNullOrEmpty(measures) )
                return;

            label2.Text = measures;
        }
    }
}
