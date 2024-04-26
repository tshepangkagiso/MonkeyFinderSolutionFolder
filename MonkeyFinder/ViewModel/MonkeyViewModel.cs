using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Networking;
using MonkeyFinder.Model;
using MonkeyFinder.Services;
using MonkeyFinder.View;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MonkeyFinder.ViewModel
{
    public partial class MonkeyViewModel : BaseViewModel
    {
        public ObservableCollection<Monkey> Monkeys { get; } = new();

        MonkeyService monkeyService;

        IConnectivity connectivity;
        IGeolocation geolocation;
        public MonkeyViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
        {
            Title = "Monkey Finder";
            this.monkeyService = monkeyService;
            this.connectivity = connectivity;
            this.geolocation = geolocation;
        }

        [RelayCommand]
        async Task GetClosetMonkeyAsync()
        {
            if(IsBusy || Monkeys.Count == 0) return;

            try
            {
                var location = await geolocation.GetLastKnownLocationAsync();
                if(location is null)
                {
                    location = await geolocation.GetLocationAsync(
                        new GeolocationRequest
                        {
                            DesiredAccuracy = GeolocationAccuracy.Medium,
                            Timeout = TimeSpan.FromSeconds(30),

                        });
                }

                if(location is null) return;

                var first = Monkeys.OrderBy(m => location.CalculateDistance(m.Latitude, m.Longitude, DistanceUnits.Miles)).FirstOrDefault();
                if(first is null) return;
                await Shell.Current.DisplayAlert("Closest Monkey", $"{first.Name} in {first.Location}", "OK");
            }
            catch(Exception ex)
            {
                Debug.WriteLine($"Unable to get closest monkey: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {

            }
        }

        [RelayCommand]
        async Task GotoDetailsAsync(Monkey monkey)
        {
            if (monkey is null)
                return;

            //await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?monkeyId={monkey.Name}", true);

            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}",true, new Dictionary<string, object>{{"Monkey", monkey }});
        }

        [RelayCommand]
        async Task GetMonkeysAsync()
        {
            if (IsBusy)
                return;

            try
            {
                //checking connectivity
                if (connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("Internet Issue!", "No internet!", "OK");
                    return;
                }
                //getting monkeys
                IsBusy = true;
                var monkeys = await monkeyService.GetMonkeys();

                if (Monkeys.Count != 0)
                    Monkeys.Clear();

                foreach (var monkey in monkeys)
                    Monkeys.Add(monkey);

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get monkeys: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }

        }


    }
}
