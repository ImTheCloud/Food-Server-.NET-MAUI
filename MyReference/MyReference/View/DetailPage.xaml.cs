
namespace MyReference.View;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
        ValidateButton.Command = new Command(SaveItem);

    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        var validateButton = this.FindByName<Button>("ValidateButton");
        validateButton.Clicked += ValidateButton_Clicked;
    }

    private async void ValidateButton_Clicked(object sender, EventArgs e)
    {
        await redirection();
    }
    public async Task redirection()
    {
        await Shell.Current.GoToAsync("///MainPage");
    }
    public async void SaveItem()
    {
        var item = new Food
        {
            Name = NomEntry.Text,
            Quantite = int.Parse(QuantiteEntry.Text),
            Details = DetailsEntry.Text,
            Image = ImageEntry.Text,
            Code = CodeEntry.Text,
            Prix = double.Parse(PrixEntry.Text)

        };

        FoodService myService = new();

        Globals.MyStaticList.Add(item);
        await myService.SetFoodJson();
    }




}