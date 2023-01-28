
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

    public async Task<List<Students>> Get() =>
        await _context.Student.Include(s => s.PhoneStudent).Include(s => s.EmailAddressStudent).Include(s => s.AddressStudent).ToListAsync();

    public async Task<Students> GetAsId(int id) =>
        await _context.Student.Include(s => s.PhoneStudent).Include(s => s.EmailAddressStudent).Include(s => s.AddressStudent).FirstOrDefaultAsync(s => s.StudentId == id);

    public async Task<Students> AddStudent(Students students)
    {
        await _context.Student.AddAsync(students);
        await _context.SaveChangesAsync();
        return students;
    }

    public async Task<Students> UpdateStudent(int id, Students students)
    {
        var result = await _context.Student.Include(s => s.PhoneStudent).Include(s => s.EmailAddressStudent).Include(s => s.AddressStudent).FirstOrDefaultAsync(s => s.StudentId == id);
        if (result != null)
        {
            result.UserName = students.UserName;
            result.FirstName = students.FirstName;
            result.SecondName = students.SecondName;
            result.LastName = students.LastName;
            result.School = students.School;
            result.RegistrationDate= students.RegistrationDate;
            result.PhoneStudent = students.PhoneStudent;
            result.EmailAddressStudent= students.EmailAddressStudent;
            result.AddressStudent = students.AddressStudent;

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
        catch (Exception)
        {
            return false;
        }
    }
}