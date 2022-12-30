
using StudentApp.Models;
using StudentApp.Repo;

namespace StudentApp.Services;

public class Service : IService
{ 

    private readonly IRepo _repo;

    public Service(IRepo repo)
    {
        _repo = repo;
    }

    public async Task<List<Students>> Get() => await _repo.Get();
    public async Task<Students> GetAsId(int id) => await _repo.GetAsId(id);
    public async Task<Students> AddStudent(Students students) => await _repo.AddStudent(students);
    public async Task<Students> UpdateStudent(int id, Students students) => await _repo.UpdateStudent(id, students);
    public async Task<bool?> DeleteStudent(int id) => await _repo.DeleteStudent(id);
}
