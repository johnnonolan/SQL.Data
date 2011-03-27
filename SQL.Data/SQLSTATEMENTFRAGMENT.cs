using System;
using System.Collections.Generic;
using System.Dynamic;

namespace SQL.Data
{
    public class SQLSTATEMENTFRAGMENT : DynamicObject
    {
        protected  string StatementFragement ="";
        protected SQLSTATEMENTFRAGMENT()
        {
                
        }
        public SQLSTATEMENTFRAGMENT(string statementFragement)
        {
            StatementFragement = statementFragement;
        }

        public override string ToString()
        {
            return  StatementFragement;
        }

        public static SQLSTATEMENTFRAGMENT operator * (SQLSTATEMENTFRAGMENT op1, SQLSTATEMENTFRAGMENT op2)
        {
            return new SQLSTATEMENTFRAGMENT(op1+" * "+op2);
        }


        public static SQLSTATEMENTFRAGMENT operator ==(SQLSTATEMENTFRAGMENT op1, dynamic op2)
        {
            return new SQLSTATEMENTFRAGMENT(op1 + " = " + op2);
        }



        public static SQLSTATEMENTFRAGMENT operator !=(SQLSTATEMENTFRAGMENT op1, dynamic op2)
        {
            return new SQLSTATEMENTFRAGMENT(op1 + " <> " + op2);
        }

        // The inner dictionary.
        protected Dictionary<string, object> innerDictionary
            = new Dictionary<string, object>();

        // If you try to get a value of a property 
        // not defined in the class, this method is called.
        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            string name = binder.Name.ToLower();

            if (!innerDictionary.TryGetValue(name, out result))
            {
                result = new SQLSTATEMENTFRAGMENT(ToString()+' '+ binder.Name);
                return true;
            }
            return innerDictionary.TryGetValue(name, out result);
        }


        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            innerDictionary[binder.Name.ToLower()] = value;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var predicateMember = "";
            foreach (var o in args)
            {
                if (predicateMember != "")
                    predicateMember += ",";
                predicateMember += o;

            }
            result = new SQLSTATEMENTFRAGMENT(String.Format("{0} {1} ({2})",StatementFragement,binder.Name,predicateMember));
            return true;

        }
    }
}