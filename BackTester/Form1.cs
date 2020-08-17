using System;
using System.Drawing;
using System.Windows.Forms;

namespace BackTester
{
    public partial class Form1 : Form
    {
        GlobalKeyboardHook gHook;
        bool running = false;
        double win, loss = 0;
        public Form1()
        {
            InitializeComponent();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            gHook = new GlobalKeyboardHook();
            gHook.KeyDown += new KeyEventHandler(gHook_KeyDown);
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
                gHook.HookedKeys.Add(key);
            running = true;
            updateStatus();
        }

        public void gHook_KeyDown(object sender, KeyEventArgs e)
        {
            string input = ((char)e.KeyValue).ToString().ToUpper();

            if (input == Keys.Z.ToString())
            {
                win++;
                labelWin.Text = win.ToString();
            }
            else if (input == Keys.X.ToString())
            {
                loss++;
                labelLoss.Text = loss.ToString();
            }
            else if (input == Keys.Q.ToString())
            {
                win = loss = 0;
                labelWin.Text = "--";
                labelLoss.Text = "--";
            }
            else if (input == Keys.A.ToString())
            {
                if (win > 0)
                {
                    win--;
                    labelWin.Text = win.ToString();
                }
            }
            else if (input == Keys.S.ToString())
            {
                if (loss > 0)
                {
                    loss--;
                    labelLoss.Text = loss.ToString();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            running = !running;
            updateStatus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            gHook.unhook();
        }

        private void updateStatus()
        {
            if (running) {
                gHook.hook();
                pictureBox1.BackColor = Color.Green;
                button1.Text = "STOP";
            }
            else
            {
                gHook.unhook();
                pictureBox1.BackColor = Color.Red;
                button1.Text = "RUN";
            }
        }

    }
}
