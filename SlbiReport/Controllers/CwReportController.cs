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
    public class CwReportController : Controller
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


            //DataTable dt = new DataTable();
            //DataSet ds = new DataSet();
            //List<string> lists = new List<string>();
            //string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/ZDM_M001_Q002" + urltt + "Results?$select=ZDMPLANT_T,A00O2TFKZNC7K2N5JLDC4434TM&" + token;
            //XmlDocument doc = new XmlDocument();
            //try
            //{
            //    doc.Load(fileName);
            //}
            //catch {

            //    return null;
            //}

            //ds = ConvertXMLFileToDataSet(doc);

            //List<VisitSource> listss = new List<VisitSource>();
            //foreach (DataRow dr in ds.Tables["properties"].Rows)
            //{
            //    var obj = new VisitSource() { name = Convert.ToString(dr["ZDMPLANT_T"]), value = Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC4434TM"]) };
            //    listss.Add(obj);
            //    lists.Add(Convert.ToString(dr["ZDMPLANT_T"]));
            //}
            //dt = ds.Tables["properties"];
            //return result;
            //var pie = new PieMapViewModel()
            //{
            //    Title = "test",
            //    SubTitle = "subtest",
            //    LegendData = lists,
            //    SeriesData = listss
            //};


            return Json(new { status = 1, result = pie });
        }


        [HttpPost]
        public ActionResult BarMap(string id)
        {
            string cmd = Request["pagequeryParams"];
            string urltt = QueryParamsurl(cmd);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/ZDM_M001_Q002" + urltt + "Results?$select=A0CALMONTH,A00O2TFKZNC7K2N5JLDC443B56,A00O2TFKZNC7K2N5JLDC443NSA&" + token;
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

            List<string> legend = new List<string>();


            List<string> xaxisdata = new List<string>();
            List<BarSeriesModel> series = new List<BarSeriesModel>();
            //List<TempObjectModel> s1 = new List<TempObjectModel>();
            BarViewModel oBarViewModel = new BarViewModel();
            oBarViewModel.Series = new List<BarSeriesModel>();
            oBarViewModel.SeriesStr = "A00O2TFKZNC7K2N5JLDC443B56,A00O2TFKZNC7K2N5JLDC443NSA";
            oBarViewModel.AxisDataStr = "A0CALMONTH";
            string[] SeriesStrs = oBarViewModel.SeriesStr.Split(',');
            string[] AxisDataStrs = oBarViewModel.AxisDataStr.Split(',');
            PostParams oPostParams = new PostParams();

            //for (int i = 0; i <= SeriesStrs.Count(); i++)
            //{
            //    oPostParams.Add(SeriesStrs[0], new List<string>());
            //}
            foreach (DataRow dr in ds.Tables["properties"].Rows)
            {

                for (int i = 0; i < AxisDataStrs.Count(); i++)
                {
                    xaxisdata.Add(Convert.ToString(dr[AxisDataStrs[i]]));
                }

                for (int i = 0; i < SeriesStrs.Count(); i++)
                {

                    List<string> valuelist = (List<string>)oPostParams.GetObject(SeriesStrs[i]);
                    if (valuelist == null)
                    {
                        valuelist = new List<string>();
                        oPostParams.Add(SeriesStrs[i], valuelist);
                    }
                    valuelist.Add(Convert.ToString(dr[SeriesStrs[i]]));

                }
                //var obj = new TempObjectModel { name = Convert.ToString(dr["A0CALMONTH"]), value = Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC443B56"]), group = "实际" };
                //var obj1 = new TempObjectModel { name = Convert.ToString(dr["A0CALMONTH"]), value = Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC443B56"]), group = "实际" };



                //BarSeriesModel oBarSeriesModel1 = new BarSeriesModel();
                //oBarSeriesModel1.SeriesName = Convert.ToString(dr["A0CALMONTH"]);
                //oBarSeriesModel1.SeriesData.Add(Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC443B56"]));
                //series.Add(oBarSeriesModel1);

                //BarSeriesModel oBarSeriesModel2 = new BarSeriesModel();
                //oBarSeriesModel2.SeriesName = Convert.ToString(dr["A0CALMONTH"]);
                //oBarSeriesModel2.SeriesData.Add(Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC443B56"]));
                //series.Add(oBarSeriesModel2);
                //s1.Add(obj);
                //s1.Add(obj1);
            }

            for (int i = 0; i < SeriesStrs.Count(); i++)
            {

                List<string> valuelist = (List<string>)oPostParams.GetObject(SeriesStrs[i]);

                oBarViewModel.Series.Add(new BarSeriesModel()
                {
                    name = SeriesStrs[i],
                    data = valuelist
                });
            }
            oBarViewModel.LegendDataStr = "实际,预测";
            legend = oBarViewModel.LegendDataStr.Split(',').ToList();


            //legend.Add("实际");
            //legend.Add("预测");
            //return result;
            var bar = new BarViewModel()
            {
                Title = "testbar",
                SubTitle = "subtestbar",
                AxisData = xaxisdata,
                LegendData = legend,
                Series = oBarViewModel.Series
                //SeriesData1 = series1,
                //SeriesData2 = series2,
                //SeriesName1 = "实际",
                //SeriesName2 = "预测"
            };


            return Json(new { status = 1, result = bar });
        }

        public static Dictionary<string, object> PostParams(string queryString, string split1 = "|", string split2 = "(0_0)")
        {

            Dictionary<string, object> oInsideParams = new Dictionary<string, object>();

            if (!String.IsNullOrEmpty(queryString))
            {

                try

                {

                    string[] strList1 = queryString.Split(split1.ToArray());

                    for (int i = 0; i < strList1.Length; i++)

                    {

                        string[] strList2 = strList1[i].Replace("(0_0)", "|").Split(split1.ToArray());

                        if (String.IsNullOrEmpty(strList2[0])) continue;

                        if (oInsideParams.ContainsKey(strList2[0]))

                            oInsideParams[strList2[0]] = strList2[1];

                        else

                            oInsideParams.Add(strList2[0], strList2[1]);

                    }

                }

                catch { }

            }

            return oInsideParams;

        }

        public static Object StringToEntityValue(object oOject, string sParamStr)

        {

            Dictionary<string, object> oDic = PostParams(sParamStr);

            string sFieldName = string.Empty;

            foreach (var item in oDic)

            {

                FieldAssignment(oOject, item.Key, item.Value.ToString());

            }

            return oOject;

        }

        public static int FieldAssignment(object oObject, string sKey, string sValue)

        {





            string[] sFkKey = sKey.Split('.');

            var oObjectNew = new object();

            if (sFkKey.Length > 1)

            {

                oObjectNew = GetForeignkeyObject(oObject, sFkKey[0]);

                if (oObjectNew == null)

                {

                    return -1;

                }

            }

            else

                oObjectNew = oObject;

            for (int i = 1; i < sFkKey.Length - 1; i++)

            {

                oObjectNew = GetForeignkeyObject(oObjectNew, sFkKey[i]);

            }

            PropertyInfo oProperty = oObjectNew.GetType().GetProperty(sFkKey[sFkKey.Length - 1]);

            if (oProperty != null)

            {

                switch (oProperty.PropertyType.Name)

                {

                    case "String":

                        oProperty.SetValue(oObjectNew, sValue);

                        break;

                    case "Int32":

                        int nValue = 0;

                        Int32.TryParse(sValue, out nValue);

                        oProperty.SetValue(oObjectNew, nValue);

                        break;

                    case "Byte":

                        byte bValue = 0;

                        //bValue = CommonHelper.Getbyte(sValue);

                        Byte.TryParse(sValue, out bValue);

                        oProperty.SetValue(oObjectNew, bValue);

                        break;

                    case "Guid":

                        Guid gGuid;

                        Guid.TryParse(sValue, out gGuid);

                        oProperty.SetValue(oObjectNew, gGuid);

                        break;

                    case "DateTimeOffset":

                        DateTimeOffset oData;

                        DateTimeOffset.TryParse(sValue, out oData);

                        oProperty.SetValue(oObjectNew, oData);

                        break;

                    case "Decimal":

                        System.Decimal dDecimal = 0;

                        System.Decimal.TryParse(sValue, out dDecimal);

                        oProperty.SetValue(oObjectNew, dDecimal);

                        break;

                    case "Int64":

                        long lLong = 0;

                        long.TryParse(sValue, out lLong);

                        oProperty.SetValue(oObjectNew, lLong);

                        break;

                    case "Boolean":

                        Boolean oBool = true;

                        //sValue = sValue == "0" ? "false" : "true";

                        //Boolean.TryParse(sValue, out oBool);

                        if (sValue == "0" || sValue.ToLower() == "false" || String.IsNullOrEmpty(sValue))

                            oBool = false;

                        oProperty.SetValue(oObjectNew, oBool);

                        break;

                    case "Nullable`1":

                        if (oProperty.PropertyType.GenericTypeArguments[0].Name == "Guid")

                        {

                            Guid oGid;

                            Guid.TryParse(sValue, out oGid);

                            if (oGid == null || oGid == Guid.Empty)

                            {

                                oProperty.SetValue(oObjectNew, null);

                            }

                            else

                            {

                                oProperty.SetValue(oObjectNew, oGid);

                            }



                        }

                        if (oProperty.PropertyType.GenericTypeArguments[0].Name == "DateTimeOffset")

                        {

                            DateTimeOffset oNullableData;

                            DateTimeOffset.TryParse(sValue, out oNullableData);

                            oProperty.SetValue(oObjectNew, oNullableData);

                        }

                        break;

                    default:

                        return -1;

                }

            }

            else

                return -1;

            return 0;

        }

        public static object GetForeignkeyObject(object oObject, string sKey)

        {

            PropertyInfo oPropertyInfo = oObject.GetType().GetProperty(sKey);

            if (oPropertyInfo == null)

            {

                return null;

            }

            var oValue = oPropertyInfo.GetValue(oObject);



            if (oValue == null)

            {

                var oIsValueType = oPropertyInfo.PropertyType.IsValueType;

                oValue = Activator.CreateInstance(oPropertyInfo.PropertyType);

            }

            oObject.GetType().GetProperty(sKey).SetValue(oObject, oValue);

            //}

            return oValue;

        }


        public PieMapViewModel GetPieMapViewModel(string sName, string sUrltt, string sToken)

        {

            PieMapViewModel oPieMapViewModel = new PieMapViewModel();

            string sResources = LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName).ToString();



            StringToEntityValue(oPieMapViewModel, sResources);



            oPieMapViewModel.Url = oPieMapViewModel.Url.Replace("{0}", sUrltt) + sToken;



            List<string> lists = new List<string>();

            DataTable dt = new DataTable();

            DataSet ds = new DataSet();



            XmlDocument doc = new XmlDocument();
            doc.Load(oPieMapViewModel.Url);
            ds = ConvertXMLFileToDataSet(doc);

            List<VisitSource> listss = new List<VisitSource>();



            foreach (DataRow dr in ds.Tables["properties"].Rows)
            {
                var obj = new VisitSource()

                {

                    name = Convert.ToString(dr[oPieMapViewModel.PieMapSelectName]),

                    value = Convert.ToString(dr[oPieMapViewModel.PieMapSelectValue])

                };

                listss.Add(obj);

                lists.Add(Convert.ToString(dr[oPieMapViewModel.PieMapSelectName]));


            }



            dt = ds.Tables["properties"];

            oPieMapViewModel.LegendData = lists;

            oPieMapViewModel.SeriesData = listss;

            return oPieMapViewModel;

        }

        [HttpPost]
        public ActionResult BarMap1(string id)
        {
            string cmd = Request["pagequeryParams"];
            string urltt = QueryParamsurl(cmd);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/ZDM_M001_Q002" + urltt + "Results?$select=A0CALMONTH,A00O2TFKZNC7K2N5JLDC443B56,A00O2TFKZNC7K2N5JLDC443NSA&" + token;
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

            List<string> legend = new List<string>();


            List<string> xaxisdata = new List<string>();
            List<BarSeriesModel> series = new List<BarSeriesModel>();
            List<TempObjectModel> s1 = new List<TempObjectModel>();
            foreach (DataRow dr in ds.Tables["properties"].Rows)
            {
                var obj = new TempObjectModel { name = Convert.ToString(dr["A0CALMONTH"]), value = Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC443B56"]), group = "实际" };
                var obj1 = new TempObjectModel { name = Convert.ToString(dr["A0CALMONTH"]), value = Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC443B56"]), group = "实际" };

                s1.Add(obj);
                s1.Add(obj1);
            }





            legend.Add("实际");
            legend.Add("预测");
            //return result;

            var bar = new BarViewModel()
            {
                Title = "testbar",
                SubTitle = "subtestbar",
                AxisData = xaxisdata,
                LegendData = legend,
                //SeriesData1 = series1,
                //SeriesData2 = series2,
                //SeriesName1 = "实际",
                //SeriesName2 = "预测"
            };


            return Json(new { status = 1, result = bar });
        }

        public ActionResult BarMap2(string id)
        {
            string cmd = Request["pagequeryParams"];
            string urltt = QueryParamsurl(cmd);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/ZDM_M001_Q002" + urltt + "Results?$select=A0CALMONTH,A00O2TFKZNC7K2N5JLDC443B56,A00O2TFKZNC7K2N5JLDC443HGQ,A00O2TFKZNC7K2N5JLDC443NSA&" + token;
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

            List<string> legend = new List<string>();
            List<string> xaxisdata = new List<string>();
            List<string> series1 = new List<string>();
            List<string> series2 = new List<string>();
            List<string> series3 = new List<string>();


            foreach (DataRow dr in ds.Tables["properties"].Rows)
            {
                xaxisdata.Add(Convert.ToString(dr["A0CALMONTH"]));
                series1.Add(Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC443B56"]));
                series2.Add(Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC443NSA"]));
                series3.Add(Convert.ToString(dr["A00O2TFKZNC7K2N5JLDC443HGQ"]));
            }

            legend.Add("实际");
            legend.Add("预测");
            legend.Add("XX");
            //return result;

            var bar = new BarViewModel()
            {
                Title = "testbar",
                SubTitle = "subtestbar",
                AxisData = xaxisdata,
                LegendData = legend,
                //SeriesData1 = series1,
                //SeriesData2 = series2,
                //SeriesData3 = series3,
                //SeriesName1 = "实际",
                //SeriesName2 = "预测",
                //SeriesName3 = "XX"
            };


            return Json(new { status = 1, result = bar });
        }


        public String TableMap(string id)
        {

            string cmd = Request["pagequeryParams"];

            string urltt = QueryParamsurl(cmd);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/ZDM_M001_Q002" + urltt + "Results?" + token;
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
            try
            {
                doc.Load(fileName);
            }
            catch
            {

                return null;
            }
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
            List<TableColumn> tablecol = new List<TableColumn>();

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
            try
            {
                doc.Load(fileName);
            }
            catch
            {

                return null;
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
            string fileName = "http://hanadev.shuanglin.com:8000/sap/opu/odata/sap/ZDM_M001_Q002_SRV/ZDM_M001_Q002?$select=" + id + "," + text + "&" + token;
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

            dt = ds.Tables["Property"];
            List<object> lists = new List<object>();
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


        public ActionResult Rp2_Table1Metadata(string id)
        {

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://bwdev.shuanglin.com:8000/sap/opu/odata/sap/ZFI_M001_Q0003_SRV/$metadata?" + token;
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

            dt = ds.Tables["Property"];

            DataRow[] drArr = dt.Select("EntityType_Id=0 and  text <> '' ");//查询

            DataTable dtNew = dt.Clone();

            //DataRow drd = dt.Select("EntityType_Id=0 and  Name = 'A0CALMONTH' ")[0];//查询
            //drd["text"] = "A0CALMONTH";
            //dtNew.ImportRow(drd);

            for (int i = 0; i < drArr.Length; i++)
            {
                dtNew.ImportRow(drArr[i]);

            }

            //return result;
            List<TableColumn> tablecol = new List<TableColumn>();

            foreach (DataRow dr in dtNew.Rows)
            {
                Boolean frozen = false;
                if (Convert.ToString(dr["aggregation-role"]) == "dimension")
                    frozen = true;

                var obj = new TableColumn() { field = Convert.ToString(dr["text"]), title = Convert.ToString(dr["label"]), width = "100", frozen = frozen };
                tablecol.Add(obj);
            }


            var table = new TableViewModel()
            {
                Title = "到期回款状况：",
                // Column = tablecol,
                FrozenColumns = "[[{field: 'ZCUSTOMER_T',title: '客户',sortable:true, formatter:'',fixed:true}]]",
                Columns = "[[{ title: 'Item Details', colspan: 7 }], [{ field: 'A00O2TFHXIFF3PJIBEFO12Z9IL_F',  title: '本月到期款-原币', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJAN433USWUC_F',   title: '本月到期款-本币', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJAY65NP5OBO_F',   title: '回款金额-现汇', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJAY65NP5UN8_F', title: '回款金额-承兑', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJB7XWANDFNH_F', title: '回款金额-小计', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJBCMEH9SZGV_F', title: '回款率', sortable: true, fixed: true, align: 'right' },{ field: 'A00O2TFHXIFF3PJJBDKS5JMP3K_F', title: '差异', sortable: true, fixed: true, align: 'right' }]]"
            };

            return Json(new { status = 1, result = table });
        }


        public String Rp2_Table1data(String id, int page, int rows)
        {
            int skip = (page - 1) * rows;

            string cmd = Request["pagequeryParams"];
            string urltt = QueryParamsurl(cmd);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://bwdev.shuanglin.com:8000/sap/opu/odata/sap/ZFI_M001_Q0003_SRV/ZFI_M001_Q0003" + urltt + "Results?$inlinecount=allpages&$skip=" + skip + "&$top=" + rows + "&" + token;
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


        public ActionResult Rp2_Bar1Map(string id)
        {
            string cmd = Request["pagequeryParams"];
            string urltt = QueryParamsurl(cmd);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string fileName = "http://bwdev.shuanglin.com:8000/sap/opu/odata/sap/ZFI_M001_Q0003_SRV/ZFI_M001_Q0003" + urltt + "Results?$select=A00O2TFHXIFF3PJJB7XWANDFNH,A00O2TFKZNC7K2N5JLDC443B56,A00O2TFKZNC7K2N5JLDC443NSA&" + token;
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
                AxisData = xaxisdata,
                LegendData = legend,
                //SeriesData1 = series1,
                //SeriesData2 = series2,
                //SeriesName1 = "实际",
                //SeriesName2 = "预测"
            };


            return Json(new { status = 1, result = bar });
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

        public ActionResult Report1()
        {
            return View();
        }

        public ActionResult Report2()
        {
            return View();
        }

        public ActionResult Report3()
        {
            return View();
        }

        public ActionResult Report4()
        {
            return View();
        }

    }
}