﻿using System;
using System.Collections.Generic;
using System.Data.SqlServerCe;
using System.Dynamic;

namespace SQL.Data
{
    public class SqlStatementFragment : DynamicObject
    {
        protected Dictionary<string, object> InnerDictionary = new Dictionary<string, object>();

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
                var commandText = this.ToString();

                try
                {
                    using (var command = new SqlCeCommand(commandText))
                    {
                        var fieldnames = new Dictionary<string, object>();
                        command.Connection = connection;
                        connection.Open();

                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                for (var i = 0; i < reader.FieldCount; i++)
                                {
                                    fieldnames.Add(reader.GetName(i), reader.GetValue(i));
                                }

                                var rec = new DataRecord(fieldnames);

                                newList.Add(rec);

                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(String.Format("Exception when executing: {0}",commandText), ex);
                }
            }
            return newList;
        }

        public override string ToString()
        {
            if (StatementFragement.StartsWith("INSERT"))
            {  
                var openBracketPos = StatementFragement.IndexOf('(');
                if (openBracketPos > -1)
                {
                
                    var beginningFragement = StatementFragement.Substring(0, openBracketPos);
                    var closeBracketPos = StatementFragement.IndexOf(')');
                    var endfragment = StatementFragement.Substring(closeBracketPos,
                                                                   StatementFragement.Length - closeBracketPos);

                    var insertedFields = StatementFragement.Substring(openBracketPos, closeBracketPos - openBracketPos);
                    var cleanedInsertedFields = insertedFields.Replace("\'", "");
                    StatementFragement = beginningFragement + cleanedInsertedFields + endfragment;

                }

            }
         
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

        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            string name = binder.Name.ToLower();

            if (!InnerDictionary.TryGetValue(name, out result))
            {
                result = new SqlStatementFragment(ToString()+' '+ binder.Name);
                return true;
            }
            return InnerDictionary.TryGetValue(name, out result);
        }

        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            InnerDictionary[binder.Name.ToLower()] = value;
            return true;
        }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            var predicateMember = "";
            foreach (var o in args)
            {
                if (predicateMember != "")
                    predicateMember += ",";
                if (o is String)
                    predicateMember += @"'" + o + @"'";
                else
                    if (o is Boolean)
                        predicateMember += ((bool)o).ToSQLBitField();
                    else
                        predicateMember += o;  
                  
            }

            result = new SqlStatementFragment(String.Format("{0} {1} ({2})",StatementFragement,binder.Name,predicateMember));
            return true;
        }
    }

    internal static class BoolExtensions
    {
        public static int ToSQLBitField(this bool value)
        {
            return value ? 1 : 0;
        }
    }
}