using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace XCodeFramework.Web.Core.Mvc
{
    public class JsonNetResult : System.Web.Mvc.JsonResult
    {
        public JsonSerializerSettings JsonSerializerSettings { get; set; }

        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            var response = context.HttpContext.Response;

            response.ContentType = !String.IsNullOrEmpty(ContentType) ? ContentType : "application/json";

            if (ContentEncoding != null)
            {
                response.ContentEncoding = ContentEncoding;
            }

            JsonSerializerSettings jsonSerializerSettings = this.JsonSerializerSettings ?? new JsonSerializerSettings();
            jsonSerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            jsonSerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm;ss"; //日期格式
            jsonSerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();// 首字母小写的设置
            var serializedObject = JsonConvert.SerializeObject(Data, Formatting.None, jsonSerializerSettings);

            response.Write(serializedObject);
        }
    }
}
