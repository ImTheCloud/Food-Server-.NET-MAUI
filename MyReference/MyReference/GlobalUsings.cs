global using System.Data.OleDb;
global using MyReference.View;
global using MyReference.ViewModel;
global using MyReference.Model;
global using MyReference.Services;
global using System.Data;
global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;
global using System.Diagnostics;
global using System.Collections.ObjectModel;
global using System.ComponentModel;
global using System.Runtime.CompilerServices;
global using System.Text.Json;


   

public class Globals 
{
    // variable dispo dans tout le programme
    public static List<Food> MyStaticList = new List<Food>();
    // internal static Queue<string> SerialBuffer = new Queue<string>();


    public static DataSet UserSet = new();
    public static List<User> UserList = new();
    public static Boolean isAdmin = false;


}

