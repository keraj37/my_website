using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using AS3TOCS;

namespace AS3TOCS
{
    class Program
    {
        private static AS3TOCSConverter converter;
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Error: AS3 file path was not given.");
                return;
            }

            converter = new AS3TOCSConverter();
            string result = converter.Convert(args[0]);

            if (args.Length > 1)
            {
                SaveFile(args[1], result);
            }
            else
            {
                Console.Write(result);
            }
        }

        private static void SaveFile(string path, string result)
        {
            File.WriteAllText(path, result);
        }
    }
}
