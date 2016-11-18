using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Models
{
    class Header
    {
        public string FileTyp { get; set; }
        public string Prozessmodul { get; set; }
        public string Losnummer { get; set; }
        public int Slotnummer { get; set; }
        public string ProcesID { get; set; }
        public DateTime DateAndTime { get; set; }
        public string FileName { get; internal set; }
    }
}
