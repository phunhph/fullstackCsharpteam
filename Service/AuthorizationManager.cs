
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using X.PagedList;

namespace fullstackCsharp.Service
{
   enum RoleNameId
    {
        admin = 1,//tất cả
        EM = 2,//quản lý nhân sự
        ADM = 3,//quản lý điểm danh
        SM = 4,//quản lý lương
        Staff = 5//nhân viên
    }
    enum CheckStatus
    {
        open = 1,
        close =0
    }
    //check đăng nhập cho tất cả các action
    public class AuthorizeFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if(context.HttpContext.Session.GetString("username") == null)
            {
                context.Result = new RedirectToActionResult("Login", "Account", null);
            }
        }
        
    }

    public class AuthorizationManager
    {
        public static bool CkeckUserRole(HttpContext httpContext, params int[] roleIds)
        {
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
