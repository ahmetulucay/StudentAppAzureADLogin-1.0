
using Microsoft.EntityFrameworkCore;
using StudentApp.Data;
using StudentApp.Models;

namespace StudentApp.Repo;

public class Repo : IRepo
{
    private readonly StudentAppContext _context;
    private readonly ILogger<StudentAppContext> _logger;

    public Repo(StudentAppContext context, ILogger<StudentAppContext> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Students>> Get() =>
        await _context.Student.Include(s => s.AddressStudent)
        .Include(s => s.EmailAddressStudent)
        .Include(s => s.ImageStudent)
        .Include(s => s.PhoneStudent).ToListAsync();

    public async Task<Students> GetAsId(int id) =>
        await _context.Student.Include(s => s.AddressStudent).Include(s => s.EmailAddressStudent).Include(s => s.ImageStudent).Include(s => s.PhoneStudent).FirstOrDefaultAsync(s => s.StudentId == id);

    public async Task<Students> AddStudent(Students students)
    {
        await _context.Student.AddAsync(students);
        await _context.SaveChangesAsync();
        return students;
    }

    public async Task<Students> UpdateStudent(int id, Students students)
    {
        var result = await _context.Student.FirstOrDefaultAsync(s => s.StudentId == id);
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
            result.ImageStudent = students.ImageStudent;

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
            //Delete cascade in Db
            var addresses = _context.StudentAddress.Find(sa => sa.StudentsId == id).ToList();
            var images = _context.StudentImage.Find(si => si.StudentsId == id).ToList();
            var phones = _context.StudentPhoneNo.Find(sp => sp.StudentsId == id).ToList();
            var mails = _context.StudentEmailAddress.Find(se => se.StudentsId == id).ToList();

            _context.StudentAddress.RemoveRange(addresses);
            _context.StudentImage.RemoveRange(images);
            _context.StudentPhoneNo.RemoveRange(phones);
            _context.StudentEmailAddress.RemoveRange(mails);

            _context.Student.Remove(result);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "We caught a deleting student data ('db cascade') from context.");
            return false;
        }
    }
}