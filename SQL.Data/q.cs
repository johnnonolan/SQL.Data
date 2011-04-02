using System;

namespace SQL.Data
{
    public class q
    {
        public static SelectOperator SELECT = new SelectOperator();
        public static dynamic FROM= new SqlStatementFragment("FROM");

        public static string ConnectionString { get;set; }
    }
}   