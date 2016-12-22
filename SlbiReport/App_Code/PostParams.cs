using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlbiReport.App_Code
{
    public class PostParams
    {
        private Dictionary<string, object> oInsideParams;   // 参数键值对

        #region 构造函数和初始化
        /// <summary>
        /// 构造函数，按二级分隔符
        /// </summary>
        /// <param name="queryString">原字符串</param>
        /// <param name="split1">一级分隔符</param>
        /// <param name="split2">二级分隔符</param>
        /// <param name="stringProtect">是否执行SQL注入保护</param>

        public PostParams(string queryString = "", string split1 = "|", string split2 = "(0_0)")
        {
            oInsideParams = new Dictionary<string, object>();
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
        }

        #endregion

        #region 调用接口
        /// <summary>
        /// 返回键值对数量
        /// </summary>
        /// <returns></returns>
        public int Count
        {
            get
            {
                return oInsideParams.Keys.Count;
            }
        }

        public Dictionary<string, object> GetInsideParams()
        {
            return this.oInsideParams;
        }

        /// <summary>
        /// 获取对象
        /// </summary>
        /// <param name="sKey"></param>
        /// <param name="oDefault"></param>
        /// <returns></returns>
        public object GetObject(string sKey, object oDefault = null)
        {
            object result = oDefault;
            if (oInsideParams.ContainsKey(sKey))
                result = oInsideParams[sKey];
            return result;
        }

        /// <summary>
        /// 获取字符串值
        /// </summary>
        /// <param name="sKey">键</param>
        /// <param name="sDefault">默认值</param>
        /// <returns>值</returns>
        public string GetString(string sKey, string sDefault = "")
        {
            string result = sDefault;
            try
            {
                if (oInsideParams.ContainsKey(sKey))
                    result = oInsideParams[sKey].ToString().Trim();
            }
            catch
            { }
            return result;
        }

        public int GetInteger(string sKey, int nDefault = 0)
        {
            int result = nDefault;
            if (oInsideParams.ContainsKey(sKey))
                Int32.TryParse(oInsideParams[sKey].ToString().Trim(), out result);
            return result;
        }

        public long GetLong(string sKey, long nDefault = 0)
        {
            long nResult = nDefault;
            if (oInsideParams.ContainsKey(sKey))
            {
                long.TryParse(oInsideParams[sKey].ToString().Trim(), out nResult);
            }

            return nResult;
        }

        public byte GetByte(string sKey, byte nDefault = 0)
        {
            byte result = nDefault;
            if (oInsideParams.ContainsKey(sKey))
                Byte.TryParse(oInsideParams[sKey].ToString().Trim(), out result);
            return result;
        }

        public decimal GetDecimal(string sKey, decimal nDefault = 0m)
        {
            decimal result = nDefault;
            if (oInsideParams.ContainsKey(sKey))
                Decimal.TryParse(oInsideParams[sKey].ToString().Trim(), out result);
            return result;
        }

        public DateTime GetDateTime(string sKey)
        {
            return GetDateTime(sKey, DateTime.Now);
        }

        public DateTime GetDateTime(string sKey, DateTime dtDefault)
        {
            DateTime result = dtDefault;
            if (oInsideParams.ContainsKey(sKey))
            {
                string s0 = oInsideParams[sKey].ToString().Trim().Replace("：", ":");
                DateTime.TryParse(s0, out result);
            }
            return result;
        }

        public DateTimeOffset GetDateTimeOffset(string sKey)
        {
            return GetDateTimeOffset(sKey, DateTimeOffset.Now);
        }

        public DateTimeOffset GetDateTimeOffset(string sKey, DateTimeOffset dtDefault)
        {
            DateTimeOffset result = dtDefault;
            if (oInsideParams.ContainsKey(sKey))
            {
                string s0 = oInsideParams[sKey].ToString().Trim().Replace("：", ":");
                DateTimeOffset.TryParse(s0, out result);
            }
            return result;
        }

        public Guid GetGuid(string sKey)
        {
            return GetGuid(sKey, Guid.Empty);
        }

        public Guid GetGuid(string sKey, Guid oDefault)
        {
            Guid result = oDefault;
            if (oInsideParams.ContainsKey(sKey))
                Guid.TryParse(oInsideParams[sKey].ToString().Trim(), out result);
            return result;
        }

        public bool GetBoolean(string sKey, bool bDefault = false)
        {
            bool result = bDefault;
            if (oInsideParams.ContainsKey(sKey))
                Boolean.TryParse(oInsideParams[sKey].ToString().Trim(), out result);
            return result;
        }

        #endregion


    }
}