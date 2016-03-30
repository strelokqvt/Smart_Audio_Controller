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
using System.Media;
//qvt git

namespace SmartAC
{
    public partial class Form1 : Form
    {
        WaveIn waveIn;
        WaveFileWriter writer;
        string outputFilename = (Application.StartupPath + "\\command.wav");
        bool ON = false;
        public Form1()
        {
            InitializeComponent();
        }
        //
        SoundPlayer spSound;

        private void button1_Click(object sender, EventArgs e)
        {
            Image StopIcon = Image.FromFile(Application.StartupPath + "\\stop.png");
            Image StartIcon = Image.FromFile(Application.StartupPath + "\\start.png");	
            playStartRecord();
            if (ON == false)
            {
                waveIn = new WaveIn();
                waveIn.DeviceNumber = 0;
                waveIn.DataAvailable += waveIn_DataAvailable;
                waveIn.RecordingStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(waveIn_RecordingStopped);
                waveIn.WaveFormat = new WaveFormat(16000, 1);
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                pictureBox1.Image = StopIcon;
                waveIn.StartRecording();
                ON = true;
                
            }
            else
            {
                waveIn.StopRecording();
                playStopRecord();
                ON = false;
                pictureBox1.Image = StartIcon;
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
            if (ON == false)
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
            run_proc(text.ToLower()); 
            // Clean up the streams.
            reader.Close();
            response.Close();
            
            }
			else
			{
             recordNoFinish();
		     MessageBox.Show("Запись не завершена", "Внимание!", MessageBoxButtons.OK, 
			 MessageBoxIcon.Information);
			}
		}
		
        private void run_proc(string text)	
        {	
            Process proc = new Process();
			string proc_path = ReadXML(text); //передаем записанный текст
			if (proc_path != "NotFoundPath")
			{
            proc.StartInfo.FileName = proc_path; 
            proc.StartInfo.Arguments = "";
            proc.Start();							
			}
			else
			{
             playNoFoundCommand();
		     MessageBox.Show("Команда не распознана либо неверно задана", "Внимание!", MessageBoxButtons.OK, 
			 MessageBoxIcon.Information);
			}			 
		}			

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult rsl = MessageBox.Show("Вы действительно хотите выйти из приложения?", "Внимание!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rsl == DialogResult.Yes)
            { // выходим из приложения 
                Application.Exit();

            }
        }

        private void playStartRecord()
        {
            spSound = new SoundPlayer(Application.StartupPath + "\\start_rec.wav");
            spSound.Play();
        }

        private void playEmptyRequest()
        {
            spSound = new SoundPlayer(Application.StartupPath + "\\empty_request.wav");
            spSound.Play();
        }

        private void playCommandDone()
        {
            spSound = new SoundPlayer(Application.StartupPath + "\\command_done.wav");
            spSound.Play();
        }

        private void playWarnNoPath()
        {
            spSound = new SoundPlayer(Application.StartupPath + "\\warn_no_path.wav");
            spSound.Play();
        }

        private void playNoFoundCommand()
        {
            spSound = new SoundPlayer(Application.StartupPath + "\\nofound_command.wav");
            spSound.Play();
        }

        private void recordNoFinish()
        {
            spSound = new SoundPlayer(Application.StartupPath + "\\record_nofinish.wav");
            spSound.Play();
        }

        private void playStopRecord()
        {
            spSound = new SoundPlayer(Application.StartupPath + "\\stop_rec.wav");
            spSound.Play();
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

            string text = reader.ReadToEnd();
			string web_adress_name = get_site(text);
			if (web_adress_name != null)
			{
               System.Diagnostics.Process.Start(web_adress_name);
			}
			else
			{
               playNoFoundCommand();
		       MessageBox.Show("Команда не распознана либо неверно задана", "Внимание!", MessageBoxButtons.OK, 
			   MessageBoxIcon.Information);				
			}	
            reader.Close();
            response.Close();
		}

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            //image for tabcontrol pages
            string s = tabControl1.TabPages[e.Index].Text;
            if (s.Length > 20)
                s = s.Substring(0, 17) + "...";

            //Image newImage = Image.FromFile("tabpng.png");
            //Image newImage = Image.FromFile(Application.StartupPath + "\\tabpage.png");
            //Rectangle destRect = new Rectangle(e.Bounds.Left-12, e.Bounds.Top-5, e.Bounds.Width+15, 40);
           // e.Graphics.DrawImage(newImage, destRect);
            e.Graphics.DrawString(s, e.Font, Brushes.Black, new RectangleF(e.Bounds.Left + 35, e.Bounds.Top + 6, e.Bounds.Width, e.Bounds.Height));
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = "executable file (*.exe)|*.exe;";
            if (openFile.ShowDialog() == DialogResult.OK)
                textBox2.Text = openFile.FileName; //next for create command in XML
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string keywordValue = textBox3.Text;
            if (textBox2.Text.Length >= 4)
            {
                string pathValueCheck = textBox2.Text.Substring(textBox2.Text.Length - 4); // ".exe"  
                if (pathValueCheck == ".exe")
                {
                    if (textBox3.Text != "")
                    {
                        XmlDocument document = new XmlDocument();
                        document.Load(Application.StartupPath + "\\CfgFile.xml");
                        int oldLoadCount = loadXmlBlockCount();
                        int newLoadCount = oldLoadCount + 1;

                        XmlNode block_num = document.CreateElement("block_" + newLoadCount + "_st");
                        document.DocumentElement.AppendChild(block_num);
                        XmlNode keyword_element = document.CreateElement("keyword");
                        keyword_element.InnerText = keywordValue;
                        block_num.AppendChild(keyword_element);
                        XmlNode path_element = document.CreateElement("Path");
                        path_element.InnerText = textBox2.Text;
                        block_num.AppendChild(path_element);

                        document.Save(Application.StartupPath + "\\CfgFile.xml");
                        changeBlocksCount(newLoadCount);
                        playCommandDone();
                        MessageBox.Show("Команда успешно добавлена", "Внимание!", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Ключевое слово отсутствует", "Внимание!", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                }
                else
                {
                    playWarnNoPath();
                    MessageBox.Show("Строка не является путем к исполняемому файлу", "Внимание!", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }
            }
            else
            {
                playWarnNoPath();
                MessageBox.Show("Строка не является путем к исполняемому файлу", "Внимание!", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Image StopIcon = Image.FromFile(Application.StartupPath + "\\stop.png");
            Image StartIcon = Image.FromFile(Application.StartupPath + "\\start.png");	
            playStartRecord();
            if (ON == false)
            {
                waveIn = new WaveIn();
                waveIn.DeviceNumber = 0;
                waveIn.DataAvailable += waveIn_DataAvailable;
                waveIn.RecordingStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(waveIn_RecordingStopped);
                waveIn.WaveFormat = new WaveFormat(16000, 1);
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                pictureBox3.Image = StopIcon;
                waveIn.StartRecording();
                ON = true;

            }
            else
            {
                waveIn.StopRecording();
                playStopRecord();
                ON = false;
                pictureBox3.Image = StartIcon;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (ON == false)
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

                string text = reader.ReadToEnd();
                if (text.Contains("transcript"))
                {
                    text = text.Substring(text.IndexOf("\"transcript\":\"") + 14); //убираем все, до ключевого слова
                    string second_word = text.Remove(text.IndexOf('"'), text.Length - text.IndexOf('"')); //убираем все, после ключевого слова
                    label11.Text = "Ваш запрос:" + " " + second_word;
                    System.Diagnostics.Process.Start("https://ru.wikipedia.org/wiki/" + second_word);
                }
                else //(text.Contains("transcript")==false)
                {
                    playEmptyRequest();
                    MessageBox.Show("Задан пустой запрос", "Внимание!", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);	
                }
                reader.Close();
                response.Close();

            }
            if (ON == true)
            {
                recordNoFinish();
                MessageBox.Show("Запись не завершена", "Внимание!", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Image StopIcon = Image.FromFile(Application.StartupPath + "\\stop.png");
            Image StartIcon = Image.FromFile(Application.StartupPath + "\\start.png");	
            playStartRecord();
            if (ON == false)
            {
                waveIn = new WaveIn();
                waveIn.DeviceNumber = 0;
                waveIn.DataAvailable += waveIn_DataAvailable;
                waveIn.RecordingStopped += new EventHandler<NAudio.Wave.StoppedEventArgs>(waveIn_RecordingStopped);
                waveIn.WaveFormat = new WaveFormat(16000, 1);
                writer = new WaveFileWriter(outputFilename, waveIn.WaveFormat);
                pictureBox2.Image = StopIcon;
                waveIn.StartRecording();
                ON = true;

            }
            else
            {
                waveIn.StopRecording();
                playStopRecord();
                ON = false;
                pictureBox2.Image = StartIcon;
            }
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (ON == true)
                e.Cancel = true;
        }

        }
    //
}
