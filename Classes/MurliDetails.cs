
using System;
using System.Collections.Generic;
using System.Drawing;

namespace MurliAnveshan.Classes
{
    public class MurliDetails
    {
        public MurliDetails()
        {
            MurliLines = new List<string>();
        }

        public string MurliDate { get; set; }

        public string MurliTitle { get; set; }

        public string FileName { get; set; }

        public int PageNumber { get; set; }
        
        public int LineNumber { get; set; }

        public List<string> MurliLines { get; set; }

        public string SearchTerm { get; set; }

    }



    public class MurliDetailsForResultCard
    {
        public string MurliTitle { get; set; }

        public string MurliLines { get; set; }

        public string MurliDate { get; set; }

        public string FileName { get; set; }

        public int PageNumber { get; set; }

        public string SearchTerm { get; set; }

        public string SearchLocation { get; set; }

        public int Count { get; set; }

        public bool CountVisiblity { get; set; }

        public bool MurliTextVisiblity { get; set; }

        //public ResultCardSizeEnum ResultCardSize { get; set; }

        public bool IsFavorite { get; set; }

        public Size ResultCardActualSize { get; set; }

    }
}
