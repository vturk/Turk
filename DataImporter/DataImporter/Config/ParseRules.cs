using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Config
{
    static class ParseRules
    {
        // This properties serve as markers
        // 1. Line rules
        public static string HistoricalDataLogger { get { return "HistoricalDataLogger file:"; } }
        
        public static string L1_0PathEnd { get { return @"\"; } }
        public static string WaferId { get { return "waferId"; } }
        public static string Equipment { get { return "Equipment:"; } }
    }
}
