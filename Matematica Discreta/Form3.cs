
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Matematica_Discreta
{
    public partial class Form3 : Form
    {
        Form3 thisForm;
        public Form3()
        {
            InitializeComponent();
            thisForm = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            thisForm.Hide();
            Form1 form1 = new Form1(ref thisForm);
            form1.Visible = false;
            form1.ShowDialog();


        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Enabled = true;
        }
    }
}
