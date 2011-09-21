using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using System.Dynamic;

namespace SQL.Data
{
    public class SqlStatementFragment : DynamicObject
    {
        protected  string StatementFragement ="";
        protected SqlStatementFragment()
        {}

        public SqlStatementFragment(string statementFragement)
        {
            StatementFragement = statementFragement;
        }



        public IEnumerable<DataRecord> GO()
        {
            var newList =  new List<DataRecord>();
            using (var connection = new SqlCeConnection(_.ConnectionString))
            {
                string commandText = this.ToString();
                using (var command = new SqlCeCommand(commandText))
                {
                    command.Connection = connection;
                    connection.Open();
                
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            newList.Add(new DataRecord());

                        }
                    }
                }
            }

            return newList;
        }

        public override string ToString()
        {
            return  StatementFragement;
        }

        public static SqlStatementFragment operator * (SqlStatementFragment op1, SqlStatementFragment op2)
        {
            return new SqlStatementFragment(op1+" * "+op2);
        }


        public static SqlStatementFragment operator ==(SqlStatementFragment op1, dynamic op2)
        {
            return new SqlStatementFragment(op1 + " = " + op2);
        }



        public static SqlStatementFragment operator !=(SqlStatementFragment op1, dynamic op2)
        {
            return new SqlStatementFragment(op1 + " <> " + op2);
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
                result = new SqlStatementFragment(ToString()+' '+ binder.Name);
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
            result = new SqlStatementFragment(String.Format("{0} {1} ({2})",StatementFragement,binder.Name,predicateMember));
            return true;

        }
    }
}