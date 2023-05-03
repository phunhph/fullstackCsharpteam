
namespace fullstackCsharp.Service
{
    enum RoleNameId
    {
        Admin = 1,
        HR = 2,
        HRM = 3,
        HRS = 4,
        User = 5
    }
    enum CheckStatus
    {
        open = 1,
        close =0
    }
    public class AuthorizationManager
    {
        public static bool CheckUserLogin(HttpContext httpContext)
        {
            if (httpContext.Session.GetString("username") == null)
            {
                //httpContext.Response.Redirect("/Account/Login");
                return false;
            }
            return true;
        }
        public static bool CkeckUserRole(HttpContext httpContext, params int[] roleIds)
        {
            
            /*if (httpContext.Session.GetString("username") == null)
            {
                httpContext.Response.Redirect("/Account/Login");
                return false;
            }*/
            string roleIdsStr = httpContext.Session.GetString("roleIds");

            List<int> userRoleIds = new List<int>();
            if (!string.IsNullOrEmpty(roleIdsStr))
            {
                userRoleIds = roleIdsStr.Split(",").Select(int.Parse).ToList() ?? new List<int>();
            }
            return roleIds.Any(r => userRoleIds.Contains(r));
        }

    }
}
