using System.Collections.Generic;
using Machine.Specifications;

namespace SQL.Data.Specifications
{
    [Subject("Inserting")]
    public class When_inserting_into_the_client_db
    {
        Establish that = () =>
        {
            _.ConnectionString = connectionString;
            SUTHelpers.DeleteUsers(connectionString);


        };

        Because of = () => _.INSERT.INTO.Users("UserID","UserName","CreatedDate").VALUES(1,"John","01-Jan-2010").GO();

        It should_have_one_record = () =>
            {
                result = (_.SELECT * _.FROM.Users).GO();
                result.Count.ShouldEqual(1);
            };

        It should_have_stringfield_set = () =>
            {
                resultLine = result[0];
                resultLine.UserName.ShouldEqual("John");
            };

        Cleanup after = () => SUTHelpers.DeleteUsers(connectionString);
        static dynamic resultLine;
        static IList<DataRecord> result;
        static string connectionString = @"Data Source = |DataDirectory|\TestDb.sdf";

    }

    [Subject("Inspecting SQL")]
    public class When_calling_insert_from_users
    {

        Because of = () => result = (_.INSERT.INTO.Users("UserID","UserName").VALUES(1,"John")).ToString();
        It should_return_a_valid_insert_statement = () => result.ShouldEqual(@"INSERT INTO Users (UserID,UserName) VALUES (1,'John')");
        static string result;
    }
}
