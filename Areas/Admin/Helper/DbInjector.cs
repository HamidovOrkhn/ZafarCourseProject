using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZafarCoursesWebApp.Areas.Admin.Data;
using ZafarCoursesWebApp.Areas.Admin.Models;

namespace ZafarCoursesWebApp.Areas.Admin.Helper
{
    public static class DbInjector
    {
        public static bool IsAuthenticated(this AdminContext context,int id, string token)
        {
            User user = context.Users.Where(a => a.Id == id).FirstOrDefault();
            if (user is object)
            {
                if (user.Token == token)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
