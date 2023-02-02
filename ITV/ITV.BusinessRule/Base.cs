using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITV.DataAccess.ITVDatabase;

namespace ITV.BusinessRule
{
    public class Base
    {
        //connection string
        protected ItVdB db = new ItVdB("Database=ITVDB;Data Source=192.168.200.212;User Id=root;Password=itv;DbLinqProvider=MySql;DbLinqConnectionType=MySql.Data.MySqlClient.MySqlConnection, MySql.Data");

        public Base()
        {
        }

        ~Base()
        {
        }
    }
}
