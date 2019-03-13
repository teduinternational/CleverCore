using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CleverCore.Application.Interfaces;
using CleverCore.Application.ViewModels.System;
using CleverCore.Utilities.Constants;
using CleverCore.WebApp.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CleverCore.WebApp.Areas.Admin.Components
{
    public class SideBarViewComponent : ViewComponent
    {
        private readonly IFunctionService _functionService;

        public SideBarViewComponent(IFunctionService functionService)
        {
            _functionService = functionService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var roles = ((ClaimsPrincipal)User).GetSpecificClaim("Roles");
            List<FunctionViewModel> functions;
            if (roles.Split(";").Contains(CommonConstants.AppRole.AdminRole))
            {
                functions = await _functionService.GetAll(string.Empty);
            }
            else
            {
                //TODO: Get by permission
                functions = await _functionService.GetAll(string.Empty);
            }
            return View(functions);
        }
    }
}