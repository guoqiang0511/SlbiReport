using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel1.Show();
            
        }

        private void Url_Click(object sender, EventArgs e)
        {
            string s = pie1.Text.ToString();
            pie6.Text = s;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string s = "Url(0_0)"+pie1.Text.ToString()+ "{0}Results?"+ "|Title(0_0)"+ pie2.Text.ToString() + "|SubTitle(0_0)"+pie3.Text.ToString() + "|PieMapSelectName(0_0)" + pie4.Text.ToString() + "|PieMapSelectValue(0_0)"+pie5.Text.ToString();
            pie6.Text = s;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = false;
            panel2.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string s = "Url(0_0)" + bar1.Text.ToString() + "{0}Results?" + "|Title(0_0)" + bar2.Text.ToString() + "|SubTitle(0_0)" + bar3.Text.ToString() + "|SeriesStr(0_0)" + bar4.Text.ToString() + "|AxisDataStr(0_0)" + bar5.Text.ToString() + "|LegendDataStr(0_0)" + bar6.Text.ToString();
            bar7.Text = s;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string s = "Url(0_0)" + tab1.Text.ToString() + "{0}Results?" + "|Title(0_0)" + tab2.Text.ToString() + "|FrozenColumns(0_0)" + tab3.Text.ToString() + "|Columns(0_0)" + tab4.Text.ToString();
            tab5.Text = s;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Show();
        }

    
    }
}
