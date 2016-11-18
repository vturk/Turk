using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataImporter.Config;

namespace DataImporter.Repository
{
    class ReadFilesImpl : IRepo
    {
        string _ext;
        public string PathName { get; set; }
        public string Extension { get { return string.Format("*.{0}", _ext); } set { _ext = value; } }
        public DateTime DateFrom { get; set; }

        public ReadFilesImpl(string extension, string pathname, DateTime dateFrom)
        {
            this.PathName = pathname;
            this.Extension = extension;
            this.DateFrom = dateFrom;
            ProcessDirectory(PathName);
        }
        

        // Process all files in the directory passed in, recurse on any directories 
        // that are found, and process the files they contain.
        public void ProcessDirectory(string targetDirectory)
        {
            // Process the list of files found in the directory.
            string[] fileEntries = Directory.GetFiles(targetDirectory, Extension, SearchOption.TopDirectoryOnly);

            foreach (string fileName in fileEntries)
            {
                ParseLinesToRawModel(fileName);
            }

            // Recurse into subdirectories of this directory.
            foreach (string subdirectory in Directory.GetDirectories(targetDirectory).Reverse())
            {
                try
                {
                    ProcessDirectory(subdirectory);
                }
                catch (Exception ex)
                {
                    //lblError.Text = ex.Message;
                }
            }

        }


        public void ParseLinesToRawModel(string path)
        {
            StreamReader rdr = new StreamReader(path);
            string line;
            while ((line = rdr.ReadLine()) != null)
            {
                if (line.Contains(ParseRules.HistoricalDataLogger))
                {
                    ProcessHeader(line);
                }
                //ParseLinesToRawModel()
                //Debug.WriteLine(line);
            }

            rdr.Close();
        }

        private void ProcessHeader(string line)
        {
            try
            {
                // Here goes the logic: 
                // 1. We are looking for end of path (\ character) -> Filename is everything after that
                // 2. Split line by . separator and all other data is splitted in array
                // 3. Extract file type from position 0 - depending if file has std or process in filename
                // 4. Rest is only formatting data
                int indexOfPathEnd = line.LastIndexOf(ParseRules.L1_0PathEnd) + 1;
                string fileName = line.Substring(indexOfPathEnd, line.Length - indexOfPathEnd);

                string[] splitByDot = line.Split('.');
                DateTime date = GenerateDateTime(splitByDot[3], splitByDot[4]);
                

                if (line.ToLower().Contains("std"))
                {
                    string[] splitProcessAndFileType = splitByDot[0].Split('\\');
                    string[] std_pm = splitProcessAndFileType[2].Split('_');
                    string std = std_pm[0];
                    string pm = std_pm[1];
                }
                else if (line.ToLower().Contains("process"))
                {
                    string[] splitProcessAndFileType = splitByDot[0].Split('\\');
                    string fileType = splitProcessAndFileType[1];
                    string pm = splitProcessAndFileType[2];
                }
                else
                    throw new Exception("ProcessFileName method -> File structure is not recognized:");

            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
            }
            
        }

        private DateTime GenerateDateTime(string dateString, string timeString)
        {
            string[] dateSplitted = dateString.Split('-');
            string[] timeSplitted = timeString.Split('-');

            return new DateTime(int.Parse(dateSplitted[2]), int.Parse(dateSplitted[0]), int.Parse(dateSplitted[1]),
                                int.Parse(timeSplitted[0]), int.Parse(timeSplitted[1]), int.Parse(timeSplitted[2]));
        }

      
    }
}
