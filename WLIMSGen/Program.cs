using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLIMSGen
{
    class Program
    {
        static void Main(string[] args)
        {
            string a = "";
            var asp = a.Split('\t');
            for(int i = 0 ; i < asp.Length ; i++)
            {
                Console.WriteLine($"{{    field:'filed{i}',    title:'{asp[i]}'    }},");
            }

            Console.WriteLine();
            Console.WriteLine();
            for(int k = 1; k <= 6; k++)
            {
                Console.WriteLine("{");
                for (int i = 0; i < asp.Length; i++)
                {
                    Console.WriteLine($"    'filed{i}':'{asp[i]}{k}'" +(i==asp.Length-1?"":","));
                }
                Console.WriteLine("}");

            }

        }
    }
}
