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
		[Route("_Leadslist/{LeadId?}")]
		[Route("Home/_Leadslist/{LeadId?}")]

		public async Task<IActionResult> _Leadslist()
		{

			// Create page object
			_Leads_List = new __Leads_List(this);
			_Leads_List.Cache = _cache;

			// Run the page
			return await _Leads_List.Run();
		}

		// view
		[Route("_Leadsview/{LeadId?}")]
		[Route("Home/_Leadsview/{LeadId?}")]

		public async Task<IActionResult> _Leadsview()
		{

			// Create page object
			_Leads_View = new __Leads_View(this);

			// Run the page
			return await _Leads_View.Run();
		}

		// edit
		[Route("_Leadsedit/{LeadId?}")]
		[Route("Home/_Leadsedit/{LeadId?}")]

		public async Task<IActionResult> _Leadsedit()
		{

			// Create page object
			_Leads_Edit = new __Leads_Edit(this);

			// Run the page
			return await _Leads_Edit.Run();
		}

		// search
		[Route("_Leadssrch")]
		[Route("Home/_Leadssrch")]

		public async Task<IActionResult> _Leadssrch()
		{

			// Create page object
			_Leads_Search = new __Leads_Search(this);

			// Run the page
			return await _Leads_Search.Run();
		}
	}
}