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

            // read .xml file and save data to memory
            XMLReader reader = new XMLReader(@"C:\Users\kagrela\Desktop\przykladowa mapa\seg_Klingspor_100006-06d6_AGV_Z1_AGV_Forklift_level0_ver11.xml");
            reader.ReadFile();

            // save to .txt
            TextWriter output = new StreamWriter(@"C:\Users\kagrela\Desktop\Output.txt");
            output.Write(reader.metCreateDataString());
            output.Close();

            // open .txt file
            Process.Start("notepad.exe", @"C:\Users\kagrela\Desktop\Output.txt");

        }
    }

}
