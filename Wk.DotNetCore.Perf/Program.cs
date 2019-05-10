using System;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace Wk.DotNetCore.Perf
{
    public  class Program
    {
         static void Main(string[] args)
        {
            Console.WriteLine("Starting the work....");

            BenchmarkRunner.Run<BenchMarkTest>();
          
            Console.ReadLine();
        }
    }


    [SimpleJob(launchCount: 10)]
    [RPlotExporter, RankColumn]
    public class BenchMarkTest
    {
        Suspect row = new Suspect();

        public BenchMarkTest()
        {
            row.Set("StringFieldKey", "somelongandbigvalue");
            row.Set("BoolFieldKey", (bool?)true);
            row.Set("DecimalFiledKey", (decimal?)12.34m);
            row.Set("DateFieldKey", (DateTime?)DateTime.UtcNow);
            row.Set("NullFieldKey", (decimal?)null);           
        }

        [Benchmark]
        public void GetOrDefaultSlow()
        {
            var t = row.GetOrDefaultSlow<decimal?>("DecimalFiledKey");
        }
    }
}
