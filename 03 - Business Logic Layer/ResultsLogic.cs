using System.Collections.Generic;

namespace ThirdProject
{
    public class ResultsLogic : BaseLogic
    {
        public void AddResults(List<string> paths, int searchID)
        {
            foreach (string item in paths)
            {
                DB.Results.Add(new Result { SearchID = searchID, File_Path = item });
            }
            DB.SaveChanges();
            
            
        }
    }
}
