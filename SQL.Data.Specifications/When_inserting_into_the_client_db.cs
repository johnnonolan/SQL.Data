using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;

namespace SQL.Data.Specifications
{
    [Subject("Inserting")]
    public class When_inserting_into_the_client_db
    {
        Establish that = () =>
        {
            _.ConnectionString = connectionString;
        };
        Because of = () => _.INSERT.INTO.Users("UserID").VALUES(1);

        It should_have_one_record = () =>
            {
                result = (_.SELECT * _.FROM.Users).GO();
                result.Count.ShouldEqual(1);
            };
        static IList<DataRecord> result;
        static string connectionString = @"Data Source = |DataDirectory|\TestDb.sdf";

    }

    [Subject("Inspecting SQL")]
    public class When_calling_inset_from_users
    {
        Because of = () => result = (_.INSERT.INTO.Users("UserID").VALUES(1)).ToString();
        It should_return_select_star_from_users = () => result.ShouldEqual("INSERT INTO Users (UserID) VALUES (1)");
        static string result;
    }
}
