using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlbiReport.Models
{
    public class SelectColumn
    {
        public string label { get; set; }

        public string valueField { get; set; }

        public string textField { get; set; }

        public string value { get; set; }

        public string labelPosition { get; set; }

        public string width { get; set; }

        public Boolean  multiple { get; set; }


    }
}