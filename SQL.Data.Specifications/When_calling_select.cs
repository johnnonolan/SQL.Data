using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data.SqlServerCe;
using Machine.Specifications;

namespace SQL.Data.Specifications
{
    [Subject("SELECT Inspecting SQL")]
    public class When_calling_select_from_users
    {
        Because of = () => result = (_.SELECT * _.FROM.Users).ToString();
        It should_return_select_star_from_users = () => result.ShouldEqual("SELECT * FROM Users");
        static string result;
    }


    [Subject("Inspecting SQL")]
    public class When_calling_from_users
    {
        Because of = () => result = _.FROM.Users.ToString();
        It should_return_select_star_from_users = () => result.ShouldEqual("FROM Users");
        static string result;
    }

    [Subject("Inspecting SQL")]
    public class When_calling_select_from_users_with_predicate
    {

        Because of = () => result = (_.SELECT * _.FROM.Users.WHERE.UserId == 1).ToString();
        It should_return_a_empty_set = () => result.ShouldEqual("SELECT * FROM Users WHERE UserId = 1");
        static string result;
    }

    [Subject("Inspecting SQL")]
    public class When_calling_select_from_users_with_predicate_collection
    {
        Because of = () => result = (_.SELECT * _.FROM.Users.WHERE.UserId.IN(1,2)).ToString();
        It should_return_select_star_from_users = () => result.ShouldEqual("SELECT * FROM Users WHERE UserId IN (1,2)");
        static string result;
    }

    [Subject("")]
    public class When_calling_select_returns_a_datarecord
    {
        Establish that = () =>
            {
                _.ConnectionString = connectionString;
                //so here we should enter a record.
                // or make sure the db is empty
                SUTHelpers.DeleteUsers(connectionString);
            };
        Because of = () => result = (_.SELECT * _.FROM.Users).GO();
        It should_return_a_data_result = () => result.ShouldBeOfType<IEnumerable<DataRecord>>();
        It should_return_zero_results = () => (result as List<DataRecord>).Count.ShouldEqual(0);
        Cleanup after = () => SUTHelpers.DeleteUsers(connectionString);
            static Object result;
        static string connectionString = @"Data Source = |DataDirectory|\TestDb.sdf";
    }

    internal static class SUTHelpers
    {
        public static void DeleteUsers(string connectionString)
        {
            const string queryString = "DELETE FROM Users;";
            using (var connection = new SqlCeConnection(
               connectionString))
            {
                var command = new SqlCeCommand(queryString, connection);
                connection.Open();
                command.ExecuteNonQuery();
            }
        }
    }
}
