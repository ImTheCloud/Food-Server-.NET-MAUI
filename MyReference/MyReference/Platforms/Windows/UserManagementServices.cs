using System.Data.OleDb;

namespace MyReference.Services;

public partial class UserManagementServices
{
    public OleDbConnection Connexion = new();
    public OleDbDataAdapter UserAdapter = new();

    internal void ConfigTools()
    {
        Connexion.ConnectionString = "Provider=Microsoft.ACE.OLEDB.16.0;"
                                    + @"Data Source= "
                                    + Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop) , "QualityServer", "UserAccounts.accdb")
                                    + ";Persist Security Info=false";

        string DeleteCommandeTxt = "DELETE FROM DB_Users WHERE UserName=@UserName";
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
    internal async void FillUsersFromDB()
    {
        try
        {
            Connexion.Open();

            UserAdapter.Fill(Globals.UserSet.Tables["Users"]);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
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
            if(reader.HasRows)
            {
                while (reader.Read()){
                    Globals.UserSet.Tables["Acces"].Rows.Add(reader[0], reader[1], reader[2], reader[3], reader[4], reader[5]);
                }
            }
            reader.Close();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
        }
        finally
        {
            Connexion.Close();
        }
    }
    internal async void insertIntoDB(string name,string pass,Int32 acces)
    {
        try
        {
            Connexion.Open();
            UserAdapter.InsertCommand.Parameters[0].Value = name;
            UserAdapter.InsertCommand.Parameters[1].Value = pass;
            UserAdapter.InsertCommand.Parameters[2].Value = acces;

            int buffer = UserAdapter.InsertCommand.ExecuteNonQuery();

            if(buffer !=0)
            {
                await Shell.Current.DisplayAlert("DataBase", "User succesfully inserted", "ok");

            }
            else
            {
                await Shell.Current.DisplayAlert("DataBase", "User not inserted", "ok");

            }

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
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
                UserAdapter.InsertCommand.Parameters[0].Value = name;


                int buffer = UserAdapter.DeleteCommand.ExecuteNonQuery();

                if (buffer != 0)
                {
                    await Shell.Current.DisplayAlert("DataBase", "User succesfully deleted", "ok");

                }
                else
                {
                    await Shell.Current.DisplayAlert("DataBase", "User not deleted", "ok");

                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
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

            int buffer = UserAdapter.Update(Globals.UserSet.Tables["Users"]);

            if (buffer != 0)
            {
                await Shell.Current.DisplayAlert("DataBase", "User succesfully updated", "ok");

            }
            else
            {
                await Shell.Current.DisplayAlert("DataBase", "User not updated", "ok");

            }

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("DataBase", ex.Message, "ok");
        }
        finally
        {
            Connexion.Close();
        }

    }
    }

