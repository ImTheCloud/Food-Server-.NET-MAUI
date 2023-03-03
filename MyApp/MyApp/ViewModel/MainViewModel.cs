using MyApp.Model;
using System.Collections.ObjectModel;

namespace MyApp.ViewModel;

public partial class MainViewModel : ObservableObject
{

    [ObservableProperty]
    String monTexte = "go to details";

    StudentService MyService;

    public ObservableCollection <StudentModel> MyShownList { get; } = new();

    public MainViewModel(StudentService MyService)
    {
        this.MyService = MyService;
    }

    [RelayCommand]
    async Task GoToDetailsPage(string data)
    {
        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object> {
            {"Databc", data}
        });
    }

    [RelayCommand]
    async Task GetObject()
    {
        try
        {
            Globals.MyList = await MyService.GetStudents();

        }catch(Exception ex)
        {
            Debug.WriteLine($"Unable to get Students: {ex.Message}");
            await Shell.Current.DisplayAlert("Error:", ex.Message, "OK");
        }

        MyShownList.Clear();

        foreach(StudentModel student in Globals.MyList)
        {
            MyShownList.Add(student);
        }
        
    }
}