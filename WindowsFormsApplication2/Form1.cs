using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.CheckedListBox;
using static System.Windows.Forms.ListBox;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public List<TabP> dimensionlist;
        public List<TabP> measurelist;
        public List<TreeNode> TreeNodeList;
        public List<string> Ctreeslist;
        public Form1()
        {
            InitializeComponent();
            dimensionlist = new List<TabP>();
            measurelist = new List<TabP>();
            panel2.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel2.Show();
            Sel.Visible = false;
            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();
            treeView1.Nodes.Clear();
            dimensionlist = new List<TabP>();
            measurelist = new List<TabP>();
            TreeNodeList = new List<TreeNode>();
            string sUrl = "http://bwdev.shuanglin.com:8000/sap/opu/odata/sap/"+ url.Text + "_SRV/$metadata?sap-user=guoq&sap-password=ghg2587758";

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(sUrl);
            }
            catch
            {
                //return null;
            }
            ds = ConvertXMLFileToDataSet(doc);
            dt = ds.Tables["Property"];
            treeView1.Nodes.Add(new TreeNode("Table"));
            //treeView1.Nodes.Add(new TreeNode("measure"));
            TreeNode dimension = treeView1.Nodes[0];
            //TreeNode measure = treeView1.Nodes[1];
            DataRow[] drArr = dt.Select("EntityType_Id=0 and  text <> '' ");//查询
            foreach (var dr in drArr)
            {
                string sssssss = Convert.ToString(dr["text"]) + ":" + Convert.ToString(dr["label"]);
                if (Convert.ToString(dr["aggregation-role"]) == "dimension")
                {
                    //dimension.Nodes.Add(sssssss);
                    //dimensionlist.Add(
                    //    new TabP()
                    //    {
                    //        sNameValue = sssssss,
                    //        sName = Convert.ToString(dr["text"]) ,
                    //        sValue = Convert.ToString(dr["label"]),
                    //    });
                    checkedListBox1.Items.Add(sssssss, false);
                }
                else
                {
                    //measure.Nodes.Add(sssssss);
                    //measurelist.Add(
                    //    new TabP()
                    //{
                    //    sNameValue = sssssss,
                    //    sName = Convert.ToString(dr["text"]),
                    //    sValue = Convert.ToString(dr["label"]),
                    //});
                    checkedListBox2.Items.Add(sssssss, false);
                }
            }
        }
        public static DataSet ConvertXMLFileToDataSet(XmlDocument xmld)
        {
            StringReader stream = null;
            XmlTextReader reader = null;
            try
            {
                DataSet xmlDS = new DataSet();
                stream = new StringReader(xmld.InnerXml);
                //从stream装载到XmlTextReader
                reader = new XmlTextReader(stream);
                xmlDS.ReadXml(reader);
                //xmlDS.ReadXml(xmlFile);
                return xmlDS;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }

        private void tab_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count!=0)
            {
                for (int i = checkedListBox1.Items.Count-1; i >= 0 ; i--)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        checkedListBox1.Items.RemoveAt(i);
                    }
                }
            }

            if (checkedListBox2.CheckedItems.Count != 0)
            {
                for (int i = checkedListBox2.Items.Count - 1; i >= 0; i--)
                {
                    if (checkedListBox2.GetItemChecked(i))
                    {
                        checkedListBox2.Items.RemoveAt(i);
                    }
                }
            }

            PrintTreeViewNode(treeView1.Nodes);
        }
        public void PrintTreeViewNode(TreeNodeCollection node)
        {
            for (int i = node.Count - 1; i >=0 ; i--)
            {
                bool obool = node[i].Checked;
                if (node[i].Nodes.Count != 0)
                {
                    PrintTreeViewNode(node[i].Nodes);
                }
                else
                {
                    if (obool)
                    {
                        string aname = node[i].ToString().Replace("TreeNode: ","");
                        TabP oTabP = dimensionlist.Where(o => o.sNameValue == aname).FirstOrDefault();
                        if (oTabP != null)
                        {
                            dimensionlist.Remove(oTabP);
                            checkedListBox1.Items.Add(aname, false);
                        }
                        else
                        {
                            oTabP = measurelist.Where(o => o.sNameValue == aname).FirstOrDefault();
                            if (oTabP != null)
                            {
                                measurelist.Remove(oTabP);
                                checkedListBox2.Items.Add(aname, false);
                            }
                        }
                        node[i].Remove();
                    }
                }
            }
        }
       

        private void tab2_Click(object sender, EventArgs e)
        {

            //if (checkedListBox1.CheckedItems.Count != 0)
            //{
            //    for (int i = checkedListBox1.Items.Count - 1; i >= 0; i--)
            //    {
            //        if (checkedListBox1.GetItemChecked(i))
            //        {
            //            var oTabP = dimensionlist.Where(o => o.sNameValue == checkedListBox1.Items[i].ToString()).FirstOrDefault();
            //            if (oTabP != null)
            //            {
            //                dimensionlist.Remove(oTabP);
            //            }
            //            checkedListBox1.Items.RemoveAt(i);
            //        }
            //    }
            //}
            string sText = TabP.Text;
            TreeNode TreeNodeP = new TreeNode(sText);
            treeView1.Nodes[0].Nodes.Add(TreeNodeP);
            for (int i = 0; i < treeView1.Nodes[0].Nodes.Count - 1; i++)
            {
                bool obool = treeView1.Nodes[0].Nodes[i].Checked;
                if (obool)
                {
                    treeView1.Nodes[0].Nodes[i].Checked = false;
                    TreeNode newTreeNode = (TreeNode)treeView1.Nodes[0].Nodes[i].Clone();
                    treeView1.Nodes[0].Nodes[i].Remove();
                    TreeNodeP.Nodes.Add(newTreeNode);
                    i--;
                }
            }
                


        }
        private void treeView1_NodeMouseClick(object sender,TreeNodeMouseClickEventArgs e)
        { 
            if (e.Node!=treeView1.Nodes[0])
            {
                if (e.Node.Checked == true)
                {
                    e.Node.Checked = false;
                    //e.Node.BackColor = Color.White;
                    TreeNodeList.Remove(e.Node);
                }
                else
                {
                    e.Node.Checked = true;
                    //e.Node.BackColor = Color.Red;
                    TreeNodeList.Add(e.Node);
                }
            }

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            bool adsfa=  e.Node.IsSelected;
            if (e.Node.BackColor == Color.Red)
            {
                e.Node.Checked = false;
                e.Node.BackColor = Color.White;
                TreeNodeList.Remove(e.Node);
            }
            else
            {
                e.Node.Checked = true;
                e.Node.BackColor = Color.Red;
                TreeNodeList.Add(e.Node);
            }
           
        }

        private void dmtotab_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count != 0)
            {
                for (int i = 0; i <= checkedListBox1.Items.Count - 1; i++)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                        var oTabP = dimensionlist.Where(o => o.sNameValue == checkedListBox1.Items[i].ToString()).FirstOrDefault();
                        string[] oItems = checkedListBox1.Items[i].ToString().Split(':');
                        if (oTabP == null)
                        {
                            dimensionlist.Add(new TabP()
                            {
                                sNameValue = checkedListBox1.Items[i].ToString(),
                                sName = oItems[0],
                                sValue = oItems[1],
                            });
                            treeView1.Nodes[0].Nodes.Add(checkedListBox1.Items[i].ToString());
                        }
                        
                        checkedListBox1.Items.RemoveAt(i);
                        i--;
                    }
                }
            }

            if (checkedListBox2.CheckedItems.Count != 0)
            {
                for (int i = 0; i <= checkedListBox2.Items.Count - 1; i++)
                {
                    if (checkedListBox2.GetItemChecked(i))
                    {
                        var oTabP = measurelist.Where(o => o.sNameValue == checkedListBox2.Items[i].ToString()).FirstOrDefault();
                        string[] oItems = checkedListBox2.Items[i].ToString().Split(':');
                        if (oTabP == null)
                        {
                            measurelist.Add(new TabP()
                            {
                                sNameValue = checkedListBox2.Items[i].ToString(),
                                sName = oItems[0],
                                sValue = oItems[1],
                            });
                            treeView1.Nodes[0].Nodes.Add(checkedListBox2.Items[i].ToString());
                        }
                        checkedListBox2.Items.RemoveAt(i);
                        i--;
                    }
                }
            }
        }

        private void CreateT_Click(object sender, EventArgs e)
        {
            Ctreeslist = new List<string>();
            //int i = treeView1.Nodes[0].
            List<TabP> otablist = new List<TabP>();
            CtreesT(treeView1.Nodes[0].Nodes);
            int maxlv = 0;
            string sTab = "";
            string sTabC = "[";
            foreach (string item in Ctreeslist)
            {
                string[] items = item.Split('|');
                TabP otab = new TabP();
                otab.son = Convert.ToInt32(items[2]);
                otab.lv = Convert.ToInt32(items[0]);
                if (otab.lv > maxlv)
                {
                    maxlv = otab.lv;
                }
                if (items[1].Contains(":"))
                {
                    otab.sName = items[1].Split(':')[0];
                    otab.sValue = items[1].Split(':')[1];
                }
                otab.sNameValue = items[1];
                otab.parent = items[3];
                otablist.Add(otab);
            }
            for (int i = 1; i <= maxlv; i++)
            {
                sTabC += "[";
                foreach (var item in otablist.Where(o => o.lv == i))
                {
                    if (item.son == 0)
                    {
                        sTabC += "{field:" + "'" + item.sName + "'" + "," + "title:" + "'" + item.sValue + "'" + ", sortable: true, fixed: true, align:";
                        if (dimensionlist.Where(o => o.sNameValue == item.sNameValue).FirstOrDefault() != null)
                        {
                            sTabC += "'" + "left" + "',";
                        }
                        else
                        {
                            sTabC += "'" + "right" + "',";
                        }
                        if (maxlv -item.lv > 0)
                        {
                            sTabC += "rowspan:" + (maxlv - item.lv+1);
                        }
                        sTabC += "},";
                    }
                    else
                    {
                        sTabC += "{title:" + "'" + item.sNameValue + "'," + "colspan:" + item.son + "},";
                    }
                }
                sTabC += "],";
            }
            sTabC += "]";
            string sUrl = "http://bwdev.shuanglin.com:8000/sap/opu/odata/sap/" + url.Text + "_SRV/" +url.Text+ "{0}Results?|";
            sTab += "Url(0_0)"+ sUrl+ "Title(0_0)"+tabname.Text+ "|FrozenColumns(0_0)"+ FrozenColumns.Text + "|Columns(0_0)"+ sTabC;

            tabstr.Text = sTab;
        }

        public void CtreesT(TreeNodeCollection node)
        {
            foreach (TreeNode item in node)
            {

                if (item.Nodes.Count != 0)
                {
                    //ison++;
                   CtreesT(item.Nodes);
                    //Ctreeslist.Add(item.Level.ToString() + "|" + item.ToString().Replace("TreeNode: ", "") + "|" + ison + "|" + item.Parent.ToString().Replace("TreeNode: ", ""));
                    //ison = 0;
                }
                //else
                //{
                    //ison++;
                    Ctreeslist.Add(item.Level.ToString() + "|" + item.ToString().Replace("TreeNode: ", "") + "|" + item.Nodes.Count + "|" + item.Parent.ToString().Replace("TreeNode: ", ""));
                //}
                
               
            }
            //return ison;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = false;
            Sel.Show();

            checkedListBox1.Items.Clear();
            checkedListBox2.Items.Clear();

            dimensionlist = new List<TabP>();
            measurelist = new List<TabP>();
            TreeNodeList = new List<TreeNode>();
            string sUrl = "http://bwdev.shuanglin.com:8000/sap/opu/odata/sap/" + url.Text + "_SRV/$metadata?sap-user=guoq&sap-password=ghg2587758";

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(sUrl);
            }
            catch
            {
            }
            ds = ConvertXMLFileToDataSet(doc);

            dt = ds.Tables["Property"];

            DataRow[] drArr = dt.Select("EntityType_Id=1 and  text <> '' ");//查询

            DataTable dtNew = dt.Clone();


            for (int i = 0; i < drArr.Length; i++)
            {
                dtNew.ImportRow(drArr[i]);

            }

            //return result;
            dimensionlist = new List<WindowsFormsApplication2.TabP>();

            foreach (DataRow dr in dtNew.Rows)
            {
                string sNV = Convert.ToString(dr["name"]) + ":" +  Convert.ToString(dr["label"]);
                checkedListBox1.Items.Add(sNV, true);
                dimensionlist.Add(
                    new WindowsFormsApplication2.TabP()
                    {
                        sName = Convert.ToString(dr["name"]),
                        
                        sValue =Convert.ToString(dr["label"]),
                        sNameValue =sNV,
                    }
                    );
                //var obj = new SelectColumn() { ValueField = Convert.ToString(dr["name"]), Width = "200", Multiple = false, Label = Convert.ToString(dr["label"]), TextField = Convert.ToString(dr["text"]) };
                //selectlist.Add(obj);
            }
        }

        private void SelQ_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.CheckedItems.Count != 0)
            {
                for (int i = checkedListBox1.Items.Count - 1; i >= 0; i--)
                {
                    if (checkedListBox1.GetItemChecked(i))
                    {
                       TabP oTabP = dimensionlist.Where(o => o.sNameValue == checkedListBox1.Items[i].ToString()).FirstOrDefault();
                        if (oTabP != null)
                        {
                            dimensionlist.Remove(oTabP);
                        }
                        checkedListBox1.Items.RemoveAt(i);
                    }
                }
            }
        }

        private void SelC_Click(object sender, EventArgs e)
        {
            string selC = "";

            foreach (var a in dimensionlist)
            {
                selC += "Url(0_0)http://bwdev.shuanglin.com:8000/sap/opu/odata/sap/" + url.Text + "_SRV/|Label(0_0)" + a.sValue + "|TextField(0_0)" + a.sName + "MText|ValueField(0_0)" + a.sName + "|Width(0_0)200|Multiple(0_0)true*";
            }
            tabstr.Text = selC;
        }
    }
    public class TabP
    {
        public string sNameValue { get; set; }
        public string sName{ get; set; }

        public string sValue { get; set; }

        public int lv { get; set; }
        public int son { get; set; }
        public string parent { get; set; }
    }
}
