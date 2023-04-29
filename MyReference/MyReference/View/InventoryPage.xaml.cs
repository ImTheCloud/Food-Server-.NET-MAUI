namespace MyReference.View;

public partial class InventoryPage : ContentPage
{
	public InventoryPage(InventoryViewModel viewmodel)
	{
		InitializeComponent();
		BindingContext = viewmodel;
	}

    private void InitializeComponent()
    {
        throw new NotImplementedException();
    }
}