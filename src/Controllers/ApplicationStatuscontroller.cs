// ASP.NET Maker 2020
// Copyright (c) 2019 e.World Technology Limited. All rights reserved.

using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using AspNetMaker2020.Models;
using static AspNetMaker2020.Models.project1;

// Controllers
namespace AspNetMaker2020.Controllers
{

	// Partial class
	public partial class HomeController : Controller
	{

		// list
		[Route("ApplicationStatuslist/{Id?}")]
		[Route("Home/ApplicationStatuslist/{Id?}")]

		public async Task<IActionResult> ApplicationStatuslist()
		{

			// Create page object
			ApplicationStatus_List = new _ApplicationStatus_List(this);
			ApplicationStatus_List.Cache = _cache;

			// Run the page
			return await ApplicationStatus_List.Run();
		}

		// add
		[Route("ApplicationStatusadd/{Id?}")]
		[Route("Home/ApplicationStatusadd/{Id?}")]

		public async Task<IActionResult> ApplicationStatusadd()
		{

			// Create page object
			ApplicationStatus_Add = new _ApplicationStatus_Add(this);

			// Run the page
			return await ApplicationStatus_Add.Run();
		}

		// view
		[Route("ApplicationStatusview/{Id?}")]
		[Route("Home/ApplicationStatusview/{Id?}")]

		public async Task<IActionResult> ApplicationStatusview()
		{

			// Create page object
			ApplicationStatus_View = new _ApplicationStatus_View(this);

			// Run the page
			return await ApplicationStatus_View.Run();
		}

		// edit
		[Route("ApplicationStatusedit/{Id?}")]
		[Route("Home/ApplicationStatusedit/{Id?}")]

		public async Task<IActionResult> ApplicationStatusedit()
		{

			// Create page object
			ApplicationStatus_Edit = new _ApplicationStatus_Edit(this);

			// Run the page
			return await ApplicationStatus_Edit.Run();
		}

		// delete
		[Route("ApplicationStatusdelete/{Id?}")]
		[Route("Home/ApplicationStatusdelete/{Id?}")]

		public async Task<IActionResult> ApplicationStatusdelete()
		{

			// Create page object
			ApplicationStatus_Delete = new _ApplicationStatus_Delete(this);

			// Run the page
			return await ApplicationStatus_Delete.Run();
		}
	}
}