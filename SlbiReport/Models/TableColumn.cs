﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SlbiReport.Models
{
    public class TableColumn
    {
        public string field { get; set; }

        public string title { get; set; }

        public string width { get; set; }

        public Boolean frozen { get; set; }

        public string frozenColumns { get; set; }

        public string columns { get; set; }
    }
}