namespace MyReference.Services
{
  
    public partial class UserManagementServices
    {

    }

    public class CreateUserTables
    {
        // Classe CreateUserTables

        public CreateUserTables()
        {
            // Constructeur de la classe CreateUserTables

            DataTable UserTable = new();
            DataTable AccessTable = new();
            // Cr�ation de deux objets DataTable pour stocker les informations des utilisateurs et des acc�s

            DataColumn User_ID = new DataColumn("User_ID", System.Type.GetType("System.Int16"));
            DataColumn UserName = new DataColumn("UserName", System.Type.GetType("System.String"));
            DataColumn UserPassword = new DataColumn("UserPassword", System.Type.GetType("System.String"));
            DataColumn AccessType = new DataColumn("UserAccessType", System.Type.GetType("System.Int16"));
            // D�finition des colonnes de la table des utilisateurs avec leurs types de donn�es

            DataColumn Access_ID = new DataColumn("Access_ID", System.Type.GetType("System.Int16"));
            DataColumn AccessName = new DataColumn("AccessName", System.Type.GetType("System.String"));
            DataColumn CreateObject = new DataColumn("CreateObject", System.Type.GetType("System.Boolean"));
            DataColumn DestroyObject = new DataColumn("DestroyObject", System.Type.GetType("System.Boolean"));
            DataColumn ModifyObject = new DataColumn("ModifyObject", System.Type.GetType("System.Boolean"));
            DataColumn ChangeUserRights = new DataColumn("ChangeUserRights", System.Type.GetType("System.Boolean"));
            // D�finition des colonnes de la table des acc�s avec leurs types de donn�es

            //UserTable
            UserTable.TableName = "Users";
            // Nom de la table des utilisateurs

            User_ID.AutoIncrement = true;
            User_ID.Unique = true;
            UserTable.Columns.Add(User_ID);
            // Ajout de la colonne User_ID � la table des utilisateurs avec l'auto-incr�mentation et l'unicit�

            UserName.Unique = true;
            UserTable.Columns.Add(UserName);
            // Ajout de la colonne UserName � la table des utilisateurs avec l'unicit�

            UserTable.Columns.Add(UserPassword);
            UserTable.Columns.Add(AccessType);
            // Ajout des colonnes UserPassword et AccessType � la table des utilisateurs

            //AccesTable
            AccessTable.TableName = "Access";
            // Nom de la table des acc�s

            Access_ID.AutoIncrement = true;
            Access_ID.Unique = true;
            AccessTable.Columns.Add(Access_ID);
            // Ajout de la colonne Access_ID � la table des acc�s avec l'auto-incr�mentation et l'unicit�

            AccessName.Unique = true;
            AccessTable.Columns.Add(AccessName);
            // Ajout de la colonne AccessName � la table des acc�s avec l'unicit�

            AccessTable.Columns.Add(CreateObject);
            AccessTable.Columns.Add(DestroyObject);
            AccessTable.Columns.Add(ModifyObject);
            AccessTable.Columns.Add(ChangeUserRights);
            // Ajout des colonnes CreateObject, DestroyObject, ModifyObject et ChangeUserRights � la table des acc�s

            Globals.UserSet.Tables.Add(UserTable);
            Globals.UserSet.Tables.Add(AccessTable);
            // Ajout des tables UserTable et AccessTable � l'ensemble de donn�es (DataSet) Globals.UserSet

            DataColumn parentColumn = Globals.UserSet.Tables["Access"].Columns["Access_ID"];
            DataColumn childColumn = Globals.UserSet.Tables["Users"].Columns["UserAccessType"];
            // D�finition des colonnes parent et enfant pour �tablir une relation entre les tables Access et Users

            DataRelation relation = new DataRelation("Access2User", parentColumn, childColumn);
            // Cr�ation d'une relation de donn�es nomm�e "Access2User" entre les colonnes parent et enfant

            // DEFINI USERSET DANS GLOBALUSING
            Globals.UserSet.Tables["Users"].ParentRelations.Add(relation);
            // Ajout de la relation � la collection des relations parent de la table Users dans l'ensemble de donn�es Globals.UserSet
        }
    }
}
