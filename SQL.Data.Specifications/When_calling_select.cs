using Machine.Specifications;

namespace SQL.Data.Specifications
{
    [Subject("Select")]
    public class When_calling_select_from_users
    {
        Because of = () => result = (q.SELECT*FROM.users()).ToString();
        It should_return_select_star_from_users = () => result.ShouldEqual("SELECT * FROM USERS");
        static string result;
    }
}
