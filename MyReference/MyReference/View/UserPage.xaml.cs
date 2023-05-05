namespace MyReference;

public partial class UserPage : ContentPage
{
	public UserPage(UserViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}