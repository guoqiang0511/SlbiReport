using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Xml;

namespace SlbiReport.Controllers
{
    public class CwReportController : Controller
    {
        // GET: CwReport
        public ActionResult Index()
        {
            return View();
        }

        public String PieMap1(string id)
        {
            string month_s = Request["month_s"];
            string month_sto = Request["month_sto"];
            string mbu_p = Request["mbu_p"];
            string qx = Request["qx"];
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/ZDM_M001_Q002(ZMONTH_S001='" + month_s + "',ZMONTH_S001To='" + month_sto + "',ZDMBU_P001='" + mbu_p + "')/Results?$select=" + qx + "&sap-user=guoq&sap-password=ghg2587758";
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);

            ds = ConvertXMLFileToDataSet(doc);
            dt = ds.Tables["properties"];

            string result = Dtb2Json(dt);

            return result;
           // return Json(dt, JsonRequestBehavior.AllowGet);
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

        public ActionResult Report1()
        {
            return View();
        }
    }
}