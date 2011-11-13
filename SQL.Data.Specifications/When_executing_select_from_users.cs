using System.Collections.Generic;
using System.Dynamic;
using Machine.Specifications;

namespace SQL.Data.Specifications
{

    [Subject("SELECT Executing")]
    public class When_Select_some_fields_from_users
    {
        Establish that = () =>
            {
                _.ConnectionString = connectionString;
                //delete all;
                SUTHelpers.DeleteUsers(connectionString);

                //insert a record;
                _.INSERT.INTO.Users("UserID", "UserName", "CreatedDate", "Price", "Active").VALUES(1, "John", "01-Jan-2010", 200m, true).GO();
            };

        Because of = () => result  = _.Select("UserId","UserName").FROM.USERS.GO();

        It should_return_userName = () =>
            {
                resultline = result[0];
                dynamic s = resultline.UserName;                
                
                ((string) s).ShouldEqual("John");
            };
        It should_not_return_active;
        static IList<DataRecord> result;
        static dynamic resultline;
        static string connectionString = @"Data Source = |DataDirectory|\TestDb.sdf";

    }

    [Subject("SELECT Executing")]
    public class When_executing_select_from_users
    {
        Establish that = () => { 
            _.ConnectionString = connectionString;
                                   SUTHelpers.DeleteUsers(connectionString);
            _.INSERT.INTO.Users("UserID", "UserName", "CreatedDate", "Price", "Active").VALUES(1, "John", "01-Jan-2010", 200m, true).GO();
        };
        Because of = () => result = (_.SELECT * _.FROM.Users).GO();
        It should_return_select_star_from_users = () => result.Count.ShouldEqual(1);

        It should_return_username_set = () =>
            {
                dynamic resultline = result[0];
                dynamic s = resultline.UserName.ShouldEqual("John");


            };
        static IList<DataRecord> result;
        static string connectionString = @"Data Source = |DataDirectory|\TestDb.sdf";

    }

  
}