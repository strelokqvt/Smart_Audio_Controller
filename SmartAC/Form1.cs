//Copyrigths by StrelokQvt
//Created: 27.08.2015
//Last upd:
//Based on NAudio and Google
//Don't call me strilok_avs :)

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio;
using NAudio.Wave;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System.Xml;
using System.Xml.Linq;
//qvt git

namespace SmartAC
{
    public partial class Form1 : Form
    {
        WaveIn waveIn;
        WaveFileWriter writer;
        string outputFilename = "demo.wav";
        bool ON = false;
        public Form1()
        {
            InitializeComponent();
        }
        //
        private void button1_Click(object sender, EventArgs e)
        {
            if (ON == false)
            {
                waveIn = new WaveIn();
                waveIn.DeviceNumber = 0;
                waveIn.DataAvailable += waveIn_DataAvailable;
                waveIn.RecordingStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(waveIn_RecordingStopped);
                waveIn.WaveFormat = new WaveFormat(16000, 1);
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                label2.Text = "Идет запись...";
                button1.Text = "Стоп";
                waveIn.StartRecording();
                ON = true;
                
            }
            else
            {
                waveIn.StopRecording();
                label2.Text = "";
                ON = false;
                button1.Text = "Запись";
                //button2_Click(this, EventArgs.Empty);
            }
        }
        void waveIn_DataAvailable(object sender, WaveInEventArgs e)
        {
            writer.WriteData(e.Buffer, 0, e.BytesRecorded);
        }


        void waveIn_RecordingStopped(object sender, EventArgs e)
        {
            waveIn.Dispose();
            waveIn = null;
            writer.Close();
            writer = null;
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Запись")
            {			
            WebRequest request = WebRequest.Create("https://www.google.com/speech-api/v2/recognize?output=json&lang=ru-RU&key=AIzaSyBOti4mM-6x9WDnZIjIeyEU21OpBXqWBgw");
            //
            request.Method = "POST";
            byte[] byteArray = File.ReadAllBytes(outputFilename);
            request.ContentType = "audio/l16; rate=16000"; //"16000";
            request.ContentLength = byteArray.Length;
            request.GetRequestStream().Write(byteArray, 0, byteArray.Length);



            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            //label1.Text = reader.ReadToEnd();
            string text = reader.ReadToEnd();
			run_proc( text ); 
            // Clean up the streams.
            reader.Close();
            response.Close();
            
            }
			else
			{
		     MessageBox.Show("Запись не завершена", "Внимание!", MessageBoxButtons.OK, 
			 MessageBoxIcon.Information);
			}
		}
		
        private void run_proc(string text)	
        {	
            Process proc = new Process();
            proc.StartInfo.FileName = ReadXML(text); //передаем записанный текст
            proc.StartInfo.Arguments = "";
            proc.Start();								
		}			

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult rsl = MessageBox.Show("Вы действительно хотите выйти из приложения?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rsl == DialogResult.Yes)
            { // выходим из приложения 
                Application.Exit();

            }
        }

        private string ReadXML(string text)
        {
            // Загружаем файл
            XDocument xDocument1 = new XDocument();
            xDocument1 = XDocument.Load("CfgFile.xml");
            string PathName = "notepad.exe";
            string block_count = xDocument1.Element("paths").Element("blocks_count").Value;
            int h = int.Parse(block_count);
            for (int i = 1; i < h+1; i++)
            {
                string xmlnum = i.ToString();
                if (text.Contains(xDocument1.Element("paths").Element("block_" + i + "_st").Element("keyword").Value))
                    {
                        PathName = xDocument1.Element("paths").Element("block_" + i + "_st").Element("Path").Value;
                        return PathName;
                        break; //yeah, release it!
                    }
            }
            return PathName;
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    textBox1.Text = "F1";
                    e.Handled = true;
                    break;
                case Keys.F2:
                    textBox1.Text = "F2";
                    e.Handled = true;
                    break;
                case Keys.F3:
                    textBox1.Text = "F3";
                    e.Handled = true;
                    break;
                case Keys.F4:
                    textBox1.Text = "F4";
                    e.Handled = true;
                    break;
                case Keys.F5:
                    textBox1.Text = "F5";
                    e.Handled = true;
                    break;
            }
        }

        private void get_site(string text)
        {
            string open_wild = "вилдберрис";
            string open_wild_2 = "Вилдберис";
            string open_vk = "ВК";
            string open_vk_2 = "ВКонтакте";
            if (text.Contains(open_wild) || text.Contains(open_wild_2))
            {
             System.Diagnostics.Process.Start("https://www.wildberries.ru");
            }
            if (text.Contains(open_vk) || text.Contains(open_vk_2))
            {
                System.Diagnostics.Process.Start("http://www.vk.com");
            }								
        }			

        private void button3_Click(object sender, EventArgs e)
        {
            WebRequest request = WebRequest.Create("https://www.google.com/speech-api/v2/recognize?output=json&lang=ru-RU&key=AIzaSyBOti4mM-6x9WDnZIjIeyEU21OpBXqWBgw");
            //
            request.Method = "POST";
            byte[] byteArray = File.ReadAllBytes(outputFilename);
            request.ContentType = "audio/l16; rate=16000"; //"16000";
            request.ContentLength = byteArray.Length;
            request.GetRequestStream().Write(byteArray, 0, byteArray.Length);



            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            StreamReader reader = new StreamReader(response.GetResponseStream());

            //label1.Text = reader.ReadToEnd();
            string text = reader.ReadToEnd();
			get_site( text ); 
            // Clean up the streams.
            reader.Close();
            response.Close();
		}

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
             button1_Click(null, null);
            }
            if (e.KeyCode == Keys.F2)
            {
                button2_Click(null, null);
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

        }
        }
    
}
