using System;

namespace ThirdProject
{
    class SearchResultEvent : EventArgs
    {
        public string FilePath { get; set; }
        public int FileIndex { get; set; }

        public SearchResultEvent(string filePath, int fileIndex)
        {
            FilePath = filePath;
            FileIndex = fileIndex;
        }
    }
}

