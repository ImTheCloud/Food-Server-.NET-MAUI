using System.Windows.Input;

namespace MyReference.ViewModel;

public partial class InventoryViewModel : BaseViewModel
{
    public InventoryViewModel()
    {
        AllFoodCollection();

    }

    public ObservableCollection<Food> AllFoodList { get; } = new ObservableCollection<Food>();
    public ICommand DeleteCommand => new Command<Food>(DeleteFood);
    public ICommand OnSearchCommand => new Command(SearchingData);

    [ObservableProperty]
    public string code;
   

    public void AllFoodCollection()
    {
        AllFoodList.Clear();
        foreach (Food stu in Globals.MyStaticList)
        {
            AllFoodList.Add(stu);
        }
    }


    private async void DeleteFood(Food food)
    {
        Globals.MyStaticList.Remove(food);
        AllFoodCollection();
        FoodService myService = new();
        await myService.SetFoodJson();
    }




    public void SearchingData()
    {
        string code = Code;

        AllFoodList.Clear();
        foreach (Food stu in Globals.MyStaticList)
        {
            if (stu.Code == code)
            {
                AllFoodList.Add(stu);
            }
            else if (code == "")
            {
                AllFoodList.Add(stu);
            }
        }
    }

}