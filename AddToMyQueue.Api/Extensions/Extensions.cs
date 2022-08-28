using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddToMyQueue.Api.Extensions
{
    public static class Extensions
    {
        public static bool IsNullOrEmpty(this string str)
        {
            return (str == null || str == string.Empty);
        }
    }
}
