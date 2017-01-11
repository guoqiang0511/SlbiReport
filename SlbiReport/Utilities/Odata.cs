using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Xml;

namespace SlbiReport.Utilities
{
    public class Odata
    {
        public DataTable dt = new DataTable();
        public DataSet ds = new DataSet();
        public XmlDocument doc = new XmlDocument();
        public DataRowCollection oProperties;
        public Odata(string sUrl)
        {
            try
            {
                doc.Load(sUrl);
                ds = CommonHelper.ConvertXMLFileToDataSet(doc);
                oProperties = ds.Tables["properties"].Rows;
            }
            catch
            {
               
            }

        }

    }
}