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

		/// <summary>
		/// _Leads_Search
		/// </summary>
		public static __Leads_Search _Leads_Search {
			get => HttpData.Get<__Leads_Search>("_Leads_Search");
			set => HttpData["_Leads_Search"] = value;
		}

		/// <summary>
		/// Page class for Leads
		/// </summary>
		public class __Leads_Search : __Leads_SearchBase
		{

			// Construtor
			public __Leads_Search(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>
		public class __Leads_SearchBase : __Leads, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "search";

			// Project ID
			public string ProjectID = "{DE72B0A5-4A34-400E-B744-FF3F81D69E8F}";

			// Table name
			public string TableName { get; set; } = "Leads";

			// Page object name
			public string PageObjName = "_Leads_Search";

			// Page headings
			public string Heading = "";

			public string Subheading = "";

			public string PageHeader = "";

			public string PageFooter = "";

			// Token
			public string Token = null; // DN

			public bool CheckToken = Config.CheckToken;

			// Action result // DN
			public IActionResult ActionResult;

			// Cache // DN
			public IMemoryCache Cache;

			// Page terminated // DN
			private bool _terminated = false;

			// Page URL
			private string _pageUrl = "";

			// Page action result
			public IActionResult PageResult() {
				if (ActionResult != null)
					return ActionResult;
				SetupMenus();
				return Controller.View();
			}

			// Page heading
			public string PageHeading {
				get {
					if (!Empty(Heading))
						return Heading;
					else if (!Empty(Caption))
						return Caption;
					else
						return "";
				}
			}

			// Page subheading
			public string PageSubheading {
				get {
					if (!Empty(Subheading))
						return Subheading;
					if (!Empty(TableName))
						return Language.Phrase(PageID);
					return "";
				}
			}

			// Page name
			public string PageName => CurrentPageName();

			// Page URL
			public string PageUrl {
				get {
					if (_pageUrl == "") {
						_pageUrl = CurrentPageName() + "?";
					}
					return _pageUrl;
				}
			}

			// Private properties
			private string _message = "";

			private string _failureMessage = "";

			private string _successMessage = "";

			private string _warningMessage = "";

			// Message
			public string Message {
				get => Session.TryGetValue(Config.SessionMessage, out string message) ? message : _message;
				set {
					_message = AddMessage(Message, value);
					Session[Config.SessionMessage] = _message;
				}
			}

			// Failure Message
			public string FailureMessage {
				get => Session.TryGetValue(Config.SessionFailureMessage, out string failureMessage) ? failureMessage : _failureMessage;
				set {
					_failureMessage = AddMessage(FailureMessage, value);
					Session[Config.SessionFailureMessage] = _failureMessage;
				}
			}

			// Success Message
			public string SuccessMessage {
				get => Session.TryGetValue(Config.SessionSuccessMessage, out string successMessage) ? successMessage : _successMessage;
				set {
					_successMessage = AddMessage(SuccessMessage, value);
					Session[Config.SessionSuccessMessage] = _successMessage;
				}
			}

			// Warning Message
			public string WarningMessage {
				get => Session.TryGetValue(Config.SessionWarningMessage, out string warningMessage) ? warningMessage : _warningMessage;
				set {
					_warningMessage = AddMessage(WarningMessage, value);
					Session[Config.SessionWarningMessage] = _warningMessage;
				}
			}

			// Clear message
			public void ClearMessage() {
				_message = "";
				Session[Config.SessionMessage] = _message;
			}

			// Clear failure message
			public void ClearFailureMessage() {
				_failureMessage = "";
				Session[Config.SessionFailureMessage] = _failureMessage;
			}

			// Clear success message
			public void ClearSuccessMessage() {
				_successMessage = "";
				Session[Config.SessionSuccessMessage] = _successMessage;
			}

			// Clear warning message
			public void ClearWarningMessage() {
				_warningMessage = "";
				Session[Config.SessionWarningMessage] = _warningMessage;
			}

			// Clear all messages
			public void ClearMessages() {
				ClearMessage();
				ClearFailureMessage();
				ClearSuccessMessage();
				ClearWarningMessage();
			}

			// Get message
			public string GetMessage() { // DN
				bool hidden = true;
				string html = "";

				// Message
				string message = Message;
				Message_Showing(ref message, "");
				if (!Empty(message)) { // Message in Session, display
					if (!hidden)
						message = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + message;
					html += "<div class=\"alert alert-info alert-dismissible ew-info\"><i class=\"icon fas fa-info\"></i>" + message + "</div>";
					Session[Config.SessionMessage] = ""; // Clear message in Session
				}

				// Warning message
				string warningMessage = WarningMessage;
				Message_Showing(ref warningMessage, "warning");
				if (!Empty(warningMessage)) { // Message in Session, display
					if (!hidden)
						warningMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + warningMessage;
					html += "<div class=\"alert alert-warning alert-dismissible ew-warning\"><i class=\"icon fas fa-exclamation\"></i>" + warningMessage + "</div>";
					Session[Config.SessionWarningMessage] = ""; // Clear message in Session
				}

				// Success message
				string successMessage = SuccessMessage;
				Message_Showing(ref successMessage, "success");
				if (!Empty(successMessage)) { // Message in Session, display
					if (!hidden)
						successMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + successMessage;
					html += "<div class=\"alert alert-success alert-dismissible ew-success\"><i class=\"icon fas fa-check\"></i>" + successMessage + "</div>";
					Session[Config.SessionSuccessMessage] = ""; // Clear message in Session
				}

				// Failure message
				string errorMessage = FailureMessage;
				Message_Showing(ref errorMessage, "failure");
				if (!Empty(errorMessage)) { // Message in Session, display
					if (!hidden)
						errorMessage = "<button type=\"button\" class=\"close\" data-dismiss=\"alert\" aria-label=\"Close\"><span aria-hidden=\"true\">&times;</span></button>" + errorMessage;
					html += "<div class=\"alert alert-danger alert-dismissible ew-error\"><i class=\"icon fas fa-ban\"></i>" + errorMessage + "</div>";
					Session[Config.SessionFailureMessage] = ""; // Clear message in Session
				}
				return "<div class=\"ew-message-dialog" + (hidden ? " d-none" : "") + "\">" + html + "</div>"; // DN
			}

			// Show message as IHtmlContent // DN
			public IHtmlContent ShowMessages() => new HtmlString(GetMessage());

			// Get messages
			public Dictionary<string, string> GetMessages() {
				var d = new Dictionary<string, string>();

				// Message
				string message = Message;
				if (!Empty(message)) { // Message in Session, display
					d.Add("message", message);
					Session[Config.SessionMessage] = ""; // Clear message in Session
				}

				// Warning message
				string warningMessage = WarningMessage;
				if (!Empty(warningMessage)) { // Message in Session, display
					d.Add("warningMessage", warningMessage);
					Session[Config.SessionWarningMessage] = ""; // Clear message in Session
				}

				// Success message
				string successMessage = SuccessMessage;
				if (!Empty(successMessage)) { // Message in Session, display
					d.Add("successMessage", successMessage);
					Session[Config.SessionSuccessMessage] = ""; // Clear message in Session
				}

				// Failure message
				string failureMessage = FailureMessage;
				if (!Empty(failureMessage)) { // Message in Session, display
					d.Add("failureMessage", failureMessage);
					Session[Config.SessionFailureMessage] = ""; // Clear message in Session
				}
				return d;
			}

			// Show Page Header
			public IHtmlContent ShowPageHeader() {
				string header = PageHeader;
				Page_DataRendering(ref header);
				if (!Empty(header)) // Header exists, display
					return new HtmlString("<p id=\"ew-page-header\">" + header + "</p>");
				return null;
			}

			// Show Page Footer
			public IHtmlContent ShowPageFooter() {
				string footer = PageFooter;
				Page_DataRendered(ref footer);
				if (!Empty(footer)) // Footer exists, display
					return new HtmlString("<p id=\"ew-page-footer\">" + footer + "</p>");
				return null;
			}

			// Validate page request
			public bool IsPageRequest => true;

			// Valid post
			protected async Task<bool> ValidPost() => !CheckToken || !IsPost() || IsApi() || await Antiforgery.IsRequestValidAsync(HttpContext);

			// Create token
			public void CreateToken() {
				Token ??= Antiforgery.GetAndStoreTokens(HttpContext).RequestToken;
				CurrentToken = Token; // Save to global variable
			}

			// Constructor
			public __Leads_SearchBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language ??= new Lang();

				// Table object (_Leads)
				if (_Leads == null || _Leads is __Leads)
					_Leads = this;

				// Start time
				StartTime = Environment.TickCount;

				// Debug message
				LoadDebugMessage();

				// Open connection
				Conn = Connection; // DN
			}
			#pragma warning disable 1998

			// Export view result
			public async Task<IActionResult> ExportView() { // DN
				if (!Empty(CustomExport) && CustomExport == Export && Config.Export.TryGetValue(CustomExport, out string classname)) {
					IActionResult result = null;
					string content = await GetViewOutput();
					if (Empty(ExportFileName))
						ExportFileName = TableVar;
					dynamic doc = CreateInstance(classname, new object[] { _Leads, "" }); // DN
					doc.Text.Append(content);
					result = doc.Export();
					DeleteTempImages(); // Delete temp images
					return result;
				}
				return null;
			}
			#pragma warning restore 1998

			/// <summary>
			/// Terminate page
			/// </summary>
			/// <param name="url">URL to rediect to</param>
			/// <returns>Page result</returns>
			public IActionResult Terminate(string url = "") { // DN
				if (_terminated) // DN
					return null;

				// Page Unload event
				Page_Unload();

				// Global Page Unloaded event
				Page_Unloaded();
				if (!IsApi())
					Page_Redirecting(ref url);

				// Close connection
				CloseConnections();

				// Gargage collection
				Collect(); // DN

				// Terminate
				_terminated = true; // DN

				// Return for API
				if (IsApi()) {
					bool res = !Empty(url);
					if (!res) { // Show error
						var showError = new Dictionary<string, string> { { "success", "false" }, { "version", Config.ProductVersion } };
						foreach (var (key, value) in GetMessages())
							showError.Add(key, value);
						return Controller.Json(showError);
					}
				} else if (ActionResult != null) { // Check action result
					return ActionResult;
				}

				// Go to URL if specified
				if (!Empty(url)) {
					if (!Config.Debug)
						ResponseClear();
					if (!Response.HasStarted) {

						// Handle modal response
						if (IsModal) { // Show as modal
							var row = new Dictionary<string, string> { {"url", GetUrl(url)}, {"modal", "1"} };
							string pageName = GetPageName(url);
							if (pageName != ListUrl) { // Not List page
								row.Add("caption", GetModalCaption(pageName));
								if (pageName == "_Leadsview")
									row.Add("view", "1");
							} else { // List page should not be shown as modal => error
								row.Add("error", FailureMessage);
								ClearFailureMessage();
							}
							return Controller.Json(row);
						} else {
							SaveDebugMessage();
							return Controller.LocalRedirect(AppPath(url));
						}
					}
				}
				return null;
			}

			// Get all records from datareader
			protected async Task<List<Dictionary<string, object>>> GetRecordsFromRecordset(DbDataReader rs)
			{
				var rows = new List<Dictionary<string, object>>();
				while (rs != null && await rs.ReadAsync()) {
					await LoadRowValues(rs); // Set up DbValue/CurrentValue
					rows.Add(GetRecordFromDictionary(GetDictionary(rs)));
				}
				return rows;
			}

			// Get the first record from datareader
			protected async Task<Dictionary<string, object>> GetRecordFromRecordset(DbDataReader rs)
			{
				if (rs != null) {
					await LoadRowValues(rs); // Set up DbValue/CurrentValue
					return GetRecordFromDictionary(GetDictionary(rs));
				}
				return null;
			}

			// Get the first record from the list of records
			protected Dictionary<string, object> GetRecordFromRecordset(List<Dictionary<string, object>> ar) => GetRecordFromDictionary(ar[0]);

			// Get record from Dictionary
			protected Dictionary<string, object> GetRecordFromDictionary(Dictionary<string, object> ar) {
				var row = new Dictionary<string, object>();
				foreach (var (key, value) in ar) {
					if (Fields.TryGetValue(key, out DbField fld)) {
						if (fld.Visible || fld.IsPrimaryKey) { // Primary key or Visible
							if (fld.HtmlTag == "FILE") { // Upload field
								if (Empty(value)) {
									row[key] = null;
								} else {
									if (fld.DataType == Config.DataTypeBlob) {
										string url = FullUrl(GetPageName(Config.ApiUrl) + "/" + Config.ApiFileAction + "/" + fld.TableVar + "/" + fld.Param + "/" + GetRecordKeyValue(ar)); // Query string format
										row[key] = new Dictionary<string, object> { { "mimeType", ContentType((byte[])value) }, { "url", url } };
									} else if (!fld.UploadMultiple || !Convert.ToString(value).Contains(Config.MultipleUploadSeparator)) { // Single file
										row[key] = new Dictionary<string, object> { { "mimeType", ContentType(Convert.ToString(value)) }, { "url", FullUrl(fld.HrefPath + Convert.ToString(value)) } };
									} else { // Multiple files
										var files = Convert.ToString(value).Split(Config.MultipleUploadSeparator);
										row[key] = files.Where(file => !Empty(file)).Select(file => new Dictionary<string, object> { { "type", ContentType(file) }, { "url", FullUrl(fld.HrefPath + file) } });
									}
								}
							} else {
								row[key] = Convert.ToString(value);
							}
						}
					}
				}
				return row;
			}

			// Get record key value from array
			protected string GetRecordKeyValue(Dictionary<string, object> ar) {
				string key = "";
				key += UrlEncode(Convert.ToString(ar["LeadId"]));
				return key;
			}

			// Hide fields for Add/Edit
			protected void HideFieldsForAddEdit() {
			}
			#pragma warning disable 219

			/// <summary>
			/// Lookup data from table
			/// </summary>
			public async Task<JsonBoolResult> Lookup() {
				Language ??= new Lang(Config.LanguageFolder, Post("language"));

				// Set up API request
				if (!await SetupApiRequest())
					return JsonBoolResult.FalseResult;

				// Get lookup object
				string fieldName = Post("field");
				DbField lookupField = FieldByName(fieldName);
				if (lookupField == null)
					return JsonBoolResult.FalseResult;
				Lookup<DbField> lookup = lookupField.Lookup;
				if (lookup == null)
					return JsonBoolResult.FalseResult;
				string lookupType = Post("ajax");
				int pageSize = -1;
				int offset = -1;
				string searchValue = "";
				if (SameText(lookupType, "modal")) {
					searchValue = Post("sv");
					if (!Post("recperpage", out StringValues rpp))
						pageSize = 10;
					else
						pageSize = ConvertToInt(rpp.ToString());
					offset = Post<int>("start");
				} else if (SameText(lookupType, "autosuggest")) {
					searchValue = Get("q");
					pageSize = IsNumeric(Param("n")) ? Param<int>("n") : -1;
					if (pageSize <= 0)
						pageSize = Config.AutoSuggestMaxEntries;
					int start = IsNumeric(Param("start")) ? Param<int>("start") : -1;
					int page = IsNumeric(Param("page")) ? Param<int>("page") : -1;
					offset = start >= 0 ? start : (page > 0 && pageSize > 0 ? (page - 1) * pageSize : 0);
				}
				string userSelect = Decrypt(Post("s"));
				string userFilter = Decrypt(Post("f"));
				string userOrderBy = Decrypt(Post("o"));

				// Selected records from modal, skip parent/filter fields and show all records
				lookup.LookupType = lookupType; // Lookup type
				if (Post("keys[]", out StringValues keys)) { // Selected records from modal
					lookup.FilterFields = new Dictionary<string, string>(); // Skip parent fields if any
					pageSize = -1; // Show all records
					lookup.FilterValues.Add(string.Join(",", keys));
				} else { // Lookup values
					lookup.FilterValues.Add(Post<string>("v0") ?? Post("lookupValue"));
				}
				int cnt = IsDictionary(lookup.FilterFields) ? lookup.FilterFields.Count : 0;
				for (int i = 1; i <= cnt; i++)
					lookup.FilterValues.Add(UrlDecode(Post("v" + i)));
				lookup.SearchValue = searchValue;
				lookup.PageSize = pageSize;
				lookup.Offset = offset;
				if (userSelect != "")
					lookup.UserSelect = userSelect;
				if (userFilter != "")
					lookup.UserFilter = userFilter;
				if (userOrderBy != "")
					lookup.UserOrderBy = userOrderBy;
				return await lookup.ToJson(this);
			}
			#pragma warning restore 219
			#pragma warning disable 1998

			/// <summary>
			/// Set up API request
			/// </summary>
			public async Task<bool> SetupApiRequest()
			{

				// Check security for API request
				if (ValidApiRequest()) {
					return true;
				}
				return false;
			}
			#pragma warning restore 1998

			public string FormClassName = "ew-horizontal ew-form ew-search-form";

			public bool IsModal = false;

			public bool IsMobileOrModal = false;

			/// <summary>
			/// Page run
			/// </summary>
			/// <returns>Page result</returns>
			public async Task<IActionResult> Run() {

				// Header
				Header(Config.Cache);

				// Is modal
				IsModal = Param<bool>("modal");

				// User profile
				Profile = new UserProfile();

				// Security
				if (!await SetupApiRequest()) {
					Security ??= CreateSecurity(); // DN
				}

				// Create form object
				CurrentForm = new HttpForm();
				CurrentAction = Param("action"); // Set up current action
				LeadId.SetVisibility();
				_Name.SetVisibility();
				State.SetVisibility();
				LeadStatusId.SetVisibility();
				BranchId.SetVisibility();
				UserId.SetVisibility();
				FirstName.SetVisibility();
				LastName.SetVisibility();
				BlobUrl.SetVisibility();
				EmailAddress.SetVisibility();
				PhoneNumber.SetVisibility();
				HideFieldsForAddEdit();

				// Do not use lookup cache
				SetUseLookupCache(false);

				// Global Page Loading event
				Page_Loading();

				// Page Load event
				Page_Load();

				// Check token
				if (!await ValidPost())
					End(Language.Phrase("InvalidPostRequest"));

				// Check action result
				if (ActionResult != null) // Action result set by server event // DN
					return ActionResult;

				// Create token
				CreateToken();

				// Set up lookup cache
				await SetupLookupOptions(LeadStatusId);
				await SetupLookupOptions(BranchId);

				// Set up Breadcrumb
				SetupBreadcrumb();

				// Check modal
				if (IsModal)
					SkipHeaderFooter = true;
				IsMobileOrModal = IsMobile() || IsModal;
				if (IsPageRequest) { // Validate request

					// Get action
					CurrentAction = CurrentForm.GetValue("action");
					if (IsSearch) {

						// Build search string for advanced search, remove blank field
						LoadSearchValues(); // Get search values
						string srchStr;
						if (ValidateSearch()) {
							srchStr = BuildAdvancedSearch();
						} else {
							srchStr = "";
							FailureMessage = SearchError;
						}
						if (!Empty(srchStr)) {
							srchStr = UrlParm(srchStr);
							srchStr = "_Leadslist?" + srchStr;
							return Terminate(srchStr); // Go to List page
						}
					}
				}

				// Restore search settings from Session
				if (Empty(SearchError))
					LoadAdvancedSearch();

				// Render row for search
				RowType = Config.RowTypeSearch;
				ResetAttributes();
				await RenderRow();
				return PageResult();
			}

			// Build advanced search
			protected string BuildAdvancedSearch() {
				string srchUrl = "";
				BuildSearchUrl(ref srchUrl, LeadId); // LeadId
				BuildSearchUrl(ref srchUrl, _Name); // Name
				BuildSearchUrl(ref srchUrl, State); // State
				BuildSearchUrl(ref srchUrl, LeadStatusId); // LeadStatusId
				BuildSearchUrl(ref srchUrl, BranchId); // BranchId
				BuildSearchUrl(ref srchUrl, UserId); // UserId
				BuildSearchUrl(ref srchUrl, FirstName); // FirstName
				BuildSearchUrl(ref srchUrl, LastName); // LastName
				BuildSearchUrl(ref srchUrl, BlobUrl); // BlobUrl
				BuildSearchUrl(ref srchUrl, EmailAddress); // EmailAddress
				BuildSearchUrl(ref srchUrl, PhoneNumber); // PhoneNumber
				if (!Empty(srchUrl))
					srchUrl += "&";
				srchUrl += "cmd=search";
				return srchUrl;
			}

			// Build search URL
			protected void BuildSearchUrl(ref string url, DbField fld, bool oprOnly = false) {
				bool isValid;
				string wrk = "";
				string fldParm = fld.Param;
				string ctl = "x_" + fldParm;
				string ctl2 = "y_" + fldParm;
				if (fld.IsMultiSelect) { // DN
					ctl += "[]";
					ctl2 += "[]";
				}
				string fldVal = CurrentForm.GetValue(ctl);
				string fldOpr = CurrentForm.GetValue("z_" + fldParm);
				string fldCond = CurrentForm.GetValue("v_" + fldParm);
				string fldVal2 = CurrentForm.GetValue(ctl2);
				string fldOpr2 = CurrentForm.GetValue("w_" + fldParm);
				int fldDataType = fld.IsVirtual ? Config.DataTypeString : fld.DataType;
				if (SameText(fldOpr, "BETWEEN")) {
					isValid = (fldDataType != Config.DataTypeNumber) || (fldDataType == Config.DataTypeNumber && IsNumeric(fldVal) && IsNumeric(fldVal2));
					if (!Empty(fldVal) && !Empty(fldVal2) && isValid) {
						wrk = ctl + "=" + UrlEncode(fldVal) + "&" + ctl2 + "=" + UrlEncode(fldVal2) + "&z_" + fldParm + "=" + UrlEncode(fldOpr);
					}
				} else {
					isValid = (fldDataType != Config.DataTypeNumber) ||
						(fldDataType == Config.DataTypeNumber && SearchValueIsNumeric(fld, fldVal));
					if (!Empty(fldVal) && isValid && IsValidOperator(fldOpr, fldDataType)) {
						wrk = ctl + "=" + UrlEncode(fldVal) + "&z_" + fldParm + "=" + UrlEncode(fldOpr);
					} else if (fldOpr == "IS NULL" || fldOpr == "IS NOT NULL" || (!Empty(fldOpr) && oprOnly && IsValidOperator(fldOpr, fldDataType))) {
						wrk = "z_" + fldParm + "=" + UrlEncode(fldOpr);
					}
					isValid = (fldDataType != Config.DataTypeNumber) ||
						(fldDataType == Config.DataTypeNumber && SearchValueIsNumeric(fld, fldVal2));
					if (!Empty(fldVal2) && isValid && IsValidOperator(fldOpr2, fldDataType)) {
						if (!Empty(wrk)) 
							wrk += "&v_" + fldParm + "=" + fldCond + "&";
						wrk += ctl2 + "=" + UrlEncode(fldVal2) + "&w_" + fldParm + "=" + UrlEncode(fldOpr2);
					} else if (fldOpr2 == "IS NULL" || fldOpr2 == "IS NOT NULL" || (!Empty(fldOpr2) && oprOnly && IsValidOperator(fldOpr2, fldDataType))) {
						if (!Empty(wrk)) 
							wrk += "&v_" + fldParm + "=" + UrlEncode(fldCond) + "&";
						wrk += "w_" + fldParm + "=" + UrlEncode(fldOpr2);
					}
				}
				if (!Empty(wrk)) {
					if (!Empty(url)) 
						url += "&";
					url += wrk;
				}
			}

			// Is numeric
			protected bool SearchValueIsNumeric(DbField fld, string value) {
				if (IsFloatFormat(fld.Type))
					value = ConvertToFloatString(value);
				return IsNumeric(value);
			}

			// Load search values for validation // DN
			protected void LoadSearchValues() {

				// LeadId
				if (!IsAddOrEdit)
					if (Form.ContainsKey("x_LeadId"))
						LeadId.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_LeadId");
				if (Form.ContainsKey("z_LeadId"))
					LeadId.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_LeadId");

				// _Name
				if (!IsAddOrEdit)
					if (Form.ContainsKey("x__Name"))
						_Name.AdvancedSearch.SearchValue = CurrentForm.GetValue("x__Name");
				if (Form.ContainsKey("z__Name"))
					_Name.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z__Name");

				// State
				if (!IsAddOrEdit)
					if (Form.ContainsKey("x_State"))
						State.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_State");
				if (Form.ContainsKey("z_State"))
					State.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_State");

				// LeadStatusId
				if (!IsAddOrEdit)
					if (Form.ContainsKey("x_LeadStatusId"))
						LeadStatusId.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_LeadStatusId");
				if (Form.ContainsKey("z_LeadStatusId"))
					LeadStatusId.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_LeadStatusId");

				// BranchId
				if (!IsAddOrEdit)
					if (Form.ContainsKey("x_BranchId"))
						BranchId.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_BranchId");
				if (Form.ContainsKey("z_BranchId"))
					BranchId.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_BranchId");

				// UserId
				if (!IsAddOrEdit)
					if (Form.ContainsKey("x_UserId"))
						UserId.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_UserId");
				if (Form.ContainsKey("z_UserId"))
					UserId.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_UserId");

				// FirstName
				if (!IsAddOrEdit)
					if (Form.ContainsKey("x_FirstName"))
						FirstName.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_FirstName");
				if (Form.ContainsKey("z_FirstName"))
					FirstName.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_FirstName");

				// LastName
				if (!IsAddOrEdit)
					if (Form.ContainsKey("x_LastName"))
						LastName.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_LastName");
				if (Form.ContainsKey("z_LastName"))
					LastName.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_LastName");

				// BlobUrl
				if (!IsAddOrEdit)
					if (Form.ContainsKey("x_BlobUrl"))
						BlobUrl.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_BlobUrl");
				if (Form.ContainsKey("z_BlobUrl"))
					BlobUrl.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_BlobUrl");

				// EmailAddress
				if (!IsAddOrEdit)
					if (Form.ContainsKey("x_EmailAddress"))
						EmailAddress.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_EmailAddress");
				if (Form.ContainsKey("z_EmailAddress"))
					EmailAddress.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_EmailAddress");

				// PhoneNumber
				if (!IsAddOrEdit)
					if (Form.ContainsKey("x_PhoneNumber"))
						PhoneNumber.AdvancedSearch.SearchValue = CurrentForm.GetValue("x_PhoneNumber");
				if (Form.ContainsKey("z_PhoneNumber"))
					PhoneNumber.AdvancedSearch.SearchOperator = CurrentForm.GetValue("z_PhoneNumber");
			}

			// Load row based on key values
			public async Task<bool> LoadRow() {
				string filter = GetRecordFilter();

				// Call Row Selecting event
				Row_Selecting(ref filter);

				// Load SQL based on filter
				CurrentFilter = filter;
				string sql = CurrentSql;
				bool res = false;
				try {
					using var rsrow = await Connection.OpenDataReaderAsync(sql);
					if (rsrow != null && await rsrow.ReadAsync()) {
						await LoadRowValues(rsrow);
						res = true;
					} else {
						return false;
					}
				} catch {
					if (Config.Debug)
						throw;
				}
				return res;
			}
			#pragma warning disable 162, 168, 1998

			// Load row values from recordset
			public async Task LoadRowValues(DbDataReader dr = null) {
				Dictionary<string, object> row;
				object v;
				if (dr != null && dr.HasRows)
					row = Connection.GetRow(dr); // DN
				else
					row = NewRow();

				// Call Row Selected event
				Row_Selected(row);
				if (dr == null || !dr.HasRows)
					return;
				LeadId.SetDbValue(row["LeadId"]);
				_Name.SetDbValue(row["Name"]);
				State.SetDbValue(row["State"]);
				LeadStatusId.SetDbValue(row["LeadStatusId"]);
				BranchId.SetDbValue(row["BranchId"]);
				UserId.SetDbValue(row["UserId"]);
				FirstName.SetDbValue(row["FirstName"]);
				LastName.SetDbValue(row["LastName"]);
				BlobUrl.SetDbValue(row["BlobUrl"]);
				EmailAddress.SetDbValue(row["EmailAddress"]);
				PhoneNumber.SetDbValue(row["PhoneNumber"]);
			}
			#pragma warning restore 162, 168, 1998

			// Return a row with default values
			protected Dictionary<string, object> NewRow() {
				var row = new Dictionary<string, object>();
				row.Add("LeadId", System.DBNull.Value);
				row.Add("Name", System.DBNull.Value);
				row.Add("State", System.DBNull.Value);
				row.Add("LeadStatusId", System.DBNull.Value);
				row.Add("BranchId", System.DBNull.Value);
				row.Add("UserId", System.DBNull.Value);
				row.Add("FirstName", System.DBNull.Value);
				row.Add("LastName", System.DBNull.Value);
				row.Add("BlobUrl", System.DBNull.Value);
				row.Add("EmailAddress", System.DBNull.Value);
				row.Add("PhoneNumber", System.DBNull.Value);
				return row;
			}
			#pragma warning disable 1998

			// Render row values based on field settings
			public async Task RenderRow() {

				// Call Row_Rendering event
				Row_Rendering();

				// Common render codes for all row types
				// LeadId
				// Name
				// State
				// LeadStatusId
				// BranchId
				// UserId
				// FirstName
				// LastName
				// BlobUrl
				// EmailAddress
				// PhoneNumber

				if (RowType == Config.RowTypeView) { // View row

					// LeadId
					LeadId.ViewValue = LeadId.CurrentValue;
					LeadId.ViewCustomAttributes = "";

					// Name
					_Name.ViewValue = Convert.ToString(_Name.CurrentValue); // DN
					_Name.ViewCustomAttributes = "";

					// State
					State.ViewValue = Convert.ToString(State.CurrentValue); // DN
					State.ViewCustomAttributes = "";

					// LeadStatusId
					curVal = Convert.ToString(LeadStatusId.CurrentValue);
					if (!Empty(curVal)) {
						LeadStatusId.ViewValue = LeadStatusId.LookupCacheOption(curVal);
						if (LeadStatusId.ViewValue == null) { // Lookup from database
							filterWrk = "[Id]" + SearchString("=", curVal.Trim(), Config.DataTypeNumber, "");
							sqlWrk = LeadStatusId.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(listwrk[1]);
								LeadStatusId.ViewValue = LeadStatusId.DisplayValue(listwrk);
							} else {
								LeadStatusId.ViewValue = LeadStatusId.CurrentValue;
							}
						}
					} else {
						LeadStatusId.ViewValue = System.DBNull.Value;
					}
					LeadStatusId.ViewCustomAttributes = "";

					// BranchId
					curVal = Convert.ToString(BranchId.CurrentValue);
					if (!Empty(curVal)) {
						BranchId.ViewValue = BranchId.LookupCacheOption(curVal);
						if (BranchId.ViewValue == null) { // Lookup from database
							filterWrk = "[Id]" + SearchString("=", curVal.Trim(), Config.DataTypeNumber, "");
							sqlWrk = BranchId.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(listwrk[1]);
								BranchId.ViewValue = BranchId.DisplayValue(listwrk);
							} else {
								BranchId.ViewValue = BranchId.CurrentValue;
							}
						}
					} else {
						BranchId.ViewValue = System.DBNull.Value;
					}
					BranchId.ViewCustomAttributes = "";

					// UserId
					UserId.ViewValue = Convert.ToString(UserId.CurrentValue); // DN
					UserId.ViewCustomAttributes = "";

					// FirstName
					FirstName.ViewValue = Convert.ToString(FirstName.CurrentValue); // DN
					FirstName.ViewCustomAttributes = "";

					// LastName
					LastName.ViewValue = Convert.ToString(LastName.CurrentValue); // DN
					LastName.ViewCustomAttributes = "";

					// BlobUrl
					BlobUrl.ViewValue = Convert.ToString(BlobUrl.CurrentValue); // DN
					BlobUrl.ViewCustomAttributes = "";

					// EmailAddress
					EmailAddress.ViewValue = Convert.ToString(EmailAddress.CurrentValue); // DN
					EmailAddress.ViewCustomAttributes = "";

					// PhoneNumber
					PhoneNumber.ViewValue = Convert.ToString(PhoneNumber.CurrentValue); // DN
					PhoneNumber.ViewCustomAttributes = "";

					// LeadId
					LeadId.HrefValue = "";
					LeadId.TooltipValue = "";

					// Name
					_Name.HrefValue = "";
					_Name.TooltipValue = "";

					// State
					State.HrefValue = "";
					State.TooltipValue = "";

					// LeadStatusId
					LeadStatusId.HrefValue = "";
					LeadStatusId.TooltipValue = "";

					// BranchId
					BranchId.HrefValue = "";
					BranchId.TooltipValue = "";

					// UserId
					UserId.HrefValue = "";
					UserId.TooltipValue = "";

					// FirstName
					FirstName.HrefValue = "";
					FirstName.TooltipValue = "";

					// LastName
					LastName.HrefValue = "";
					LastName.TooltipValue = "";

					// BlobUrl
					BlobUrl.HrefValue = "";
					BlobUrl.TooltipValue = "";

					// EmailAddress
					EmailAddress.HrefValue = "";
					EmailAddress.TooltipValue = "";

					// PhoneNumber
					PhoneNumber.HrefValue = "";
					PhoneNumber.TooltipValue = "";
				} else if (RowType == Config.RowTypeSearch) { // Search row

					// LeadId
					LeadId.EditAttrs["class"] = "form-control";
					LeadId.EditValue = LeadId.AdvancedSearch.SearchValue; // DN
					LeadId.PlaceHolder = RemoveHtml(LeadId.Caption);

					// Name
					_Name.EditAttrs["class"] = "form-control";
					if (!_Name.Raw)
						_Name.AdvancedSearch.SearchValue = HtmlDecode(_Name.AdvancedSearch.SearchValue);
					_Name.EditValue = _Name.AdvancedSearch.SearchValue; // DN
					_Name.PlaceHolder = RemoveHtml(_Name.Caption);

					// State
					State.EditAttrs["class"] = "form-control";
					if (!State.Raw)
						State.AdvancedSearch.SearchValue = HtmlDecode(State.AdvancedSearch.SearchValue);
					State.EditValue = State.AdvancedSearch.SearchValue; // DN
					State.PlaceHolder = RemoveHtml(State.Caption);

					// LeadStatusId
					LeadStatusId.EditAttrs["class"] = "form-control";
					curVal = Convert.ToString(LeadStatusId.AdvancedSearch.SearchValue)?.Trim() ?? "";
					if (curVal != "")
						LeadStatusId.AdvancedSearch.ViewValue = LeadStatusId.LookupCacheOption(curVal);
					else
						LeadStatusId.AdvancedSearch.ViewValue = LeadStatusId.Lookup != null && IsList(LeadStatusId.Lookup.Options) ? curVal : null;
					if (LeadStatusId.AdvancedSearch.ViewValue != null) { // Load from cache
						LeadStatusId.EditValue = LeadStatusId.Lookup.Options.Values.ToList();
					} else { // Lookup from database
						if (curVal == "") {
							filterWrk = "0=1";
						} else {
							filterWrk = "[Id]" + SearchString("=", Convert.ToString(LeadStatusId.AdvancedSearch.SearchValue), Config.DataTypeNumber, "");
						}
						sqlWrk = LeadStatusId.Lookup.GetSql(true, filterWrk, null, this);
						rswrk = await Connection.GetRowsAsync(sqlWrk);
						LeadStatusId.EditValue = rswrk;
					}

					// BranchId
					BranchId.EditAttrs["class"] = "form-control";
					curVal = Convert.ToString(BranchId.AdvancedSearch.SearchValue)?.Trim() ?? "";
					if (curVal != "")
						BranchId.AdvancedSearch.ViewValue = BranchId.LookupCacheOption(curVal);
					else
						BranchId.AdvancedSearch.ViewValue = BranchId.Lookup != null && IsList(BranchId.Lookup.Options) ? curVal : null;
					if (BranchId.AdvancedSearch.ViewValue != null) { // Load from cache
						BranchId.EditValue = BranchId.Lookup.Options.Values.ToList();
					} else { // Lookup from database
						if (curVal == "") {
							filterWrk = "0=1";
						} else {
							filterWrk = "[Id]" + SearchString("=", Convert.ToString(BranchId.AdvancedSearch.SearchValue), Config.DataTypeNumber, "");
						}
						sqlWrk = BranchId.Lookup.GetSql(true, filterWrk, null, this);
						rswrk = await Connection.GetRowsAsync(sqlWrk);
						BranchId.EditValue = rswrk;
					}

					// UserId
					UserId.EditAttrs["class"] = "form-control";
					UserId.EditValue = UserId.AdvancedSearch.SearchValue; // DN
					UserId.PlaceHolder = RemoveHtml(UserId.Caption);

					// FirstName
					FirstName.EditAttrs["class"] = "form-control";
					if (!FirstName.Raw)
						FirstName.AdvancedSearch.SearchValue = HtmlDecode(FirstName.AdvancedSearch.SearchValue);
					FirstName.EditValue = FirstName.AdvancedSearch.SearchValue; // DN
					FirstName.PlaceHolder = RemoveHtml(FirstName.Caption);

					// LastName
					LastName.EditAttrs["class"] = "form-control";
					if (!LastName.Raw)
						LastName.AdvancedSearch.SearchValue = HtmlDecode(LastName.AdvancedSearch.SearchValue);
					LastName.EditValue = LastName.AdvancedSearch.SearchValue; // DN
					LastName.PlaceHolder = RemoveHtml(LastName.Caption);

					// BlobUrl
					BlobUrl.EditAttrs["class"] = "form-control";
					if (!BlobUrl.Raw)
						BlobUrl.AdvancedSearch.SearchValue = HtmlDecode(BlobUrl.AdvancedSearch.SearchValue);
					BlobUrl.EditValue = BlobUrl.AdvancedSearch.SearchValue; // DN
					BlobUrl.PlaceHolder = RemoveHtml(BlobUrl.Caption);

					// EmailAddress
					EmailAddress.EditAttrs["class"] = "form-control";
					if (!EmailAddress.Raw)
						EmailAddress.AdvancedSearch.SearchValue = HtmlDecode(EmailAddress.AdvancedSearch.SearchValue);
					EmailAddress.EditValue = EmailAddress.AdvancedSearch.SearchValue; // DN
					EmailAddress.PlaceHolder = RemoveHtml(EmailAddress.Caption);

					// PhoneNumber
					PhoneNumber.EditAttrs["class"] = "form-control";
					if (!PhoneNumber.Raw)
						PhoneNumber.AdvancedSearch.SearchValue = HtmlDecode(PhoneNumber.AdvancedSearch.SearchValue);
					PhoneNumber.EditValue = PhoneNumber.AdvancedSearch.SearchValue; // DN
					PhoneNumber.PlaceHolder = RemoveHtml(PhoneNumber.Caption);
				}
				if (RowType == Config.RowTypeAdd || RowType == Config.RowTypeEdit || RowType == Config.RowTypeSearch) // Add/Edit/Search row
					SetupFieldTitles();

				// Call Row Rendered event
				if (RowType != Config.RowTypeAggregateInit)
					Row_Rendered();
			}
			#pragma warning restore 1998

			// Validate search
			protected bool ValidateSearch() {

				// Initialize
				SearchError = "";

				// Check if validation required
				if (!Config.ServerValidate)
					return true;
				if (!CheckGuid(Convert.ToString(UserId.AdvancedSearch.SearchValue))) {
					SearchError = AddMessage(SearchError, UserId.ErrorMessage);
				}
				if (!CheckPhone(Convert.ToString(PhoneNumber.AdvancedSearch.SearchValue))) {
					SearchError = AddMessage(SearchError, PhoneNumber.ErrorMessage);
				}

				// Return validate result
				bool valid = Empty(SearchError);

				// Call Form_CustomValidate event
				string formCustomError = "";
				valid = valid && Form_CustomValidate(ref formCustomError);
				SearchError = AddMessage(SearchError, formCustomError);
				return valid;
			}

			// Save data to memory cache
			public void SetCache<T>(string key, T value, int span) => Cache.Set<T>(key, value, new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMilliseconds(span))); // Keep in cache for this time, reset time if accessed

			// Gete data from memory cache
			public void GetCache<T>(string key) => Cache.Get<T>(key);

			// Load advanced search
			public void LoadAdvancedSearch() {
				LeadId.AdvancedSearch.Load();
				_Name.AdvancedSearch.Load();
				State.AdvancedSearch.Load();
				LeadStatusId.AdvancedSearch.Load();
				BranchId.AdvancedSearch.Load();
				UserId.AdvancedSearch.Load();
				FirstName.AdvancedSearch.Load();
				LastName.AdvancedSearch.Load();
				BlobUrl.AdvancedSearch.Load();
				EmailAddress.AdvancedSearch.Load();
				PhoneNumber.AdvancedSearch.Load();
			}

			// Set up Breadcrumb
			protected void SetupBreadcrumb() {
				var breadcrumb = new Breadcrumb();
				string url = CurrentUrl();
				breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("_Leadslist")), "", TableVar, true);
				string pageId = "search";
				breadcrumb.Add("search", pageId, url);
				CurrentBreadcrumb = breadcrumb;
			}

			// Setup lookup options
			public async Task SetupLookupOptions(DbField fld)
			{
				Func<string> lookupFilter = null;
				var conn = Connection;
				if (!Empty(fld.Lookup) && fld.Lookup.Options.Count == 0) {

					// Set up lookup SQL
					// Always call to Lookup.GetSql so that user can setup Lookup.Options in Lookup_Selecting server event

					var sql = fld.Lookup.GetSql(false, "", lookupFilter, this);

					// Set up lookup cache
					if (fld.UseLookupCache && !Empty(sql) && fld.Lookup.ParentFields.Count == 0 && fld.Lookup.Options.Count == 0) {
						int totalCnt = await TryGetRecordCount(sql, conn);
						if (totalCnt > fld.LookupCacheCount) // Total count > cache count, do not cache
							return;
						var ar = new Dictionary<string, Dictionary<string, object>>();
						var values = new List<object>();
						List<Dictionary<string, object>> rs = await conn.GetRowsAsync(sql);
						if (rs != null) {
							foreach (var row in rs) {

								// Format the field values
								switch (fld.FieldVar) {
									case "x_LeadStatusId":
									break;
								}

								// Format the field values
								switch (fld.FieldVar) {
									case "x_BranchId":
									break;
								}
								string key = row.Values.First()?.ToString() ?? string.Empty;
								if (!ar.ContainsKey(key))
									ar.Add(key, row);
							}
						}
						fld.Lookup.Options = ar;
					}
				}
			}

			// Page Load event
			public virtual void Page_Load() {

				//Log("Page Load");
			}

			// Page Unload event
			public virtual void Page_Unload() {

				//Log("Page Unload");
			}

			// Page Redirecting event
			public virtual void Page_Redirecting(ref string url) {

				//url = newurl;
			}

			// Message Showing event
			// type = ""|"success"|"failure"|"warning"
			public virtual void Message_Showing(ref string msg, string type) {

				// Note: Do not change msg outside the following 4 cases.
				if (type == "success") {

					//msg = "your success message";
				} else if (type == "failure") {

					//msg = "your failure message";
				} else if (type == "warning") {

					//msg = "your warning message";
				} else {

					//msg = "your message";
				}
			}

			// Page Load event
			public virtual void Page_Render() {

				//Log("Page Render");
			}

			// Page Data Rendering event
			public virtual void Page_DataRendering(ref string header) {

				// Example:
				//header = "your header";

			}

			// Page Data Rendered event
			public virtual void Page_DataRendered(ref string footer) {

				// Example:
				//footer = "your footer";

			}

			// Form Custom Validate event
			public virtual bool Form_CustomValidate(ref string customError) {

				//Return error message in customError
				return true;
			}
		} // End page class
	} // End Partial class
} // End namespace