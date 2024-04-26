using MonkeyFinder.ViewModel;

namespace MonkeyFinder.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(MonkeyDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
    }

    private async void btnReturn_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

}