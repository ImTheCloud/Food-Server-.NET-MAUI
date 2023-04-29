namespace MyReference.ViewModel;

public class InventoryViewModel : ContentPage
{
	public InventoryViewModel()
	{
        AllFoodCollection();

    }

    public ObservableCollection<Food> AllFoodList { get; } = new ObservableCollection<Food>();


    public async void AllFoodCollection()
    {
        AllFoodList.Clear();
        foreach (Food stu in Globals.MyStaticList)
        {
            AllFoodList.Add(stu);
        }
    }
}