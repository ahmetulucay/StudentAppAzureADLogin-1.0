
using Microsoft.EntityFrameworkCore;
using StudentApp.Data;
using StudentApp.Models;

namespace StudentApp.Repo;

public class Repo : IRepo
{
    private readonly StudentAppContext _context;

    public Repo(StudentAppContext context)
    {
        _context = context;
    }

    public Task<List<Students>> Get() => 
        _context.Student.ToListAsync();

    public async Task<Students> GetAsId(int id) => 
        await _context.Student.FirstOrDefaultAsync(s => s.Id == id);

    public async Task<Students> AddStudent(Students students)
    {
        await _context.Student.AddAsync(students);
        await _context.SaveChangesAsync();
        return students;
    }

    public async Task<Students> UpdateStudent(int id, Students students)
    {
        var result = await _context.Student.FirstOrDefaultAsync(s => s.Id == id);
        if (result != null)
        {
            result.UserName = students.UserName;
            result.FirstName = students.FirstName;
            result.SecondName = students.SecondName;
            result.LastName = students.LastName;
            result.TlfNo = students.TlfNo;
            result.School = students.School;
            result.Address = students.Address;
            result.RegistrationDate= students.RegistrationDate;

            _context.Student.Update(result);
            await _context.SaveChangesAsync();
            return result;
        }
        return null;
    }   

    public async Task<bool?> DeleteStudent(int id)
    {
        var result = await _context.Student.FindAsync(id);
        if (result == null)
        {
            return null;
        }
        try
        {
            _context.Student.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }
}








