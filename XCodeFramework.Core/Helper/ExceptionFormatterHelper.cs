using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lavazza.Infrastructure.Exceptions
{
    public class ExceptionFormatterHepler
    {
        public static string Format(Exception exception)
        {
            if (exception == null)
            {
                return null;
            }

            StringBuilder builder = new StringBuilder();

            builder.AppendLine(exception.Message);
            builder.AppendLine("StackTrace");
            builder.AppendLine(exception.StackTrace);

            Exception innerException = exception.InnerException;
            while (innerException != null)
            {
                builder.AppendLine("InnerException");
                builder.AppendLine(Format(innerException));
                innerException = innerException.InnerException;
            }

            return builder.ToString();
        }
    }
}