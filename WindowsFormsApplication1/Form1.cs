using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlbiReport.Utilities;

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
            panel4.Visible = false;
            panel5.Visible = false;
            panel1.Show();
            
        }

        private void Url_Click(object sender, EventArgs e)
        {
            string s = pie1.Text.ToString();
            pie7.Text = s;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string s = "Url(0_0)"+pie1.Text.ToString()+ "{0}Results?"+ "|Title(0_0)"+ pie2.Text.ToString() + "|SubTitle(0_0)"+pie3.Text.ToString() + "|PieMapSelectName(0_0)" + pie4.Text.ToString() + "|PieMapSelectValue(0_0)"+pie5.Text.ToString() + "|SeriesName(0_0)" + pie6.Text.ToString();
            pie7.Text = s;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Visible = false;
            panel1.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
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
            panel4.Visible = false;
            panel5.Visible = false;
            panel3.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel5.Visible = false;
            panel4.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string s = "Url(0_0)" + sel1.Text.ToString() + "|Label(0_0)" + sel2.Text.ToString() + "|TextField(0_0)" + sel3.Text.ToString() + "|ValueField(0_0)" + sel4.Text.ToString() + "|Width(0_0)" + sel5.Text.ToString() + "|Multiple(0_0)" + sel7.Text.ToString()+ "*";
            sel6.Text = s;
        }

      

        private void button9_Click(object sender, EventArgs e)
        {
            panel1.Visible = false;
            panel2.Visible = false;
            panel3.Visible = false;
            panel4.Visible = false;
            panel5.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            string s = z1.Text.ToString().Replace("\r\n","");
            string[] ss = s.Replace("/>", "|").Split('|');
            //< Property Name =\"A00O2TFHXIFF3PK5MID5C3J9PX\" Type=\"Edm.Decimal\" Precision=\"36\" Scale=\"34\" sap:aggregation-role=\"measure\" sap:creatable=\"false\" sap:filterable=\"false\" sap:label=\"月采购金额\" sap:text=\"A00O2TFHXIFF3PK5MID5C3J9PX_F\" sap:updatable=\"false\"
            List<oDatas> oDataslist = new List<oDatas>();
            foreach (var item in ss)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string a = item.Replace("=\"", "(0_0)").Replace("\"", "|").Replace("<", "").Replace(" ", "").Replace(":", "");
                    PostParams oPostParams = new PostParams(a);
                    oDatas oData = new oDatas();
                    CommonHelper.StringToEntityValue(oData, a);
                    if (!oData.PropertyName.Contains("_F"))
                    {
                        if (oDataslist.Where(o => o.saplabel == oData.saplabel).FirstOrDefault() == null)
                        {
                            oDataslist.Add(oData);
                        }
                    }
                    else
                    {
                        string sPropertyName = oData.PropertyName.Replace("_F", "");
                        var oDatas = oDataslist.Where(o => o.PropertyName == sPropertyName).FirstOrDefault();
                        if (oDatas == null)
                        {
                            oDataslist.Add(oData);
                        }
                        else
                        {
                            oDatas.PropertyName = oData.PropertyName;
                        }
                    }
                }
            }
            string res = "";
            foreach (var item in oDataslist)
            {
                res += "{ field: '" + item.PropertyName + "',   title: '" + item.saplabel + "', sortable: true, fixed: true, align: 'right' } ，";
            }

            z2.Text = res;
        }

        
    }

    public class oDatas
    {
        public string PropertyName { get; set; }

        public string saplabel { get; set; }
    }
}
