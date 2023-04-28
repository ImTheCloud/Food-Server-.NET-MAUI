namespace MyReference.ViewModel;

[QueryProperty(nameof(MonTxt), "Databc")]
public partial class AddProductViewModel : BaseViewModel
{
    [ObservableProperty]
    string monTxt;
    public AddProductViewModel()
    {

    }
}