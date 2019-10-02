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
using System.Net.Http;
using System.IO;

namespace smsapi
{
    public partial class Form1 : Form
    {
        public string phoneNumber;
        public string phoneId;
        public string sms;

        public Form1()
        {
            InitializeComponent();
        }

        private static readonly HttpClient client = new HttpClient();

        private void button1_Click(object sender, EventArgs e)
        {
            GetNumberAsync();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            getSmsAsync();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            endOfActivation();
        }

        private string response()
        {
            string url = @"http://sms-activate.ru/stubs/handler_api.php?api_key=YourApiKey&action=getNumber&service=" + "\"ig\"" + "&forward=$forward&operator=any&ref=$ref&country=0";
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            richTextBox1.AppendText(html);

            return html;
        }


        private async Task getSmsAsync()
        {
            string url = @"https://sms-online.pro/stubs/handler_api.php?api_key=YOURAPIKEY&action=getNumber&service=ig&ref=$ref&country=2";
            string html = string.Empty;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                html = reader.ReadToEnd();
            }
            richTextBox1.AppendText(html);
        }

        public string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        private async Task GetNumberAsync()
        {
            var values = new Dictionary<string, string>
            {
               { "api_key", "YOURAPIKEY" },
               { "action", "getNumber" },
               { "service", "ig" },
               { "forward", "0" },
               { "operator", "any" },
               { "ref", "" },
               { "country", "0" }
            };

            var content = new FormUrlEncodedContent(values);
            var response = await client.PostAsync("http://sms-activate.ru/stubs/handler_api.php", content);
            var responseString = await response.Content.ReadAsStringAsync();
            richTextBox1.AppendText(responseString);
            phoneNumber = Between(responseString + "XD", ":", "XD");
            phoneId = Between(responseString, "R:", ":");
            richTextBox1.AppendText(phoneNumber + ":");
            richTextBox1.AppendText(phoneId);
        }


    }
}
