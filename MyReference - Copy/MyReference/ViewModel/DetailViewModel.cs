namespace MyReference.ViewModel;

[QueryProperty(nameof(MonTxt), "Databc")]
public partial class DetailViewModel : BaseViewModel
{
	[ObservableProperty]
	string monTxt;
	public DetailViewModel()
	{
		
	}
}