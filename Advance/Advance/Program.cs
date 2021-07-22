using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Advance
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("hexinping");
            List<int> list = new List<int>(100);
            list.Add(1);

  
        
            
            
            Task<int> taskAsync= GetPageLengthAsync("https://www.baidu.com/");
            Console.WriteLine($"Length is {taskAsync.Result}");  //taskAsync需要解包下
            
         
            
            
            
            
            Console.ReadLine();
        }

        static async Task<int> GetPageLengthAsync(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                Task<string> f = client.GetStringAsync(url);
                int length = (await f).Length;
                return length;  //虽然返回int，但是函数声明返回值为Task<int> 会自动进行包装，
            }
        }
    }
}
