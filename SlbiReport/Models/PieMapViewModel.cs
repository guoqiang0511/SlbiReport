using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlbiReport.Models
{
    public class PieMapViewModel
    {
  
        public String Title { get; set; }

        public String SubTitle { get; set; }

        public List<string> LegendData { get; set; }

        public String Url { get; set; }

        public String SelectName { get; set; }

        public String SelectValue { get; set; }
        /// <summary>
        /// 图表数据
        /// </summary>
        public List<VisitSource> SeriesData { get; set; }

        public String SeriesName { get; set; }
    }
}