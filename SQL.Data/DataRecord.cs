using System;
using System.Collections.Generic;
using System.Dynamic;

namespace SQL.Data
{
    public class DataRecord : DynamicObject
    {

        // The inner dictionary.
        protected Dictionary<string, object> InnerDictionary
            = new Dictionary<string, object>();

        public DataRecord(Dictionary<string, object> fieldnames)
        {
            InnerDictionary = fieldnames;
        }

        // If you try to get a value of a property 
        // not defined in the class, this method is called.
        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            string name = binder.Name.ToLower();

            if (!InnerDictionary.TryGetValue(name, out result))
            {
                result = new SqlStatementFragment(ToString() + ' ' + binder.Name);
                return true;
            }
            return InnerDictionary.TryGetValue(name, out result);
        }

    }
}