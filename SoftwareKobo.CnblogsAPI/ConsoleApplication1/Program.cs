using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SoftwareKobo.CnblogsAPI.Service;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
var x=            BlogService.BodyAsync(41358741).Result;
            Console.WriteLine(x);
        }
    }
}
