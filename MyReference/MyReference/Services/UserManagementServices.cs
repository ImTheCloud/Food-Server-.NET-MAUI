namespace MyReference.Services
{
  
    public partial class UserManagementServices
    {

    }

    public class CreateUserTables
    {


        public CreateUserTables()
        {
            // Constructeur de la classe CreateUserTables

            DataTable UserTable = new();
            DataTable AccessTable = new();
            // Création de deux objets DataTable pour stocker les informations des utilisateurs et des accès

            DataColumn User_ID = new DataColumn("User_ID", System.Type.GetType("System.Int16"));
            DataColumn UserName = new DataColumn("UserName", System.Type.GetType("System.String"));
            DataColumn UserPassword = new DataColumn("UserPassword", System.Type.GetType("System.String"));
            DataColumn AccessType = new DataColumn("UserAccessType", System.Type.GetType("System.Int16"));
            // Définition des colonnes de la table des utilisateurs avec leurs types de données

            DataColumn Access_ID = new DataColumn("Access_ID", System.Type.GetType("System.Int16"));
            DataColumn AccessName = new DataColumn("AccessName", System.Type.GetType("System.String"));
            DataColumn CreateObject = new DataColumn("CreateObject", System.Type.GetType("System.Boolean"));
            DataColumn DestroyObject = new DataColumn("DestroyObject", System.Type.GetType("System.Boolean"));
            DataColumn ModifyObject = new DataColumn("ModifyObject", System.Type.GetType("System.Boolean"));
            DataColumn ChangeUserRights = new DataColumn("ChangeUserRights", System.Type.GetType("System.Boolean"));
            // Définition des colonnes de la table des accès avec leurs types de données

            //UserTable
            UserTable.TableName = "Users";
            // Nom de la table des utilisateurs

            User_ID.AutoIncrement = true;
            User_ID.Unique = true;
            UserTable.Columns.Add(User_ID);
            // Ajout de la colonne User_ID à la table des utilisateurs avec l'auto-incrémentation et l'unicité

            UserName.Unique = true;
            UserTable.Columns.Add(UserName);
            // Ajout de la colonne UserName à la table des utilisateurs avec l'unicité

            UserTable.Columns.Add(UserPassword);
            UserTable.Columns.Add(AccessType);
            // Ajout des colonnes UserPassword et AccessType à la table des utilisateurs

            //AccesTable
            AccessTable.TableName = "Access";
            // Nom de la table des accès

            Access_ID.AutoIncrement = true;
            Access_ID.Unique = true;
            AccessTable.Columns.Add(Access_ID);
            // Ajout de la colonne Access_ID à la table des accès avec l'auto-incrémentation et l'unicité

            AccessName.Unique = true;
            AccessTable.Columns.Add(AccessName);
            // Ajout de la colonne AccessName à la table des accès avec l'unicité

            AccessTable.Columns.Add(CreateObject);
            AccessTable.Columns.Add(DestroyObject);
            AccessTable.Columns.Add(ModifyObject);
            AccessTable.Columns.Add(ChangeUserRights);
            // Ajout des colonnes CreateObject, DestroyObject, ModifyObject et ChangeUserRights à la table des accès

            Globals.UserSet.Tables.Add(UserTable);
            Globals.UserSet.Tables.Add(AccessTable);
            // Ajout des tables UserTable et AccessTable à l'ensemble de données (DataSet) Globals.UserSet

            DataColumn parentColumn = Globals.UserSet.Tables["Access"].Columns["Access_ID"];
            DataColumn childColumn = Globals.UserSet.Tables["Users"].Columns["UserAccessType"];
            // Définition des colonnes parent et enfant pour établir une relation entre les tables Access et Users

            DataRelation relation = new DataRelation("Access2User", parentColumn, childColumn);
            // Création d'une relation de données nommée "Access2User" entre les colonnes parent et enfant

            // DEFINI USERSET DANS GLOBALUSING
            Globals.UserSet.Tables["Users"].ParentRelations.Add(relation);
            // Ajout de la relation à la collection des relations parent de la table Users dans l'ensemble de données Globals.UserSet
        }
    }
}
