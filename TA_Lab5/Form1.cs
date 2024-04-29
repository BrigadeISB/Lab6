using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing.Text;

namespace TA_Lab5
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Multiplication formM = new Multiplication();
            formM.Show();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Division formD = new Division();
            formD.Show();
        }
    }
}
