using System;
namespace DapperExtender.DapperExtention
{
    [System.AttributeUsage(AttributeTargets.Class)]
    public class QueryNameAttribute : Attribute
    {
        public string QueryName { get; set; }

        public QueryNameAttribute(string queryName)
        {
            QueryName = queryName;
        }
    }
   
}

