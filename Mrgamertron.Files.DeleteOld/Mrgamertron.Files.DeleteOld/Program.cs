using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Mrgamertron.Files.DeleteOld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Number of command line parameters = {0}",
             args.Length);
            for (int i = 0; i < args.Length; i++)
            {
                Console.WriteLine("Arg[{0}] = [{1}]", i, args[i]);
            }
            
            //aufgabe 3
            //aufruf über cmd mit
            //C:\Users\nick.arp\Desktop\Test.git\Mrgamertron.Files.DeleteOld\Mrgamer.DeleteOld\bin\Debug>Mrgamertron.Files.DeleteOld.exe C:\temp\nick 30 /i=name
            //legt automatisch ein task an, dass beim start läuft

            //alte dateien, die älter sind als eine woche löschen
            //rekursiv ordner durchgehen
            //leere ordner auch löschen
            //ausgeben der gelöschten dateien auf der konsole
            try
            {
                int days = Convert.ToInt32(args[1]);
                DeleteOldFilesInsFolders(args[0], days);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }

        public static void DeleteOldFilesInsFolders(string path, int days)
        {

            if (Directory.Exists(path))
            {
                Console.WriteLine("Checking: "+ path);

                var parentDirectory = new DirectoryInfo(path);

                foreach (FileInfo f in parentDirectory.GetFiles())
                {
                    

                    if (f.LastAccessTime < DateTime.Now.AddDays(-days))
                    {
                        Console.WriteLine("Deleting file: " + f.Name);
                        f.Delete();
                    }
                }

                foreach (DirectoryInfo d in parentDirectory.GetDirectories())
                {
                    DeleteOldFilesInsFolders(d.FullName, days);
                    if (Directory.GetFiles(d.FullName).Length < 1)
                    {
                        try
                        {
                            Console.WriteLine("Deleting folder:"+ d.FullName);
                            Directory.Delete(d.FullName);
                        }
                        catch (IOException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }

                }
            }
            else
            {
                Console.WriteLine(path+ " does not exist");
            }
        }

    }
}
