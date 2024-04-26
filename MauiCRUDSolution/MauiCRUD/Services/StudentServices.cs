using MauiCRUD.Models;
using SQLite;

namespace MauiCRUD.Services
{
    public class StudentServices : IStudentServices
    {
        private SQLiteAsyncConnection _dbConnection;
        public StudentServices()
        {
            SetUpDb();
        }

        private async void SetUpDb()
        {
            if (this._dbConnection == null)
            {
                string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Student.db3");
                _dbConnection = new SQLiteAsyncConnection(dbPath);
                await _dbConnection.CreateTableAsync<StudentModel>();
            }
        }

        public Task<int> AddStudent(StudentModel studentModel)
        {
            return _dbConnection.InsertAsync(studentModel);
        }

        public Task<int> DeleteStudent(StudentModel studentModel)
        {
            return _dbConnection.DeleteAsync(studentModel);
        }

        public async Task<List<StudentModel>> GetAllStudents()
        {
            var studentList = await _dbConnection.Table<StudentModel>().ToListAsync();
            return studentList;
        }

        public Task<int> UpdateStudent(StudentModel studentModel)
        {
            return _dbConnection.UpdateAsync(studentModel);
        }
    }

}
