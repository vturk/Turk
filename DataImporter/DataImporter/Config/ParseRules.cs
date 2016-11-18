using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Config
{
    static class ParseRules
    {
        // This properties are some kind of checkpoint
        // 1. Line rules
        public static string HistoricalDataLogger { get { return "HistoricalDataLogger file:"; } }
        /// <summary>
        /// Last index of should be sought
        /// </summary>
        public static string L1_0PathEnd { get { return @"\"; } }

    }
}
