using SlbiReport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;
using System.Xml;

namespace SlbiReport.Utilities
{
    public static class CommonHelper
    {
        public static PieMapViewModel GetPieMapViewModel(string sName, string sUrltt, string sToken)

        {

            PieMapViewModel oPieMapViewModel = new PieMapViewModel();
            string sResources = LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName).ToString();
            sResources = sResources.Replace("，", ",");
            StringToEntityValue(oPieMapViewModel, sResources);
            if (oPieMapViewModel.Url != "" && oPieMapViewModel.Url != null)
            {
                //默认日期
                if (!string.IsNullOrEmpty(sUrltt))
                {
                    sUrltt = AddQuery(sUrltt, oPieMapViewModel.Url, sToken);
                    sUrltt = GetDateDefaultValue(sUrltt);
                }
                oPieMapViewModel.Url = oPieMapViewModel.Url.Replace("{0}", sUrltt);
            }   

            if (!string.IsNullOrEmpty(oPieMapViewModel.PieMapSelectName) && !string.IsNullOrEmpty(oPieMapViewModel.PieMapSelectValue))
                oPieMapViewModel.Url += "$select=" + oPieMapViewModel.PieMapSelectName + "," + oPieMapViewModel.PieMapSelectValue + "&";
            oPieMapViewModel.Url += sToken;

            List<string> lists = new List<string>();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(oPieMapViewModel.Url);
            }
            catch
            {

                return null;
            }

            ds = ConvertXMLFileToDataSet(doc);
            List<VisitSource> listss = new List<VisitSource>();

            if (ds.Tables["properties"] == null)
            { return null; }

            foreach (DataRow dr in ds.Tables["properties"].Rows)
            {
                var obj = new VisitSource()
                {
                    name = Convert.ToString(dr[oPieMapViewModel.PieMapSelectName]),
                    value = Convert.ToString(Convert.ToDouble(dr[oPieMapViewModel.PieMapSelectValue]))
                };
                listss.Add(obj);
                lists.Add(Convert.ToString(dr[oPieMapViewModel.PieMapSelectName]));
            }

            dt = ds.Tables["properties"];
            oPieMapViewModel.LegendData = lists;
            oPieMapViewModel.SeriesData = listss;
            return oPieMapViewModel;

        }

        public static BarViewModel GetBarViewModel(string sName, string sUrltt, string sToken)
        {
            BarViewModel oBarViewModel = new BarViewModel();

            string sResources = Convert.ToString(LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName));
            sResources = sResources.Replace("，", ",").Replace("\r\n", "");
            StringToEntityValue(oBarViewModel, sResources);

            if (!string.IsNullOrEmpty(sUrltt))
            {
                sUrltt = AddQuery(sUrltt, oBarViewModel.Url, sToken);
                sUrltt = GetDateDefaultValue(sUrltt);   
            }
            

            string sMetadataUrl = oBarViewModel.Url.Remove(oBarViewModel.Url.LastIndexOf('/') + 1) + "$metadata?" + sToken;
           
            oBarViewModel.Url = oBarViewModel.Url.Replace("{0}", sUrltt);
            if (!string.IsNullOrEmpty(oBarViewModel.AxisDataStr) && !string.IsNullOrEmpty(oBarViewModel.SeriesStr))
                oBarViewModel.Url += "$select=" + oBarViewModel.AxisDataStr + "," + oBarViewModel.SeriesStr + "&";
            oBarViewModel.Url += sToken;
            string sUrl = oBarViewModel.Url;
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(sUrl);
            }
            catch
            {
                return null;
            }
            ds = ConvertXMLFileToDataSet(doc);

            if (ds.Tables["properties"] == null)
            {
                return null;
            }

            string[] SeriesStrs = oBarViewModel.SeriesStr.Split(',');
            string[] AxisDataStrs = oBarViewModel.AxisDataStr.Split(',');
            List<string> xaxisdata = new List<string>();

            PostParams oPostParams = new PostParams();
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
                    double odouble = Convert.ToDouble(dr[SeriesStrs[i]]);
                    valuelist.Add(Convert.ToString(odouble.ToString()));

                }
            }

            try
            {
                doc.Load(sMetadataUrl);
            }
            catch
            {

                return null;
            }
            ds = ConvertXMLFileToDataSet(doc);
            if (ds.Tables["Schema"] == null)
            {
                return null;
            }
            List<string> legend = new List<string>();
            legend = oBarViewModel.LegendDataStr.Split(',').ToList();
            List<string> LegendDataStrs = new List<string>();
            //Dictionary<string, string> sDictionary = new Dictionary<string, string>();
            oBarViewModel.Series = new List<BarSeriesModel>();

            for (int i = 0; i < SeriesStrs.Count(); i++)
            {
                List<string> valuelist = (List<string>)oPostParams.GetObject(SeriesStrs[i]);
                foreach (DataRow item in ds.Tables["Property"].Rows)
                {
                    if (item["Name"].ToString() == SeriesStrs[i])
                    {
                        LegendDataStrs.Add(Convert.ToString(item["label"]));
                        oBarViewModel.Series.Add(new BarSeriesModel()
                        {
                            name = Convert.ToString(item["label"]),
                            data = valuelist
                        });
                    }
                }
            }

            //foreach (DataRow item in ds.Tables["Property"].Rows)
            //{           
            //    for (int i = 0; i < legend.Count(); i++)
            //    {
            //        List<string> valuelist = (List<string>)oPostParams.GetObject(SeriesStrs[i]);
            //        if (item["Name"].ToString() == SeriesStrs[i])
            //        {
            //            LegendDataStrs.Add(Convert.ToString(item["label"]));
            //        } 
            //    }
            //    for (int i = 0; i < SeriesStrs.Count(); i++)
            //    {
            //        List<string> valuelist = (List<string>)oPostParams.GetObject(SeriesStrs[i]);
            //        if (item["Name"].ToString() == SeriesStrs[i])
            //        {
            //            oBarViewModel.Series.Add(new BarSeriesModel()
            //            {
            //                name = Convert.ToString(item["label"]),
            //                data = valuelist
            //            });
            //        }
            //    }
            //}

            
            //for (int i = 0; i < SeriesStrs.Count(); i++)
            //{

            //    List<string> valuelist = (List<string>)oPostParams.GetObject(SeriesStrs[i]);
            //    var sSeriesName = (from o in sDictionary
            //                       where o.Key == SeriesStrs[0]
            //                       select o.Value).FirstOrDefault();
            //    oBarViewModel.Series.Add(new BarSeriesModel()
            //    {
            //        name = sSeriesName,
            //        data = valuelist
            //    });
            //}

           

            var bar = new BarViewModel()
            {
                Title = oBarViewModel.Title,
                SubTitle = oBarViewModel.SubTitle,
                AxisData = xaxisdata,
                LegendData = LegendDataStrs,
                Series = oBarViewModel.Series
            };

            return bar;
        }

        public static TableViewModel GetTableMetadata(string sName)
        {
            TableViewModel oTableViewModel = new TableViewModel();
            string sResources = Convert.ToString(LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName));
            sResources = sResources.Replace("，", ",").Replace("\r\n", "").Replace("‘", "'").Replace("’", "'").Replace("：", ":");
            StringToEntityValue(oTableViewModel, sResources);
            string sColList = "";

            if (oTableViewModel.Columns.Contains("dimension"))
            {
                string sColumns1 = oTableViewModel.Columns.Replace(" ", "").Replace("},", "|");
                sColumns1 = StringReplace(sColumns1, "[,],{");
                string[] oColumns = sColumns1.Split('|');

                foreach (var item in oColumns)
                {
                    if (item.Contains("dimension"))
                    {
                        string[] oColumns2 = item.Replace("'", "").Split(',');
                        if (oColumns2 != null)
                        {
                            if (!string.IsNullOrEmpty(oColumns2[0]))
                            {
                                sColList += oColumns2[0].Replace("field:", "") + ",";
                            }
                        }
                    }
                }
            }
            else
            {
                string sColumns = StringReplace(oTableViewModel.Columns, "[,],{,},'");
                var sColumn = sColumns.Split(',');
                List<string> oColumnList = new List<string>();

                foreach (string item in sColumn)
                {
                    if (item.Contains("field:"))
                    {
                        string sItem = item.Replace("field:", "").Replace(" ", "");
                        oColumnList.Add(sItem);
                    }

                }

                string fileName = oTableViewModel.Url.Remove(oTableViewModel.Url.LastIndexOf('/') + 1) + "$metadata?" + "sap-user=guoq&sap-password=ghg2587758";
                DataTable dt = new DataTable();
                DataSet ds = new DataSet();
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

                foreach (DataRow dr in ds.Tables["Property"].Rows)
                {
                    if (Convert.ToString(dr["aggregation-role"]) == "dimension")
                    {
                        string sNames = Convert.ToString(dr["Name"]);
                        var abc = oColumnList.Contains(sNames);
                        if (oColumnList.Contains(sNames))
                        {
                            sColList += sNames + ",";
                        }
                    }
                }
            }

            oTableViewModel.Columns.Replace("aggregation:dimension ,", "");

            oTableViewModel.ColList = sColList;

            return oTableViewModel;
        }

        public static string StringReplace(string sStr, string sSplit)
        {
            sStr.Replace(" ", "");
            string[] oSplits = sSplit.Split(',');
            foreach (var item in oSplits)
            {
                sStr = sStr.Replace(item, "");
            }

            return sStr;
        }


        public static string GetTableData(string sName, string sUrltt, string sSkip, string sRows, string sToken)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            TableViewModel oTableViewModel = new TableViewModel();
            string sResources = Convert.ToString(LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName));
            sResources = sResources.Replace("，", ",");
            StringToEntityValue(oTableViewModel, sResources);
            //默认日期
            if (!string.IsNullOrEmpty(sUrltt))
            {
                sUrltt = AddQuery(sUrltt, oTableViewModel.Url, sToken);
                sUrltt = GetDateDefaultValue(sUrltt);
            }
            oTableViewModel.Url = oTableViewModel.Url.Replace("{0}", sUrltt);
            oTableViewModel.Url += "$inlinecount=allpages&" + sToken;

            if (sSkip != null && sSkip != "")
            {
                oTableViewModel.Url += "&$skip=" + sSkip ;
            }

            if (sRows != null && sRows != "")
            {
                oTableViewModel.Url += "&$top=" + sRows;
            }

            int iTotalIndex = sResources.IndexOf("Total(0_0)");
            string strTotle = string.Empty;
            if (iTotalIndex > 0)
            {
                strTotle = sResources.Substring(iTotalIndex, sResources.Length - iTotalIndex).ToString().Replace("Total(0_0)", "");
            }

            string[] sTotle = strTotle.Split(',');
            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(oTableViewModel.Url);
            }
            catch
            {

                return null;
            }
            ds = ConvertXMLFileToDataSet(doc);

            if (ds.Tables["properties"] == null)
            {
                return null;
            }

            dt = ds.Tables["properties"];

            DataRow dr = ds.Tables["feed"].Select()[0];

           string totalnum = Convert.ToString(dr["count"]);


            List<String> items = new List<String>();

            //合计处理
            if (sTotle.Length > 1)
            {
                DataRow drProperties = dt.NewRow();
                drProperties["" + sTotle[0].ToString() + ""] = "合计";
                double dSum = 0;
                //合计列值计算
                for (int i = 1; i < sTotle.Length; i++)
                {
                    foreach (DataRow drP in dt.Rows)
                    {
                        if (drP["" + sTotle[i] + ""].Equals(string.Empty))
                        {
                            dSum = 0;
                        }
                        else
                        {
                            dSum = Convert.ToDouble(drP["" + sTotle[i] + ""].ToString().Replace(",", ""));
                        }
                        dSum += dSum;
                    }

                    drProperties["" + sTotle[i] + ""] = string.Format("{0:N}", dSum);
                }
                dt.Rows.Add(drProperties);
            }


            string result = "{ \"total\":" + totalnum + " ,\"rows\": " + Dtb2Json(dt) + "}";

            return result;
        }

        //public static string GetTableData_Auto(string sName, string sUrltt, string sSkip,string field, string sRows, string sToken)
        //{
        //    DataTable dt = new DataTable();
        //    DataSet ds = new DataSet();
        //    TableViewModel oTableViewModel = new TableViewModel();
        //    string sResources = Convert.ToString(LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName));
        //    StringToEntityValue(oTableViewModel, sResources);

        //    oTableViewModel.Url = oTableViewModel.Url.Replace("{0}", sUrltt);
        //    oTableViewModel.Url += "$inlinecount=allpages&$select=" + field + "&$skip=" + sSkip + "&$top=" + sRows + "&" + sToken;

        //    XmlDocument doc = new XmlDocument();
        //    try
        //    {
        //        doc.Load(oTableViewModel.Url);
        //    }
        //    catch
        //    {

        //        return null;
        //    }
        //    ds = ConvertXMLFileToDataSet(doc);

        //    dt = ds.Tables["properties"];

        //    DataRow dr = ds.Tables["feed"].Select()[0];

        //    string totalnum = Convert.ToString(dr["count"]);


        //    List<String> items = new List<String>();

        //    string result = "{ \"total\":" + totalnum + " ,\"rows\": " + Dtb2Json(dt) + "}";

        //    return result;
        //}
        public static string GetTableData_Auto(string sName, string sUrltt, string sSkip, string field, string sRows, string sToken)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            TableViewModel oTableViewModel = new TableViewModel();

            string sResources = Convert.ToString(LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName));

            sResources = sResources.Replace("，", ",");
            StringToEntityValue(oTableViewModel, sResources);
            if (!string.IsNullOrEmpty(sUrltt))
            {
                sUrltt = AddQuery(sUrltt, oTableViewModel.Url, sToken);
                sUrltt = GetDateDefaultValue(sUrltt);
            }
            ////TableData的Url特殊标记（★）
            //string sURL = sResources.Replace("Url(0_0)", "★");
            ////TableData的开始位置
            //int iStartIndex = sURL.LastIndexOf("★", sURL.Length - 1) + 1;
            ////TableData的结束位置
            //int iEndIndex = sURL.IndexOf("|Title", 1);
            ////TableData的Url取得
            //sURL = sURL.Substring(iStartIndex, (iEndIndex - iStartIndex));
            //oTableViewModel.Url = sURL;


            oTableViewModel.Url = oTableViewModel.Url.Replace("{0}", sUrltt);
            oTableViewModel.Url += "$inlinecount=allpages&$select=" + field + "&$skip=" + sSkip + "&$top=" + sRows + "&" + sToken;

            XmlDocument doc = new XmlDocument();
            try
            {
                doc.Load(oTableViewModel.Url);
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
        }

        public static List<SelectColumn> GetSelect(string sName)
        {
            List<SelectColumn> selectlist = new List<Models.SelectColumn>();
            string sResources = Convert.ToString(LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName));
            //string strMrz = dr[sId + "_ID"] + "= ''";
            string[] selects = sResources.Split('*');
            for (int i = 0; i < selects.Count()-1; i++)
            {
                SelectColumn oSelectColumn = new SelectColumn();
                StringToEntityValue(oSelectColumn, selects[i]);

                string strMrz = oSelectColumn.ValueField + "= ''";
                strMrz = strMrz.Replace(" ", "");
                string strMrz1 = GetDateDefaultValue(strMrz);
                if (strMrz1.Length!= strMrz.Length)
                {
                    strMrz1 = StringReplace(strMrz1, "(,),'");
                    oSelectColumn.Select = strMrz1.Split('=')[1];
                }

                selectlist.Add(oSelectColumn);
            }

            return selectlist;
        }

        public static List<SelectColumn> GetSelect_Auto(string sName, string sToken)
        {


            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            string sResources = Convert.ToString(LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName));
            string fileName = sResources + sToken;
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
                var obj = new SelectColumn() { ValueField = Convert.ToString(dr["name"]), Width = "200", Multiple = false, Label = Convert.ToString(dr["label"]), TextField = Convert.ToString(dr["text"]) };
                selectlist.Add(obj);
            }

        
            return selectlist;
        }

        public static TableViewModel GetTableMetadata_Auto(string sName, string sToken)
        {


            TableViewModel oTableViewModel = new TableViewModel();
            string sResources = Convert.ToString(LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName));

            StringToEntityValue(oTableViewModel, sResources);

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            //string fileName = oTableViewModel.Url + sToken;
            string fileName = oTableViewModel.Url.Remove(oTableViewModel.Url.LastIndexOf('/')) + "/$metadata?" + sToken;
            //fileName = "http://bwdev.shuanglin.com:8000/sap/opu/odata/sap/ZPU_M001_Q0001_SRV/$metadata?sap-user=guoq&sap-password=ghg2587758";
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
                Title = oTableViewModel.Title,
                Column = tablecol
            };


            return table;
        }

        public static string GetSelect_Dim(string sName, string sId, string sToken)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            // string fileName = "http://bwdev.shuanglin.com:8000/sap/opu/odata/sap/ZFI_M001_Q0003_SRV/ZFI_M001_Q0003Results?$select=" + id + "," + text + "&" + token;
            string sResources = Convert.ToString(LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName));

            PostParams oPostParams = new PostParams(sResources);

            //string sUrl = oPostParams.GetString("Url");
            string sUrl = oPostParams.GetString("*Url");
            if (string.IsNullOrEmpty(sUrl))
            {
                sUrl = oPostParams.GetString("Url");
            }



            string fileName = sUrl + sId + "?" + sToken;

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

            //dt = ds.Tables["Property"];
            List<object> lists = new List<object>();
            lists.Add(new { id = "", text = "" });

            if (ds.Tables["properties"] != null)
            {
                foreach (DataRow dr in ds.Tables["properties"].Rows)
                {
                    var obj = new { id = dr[sId + "_ID"], text = dr[sId + "_TEXT"] };
                    lists.Add(obj);
                }
            }

            JavaScriptSerializer jsS = new JavaScriptSerializer();
            String result = jsS.Serialize(lists);
            return result;
        }

        public static string Dtb2Json(DataTable dtb)
        {
            if (dtb == null)
            {
                return null;
            }
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

        public static Object StringToEntityValue(object oOject, string sParamStr)

        {
            PostParams oPostParams = new PostParams(sParamStr);


            Dictionary<string, object> oDic = oPostParams.GetInsideParams();

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

        public static string GetDateDefaultValue(string sStr)
        {
            
            //(ZBU001_M='',ZPLANT001_M='',ZMATLGROUP001_M='',ZMONTH002_I='2013.11',ZMONTH002_ITo='2014.02')
            //(ZBU001_M = '',ZPLANT001_M = '2011',ZMATLGROUP001_M = '',ZMONTH002_I = '',ZMONTH002_ITo = '')
            //string[] oStr = sStr.Split(',');
            sStr = sStr.Replace(" ", "");
             DateTime oDateTime1;
            DateTime oDateTime = DateTime.Now;
            //string sDateTime = oDateTime.ToString("yyyy-MM-dd");
            //string[] a = sDateTime.Split('-');
            //日历年/月(单值必输,默认上月)默认单值 2016.12
            if (sStr.Contains("ZMONTH001_P=''"))
            {
                oDateTime1 = oDateTime.AddMonths(-1);
                string sMonth = oDateTime1.Month.ToString();
                if (sMonth.Length==1)
                {
                    sMonth = "0" + sMonth;
                }
                sStr = sStr.Replace("ZMONTH001_P=''", "ZMONTH001_P='" + oDateTime1.Year + "." + sMonth + "'");
            }
            //日历年/月(区间必输,默认上月)默认区间 2016.11 - 2016.12
            if (sStr.Contains("ZMONTH002_I=''"))
            {
                oDateTime1 = oDateTime.AddMonths(-1);
                string sMonth1 = oDateTime1.Month.ToString();
                if (sMonth1.Length == 1)
                {
                    sMonth1 = "0" + sMonth1;
                }
                //string sMonth = oDateTime.Month.ToString();
                //if (sMonth.Length == 1)
                //{
                //    sMonth = "0" + sMonth;
                //}
                sStr = sStr.Replace("ZMONTH002_I=''", "ZMONTH002_I='" + oDateTime1.Year + "." + sMonth1 + "'");
                //sStr = sStr.Replace("ZMONTH002_ITo=''", "ZMONTH002_ITo='" + oDateTime1.Year + "." + sMonth1 + "'");
            }
            if (sStr.Contains("ZMONTH002_ITo=''"))
            {
                oDateTime1 = oDateTime.AddMonths(-1);
                string sMonth1 = oDateTime1.Month.ToString();
                if (sMonth1.Length == 1)
                {
                    sMonth1 = "0" + sMonth1;
                }
                //sStr = sStr.Replace("ZMONTH002_I=''", "ZMONTH002_I='" + oDateTime1.Year + "." + sMonth1 + "'");
                sStr = sStr.Replace("ZMONTH002_ITo=''", "ZMONTH002_ITo='" + oDateTime1.Year + "." + sMonth1 + "'");
            }
            //日历年月(区间必输,默认本年)默认区间 2017.01 - 2017.01
            if (sStr.Contains("ZMONTH004_I=''"))
            {
                string sMonth = oDateTime.Month.ToString();
                if (sMonth.Length == 1)
                {
                    sMonth = "0" + sMonth;
                }
                sStr = sStr.Replace("ZMONTH004_I=''", "ZMONTH004_I='" + oDateTime.Year + "." + "01" + "'");
                sStr = sStr.Replace("ZMONTH004_ITo=''", "ZMONTH004_ITo='" + oDateTime.Year + "." + sMonth + "'");
            }
            if (sStr.Contains("ZMONTH004_ITo=''"))
            {
                string sMonth = oDateTime.Month.ToString();
                if (sMonth.Length == 1)
                {
                    sMonth = "0" + sMonth;
                }
                //sStr = sStr.Replace("ZMONTH004_I=''", "ZMONTH004_I='" + oDateTime.Year + "." + "01" + "'");
                sStr = sStr.Replace("ZMONTH004_ITo=''", "ZMONTH004_ITo='" + oDateTime.Year + "." + sMonth + "'");
            }
            // 日历日(单值必输, 默认当前一天)默认单值 2017.01.08
            if (sStr.Contains("ZDAY001_P=''"))
            {
                oDateTime1 = oDateTime.AddDays(-1);
                string sYear = oDateTime1.Year.ToString();
                string sMonth = oDateTime1.Month.ToString();
                string sDay = oDateTime1.Day.ToString();
                if (sMonth.Length == 1)
                {
                    sMonth = "0" + sMonth;
                }
                if (sDay.Length == 1)
                {
                    sDay = "0" + sDay;
                }
                sStr = sStr.Replace("ZDAY001_P=''", "ZDAY001_P='" + sYear + "." + sMonth + "."+ sDay + "'");
            }
            //总账科目(区间,应付账款From)
            if (sStr.Contains("ZGL_ACCOUNT001_M=''"))
            {
                string strZGL_ACCOUNT001_M = "2202010000";
                sStr = sStr.Replace("ZGL_ACCOUNT001_M=''", "ZGL_ACCOUNT001_M='" + strZGL_ACCOUNT001_M + "'");
            }
            //总账科目(区间,应付账款To)
            if (sStr.Contains("ZGL_ACCOUNT001_MTo=''"))
            {
                string strZGL_ACCOUNT001_MTo = "2202999999";
                sStr = sStr.Replace("ZGL_ACCOUNT001_MTo=''", "ZGL_ACCOUNT001_MTo='" + strZGL_ACCOUNT001_MTo + "'");
            }
            return sStr;
        }

        public static string AddQuery(string sStr,string sUrl,string sToken)
        {
            if (string.IsNullOrEmpty(sStr))
            {
                return sStr;
            }

            string sStr1 = StringReplace(sStr, "(,),/");
            string[] oStrs = sStr1.Split(',');
            List<string> oNameList = new List<string>();
            List<string> oValueList = new List<string>();

            foreach (var item in oStrs)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    string[] oitem = item.Split('=');
                    oNameList.Add(oitem[0]);
                    oValueList.Add(oitem[1]);
                }
            }

            sUrl = sUrl.Remove(sUrl.LastIndexOf('/'));
            sUrl =  sUrl + "/$metadata?"+ sToken;

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
            List<string> oDataRow = new List<string>();
            foreach ( DataRow dr in dtNew.Rows)
            {
                if (!oNameList.Contains(Convert.ToString(dr["name"])))
                {
                    oNameList.Add(Convert.ToString(dr["name"]));
                    oValueList.Add("''");
                    //sStr = sStr.Replace(")", "," + Convert.ToString(dr["name"]) + "='')");
                }
                oDataRow.Add(Convert.ToString(dr["name"]));
            }
            string sRestr = "(";
            for (int i = 0; i <=oNameList.Count-1; i++)
            {
                if (oDataRow.Contains(oNameList[i]))
                {
                    sRestr += oNameList[i] + "=" + oValueList[i] + ",";
                }
            }
            sRestr += ")/";

            
            return sRestr;
        }

    }
}