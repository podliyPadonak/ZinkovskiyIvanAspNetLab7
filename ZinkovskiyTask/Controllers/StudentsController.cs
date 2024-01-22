using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ZinkovskiyTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : Controller
    {
        private static List<Student> _students = new List<Student>
        {
            new Student { Id = 1, Name = "Ivan Zinkovskiy", Age = 20 },
            new Student { Id = 2, Name = "Oleg Stelmakh", Age = 55  }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(_students);
        }

        [HttpGet("{id}")]
        public ActionResult<Student> GetStudentById(int id)
        {
            var student = _students.FirstOrDefault(s => s.Id == id);
            if (student == null)
                return NotFound();

            return Ok(student);
        }

        [HttpPost]
        public ActionResult<Student> CreateStudent(Student newSt)
        {
            newSt.Id = _students.Count + 1;
            _students.Add(newSt);
            return CreatedAtAction(nameof(GetStudentById), new { id = newSt.Id }, newSt);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateStudent(int id, Student updatedStudent)
        {
            var existingStudent = _students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
                return NotFound();

            existingStudent.Name = updatedStudent.Name;
            existingStudent.Age = updatedStudent.Age;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(int id)
        {
            var existingStudent = _students.FirstOrDefault(s => s.Id == id);
            if (existingStudent == null)
                return NotFound();

            _students.Remove(existingStudent);
            return NoContent();
        }
    }
}
