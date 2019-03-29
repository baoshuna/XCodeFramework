using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TypeOf1
{
    class Program
    {
        static void Main(string[] args)
        {
            MethodInfo method = typeof(string).GetMethod("Copy");
            Type t = method.ReturnType.GetInterface(typeof(IEnumerable<>).Name);
            if (t != null)
                Console.WriteLine(t);
            else
                Console.WriteLine("The return type is not IEnumerable<T>.");
            Console.ReadKey();
        }
    }
}
