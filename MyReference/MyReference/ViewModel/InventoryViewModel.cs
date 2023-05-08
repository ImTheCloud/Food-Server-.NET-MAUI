using System.Windows.Input;

namespace MyReference.ViewModel;

public class InventoryViewModel : ContentPage
{
	public InventoryViewModel()
	{
        AllFoodCollection();

    }

    public ObservableCollection<Food> AllFoodList { get; } = new ObservableCollection<Food>();
    public ICommand DeleteCommand => new Command<Food>(DeleteFood);
    public ICommand OnSearchCommand => new Command(SearchingData);


    public string _code;
    public string barcodeData;
    public string Code
    {
        get { return _code; }
        set
        {
            if (_code != value)
            {
                _code = value;
                OnPropertyChanged(nameof(Code));
            }
        }
    }

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
        barcodeData = Code;

        AllFoodList.Clear();
        foreach (Food stu in Globals.MyStaticList)
        {
            if (stu.Code == barcodeData)
            {
                AllFoodList.Add(stu);
            }
            else if(barcodeData== "")
            {
                AllFoodList.Add(stu);
            }
        }
    }

}