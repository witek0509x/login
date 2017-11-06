using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace md5_hash
{
    public partial class Form1 : Form
    {
        DB db = new DB("db.sqlite");
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string tekst = textBox1.Text;
            label1.Text = db.Compute(tekst);
        }
    }
}
