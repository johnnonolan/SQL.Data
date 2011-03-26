namespace SQL.Data.Specifications
{
    public class SELECTOPERATOR : SQLSTATEMENTPART
    {
        internal SELECTOPERATOR()
        {
            _statementPart = "SELECT";
        }

        public override string ToString()
        {
            return _statementPart;
        }
    }
}