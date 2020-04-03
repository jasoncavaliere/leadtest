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
		[Route("AuditTraillist/{Id?}")]
		[Route("Home/AuditTraillist/{Id?}")]

		public async Task<IActionResult> AuditTraillist()
		{

			// Create page object
			AuditTrail_List = new _AuditTrail_List(this);
			AuditTrail_List.Cache = _cache;

			// Run the page
			return await AuditTrail_List.Run();
		}

		// add
		[Route("AuditTrailadd/{Id?}")]
		[Route("Home/AuditTrailadd/{Id?}")]

		public async Task<IActionResult> AuditTrailadd()
		{

			// Create page object
			AuditTrail_Add = new _AuditTrail_Add(this);

			// Run the page
			return await AuditTrail_Add.Run();
		}

		// view
		[Route("AuditTrailview/{Id?}")]
		[Route("Home/AuditTrailview/{Id?}")]

		public async Task<IActionResult> AuditTrailview()
		{

			// Create page object
			AuditTrail_View = new _AuditTrail_View(this);

			// Run the page
			return await AuditTrail_View.Run();
		}
	}
}