using System.Collections.Generic;

namespace ThirdProject
{
    class BLL_Link // a class designated to connect the UI with the BLL, all functions that call to BLL are used here
    {
        public int AddSearchToDB(string name, int counter, string path = null)
        {
            using (SearchesLogic logic = new SearchesLogic())
            {
                logic.AddSearches(name, counter, path);
                return logic.GetLastSearchID();
            }
        }

        public void AddResultsToDB(List<string> allfiles, int SearchID)
        {
            using (ResultsLogic logic = new ResultsLogic())
            {
                logic.AddResults(allfiles, SearchID);
            }
        }
    }
}
