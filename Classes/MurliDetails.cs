using System.Collections.Generic;

namespace MurliAnveshan.Classes
{
    public class AvyaktMurliDetails : MurliDetailsBase
    {
        #region Public Constructors

        public AvyaktMurliDetails() : base()
        {
        }

        #endregion Public Constructors
    }

    public abstract class MurliDetailsBase
    {
        #region Public Constructors

        public MurliDetailsBase()
        {
            MurliLines = new List<string>();
        }

        #endregion Public Constructors

        #region Public Properties

        public string FileName { get; set; }
        public string MurliDate { get; set; }

        public List<string> MurliLines { get; set; }
        public string MurliTitle { get; set; }
        public string SearchTerm { get; set; }

        #endregion Public Properties

    }
    public class SakarMurliDetails : MurliDetailsBase
    {
        #region Public Constructors

        public SakarMurliDetails() : base()
        {
            Questions = new List<string>();
            Answers = new List<string>();
            DharnaPoints = new List<string>();
            Blessing = new List<string>();
        }

        #endregion Public Constructors


        #region Public Properties

        public List<string> Answers { get; set; }
        //Varadhan
        public List<string> Blessing { get; set; }
        public List<string> DharnaPoints { get; set; }
        public List<string> Questions { get; set; }
        public string Slogan { get; set; }

        #endregion Public Properties

    }



    /*
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
    */
}
