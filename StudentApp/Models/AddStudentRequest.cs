namespace StudentApp.Models
{
    public class AddStudentRequest
    {
        public AddStudentRequest()
        {
        }
        public AddStudentRequest(Students students)
        {
            UserName= students.UserName;
            FirstName= students.FirstName;
            LastName= students.LastName;   
            Address= students.Address;  
            TlfNo= students.TlfNo; 
            School= students.School;
        }

        public string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        public string? Address { get; set; }
        public string? TlfNo { get; set; }
        public string? School { get; set; }

        public virtual Students ToStudent(AddStudentRequest addStudentRequest)
        {
            return new Students 
            {
                UserName = addStudentRequest.UserName,
                FirstName = addStudentRequest.FirstName,
                LastName = addStudentRequest.LastName,
                Address = addStudentRequest.Address,
                TlfNo = addStudentRequest.TlfNo,
                School = addStudentRequest.School
            };
        }
    }
}
