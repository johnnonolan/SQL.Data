using Machine.Specifications;

namespace SQL.Data.Specifications
{
    public class When_deleting_from_the_clients_table
    {
        Establish that = () =>
            {
                _.ConnectionString = ConnectionString;
                SUTHelpers.DeleteUsers(ConnectionString);
                _.INSERT.INTO.Users("UserID", "UserName", "CreatedDate", "Price", "Active").VALUES(
                    1, 
                    "John",
                    "01-Jan-2010", 
                    200m,
                    true).GO();
            };

        Because of = () => _.DELETE.FROM.USERS.GO();

        It should_have_no_records_in_the_Table = () =>
            {
                int result = (_.SELECT * _.FROM.Users).GO().Count;
                result.ShouldEqual(0);
            };
        static string ConnectionString = @"Data Source = |DataDirectory|\TestDb.sdf";
    }
}
