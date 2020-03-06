using System;
using System.Threading.Tasks;

namespace Multipass
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var ares = DoOne(10);
            Console.Write("Resultado ?");
            Console.Write(ares.Result);

            Console.Read();

        }

        
        static async Task<int> DoOne(int param)
        {
            var res = (param * 21) ; 
            await Task.Delay(5000);
            return res;
        }
    }
}
