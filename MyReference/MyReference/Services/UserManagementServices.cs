namespace MyReference.Services;

public class UserManagementServices
{

}

    public class CreateUserTables
{
    public CreateUserTables() 
    {
        DataTable UserTable = new();
        DataTable AccesTable= new();

        DataColumn User_ID = new DataColumn("User_ID",System.Type.GetType("System.Int16"));
        DataColumn UserName = new DataColumn("UserName", System.Type.GetType("System.String"));
        DataColumn UserPassword = new DataColumn("UserPassword", System.Type.GetType("System.String"));
        DataColumn AccesType = new DataColumn("UserAccesType", System.Type.GetType("System.Int16"));

        DataColumn Acces_ID = new DataColumn("Acces_ID", System.Type.GetType("System.Int16"));
        DataColumn AccesName = new DataColumn("AccesName", System.Type.GetType("System.String"));
        DataColumn CreateObject = new DataColumn("CreateObject", System.Type.GetType("System.Boolean"));
        DataColumn DestroyObject = new DataColumn("DestroyObject", System.Type.GetType("System.Boolean"));
        DataColumn ModifyObject = new DataColumn("ModifyObject", System.Type.GetType("System.Boolean"));
        DataColumn ChangeUserRights = new DataColumn("ChangeUserRights", System.Type.GetType("System.Boolean"));






    }
}