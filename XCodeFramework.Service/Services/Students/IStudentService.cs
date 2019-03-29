using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCodeFramework.Core.Domain.Common;

namespace XCodeFramework.Service.Services.Students
{
    public interface IStudentService
    {
        List<Student> GetList();

        bool Add(Student student);

        #region 测试方法
        void TestTransaction();

        void TestCRUD();

        Task TestCrudAsync();

        void PerformanceTest_Query();

        void PerformanceTest_Insert();

        void TestBlog();
        #endregion
    }
}
