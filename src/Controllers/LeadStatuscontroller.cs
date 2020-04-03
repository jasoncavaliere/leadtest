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
		[Route("LeadStatuslist/{Id?}")]
		[Route("Home/LeadStatuslist/{Id?}")]

		public async Task<IActionResult> LeadStatuslist()
		{

			// Create page object
			LeadStatus_List = new _LeadStatus_List(this);
			LeadStatus_List.Cache = _cache;

			// Run the page
			return await LeadStatus_List.Run();
		}

		// add
		[Route("LeadStatusadd/{Id?}")]
		[Route("Home/LeadStatusadd/{Id?}")]

		public async Task<IActionResult> LeadStatusadd()
		{

			// Create page object
			LeadStatus_Add = new _LeadStatus_Add(this);

			// Run the page
			return await LeadStatus_Add.Run();
		}

		// view
		[Route("LeadStatusview/{Id?}")]
		[Route("Home/LeadStatusview/{Id?}")]

		public async Task<IActionResult> LeadStatusview()
		{

			// Create page object
			LeadStatus_View = new _LeadStatus_View(this);

			// Run the page
			return await LeadStatus_View.Run();
		}

		// edit
		[Route("LeadStatusedit/{Id?}")]
		[Route("Home/LeadStatusedit/{Id?}")]

		public async Task<IActionResult> LeadStatusedit()
		{

			// Create page object
			LeadStatus_Edit = new _LeadStatus_Edit(this);

			// Run the page
			return await LeadStatus_Edit.Run();
		}

		// delete
		[Route("LeadStatusdelete/{Id?}")]
		[Route("Home/LeadStatusdelete/{Id?}")]

		public async Task<IActionResult> LeadStatusdelete()
		{

			// Create page object
			LeadStatus_Delete = new _LeadStatus_Delete(this);

			// Run the page
			return await LeadStatus_Delete.Run();
		}
	}
}