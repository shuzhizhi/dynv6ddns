using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Net.Sockets;
using System.Threading;


namespace dynv6ddns
{
    public partial class Form1 : Form
    {
        public static string ipv6 = "";
        public static int yunxing = 1;
        public static int xiumian = 600;
        public static DateTime y = DateTime.Now;
        public static string lujing = System.IO.Directory.GetCurrentDirectory() + "\\";
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)//软件加载时将txt文本加载入text文档
        {
           
            string[] lines = System.IO.File.ReadAllLines(lujing+"config.txt", Encoding.Default);
            /* for (int i = 0; i < lines.Length; i++)
             {
                 textBox4.Text = textBox4.Text + lines[i];
             }*/
            textBox1.Text = lines[0];
            textBox2.Text = lines[1];
            textBox3.Text = lines[2];
            textBox5.Text = lines[3];
            timer1.Start();
            timer1.Interval = 10000;
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            /*  string url = "https://ipv6.dynv6.com/api/update?hostname=" + textBox1.Text + "&token=" + textBox2.Text + "&ipv6=" + textBox3.Text;

              //    e.FromGroup.SendMessage(e.RobotQQ, url);

              WebResponse response;



              HttpWebRequest request = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
              try
              {
                  response = request.GetResponse();
              }
              catch (WebException ex)
              {
                  response = ex.Response;
              }
              // WebResponse response = request.GetResponse();
              StreamReader reader = new StreamReader(response.GetResponseStream());
              string y = reader.ReadToEnd();//a为转换网页的整个文档  
              textBox4.Text = y;*/
           // string[] lines = { textBox1.Text, textBox2.Text, textBox1.Text, textBox5.Text };//将数据保存至txt文档中
          

        }

        private void button4_Click(object sender, EventArgs e)
        {
            GetLocalIPV6();
            textBox3.Text = ipv6;
        }
        /// <summary>
        /// 取本机主机ip
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPV6()
        {
            try
            {

                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                    //从IP地址列表中筛选出IPv4类型的IP地址
                    //AddressFamily.InterNetwork表示此IP为IPv4,
                    //AddressFamily.InterNetworkV6表示此地址为IPv6类型
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetworkV6)
                    {

                        ipv6 = IpEntry.AddressList[i + 1].ToString();
                        return IpEntry.AddressList[i + 1].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            yunxing = 0;
            textBox7.Text = "结束运行";
            timer1.Stop();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            yunxing = 1;
            timer1.Start();

            string[] lines = { textBox1.Text, textBox2.Text, textBox3.Text, textBox5.Text };//将数据保存至txt文档中
            System.IO.File.WriteAllLines(lujing + "config.txt", lines);



        }


        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Interval = int.Parse(textBox5.Text) * 1000;
            if (y.AddSeconds(int.Parse(textBox5.Text)) > DateTime.Now)
            {
                Yx(sender, e);
                y = DateTime.Now;
            }
            /*  if (int.Parse(DateTime.Now.ToString())- int.Parse(y.ToString())> int.Parse(textBox5.Text)*1000)
              {
                  Yx(1,e); 
              }*/
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }
        public void Yx(object sender, EventArgs e)
        {

            if (yunxing == 1)
            {
                string url = "http://ipv6.dynv6.com/api/update?hostname=" + textBox1.Text + "&token=" + textBox2.Text + "&ipv6=" + GetLocalIPV6();

                //    e.FromGroup.SendMessage(e.RobotQQ, url);

                WebResponse response;



                HttpWebRequest request = (HttpWebRequest)WebRequest.CreateDefault(new Uri(url));
                try
                {
                    response = request.GetResponse();
                }
                catch (WebException ex)
                {
                    response = ex.Response;
                }
                // WebResponse response = request.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string y = reader.ReadToEnd();//a为转换网页的整个文档  
                textBox6.Text = DateTime.Now + y;


                xiumian = int.Parse(textBox5.Text);
                textBox3.Text = GetLocalIPV6();
                string[] lines = { textBox1.Text,textBox2.Text,textBox3.Text,textBox5.Text };//将数据保存至txt文档中
                System.IO.File.WriteAllLines(lujing + "config.txt", lines);
                if (xiumian != 0)
                {
                    textBox7.Text = "正在运行，设置的运行间隔为" + xiumian + "秒";

                }


            }
            else
            {
                yunxing = 0;
                textBox7.Text = "时间设置错误，已结束运行";
            }





        }

    }
}
