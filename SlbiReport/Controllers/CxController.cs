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
    public class CxController : Controller
    {
        // GET: Cx
        String token = "sap-user=guoq&sap-password=ghg2587758";

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

            var bar = CommonHelper.GetBarViewModel(id, urltt, token);

            return Json(new { status = 1, result = bar });
        }

        [HttpPost]
        public ActionResult Select(string id)
        {

            List<SelectColumn> selectlist = CommonHelper.GetSelect("Sel1");

            return Json(new { status = 1, result = selectlist });
        }

        public String Select_Dim()
        {
            String id = Request["id"];
            String text = Request["text"];

            String result = CommonHelper.GetSelect_Dim("Sel1", id, token);

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


        public ActionResult Rp2_Table1Metadata(string id)
        {

            TableViewModel table = CommonHelper.GetTableMetadata("Tab1");


            return Json(new { status = 1, result = table });
        }


        public String Rp2_Table1data(String id, int page, int rows)
        {
            int skip = (page - 1) * rows;

            string cmd = Request["pagequeryParams"];
            string urltt = QueryParamsurl(cmd);


            string result = CommonHelper.GetTableData("Tab1", urltt, skip.ToString(), rows.ToString(), token);

            return result;

        }



        private static List<T> TableToEntity<T>(DataTable dt) where T : class, new()
        {
            Type type = typeof(T);
            List<T> list = new List<T>();

            foreach (DataRow row in dt.Rows)
            {
                PropertyInfo[] pArray = type.GetProperties();
                T entity = new T();
                foreach (PropertyInfo p in pArray)
                {
                    if (row[p.Name] is Int64)
                    {
                        p.SetValue(entity, Convert.ToInt32(row[p.Name]), null);
                        continue;
                    }
                    p.SetValue(entity, row[p.Name], null);
                }
                list.Add(entity);
            }
            return list;
        }

        public ActionResult TableMetaMap(string id)
        {

            TableViewModel table = CommonHelper.GetTableMetadata(id);

            return Json(new { status = 1, result = table });
        }


        public String TableMap(String id, int page, int rows)
        {
            int skip = (page - 1) * rows;

            string cmd = Request["pagequeryParams"];
            string urltt = QueryParamsurl(cmd);


            string result = CommonHelper.GetTableData(id, urltt, skip.ToString(), rows.ToString(), token);

            return result;

            //return Json(new { total = 1, rows = result },JsonRequestBehavior.AllowGet);
        }
        public ActionResult TableMetaMap_Auto(string id)
        {
            return Json(new { status = 1, result = CommonHelper.GetTableMetadata_Auto(id, token) });
        }

        public String TableMap_Auto(String id, int page, int rows)
        {
            int skip = (page - 1) * rows;

            string cmd = Request["pagequeryParams"];
            string field = Request["field"];
            string urltt = QueryParamsurl(cmd);
            string result = CommonHelper.GetTableData_Auto(id, urltt, skip.ToString(), field, rows.ToString(), token);
            return result;
        }
        public ActionResult ZSD_M001_Q0013()
        {
            return View();
        }
        public ActionResult ZSD_M001_Q0006AndQ0007()
        {
            return View();
        }
        public ActionResult ZSD_M001_Q0004AndQ0005()
        {
            return View();
        }
        public ActionResult ZSD_M001_Q0001AndQ0002AndQ0003()
        {
            return View();
        }
        public ActionResult ZMM_M001_Q0001AndQ0002AndQ0003()
        {
            return View();
        }
        public ActionResult ZMM_M002_Q0003()
        {
            return View();
        }
    }
}