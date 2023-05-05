namespace MyReference;

public partial class InventoryPage : ContentPage
{
	public InventoryPage(InventoryViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}

    
}