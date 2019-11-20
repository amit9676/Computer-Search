using System;
using System.Linq;

namespace ThirdProject
{
    public class SearchesLogic : BaseLogic
    {
        public void AddSearches(string fileName, int counter, string path = null)
        {
            DB.Searches.Add(new Search { Search_text = fileName, Search_folder = path, Time_of_search = DateTime.Now, Number_of_Results = counter});
            DB.Searches.OrderByDescending(p => p.Time_of_search);
            DB.SaveChanges();
        }

        public int GetLastSearchID()
        {
            return DB.Searches.ToList()[DB.Searches.ToList().Count - 1].SearchID;
        }
    }
}
