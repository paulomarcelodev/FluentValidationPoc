using Microsoft.AspNetCore.Mvc;

namespace Api;

[Route("api/students")]
public class StudentsController : Controller
{
    private readonly StudentRepository _studentRepository;
    private readonly CourseRepository _courseRepository;

    public StudentsController(StudentRepository studentRepository, CourseRepository courseRepository)
    {
        _studentRepository = studentRepository;
        _courseRepository = courseRepository;
    }

    [HttpPost]
    public IActionResult Register([FromBody] DataContracts.RegisterRequest request)
    {
        var validator = new RegisterRequestValidator();
        var validationResult = validator.Validate(request);
        if (validationResult.IsValid == false)
        {
            return BadRequest(validationResult.Errors.First().ErrorMessage);
        }

        var addresses = request.Addresses
            .Select(x => new Address(x.State, x.City, x.Street, x.ZipCode))
            .ToArray();
        var student = new Student(request.Email, request.Name, addresses);
        _studentRepository.Save(student);

        var response = new DataContracts.RegisterResponse
        {
            Id = student.Id
        };
        return Ok(response);
    }

    [HttpPut("{id:long}")]
    public IActionResult EditPersonalInfo(long id, [FromBody] DataContracts.EditPersonalInfoRequest request)
    {
        var validator = new EditPersonalInfoRequestValidator();
        var validationResult = validator.Validate(request);
        if (validationResult.IsValid == false)
        {
            return BadRequest(validationResult.Errors.First().ErrorMessage);
        }

        var addresses = request.Addresses
            .Select(x => new Address(x.State, x.City, x.Street, x.ZipCode))
            .ToArray();
        var student = _studentRepository.GetById(id);
        if (student is null) return NotFound();
        student.EditPersonalInfo(request.Name, addresses);
        _studentRepository.Save(student);

        return Ok();
    }

    [HttpPost("{id:long}/enrollments")]
    public IActionResult Enroll(long id, [FromBody] DataContracts.EnrollRequest request)
    {
        var student = _studentRepository.GetById(id);
        if (student is null) return NotFound();

        foreach (var enrollmentDto in request.Enrollments)
        {
            var course = _courseRepository.GetByName(enrollmentDto.Course);
            if (course is null) return NotFound();
            var grade = Enum.Parse<Grade>(enrollmentDto.Grade);

            student.Enroll(course, grade);
        }

        return Ok();
    }

    [HttpGet("{id:long}")]
    public IActionResult Get(long id)
    {
        var student = _studentRepository.GetById(id);
        if (student is null) return NotFound();

        var response = new DataContracts.GetResponse
        {
            Addresses = student.Addresses.Select(x => new DataContracts.AddressDto
                { State = x.State, City = x.City, Street = x.Street, ZipCode = x.ZipCode, }).ToArray(),
            Email = student.Email!,
            Name = student.Name!,
            Enrollments = student.Enrollments.Select(x => new DataContracts.CourseEnrollmentDto
            {
                Course = x.Course.Name,
                Grade = x.Grade.ToString()
            }).ToArray()
        };
        return Ok(response);
    }
}
