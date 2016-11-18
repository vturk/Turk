using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Repository
{
    interface IRepo
    {
        void ProcessDirectory(string targetDirectory);
        void ParseLinesToRawModel(string path);

    }
}
