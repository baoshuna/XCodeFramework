using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using XCodeFramework.Core.Domain.Common;
using XCodeFramework.Core.Domain.Students;
using XCodeFramework.Core.Helpers;
using XCodeFramework.Service;
using XCodeFramework.Service.Services.Students;
using XCodeFramework.Web.Core.Mvc;
using XCodeFramework.Web.Infrastructure;
using XCodeFramework.Web.Models.Student;

namespace XCodeFramework.Web.Controllers
{
    public class StudentController : BaseController
    {
        private readonly IStudentService studentService;
        private readonly IMapper mapper;
        private readonly MapperConfiguration mapperConfiguration;

        public StudentController(IStudentService studentService,IMapper mapper, MapperConfiguration mapperConfiguration)
        {
            this.studentService = studentService;
            this.mapper = mapper;
            this.mapperConfiguration = mapperConfiguration;
        }

        // [ValidateAntiForgeryToken]
        public ActionResult Index()
        {
            var result = studentService.GetList();
            var dto = mapper.Map<List<Student>, List<StudentModel>>(result);
            var n = result.AsQueryable().ProjectTo<StudentModel>(mapperConfiguration).ToList();
            return View(dto);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(StudentModel student)
        {
            student.Id = Guid.NewGuid().ToString();
            if (ModelState.IsValid)
            {
                var result = mapper.Map<StudentModel, Student>(student);
                studentService.Add(result);
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        public async Task<FileResult> ExportAsync()
        {
            var data = GetData();

            Stopwatch watch = new Stopwatch();
            watch.Start();
            var exportTask = Task.Run(() =>
            {
                ExcelHelper helper = new ExcelHelper();
                var fileStream = helper.ExportToExcel(data);
                return fileStream;
            });
            

            for (int i = 0; i < 200; i++)
            {
                var result = studentService.GetList();
            }

            var xx = await exportTask;

            watch.Stop();
            var xxx = watch.ElapsedMilliseconds;

            return File(xx, "application/vnd.ms-excel", "用户信息.xlsx");
        }

        public FileResult Export()
        {
            var data = GetData();

            Stopwatch watch = new Stopwatch();
            watch.Start();

            ExcelHelper helper = new ExcelHelper();
            var fileStream = helper.ExportToExcel(data);

            for (int i = 0; i < 200; i++)
            {
                var result = studentService.GetList();
            }

            watch.Stop();
            var xxx = watch.ElapsedMilliseconds;

            return File(fileStream, "application/vnd.ms-excel", "用户信息.xlsx");
        }

        public ActionResult Import()
        {
            ExcelHelper helper = new ExcelHelper(@"C:\Users\11301\Downloads\Demo.xlsx");
            var x = helper.ExcelToList<StudentModel>();
            return Json("");
        }

        private List<StudentModel> GetData()
        {
            var list = new List<StudentModel>();
            var result = studentService.GetList();
            var dto = mapper.Map<List<Student>, List<StudentModel>>(result);

            for (int i = 0; i < 20000; i++)
            {
                list.AddRange(dto);
            }
            return list;
        }

        #region 测试方法
        public ActionResult Index2()
        {
            studentService.TestBlog();
            // studentService.TestCRUD();

            //studentService.TestTransaction();

            //studentService.PerformanceTest_Query();

            //studentService.PerformanceTest_Insert();

            #region AutoMapper相关
            // var studentDto = students.Select(it => new StudentModel { Id = it.Id, Name = it.Name });
            // return View(studentDto);
            // var result = students.AsQueryable().ProjectTo<IEnumerable<StudentModel>>();

            //var studentModels = mapper.Map<IEnumerable<Student>, IEnumerable<StudentModel>>(students);
            #endregion
            return View();
        }

        public JsonResult GetStudents()
        {
            return Json(studentService.GetList(), JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}