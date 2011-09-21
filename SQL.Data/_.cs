namespace SQL.Data
{
    public class _
    {
        public static SelectOperator SELECT = new SelectOperator();
        public static dynamic FROM= new SqlStatementFragment("FROM");
        public static dynamic INSERT = new SqlStatementFragment("INSERT");

        public static string ConnectionString { get;set; }
    }
}   