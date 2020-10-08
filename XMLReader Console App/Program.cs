using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

namespace XMLReader_Console_App
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            // read file
            XMLReader reader = new XMLReader(@"C:\Users\kagrela\Desktop\przykladowa mapa\mala mapa\seg_newAWM_ContinousTest2_20200602_Schwarzmueller_Diff_level0_ver3.xml");
            reader.XMLReader_Workflow();

            // save to .txt
            TextWriter output = new StreamWriter(@"C:\Users\kagrela\Desktop\Output.txt");
            output.Write(reader.metCreateDataString());
            output.Close();
            Process.Start("notepad.exe", @"C:\Users\kagrela\Desktop\Output.txt");


            //reader.segmentData.Segments;
        }
    }

}
