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
		/// Application_List
		/// </summary>
		public static _Application_List Application_List {
			get => HttpData.Get<_Application_List>("Application_List");
			set => HttpData["Application_List"] = value;
		}

		/// <summary>
		/// Page class for Application
		/// </summary>
		public class _Application_List : _Application_ListBase
		{

			// Construtor
			public _Application_List(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>
		public class _Application_ListBase : _Application, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "list";

			// Project ID
			public string ProjectID = "{DE72B0A5-4A34-400E-B744-FF3F81D69E8F}";

			// Table name
			public string TableName { get; set; } = "Application";

			// Page object name
			public string PageObjName = "Application_List";

			// Grid form hidden field names
			public string FormName = "fApplicationlist";

			public string FormActionName = "k_action";

			public string FormKeyName = "k_key";

			public string FormOldKeyName = "k_oldkey";

			public string FormBlankRowName = "k_blankrow";

			public string FormKeyCountName = "key_count";

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

			// Export URLs
			public string ExportPrintUrl = "";

			public string ExportHtmlUrl = "";

			public string ExportExcelUrl = "";

			public string ExportWordUrl = "";

			public string ExportXmlUrl = "";

			public string ExportCsvUrl = "";

			public string ExportPdfUrl = "";

			// Custom export
			public bool ExportExcelCustom = false;

			public bool ExportWordCustom = false;

			public bool ExportPdfCustom = false;

			public bool ExportEmailCustom = false;

			// Update URLs
			public string InlineAddUrl = "";

			public string GridAddUrl = "";

			public string GridEditUrl = "";

			public string MultiDeleteUrl = "";

			public string MultiUpdateUrl = "";

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
			public _Application_ListBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				CurrentPage = this;

				// Language object
				Language ??= new Lang();

				// Table object (Application)
				if (Application == null || Application is _Application)
					Application = this;

				// Initialize URLs
				ExportPrintUrl = PageUrl + "export=print";
				ExportExcelUrl = PageUrl + "export=excel";
				ExportWordUrl = PageUrl + "export=word";
				ExportPdfUrl = PageUrl + "export=pdf";
				ExportHtmlUrl = PageUrl + "export=html";
				ExportXmlUrl = PageUrl + "export=xml";
				ExportCsvUrl = PageUrl + "export=csv";
				AddUrl = "Applicationadd";
				InlineAddUrl = PageUrl + "action=add";
				GridAddUrl = PageUrl + "action=gridadd";
				GridEditUrl = PageUrl + "action=gridedit";
				MultiDeleteUrl = "Applicationdelete";
				MultiUpdateUrl = "Applicationupdate";

				// Start time
				StartTime = Environment.TickCount;

				// Debug message
				LoadDebugMessage();

				// Open connection
				Conn = Connection; // DN

				// List options
				ListOptions = new ListOptions { TableVar = TableVar };

				// Export options
				ExportOptions = new ListOptions { Tag = "div", TagClassName = "ew-export-option" };

				// Import options
				ImportOptions = new ListOptions { Tag = "div", TagClassName = "ew-import-option" };

				// Other options
				OtherOptions["addedit"] = new ListOptions { Tag = "div", TagClassName = "ew-add-edit-option" };

				// Other options
				OtherOptions["detail"] = new ListOptions { Tag = "div", TagClassName = "ew-detail-option" };
				OtherOptions["action"] = new ListOptions { Tag = "div", TagClassName = "ew-action-option" };

				// Filter options
				FilterOptions = new ListOptions { Tag = "div", TagClassName = "ew-filter-option fApplicationlistsrch" };

				// List actions
				ListActions = new ListActions();
			}
			#pragma warning disable 1998

			// Export view result
			public async Task<IActionResult> ExportView() { // DN
				if (!Empty(CustomExport) && CustomExport == Export && Config.Export.TryGetValue(CustomExport, out string classname)) {
					IActionResult result = null;
					string content = await GetViewOutput();
					if (Empty(ExportFileName))
						ExportFileName = TableVar;
					dynamic doc = CreateInstance(classname, new object[] { Application, "" }); // DN
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
						SaveDebugMessage();
						return Controller.LocalRedirect(AppPath(url));
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
				key += UrlEncode(Convert.ToString(ar["ApplicationId"]));
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

			// Class properties
			private Pager _pager; // DN

			public ListOptions ListOptions; // List options

			public ListOptions ExportOptions; // Export options

			public ListOptions SearchOptions; // Search options

			public ListOptionsDictionary OtherOptions = new ListOptionsDictionary(); // Other options

			public ListOptions FilterOptions; // Filter options

			public ListOptions ImportOptions; // Import options

			public ListActions ListActions; // List actions

			public int SelectedCount = 0;

			public int SelectedIndex = 0;

			public int DisplayRecords = 20; // Number of display records

			public int StartRecord;

			public int StopRecord;

			public int TotalRecords = -1;

			public int RecordRange = 10;

			public string PageSizes = ""; // Page sizes (comma separated)

			public string DefaultSearchWhere = ""; // Default search WHERE clause

			public string SearchWhere = ""; // Search WHERE clause

			public string SearchPanelClass = "ew-search-panel collapse show"; // Search panel class

			public int SearchRowCount = 0; // For extended search

			public int SearchColumnCount = 0; // For extended search

			public int SearchFieldsPerRow = 1; // For extended search

			public int RecordCount = 0; // Record count

			public int EditRowCount;

			public int StartRowCount = 1;

			public List<dynamic> Attributes = new List<dynamic>(); // Row attributes and cell attributes

			public object RowIndex = 0; // Row index

			public int KeyCount = 0; // Key count

			public string RowAction = ""; // Row action

			public string RowOldKey = ""; // Row old key (for copy)

			public string MultiColumnClass = "col-sm";

			public string MultiColumnEditClass = "w-100";

			public int MultiColumnCount = 12;

			public int MultiColumnEditCount = 12;

			public string DbMasterFilter = ""; // Master filter

			public string DbDetailFilter = ""; // Detail filter

			public bool MasterRecordExists;

			public string MultiSelectKey = "";

			public bool RestoreSearch = false;

			public SubPages DetailPages;

			public DbDataReader Recordset;

			public DbDataReader OldRecordset;

			// Pager
			public Pager Pager {
				get {
					_pager ??= new PrevNextPager(StartRecord, RecordsPerPage, TotalRecords, PageSizes, RecordRange, AutoHidePager, AutoHidePageSizeSelector);
					return _pager;
				}
			}

			/// <summary>
			/// Page run
			/// </summary>
			/// <returns>Page result</returns>
			public async Task<IActionResult> Run() {

				// Header
				Header(Config.Cache);

				// User profile
				Profile = new UserProfile();

				// Security
				if (!await SetupApiRequest()) {
					Security ??= CreateSecurity(); // DN
				}
				CurrentAction = Param("action"); // Set up current action

				// Get grid add count
				int gridaddcnt = Get<int>(Config.TableGridAddRowCount);
				if (gridaddcnt > 0)
					GridAddRowCount = gridaddcnt;

				// Set up list options
				await SetupListOptions();
				ApplicationId.Visible = false;
				_Name.SetVisibility();
				ApplicationstatusId.SetVisibility();
				BranchId.SetVisibility();
				UserId.SetVisibility();
				State.SetVisibility();
				FirstName.SetVisibility();
				LastName.SetVisibility();
				BlobUrl.Visible = false;
				EmailAddress.SetVisibility();
				PhoneNumber.SetVisibility();
				HideFieldsForAddEdit();

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

				// Setup other options
				SetupOtherOptions();

				// Set up custom action (compatible with old version)
				ListActions.Add(CustomActions);

				// Show checkbox column if multiple action
				if (ListActions.Items.Any(kvp => kvp.Value.Select == Config.ActionMultiple && kvp.Value.Allowed))
					ListOptions["checkbox"].Visible = true;

				// Set up lookup cache
				await SetupLookupOptions(ApplicationstatusId);
				await SetupLookupOptions(BranchId);

				// Search filters
				string srchAdvanced = ""; // Advanced search filter
				string srchBasic = ""; // Basic search filter
				string filter = "";

				// Get command
				Command = Get("cmd").ToLower();
				if (IsPageRequest) { // Validate request

					// Process list action first
					var result = await ProcessListAction();
					if (result != null) // Ajax request
						return result;

					// Handle reset command
					ResetCommand();

					// Set up Breadcrumb
					if (!IsExport())
						SetupBreadcrumb();

					// Hide list options
					if (IsExport()) {
						ListOptions.HideAllOptions(new List<string> {"sequence"});
						ListOptions.UseDropDownButton = false; // Disable drop down button
						ListOptions.UseButtonGroup = false; // Disable button group
					} else if (IsGridAdd || IsGridEdit) {
						ListOptions.HideAllOptions();
						ListOptions.UseDropDownButton = false; // Disable drop down button
						ListOptions.UseButtonGroup = false; // Disable button group
					}

					// Hide options
					if (IsExport() || !Empty(CurrentAction)) {
						ExportOptions.HideAllOptions();
						FilterOptions.HideAllOptions();
						ImportOptions.HideAllOptions();
					}

					// Hide other options
					if (IsExport()) {
						foreach (var (key, value) in OtherOptions)
							value.HideAllOptions();
					}

					// Get default search criteria
					AddFilter(ref DefaultSearchWhere, BasicSearchWhere(true));
					AddFilter(ref DefaultSearchWhere, AdvancedSearchWhere(true));

					// Get basic search values
					LoadBasicSearchValues();

					// Get and validate search values for advanced search
					LoadSearchValues(); // Get search values

					// Process filter list
					var filterResult = await ProcessFilterList();
					if (filterResult != null) {

						// Clean output buffer
						if (!Config.Debug)
							Response.Clear();
						return Controller.Json(filterResult);
					}
					if (!ValidateSearch())
						FailureMessage = SearchError;

					// Restore search parms from Session if not searching / reset / export
					if ((IsExport() || Command != "search" && Command != "reset" && Command != "resetall") && Command != "json" && CheckSearchParms())
						RestoreSearchParms();

					// Call Recordset SearchValidated event
					Recordset_SearchValidated();

					// Set up sorting order
					SetupSortOrder();

					// Get basic search criteria
					if (Empty(SearchError))
						srchBasic = BasicSearchWhere();

					// Get search criteria for advanced search
					if (Empty(SearchError))
						srchAdvanced = AdvancedSearchWhere();
				}

				// Restore display records
				if (Command != "json" && (RecordsPerPage == -1 || RecordsPerPage > 0)) {
					DisplayRecords = RecordsPerPage; // Restore from Session
				} else {
					DisplayRecords = 20; // Load default
					RecordsPerPage = DisplayRecords; // Save default to session
				}

				// Load Sorting Order
				if (Command != "json")
					LoadSortOrder();

				// Load search default if no existing search criteria
				if (!CheckSearchParms()) {

					// Load basic search from default
					BasicSearch.LoadDefault();
					if (!Empty(BasicSearch.Keyword))
						srchBasic = BasicSearchWhere();

					// Load advanced search from default
					if (LoadAdvancedSearchDefault())
						srchAdvanced = AdvancedSearchWhere();
				}

				// Build search criteria
				AddFilter(ref SearchWhere, srchAdvanced);
				AddFilter(ref SearchWhere, srchBasic);

				// Call Recordset_Searching event
				Recordset_Searching(ref SearchWhere);

				// Save search criteria
				if (Command == "search" && !RestoreSearch) {
					SessionSearchWhere = SearchWhere; // Save to Session (rename as SessionSearchWhere property)
					StartRecord = 1; // Reset start record counter
					StartRecordNumber = StartRecord;
				} else if (Command != "json") {
					SearchWhere = SessionSearchWhere;
				}

				// Build filter
				filter = "";
				AddFilter(ref filter, DbDetailFilter);
				AddFilter(ref filter, SearchWhere);

				// Set up filter
				if (Command == "json") {
					UseSessionForListSql = false; // Do not use session for ListSql
					CurrentFilter = filter;
				} else {
					SessionWhere = filter;
					CurrentFilter = "";
				}
				if (IsGridAdd) {
					CurrentFilter = "0=1";
					StartRecord = 1;
					DisplayRecords = GridAddRowCount;
					TotalRecords = DisplayRecords;
					StopRecord = DisplayRecords;
				} else {
					TotalRecords = await ListRecordCount();
					StopRecord = DisplayRecords;
					StartRecord = 1;
				if (DisplayRecords <= 0 || (IsExport() && ExportAll)) // Display all records
					DisplayRecords = TotalRecords;
				if (!(IsExport() && ExportAll)) // Set up start record position
					SetupStartRecord();

				// Recordset
				bool selectLimit = UseSelectLimit;
				if (selectLimit)
					Recordset = await LoadRecordset(StartRecord - 1, DisplayRecords);

				// Set no record found message
				if (Empty(CurrentAction) && TotalRecords == 0) {
					if (SearchWhere == "0=101")
						WarningMessage = Language.Phrase("EnterSearchCriteria");
					else
						WarningMessage = Language.Phrase("NoRecord");
				}
				}

				// Search options
				SetupSearchOptions();

				// Normal return
				if (IsApi()) {
					if (!Connection.SelectOffset) // DN
						for (var i = 1; i <= StartRecord - 1; i++) // Move to first record
							await Recordset.ReadAsync();
					using (Recordset) {
						return Controller.Json(new Dictionary<string, object> { {"success", true}, {TableVar, await GetRecordsFromRecordset(Recordset)}, {"totalRecordCount", TotalRecords}, {"version", Config.ProductVersion} });
					}
				}
				return PageResult();
			}

			// Build filter for all keys
			protected string BuildKeyFilter() {
				string wrkFilter = "";

				// Update row index and get row key
				int rowindex = 1;
				CurrentForm.Index = rowindex;
				string thisKey = CurrentForm.GetValue(FormKeyName);
				while (!Empty(thisKey)) {
					if (SetupKeyValues(thisKey)) {
						string filter = GetRecordFilter();
						if (!Empty(wrkFilter))
							wrkFilter += " OR ";
						wrkFilter += filter;
					} else {
						wrkFilter = "0=1";
						break;
					}

					// Update row index and get row key
					rowindex++; // next row
					CurrentForm.Index = rowindex;
					thisKey = CurrentForm.GetValue(FormKeyName);
				}
				return wrkFilter;
			}

			// Set up key values
			protected bool SetupKeyValues(string key) {
				var keyFields = key.Split(Convert.ToChar(Config.CompositeKeySeparator));
				if (keyFields.Length >= 1) {
					ApplicationId.OldValue = keyFields[0];
				}
				return true;
			}

			// Check if empty row
			public bool EmptyRow() => false;
			#pragma warning disable 162, 1998

			// Get list of filters
			public async Task<string> GetFilterList() {
				string filterList = "";

				// Initialize
				var filters = new JObject(); // DN
				filters.Merge(JObject.Parse(ApplicationId.AdvancedSearch.ToJson())); // Field ApplicationId
				filters.Merge(JObject.Parse(_Name.AdvancedSearch.ToJson())); // Field Name
				filters.Merge(JObject.Parse(ApplicationstatusId.AdvancedSearch.ToJson())); // Field ApplicationstatusId
				filters.Merge(JObject.Parse(BranchId.AdvancedSearch.ToJson())); // Field BranchId
				filters.Merge(JObject.Parse(UserId.AdvancedSearch.ToJson())); // Field UserId
				filters.Merge(JObject.Parse(State.AdvancedSearch.ToJson())); // Field State
				filters.Merge(JObject.Parse(FirstName.AdvancedSearch.ToJson())); // Field FirstName
				filters.Merge(JObject.Parse(LastName.AdvancedSearch.ToJson())); // Field LastName
				filters.Merge(JObject.Parse(BlobUrl.AdvancedSearch.ToJson())); // Field BlobUrl
				filters.Merge(JObject.Parse(EmailAddress.AdvancedSearch.ToJson())); // Field EmailAddress
				filters.Merge(JObject.Parse(PhoneNumber.AdvancedSearch.ToJson())); // Field PhoneNumber
				filters.Merge(JObject.Parse(Application.BasicSearch.ToJson()));

				// Return filter list in JSON
				if (filters.HasValues)
					filterList = "\"data\":" + filters.ToString();
				return (filterList != "") ? "{" + filterList + "}" : "null";
			}

			// Process filter list
			protected async Task<object> ProcessFilterList() {
				if (Post("cmd") == "resetfilter") {
					RestoreFilterList();
				}
				return null;
			}
			#pragma warning restore 162, 1998

			// Restore list of filters
			protected bool RestoreFilterList() {

				// Return if not reset filter
				if (Post("cmd") != "resetfilter")
					return false;
				Dictionary<string, string> filter = JsonConvert.DeserializeObject<Dictionary<string, string>>(Post("filter"));
				Command = "search";
				string sv;

				// Field ApplicationId
				if (filter.TryGetValue("x_ApplicationId", out sv)) {
					ApplicationId.AdvancedSearch.SearchValue = sv;
					ApplicationId.AdvancedSearch.SearchOperator = filter["z_ApplicationId"];
					ApplicationId.AdvancedSearch.SearchCondition = filter["v_ApplicationId"];
					ApplicationId.AdvancedSearch.SearchValue2 = filter["y_ApplicationId"];
					ApplicationId.AdvancedSearch.SearchOperator2 = filter["w_ApplicationId"];
					ApplicationId.AdvancedSearch.Save();
				}

				// Field Name
				if (filter.TryGetValue("x__Name", out sv)) {
					_Name.AdvancedSearch.SearchValue = sv;
					_Name.AdvancedSearch.SearchOperator = filter["z__Name"];
					_Name.AdvancedSearch.SearchCondition = filter["v__Name"];
					_Name.AdvancedSearch.SearchValue2 = filter["y__Name"];
					_Name.AdvancedSearch.SearchOperator2 = filter["w__Name"];
					_Name.AdvancedSearch.Save();
				}

				// Field ApplicationstatusId
				if (filter.TryGetValue("x_ApplicationstatusId", out sv)) {
					ApplicationstatusId.AdvancedSearch.SearchValue = sv;
					ApplicationstatusId.AdvancedSearch.SearchOperator = filter["z_ApplicationstatusId"];
					ApplicationstatusId.AdvancedSearch.SearchCondition = filter["v_ApplicationstatusId"];
					ApplicationstatusId.AdvancedSearch.SearchValue2 = filter["y_ApplicationstatusId"];
					ApplicationstatusId.AdvancedSearch.SearchOperator2 = filter["w_ApplicationstatusId"];
					ApplicationstatusId.AdvancedSearch.Save();
				}

				// Field BranchId
				if (filter.TryGetValue("x_BranchId", out sv)) {
					BranchId.AdvancedSearch.SearchValue = sv;
					BranchId.AdvancedSearch.SearchOperator = filter["z_BranchId"];
					BranchId.AdvancedSearch.SearchCondition = filter["v_BranchId"];
					BranchId.AdvancedSearch.SearchValue2 = filter["y_BranchId"];
					BranchId.AdvancedSearch.SearchOperator2 = filter["w_BranchId"];
					BranchId.AdvancedSearch.Save();
				}

				// Field UserId
				if (filter.TryGetValue("x_UserId", out sv)) {
					UserId.AdvancedSearch.SearchValue = sv;
					UserId.AdvancedSearch.SearchOperator = filter["z_UserId"];
					UserId.AdvancedSearch.SearchCondition = filter["v_UserId"];
					UserId.AdvancedSearch.SearchValue2 = filter["y_UserId"];
					UserId.AdvancedSearch.SearchOperator2 = filter["w_UserId"];
					UserId.AdvancedSearch.Save();
				}

				// Field State
				if (filter.TryGetValue("x_State", out sv)) {
					State.AdvancedSearch.SearchValue = sv;
					State.AdvancedSearch.SearchOperator = filter["z_State"];
					State.AdvancedSearch.SearchCondition = filter["v_State"];
					State.AdvancedSearch.SearchValue2 = filter["y_State"];
					State.AdvancedSearch.SearchOperator2 = filter["w_State"];
					State.AdvancedSearch.Save();
				}

				// Field FirstName
				if (filter.TryGetValue("x_FirstName", out sv)) {
					FirstName.AdvancedSearch.SearchValue = sv;
					FirstName.AdvancedSearch.SearchOperator = filter["z_FirstName"];
					FirstName.AdvancedSearch.SearchCondition = filter["v_FirstName"];
					FirstName.AdvancedSearch.SearchValue2 = filter["y_FirstName"];
					FirstName.AdvancedSearch.SearchOperator2 = filter["w_FirstName"];
					FirstName.AdvancedSearch.Save();
				}

				// Field LastName
				if (filter.TryGetValue("x_LastName", out sv)) {
					LastName.AdvancedSearch.SearchValue = sv;
					LastName.AdvancedSearch.SearchOperator = filter["z_LastName"];
					LastName.AdvancedSearch.SearchCondition = filter["v_LastName"];
					LastName.AdvancedSearch.SearchValue2 = filter["y_LastName"];
					LastName.AdvancedSearch.SearchOperator2 = filter["w_LastName"];
					LastName.AdvancedSearch.Save();
				}

				// Field BlobUrl
				if (filter.TryGetValue("x_BlobUrl", out sv)) {
					BlobUrl.AdvancedSearch.SearchValue = sv;
					BlobUrl.AdvancedSearch.SearchOperator = filter["z_BlobUrl"];
					BlobUrl.AdvancedSearch.SearchCondition = filter["v_BlobUrl"];
					BlobUrl.AdvancedSearch.SearchValue2 = filter["y_BlobUrl"];
					BlobUrl.AdvancedSearch.SearchOperator2 = filter["w_BlobUrl"];
					BlobUrl.AdvancedSearch.Save();
				}

				// Field EmailAddress
				if (filter.TryGetValue("x_EmailAddress", out sv)) {
					EmailAddress.AdvancedSearch.SearchValue = sv;
					EmailAddress.AdvancedSearch.SearchOperator = filter["z_EmailAddress"];
					EmailAddress.AdvancedSearch.SearchCondition = filter["v_EmailAddress"];
					EmailAddress.AdvancedSearch.SearchValue2 = filter["y_EmailAddress"];
					EmailAddress.AdvancedSearch.SearchOperator2 = filter["w_EmailAddress"];
					EmailAddress.AdvancedSearch.Save();
				}

				// Field PhoneNumber
				if (filter.TryGetValue("x_PhoneNumber", out sv)) {
					PhoneNumber.AdvancedSearch.SearchValue = sv;
					PhoneNumber.AdvancedSearch.SearchOperator = filter["z_PhoneNumber"];
					PhoneNumber.AdvancedSearch.SearchCondition = filter["v_PhoneNumber"];
					PhoneNumber.AdvancedSearch.SearchValue2 = filter["y_PhoneNumber"];
					PhoneNumber.AdvancedSearch.SearchOperator2 = filter["w_PhoneNumber"];
					PhoneNumber.AdvancedSearch.Save();
				}
				if (filter.TryGetValue(Config.TableBasicSearch, out string keyword))
					BasicSearch.SessionKeyword = keyword;
				if (filter.TryGetValue(Config.TableBasicSearchType, out string type))
					BasicSearch.SessionType = type;
				return true;
			}

			// Advanced search WHERE clause based on QueryString
			protected string AdvancedSearchWhere(bool def = false) {
				string where = "";
				BuildSearchSql(ref where, ApplicationId, def, false); // ApplicationId
				BuildSearchSql(ref where, _Name, def, false); // _Name
				BuildSearchSql(ref where, ApplicationstatusId, def, false); // ApplicationstatusId
				BuildSearchSql(ref where, BranchId, def, false); // BranchId
				BuildSearchSql(ref where, UserId, def, false); // UserId
				BuildSearchSql(ref where, State, def, false); // State
				BuildSearchSql(ref where, FirstName, def, false); // FirstName
				BuildSearchSql(ref where, LastName, def, false); // LastName
				BuildSearchSql(ref where, BlobUrl, def, false); // BlobUrl
				BuildSearchSql(ref where, EmailAddress, def, false); // EmailAddress
				BuildSearchSql(ref where, PhoneNumber, def, false); // PhoneNumber

				// Set up search parm
				if (!def && !Empty(where) && (new List<string> { "", "reset", "resetall" }).Contains(Command))
					Command = "search";
				if (!def && Command == "search") {
					ApplicationId.AdvancedSearch.Save(); // ApplicationId
					_Name.AdvancedSearch.Save(); // Name
					ApplicationstatusId.AdvancedSearch.Save(); // ApplicationstatusId
					BranchId.AdvancedSearch.Save(); // BranchId
					UserId.AdvancedSearch.Save(); // UserId
					State.AdvancedSearch.Save(); // State
					FirstName.AdvancedSearch.Save(); // FirstName
					LastName.AdvancedSearch.Save(); // LastName
					BlobUrl.AdvancedSearch.Save(); // BlobUrl
					EmailAddress.AdvancedSearch.Save(); // EmailAddress
					PhoneNumber.AdvancedSearch.Save(); // PhoneNumber
				}
				return where;
			}

			// Build search SQL
			public void BuildSearchSql(ref string where, DbField fld, bool def, bool multiValue) {
				string fldParm = fld.Param;
				string fldVal = def ? Convert.ToString(fld.AdvancedSearch.SearchValueDefault) : Convert.ToString(fld.AdvancedSearch.SearchValue);
				string fldOpr = def ? fld.AdvancedSearch.SearchOperatorDefault : fld.AdvancedSearch.SearchOperator;
				string fldCond = def ? fld.AdvancedSearch.SearchConditionDefault : fld.AdvancedSearch.SearchCondition;
				string fldVal2 = def ? Convert.ToString(fld.AdvancedSearch.SearchValue2Default) : Convert.ToString(fld.AdvancedSearch.SearchValue2);
				string fldOpr2 = def ? fld.AdvancedSearch.SearchOperator2Default : fld.AdvancedSearch.SearchOperator2;
				string wrk = "";
				fldOpr = fldOpr.Trim().ToUpper();
				if (Empty(fldOpr))
					fldOpr = "=";
				fldOpr2 = fldOpr2.Trim().ToUpper();
				if (Empty(fldOpr2))
					fldOpr2 = "=";
				if (Config.SearchMultiValueOption == 1)
					multiValue = false;
				if (multiValue) {
					string wrk1 = !Empty(fldVal) ? GetMultiSearchSql(fld, fldOpr, fldVal, DbId) : ""; // Field value 1
					string wrk2 = !Empty(fldVal2) ? GetMultiSearchSql(fld, fldOpr2, fldVal2, DbId) : ""; // Field value 2
					wrk = wrk1; // Build final SQL
					if (!Empty(wrk2))
						wrk = !Empty(wrk) ? "(" + wrk + ") " + fldCond + " (" + wrk2 + ")" : wrk2;
				} else {
					fldVal = ConvertSearchValue(fld, fldVal);
					fldVal2 = ConvertSearchValue(fld, fldVal2);
					wrk = GetSearchSql(fld, fldVal, fldOpr, fldCond, fldVal2, fldOpr2, DbId);
				}
				AddFilter(ref where, wrk);
			}

			// Convert search value
			protected string ConvertSearchValue(DbField fld, string fldVal) {
				if (fldVal == Config.NullValue || fldVal == Config.NotNullValue)
					return fldVal;
				string value = fldVal;
				if (fld.DataType == Config.DataTypeBoolean) {
				} else if (fld.DataType == Config.DataTypeDate || fld.DataType == Config.DataTypeTime) {
					if (!Empty(fldVal))
						value = UnformatDateTime(fldVal, fld.DateTimeFormat);
				}
				return value;
			}

			// Return basic search SQL
			protected string BasicSearchSql(List<string> keywords, string type) {
				string where = "";
				BuildBasicSearchSql(ref where, _Name, keywords, type);
				BuildBasicSearchSql(ref where, State, keywords, type);
				BuildBasicSearchSql(ref where, FirstName, keywords, type);
				BuildBasicSearchSql(ref where, LastName, keywords, type);
				BuildBasicSearchSql(ref where, BlobUrl, keywords, type);
				BuildBasicSearchSql(ref where, EmailAddress, keywords, type);
				BuildBasicSearchSql(ref where, PhoneNumber, keywords, type);
				return where;
			}

			// Build basic search SQL
			protected void BuildBasicSearchSql(ref string where, DbField fld, List<string> keywords, string type) {
				string defCond = (type == "OR") ? "OR" : "AND";
				var sqls = new List<string>(); // List for SQL parts
				var conds = new List<string>(); // List for search conditions
				int cnt = keywords.Count;
				int j = 0; // Number of SQL parts
				for (int i = 0; i < cnt; i++) {
					string keyword = keywords[i];
					keyword = keyword.Trim();
					string[] ar;
					if (!Empty(Config.BasicSearchIgnorePattern)) {
						keyword = Regex.Replace(keyword, Config.BasicSearchIgnorePattern, "\\");
						ar = keyword.Split('\\');
					} else {
						ar = new string[] { keyword };
					}
					foreach (var kw in ar) {
						if (!Empty(kw)) {
							string wrk = "";
							if (kw == "OR" && type == "") {
								if (j > 0)
									conds[j - 1] = "OR";
							} else if (kw == Config.NullValue) {
								wrk = fld.Expression + " IS NULL";
							} else if (kw == Config.NotNullValue) {
								wrk = fld.Expression + " IS NOT NULL";
							} else if (fld.IsVirtual) {
								wrk = fld.VirtualExpression + Like(QuotedValue("%" + kw + "%", Config.DataTypeString, DbId), DbId);
							} else if (fld.DataType != Config.DataTypeNumber || IsNumeric(kw)) {
								wrk = fld.BasicSearchExpression + Like(QuotedValue("%" + kw + "%", Config.DataTypeString, DbId), DbId);
							}
							if (!Empty(wrk)) {
								sqls.Add(wrk); // DN
								conds.Add(defCond); // DN
								j++;
							}
						}
					}
				}
				cnt = sqls.Count;
				bool quoted = false;
				string sql = "";
				if (cnt > 0) {
					for (int i = 0; i < cnt - 1; i++) {
						if (conds[i] == "OR") {
							if (!quoted)
								sql += "(";
							quoted = true;
						}
						sql += sqls[i];
						if (quoted && conds[i] != "OR") {
							sql += ")";
							quoted = false;
						}
						sql += " " + conds[i] + " ";
					}
					sql += sqls[cnt - 1];
					if (quoted)
						sql += ")";
				}
				if (!Empty(sql)) {
					if (!Empty(where))
						where += " OR ";
					where += "(" + sql + ")";
				}
			}

			// Return basic search WHERE clause based on search keyword and type
			protected string BasicSearchWhere(bool def = false) {
				string searchStr = "";
				string searchKeyword = def ? BasicSearch.KeywordDefault : BasicSearch.Keyword;
				string searchType = def ? BasicSearch.TypeDefault : BasicSearch.Type;

				// Get search SQL
				if (!Empty(searchKeyword)) {
					var ar = BasicSearch.KeywordList(def);
					if ((searchType == "OR" || searchType == "AND") && ConvertToBool(BasicSearch.BasicSearchAnyFields)) {
						foreach (var keyword in ar) {
							if (keyword != "") {
								if (searchStr != "")
									searchStr += " " + searchType + " ";
								searchStr += "(" + BasicSearchSql(new List<string> { keyword }, searchType) + ")";
							}
						}
					} else {
						searchStr = BasicSearchSql(ar, searchType);
					}
					if (!def && (new List<string> {"", "reset", "resetall"}).Contains(Command))
						Command = "search";
				}
				if (!def && Command == "search") {
					BasicSearch.SessionKeyword = searchKeyword;
					BasicSearch.SessionType = searchType;
				}
				return searchStr;
			}

			// Check if search parm exists
			protected bool CheckSearchParms() {

				// Check basic search
				if (BasicSearch.IssetSession)
					return true;
				if (ApplicationId.AdvancedSearch.IssetSession)
					return true;
				if (_Name.AdvancedSearch.IssetSession)
					return true;
				if (ApplicationstatusId.AdvancedSearch.IssetSession)
					return true;
				if (BranchId.AdvancedSearch.IssetSession)
					return true;
				if (UserId.AdvancedSearch.IssetSession)
					return true;
				if (State.AdvancedSearch.IssetSession)
					return true;
				if (FirstName.AdvancedSearch.IssetSession)
					return true;
				if (LastName.AdvancedSearch.IssetSession)
					return true;
				if (BlobUrl.AdvancedSearch.IssetSession)
					return true;
				if (EmailAddress.AdvancedSearch.IssetSession)
					return true;
				if (PhoneNumber.AdvancedSearch.IssetSession)
					return true;
				return false;
			}

			// Clear all search parameters
			protected void ResetSearchParms() {
				SearchWhere = "";
				SessionSearchWhere = SearchWhere;

				// Clear basic search parameters
				ResetBasicSearchParms();

				// Clear advanced search parameters
				ResetAdvancedSearchParms();
			}

			// Load advanced search default values
			protected bool LoadAdvancedSearchDefault() {
				return false;
			}

			// Clear all basic search parameters
			protected void ResetBasicSearchParms() {
				BasicSearch.UnsetSession();
			}

			// Clear all advanced search parameters
			protected void ResetAdvancedSearchParms() {
				ApplicationId.AdvancedSearch.UnsetSession();
				_Name.AdvancedSearch.UnsetSession();
				ApplicationstatusId.AdvancedSearch.UnsetSession();
				BranchId.AdvancedSearch.UnsetSession();
				UserId.AdvancedSearch.UnsetSession();
				State.AdvancedSearch.UnsetSession();
				FirstName.AdvancedSearch.UnsetSession();
				LastName.AdvancedSearch.UnsetSession();
				BlobUrl.AdvancedSearch.UnsetSession();
				EmailAddress.AdvancedSearch.UnsetSession();
				PhoneNumber.AdvancedSearch.UnsetSession();
			}

			// Restore all search parameters
			protected void RestoreSearchParms() {
				RestoreSearch = true;

				// Restore basic search values
				BasicSearch.Load();

				// Restore advanced search values
				ApplicationId.AdvancedSearch.Load();
				_Name.AdvancedSearch.Load();
				ApplicationstatusId.AdvancedSearch.Load();
				BranchId.AdvancedSearch.Load();
				UserId.AdvancedSearch.Load();
				State.AdvancedSearch.Load();
				FirstName.AdvancedSearch.Load();
				LastName.AdvancedSearch.Load();
				BlobUrl.AdvancedSearch.Load();
				EmailAddress.AdvancedSearch.Load();
				PhoneNumber.AdvancedSearch.Load();
			}

			// Set up sort parameters
			protected void SetupSortOrder() {

				// Check for "order" parameter
				if (Get("order", out StringValues sv)) {
					CurrentOrder = sv;
					CurrentOrderType = Get("ordertype");
					UpdateSort(_Name); // Name
					UpdateSort(ApplicationstatusId); // ApplicationstatusId
					UpdateSort(BranchId); // BranchId
					UpdateSort(UserId); // UserId
					UpdateSort(State); // State
					UpdateSort(FirstName); // FirstName
					UpdateSort(LastName); // LastName
					UpdateSort(EmailAddress); // EmailAddress
					UpdateSort(PhoneNumber); // PhoneNumber
					StartRecordNumber = 1; // Reset start position
				}
			}

			// Load sort order parameters
			protected void LoadSortOrder() {
				string orderBy = SessionOrderBy; // Get Order By from Session
				if (Empty(orderBy)) {
					if (!Empty(SqlOrderBy)) {
						orderBy = SqlOrderBy;
						SessionOrderBy = orderBy;
					}
				}
			}

			// Reset command
			// cmd=reset (Reset search parameters)
			// cmd=resetall (Reset search and master/detail parameters)
			// cmd=resetsort (Reset sort parameters)

			protected void ResetCommand() {

				// Get reset cmd
				if (Command.ToLower().StartsWith("reset")) {

					// Reset search criteria
					if (SameText(Command, "reset") || SameText(Command, "resetall"))
						ResetSearchParms();

					// Reset sorting order
					if (SameText(Command, "resetsort")) {
						string orderBy = "";
						SessionOrderBy = orderBy;
						_Name.Sort = "";
						ApplicationstatusId.Sort = "";
						BranchId.Sort = "";
						UserId.Sort = "";
						State.Sort = "";
						FirstName.Sort = "";
						LastName.Sort = "";
						EmailAddress.Sort = "";
						PhoneNumber.Sort = "";
					}

					// Reset start position
					StartRecord = 1;
					StartRecordNumber = StartRecord;
				}
			}
			#pragma warning disable 1998

			// Set up list options
			protected async Task SetupListOptions() {
				ListOption item;

				// Add group option item
				item = ListOptions.Add(ListOptions.GroupOptionName);
				item.Body = "";
				item.OnLeft = false;
				item.Visible = false;

				// "view"
				item = ListOptions.Add("view");
				item.CssClass = "text-nowrap";
				item.Visible = true;
				item.OnLeft = false;

				// "edit"
				item = ListOptions.Add("edit");
				item.CssClass = "text-nowrap";
				item.Visible = true;
				item.OnLeft = false;

				// List actions
				item = ListOptions.Add("listactions");
				item.CssClass = "text-nowrap";
				item.OnLeft = false;
				item.Visible = false;
				item.ShowInButtonGroup = false;
				item.ShowInDropDown = false;

				// "checkbox"
				item = ListOptions.Add("checkbox");
				item.CssStyle = "white-space: nowrap; text-align: center; vertical-align: middle; margin: 0px;";
				item.Visible = false;
				item.OnLeft = false;
				item.Header = "<div class=\"custom-control custom-checkbox d-inline-block\"><input type=\"checkbox\" name=\"key\" id=\"key\" class=\"custom-control-input\" onclick=\"ew.selectAllKey(this);\"><label class=\"custom-control-label\" for=\"key\"></label></div>";
				item.ShowInDropDown = false;
				item.ShowInButtonGroup = false;

				// Drop down button for ListOptions
				ListOptions.UseDropDownButton = false;
				ListOptions.DropDownButtonPhrase = Language.Phrase("ButtonListOptions");
				ListOptions.UseButtonGroup = false;
				if (ListOptions.UseButtonGroup && IsMobile())
					ListOptions.UseDropDownButton = true;
				ListOptions.ButtonClass = ""; // Class for button group

				// Call ListOptions_Load event
				ListOptions_Load();
				SetupListOptionsExt();
				item = ListOptions[ListOptions.GroupOptionName];
				item.Visible = ListOptions.GroupOptionVisible;
			}
			#pragma warning restore 1998

			// Render list options
			#pragma warning disable 168, 219, 1998

			public async Task RenderListOptions() {
				ListOption listOption;
				var isVisible = false; // DN
				ListOptions.LoadDefault();

				// Call ListOptions_Rendering event
				ListOptions_Rendering();

				// "view"
				listOption = ListOptions["view"];
				string viewcaption = HtmlTitle(Language.Phrase("ViewLink"));
				isVisible = true;
				if (isVisible) {
					listOption.Body = "<a class=\"ew-row-link ew-view\" title=\"" + viewcaption + "\" data-caption=\"" + viewcaption + "\" href=\"" + HtmlEncode(AppPath(ViewUrl)) + "\">" + Language.Phrase("ViewLink") + "</a>";
				} else {
					listOption.Body = "";
				}

				// "edit"
				listOption = ListOptions["edit"];
				string editcaption = HtmlTitle(Language.Phrase("EditLink"));
				isVisible = true;
				if (isVisible) {
					listOption.Body = "<a class=\"ew-row-link ew-edit\" title=\"" + editcaption + "\" data-caption=\"" + editcaption + "\" href=\"" + HtmlEncode(AppPath(EditUrl)) + "\">" + Language.Phrase("EditLink") + "</a>";
				} else {
					listOption.Body = "";
				}

				// Set up list action buttons
				listOption = ListOptions["listactions"];
				if (listOption != null && !IsExport() && CurrentAction == "") {
					string body = "";
					var links = new List<string>();
					foreach (var (key, act) in ListActions.Items) {
						if (act.Select == Config.ActionSingle && act.Allowed) {
							var action = act.Action;
							string caption = act.Caption;
							var icon = (act.Icon != "") ? "<i class=\"" + HtmlEncode(act.Icon.Replace(" ew-icon", "")) + "\" data-caption=\"" + HtmlTitle(caption) + "\"></i> " : "";
							links.Add("<li><a class=\"dropdown-item ew-action ew-list-action\" data-action=\"" + HtmlEncode(action) + "\" data-caption=\"" + HtmlTitle(caption) + "\" href=\"#\" onclick=\"return ew.submitAction(event,jQuery.extend({key:" + KeyToJson() + "}," + act.ToJson(true) + "));\">" + icon + act.Caption + "</a></li>");
							if (links.Count == 1) // Single button
								body = "<a class=\"ew-action ew-list-action\" data-action=\"" + HtmlEncode(action) + "\" title=\"" + HtmlTitle(caption) + "\" data-caption=\"" + HtmlTitle(caption) + "\" href=\"#\" onclick=\"return ew.submitAction(event,jQuery.extend({key:" + KeyToJson() + "}," + act.ToJson(true) + "));\">" + Language.Phrase("ListActionButton") + "</a>";
						}
					}
					if (links.Count > 1) { // More than one buttons, use dropdown
						body = "<button class=\"dropdown-toggle btn btn-default ew-actions\" title=\"" + HtmlTitle(Language.Phrase("ListActionButton")) + "\" data-toggle=\"dropdown\">" + Language.Phrase("ListActionButton") + "</button>";
						string content = links.Aggregate("", (result, link) => result + "<li>" + link + "</li>");
						body += "<ul class=\"dropdown-menu" + (listOption.OnLeft ? "" : " dropdown-menu-right") + "\">" + content + "</ul>";
						body = "<div class=\"btn-group btn-group-sm\">" + body + "</div>";
					}
					if (links.Count > 0) {
						listOption.Body = body;
						listOption.Visible = true;
					}
				}

				// "checkbox"
				listOption = ListOptions["checkbox"];
				listOption.Body = "<div class=\"custom-control custom-checkbox d-inline-block\"><input type=\"checkbox\" id=\"key_m_" + RowCount + "\" name=\"key_m[]\" class=\"custom-control-input ew-multi-select\" value=\"" + HtmlEncode(ApplicationId.CurrentValue) + "\" onclick=\"ew.clickMultiCheckbox(event);\"><label class=\"custom-control-label\" for=\"key_m_" + RowCount + "\"></label></div>";
				RenderListOptionsExt();

				// Call ListOptions_Rendered event
				ListOptions_Rendered();
			}

			// Set up other options
			protected void SetupOtherOptions() {
				ListOptions option;
				ListOption item;
				var options = OtherOptions;
				option = options["action"];

				// Set up options default
				foreach (var (key, opt) in options) {
					opt.UseDropDownButton = false;
					opt.UseButtonGroup = true;
					opt.ButtonClass = ""; // Class for button group
					item = opt.Add(opt.GroupOptionName);
					item.Body = "";
					item.Visible = false;
				}
				options["addedit"].DropDownButtonPhrase = Language.Phrase("ButtonAddEdit");
				options["detail"].DropDownButtonPhrase = Language.Phrase("ButtonDetails");
				options["action"].DropDownButtonPhrase = Language.Phrase("ButtonActions");

				// Filter button
				item = FilterOptions.Add("savecurrentfilter");
				item.Body = "<a class=\"ew-save-filter\" data-form=\"fApplicationlistsrch\" href=\"#\">" + Language.Phrase("SaveCurrentFilter") + "</a>";
				item.Visible = true;
				item = FilterOptions.Add("deletefilter");
				item.Body = "<a class=\"ew-delete-filter\" data-form=\"fApplicationlistsrch\" href=\"#\">" + Language.Phrase("DeleteFilter") + "</a>";
				item.Visible = true;
				FilterOptions.UseDropDownButton = true;
				FilterOptions.UseButtonGroup = !FilterOptions.UseDropDownButton;
				FilterOptions.DropDownButtonPhrase = Language.Phrase("Filters");

				// Add group option item
				item = FilterOptions.Add(FilterOptions.GroupOptionName);
				item.Body = "";
				item.Visible = false;
			}

			// Render other options
			public void RenderOtherOptions() {
				ListOptions option;
				ListOption item;
				var options = OtherOptions;
					option = options["action"];

					// Set up list action buttons
					foreach (var (key, act) in ListActions.Items.Where(kvp => kvp.Value.Select == Config.ActionMultiple)) {
						item = option.Add("custom_" + act.Action);
						string caption = act.Caption;
						var icon = (act.Icon != "") ? "<i class=\"" + HtmlEncode(act.Icon) + "\" data-caption=\"" + HtmlEncode(caption) + "\"></i> " + caption : caption;
						item.Body = "<a class=\"ew-action ew-list-action\" title=\"" + HtmlEncode(caption) + "\" data-caption=\"" + HtmlEncode(caption) + "\" href=\"#\" onclick=\"return ew.submitAction(event,jQuery.extend({f:document.fApplicationlist}," + act.ToJson(true) + "));\">" + icon + "</a>";
						item.Visible = act.Allowed;
					}

					// Hide grid edit and other options
					if (TotalRecords <= 0) {
						option = options["addedit"];
						item = option["gridedit"];
						if (item != null)
							item.Visible = false;
						option = options["action"];
						option.HideAllOptions();
					}
			}

			// Process list action
			public async Task<IActionResult> ProcessListAction() {
				string filter = GetFilterFromRecordKeys();
				string userAction = Post("useraction");
				if (filter != "" && userAction != "") {

					// Check permission first
					var actionCaption = userAction;
					foreach (var (key, act) in ListActions.Items) {
						if (SameString(key, userAction)) {
							actionCaption = act.Caption;
							if (!act.Allowed) {
								string errmsg = Language.Phrase("CustomActionNotAllowed").Replace("%s", actionCaption);
								if (Post("ajax") == userAction) // Ajax
									return Controller.Content("<p class=\"text-danger\">" + errmsg + "</p>", "text/plain", Encoding.UTF8);
								else
									FailureMessage = errmsg;
								return null;
							}
						}
					}
					CurrentFilter = filter;
					string sql = CurrentSql;
					var rsuser = await Connection.GetRowsAsync(sql);
					CurrentAction = userAction;

					// Call row custom action event
					if (rsuser != null) {
						Connection.BeginTrans();
						bool processed = true;
						SelectedCount = rsuser.Count();
						SelectedIndex = 0;
						foreach (var row in rsuser) {
							SelectedIndex++;
							processed = Row_CustomAction(userAction, row);
							if (!processed)
								break;
						}
						if (processed) {
							Connection.CommitTrans(); // Commit the changes
							if (Empty(SuccessMessage))
								SuccessMessage = Language.Phrase("CustomActionCompleted").Replace("%s", actionCaption); // Set up success message
							} else {
								Connection.RollbackTrans(); // Rollback changes

							// Set up error message
							if (!Empty(SuccessMessage) || !Empty(FailureMessage)) {

								// Use the message, do nothing
							} else if (!Empty(CancelMessage)) {
								FailureMessage = CancelMessage;
								CancelMessage = "";
							} else {
								FailureMessage = Language.Phrase("CustomActionFailed").Replace("%s", actionCaption);
							}
						}
					}
					CurrentAction = ""; // Clear action
					if (Post("ajax") == userAction) { // Ajax
						if (ActionResult != null) // Action result set by Row_CustomAction // DN
							return ActionResult;
						string msg = "";
						if (SuccessMessage != "") {
							msg = "<p class=\"text-success\">" + SuccessMessage + "</p>";
							ClearSuccessMessage(); // Clear message
						}
						if (FailureMessage != "") {
							msg = "<p class=\"text-danger\">" + FailureMessage + "</p>";
							ClearFailureMessage(); // Clear message
						}
						if (!Empty(msg))
							return Controller.Content(msg, "text/plain", Encoding.UTF8);
					}
				}
				return null; // Not ajax request
			}

			// Set up list options (extended codes)
			protected void SetupListOptionsExt() {}

			// Render list options (extended codes)
			protected void RenderListOptionsExt() {}

			// Load basic search values // DN
			protected void LoadBasicSearchValues() {
				if (Get(Config.TableBasicSearch, out StringValues keyword))
					BasicSearch.Keyword = keyword;
				if (!Empty(BasicSearch.Keyword) && Empty(Command))
					Command = "search";
				if (Get(Config.TableBasicSearchType, out StringValues type))
					BasicSearch.Type = type;
			}

			// Load search values for validation // DN
			protected void LoadSearchValues() {

				// ApplicationId
				if (!IsAddOrEdit)
					if (Query.ContainsKey("x_ApplicationId"))
						ApplicationId.AdvancedSearch.SearchValue = Get("x_ApplicationId");
					else
						ApplicationId.AdvancedSearch.SearchValue = Get("ApplicationId"); // Default Value // DN
				if (!Empty(ApplicationId.AdvancedSearch.SearchValue) && Command == "")
					Command = "search";
				if (Query.ContainsKey("z_ApplicationId"))
					ApplicationId.AdvancedSearch.SearchOperator = Get("z_ApplicationId");

				// _Name
				if (!IsAddOrEdit)
					if (Query.ContainsKey("x__Name"))
						_Name.AdvancedSearch.SearchValue = Get("x__Name");
					else
						_Name.AdvancedSearch.SearchValue = Get("_Name"); // Default Value // DN
				if (!Empty(_Name.AdvancedSearch.SearchValue) && Command == "")
					Command = "search";
				if (Query.ContainsKey("z__Name"))
					_Name.AdvancedSearch.SearchOperator = Get("z__Name");

				// ApplicationstatusId
				if (!IsAddOrEdit)
					if (Query.ContainsKey("x_ApplicationstatusId"))
						ApplicationstatusId.AdvancedSearch.SearchValue = Get("x_ApplicationstatusId");
					else
						ApplicationstatusId.AdvancedSearch.SearchValue = Get("ApplicationstatusId"); // Default Value // DN
				if (!Empty(ApplicationstatusId.AdvancedSearch.SearchValue) && Command == "")
					Command = "search";
				if (Query.ContainsKey("z_ApplicationstatusId"))
					ApplicationstatusId.AdvancedSearch.SearchOperator = Get("z_ApplicationstatusId");

				// BranchId
				if (!IsAddOrEdit)
					if (Query.ContainsKey("x_BranchId"))
						BranchId.AdvancedSearch.SearchValue = Get("x_BranchId");
					else
						BranchId.AdvancedSearch.SearchValue = Get("BranchId"); // Default Value // DN
				if (!Empty(BranchId.AdvancedSearch.SearchValue) && Command == "")
					Command = "search";
				if (Query.ContainsKey("z_BranchId"))
					BranchId.AdvancedSearch.SearchOperator = Get("z_BranchId");

				// UserId
				if (!IsAddOrEdit)
					if (Query.ContainsKey("x_UserId"))
						UserId.AdvancedSearch.SearchValue = Get("x_UserId");
					else
						UserId.AdvancedSearch.SearchValue = Get("UserId"); // Default Value // DN
				if (!Empty(UserId.AdvancedSearch.SearchValue) && Command == "")
					Command = "search";
				if (Query.ContainsKey("z_UserId"))
					UserId.AdvancedSearch.SearchOperator = Get("z_UserId");

				// State
				if (!IsAddOrEdit)
					if (Query.ContainsKey("x_State"))
						State.AdvancedSearch.SearchValue = Get("x_State");
					else
						State.AdvancedSearch.SearchValue = Get("State"); // Default Value // DN
				if (!Empty(State.AdvancedSearch.SearchValue) && Command == "")
					Command = "search";
				if (Query.ContainsKey("z_State"))
					State.AdvancedSearch.SearchOperator = Get("z_State");

				// FirstName
				if (!IsAddOrEdit)
					if (Query.ContainsKey("x_FirstName"))
						FirstName.AdvancedSearch.SearchValue = Get("x_FirstName");
					else
						FirstName.AdvancedSearch.SearchValue = Get("FirstName"); // Default Value // DN
				if (!Empty(FirstName.AdvancedSearch.SearchValue) && Command == "")
					Command = "search";
				if (Query.ContainsKey("z_FirstName"))
					FirstName.AdvancedSearch.SearchOperator = Get("z_FirstName");

				// LastName
				if (!IsAddOrEdit)
					if (Query.ContainsKey("x_LastName"))
						LastName.AdvancedSearch.SearchValue = Get("x_LastName");
					else
						LastName.AdvancedSearch.SearchValue = Get("LastName"); // Default Value // DN
				if (!Empty(LastName.AdvancedSearch.SearchValue) && Command == "")
					Command = "search";
				if (Query.ContainsKey("z_LastName"))
					LastName.AdvancedSearch.SearchOperator = Get("z_LastName");

				// BlobUrl
				if (!IsAddOrEdit)
					if (Query.ContainsKey("x_BlobUrl"))
						BlobUrl.AdvancedSearch.SearchValue = Get("x_BlobUrl");
					else
						BlobUrl.AdvancedSearch.SearchValue = Get("BlobUrl"); // Default Value // DN
				if (!Empty(BlobUrl.AdvancedSearch.SearchValue) && Command == "")
					Command = "search";
				if (Query.ContainsKey("z_BlobUrl"))
					BlobUrl.AdvancedSearch.SearchOperator = Get("z_BlobUrl");

				// EmailAddress
				if (!IsAddOrEdit)
					if (Query.ContainsKey("x_EmailAddress"))
						EmailAddress.AdvancedSearch.SearchValue = Get("x_EmailAddress");
					else
						EmailAddress.AdvancedSearch.SearchValue = Get("EmailAddress"); // Default Value // DN
				if (!Empty(EmailAddress.AdvancedSearch.SearchValue) && Command == "")
					Command = "search";
				if (Query.ContainsKey("z_EmailAddress"))
					EmailAddress.AdvancedSearch.SearchOperator = Get("z_EmailAddress");

				// PhoneNumber
				if (!IsAddOrEdit)
					if (Query.ContainsKey("x_PhoneNumber"))
						PhoneNumber.AdvancedSearch.SearchValue = Get("x_PhoneNumber");
					else
						PhoneNumber.AdvancedSearch.SearchValue = Get("PhoneNumber"); // Default Value // DN
				if (!Empty(PhoneNumber.AdvancedSearch.SearchValue) && Command == "")
					Command = "search";
				if (Query.ContainsKey("z_PhoneNumber"))
					PhoneNumber.AdvancedSearch.SearchOperator = Get("z_PhoneNumber");
			}

			// Load recordset // DN
			public async Task<DbDataReader> LoadRecordset(int offset = -1, int rowcnt = -1) {

				// Load list page SQL
				string sql = ListSql;

				// Load recordset (Recordset_Selected event not supported) // DN
				return await Connection.SelectLimit(sql, rowcnt, offset, !Empty(OrderBy) || !Empty(SessionOrderBy));
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
				ApplicationId.SetDbValue(row["ApplicationId"]);
				_Name.SetDbValue(row["Name"]);
				ApplicationstatusId.SetDbValue(row["ApplicationstatusId"]);
				BranchId.SetDbValue(row["BranchId"]);
				UserId.SetDbValue(row["UserId"]);
				State.SetDbValue(row["State"]);
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
				row.Add("ApplicationId", System.DBNull.Value);
				row.Add("Name", System.DBNull.Value);
				row.Add("ApplicationstatusId", System.DBNull.Value);
				row.Add("BranchId", System.DBNull.Value);
				row.Add("UserId", System.DBNull.Value);
				row.Add("State", System.DBNull.Value);
				row.Add("FirstName", System.DBNull.Value);
				row.Add("LastName", System.DBNull.Value);
				row.Add("BlobUrl", System.DBNull.Value);
				row.Add("EmailAddress", System.DBNull.Value);
				row.Add("PhoneNumber", System.DBNull.Value);
				return row;
			}
			#pragma warning disable 618, 1998

			// Load old record
			protected async Task<bool> LoadOldRecord(DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> cnn = null) {
				bool validKey = true;
				if (!Empty(GetKey("ApplicationId")))
					ApplicationId.OldValue = GetKey("ApplicationId"); // ApplicationId
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
				// ApplicationId
				// Name
				// ApplicationstatusId
				// BranchId
				// UserId
				// State
				// FirstName
				// LastName
				// BlobUrl
				// EmailAddress
				// PhoneNumber

				if (RowType == Config.RowTypeView) { // View row

					// ApplicationId
					ApplicationId.ViewValue = ApplicationId.CurrentValue;
					ApplicationId.ViewCustomAttributes = "";

					// Name
					_Name.ViewValue = Convert.ToString(_Name.CurrentValue); // DN
					_Name.ViewCustomAttributes = "";

					// ApplicationstatusId
					curVal = Convert.ToString(ApplicationstatusId.CurrentValue);
					if (!Empty(curVal)) {
						ApplicationstatusId.ViewValue = ApplicationstatusId.LookupCacheOption(curVal);
						if (ApplicationstatusId.ViewValue == null) { // Lookup from database
							filterWrk = "[Id]" + SearchString("=", curVal.Trim(), Config.DataTypeNumber, "");
							sqlWrk = ApplicationstatusId.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(listwrk[1]);
								ApplicationstatusId.ViewValue = ApplicationstatusId.DisplayValue(listwrk);
							} else {
								ApplicationstatusId.ViewValue = ApplicationstatusId.CurrentValue;
							}
						}
					} else {
						ApplicationstatusId.ViewValue = System.DBNull.Value;
					}
					ApplicationstatusId.ViewCustomAttributes = "";

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

					// State
					State.ViewValue = Convert.ToString(State.CurrentValue); // DN
					State.ViewCustomAttributes = "";

					// FirstName
					FirstName.ViewValue = Convert.ToString(FirstName.CurrentValue); // DN
					FirstName.ViewCustomAttributes = "";

					// LastName
					LastName.ViewValue = Convert.ToString(LastName.CurrentValue); // DN
					LastName.ViewCustomAttributes = "";

					// EmailAddress
					EmailAddress.ViewValue = Convert.ToString(EmailAddress.CurrentValue); // DN
					EmailAddress.ViewCustomAttributes = "";

					// PhoneNumber
					PhoneNumber.ViewValue = Convert.ToString(PhoneNumber.CurrentValue); // DN
					PhoneNumber.ViewCustomAttributes = "";

					// Name
					_Name.HrefValue = "";
					_Name.TooltipValue = "";

					// ApplicationstatusId
					ApplicationstatusId.HrefValue = "";
					ApplicationstatusId.TooltipValue = "";

					// BranchId
					BranchId.HrefValue = "";
					BranchId.TooltipValue = "";

					// UserId
					UserId.HrefValue = "";
					UserId.TooltipValue = "";

					// State
					State.HrefValue = "";
					State.TooltipValue = "";

					// FirstName
					FirstName.HrefValue = "";
					FirstName.TooltipValue = "";

					// LastName
					LastName.HrefValue = "";
					LastName.TooltipValue = "";

					// EmailAddress
					EmailAddress.HrefValue = "";
					EmailAddress.TooltipValue = "";

					// PhoneNumber
					PhoneNumber.HrefValue = "";
					PhoneNumber.TooltipValue = "";
				}

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
				ApplicationId.AdvancedSearch.Load();
				_Name.AdvancedSearch.Load();
				ApplicationstatusId.AdvancedSearch.Load();
				BranchId.AdvancedSearch.Load();
				UserId.AdvancedSearch.Load();
				State.AdvancedSearch.Load();
				FirstName.AdvancedSearch.Load();
				LastName.AdvancedSearch.Load();
				BlobUrl.AdvancedSearch.Load();
				EmailAddress.AdvancedSearch.Load();
				PhoneNumber.AdvancedSearch.Load();
			}

			// Set up search options
			protected void SetupSearchOptions() {
				ListOption item;
				SearchOptions = new ListOptions();
				SearchOptions.Tag = "div";
				SearchOptions.TagClassName = "ew-search-option";

				// Search button
				item = SearchOptions.Add("searchtoggle");
				var searchToggleClass = !Empty(SearchWhere) ? " active" : " active";
				item.Body = "<button type=\"button\" class=\"btn btn-default ew-search-toggle" + searchToggleClass + "\" title=\"" + Language.Phrase("SearchPanel") + "\" data-caption=\"" + Language.Phrase("SearchPanel") + "\" data-toggle=\"button\" data-form=\"fApplicationlistsrch\">" + Language.Phrase("SearchLink") + "</button>";
				item.Visible = true;

				// Show all button
				item = SearchOptions.Add("showall");
				item.Body = "<a class=\"btn btn-default ew-show-all\" title=\"" + Language.Phrase("ShowAll") + "\" data-caption=\"" + Language.Phrase("ShowAll") + "\" href=\"" + AppPath(PageUrl) + "cmd=reset\">" + Language.Phrase("ShowAllBtn") + "</a>";
				item.Visible = (SearchWhere != DefaultSearchWhere && SearchWhere != "0=101");

				// Advanced search button
				item = SearchOptions.Add("advancedsearch");
				item.Body = "<a class=\"btn btn-default ew-advanced-search\" title=\"" + Language.Phrase("AdvancedSearch") + "\" data-caption=\"" + Language.Phrase("AdvancedSearch") + "\" href=\"" + AppPath("Applicationsrch") + "\">" + Language.Phrase("AdvancedSearchBtn") + "</a>";
				item.Visible = true;

				// Button group for search
				SearchOptions.UseDropDownButton = false;
				SearchOptions.UseButtonGroup = true;
				SearchOptions.DropDownButtonPhrase = Language.Phrase("ButtonSearch");

				// Add group option item
				item = SearchOptions.Add(SearchOptions.GroupOptionName);
				item.Body = "";
				item.Visible = false;

				// Hide search options
				if (IsExport() || !Empty(CurrentAction))
					SearchOptions.HideAllOptions();
			}

			// Set up Breadcrumb
			protected void SetupBreadcrumb() {
				var breadcrumb = new Breadcrumb();
				string url = CurrentUrl();
				url = Regex.Replace(url, @"\?cmd=reset(all)?$", ""); // Remove cmd=reset / cmd=resetall
				breadcrumb.Add("list", TableVar, url, "", TableVar, true);
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
									case "x_ApplicationstatusId":
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

			// Set up starting record parameters
			public void SetupStartRecord() {
				int pageNo;

				// Exit if DisplayRecords = 0
				if (DisplayRecords == 0)
					return;
				if (IsPageRequest) { // Validate request
					if (IsNumeric(Get(Config.TablePageNumber))) { // Check for "pageno" parameter first
						pageNo = Get<int>(Config.TablePageNumber);
						StartRecord = (pageNo - 1) * DisplayRecords + 1;
						if (StartRecord <= 0) {
							StartRecord = 1;
						} else if (StartRecord >= ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1) {
							StartRecord = ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1;
						}
						StartRecordNumber = StartRecord;
					} else if (IsNumeric(Get(Config.TableStartRec))) { // Check for a "start" parameter
						StartRecord = Get<int>(Config.TableStartRec);
						StartRecordNumber = StartRecord;
					}
				}
				StartRecord = StartRecordNumber;

				// Check if correct start record counter
				if (StartRecord <= 0) { // Avoid invalid start record counter
					StartRecord = 1; // Reset start record counter
					StartRecordNumber = StartRecord;
				} else if (StartRecord > TotalRecords) { // Avoid starting record > total records
					StartRecord = ((TotalRecords - 1) / DisplayRecords) * DisplayRecords + 1; // Point to last page first record
					StartRecordNumber = StartRecord;
				} else if ((StartRecord - 1) % DisplayRecords != 0) {
					StartRecord = ((StartRecord - 1) / DisplayRecords) * DisplayRecords + 1; // Point to page boundary
					StartRecordNumber = StartRecord;
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

			// ListOptions Load event
			public virtual void ListOptions_Load() {

				// Example:
				//var opt = ListOptions.Add("new");
				//opt.Header = "xxx";
				//opt.OnLeft = true; // Link on left
				//opt.MoveTo(0); // Move to first column

			}

			// ListOptions Rendering event
			public virtual void ListOptions_Rendering() {

				//xxxGrid.DetailAdd = (...condition...); // Set to true or false conditionally
				//xxxGrid.DetailEdit = (...condition...); // Set to true or false conditionally
				//xxxGrid.DetailView = (...condition...); // Set to true or false conditionally

			}

			// ListOptions Rendered event
			public virtual void ListOptions_Rendered() {

				//Example:
				//ListOptions["new"].Body = "xxx";

			}

			// Row Custom Action event
			public virtual bool Row_CustomAction(string action, Dictionary<string, object> row) {

				// Return false to abort
				return true;
			}

			// Page Exporting event
			// ExportDoc = export document object
			public virtual bool Page_Exporting() {

				//ExportDoc.Text.Append("<p>" + "my header" + "</p>"); // Export header
				//return false; // Return false to skip default export and use Row_Export event

				return true; // Return true to use default export and skip Row_Export event
			}

			// Row Export event
			// ExportDoc = export document object
			public virtual void Row_Export(DbDataReader rs) {

				//ExportDoc.Text.Append("<div>" + MyField.ViewValue +"</div>"); // Build HTML with field value: rs["MyField"] or MyField.ViewValue
			}

			// Page Exported event
			// ExportDoc = export document object
			public virtual void Page_Exported() {

				//ExportDoc.Text.Append("my footer"); // Export footer
				//Log("Text: {Text}", ExportDoc.Text);

			}

			// Grid Inserting event
			public virtual bool Grid_Inserting() {

				// Enter your code here
				// To reject grid insert, set return value to false

				return true;
			}

			// Grid Inserted event
			public virtual void Grid_Inserted(List<Dictionary<string, object>> rsnew) {

				//Log("Grid Inserted");
			}

			// Grid Updating event
			public virtual bool Grid_Updating(List<Dictionary<string, object>> rsold) {

				// Enter your code here
				// To reject grid update, set return value to false

				return true;
			}

			// Grid Updated event
			public virtual void Grid_Updated(List<Dictionary<string, object>> rsold, List<Dictionary<string, object>> rsnew) {

				//Log("Grid Updated");
			}
		} // End page class
	} // End Partial class
} // End namespace