using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpFTPClient
{
    class FTPListingDetail
    {
       
        public bool IsDirectory { get; set; }
        internal string Dir { get; set; }
        public long Size { get; set; }
        public string Permission { get; set; }
        public string Name { get; set; }
        public string FullPath { get; set; }
        public DateTime ModifiedTime { get; set; }
        
    }
}
