using Machine.Specifications;

namespace SQL.Data.Specifications
{
    public class When_updating_the_client_table
    {
        Establish that = () =>
        {
            _.ConnectionString = connectionString;
            SUTHelpers.DeleteUsers(connectionString);

            _.INSERT.INTO.Users("UserId", "UserName","Price").Values(1, "John",123.77m).GO();
        };

        Because of = () => _.UPDATE.Users.SET.UserName = "Jim";

        It should_update_the_field = () =>
            {
                result = (_.SELECT * _.FROM.Users).GO();
                resultLine = result[0];
                resultLine.UserName.ShouldEqual("Jim");
            };
        static string connectionString = @"Data Source = |DataDirectory|\TestDb.sdf";
        static dynamic result;
        static dynamic resultLine;
    }
}
