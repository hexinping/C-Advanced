

TODO
{

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

    练习
    {
    	一个类下的所有public属性 ==》  FieldInfo[] fields = dst.GetType().GetFields(BindingFlags.Instance | BindingFlags.Public);
		一个类下的所有具有某个arribute的属性
		当前程序集中具有某个arribute的类，创建对应的实例以及调用方法
	}
	

}