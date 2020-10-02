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


            List<Segment> l1 = new List<Segment>();
            reader.segmentData.GetList(l1);

            Segment s1 = l1[0];
            Console.WriteLine("s1: " + s1.Id);

            Console.WriteLine("______  fin  ______");
        }
    }

}
