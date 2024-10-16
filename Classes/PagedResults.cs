using System.Collections.Generic;

namespace MurliAnveshan.Classes
{
    public class PagedResults<T>
    {
        public int CurrentPage { get; set; }
        public int TotalHits { get; set; }  // Total results from the search
        public IEnumerable<T> Results { get; set; }  // Current page's results
    }
}
