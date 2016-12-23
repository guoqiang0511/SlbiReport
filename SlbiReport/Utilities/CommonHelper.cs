using SlbiReport.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Xml;

namespace SlbiReport.Utilities
{
    public static class CommonHelper
    {
        public static PieMapViewModel GetPieMapViewModel(string sName, string sUrltt, string sToken)
        {

            PieMapViewModel oPieMapViewModel = new PieMapViewModel();
            string sResources = Convert.ToString(LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName));

            StringToEntityValue(oPieMapViewModel, sResources);

            if (oPieMapViewModel.Url != "" && oPieMapViewModel.Url != null)
                oPieMapViewModel.Url = oPieMapViewModel.Url.Replace("{0}", sUrltt);

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

        //public static BarViewModel GetBarViewModel(string sName, string sUrltt, string sToken)
        //{
        //    BarViewModel oBarViewModel = new BarViewModel();

        //    string sResources = LiveAzure.Resources.Models.Common.ModelEnum.ResourceManager.GetObject(sName).ToString();
        //    StringToEntityValue(oBarViewModel, sResources);
        //    oBarViewModel.Url = oBarViewModel.Url.Replace("{0}", sUrltt);
        //    if (!string.IsNullOrEmpty(oBarViewModel.BarSelectName) && !string.IsNullOrEmpty(oBarViewModel.BarSelectValue1)&& !string.IsNullOrEmpty(oBarViewModel.BarSelectValue2))
        //        oBarViewModel.Url += "$select=" + oBarViewModel.BarSelectName + "," + oBarViewModel.BarSelectValue1 + "," + oBarViewModel.BarSelectValue2 + "&";
        //    oBarViewModel.Url += sToken;


        //    DataTable dt = new DataTable();
        //    DataSet ds = new DataSet();
        //    XmlDocument doc = new XmlDocument();
        //    try
        //    {
        //        doc.Load(oBarViewModel.Url);
        //    }
        //    catch
        //    {
        //        return null;
        //    }
        //    ds = ConvertXMLFileToDataSet(doc);

        //    List<string> legend = new List<string>();
        //    legend.Add(oBarViewModel.SeriesName1);
        //    legend.Add(oBarViewModel.SeriesName2);
        //    //legend.Add("");
        //    List<string> xaxisdata = new List<string>();
        //    List<string> series1 = new List<string>();
        //    List<string> series2 = new List<string>();

        //    foreach (DataRow dr in ds.Tables["properties"].Rows)
        //    {
        //        xaxisdata.Add(Convert.ToString(dr[oBarViewModel.BarSelectName]));
        //        series1.Add(Convert.ToString(dr[oBarViewModel.BarSelectValue1]));
        //        series2.Add(Convert.ToString(dr[oBarViewModel.BarSelectValue2]));
        //    }

        //    oBarViewModel.LegendData = legend;
        //    oBarViewModel.AxisData = xaxisdata;
        //    oBarViewModel.SeriesData1 = series1;
        //    oBarViewModel.SeriesData2 = series2;

        //    return oBarViewModel;
        //}

        public static Object StringToEntityValue(object oOject, string sParamStr)
        {            PostParams oPostParams = new PostParams(sParamStr);

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
    }
}