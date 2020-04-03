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
		/// _Leads_Add
		/// </summary>
		public static __Leads_Add _Leads_Add {
			get => HttpData.Get<__Leads_Add>("_Leads_Add");
			set => HttpData["_Leads_Add"] = value;
		}

		/// <summary>
		/// Page class for Leads
		/// </summary>
		public class __Leads_Add : __Leads_AddBase
		{

			// Construtor
			public __Leads_Add(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>
		public class __Leads_AddBase : __Leads, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "add";

			// Project ID
			public string ProjectID = "{DE72B0A5-4A34-400E-B744-FF3F81D69E8F}";

			// Table name
			public string TableName { get; set; } = "Leads";

			// Page object name
			public string PageObjName = "_Leads_Add";

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
			public __Leads_AddBase(Controller controller = null) { // DN
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

			// Properties
			public string FormClassName = "ew-horizontal ew-form ew-add-form";

			public bool IsModal = false;

			public bool IsMobileOrModal = false;

			public string DbMasterFilter = "";

			public string DbDetailFilter = "";

			public int StartRecord;

			public DbDataReader OldRecordset = null;

			public DbDataReader Recordset = null; // Reserved // DN

			public bool CopyRecord;

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

				// Check modal
				if (IsModal)
					SkipHeaderFooter = true;
				IsMobileOrModal = IsMobile() || IsModal;
				FormClassName = "ew-form ew-add-form ew-horizontal";
				bool postBack = false;
				StringValues sv;

				// Set up current action
				if (IsApi()) {
					CurrentAction = "insert"; // Add record directly
					postBack = true;
				} else if (Post("action", out sv)) {
					CurrentAction = sv; // Get form action
					postBack = true;
				} else { // Not post back

					// Load key from QueryString
					CopyRecord = true;
					string[] keyValues = null;
					object rv;
					if (IsApi() && RouteValues.TryGetValue("key", out object k))
						keyValues = k.ToString().Split('/');
					if (RouteValues.TryGetValue("LeadId", out rv)) { // DN
						LeadId.QueryValue = Convert.ToString(rv);
						SetKey("LeadId", LeadId.CurrentValue); // Set up key
					} else if (Get("LeadId", out sv)) {
						LeadId.QueryValue = sv;
						SetKey("LeadId", LeadId.CurrentValue); // Set up key
					} else if (IsApi() && !Empty(keyValues)) {
						LeadId.QueryValue = Convert.ToString(keyValues[0]);
						SetKey("LeadId", LeadId.CurrentValue); // Set up key
					} else {
						SetKey("LeadId", ""); // Clear key
						CopyRecord = false;
					}
					if (CopyRecord) {
						CurrentAction = "copy"; // Copy record
					} else {
						CurrentAction = "show"; // Display blank record
					}
				}

				// Load old record / default values
				bool loaded = await LoadOldRecord();

				// Load form values
				if (postBack) {
					await LoadFormValues(); // Load form values
				}

				// Validate form if post back
				if (postBack) {
					if (!await ValidateForm()) {
						EventCancelled = true; // Event cancelled
						RestoreFormValues(); // Restore form values
						FailureMessage = FormError;
						if (IsApi())
							return Terminate();
						else
							CurrentAction = "show"; // Form error, reset action
					}
				}

				// Perform current action
				switch (CurrentAction) {
					case "copy": // Copy an existing record
						using (OldRecordset) {} // Dispose
						if (!loaded) { // Record not loaded
							if (Empty(FailureMessage))
								FailureMessage = Language.Phrase("NoRecord"); // No record found
							return Terminate("_Leadslist"); // No matching record, return to List page // DN
						}
						break;
					case "insert": // Add new record // DN
						SendEmail = true; // Send email on add success
						var rsold = Connection.GetRow(OldRecordset);
						using (OldRecordset) {} // Dispose
						var res = await AddRow(rsold);
						if (res) { // Add successful
							if (Empty(SuccessMessage))
								SuccessMessage = Language.Phrase("AddSuccess"); // Set up success message
							string returnUrl = "";
							returnUrl = ReturnUrl;
							if (GetPageName(returnUrl) == "_Leadslist")
								returnUrl = AddMasterUrl(ListUrl); // List page, return to List page with correct master key if necessary
							else if (GetPageName(returnUrl) == "_Leadsview")
								returnUrl = ViewUrl; // View page, return to View page with key URL directly
							if (IsApi()) // Return to caller
								return res;
							else
								return Terminate(returnUrl);
						} else if (IsApi()) { // API request, return
							return Terminate();
						} else {
							EventCancelled = true; // Event cancelled
							RestoreFormValues(); // Add failed, restore form values
						}
						break;
				}

				// Set up Breadcrumb
				SetupBreadcrumb();

				// Render row based on row type
				RowType = Config.RowTypeAdd; // Render add type

				// Render row
				ResetAttributes();
				await RenderRow();
				return PageResult();
			}

			// Confirm page
			public bool ConfirmPage = false; // DN
			#pragma warning disable 1998

			// Get upload files
			public async Task GetUploadFiles()
			{

				// Get upload data
			}
			#pragma warning restore 1998

			// Load default values
			protected void LoadDefaultValues() {
				LeadId.CurrentValue = System.DBNull.Value;
				LeadId.OldValue = LeadId.CurrentValue;
				_Name.CurrentValue = System.DBNull.Value;
				_Name.OldValue = _Name.CurrentValue;
				State.CurrentValue = System.DBNull.Value;
				State.OldValue = State.CurrentValue;
				LeadStatusId.CurrentValue = System.DBNull.Value;
				LeadStatusId.OldValue = LeadStatusId.CurrentValue;
				BranchId.CurrentValue = System.DBNull.Value;
				BranchId.OldValue = BranchId.CurrentValue;
				UserId.CurrentValue = System.DBNull.Value;
				UserId.OldValue = UserId.CurrentValue;
				FirstName.CurrentValue = System.DBNull.Value;
				FirstName.OldValue = FirstName.CurrentValue;
				LastName.CurrentValue = System.DBNull.Value;
				LastName.OldValue = LastName.CurrentValue;
				BlobUrl.CurrentValue = System.DBNull.Value;
				BlobUrl.OldValue = BlobUrl.CurrentValue;
				EmailAddress.CurrentValue = System.DBNull.Value;
				EmailAddress.OldValue = EmailAddress.CurrentValue;
				PhoneNumber.CurrentValue = System.DBNull.Value;
				PhoneNumber.OldValue = PhoneNumber.CurrentValue;
			}
			#pragma warning disable 1998

			// Load form values
			protected async Task LoadFormValues() {
				string val;

				// Check field name 'LeadId' first before field var 'x_LeadId'
				val = CurrentForm.GetValue("LeadId", "x_LeadId");
				if (!LeadId.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("LeadId", "x_LeadId")) // DN
						LeadId.Visible = false; // Disable update for API request
					else
						LeadId.FormValue = val;
				}

				// Check field name 'Name' first before field var 'x__Name'
				val = CurrentForm.GetValue("Name", "x__Name");
				if (!_Name.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("Name", "x__Name")) // DN
						_Name.Visible = false; // Disable update for API request
					else
						_Name.FormValue = val;
				}

				// Check field name 'State' first before field var 'x_State'
				val = CurrentForm.GetValue("State", "x_State");
				if (!State.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("State", "x_State")) // DN
						State.Visible = false; // Disable update for API request
					else
						State.FormValue = val;
				}

				// Check field name 'LeadStatusId' first before field var 'x_LeadStatusId'
				val = CurrentForm.GetValue("LeadStatusId", "x_LeadStatusId");
				if (!LeadStatusId.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("LeadStatusId", "x_LeadStatusId")) // DN
						LeadStatusId.Visible = false; // Disable update for API request
					else
						LeadStatusId.FormValue = val;
				}

				// Check field name 'BranchId' first before field var 'x_BranchId'
				val = CurrentForm.GetValue("BranchId", "x_BranchId");
				if (!BranchId.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("BranchId", "x_BranchId")) // DN
						BranchId.Visible = false; // Disable update for API request
					else
						BranchId.FormValue = val;
				}

				// Check field name 'UserId' first before field var 'x_UserId'
				val = CurrentForm.GetValue("UserId", "x_UserId");
				if (!UserId.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("UserId", "x_UserId")) // DN
						UserId.Visible = false; // Disable update for API request
					else
						UserId.FormValue = val;
				}

				// Check field name 'FirstName' first before field var 'x_FirstName'
				val = CurrentForm.GetValue("FirstName", "x_FirstName");
				if (!FirstName.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("FirstName", "x_FirstName")) // DN
						FirstName.Visible = false; // Disable update for API request
					else
						FirstName.FormValue = val;
				}

				// Check field name 'LastName' first before field var 'x_LastName'
				val = CurrentForm.GetValue("LastName", "x_LastName");
				if (!LastName.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("LastName", "x_LastName")) // DN
						LastName.Visible = false; // Disable update for API request
					else
						LastName.FormValue = val;
				}

				// Check field name 'BlobUrl' first before field var 'x_BlobUrl'
				val = CurrentForm.GetValue("BlobUrl", "x_BlobUrl");
				if (!BlobUrl.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("BlobUrl", "x_BlobUrl")) // DN
						BlobUrl.Visible = false; // Disable update for API request
					else
						BlobUrl.FormValue = val;
				}

				// Check field name 'EmailAddress' first before field var 'x_EmailAddress'
				val = CurrentForm.GetValue("EmailAddress", "x_EmailAddress");
				if (!EmailAddress.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("EmailAddress", "x_EmailAddress")) // DN
						EmailAddress.Visible = false; // Disable update for API request
					else
						EmailAddress.FormValue = val;
				}

				// Check field name 'PhoneNumber' first before field var 'x_PhoneNumber'
				val = CurrentForm.GetValue("PhoneNumber", "x_PhoneNumber");
				if (!PhoneNumber.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("PhoneNumber", "x_PhoneNumber")) // DN
						PhoneNumber.Visible = false; // Disable update for API request
					else
						PhoneNumber.FormValue = val;
				}
			}
			#pragma warning restore 1998

			// Restore form values
			public void RestoreFormValues() {
				LeadId.CurrentValue = LeadId.FormValue;
				_Name.CurrentValue = _Name.FormValue;
				State.CurrentValue = State.FormValue;
				LeadStatusId.CurrentValue = LeadStatusId.FormValue;
				BranchId.CurrentValue = BranchId.FormValue;
				UserId.CurrentValue = UserId.FormValue;
				FirstName.CurrentValue = FirstName.FormValue;
				LastName.CurrentValue = LastName.FormValue;
				BlobUrl.CurrentValue = BlobUrl.FormValue;
				EmailAddress.CurrentValue = EmailAddress.FormValue;
				PhoneNumber.CurrentValue = PhoneNumber.FormValue;
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
				LoadDefaultValues();
				var row = new Dictionary<string, object>();
				row.Add("LeadId", LeadId.CurrentValue);
				row.Add("Name", _Name.CurrentValue);
				row.Add("State", State.CurrentValue);
				row.Add("LeadStatusId", LeadStatusId.CurrentValue);
				row.Add("BranchId", BranchId.CurrentValue);
				row.Add("UserId", UserId.CurrentValue);
				row.Add("FirstName", FirstName.CurrentValue);
				row.Add("LastName", LastName.CurrentValue);
				row.Add("BlobUrl", BlobUrl.CurrentValue);
				row.Add("EmailAddress", EmailAddress.CurrentValue);
				row.Add("PhoneNumber", PhoneNumber.CurrentValue);
				return row;
			}
			#pragma warning disable 618, 1998

			// Load old record
			protected async Task<bool> LoadOldRecord(DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> cnn = null) {
				bool validKey = true;
				if (!Empty(GetKey("LeadId")))
					LeadId.OldValue = GetKey("LeadId"); // LeadId
				else
					validKey = false;

				// Load old record
				OldRecordset = null;
				if (validKey) {
					CurrentFilter = GetRecordFilter();
					string sql = CurrentSql;
					try {
						if (cnn != null) {
							OldRecordset = await cnn.OpenDataReaderAsync(sql);
						 } else {
							OldRecordset = await Connection.OpenDataReaderAsync(sql);
						 }
						if (OldRecordset != null)
							await OldRecordset.ReadAsync();
					} catch {
						OldRecordset = null;
					}
				}
				await LoadRowValues(OldRecordset); // Load row values
				return validKey;
			}
			#pragma warning restore 618, 1998
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
					LeadId.ViewValue = Convert.ToString(LeadId.CurrentValue); // DN
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
				} else if (RowType == Config.RowTypeAdd) { // Add row

					// LeadId
					LeadId.EditAttrs["class"] = "form-control";
					LeadId.EditValue = LeadId.CurrentValue; // DN
					LeadId.PlaceHolder = RemoveHtml(LeadId.Caption);

					// Name
					_Name.EditAttrs["class"] = "form-control";
					if (!_Name.Raw)
						_Name.CurrentValue = HtmlDecode(_Name.CurrentValue);
					_Name.EditValue = _Name.CurrentValue; // DN
					_Name.PlaceHolder = RemoveHtml(_Name.Caption);

					// State
					State.EditAttrs["class"] = "form-control";
					if (!State.Raw)
						State.CurrentValue = HtmlDecode(State.CurrentValue);
					State.EditValue = State.CurrentValue; // DN
					State.PlaceHolder = RemoveHtml(State.Caption);

					// LeadStatusId
					LeadStatusId.EditAttrs["class"] = "form-control";
					curVal = Convert.ToString(LeadStatusId.CurrentValue)?.Trim() ?? "";
					if (curVal != "")
						LeadStatusId.ViewValue = LeadStatusId.LookupCacheOption(curVal);
					else
						LeadStatusId.ViewValue = LeadStatusId.Lookup != null && IsList(LeadStatusId.Lookup.Options) ? curVal : null;
					if (LeadStatusId.ViewValue != null) { // Load from cache
						LeadStatusId.EditValue = LeadStatusId.Lookup.Options.Values.ToList();
					} else { // Lookup from database
						if (curVal == "") {
							filterWrk = "0=1";
						} else {
							filterWrk = "[Id]" + SearchString("=", Convert.ToString(LeadStatusId.CurrentValue), Config.DataTypeNumber, "");
						}
						sqlWrk = LeadStatusId.Lookup.GetSql(true, filterWrk, null, this);
						rswrk = await Connection.GetRowsAsync(sqlWrk);
						LeadStatusId.EditValue = rswrk;
					}

					// BranchId
					BranchId.EditAttrs["class"] = "form-control";
					curVal = Convert.ToString(BranchId.CurrentValue)?.Trim() ?? "";
					if (curVal != "")
						BranchId.ViewValue = BranchId.LookupCacheOption(curVal);
					else
						BranchId.ViewValue = BranchId.Lookup != null && IsList(BranchId.Lookup.Options) ? curVal : null;
					if (BranchId.ViewValue != null) { // Load from cache
						BranchId.EditValue = BranchId.Lookup.Options.Values.ToList();
					} else { // Lookup from database
						if (curVal == "") {
							filterWrk = "0=1";
						} else {
							filterWrk = "[Id]" + SearchString("=", Convert.ToString(BranchId.CurrentValue), Config.DataTypeNumber, "");
						}
						sqlWrk = BranchId.Lookup.GetSql(true, filterWrk, null, this);
						rswrk = await Connection.GetRowsAsync(sqlWrk);
						BranchId.EditValue = rswrk;
					}

					// UserId
					UserId.EditAttrs["class"] = "form-control";
					UserId.EditValue = UserId.CurrentValue; // DN
					UserId.PlaceHolder = RemoveHtml(UserId.Caption);

					// FirstName
					FirstName.EditAttrs["class"] = "form-control";
					if (!FirstName.Raw)
						FirstName.CurrentValue = HtmlDecode(FirstName.CurrentValue);
					FirstName.EditValue = FirstName.CurrentValue; // DN
					FirstName.PlaceHolder = RemoveHtml(FirstName.Caption);

					// LastName
					LastName.EditAttrs["class"] = "form-control";
					if (!LastName.Raw)
						LastName.CurrentValue = HtmlDecode(LastName.CurrentValue);
					LastName.EditValue = LastName.CurrentValue; // DN
					LastName.PlaceHolder = RemoveHtml(LastName.Caption);

					// BlobUrl
					BlobUrl.EditAttrs["class"] = "form-control";
					if (!BlobUrl.Raw)
						BlobUrl.CurrentValue = HtmlDecode(BlobUrl.CurrentValue);
					BlobUrl.EditValue = BlobUrl.CurrentValue; // DN
					BlobUrl.PlaceHolder = RemoveHtml(BlobUrl.Caption);

					// EmailAddress
					EmailAddress.EditAttrs["class"] = "form-control";
					if (!EmailAddress.Raw)
						EmailAddress.CurrentValue = HtmlDecode(EmailAddress.CurrentValue);
					EmailAddress.EditValue = EmailAddress.CurrentValue; // DN
					EmailAddress.PlaceHolder = RemoveHtml(EmailAddress.Caption);

					// PhoneNumber
					PhoneNumber.EditAttrs["class"] = "form-control";
					if (!PhoneNumber.Raw)
						PhoneNumber.CurrentValue = HtmlDecode(PhoneNumber.CurrentValue);
					PhoneNumber.EditValue = PhoneNumber.CurrentValue; // DN
					PhoneNumber.PlaceHolder = RemoveHtml(PhoneNumber.Caption);

					// Add refer script
					// LeadId

					LeadId.HrefValue = "";

					// Name
					_Name.HrefValue = "";

					// State
					State.HrefValue = "";

					// LeadStatusId
					LeadStatusId.HrefValue = "";

					// BranchId
					BranchId.HrefValue = "";

					// UserId
					UserId.HrefValue = "";

					// FirstName
					FirstName.HrefValue = "";

					// LastName
					LastName.HrefValue = "";

					// BlobUrl
					BlobUrl.HrefValue = "";

					// EmailAddress
					EmailAddress.HrefValue = "";

					// PhoneNumber
					PhoneNumber.HrefValue = "";
				}
				if (RowType == Config.RowTypeAdd || RowType == Config.RowTypeEdit || RowType == Config.RowTypeSearch) // Add/Edit/Search row
					SetupFieldTitles();

				// Call Row Rendered event
				if (RowType != Config.RowTypeAggregateInit)
					Row_Rendered();
			}
			#pragma warning restore 1998
			#pragma warning disable 1998

			// Validate form
			protected async Task<bool> ValidateForm() {

				// Initialize form error message
				FormError = "";

				// Check if validation required
				if (!Config.ServerValidate)
					return (FormError == "");
				if (LeadId.Required) {
					if (!LeadId.IsDetailKey && Empty(LeadId.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(LeadId.RequiredErrorMessage).Replace("%s", LeadId.Caption));
					}
				}
				if (!CheckGuid(LeadId.FormValue)) {
					FormError = AddMessage(FormError, LeadId.ErrorMessage);
				}
				if (_Name.Required) {
					if (!_Name.IsDetailKey && Empty(_Name.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(_Name.RequiredErrorMessage).Replace("%s", _Name.Caption));
					}
				}
				if (State.Required) {
					if (!State.IsDetailKey && Empty(State.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(State.RequiredErrorMessage).Replace("%s", State.Caption));
					}
				}
				if (LeadStatusId.Required) {
					if (!LeadStatusId.IsDetailKey && Empty(LeadStatusId.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(LeadStatusId.RequiredErrorMessage).Replace("%s", LeadStatusId.Caption));
					}
				}
				if (BranchId.Required) {
					if (!BranchId.IsDetailKey && Empty(BranchId.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(BranchId.RequiredErrorMessage).Replace("%s", BranchId.Caption));
					}
				}
				if (UserId.Required) {
					if (!UserId.IsDetailKey && Empty(UserId.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(UserId.RequiredErrorMessage).Replace("%s", UserId.Caption));
					}
				}
				if (!CheckGuid(UserId.FormValue)) {
					FormError = AddMessage(FormError, UserId.ErrorMessage);
				}
				if (FirstName.Required) {
					if (!FirstName.IsDetailKey && Empty(FirstName.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(FirstName.RequiredErrorMessage).Replace("%s", FirstName.Caption));
					}
				}
				if (LastName.Required) {
					if (!LastName.IsDetailKey && Empty(LastName.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(LastName.RequiredErrorMessage).Replace("%s", LastName.Caption));
					}
				}
				if (BlobUrl.Required) {
					if (!BlobUrl.IsDetailKey && Empty(BlobUrl.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(BlobUrl.RequiredErrorMessage).Replace("%s", BlobUrl.Caption));
					}
				}
				if (EmailAddress.Required) {
					if (!EmailAddress.IsDetailKey && Empty(EmailAddress.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(EmailAddress.RequiredErrorMessage).Replace("%s", EmailAddress.Caption));
					}
				}
				if (PhoneNumber.Required) {
					if (!PhoneNumber.IsDetailKey && Empty(PhoneNumber.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(PhoneNumber.RequiredErrorMessage).Replace("%s", PhoneNumber.Caption));
					}
				}

				// Return validate result
				bool valid = Empty(FormError);

				// Call Form_CustomValidate event
				string formCustomError = "";
				valid = valid && Form_CustomValidate(ref formCustomError);
				FormError = AddMessage(FormError, formCustomError);
				return valid;
			}
			#pragma warning restore 1998

			// Save data to memory cache
			public void SetCache<T>(string key, T value, int span) => Cache.Set<T>(key, value, new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMilliseconds(span))); // Keep in cache for this time, reset time if accessed

			// Gete data from memory cache
			public void GetCache<T>(string key) => Cache.Get<T>(key);

			// Add record
			#pragma warning disable 168, 219
			protected async Task<JsonBoolResult> AddRow(Dictionary<string, object> rsold = null) { // DN
				bool result = false;
				var rsnew = new Dictionary<string, object>();
				if (!Empty(LeadId.CurrentValue)) { // Check field with unique index
					var filter = "(LeadId = '" + AdjustSql(LeadId.CurrentValue, DbId) + "')";
					using var rschk = await LoadRs(filter);
					if (rschk != null && await rschk.ReadAsync()) {
						FailureMessage = Language.Phrase("DupIndex").Replace("%f", LeadId.Caption).Replace("%v", Convert.ToString(LeadId.CurrentValue));
						return JsonBoolResult.FalseResult;
					}
				}

				// Load db values from rsold
				LoadDbValues(rsold);
				if (rsold != null) {
				}
				try {

					// LeadId
					LeadId.SetDbValue(rsnew, LeadId.CurrentValue, "{00000000-0000-0000-0000-000000000000}", false);
					if (!Empty(rsnew["LeadId"]))
						rsnew["LeadId"] = new Guid(Convert.ToString(rsnew["LeadId"])); // DN

					// Name
					_Name.SetDbValue(rsnew, _Name.CurrentValue, "", false);

					// State
					State.SetDbValue(rsnew, State.CurrentValue, "", false);

					// LeadStatusId
					LeadStatusId.SetDbValue(rsnew, LeadStatusId.CurrentValue, System.DBNull.Value, false);

					// BranchId
					BranchId.SetDbValue(rsnew, BranchId.CurrentValue, System.DBNull.Value, false);

					// UserId
					UserId.SetDbValue(rsnew, UserId.CurrentValue, System.DBNull.Value, false);
					if (!Empty(rsnew["UserId"]))
						rsnew["UserId"] = new Guid(Convert.ToString(rsnew["UserId"])); // DN

					// FirstName
					FirstName.SetDbValue(rsnew, FirstName.CurrentValue, System.DBNull.Value, false);

					// LastName
					LastName.SetDbValue(rsnew, LastName.CurrentValue, System.DBNull.Value, false);

					// BlobUrl
					BlobUrl.SetDbValue(rsnew, BlobUrl.CurrentValue, System.DBNull.Value, false);

					// EmailAddress
					EmailAddress.SetDbValue(rsnew, EmailAddress.CurrentValue, System.DBNull.Value, false);

					// PhoneNumber
					PhoneNumber.SetDbValue(rsnew, PhoneNumber.CurrentValue, System.DBNull.Value, false);
				} catch (Exception e) {
					if (Config.Debug)
						throw;
					FailureMessage = e.Message;
					return JsonBoolResult.FalseResult;
				}

				// Call Row Inserting event
				bool insertRow = Row_Inserting(rsold, rsnew);

				// Check if key value entered
				if (insertRow && ValidateKey && Empty(rsnew["LeadId"])) {
					FailureMessage = Language.Phrase("InvalidKeyValue");
					insertRow = false;
				}

				// Check for duplicate key
				if (insertRow && ValidateKey) {
					string filter = GetRecordFilter(rsnew);
					using var rschk = await LoadRs(filter);
					if (rschk != null && await rschk.ReadAsync()) {
						FailureMessage = Language.Phrase("DupKey").Replace("%f", filter);
						insertRow = false;
					}
				}
				if (insertRow) {
					try {
						await InsertAsync(rsnew);
						result = true;
					} catch (Exception e) {
						if (Config.Debug)
							throw;
						FailureMessage = e.Message;
						result = false;
					}
				} else {
					if (SuccessMessage != "" || FailureMessage != "") {

						// Use the message, do nothing
					} else if (CancelMessage != "") {
						FailureMessage = CancelMessage;
						CancelMessage = "";
					} else {
						FailureMessage = Language.Phrase("InsertCancelled");
					}
					result = false;
				}

				// Call Row Inserted event
				if (result)
					Row_Inserted(rsold, rsnew);

				// Write JSON for API request
				var d = new Dictionary<string, object>();
				d.Add("success", result);
				if (IsApi() && result) {
					var row = GetRecordFromDictionary(rsnew);
					d.Add(TableVar, row);
					d.Add("version", Config.ProductVersion);
					return new JsonBoolResult(d, result);
				}
				return new JsonBoolResult(d, result);
			}

			// Set up Breadcrumb
			protected void SetupBreadcrumb() {
				var breadcrumb = new Breadcrumb();
				string url = CurrentUrl();
				breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("_Leadslist")), "", TableVar, true);
				string pageId = IsCopy ? "Copy" : "Add";
				breadcrumb.Add("add", pageId, url);
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

			// Close recordset
			public void CloseRecordset() {
				using (Recordset) {} // Dispose
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