﻿using System;
using System.Reflection;
using System.Threading;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using FileManagerLibrary;
using System.Diagnostics;
using System.Text;

namespace FileManagerApp
{
    class Program
    {
         
        static void Main(string[] args)
        {
           
            while (true)
            {
                //Initiera valet med noll.
                int choice = 0;
                
                string filePath;

                Console.Clear();

                Console.Write("1. Add File \n2. Sort Words Alphabetically \n3. Search for a specific word \n4. View file \n5. Save File \n6. Exit Application \nChoice: ");
                
                // Check if input is a number and a correct choice
                if (int.TryParse(Console.ReadLine(), out int res) && res <= 6 && res > 0)
                {
                    choice = res;
                }                    

                // Om det är inte ett int så skickas ut ett felmeddelande för att det inte finns i alternativen.
                else
                {
                    Console.WriteLine("The choice does not exist!");
                    Thread.Sleep(2000);
                    continue;
                }

                Console.Clear();

                // Använda switch för att köra olika alternativ. 
                switch (choice)
                {
                    /*  
                        Det första alternativet är att ta emot file path i ReadFile funktionen.
                        Fångar exception om filen är inte txt fil.
                        Fångar exception om filen inte existerar.
                        Fångar exception om läggas till samma filens path.
                    */
                    case 1:
                        Console.WriteLine("Enter filepath: ");

                        try
                        {
                            FileManager.ReadFile(Console.ReadLine());
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message); Thread.Sleep(2000);
                        }
                        catch (FileNotFoundException e)
                        {
                            Console.WriteLine(e.Message); Thread.Sleep(2000);
                        }
                        catch(ArgumentException e)
                        {
                            Console.WriteLine(e.Message); Thread.Sleep(2000);
                        }

                        break;


                        /*
                         * Väljer den filen som vill sorteras.
                         * Om inte finns någon fil görs en break.
                         * Fånga exception om filen är tom.
                         */
                    case 2:
                        filePath = GetSelectedFile();
                        
                        if (filePath == null)
                            break;
                        try
                        {
                            FileManager.SortCollection(filePath);
                        }
                        catch (InvalidOperationException e)
                        {
                            Console.WriteLine(e.Message);
                            Thread.Sleep(2000);
                            continue;
                        }
                        
                        Console.WriteLine("Sorted!"); Thread.Sleep(1000);
                        break;

                        /*
                         * Söka efter det ordet som sökas i de filarna.
                         * skrivas ut totala hittade ord i de filarna och också vilket fil som har högst träffsäkethet och dess hittade ord.         
                         */
                    case 3:
                        Console.WriteLine("Specify a word to search for: ");
                        string word = Console.ReadLine();
                        
                        var result = FileManager.WordOccurrences(word);

                        if (result == null)
                        {
                            Console.WriteLine("One word only."); Thread.Sleep(1000);
                            break;
                        }
                            
                        Console.Clear();
                        Console.WriteLine($"Total Occurrences: {result[0]}");

                        if (FileManager.Files.Count > 1)
                            Console.WriteLine("File with highest accuracy: {0} -> {1}", Path.GetFileName(result[1]), result[2]);

                        Console.WriteLine("Press any key to return to menu");
                        Console.ReadKey();

                        break;

                        
                        /*
                            Väljs vilken fil som vill ses. Om filen redan är sorterad så kommer det att att visa sorterad version
                            annars kommer det att visa originell version.Om det finns ingenting att visa kommer det återgå till menyn.
                        */
                    case 4:
                        filePath = GetSelectedFile();

                        if (filePath == null)
                            break;

                        foreach (var item in FileManager.Files[filePath])
                        {
                            Console.Write(item + " ");
                        }

                        Console.WriteLine("\nPress any key to return to menu");
                        Console.ReadKey();

                        break;

                        /*
                          Spara filen i en modifierad fil. Om filen är sorterad kommer det att spara sorterad filen annars kommer
                          det att spara den originella filen. Om det finns ingenting att spara så kommer det att återgå till menyn. 
                        */                        
                    case 5:
                        filePath = GetSelectedFile();

                        if (filePath == null)
                            break;

                        FileManager.SaveFile(filePath);

                        break;

                        // Avsluta programmet
                    case 6:
                        Environment.Exit(0);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Prints all the 
        /// </summary>
        /// <returns></returns>
        private static string GetSelectedFile()
        {
            string[] filePath = new string[FileManager.Files.Count];
            int count = 1;
            foreach (var item in FileManager.Files)
            {
                filePath[count - 1] = item.Key;
                Console.WriteLine(count + ". " + Path.GetFileNameWithoutExtension(item.Key));
                count++;
            }

            Console.WriteLine("Enter choice: ");
            if (int.TryParse(Console.ReadLine(), out int res) && res <= filePath.Length && res > 0)
                return filePath[res - 1];

            else
                return null;
        }
    }
}
