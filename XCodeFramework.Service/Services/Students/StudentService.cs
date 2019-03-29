using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCodeFramework.Core.Cache;
using XCodeFramework.Core.Domain.Common;
using XCodeFramework.Core.Domain.Students;
using XCodeFramework.Data;
using XCodeFramework.Data.Repository;
using XCodeFramework.Data.Context;
using XCodeFramework.Core.Domain.BlogUsers;
using System.Data.SqlClient;

namespace XCodeFramework.Service.Services.Students
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> studentRepo;
        private readonly IBlogSiteRepository<BlogUser> blogUserRepo;
        private readonly ICacheManager cacheManager;
        private readonly IDataContext dataContext;

        public StudentService(IRepository<Student> studentRepo, ICacheManager cacheManager, IDataContext dataContext, IBlogSiteRepository<BlogUser> blogUserRepo)
        {
            this.studentRepo = studentRepo;
            this.cacheManager = cacheManager;
            this.dataContext = dataContext;
            this.blogUserRepo = blogUserRepo;
        }

        public List<Student> GetList()
        {
            return studentRepo.GetList().ToList();
        }

        public bool Add(Student student)
        {
            if (student == null)
            {
                return false;
            }
            var resp = studentRepo.Insert(student);
            return string.IsNullOrEmpty(resp) ? false : true;
        }

        #region 测试方法
        public void TestTransaction()
        {
            Student studentSample = new Student()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "测试事务",
                Age = 18,
                Sex = 1,
                CreatedBy = "ces",
                ModifyBy = "qwddss",
                ModifyTime = DateTime.Now.AddYears(1)
            };

            var transaction = studentRepo.BeginTransaction();

            SqlCommand cmd = new SqlCommand("", (SqlConnection)studentRepo.GetDbConnection());
            var x = cmd.ExecuteNonQuery();
            cmd.Transaction = (SqlTransaction)transaction;
            try
            {
                var aa = studentRepo.Get("xcx", transaction);
                var f = studentRepo.RecordCount("where age>@age", new { age = 12 }, transaction);
                studentRepo.Insert(studentSample, transaction);
                blogUserRepo.Execute("insert into [dbo].[Students] (id,name,age,sex) values (@id,@name,18,0)"
                , new { id = "gdsdjssnkl", name = "测试增加" + DateTime.Now.Minute }, transaction);

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                // Todo:Logging

                throw ex;
            }
        }

        public void TestCRUD()
        {
            Student studentSample = new Student()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "测试事务",
                Age = 18,
                Sex = 1,
                CreatedBy = "ces",
                ModifyBy = "qwddss",
                ModifyTime = DateTime.Now.AddYears(1)
            };

            // 查
            var a = studentRepo.Get("xcx");                                              // 通过id查单个       
            var b = studentRepo.GetList().ToList();                                      // 查全部                 
            var c = studentRepo.GetList("where name=@name", new { name = "a" }).ToList();// 根据条件查全部      
            var d = studentRepo.GetList(new { Age = 13 }).ToList();                      // 根据条件查全部  
            var e = studentRepo.RecordCount();                                           // 查全部数量            
            var f = studentRepo.RecordCount("where age>@age", new { age = 12 });         // 根据条件查数量      
            var g = studentRepo.GetListPaged(1, 2, "where age>@age", "id desc", new { age = 14 }).ToList();

            var h = studentRepo.Get<StudentDto>("select top 1 * from [dbo].[Students]");
            var i = studentRepo.Query<StudentDto>("select * from [dbo].[Students]").ToList();

            // 增
            var j = studentRepo.Insert(studentSample); // 返回新增对象的主键

            studentRepo.Execute("insert into [dbo].[Students] (id,name,age,sex) values (@id,@name,18,0)"
                , new { id = Guid.NewGuid().ToString(), name = "测试增加" + DateTime.Now.Minute });

            // 删  返回受影响的行数
            var k = studentRepo.Delete(studentSample);
            var l = studentRepo.Delete("2e0bc163-c52a-4ee0-b019-cad8cb70c067");
            var m = studentRepo.DeleteList(new { Age = 18 });
            var n = studentRepo.DeleteList("where Age > @Age", new { Age = 20 });
            //studentRepo.DeleteLogical()

            // 改  返回受影响的行数
            var o = studentRepo.Update(new Student
            {
                Id = "eww1",
                Name = "测试更新" + DateTime.Now.Minute,
                Age = 19,
                ModifyTime = DateTime.Now,
                Sex = 1
            });
        }

        public async Task TestCrudAsync()
        {
            Student studentSample = new Student()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "测试异步插入",
                Age = 18,
                Sex = 1,
                CreatedBy = "ces",
                ModifyBy = "qwddss",
                ModifyTime = DateTime.Now.AddYears(1)
            };

            await studentRepo.InsertAsync(studentSample);
        }

        public void PerformanceTest_Query()
        {
            //504条数据 watch1查了39秒，watch2查了39.8秒

            Stopwatch watch1 = new Stopwatch(); // dapper SqlMapper源生的方法
            Stopwatch watch2 = new Stopwatch(); // dapper.simpleCRUD拓展的方法

            // dapper SqlMapper源生的方法 性能测试
            watch1.Start();
            for (int i = 0; i < 100; i++)
            {
                var result1 = dataContext.Query<Student>("select * from students").ToList();
            }
            watch1.Stop();
            var x = watch1.ElapsedMilliseconds;

            // dapper.simpleCRUD 拓展的方法 性能测试
            watch2.Start();
            for (int i = 0; i < 100; i++)
            {
                var result2 = studentRepo.GetList().ToList();
            }
            watch2.Stop();
            var y = watch2.ElapsedMilliseconds;

            int xx = 19999;
        }

        public void PerformanceTest_Insert()
        {
            Student studentSample = new Student()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "测试性能",
                Age = 18,
                Sex = 0
            };

            Stopwatch watch1 = new Stopwatch(); // dapper SqlMapper源生的方法
            Stopwatch watch2 = new Stopwatch(); // dapper.simpleCRUD拓展的方法

            // dapper SqlMapper源生的方法 性能测试
            watch1.Start();
            for (int i = 0; i < 100; i++)
            {
                dataContext.Execute("insert into [dbo].[Students] (id,name,age,sex) values (@id,@name,@age,@sex)"
                , new { id = Guid.NewGuid().ToString(), name = "测试性能" , age = 18, sex = 0 });
            }
            watch1.Stop();
            var x = watch1.ElapsedMilliseconds;

            // dapper.simpleCRUD 拓展的方法 性能测试
            watch2.Start();
            for (int i = 0; i < 100; i++)
            {
                studentSample.Id = Guid.NewGuid().ToString();
                studentRepo.Insert(studentSample);
            }
            watch2.Stop();
            var y = watch2.ElapsedMilliseconds;

            var xx = 1;
        }

        public void TestBlog()
        {
            var x = blogUserRepo.GetList().ToList();
            var y = studentRepo.GetList().ToList();
            var z = 1;
        }
        #endregion
    }
}
