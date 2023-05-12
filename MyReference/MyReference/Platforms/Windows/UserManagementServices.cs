using System.Data.OleDb;

namespace MyReference.Services;

public partial class UserManagementServices
{
    OleDbConnection Connexion = new();
    OleDbDataAdapter UserAdapter = new();

    internal void ConfigTools()
    {
        Connexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.16.0;"
                                    + @"Data Source= "
                                    + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "QualityServer", "UserAccounts.accdb")
                                    + ";Persist Security Info=false";

        string DeleteCommandeTxt = "DELETE FROM DB_Users(UserName,UserPassword,UserAccesType) WHERE UserName=@UserName";
        string UpdateCommandeTxt = "UPDATE DB_Users SET UserName=@UserName, UserPassword=@UserPassword, UserAccesType=@UserAccesType WHERE UserName=@UserName";
        string InsertCommandeTxt = "INSERT INTO DB_Users (UserName, UserPassword, UserAccesType) VALUES (@UserName,@UserPassword,@UserAccesType)";
        string SelectCommandeTxt = "SELECT * FROM DB_Users ORDER BY User_ID";


        OleDbCommand DeleteCommand = new OleDbCommand(DeleteCommandeTxt, Connexion);
        OleDbCommand UpdateCommand = new OleDbCommand(UpdateCommandeTxt, Connexion);
        OleDbCommand InsertCommand = new OleDbCommand(InsertCommandeTxt, Connexion);
        OleDbCommand SelectCommand = new OleDbCommand(SelectCommandeTxt, Connexion);

        UserAdapter.DeleteCommand = DeleteCommand;
        UserAdapter.UpdateCommand = UpdateCommand;
        UserAdapter.InsertCommand = InsertCommand;
        UserAdapter.SelectCommand = SelectCommand;

        UserAdapter.InsertCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        UserAdapter.InsertCommand.Parameters.Add("@UserPassword", OleDbType.VarChar, 40, "UserPassword");
        UserAdapter.InsertCommand.Parameters.Add("@UserAccessType", OleDbType.Numeric, 100, "UserAccessType");
        UserAdapter.DeleteCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        UserAdapter.UpdateCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        UserAdapter.UpdateCommand.Parameters.Add("@UserPassword", OleDbType.VarChar, 40, "UserPassword");
        UserAdapter.UpdateCommand.Parameters.Add("@UserAccessType", OleDbType.Numeric, 100, "UserAccessType");


    }
}

