
using StudentApp.Models;

namespace StudentApp.Repo;

public interface IRepo
{
    Task<List<Students>> Get();
    Task<Students> GetAsId(int id);
    Task<Students> AddStudent(Students students);
    Task<Students> UpdateStudent(int id, Students students);
    Task<bool?> DeleteStudent(int id);
}
