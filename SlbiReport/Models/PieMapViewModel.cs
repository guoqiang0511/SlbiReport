using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlbiReport.Models
{
    public class PieMapViewModel
    {
        /// <summary>
        /// 图例数据
        /// </summary>
        public String Title { get; set; }

        public String SubTitle { get; set; }

        public List<string> LegendData { get; set; }

        public string Url { get; set; }

        public string PieMapSelectName { get; set; }

        public string PieMapSelectValue { get; set; }
        /// <summary>
        /// 图表数据
        /// </summary>
        public List<VisitSource> SeriesData { get; set; }
    }
}