using MonkeyFinder.ViewModel;

namespace MonkeyFinder;

public partial class MainPage : ContentPage
{

	public MainPage(MonkeyViewModel monkeyViewModel)
	{
		InitializeComponent();
		BindingContext = monkeyViewModel;
	}
}

