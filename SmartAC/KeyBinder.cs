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
        Keys play_keycode;
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
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

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.N:
                    textBox4.Text = "N";
                    play_keycode = Keys.N;
                    e.Handled = true;
                    break;
                case Keys.M:
                    textBox4.Text = "M";
                    play_keycode = Keys.M;
                    e.Handled = true;
                    break;
            }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == use_keycode)  && this.tabControl1.SelectedIndex == 0)
            {
             button1_Click(null, null);
            }
            if ((e.KeyCode == Keys.Space || e.KeyCode == play_keycode) && this.tabControl1.SelectedIndex == 0)
            {
                button2_Click(null, null);
            }
            if (e.KeyCode == Keys.F1 && this.tabControl1.SelectedIndex != 0)
            {
                tabControl1.SelectedIndex = 0;
            }
            if (e.KeyCode == Keys.F2 && this.tabControl1.SelectedIndex != 1)
            {
                tabControl1.SelectedIndex = 1;
            }
            if (e.KeyCode == Keys.F3 && this.tabControl1.SelectedIndex != 2)
            {
                tabControl1.SelectedIndex = 2;
            }
            if (e.KeyCode == Keys.F4 && this.tabControl1.SelectedIndex != 3)
            {
                tabControl1.SelectedIndex = 3;
            }

            if ((e.KeyCode == Keys.Enter) && this.tabControl1.SelectedIndex == 2)
            {
                button6_Click(null, null);
            }
            if ((e.KeyCode == Keys.Space) && this.tabControl1.SelectedIndex == 2)
            {
                button7_Click(null, null);
            }

            if ((e.KeyCode == Keys.Enter) && this.tabControl1.SelectedIndex == 3)
            {
                button8_Click(null, null);
            }
            if ((e.KeyCode == Keys.Space) && this.tabControl1.SelectedIndex == 3)
            {
                buttonRunCommand_Click(null, null);
            }
        }
    }
}