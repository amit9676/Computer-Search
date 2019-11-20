using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;

namespace ThirdProject
{
    class Actions
    {
        public event EventHandler<SearchResultEvent> FileFound;
        private List<string> Allfiles = new List<string>() { }; //files for transfer to database
        private BLL_Link BllLink = new BLL_Link(); //connection for designated BLL link class
        private int Counter;

        private bool searchTrigger = false;
        
        private string GetName() //gets and checks the user's input of search key-word
        {
            Console.Clear();
            //searchTrigger = false;
            string SearchKey = "";
            char[] symbols = { '"', '*', '?', '|', '<', '>' };
            do
            {
                Console.Write("Enter file name to search: ");
                SearchKey = Console.ReadLine().ToLower().TrimStart();

                //Conditions for file input 
                if (SearchKey == "")
                { 
                    Console.WriteLine("File's name can not be empty.\n");
                    continue;
                }

                foreach (char item in symbols)
                {
                    if (SearchKey.Contains(item) || SearchKey.Contains('/') || SearchKey.Contains('\\') || SearchKey.Contains(':'))
                    {
                        Console.WriteLine("File name can not contain the symbols: < > :  \" ? | * \\ /\n");
                        SearchKey = "";
                        break;
                    }
                }//

            }
            while (SearchKey == "");
            return SearchKey;
        }

        public async void Cut()
        {
            await Task.Run(() =>
            {
                while (!Console.KeyAvailable)
                {
                    searchTrigger = false;
                }
                searchTrigger = true;
            });
        }
        

        public void CallToFilesTotal() // Scans the entire computer for the targeted files
        {
            string SearchKey = GetName();
            Console.WriteLine("\nsearching... press any key to stop the search \n" +
                            "(note, if search process is not finished, files will not be added to database.)\n");
            DriveInfo[] AllDrives = DriveInfo.GetDrives();

            Allfiles.Clear();
            Counter = 0;

            Cut();
            foreach (DriveInfo d in AllDrives) // Checks the drives on the computer
            {
                
                try
                {
                    FileFound = (sender, e) => {
                        Displays.DisplayResults(e.FileIndex, e.FilePath);
                        Allfiles.Add(e.FilePath);
                        };

                    GetFiles(d.ToString(), "*", SearchKey);
                }
                catch (IOException) { } // exception for unready drives
            }

            int SearchID = BllLink.AddSearchToDB(SearchKey, Counter); // adds search to database
            BllLink.AddResultsToDB(Allfiles, SearchID); // adds matching results to database

            Displays.DisplayEndSearch(Counter);
        }

        public void CallToFilesPresice() // Scans selected directory for the targeted files
        {
            string SearchKey = GetName();

            string Path = "";
            Allfiles.Clear();
            bool DirectoryTrigger = false;
            do
            {
                try
                {
                    Console.Write("Enter root directory to search in (format: \"C:\\Directory1\\Directory2\"): ");
                    Path = Console.ReadLine().TrimStart().TrimEnd();


                    //Conditions for path's input:
                    if (Path.Length == 0)
                        throw new EmptyPathException();
                    if (Path.All(p => p == '\\' || p == '/' || p == '.'))
                        throw new AllSymbolException();
                    if (Path.Contains(":") && !Path.Contains(":\\"))
                        Path = Path.Replace(":", ":\\");
                    if (Path.Contains("/"))
                        Path = Path.Replace("/", "");
                    //

                    if (Directory.Exists(Path))
                        Console.WriteLine("\nsearching... press any key to stop the search \n" +
                            "(note, if search process is not finished, files will not be added to database.)\n");
                    Cut();


                    FileFound = (sender, e) =>
                    {
                        Displays.DisplayResults(e.FileIndex, e.FilePath);
                        Allfiles.Add(e.FilePath);
                    };

                    Counter = 0;
                    GetFiles(Path, "*", SearchKey);
                    DirectoryTrigger = true;

                }
                //things that can go wrong during selected directory search
                catch (DirectoryNotFoundException) { Console.WriteLine("Directory was not found, Please enter another directory.\n"); }
                catch (ArgumentException ex) { Console.WriteLine(ex.Message + "\n"); }
                catch (EmptyPathException ex) { Console.WriteLine(ex.Message + "\n"); }
                catch (AllSymbolException ex) { Console.WriteLine(ex.Message + "\n"); }
                catch (NotSupportedException ex) { Console.WriteLine(ex.Message + "\n"); }
                catch (IOException ex) { Console.WriteLine(ex.Message + "\n"); }
                //
            }
            while (!DirectoryTrigger);

            int SearchID = BllLink.AddSearchToDB(SearchKey, Counter, Path); // adds search to database
            BllLink.AddResultsToDB(Allfiles, SearchID); // adds matching results to database

            Displays.DisplayEndSearch(Counter);

        }


        private void GetFiles(string path, string pattern, string searchKey) //searches the directories for targeted files
        {
            var File = new List<string>();

            try
            {
                if (searchTrigger)
                {
                    Console.WriteLine("search stopped, press any key to return.");
                    if (Console.KeyAvailable)
                    {
                        Console.ReadKey();
                    }
                    Console.ReadKey();
                    Console.Clear();
                    SearchHandler handler = new SearchHandler();
                    handler.Start();
                }
                
                //get all files directly within a certain directory and checking them for out search input
                // displaying matches in real time, and adding them to the list for transfer to database.
                File.AddRange(Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly));

                foreach (var item in File)
                {
                    if (Path.GetFileNameWithoutExtension(item).ToLower().Contains(searchKey.ToLower()))
                    {
                        Counter++;
                        FileFound?.Invoke(this, new SearchResultEvent(item, Counter));
                    }
                }
                //

                //performing the same action on every directoy in our path
                foreach (var directory in Directory.GetDirectories(path))
                    GetFiles(directory, pattern, searchKey);
                //
                
            }
            catch (UnauthorizedAccessException) { }
            catch (PathTooLongException) { }
        }
    }
}
