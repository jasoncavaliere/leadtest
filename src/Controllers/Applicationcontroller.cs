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
		[Route("Applicationlist/{ApplicationId?}")]
		[Route("Home/Applicationlist/{ApplicationId?}")]

		public async Task<IActionResult> Applicationlist()
		{

			// Create page object
			Application_List = new _Application_List(this);
			Application_List.Cache = _cache;

			// Run the page
			return await Application_List.Run();
		}

		// view
		[Route("Applicationview/{ApplicationId?}")]
		[Route("Home/Applicationview/{ApplicationId?}")]

		public async Task<IActionResult> Applicationview()
		{

			// Create page object
			Application_View = new _Application_View(this);

			// Run the page
			return await Application_View.Run();
		}

		// edit
		[Route("Applicationedit/{ApplicationId?}")]
		[Route("Home/Applicationedit/{ApplicationId?}")]

		public async Task<IActionResult> Applicationedit()
		{

			// Create page object
			Application_Edit = new _Application_Edit(this);

			// Run the page
			return await Application_Edit.Run();
		}

		// search
		[Route("Applicationsrch")]
		[Route("Home/Applicationsrch")]

		public async Task<IActionResult> Applicationsrch()
		{

			// Create page object
			Application_Search = new _Application_Search(this);

			// Run the page
			return await Application_Search.Run();
		}
	}
}