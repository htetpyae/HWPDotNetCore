// See https://aka.ms/new-console-template for more information
using HWPDotNetCore.ConsoleApp;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Channels;
using System.Xml.Linq;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");

        //Console.ReadLine();
        //hhahhh


        // AdoDotNetExample ad = new AdoDotNetExample();
        //ad.Create();
        // ad.Edit();
        // ad.Update();
        //ad.Delete();

        //DapperExample de = new DapperExample();
        // de.Read();
        // de.Create("c#","slh","ccc");
        //de.Delete(13);
        //de.Update(3, "java", "hh", "abak");
        //de.Edit(4);
        //de.Edit(2);

        EFCoreExample ef = new EFCoreExample();
        // ef.Read();
        //ef.Create("c+","hhh","hfas");
        //ef.Edit(4);
       // ef.Update(4, "aaa", "bbb", "ccc");
        ef.Delete(5);
       Console.ReadKey();
    }
}