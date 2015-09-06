//Copyrigths by StrelokQvt
//Created: 04.09.2015
//Descr: keybinder for options
//Don't call me strilok_avs :)

using System.Windows.Forms;

namespace SmartAC
{
    public partial class Form1 : Form
    {
        Keys use_keycode;
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F1:
                    textBox1.Text = "F1";
                    use_keycode = Keys.F6;
                    e.Handled = true;
                    break;
                case Keys.F2:
                    textBox1.Text = "F2";
                    use_keycode = Keys.F6;
                    e.Handled = true;
                    break;
                case Keys.F3:
                    textBox1.Text = "F3";
                    use_keycode = Keys.F6;
                    e.Handled = true;
                    break;
                case Keys.F4:
                    textBox1.Text = "F4";
                    use_keycode = Keys.F6;
                    e.Handled = true;
                    break;
                case Keys.F5:
                    textBox1.Text = "F5";
                    use_keycode = Keys.F6;
                    e.Handled = true;
                    break;
                case Keys.F6:
                    textBox1.Text = "F6";
                    use_keycode = Keys.F6;
                    e.Handled = true;
                    break;
                case Keys.Q:
                    textBox1.Text = "Q";
                    use_keycode = Keys.Q;
                    e.Handled = true;
                    break;
                case Keys.W:
                    textBox1.Text = "W";
                    use_keycode = Keys.W;
                    e.Handled = true;
                    break;
                case Keys.E:
                    textBox1.Text = "E";
                    use_keycode = Keys.E;
                    e.Handled = true;
                    break;
                case Keys.R:
                    textBox1.Text = "R";
                    use_keycode = Keys.R;
                    e.Handled = true;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == use_keycode && this.tabControl1.SelectedIndex == 0)
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