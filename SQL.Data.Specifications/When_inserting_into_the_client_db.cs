using System;
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

        Because of = () => _.INSERT.INTO.Users("UserID","UserName","CreatedDate","Price","Active").VALUES(1,"John","01-Jan-2010",200m,true).GO();

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

        It should_have_an_integer_set = () =>
            {
                resultLine = result[0];
                resultLine.UserID.ShouldEquall(1);
            };

        It should_have_a_date_set = () =>
            {
                resultLine = result[0];
                resultLine.CreatedDate.ShouldEqual(new DateTime(2010, 1, 1));
            };

        It should_have_money_set = () =>
            {
                resultLine = result[0];
                resultLine.CreatedDate.ShouldEqual(200m);
            };

        It should_have_bit_field_set = () =>
        {
            resultLine = result[0];
            resultLine.CreatedDate.ShouldEqual(true);
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
