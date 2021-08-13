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




            

            unsafe
            {
                int i = 5;
                SquarePtrParam(&i);
                Console.WriteLine($"i is {i}");
            }
           
            
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



        /*
         unsafe关键字可以修饰一个语句块，语句块内可以支持指针类型，有指针操作，无边界检查
         unsafe关键字还可以修饰函数，类等。被修饰的域将允许不安全代码。
        如图编写了一个unsafe的函数，传入参数有一个char*指针。
        另外，如果要获取变量的指针，需要放入fixed语句获取指针。fixed必须在unsafe内部使用，用于固定指针指向的变量，避免运行时环境将变量挪动位置
         */

        unsafe static void SquarePtrParam(int *p)
        {
            *p *= *p;
        }
         
    }
}


public class UnSafeTest 
{
    public unsafe void ChangeString(char * s, int len)
    {
        for (int i = 0; i < len; i++)
        {
            ++s[i];
        }

    }

    private unsafe void ButtonClick(object sender, EventArgs e)
    {
        string strs = "hehhdfsdf";
        fixed (char *str = strs)
        {
            ChangeString(str, strs.Length);
        }
    }
}
