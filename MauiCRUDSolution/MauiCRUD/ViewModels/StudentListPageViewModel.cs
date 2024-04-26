
using System.Collections.ObjectModel;
using System.Windows.Input;
using MauiCRUD.Models;
using MauiCRUD.RelayCommandFolder;
using MauiCRUD.Services;
using MauiCRUD.Views;

namespace MauiCRUD.ViewModels
{
    public class StudentListPageViewModel
    {
        //Properties and Fields
        public ObservableCollection<StudentModel> Students { get; set; } = new ObservableCollection<StudentModel>();
        public ICommand GotoAddUpdateStudentCommand { get; private set; }
        public ICommand GetAllStudentsCommand { get; private set; }
        public ICommand DisplayActionCommand { get; private set; }

        private readonly IStudentServices _studentServices;

        //Class Constructor
        public StudentListPageViewModel(IStudentServices studentServices)
        {
            GotoAddUpdateStudentCommand = new RelayCommand(ExecuteGotoAddUpdateStudent, CanExecute);

            _studentServices = studentServices;

            GetAllStudentsCommand = new RelayCommand(ExecuteGetAllStudent, CanExecute);

            DisplayActionCommand = new RelayCommandT<StudentModel>(ExecuteDisplayAction, CanExecute);
        }

        //Methods

        private async void ExecuteGetAllStudent()
        {
            var studentList = await _studentServices.GetAllStudents();
            if(studentList?.Count > 0)
            {
                Students.Clear();

                foreach (var student in studentList)
                {
                    Students.Add(student);
                }
            }

        }

        private async void ExecuteDisplayAction(StudentModel studentModel)
        {
            var response = await AppShell.Current.DisplayActionSheet("Select Option","Cancel",null,"Edit","Delete");

            if(response == "Edit")
            {
                var navParam = new Dictionary<string, object>();
                navParam.Add("StudentDetails", studentModel);

                await Shell.Current.GoToAsync(nameof(AddUpdateStudent), navParam);
            }
            else if(response == "Delete")
            {
                var delResponse = await _studentServices.DeleteStudent(studentModel);
                if(delResponse > 0)
                {
                    ExecuteGetAllStudent();
                }
            }
        }
        private async void ExecuteGotoAddUpdateStudent()
        {
            await Shell.Current.GoToAsync(nameof(AddUpdateStudent));
        }

        private bool CanExecute()
        {
            return true;
        }
    }

}
