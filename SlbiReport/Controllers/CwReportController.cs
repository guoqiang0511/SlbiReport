using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;
using SlbiReport.Models;

namespace SlbiReport.Controllers
{
    public class CwReportController : Controller
    {

        String token = "sap-user=guoq&sap-password=ghg2587758";

        // GET: CwReport
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult PieMap1(string id)
        {

            string cmd = Request["pagequeryParams"];
            string urltt = QueryParamsurl(cmd);
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            List<string> lists = new List<string>();

            string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/ZDM_M001_Q002" + urltt + "Results?$select=ZDMPLANT_T,A00O2TFKZNC7K2N5JLDC4434TM&" + token;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            ds = ConvertXMLFileToDataSet(doc);

            List<VisitSource> listss = new List<VisitSource>();
            foreach (DataRow dr in ds.Tables["properties"].Rows)
            {
                var obj = new VisitSource() { name = Convert.ToString(dr["ZDMPLANT_T"]), value = Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC4434TM"]) };
                listss.Add(obj);
                lists.Add(Convert.ToString(dr["ZDMPLANT_T"]));
            }

            
            dt = ds.Tables["properties"];


            //return result;

            var pie = new PieMapViewModel()
            {
                Title = "test",
                SubTitle = "subtest",
                LegendData = lists,
                SeriesData = listss
            };


             return Json(new { status = 1, result = pie });
        }


        [HttpPost]
        public ActionResult BarMap(string id)
        {
            string cmd = Request["pagequeryParams"];
            string urltt = QueryParamsurl(cmd);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/ZDM_M001_Q002" + urltt + "Results?$select=A0CALMONTH,A00O2TFKZNC7K2N5JLDC443B56,A00O2TFKZNC7K2N5JLDC443NSA&"+token;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            ds = ConvertXMLFileToDataSet(doc);

            List<string> legend = new List<string>();
            List<string> xaxisdata = new List<string>();
            List<string> series1 = new List<string>();
            List<string> series2 = new List<string>();

 

            foreach (DataRow dr in ds.Tables["properties"].Rows)
            {
                xaxisdata.Add(Convert.ToString(dr["A0CALMONTH"]));
                series1.Add(Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC443B56"]));
                series2.Add(Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC443NSA"]));
            }

            legend.Add("实际");
            legend.Add("预测");
            //return result;

            var bar = new BarViewModel()
            {
                Title = "testbar",
                SubTitle = "subtestbar",
                XAxisData = xaxisdata,
                LegendData = legend,
                SeriesData1 = series1,
                SeriesData2 = series2,
                SeriesName1 = "实际",
                SeriesName2 = "预测"
            };


            return Json(new { status = 1, result = bar });
        }



        public String TableMap(string id)
        {

            string cmd = Request["pagequeryParams"];

            string urltt = QueryParamsurl(cmd);
            
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/ZDM_M001_Q002"+ urltt + "Results?" + token;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            ds = ConvertXMLFileToDataSet(doc);

            dt = ds.Tables["properties"];

            string result = Dtb2Json(dt);
            return result;

            //return Json(new { status = 1, rows = result },JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult TableMetadata(string id)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/$metadata?" + token;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            ds = ConvertXMLFileToDataSet(doc);

            dt = ds.Tables["Property"];

            DataRow[] drArr = dt.Select("EntityType_Id=0 and  text <> '' ");//查询

            DataTable dtNew = dt.Clone();

            DataRow drd = dt.Select("EntityType_Id=0 and  Name = 'A0CALMONTH' ")[0];//查询
            drd["text"] = "A0CALMONTH";
            dtNew.ImportRow(drd);

            for (int i = 0; i < drArr.Length; i++)
            {
                dtNew.ImportRow(drArr[i]);

            }

            //return result;
            List < TableColumn > tablecol = new List<TableColumn>();

            foreach (DataRow dr in dtNew.Rows)
            {
                var obj = new TableColumn() { field = Convert.ToString(dr["text"]), title = Convert.ToString(dr["label"]), width = "120" };
                tablecol.Add(obj);
            }


            return Json(new { status = 1, result = tablecol });
        }



        [HttpPost]
        public ActionResult Select(string id)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/$metadata?" + token;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            ds = ConvertXMLFileToDataSet(doc);

            dt = ds.Tables["Property"];

            DataRow[] drArr = dt.Select("EntityType_Id=1 and  text <> '' ");//查询

            DataTable dtNew = dt.Clone();


            for (int i = 0; i < drArr.Length; i++)
            {
                dtNew.ImportRow(drArr[i]);

            }

            //return result;
            List<SelectColumn> selectlist = new List<Models.SelectColumn>();

            foreach (DataRow dr in dtNew.Rows)
            {
                var obj = new SelectColumn() { valueField = Convert.ToString(dr["name"]), label = Convert.ToString(dr["label"]), textField = Convert.ToString(dr["text"]) };
                selectlist.Add(obj);
            }


            return Json(new { status = 1, result = selectlist });
        }

        public String Select_Dim()
        {
            String id = Request["id"];
            String text = Request["text"];
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/ZDM_M001_Q002?$select="+ id + "," + text + "&" + token;
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            ds = ConvertXMLFileToDataSet(doc);

            dt = ds.Tables["Property"];
            List<object>  lists = new List<object>();
            foreach (DataRow dr in ds.Tables["properties"].Rows)
            {
                var obj = new { id = dr[id], text = dr[text] };
                lists.Add(obj);
            }


            JavaScriptSerializer jsS = new JavaScriptSerializer();
            String result = jsS.Serialize(lists);
            return result;
        }

        private string Dtb2Json(DataTable dtb)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            System.Collections.ArrayList dic = new System.Collections.ArrayList();
            foreach (DataRow dr in dtb.Rows)
            {
                System.Collections.Generic.Dictionary<string, object> drow = new System.Collections.Generic.Dictionary<string, object>();
                foreach (DataColumn dc in dtb.Columns)
                {
                    drow.Add(dc.ColumnName, dr[dc.ColumnName]);
                }
                dic.Add(drow);

            }
            //序列化  
            return jss.Serialize(dic);
        }


        //将xml文件转换为DataSet
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

        private string QueryParamsurl(String param)
        {

            String urltt = "";
            if (param != "" && param != null)
            {
                urltt = urltt + "(";
                string[] strs = param.Split(new string[] { "," }, StringSplitOptions.None);
                foreach (string tt in strs)
                {
                    Console.WriteLine(tt);

                    string[] strss = tt.Split(new string[] { ":" }, StringSplitOptions.None);
                    urltt = urltt + strss[0] + "='" + strss[1] + "',";
                }
                urltt = urltt.Substring(0, urltt.Length - 1);
                urltt = urltt + ")/";
            }
           

            return urltt;
        }

        public ActionResult Report1()
        {
            return View();
        }
    }
}