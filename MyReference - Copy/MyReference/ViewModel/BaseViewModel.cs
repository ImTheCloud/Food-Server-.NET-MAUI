namespace MyReference.ViewModel;

public partial class BaseViewModel : ObservableObject
{
	[ObservableProperty]
	public string title;

	[ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    public bool IsNotBusy => !IsBusy;
    public BaseViewModel()
	{

	}
}