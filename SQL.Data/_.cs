namespace SQL.Data
{
    public class _
    {
        public static dynamic SELECT = new SqlStatementFragment("SELECT");
        public static dynamic FROM= new SqlStatementFragment("FROM");
        public static dynamic INSERT = new SqlStatementFragment("INSERT");
        public static dynamic UPDATE = new SqlStatementFragment("UPDATE");
        public static string ConnectionString { get;set; }
    }
}   