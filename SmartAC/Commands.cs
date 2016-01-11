//Copyrigths by StrelokQvt
//Created: 10.01.2016
//Descr: run system process

using System.Windows.Forms;
using System.Net;
using System.IO;
using System.IO.Compression;
using System.Diagnostics;
using System;

namespace SmartAC
{
    public partial class Form1 : Form
    {
        private void buttonRunCommand_Click(object sender, EventArgs e)
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

                string textOperation = reader.ReadToEnd();
                textOperation = textOperation.ToLower();

                if (textOperation.Contains("панель управления"))
                {
                    Process.Start("control");
                }
                else if (textOperation.Contains("звуковые устройства"))
                {
                    Process.Start("control.exe", "mmsys.cpl");
                }
                else if (textOperation.Contains("шрифты"))
                {
                    Process.Start("control.exe", "fonts");
                }
                else if (textOperation.Contains("спящий режим"))
                {
                    Process.Start("rundll32.exe", "powrprof.dll , SetSuspendState Sleep");
                    //System.Diagnostics.Process.Start("rundll32.exe",  "InetCpl.cpl,ClearMyTracksByProcess 255") //— полная очистка кэша браузера
                }
                else if (textOperation.Contains("дата и время"))
                {
                    Process.Start("timedate.cpl");
                }
                else if (textOperation.Contains("диспетчер задач"))
                {
                    Process.Start("taskmgr.exe");
                }
                else if (textOperation.Contains("командная строка"))
                {
                    Process.Start("cmd.exe");
                }
                else if (textOperation.Contains("свойства папки"))
                {
                    Process.Start("control.exe", "folders");
                }
                else if (textOperation.Contains("принтеры"))
                {
                    Process.Start("control.exe", "printers");
                }
                else if (textOperation.Contains("система"))
                {
                    Process.Start("control.exe", "system");
                }
                else if (textOperation.Contains("доступные обновления"))
                {
                    Process.Start("control.exe", "/Name Microsoft.WindowsUpdate /page pageCustomInstall");
                }
                else if (textOperation.Contains("журнал обновлений"))
                {
                    Process.Start("control.exe", "/Name Microsoft.WindowsUpdate /page pageUpdateHistory");
                }
                else if (textOperation.Contains("залипание клавиш"))
                {
                    Process.Start("control.exe", "/Name Microsoft.EaseOfAccessCenter /page pageStickyKeysSettings");
                }
                else if (textOperation.Contains("удаление программ"))
                {
                    Process.Start("control.exe", "/Name Microsoft.ProgramsAndFeatures");
                }
                else
                {
                    MessageBox.Show("Команда не распознана либо неверно задана", "Внимание!", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                }

                reader.Close();
                response.Close();

            }
            else
            {
                MessageBox.Show("Запись не завершена", "Внимание!", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }
    }
}