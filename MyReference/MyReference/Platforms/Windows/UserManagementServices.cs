using System.Data.OleDb;

namespace MyReference.Services;

public partial class UserManagementServices
{
    public OleDbConnection Connexion = new();
    public OleDbDataAdapter Users_Adapter = new();

    internal void ConfigTools()
    {
        Connexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.16.0;"
                                           + @"Data Source=" + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "QualityServer", "UserAccounts.accdb")
                                           + ";Persist Security Info=False";

        string Insert_CommandText = "INSERT INTO DB_Users(UserName,UserPassword,UserAccessType) VALUES (@UserName,@UserPassword,@UserAccessType);";
        string Delete_CommandText = "DELETE FROM DB_Users WHERE UserName = @UserName;";
        string Select_CommandText = "SELECT * FROM DB_Users ORDER BY User_ID;";
        string Update_CommandText = "UPDATE DB_Users SET UserPassword = @UserPassword, UserAccessType = @UserAccessType WHERE UserName = @UserName;";

        OleDbCommand Insert_Command = new OleDbCommand(Insert_CommandText, Connexion);
        OleDbCommand Delete_Command = new OleDbCommand(Delete_CommandText, Connexion);
        OleDbCommand Select_Command = new OleDbCommand(Select_CommandText, Connexion);
        OleDbCommand Update_Command = new OleDbCommand(Update_CommandText, Connexion);

        Users_Adapter.SelectCommand = Select_Command;
        Users_Adapter.InsertCommand = Insert_Command;
        Users_Adapter.DeleteCommand = Delete_Command;
        Users_Adapter.UpdateCommand = Update_Command;

        Users_Adapter.TableMappings.Add("DB_Users", "Users");
        Users_Adapter.TableMappings.Add("DB_Access", "Access");

        Users_Adapter.InsertCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        Users_Adapter.InsertCommand.Parameters.Add("@UserPassword", OleDbType.VarChar, 40, "UserPassword");
        Users_Adapter.InsertCommand.Parameters.Add("@UserAccessType", OleDbType.Numeric, 100, "UserAccessType");
        Users_Adapter.DeleteCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        Users_Adapter.UpdateCommand.Parameters.Add("@UserName", OleDbType.VarChar, 40, "UserName");
        Users_Adapter.UpdateCommand.Parameters.Add("@UserPassword", OleDbType.VarChar, 40, "UserPassword");
        Users_Adapter.UpdateCommand.Parameters.Add("@UserAccessType", OleDbType.Numeric, 100, "UserAccessType");

    }
    internal async void FillUsersFromDB()
    {
        try
        {
            Connexion.Open();

            Users_Adapter.Fill(Globals.UserSet.Tables["Users"]);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Base de données", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }

    internal async void ReadFromDB()
    {
        OleDbCommand SelectCommand = new OleDbCommand("SELECT * FROM DB_Access;", Connexion);

        try
        {
            Connexion.Open();
            OleDbDataReader reader = SelectCommand.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Globals.UserSet.Tables["Acces"].Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                }
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Base de données", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }

    internal async void insertIntoDB(string name, string pass, Int32 acces)
    {
        try
        {
            Connexion.Open();
            Users_Adapter.InsertCommand.Parameters[0].Value = name;
            Users_Adapter.InsertCommand.Parameters[1].Value = pass;
            Users_Adapter.InsertCommand.Parameters[2].Value = acces;

            int buffer = Users_Adapter.InsertCommand.ExecuteNonQuery();

            if (buffer != 0)
            {
                await Shell.Current.DisplayAlert("Base de données", "Utilisateur inséré avec succès", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Base de données", "Échec de l'insertion de l'utilisateur", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Base de données", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }

    internal async void DeleteIntoDB(string name)
    {
        try
        {
            Connexion.Open();
            Users_Adapter.InsertCommand.Parameters[0].Value = name;

            int buffer = Users_Adapter.DeleteCommand.ExecuteNonQuery();

            if (buffer != 0)
            {
                await Shell.Current.DisplayAlert("Base de données", "Utilisateur supprimé avec succès", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Base de données", "Échec de la suppression de l'utilisateur", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Base de données", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }

    internal async void UpdateDB()
    {
        try
        {
            Connexion.Open();

            int buffer = Users_Adapter.Update(Globals.UserSet.Tables["Users"]);

            if (buffer != 0)
            {
                await Shell.Current.DisplayAlert("Base de données", "Utilisateur mis à jour avec succès", "OK");
            }
            else
            {
                await Shell.Current.DisplayAlert("Base de données", "Échec de la mise à jour de l'utilisateur", "OK");
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Base de données", ex.Message, "OK");
        }
        finally
        {
            Connexion.Close();
        }
    }
}
