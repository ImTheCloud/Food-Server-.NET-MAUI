namespace MyReference;

public partial class ShowProductPage : ContentPage
{
    public ShowProductPage(ShowProductViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }
}

