using PCPSolver.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace PCPSolver
{
    class Program
    {
        static void Main(string[] args)
        {
            Blocks blocks = LoadBlocks();

            Classes.PCPSolver.Process(blocks);

            Console.ReadKey();
        }

        static Blocks LoadBlocks()
        {
            string path = string.Empty;
            do
            {
                Console.WriteLine("Input file name: ");

                string file = Console.ReadLine();

                path = $"../../../Instances/{file}.json";
            }
            while (!File.Exists(path));

            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Blocks));
            Blocks blocks = null;
           
            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                blocks = (Blocks)jsonFormatter.ReadObject(fileStream);
            } 

            return blocks;
        }
    }
}
