namespace SQL.Data
{
    public class SELECTOPERATOR : SQLSTATEMENTFRAGMENT
    {
        internal SELECTOPERATOR()
        {
            StatementFragement = "SELECT";
        }

        public override string ToString()
        {
            return StatementFragement;
        }
    }
}