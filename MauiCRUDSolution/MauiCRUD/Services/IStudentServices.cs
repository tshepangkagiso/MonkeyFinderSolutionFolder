using MauiCRUD.Models;


namespace MauiCRUD.Services
{
    public interface IStudentServices
    {
        Task<List<StudentModel>> GetAllStudents();
        Task<int> AddStudent(StudentModel studentModel);
        Task<int> DeleteStudent(StudentModel studentModel);
        Task<int> UpdateStudent(StudentModel studentModel);

    }
}
