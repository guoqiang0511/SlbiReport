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
        
        public List<BarSeriesModel> Series { get; set; }

        public string LegendDataStr { get; set; }
        
        public string SeriesStr { get; set; }

        public string AxisDataStr { get; set; }

        public string Url { get; set; }

    }
}