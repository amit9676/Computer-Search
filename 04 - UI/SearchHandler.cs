using System;

namespace ThirdProject
{
    class SearchHandler
    {
        public void Start()
        {
            Displays.DisplayMenu();
            Actions action = new Actions();
            while (true)
            {

                string input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        {
                            action.CallToFilesTotal();
                            break;
                        }
                    case "2":
                        {
                            action.CallToFilesPresice();
                            break;
                        }
                    case "3":
                        {
                            Environment.Exit(9);
                            break;
                        }
                    default:
                        {
                            Displays.IncorrectChoice();
                            continue;
                        }
                }
                ClearConsole();
            }
        }

        public void ClearConsole()
        {
            
            Console.ReadKey();
            Console.Clear();
            Start();
        }
    }
}
