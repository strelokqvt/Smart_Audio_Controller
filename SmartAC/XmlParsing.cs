//Copyrigths by StrelokQvt
//Created: 05.09.2015
//Descr: xml-parser for database

using System.Windows.Forms;
using System.Xml.Linq;

namespace SmartAC
{
    public partial class Form1 : Form
    {
        private void changeBlocksCount(int count)
        {
            XDocument xDocument4 = new XDocument();
            xDocument4 = XDocument.Load(Application.StartupPath + "\\CfgFile.xml");
            xDocument4.Root.Element("blocks_count").Value = count.ToString();
            xDocument4.Save(Application.StartupPath + "\\CfgFile.xml");	
        }

        private int loadXmlBlockCount()
        {
            XDocument xDocument3 = new XDocument();
            xDocument3 = XDocument.Load(Application.StartupPath + "\\CfgFile.xml");
            string block_num = xDocument3.Element("paths").Element("blocks_count").Value;
            int block_count = int.Parse(block_num);
            return block_count;

        }

        private string ReadXML(string text)
        {
            // Загружаем файл
            XDocument xDocument1 = new XDocument();
            xDocument1 = XDocument.Load(Application.StartupPath + "\\CfgFile.xml");	
            string block_count = xDocument1.Element("paths").Element("blocks_count").Value;
            int h = int.Parse(block_count);
            for (int i = 1; i < h + 1; i++)
            {
                string xmlnum = i.ToString();
                if (text.Contains(xDocument1.Element("paths").Element("block_" + i + "_st").Element("keyword").Value.ToLower()))
                {
                    string PathName = xDocument1.Element("paths").Element("block_" + i + "_st").Element("Path").Value;
                    return PathName;
                    break; //yeah, release it!
                }
            }
            return "NotFoundPath";
        }

        private string get_site(string text)
        {
            // load xml for web-sites
            XDocument xDocument2 = new XDocument();
            xDocument2 = XDocument.Load("CfgWeb.xml");
            string block_num = xDocument2.Element("paths").Element("blocks_count").Value;
            int g = int.Parse(block_num);
            for (int i = 1; i < g + 1; i++)
            {
                //string xmlnum = i.ToString();
                if (text.Contains(xDocument2.Element("paths").Element("block_" + i + "_st").Element("keyword").Value))
                {
                    string WebAdress = xDocument2.Element("paths").Element("block_" + i + "_st").Element("Adress").Value;
                    return WebAdress;
                    break;
                }
            }
            return null;
        }
    }
}