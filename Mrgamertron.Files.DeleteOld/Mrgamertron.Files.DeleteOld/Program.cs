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
            string path = @"C:\temp\nick";
            string myTime = System.DateTime.Now.ToShortDateString();
            
                         
            //alte dateien, die älter sind als eine woche löschen
            //rekursiv ordner durchgehen
            //leere ordner auch löschen
            //ausgeben der gelöschten dateien auf der konsole
            Console.WriteLine(myTime);
           DeleteOldFilesInsFolders(path);
            Console.ReadKey();


        }

        public static void DeleteOldFilesInsFolders(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                Console.WriteLine("Exists");

                DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(path);

                foreach (System.IO.FileInfo f in ParentDirectory.GetFiles())
                {
                    Console.WriteLine("File: " + f.Name);

                    if (f.LastAccessTime < System.DateTime.Now.AddMinutes(-1))
                    {
                        f.Delete();
                    }
                }

                foreach (System.IO.DirectoryInfo d in ParentDirectory.GetDirectories())
                {
                    Console.WriteLine("Folder: " + d.Name);
                    DeleteOldFilesInsFolders(d.FullName);
                }
            }
            else
            {
                Console.WriteLine("Does not Exist");
            }
        }

    }
}
