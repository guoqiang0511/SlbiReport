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
using System.Reflection;
using SlbiReport.Utilities;

namespace SlbiReport.Controllers
{
    public class DashBoardController : Controller
    {

        String token = "sap-user=guoq&sap-password=ghg2587758";

        // GET: CwReport
        public ActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public ActionResult PieMap(string id)
        {
            string cmd = Request["pagequeryParams"];
            string urltt = QueryParamsurl(cmd);

            var pie = new PieMapViewModel();

            pie = CommonHelper.GetPieMapViewModel(id, urltt, token);

            return Json(new { status = 1, result = pie });
        }


        [HttpPost]
        public ActionResult BarMap(string id)
        {
            string cmd = Request["pagequeryParams"];
            string urltt = QueryParamsurl(cmd);

            var bar = CommonHelper.GetBarViewModel(id,urltt,token);
            return Json(new { status = 1, result = bar });
        }

        

        [HttpPost]
        public ActionResult Select(string id)
        {
            return Json(new { status = 1, result = CommonHelper.GetSelect(id) });
        }

        public ActionResult Select_Auto(string id)
        {
            return Json(new { status = 1, result = CommonHelper.GetSelect_Auto(id,token) });
        }

        public String Select_Dim(string id)
        {
            String field = Request["field"];
           // String text = Request["text"];

            String result = CommonHelper.GetSelect_Dim(id, field, token);

            //DataTable dt = new DataTable();
            //DataSet ds = new DataSet();
     
            //string fileName = "http://bwdev.shuanglin.com:8000/sap/opu/odata/sap/ZFI_M001_Q0003_SRV/" + field + "?" + token;

            //XmlDocument doc = new XmlDocument();
            //try
            //{
            //    doc.Load(fileName);
            //}
            //catch
            //{

            //    return null;
            //}
            //ds = ConvertXMLFileToDataSet(doc);

            //dt = ds.Tables["Property"];
            //List<object> lists = new List<object>();
            //foreach (DataRow dr in ds.Tables["properties"].Rows)
            //{
            //    var obj = new { id = dr[field + "_ID"], text = dr[field + "_TEXT"] };
            //    lists.Add(obj);
            //}


            //JavaScriptSerializer jsS = new JavaScriptSerializer();
            //String result = jsS.Serialize(lists);
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


        public ActionResult TableMetaMap(string id)
        {

            TableViewModel table = CommonHelper.GetTableMetadata(id);

            return Json(new { status = 1, result = table });
        }


        public String TableMap(String id)
        {
           // int skip = (page - 1) * rows;

            string cmd = Request["pagequeryParams"];
            string urltt = QueryParamsurl(cmd);

            string result = CommonHelper.GetTableData(id , urltt, "","", token);

            return result;
            //return Json(new { total = 1, rows = result },JsonRequestBehavior.AllowGet);
        }


        public ActionResult TableMetaMap_Auto(string id)
        {
            return Json(new { status = 1, result = CommonHelper.GetTableMetadata_Auto(id,token) });
        }


        public String TableMap_Auto(String id,int page, int rows)
        {
            int skip = (page - 1) * rows;

            string cmd = Request["pagequeryParams"];
            string field = Request["field"];
            string urltt = QueryParamsurl(cmd);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://bwdev.shuanglin.com:8000/sap/opu/odata/sap/ZPU_M001_Q0001_SRV/ZPU_M001_Q0001" + urltt + "Results?$select="+ field + "&$inlinecount=allpages&$skip=" + skip + "&$top=" + rows + "&" + token;
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(fileName);
            }
            catch
            {

                return null;
            }
            ds = ConvertXMLFileToDataSet(doc);

            dt = ds.Tables["properties"];

            DataRow dr = ds.Tables["feed"].Select()[0];

            string totalnum = Convert.ToString(dr["count"]);



            List<String> items = new List<String>();

            string result = "{ \"total\":" + totalnum + " ,\"rows\": " + Dtb2Json(dt) + "}";


            return result;

            //return Json(new { total = 1, rows = result },JsonRequestBehavior.AllowGet);
        }


        

        public ActionResult BusinessOverview1()
        {
            return View();
        }

        public ActionResult Report()
        {
            return View();
        }

        public ActionResult BusinessOverview2()
        {
            return View();
        }

        public ActionResult Report4()
        {
            return View();
        }

    }
}