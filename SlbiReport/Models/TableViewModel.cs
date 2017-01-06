using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlbiReport.Models
{
    public class TableViewModel
    {
        /// <summary>
        /// 图例数据
        /// </summary>
        public String Title { get; set; }

        public String SubTitle { get; set; }

        public List<TableColumn> Column { get; set; }

        public string FrozenColumns { get; set; }

        public string Columns { get; set; }

        public string Url { get; set; }

        public string ColList { get; set; }
    }
}