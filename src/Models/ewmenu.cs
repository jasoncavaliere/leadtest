// ASP.NET Maker 2020
// Copyright (c) 2019 e.World Technology Limited. All rights reserved.

using MailKit.Net.Smtp;
using Microsoft.Data.SqlClient;
using MimeKit;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.NewtonsoftJson;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Dapper;
using Ganss.XSS;
using ImageMagick;
using MimeDetective.InMemory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using static AspNetMaker2020.Models.project1;

// Models
namespace AspNetMaker2020.Models {

	// Partial class
	public partial class project1 {

		// Menu language
		public static Lang MenuLanguage;

		// Set up menus
		public static void SetupMenus() {

			// Menu Language
			if (Language != null && Language.LanguageFolder == Config.LanguageFolder)
				MenuLanguage = Language;
			else
				MenuLanguage = new Lang();

			// Navbar menu
			var topMenu = new Menu("navbar", true, true);
			TopMenu = topMenu.ToScript();

			// Sidebar menu
			var sideMenu = new Menu("menu", true, false);
			sideMenu.AddMenuItem(6, "mci_Pipeline", Language.MenuPhrase("6", "MenuText"), "", -1, "", true, false, true, "", "", false);
			sideMenu.AddMenuItem(10, "mi_Application", Language.MenuPhrase("10", "MenuText"), "Applicationlist", 6, "", true, false, false, "", "", false);
			sideMenu.AddMenuItem(12, "mi_Applications_By_Status", Language.MenuPhrase("12", "MenuText"), "Applications_By_Statussmry", 6, "", true, false, false, "", "", false);
			sideMenu.AddMenuItem(8, "mci_Reports", Language.MenuPhrase("8", "MenuText"), "", -1, "", true, false, true, "", "", false);
			sideMenu.AddMenuItem(21, "mci_System", Language.MenuPhrase("21", "MenuText"), "", -1, "", true, false, true, "", "", false);
			sideMenu.AddMenuItem(9, "mi_AuditTrail", Language.MenuPhrase("9", "MenuText"), "AuditTraillist", 21, "", true, false, false, "", "", false);
			sideMenu.AddMenuItem(7, "mci_Categories", Language.MenuPhrase("7", "MenuText"), "", 21, "", true, false, true, "", "", false);
			sideMenu.AddMenuItem(1, "mi_BankBranch", Language.MenuPhrase("1", "MenuText"), "BankBranchlist", 7, "", true, false, false, "", "", false);
			sideMenu.AddMenuItem(4, "mi_Users", Language.MenuPhrase("4", "MenuText"), "Userslist", 7, "", true, false, false, "", "", false);
			sideMenu.AddMenuItem(11, "mi_ApplicationStatus", Language.MenuPhrase("11", "MenuText"), "ApplicationStatuslist", 7, "", true, false, false, "", "", false);
			SideMenu = sideMenu.ToScript();
		}
	} // End Partial class
} // End namespace