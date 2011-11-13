using System.Linq;
using System;

namespace SQL.Data
{
    public class _ : SqlStatementFragment
    {
        // ReSharper disable InconsistentNaming
        public static dynamic SELECT = new SqlStatementFragment("SELECT");

        public static dynamic FROM= new SqlStatementFragment("FROM");
        public static dynamic INSERT = new SqlStatementFragment("INSERT");
        public static dynamic UPDATE = new SqlStatementFragment("UPDATE");
        public static dynamic DELETE = new SqlStatementFragment("DELETE");
        public static dynamic Select(params string[] x)
        {
            if (x.Length ==0)
                return new SqlStatementFragment("SELECT");
            else
            {
                string joinedSequence = String.Join(",", x);
                return new SqlStatementFragment(String.Format("SELECT {0}", joinedSequence));
            }

        }
        // ReSharper restore InconsistentNaming
        public static string ConnectionString { get;set; }
    }
}   