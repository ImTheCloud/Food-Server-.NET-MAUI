
namespace MyReference.View;


public partial class AddProductPage : ContentPage
{
    public AddProductPage(AddProductViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }
    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    private async void OnValidateButtonClicked(object sender, EventArgs e)
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
        await DisplayAlert("Le produit a été enregistré.", "Enregistrement réussi", "OK");
    }

}