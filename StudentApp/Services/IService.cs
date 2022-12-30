
using StudentApp.Models;

namespace StudentApp.Services
{
    public interface IService
    {
        Task<List<Students>> Get();
        Task<Students> GetAsId(int id);
        Task<Students> AddStudent(Students students);
        Task<Students> UpdateStudent(int id, Students students);
        Task<bool?> DeleteStudent(int id);
    }
}
