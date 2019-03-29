using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Helpers
{
    public class PasswordHelper
    {
        /// <summary>
        /// max is 32
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GenerateRandomPassword(int length)
        {
            if (length > 32)
            {
                length = 32;
            }

            return Guid.NewGuid().ToString("N").Substring(0, length);
        }
    }
}
