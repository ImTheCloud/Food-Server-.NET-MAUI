global using MyReference.View;
global using MyReference.ViewModel;
global using MyReference.Model;
global using MyReference.Services;

global using CommunityToolkit.Mvvm.Input;
global using CommunityToolkit.Mvvm.ComponentModel;


global using System.Diagnostics;
global using System.Collections.ObjectModel;
global using System.ComponentModel;
global using System.Runtime.CompilerServices;
global using System.Text.Json;


global using MyReference.ViewModel;
global using MyReference.View;
global using MyReference.Model;
global using MyReference.Services;


public class Globals
{

    public static List<Monkey> MyStaticList = new List<Monkey>();
    internal static Queue<string> SerialBuffer = new Queue<string>();
}