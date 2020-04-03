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
		/// AuditTrail_Add
		/// </summary>
		public static _AuditTrail_Add AuditTrail_Add {
			get => HttpData.Get<_AuditTrail_Add>("AuditTrail_Add");
			set => HttpData["AuditTrail_Add"] = value;
		}

		/// <summary>
		/// Page class for AuditTrail
		/// </summary>
		public class _AuditTrail_Add : _AuditTrail_AddBase
		{

			// Construtor
			public _AuditTrail_Add(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>
		public class _AuditTrail_AddBase : _AuditTrail, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "add";

			// Project ID
			public string ProjectID = "{DE72B0A5-4A34-400E-B744-FF3F81D69E8F}";

			// Table name
			public string TableName { get; set; } = "AuditTrail";

			// Page object name
			public string PageObjName = "AuditTrail_Add";

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
			public _AuditTrail_AddBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language ??= new Lang();

				// Table object (AuditTrail)
				if (AuditTrail == null || AuditTrail is _AuditTrail)
					AuditTrail = this;

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
					dynamic doc = CreateInstance(classname, new object[] { AuditTrail, "" }); // DN
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
								if (pageName == "AuditTrailview")
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
				key += UrlEncode(Convert.ToString(ar["Id"]));
				return key;
			}

			// Hide fields for Add/Edit
			protected void HideFieldsForAddEdit() {
				if (IsAdd || IsCopy || IsGridAdd)
					Id.Visible = false;
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
				Id.Visible = false;
				_DateTime.SetVisibility();
				Script.SetVisibility();
				_User.SetVisibility();
				Action.SetVisibility();
				_Table.SetVisibility();
				_Field.SetVisibility();
				KeyValue.SetVisibility();
				OldValue.SetVisibility();
				NewValue.SetVisibility();
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
					if (RouteValues.TryGetValue("Id", out rv)) { // DN
						Id.QueryValue = Convert.ToString(rv);
						SetKey("Id", Id.CurrentValue); // Set up key
					} else if (Get("Id", out sv)) {
						Id.QueryValue = sv;
						SetKey("Id", Id.CurrentValue); // Set up key
					} else if (IsApi() && !Empty(keyValues)) {
						Id.QueryValue = Convert.ToString(keyValues[0]);
						SetKey("Id", Id.CurrentValue); // Set up key
					} else {
						SetKey("Id", ""); // Clear key
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
							return Terminate("AuditTraillist"); // No matching record, return to List page // DN
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
							if (GetPageName(returnUrl) == "AuditTraillist")
								returnUrl = AddMasterUrl(ListUrl); // List page, return to List page with correct master key if necessary
							else if (GetPageName(returnUrl) == "AuditTrailview")
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
				Id.CurrentValue = System.DBNull.Value;
				Id.OldValue = Id.CurrentValue;
				_DateTime.CurrentValue = System.DBNull.Value;
				_DateTime.OldValue = _DateTime.CurrentValue;
				Script.CurrentValue = System.DBNull.Value;
				Script.OldValue = Script.CurrentValue;
				_User.CurrentValue = System.DBNull.Value;
				_User.OldValue = _User.CurrentValue;
				Action.CurrentValue = System.DBNull.Value;
				Action.OldValue = Action.CurrentValue;
				_Table.CurrentValue = System.DBNull.Value;
				_Table.OldValue = _Table.CurrentValue;
				_Field.CurrentValue = System.DBNull.Value;
				_Field.OldValue = _Field.CurrentValue;
				KeyValue.CurrentValue = System.DBNull.Value;
				KeyValue.OldValue = KeyValue.CurrentValue;
				OldValue.CurrentValue = System.DBNull.Value;
				OldValue.OldValue = OldValue.CurrentValue;
				NewValue.CurrentValue = System.DBNull.Value;
				NewValue.OldValue = NewValue.CurrentValue;
			}
			#pragma warning disable 1998

			// Load form values
			protected async Task LoadFormValues() {
				string val;

				// Check field name 'DateTime' first before field var 'x__DateTime'
				val = CurrentForm.GetValue("DateTime", "x__DateTime");
				if (!_DateTime.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("DateTime", "x__DateTime")) // DN
						_DateTime.Visible = false; // Disable update for API request
					else
						_DateTime.FormValue = val;
					_DateTime.CurrentValue = UnformatDateTime(_DateTime.CurrentValue, 0);
				}

				// Check field name 'Script' first before field var 'x_Script'
				val = CurrentForm.GetValue("Script", "x_Script");
				if (!Script.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("Script", "x_Script")) // DN
						Script.Visible = false; // Disable update for API request
					else
						Script.FormValue = val;
				}

				// Check field name 'User' first before field var 'x__User'
				val = CurrentForm.GetValue("User", "x__User");
				if (!_User.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("User", "x__User")) // DN
						_User.Visible = false; // Disable update for API request
					else
						_User.FormValue = val;
				}

				// Check field name 'Action' first before field var 'x_Action'
				val = CurrentForm.GetValue("Action", "x_Action");
				if (!Action.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("Action", "x_Action")) // DN
						Action.Visible = false; // Disable update for API request
					else
						Action.FormValue = val;
				}

				// Check field name 'Table' first before field var 'x__Table'
				val = CurrentForm.GetValue("Table", "x__Table");
				if (!_Table.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("Table", "x__Table")) // DN
						_Table.Visible = false; // Disable update for API request
					else
						_Table.FormValue = val;
				}

				// Check field name 'Field' first before field var 'x__Field'
				val = CurrentForm.GetValue("Field", "x__Field");
				if (!_Field.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("Field", "x__Field")) // DN
						_Field.Visible = false; // Disable update for API request
					else
						_Field.FormValue = val;
				}

				// Check field name 'KeyValue' first before field var 'x_KeyValue'
				val = CurrentForm.GetValue("KeyValue", "x_KeyValue");
				if (!KeyValue.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("KeyValue", "x_KeyValue")) // DN
						KeyValue.Visible = false; // Disable update for API request
					else
						KeyValue.FormValue = val;
				}

				// Check field name 'OldValue' first before field var 'x_OldValue'
				val = CurrentForm.GetValue("OldValue", "x_OldValue");
				if (!OldValue.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("OldValue", "x_OldValue")) // DN
						OldValue.Visible = false; // Disable update for API request
					else
						OldValue.FormValue = val;
				}

				// Check field name 'NewValue' first before field var 'x_NewValue'
				val = CurrentForm.GetValue("NewValue", "x_NewValue");
				if (!NewValue.IsDetailKey) {
					if (IsApi() && !CurrentForm.HasValue("NewValue", "x_NewValue")) // DN
						NewValue.Visible = false; // Disable update for API request
					else
						NewValue.FormValue = val;
				}

				// Check field name 'Id' first before field var 'x_Id'
				val = CurrentForm.GetValue("Id", "x_Id");
			}
			#pragma warning restore 1998

			// Restore form values
			public void RestoreFormValues() {
				_DateTime.CurrentValue = _DateTime.FormValue;
				_DateTime.CurrentValue = UnformatDateTime(_DateTime.CurrentValue, 0);
				Script.CurrentValue = Script.FormValue;
				_User.CurrentValue = _User.FormValue;
				Action.CurrentValue = Action.FormValue;
				_Table.CurrentValue = _Table.FormValue;
				_Field.CurrentValue = _Field.FormValue;
				KeyValue.CurrentValue = KeyValue.FormValue;
				OldValue.CurrentValue = OldValue.FormValue;
				NewValue.CurrentValue = NewValue.FormValue;
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
				Id.SetDbValue(row["Id"]);
				_DateTime.SetDbValue(row["DateTime"]);
				Script.SetDbValue(row["Script"]);
				_User.SetDbValue(row["User"]);
				Action.SetDbValue(row["Action"]);
				_Table.SetDbValue(row["Table"]);
				_Field.SetDbValue(row["Field"]);
				KeyValue.SetDbValue(row["KeyValue"]);
				OldValue.SetDbValue(row["OldValue"]);
				NewValue.SetDbValue(row["NewValue"]);
			}
			#pragma warning restore 162, 168, 1998

			// Return a row with default values
			protected Dictionary<string, object> NewRow() {
				LoadDefaultValues();
				var row = new Dictionary<string, object>();
				row.Add("Id", Id.CurrentValue);
				row.Add("DateTime", _DateTime.CurrentValue);
				row.Add("Script", Script.CurrentValue);
				row.Add("User", _User.CurrentValue);
				row.Add("Action", Action.CurrentValue);
				row.Add("Table", _Table.CurrentValue);
				row.Add("Field", _Field.CurrentValue);
				row.Add("KeyValue", KeyValue.CurrentValue);
				row.Add("OldValue", OldValue.CurrentValue);
				row.Add("NewValue", NewValue.CurrentValue);
				return row;
			}
			#pragma warning disable 618, 1998

			// Load old record
			protected async Task<bool> LoadOldRecord(DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> cnn = null) {
				bool validKey = true;
				if (!Empty(GetKey("Id")))
					Id.OldValue = GetKey("Id"); // Id
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
				// Id
				// DateTime
				// Script
				// User
				// Action
				// Table
				// Field
				// KeyValue
				// OldValue
				// NewValue

				if (RowType == Config.RowTypeView) { // View row

					// Id
					Id.ViewValue = Id.CurrentValue;
					Id.ViewCustomAttributes = "";

					// DateTime
					_DateTime.ViewValue = Convert.ToString(_DateTime.CurrentValue); // DN
					_DateTime.ViewValue = FormatDateTime(_DateTime.ViewValue, 0);
					_DateTime.ViewCustomAttributes = "";

					// Script
					Script.ViewValue = Convert.ToString(Script.CurrentValue); // DN
					Script.ViewCustomAttributes = "";

					// User
					_User.ViewValue = Convert.ToString(_User.CurrentValue); // DN
					_User.ViewCustomAttributes = "";

					// Action
					Action.ViewValue = Convert.ToString(Action.CurrentValue); // DN
					Action.ViewCustomAttributes = "";

					// Table
					_Table.ViewValue = Convert.ToString(_Table.CurrentValue); // DN
					_Table.ViewCustomAttributes = "";

					// Field
					_Field.ViewValue = Convert.ToString(_Field.CurrentValue); // DN
					_Field.ViewCustomAttributes = "";

					// KeyValue
					KeyValue.ViewValue = KeyValue.CurrentValue;
					KeyValue.ViewCustomAttributes = "";

					// OldValue
					OldValue.ViewValue = OldValue.CurrentValue;
					OldValue.ViewCustomAttributes = "";

					// NewValue
					NewValue.ViewValue = NewValue.CurrentValue;
					NewValue.ViewCustomAttributes = "";

					// DateTime
					_DateTime.HrefValue = "";
					_DateTime.TooltipValue = "";

					// Script
					Script.HrefValue = "";
					Script.TooltipValue = "";

					// User
					_User.HrefValue = "";
					_User.TooltipValue = "";

					// Action
					Action.HrefValue = "";
					Action.TooltipValue = "";

					// Table
					_Table.HrefValue = "";
					_Table.TooltipValue = "";

					// Field
					_Field.HrefValue = "";
					_Field.TooltipValue = "";

					// KeyValue
					KeyValue.HrefValue = "";
					KeyValue.TooltipValue = "";

					// OldValue
					OldValue.HrefValue = "";
					OldValue.TooltipValue = "";

					// NewValue
					NewValue.HrefValue = "";
					NewValue.TooltipValue = "";
				} else if (RowType == Config.RowTypeAdd) { // Add row

					// DateTime
					_DateTime.EditAttrs["class"] = "form-control";
					_DateTime.EditValue = FormatDateTime(_DateTime.CurrentValue, 8); // DN
					_DateTime.PlaceHolder = RemoveHtml(_DateTime.Caption);

					// Script
					Script.EditAttrs["class"] = "form-control";
					if (!Script.Raw)
						Script.CurrentValue = HtmlDecode(Script.CurrentValue);
					Script.EditValue = Script.CurrentValue; // DN
					Script.PlaceHolder = RemoveHtml(Script.Caption);

					// User
					_User.EditAttrs["class"] = "form-control";
					if (!_User.Raw)
						_User.CurrentValue = HtmlDecode(_User.CurrentValue);
					_User.EditValue = _User.CurrentValue; // DN
					_User.PlaceHolder = RemoveHtml(_User.Caption);

					// Action
					Action.EditAttrs["class"] = "form-control";
					if (!Action.Raw)
						Action.CurrentValue = HtmlDecode(Action.CurrentValue);
					Action.EditValue = Action.CurrentValue; // DN
					Action.PlaceHolder = RemoveHtml(Action.Caption);

					// Table
					_Table.EditAttrs["class"] = "form-control";
					if (!_Table.Raw)
						_Table.CurrentValue = HtmlDecode(_Table.CurrentValue);
					_Table.EditValue = _Table.CurrentValue; // DN
					_Table.PlaceHolder = RemoveHtml(_Table.Caption);

					// Field
					_Field.EditAttrs["class"] = "form-control";
					if (!_Field.Raw)
						_Field.CurrentValue = HtmlDecode(_Field.CurrentValue);
					_Field.EditValue = _Field.CurrentValue; // DN
					_Field.PlaceHolder = RemoveHtml(_Field.Caption);

					// KeyValue
					KeyValue.EditAttrs["class"] = "form-control";
					KeyValue.EditValue = KeyValue.CurrentValue; // DN
					KeyValue.PlaceHolder = RemoveHtml(KeyValue.Caption);

					// OldValue
					OldValue.EditAttrs["class"] = "form-control";
					OldValue.EditValue = OldValue.CurrentValue; // DN
					OldValue.PlaceHolder = RemoveHtml(OldValue.Caption);

					// NewValue
					NewValue.EditAttrs["class"] = "form-control";
					NewValue.EditValue = NewValue.CurrentValue; // DN
					NewValue.PlaceHolder = RemoveHtml(NewValue.Caption);

					// Add refer script
					// DateTime

					_DateTime.HrefValue = "";

					// Script
					Script.HrefValue = "";

					// User
					_User.HrefValue = "";

					// Action
					Action.HrefValue = "";

					// Table
					_Table.HrefValue = "";

					// Field
					_Field.HrefValue = "";

					// KeyValue
					KeyValue.HrefValue = "";

					// OldValue
					OldValue.HrefValue = "";

					// NewValue
					NewValue.HrefValue = "";
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
				if (_DateTime.Required) {
					if (!_DateTime.IsDetailKey && Empty(_DateTime.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(_DateTime.RequiredErrorMessage).Replace("%s", _DateTime.Caption));
					}
				}
				if (!CheckDate(_DateTime.FormValue)) {
					FormError = AddMessage(FormError, _DateTime.ErrorMessage);
				}
				if (Script.Required) {
					if (!Script.IsDetailKey && Empty(Script.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(Script.RequiredErrorMessage).Replace("%s", Script.Caption));
					}
				}
				if (_User.Required) {
					if (!_User.IsDetailKey && Empty(_User.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(_User.RequiredErrorMessage).Replace("%s", _User.Caption));
					}
				}
				if (Action.Required) {
					if (!Action.IsDetailKey && Empty(Action.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(Action.RequiredErrorMessage).Replace("%s", Action.Caption));
					}
				}
				if (_Table.Required) {
					if (!_Table.IsDetailKey && Empty(_Table.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(_Table.RequiredErrorMessage).Replace("%s", _Table.Caption));
					}
				}
				if (_Field.Required) {
					if (!_Field.IsDetailKey && Empty(_Field.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(_Field.RequiredErrorMessage).Replace("%s", _Field.Caption));
					}
				}
				if (KeyValue.Required) {
					if (!KeyValue.IsDetailKey && Empty(KeyValue.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(KeyValue.RequiredErrorMessage).Replace("%s", KeyValue.Caption));
					}
				}
				if (OldValue.Required) {
					if (!OldValue.IsDetailKey && Empty(OldValue.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(OldValue.RequiredErrorMessage).Replace("%s", OldValue.Caption));
					}
				}
				if (NewValue.Required) {
					if (!NewValue.IsDetailKey && Empty(NewValue.FormValue)) {
						FormError = AddMessage(FormError, Convert.ToString(NewValue.RequiredErrorMessage).Replace("%s", NewValue.Caption));
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

				// Load db values from rsold
				LoadDbValues(rsold);
				if (rsold != null) {
				}
				try {

					// DateTime
					_DateTime.SetDbValue(rsnew, UnformatDateTime(_DateTime.CurrentValue, 0), DateTime.Now, false);

					// Script
					Script.SetDbValue(rsnew, Script.CurrentValue, System.DBNull.Value, false);

					// User
					_User.SetDbValue(rsnew, _User.CurrentValue, System.DBNull.Value, false);

					// Action
					Action.SetDbValue(rsnew, Action.CurrentValue, System.DBNull.Value, false);

					// Table
					_Table.SetDbValue(rsnew, _Table.CurrentValue, System.DBNull.Value, false);

					// Field
					_Field.SetDbValue(rsnew, _Field.CurrentValue, System.DBNull.Value, false);

					// KeyValue
					KeyValue.SetDbValue(rsnew, KeyValue.CurrentValue, System.DBNull.Value, false);

					// OldValue
					OldValue.SetDbValue(rsnew, OldValue.CurrentValue, System.DBNull.Value, false);

					// NewValue
					NewValue.SetDbValue(rsnew, NewValue.CurrentValue, System.DBNull.Value, false);
				} catch (Exception e) {
					if (Config.Debug)
						throw;
					FailureMessage = e.Message;
					return JsonBoolResult.FalseResult;
				}

				// Call Row Inserting event
				bool insertRow = Row_Inserting(rsold, rsnew);
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
				breadcrumb.Add("list", TableVar, AppPath(AddMasterUrl("AuditTraillist")), "", TableVar, true);
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