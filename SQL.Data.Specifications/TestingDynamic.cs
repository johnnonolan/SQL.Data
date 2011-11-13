using System.Dynamic;
using Machine.Specifications;

namespace SQL.Data.Specifications
{
    [Subject("what comes first?")]
    public class TestingDynamic
    {
        Establish that = () => trex = new Dynosaur();
        Because of = () => trex.Roar();

        It should_try_defined_method_first = () => trex.lastGrowl.ShouldEqual("A static roar");
        static Dynosaur trex;

        private class Dynosaur : DynamicObject
        {
            public string lastGrowl { get; set; }

            public override bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
            {
                result = "a dyno roar";
                return true;
            }

            public void Roar()
            {
                lastGrowl = "A static roar";
            }
        }
    }
}