using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;


using System.Linq;
using System.Xml;
using System.Xml.Linq;
using XMLReader_Console_App.Parent_Nodes_Classes;
using System.Data.SqlTypes;

namespace XMLReader_Console_App
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Beginning");

            // read .xml file and save data to memory
            string loc = @"C:\Users\kagrela\source\repos\XMLReader Console App\XMLReader Console App\XML FIles";
            string name = @"seg_Klingspor_zmod.xml";
            string path = loc + @"\" + name;

            
            XMLReader reader = new XMLReader(path);
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
