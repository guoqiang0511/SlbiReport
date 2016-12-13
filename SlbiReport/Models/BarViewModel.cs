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

        public List<string> XAxisData { get; set; }
        
        /// <summary>
        /// 图表数据
        /// </summary>
        public List<string> SeriesData1 { get; set; }

        public List<string> SeriesData2 { get; set; }

        public List<string> SeriesData3 { get; set; }
    }
}