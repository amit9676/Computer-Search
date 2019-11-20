using System;

namespace ThirdProject
{
    public class BaseLogic : IDisposable
    {
        protected Search_HistoryEntities DB = new Search_HistoryEntities();

        public void Dispose()
        {
            DB.Dispose();
        }
    }
}
