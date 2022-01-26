using System.Collections.Generic;
using System.Web.Http;
using NusPay.BLL.Security;
using NusPay.Security.Domain.Model;

namespace NusPay.Web.Areas.Administrator.Controllers
{

    public class ApiRoleController : ApiController
    {
        private readonly IRoleService _roleService;

        public ApiRoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public IEnumerable<RoleModel> RoleList()
        {
            return _roleService.GetRoleList();
        }
    }
}
