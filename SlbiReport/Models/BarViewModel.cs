using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlbiReport.Models
{
    public class BarViewModel
    {
        /// <summary>
        /// 图例数据
        /// </summary>
        public String Title { get; set; }

        public String SubTitle { get; set; }

        public List<string> LegendData { get; set; }

        public List<string> AxisData { get; set; }
        
        /// <summary>
        /// 图表数据
        /// </summary>
        public List<string> SeriesData1 { get; set; }

        public List<string> SeriesData2 { get; set; }

        public List<string> SeriesData3 { get; set; }

        public string SeriesName1 { get; set; }

        public string SeriesName2 { get; set; }

        public string SeriesName3 { get; set; }

        public string Url { get; set; }

        public string BarSelectName { get; set; }

        public string BarSelectValue1 { get; set; }
        public string BarSelectValue2 { get; set; }
    }
}