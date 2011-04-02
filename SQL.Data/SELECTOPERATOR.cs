namespace SQL.Data
{
    public class SelectOperator : SqlStatementFragment
    {
        internal SelectOperator()
        {
            StatementFragement = "SELECT";
        }

        public override string ToString()
        {
            return StatementFragement;
        }
    }
}