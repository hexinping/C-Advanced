#define BUG
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Advance
{
    [AttributeUsage(AttributeTargets.Class |
        AttributeTargets.Constructor |
       AttributeTargets.Field |
        AttributeTargets.Method |
        AttributeTargets.Property,
AllowMultiple = true)]
    public class DeBugInfoAttribute : System.Attribute
    { 
        
    }

    [AttributeUsage(AttributeTargets.Class |
    AttributeTargets.Constructor |
   AttributeTargets.Field |
    AttributeTargets.Method |
    AttributeTargets.Property,
AllowMultiple = true)]
    public class HxpTestAttribute : System.Attribute
    {

    }

    [HxpTest]
    public class HxpGame
    { 
    
    }
    public class AttributeTest1
    {
        public int age;
        public string name;
        private int money;

        public void PrintAge()
        { 
        
        }

        private void PrintAge1()
        {

        }

        private int _atk;
        public int Atk { get; set; }

        [DeBugInfo]
        public int attributeTest;

        [DeBugInfo]
        public void PrintattributeTest()
        { 
        
        }
    }
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

            //struct内存自定义布局
            //https://blog.csdn.net/bigpudding24/article/details/50727792
            TestStructMemLayout();

            Console.ReadLine();
        }

        [StructLayout(LayoutKind.Explicit)]
        public struct TestMem {
            [FieldOffset(0)]
            public bool i;  //1Byte
            [FieldOffset(1)]
            public double c;//8byte
            [FieldOffset(9)]
            public bool b;  //1byte

        }
        static void TestStructMemLayout()
        {
            TestMem tMem = new TestMem();
            tMem.i = true;
            tMem.c = 4546;
            tMem.b = false;

            //Console.WriteLine($"tMem ====={tMem.i} {tMem.c} {tMem.b} size: {Marshal.SizeOf(tMem)}");

            unsafe
            {
                Console.WriteLine($"tMem ====={tMem.i} {tMem.c} {tMem.b} size: {sizeof(TestMem)}");
            }
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
                
                
                添加自定义Attribute后 其实只要通过反射就能拿到对应的信息（通过发射获得），然后进行不同处理，
                https://blog.csdn.net/FantasiaX/article/details/1627694
                https://blog.csdn.net/FantasiaX/article/details/1636913
                
                https://blog.csdn.net/aladdinty/article/details/3717572

            //获取程序集里的类型
        http://m138640392501.lofter.com/post/1cc5e727_8d26300
         */

        static void TestAttribute()
        {
            PrintA();
            PrintB(); //PrintB的条件是定义了NOBUG才会生效，感觉编译后这句代码会被优化，等于没有执行

            //获取一个类下的所有public属性 ==》  FieldInfo[] fields = dst.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
            TestAttribute1();

            //一个类下的所有具有某个arribute的字段名
            TestAttribute2();

            //当前程序集中具有某个arribute的类，创建对应的实例以及调用方法
            TestAttribute3();
        }

        static void TestAttribute3()
        { 
        
        }
        static void TestAttribute2()
        {
            Console.WriteLine("TestAttribute2==========");
            AttributeTest1 t = new AttributeTest1();
            MethodInfo[] methodInfo = t.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo mInfo in methodInfo)
            {
                Attribute[] attributes = System.Attribute.GetCustomAttributes(mInfo, true);
                foreach (var item in attributes)
                {
                    if (item is DeBugInfoAttribute)
                    {
                        Console.WriteLine($"{mInfo.Name} 具有 DeBugInfoAttribute==========");
                    }
                }
            }

            FieldInfo[] fieldInfos = t.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                Attribute[] attributes = System.Attribute.GetCustomAttributes(fieldInfos[i], true);
                foreach (var item in attributes)
                {
                    if (item is DeBugInfoAttribute)
                    {
                        Console.WriteLine($"{fieldInfos[i].Name} 具有 DeBugInfoAttribute==========");
                    }
                }
            }


            //t.GetType().GetCustomAttribute(typeof(DeBugInfoAttribute), true); 得到一个自定义CustomAttribute
        }
        static void TestAttribute1()
        {
            Console.WriteLine("TestAttribute1==========");
            AttributeTest1 t = new AttributeTest1();
            //获取字段名
            FieldInfo[] fieldInfos = t.GetType().GetFields(BindingFlags.Instance|BindingFlags.Public| BindingFlags.NonPublic);
            for (int i = 0; i < fieldInfos.Length; i++)
            {
                Console.WriteLine($"Field : {fieldInfos[i].Name}");
            }

            //获取方法名，默认是返回所有public 方法，
            MethodInfo[] methodInfo = t.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic|BindingFlags.Instance | BindingFlags.DeclaredOnly);
            foreach (MethodInfo mInfo in methodInfo)
            {
                Console.WriteLine($"Method:  {mInfo.Name}");
            }
            //获取属性名，
            PropertyInfo[] propertyInfos= t.GetType().GetProperties(BindingFlags.Public | BindingFlags.DeclaredOnly | BindingFlags.Instance);
            foreach (PropertyInfo mInfo in propertyInfos)
            {
                Console.WriteLine($"Property:  {mInfo.Name}");
            }
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
