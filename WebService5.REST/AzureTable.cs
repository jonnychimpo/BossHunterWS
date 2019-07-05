using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebService5.REST
{
    public class AzureTable
    {
        public string TableName;

        public AzureTable(string tablename)
        {
            TableName = tablename;
        }
    }
}