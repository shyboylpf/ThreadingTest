using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HS.DAL;
using PetaPoco;

namespace petapocoTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //DefaultConnectionDB.Factory = new entryString();
            var rows = AP_Equ_MattressRunStateChangeList.Single(2);
            Console.WriteLine(rows.changeDeviceRunState);
        }
    }

    public class entryString : DefaultConnectionDB.IFactory
    {
        public DefaultConnectionDB GetInstance()
        {
            DefaultConnectionDB db = new DefaultConnectionDB("server=123.57.0.0;database=Develop;uid=sa;pwd=encryptstring", "System.Data.SqlClient");
            return db;
        }
    }
}