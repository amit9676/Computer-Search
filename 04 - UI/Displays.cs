using System;

namespace ThirdProject
{
    static class Displays
    {
        public static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Enter a file name to search." +
                "\n2. Enter a file name to search + parent directory to search in." +
                "\n3. Exit.");
        }

        public static void IncorrectChoice()
        {
            Console.WriteLine("Invalid input! please choose an option from 1 to 3.");
        }

        public static void DisplayEndSearch(int counter)
        {
            if (counter == 1)
                Console.WriteLine("Done. " + counter + " file was found. File added to Database. Press any key to return.");
            else if (counter == 0)
                Console.WriteLine("No files were found. Press any key to return.");
            else
                Console.WriteLine("Done. " + counter + " files were found. Files added to Database. Press any key to return.");
        }

        public static void DisplayResults(int fileIndex, string filePath)
        {
            Console.WriteLine(fileIndex + ". " + filePath);
        }
    }
}
