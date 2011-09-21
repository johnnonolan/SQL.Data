using System.Collections.Generic;
using System.Dynamic;
using Machine.Specifications;

namespace SQL.Data.Specifications
{
    [Subject("SELECT Executing")]
    public class When_executing_select_from_users
    {
        Establish that = () => { 
            _.ConnectionString = connectionString;
        };
        Because of = () => result = (_.SELECT * _.FROM.Users).GO();
        It should_return_select_star_from_users = () => result.Count.ShouldEqual(0);
        static IList<DataRecord> result;
        static string connectionString = @"Data Source = |DataDirectory|\TestDb.sdf";

    }

    [Subject("what comes first?")]
    public class TestingDynamic
    {
        Establish that = () => trex = new Dynosaur();
        Because of = () => trex.Roar();

        It should_try_defined_method_first = () => trex.lastGrowl.ShouldEqual("A static roar");
        static Dynosaur trex;
    }

    internal class Dynosaur :DynamicObject
    {
        public string lastGrowl { get; set; }

        public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
        {
            result = "a dyno roar";
            return true;
            //return base.TryInvokeMember(binder, args, out result);
        }

        public void Roar()
        {
            lastGrowl = "A static roar";
        }
    }
}