# SQL.DATA #
A micro-orm for people who just don't want to give up the SQL syntax.


Some people *actually* like to spend their days messing around in SQL.
Unfortunately though the elitists and fashinistas have deemed that writing actual SQL is less preferable to writing REAL MAN'S CODE, IN DOT NETS.

So I've created this framework to create the best of all worlds!

Here's how you use it.

## SELECTS ##
(_.SELECT * _.FROM.Users).GO();
## UPDATES ##
_.UPDATE.Users.SET.UserName = "Jim";
## DELETES ##
_DELETE.FROM.USERS.GO();
# INSERTS #
_.INSERT.INTO.Users("UserID","UserName","CreatedDate","Price","Active").VALUES(1,"John","01-Jan-2010",200m,true).GO();

