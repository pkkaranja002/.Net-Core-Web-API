using APIDemo.Data;
using APIDemo.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {

        private  ApplicationDbContext _db;

        public StudentsController(ApplicationDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public List<StudentEntity> GetAllStudents() 
        {
            return _db.StudentsRegister.ToList();
        }

        [HttpGet("GetStudentsById")]

        public ActionResult<StudentEntity>GetStudentDetails (Int32 Id)
        {
            if (Id == 0)
            {
                return BadRequest();
            }

            var studentDetails = _db.StudentsRegister.FirstOrDefault(x => x.Id == Id);

            if (studentDetails == null)
            {
                return NotFound();
            }
            return studentDetails;
        }

        [HttpPost]

        public ActionResult<StudentEntity> AddStudent([FromBody] StudentEntity student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.StudentsRegister.Add(student);
            _db.SaveChanges();

            return Ok(student);
        }

        [HttpPost("UpdateStudentDetails")]

        public ActionResult<StudentEntity> UpdateStudent(Int32 Id,[FromBody] StudentEntity studentDetails)
        {
            if (studentDetails == null)
            {
                return BadRequest(studentDetails);
            }
            var StudentDetails = _db.StudentsRegister.FirstOrDefault(x => x.Id == Id);
            if(studentDetails == null)
            {
                return NotFound();
            }

            StudentDetails.Name = studentDetails.Name;
            StudentDetails.Age = studentDetails.Age;
            StudentDetails.Standard = studentDetails.Standard;
            StudentDetails.EmailAddress = studentDetails.EmailAddress;

            _db.SaveChanges();
            
            return Ok(studentDetails);
        }


        [HttpPut("DeleteStudent")]

        public ActionResult<StudentEntity> Delete(Int32 Id)
        {
            var StudentDetails = _db.StudentsRegister.FirstOrDefault(x => x.Id == Id);
            if (StudentDetails == null)
            {
                return NotFound();
            }
            _db.Remove(StudentDetails);


            _db.SaveChanges();

            return NoContent();
        }
    }
}
