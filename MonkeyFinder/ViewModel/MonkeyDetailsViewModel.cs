using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MonkeyFinder.Model;
using System.Diagnostics;

namespace MonkeyFinder.ViewModel
{
    [QueryProperty("Monkey", "Monkey")]
    
    public partial class MonkeyDetailsViewModel:BaseViewModel
    {
        IMap map;
        public MonkeyDetailsViewModel(IMap map)
        {
            this.map = map;
        }


        [ObservableProperty]
        Monkey monkey;


        [RelayCommand]
        async Task GoBackAsync()
        {
            try
            {

                await map.OpenAsync(Monkey.Longitude, Monkey.Latitude,
                    new MapLaunchOptions
                    {
                        Name = Monkey.Name,
                        NavigationMode = NavigationMode.None
                    });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to open map: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }
    }
}
