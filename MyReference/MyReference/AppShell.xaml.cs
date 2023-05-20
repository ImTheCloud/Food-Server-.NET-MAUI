namespace MyReference;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

        Routing.RegisterRoute(nameof(AddProductPage), typeof(AddProductPage));
        Routing.RegisterRoute(nameof(InventoryPage), typeof(InventoryPage));
        Routing.RegisterRoute(nameof(ShowProductPage), typeof(ShowProductPage));
        Routing.RegisterRoute(nameof(AdminPage), typeof(AdminPage));

    }
}
