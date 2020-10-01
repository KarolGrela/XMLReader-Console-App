using System;

namespace XMLReader_Console_App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string version = "3", area = "XYZ", status = "dobry";
            FileInfo info = new FileInfo();
            info.Version = version;
            info.Production_area = area;
            info.Status = status;

            Console.WriteLine($"{info.Version}\n{info.Production_area}\n{info.Status}");
        }
    }
}
