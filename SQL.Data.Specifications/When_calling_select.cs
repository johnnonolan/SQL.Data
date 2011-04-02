using Machine.Specifications;

namespace SQL.Data.Specifications
{
    [Subject("SELECT Inspecting SQL")]
    public class When_calling_select_from_users
    {
        Because of = () => result = (q.SELECT * q.FROM.Users).ToString();
        It should_return_select_star_from_users = () => result.ShouldEqual("SELECT * FROM Users");
        static string result;
    }


    [Subject("Inspecting SQL")]
    public class When_calling_from_users
    {
        Because of = () => result = q.FROM.Users.ToString();
        It should_return_select_star_from_users = () => result.ShouldEqual("FROM Users");
        static string result;
    }

    [Subject("Inspecting SQL")]
    public class When_calling_select_from_users_with_predicate
    {

        Because of = () => result = (q.SELECT * q.FROM.Users.WHERE.UserId == 1).ToString();
        It should_return_a_empty_set = () => result.ShouldEqual("SELECT * FROM Users WHERE UserId = 1");
        static string result;
    }

    [Subject("Inspecting SQL")]
    public class When_calling_select_from_users_with_predicate_collection
    {
        Because of = () => result = (q.SELECT * q.FROM.Users.WHERE.UserId.IN(1,2)).ToString();
        It should_return_select_star_from_users = () => result.ShouldEqual("SELECT * FROM Users WHERE UserId IN (1,2)");
        static string result;
    }
}
