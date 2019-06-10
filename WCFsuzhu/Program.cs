using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
//Download by http://www.codesc.net
namespace WCFsuzhu
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("正在启动服务");
            ServiceHost sh = new ServiceHost(typeof(CoreDll.Service1));
            sh.Open();
            Console.WriteLine("启动服务成功！回车键退出。");
            Console.ReadLine();
            sh.Abort();
            sh.Close(); 
        }
    }
}
