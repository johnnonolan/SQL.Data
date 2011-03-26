namespace SQL.Data.Specifications
{
    public class FROM : SQLSTATEMENTPART
    {
        public static SQLSTATEMENTPART users()
        {
            
            return new SQLSTATEMENTPART("FROM"+" USERS");
        }

        public static new string ToString()
        {
            return "FROM";
        }

    }
}