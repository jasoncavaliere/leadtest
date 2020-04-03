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
		[Route("BankBranchlist/{Id?}")]
		[Route("Home/BankBranchlist/{Id?}")]

		public async Task<IActionResult> BankBranchlist()
		{

			// Create page object
			BankBranch_List = new _BankBranch_List(this);
			BankBranch_List.Cache = _cache;

			// Run the page
			return await BankBranch_List.Run();
		}

		// add
		[Route("BankBranchadd/{Id?}")]
		[Route("Home/BankBranchadd/{Id?}")]

		public async Task<IActionResult> BankBranchadd()
		{

			// Create page object
			BankBranch_Add = new _BankBranch_Add(this);

			// Run the page
			return await BankBranch_Add.Run();
		}

		// view
		[Route("BankBranchview/{Id?}")]
		[Route("Home/BankBranchview/{Id?}")]

		public async Task<IActionResult> BankBranchview()
		{

			// Create page object
			BankBranch_View = new _BankBranch_View(this);

			// Run the page
			return await BankBranch_View.Run();
		}

		// edit
		[Route("BankBranchedit/{Id?}")]
		[Route("Home/BankBranchedit/{Id?}")]

		public async Task<IActionResult> BankBranchedit()
		{

			// Create page object
			BankBranch_Edit = new _BankBranch_Edit(this);

			// Run the page
			return await BankBranch_Edit.Run();
		}

		// delete
		[Route("BankBranchdelete/{Id?}")]
		[Route("Home/BankBranchdelete/{Id?}")]

		public async Task<IActionResult> BankBranchdelete()
		{

			// Create page object
			BankBranch_Delete = new _BankBranch_Delete(this);

			// Run the page
			return await BankBranch_Delete.Run();
		}
	}
}