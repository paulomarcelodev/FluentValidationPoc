namespace Api;

public class DataContracts
{
    public class RegisterRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    public class RegisterResponse
    {
        public long Id { get; set; }
    }

    public class EditPersonalInfoRequest
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }

    public class EnrollRequest
    {
        public CourseEnrollmentDto[] Enrollments { get; set; }
    }

    public class CourseEnrollmentDto
    {
        public string Course { get; set; }
        public string Grade { get; set; }
    }

    public class GetResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public CourseEnrollmentDto[] Enrollments { get; set; }
    }
}
