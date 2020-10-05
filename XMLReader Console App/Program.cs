using System;
using System.Collections.Generic;

namespace XMLReader_Console_App
{
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            XMLReader reader = new XMLReader(@"C:\Users\kagrela\source\repos\XMLReader Console App\seg_newAWM_ContinousTest2_20200602_Schwarzmueller_Diff_level0_ver3.xml");
            reader.XMLReader_Workflow();

            Console.WriteLine(reader.fileInfo.DisplayData());

            Console.WriteLine("______  fin  ______");

            Console.WriteLine(reader.segmentData.Segments.Count);
        }
    }

}
