using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;
using WebForm2.Data;
using WebForm2.Models;
using WebForm2.Models.Domain;

namespace WebForm2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly StudentDbContext dbContext;

        public StudentsController(StudentDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        //for displaying table format 
        [HttpGet]
        public IActionResult Index()
        {
            var students = dbContext.Students.ToList();
            return View(students);
        }
        //for adding information
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel model)
        {
            var student = new Student()
            {
                Id = new int(),
                Name = model.Name,
                Email = model.Email,
                Phone = model.Phone,
                DateOfBirth = model.DateOfBirth,
            };
            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        //for making view
        [HttpGet]
        public async Task<IActionResult> View(int Id)
        {
            var student = await dbContext.Students.FindAsync(Id);
            if(student != null)
            {
                var update = new UpdateStudentViewModel()
                {
                    Id = student.Id,
                    Name = student.Name,
                    Email = student.Email,
                    Phone = student.Phone,
                    DateOfBirth = student.DateOfBirth
                };
                return await Task.Run(()=> View("view",update));
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateStudentViewModel model)
        {
            var student = await dbContext.Students.FindAsync(model.Id);
            if(student != null)
            {
                student.Name = model.Name;
                student.Email = model.Email;
                student.Phone = model.Phone;
                student.DateOfBirth = model.DateOfBirth;

                await dbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await dbContext.Students.FindAsync(id);
            if( student != null)
            {
                dbContext.Students.Remove(student);
                dbContext.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
