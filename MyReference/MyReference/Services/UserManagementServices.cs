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

        DataColumn User_ID             = new DataColumn("User_ID",System.Type.GetType("System.Int16"));
        DataColumn UserName            = new DataColumn("UserName", System.Type.GetType("System.String"));
        DataColumn UserPassword        = new DataColumn("UserPassword", System.Type.GetType("System.String"));
        DataColumn AccesType           = new DataColumn("UserAccesType", System.Type.GetType("System.Int16"));

        DataColumn Acces_ID            = new DataColumn("Acces_ID", System.Type.GetType("System.Int16"));
        DataColumn AccesName           = new DataColumn("AccesName", System.Type.GetType("System.String"));
        DataColumn CreateObject        = new DataColumn("CreateObject", System.Type.GetType("System.Boolean"));
        DataColumn DestroyObject       = new DataColumn("DestroyObject", System.Type.GetType("System.Boolean"));
        DataColumn ModifyObject        = new DataColumn("ModifyObject", System.Type.GetType("System.Boolean"));
        DataColumn ChangeUserRights    = new DataColumn("ChangeUserRights", System.Type.GetType("System.Boolean"));

        //UserTable
        UserTable.TableName="users";
        User_ID.AutoIncrement=true;
        User_ID.Unique=true;
        UserTable.Columns.Add(User_ID);

        UserName.Unique =true;
        UserTable.Columns.Add(UserName);

        UserTable.Columns.Add(UserPassword);
        UserTable.Columns.Add(AccesType);


        //AccesTable
        AccesTable.TableName = "Acces";

        Acces_ID.AutoIncrement = true;
        Acces_ID.Unique = true;
        AccesTable.Columns.Add(Acces_ID);

        AccesName.Unique = true;
        AccesTable.Columns.Add(AccesName);

        AccesTable.Columns.Add(CreateObject);
        AccesTable.Columns.Add(DestroyObject);
        AccesTable.Columns.Add(ModifyObject);
        AccesTable.Columns.Add(ChangeUserRights);

        Globals.UserSet.Tables.Add(UserTable);
        Globals.UserSet.Tables.Add(AccesTable);

        DataColumn parentColumn = Globals.UserSet.Tables["Acces"].Columns["Acces_ID"];
        DataColumn childColumn = Globals.UserSet.Tables["Users"].Columns["UserAccesType"];

        DataRelation relation = new DataRelation("Acces2User",parentColumn,childColumn);

        // DEFINI USERSET DANS GLOBALUSING
        Globals.UserSet.Tables["Users"].ParentRelations.Add(relation);


      
    }
}