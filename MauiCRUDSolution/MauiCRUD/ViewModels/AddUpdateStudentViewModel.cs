using MauiCRUD.Models;
using MauiCRUD.RelayCommandFolder;
using MauiCRUD.Services;
using MauiCRUD.Views;
using System.ComponentModel;
using System.Windows.Input;

namespace MauiCRUD.ViewModels
{
    [QueryProperty(nameof(StudentDetail), "StudentDetails")]
    public class AddUpdateStudentViewModel : INotifyPropertyChanged
    {
        private StudentModel _studentDetail = new StudentModel();
        public ICommand AddStudentCommand { get; private set; }

        private readonly IStudentServices _studentServices;

        public AddUpdateStudentViewModel(IStudentServices studentServices)
        {
            _studentServices = studentServices;
            AddStudentCommand = new RelayCommand(ExecuteAddStudent, CanExecute);
        }

        public StudentModel StudentDetail
        {
            get { return _studentDetail; }
            set
            {
                if (_studentDetail != value)
                {
                    _studentDetail = value;
                    OnPropertyChanged(nameof(StudentDetail));
                }
            }
        }

        private async void ExecuteAddStudent()
        {
            int response = -1;

            if(StudentDetail.StudentID > 0)
            {
                response = await _studentServices.UpdateStudent(StudentDetail);
                await Shell.Current.DisplayAlert("Message", "Student Updated!", "Ok");
                await Shell.Current.GoToAsync(nameof(StudentListPage));
            }
            else
            {
                response = await _studentServices.AddStudent(new Models.StudentModel
                {
                    FirstName = StudentDetail.FirstName,
                    LastName = StudentDetail.LastName,
                    Email = StudentDetail.Email
                });

                if(response > 0)
                {
                    await Shell.Current.DisplayAlert("Message", "New Student Added!", "Ok");
                    StudentDetail.FirstName = "";
                    StudentDetail.LastName = "";
                    StudentDetail.Email = "";
                    await Shell.Current.GoToAsync(nameof(StudentListPage));
                }
                else
                {
                    await Shell.Current.DisplayAlert("Message", "Student Not Added!", "Ok");
                    await Shell.Current.GoToAsync(nameof(StudentListPage));
                }

            }
        }

        private bool CanExecute()
        {
            return true;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
