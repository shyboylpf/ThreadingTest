using Light.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace postGreSqlTest
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContextConfiguration.SetConfigFilePath("Config/lightdata.json");
            DataContext context = new DataContext("postgresql");
            //var item = context.Query<mattressdatacollectlist>().Where(x => x.collecttype == 1).First();
            //var sql = "SELECT * FROM mattressdatacollectlist;";
            //var executor = context.CreateSqlStringExecutor(sql);
            //var ret = executor.ExecuteNonQuery();
            var item = context.Query<mattressdatacollectlist>().Where(x => x.collecttype == 1).First();
            Console.WriteLine(item.collecttype);
        }
    }
    public class MyDataContext : DataContext
    {
        public MyDataContext() : base("postgresql")
        {

        }
    }
}
