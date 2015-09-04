//Copyrigths by StrelokQvt
//Created: 04.09.2015
//Descr: keybinder for options
//Don't call me strilok_avs :)

using System.Windows.Forms;

namespace SmartAC
{
    public partial class Form1 : Form
    {
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

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1 && this.tabControl1.SelectedIndex == 0)
            {
                button1_Click(null, null);
            }
            if (e.KeyCode == Keys.F2 && this.tabControl1.SelectedIndex == 0)
            {
                button2_Click(null, null);
            }
        }
    }
}