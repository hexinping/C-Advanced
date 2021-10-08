#define BUG
using System;
using System.Collections.Generic;
using System.Diagnostics;
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



            //Attribute 测试
            TestAttribute();

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


        /*
            Attribute是一种可由用户自有定义的修饰符（Modifier），可以用来修饰各种需要被修饰的目标，修饰符（比如private、public、static、override、virtual等等）是C#语言本身的关键字。
            简单地说，Attribute就是一种“附着物”——就像牡蛎吸附在船底或礁石上一样。
            这些附着物的作用是为它们的附着体追加上一些额外的信息（这些信息保存在附着物的体内）——比如“这个类是我写的”或者“这个函数以前出过问题”

            https://blog.csdn.net/xiaouncle/article/details/70216951
            https://blog.csdn.net/xiaouncle/article/details/70229119

            Attribute本质上就是一个类，它附着在目标对象上最终实例化
            Attribute并不是修饰符，而是一个有着独特实例化形式的类

            attribute
        https://blog.csdn.net/aladdinty/article/details/3717572
        C#获取某个Attribute标记过的所有类  ==> 获取类名之后，可以通过反射Activator.CreateInstance(type) 创建实例
            https://blog.csdn.net/u014370148/article/details/88416326

        C# Attribute使用技巧 遍历特性类 创建响应事件
        https://blog.csdn.net/u010294054/article/details/89442390?utm_medium=distribute.pc_relevant.none-task-blog-2%7Edefault%7ECTRLIST%7Edefault-1.no_search_link&depth_1-utm_source=distribute.pc_relevant.none-task-blog-2%7Edefault%7ECTRLIST%7Edefault-1.no_search_link

         */

        static void TestAttribute()
        {
            PrintA();
            PrintB(); //PrintB的条件是定义了NOBUG才会生效，感觉编译后这句代码会被优化，等于没有执行
        }

        //Conditional 是C#本身的特性，意思就是方法PrintA，只有在定义了“BUG”才会生效
        [Conditional("BUG")]
        static void PrintA()
        {
            Console.WriteLine("PrintA==========");
        }

        [Conditional("NOBUG")]
        static void PrintB()
        {
            Console.WriteLine("PrintB==========");
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
