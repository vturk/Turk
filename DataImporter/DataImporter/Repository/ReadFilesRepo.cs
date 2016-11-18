using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataImporter.Repository
{
    class ReadFilesRepo
    {
        IRepo repo;

        public ReadFilesRepo(string extension, string pathname, DateTime dateFrom)
        {
            this.repo = new ReadFilesImpl(extension, pathname, dateFrom);
        }


    }
}
