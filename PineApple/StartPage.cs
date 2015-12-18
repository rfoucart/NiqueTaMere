using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PineApple
{
    public partial class StartPage : Form
    {
        public string name{get; private set;}

        public DateTime start { get; private set; }

        public StartPage()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {

        }

        private void StartPage_Load(object sender, EventArgs e)
        {
           

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                name = textBox1.Text;
                start = DateTime.Now;
                this.Close();
            }
            catch { MessageBox.Show("Bad Idea"); }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
