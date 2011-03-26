namespace SQL.Data.Specifications
{
    public class SQLSTATEMENTPART
    {
        protected  string _statementPart ="";

        protected SQLSTATEMENTPART()
        {
                
        }
        public SQLSTATEMENTPART(string statementPart)
        {
            _statementPart = statementPart;
        }

        public override string ToString()
        {
            return _statementPart;
        }

        public static SQLSTATEMENTPART operator * (SQLSTATEMENTPART op1, SQLSTATEMENTPART op2)
        {
            return new SQLSTATEMENTPART(op1+" * "+op2);
        }
    }
}