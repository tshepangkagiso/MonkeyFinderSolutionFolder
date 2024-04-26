using MauiCRUD.RelayCommandFolder;
using MauiCRUD.Views;
using System.Windows.Input;

namespace MauiCRUD.ViewModels
{
    public class MainPageViewModel
    {
        public ICommand GotoStudentListPageCommand {  get; private set; }
        public MainPageViewModel()
        {
            GotoStudentListPageCommand = new RelayCommand(ExecuteGotoStudentListPage, CanExecute);
        }

        private async void ExecuteGotoStudentListPage()
        {
            // Assuming Shell.Current is a valid Shell instance
            await Shell.Current.GoToAsync(nameof(StudentListPage));
        }

        private bool CanExecute()
        {
            return true;
        }
    }
}
