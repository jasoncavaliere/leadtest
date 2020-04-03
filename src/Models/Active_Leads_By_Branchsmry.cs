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
		/// Active_Leads_By_Branch_Summary
		/// </summary>
		public static _Active_Leads_By_Branch_Summary Active_Leads_By_Branch_Summary {
			get => HttpData.Get<_Active_Leads_By_Branch_Summary>("Active_Leads_By_Branch_Summary");
			set => HttpData["Active_Leads_By_Branch_Summary"] = value;
		}

		/// <summary>
		/// Page class for Active Leads By Branch
		/// </summary>
		public class _Active_Leads_By_Branch_Summary : _Active_Leads_By_Branch_SummaryBase
		{

			// Construtor
			public _Active_Leads_By_Branch_Summary(Controller controller = null) : base(controller) {
			}
		}

		/// <summary>
		/// Page base class
		/// </summary>
		public class _Active_Leads_By_Branch_SummaryBase : _Active_Leads_By_Branch, IAspNetMakerPage
		{

			// Page ID
			public string PageID = "summary";

			// Project ID
			public string ProjectID = "{DE72B0A5-4A34-400E-B744-FF3F81D69E8F}";

			// Table name
			public string TableName { get; set; } = "Active Leads By Branch";

			// Page object name
			public string PageObjName = "Active_Leads_By_Branch_Summary";

			// Page headings
			public string Heading = "";

			public string Subheading = "";

			public string PageHeader = "";

			public string PageFooter = "";

			// CSS
			public string ReportTableClass = "";

			public string ReportTableStyle = null; // DN

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
				var result = ExportView().GetAwaiter().GetResult();
				if (result != null)
					return result;
				else
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
			public _Active_Leads_By_Branch_SummaryBase(Controller controller = null) { // DN
				if (controller != null)
					Controller = controller;

				// Initialize
				if (!DashboardReport)
					CurrentPage = this;

				// Language object
				Language ??= new Lang();

				// Table object (Active_Leads_By_Branch)
				if (Active_Leads_By_Branch == null || Active_Leads_By_Branch is _Active_Leads_By_Branch)
					Active_Leads_By_Branch = this;

				// Initialize URLs
				ExportPrintUrl = PageUrl + "export=print";
				ExportExcelUrl = PageUrl + "export=excel";
				ExportWordUrl = PageUrl + "export=word";
				ExportPdfUrl = PageUrl + "export=pdf";

				// Start time
				StartTime = Environment.TickCount;

				// Debug message
				LoadDebugMessage();

				// Open connection
				Conn = Connection; // DN

				// Export options
				ExportOptions = new ListOptions { Tag = "div", TagClassName = "ew-export-option" };

				// Filter options
				FilterOptions = new ListOptions { Tag = "div", TagClassName = "ew-filter-option fsummary" };
			}
			#pragma warning disable 1998

			// Export view result
			public async Task<IActionResult> ExportView() { // DN
				if (IsExport() && !IsExport("print") && Config.ExportReport.TryGetValue(Export, out string methodname)) {
					string content = await GetViewOutput();
					if (!Empty(content)) { // DN
						var parser = new AngleSharp.Html.Parser.HtmlParser();
						var doc = await parser.ParseDocumentAsync(content, default(CancellationToken));

						// Remove main custom template
						doc.QuerySelector("script[id^=tmp_]")?.Remove();

						// Remove JavaScript script tags
						foreach (var element in doc.QuerySelectorAll("script:not([type])"))
							element.Remove();

						// Handle custom templates
						foreach (var element in doc.QuerySelectorAll("script[type='text/html']")) {
							if (element.Parent is AngleSharp.Dom.IElement) {
								var parent = (AngleSharp.Dom.IElement)element.Parent;
								var nodes = parser.ParseFragment(element.InnerHtml, parent);
								element.Remove();
								parent.Append(nodes.ToArray());
							}
						}

						// Remove empty div tags
						var elements = doc.QuerySelectorAll("div").Where(el => EmptyString(el.InnerHtml));
						while (elements.Any()) {
							foreach (var element in elements)
								element.Remove();
							elements = doc.QuerySelectorAll("div").Where(el => EmptyString(el.InnerHtml));
						}

						// Remove all <div data-tagid="..." id="orig..." class="hide">...</div> (for customviewtag export, except "googlemaps")
						elements = doc.QuerySelectorAll("div.hide[data-tagid][id^=orig]");
						foreach (var element in elements) {
							if (element.GetAttribute("data-tagid") != "googlemaps")
								element.Remove();
						}

						// Use single CSS class for PDF
						if (IsExport("pdf")) {
							foreach (var element in doc.QuerySelectorAll(".ew-table"))
								element.SetAttribute("class", "ew-table");
						}

						// Set the content
						content = $@"<!DOCTYPE html><html><head><meta http-equiv=""Content-Type"" content=""text/html; charset={Config.Charset}""/>
							<style type=""text/css"">{ExportStyles}</style></head><body>{doc.Body.InnerHtml.Trim()}</body></html>";

						// Set the export file name
						if (Empty(ExportFileName))
							ExportFileName = TableVar;

						// Export
						return (IActionResult)Invoke(methodname, new object[] { content });
					}
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

				// Close connection if not in dashboard
				if (!DashboardReport)
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
				if ((new List<string> { ReportSourceTable, TableVar }).Contains(lookup.LinkTable))
					lookup.RenderViewFunc = "RenderLookup"; // Set up view renderer
				lookup.RenderEditFunc = ""; // Set up edit renderer
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
			private Pager _pager; // DN

			public int GroupIndex = 0;

			public int RowIndex = 0;

			public bool HideOptions = false;

			public ListOptions ExportOptions; // Export options

			public ListOptions SearchOptions; // Search options

			public ListOptions FilterOptions; // Filter options

			// Records
			public int DetailRecordCount = 0;

			public List<Dictionary<string, object>> DetailRecords = new List<Dictionary<string, object>>(); // DN

			public List<Dictionary<string, object>> GroupRecords = new List<Dictionary<string, object>>(); // DN

			// Paging variables
			public int RecordIndex = 0; // Record index

			public int RecordCount = 0; // Record count

			public int StartGroup = 0; // Start group

			public int StopGroup = 0; // Stop group

			public int TotalGroups = 0; // Total groups

			public int GroupCount = 0; // Group count

			public Dictionary<int, int> GroupCounter = new Dictionary<int, int>(); // Group counter

			public int DisplayGroups = 3; // Groups per page

			public int GroupRange = 10;

			public string PageSizes = "1,2,3,5,-1"; // Group sizes (comma separated)

			public string Sort = "";

			public string Filter = "";

			public string PageFirstGroupFilter = "";

			public string UserIDFilter = "";

			public string DefaultSearchWhere = ""; // Default search WHERE clause

			public string SearchWhere = "";

			public string SearchPanelClass = "ew-search-panel collapse show"; // Search Panel class

			public int SearchRowCount = 0; // For extended search

			public int SearchColumnCount = 0; // For extended search

			public int SearchFieldsPerRow = 1; // For extended search

			public string DrillDownList = "";

			public string DbMasterFilter = ""; // Master filter

			public string DbDetailFilter = ""; // Detail filter

			public bool SearchCommand = false;

			public bool ShowHeader;

			public int GroupColumnCount = 0;

			public int SubGroupColumnCount = 0;

			public int DetailColumnCount = 0;

			public int TotalCount;

			public int PageTotalCount;

			public string TopContentClass = "col-sm-12 ew-top";

			public string LeftContentClass = "ew-left";

			public string CenterContentClass = "col-sm-12 ew-center";

			public string RightContentClass = "ew-right";

			public string BottomContentClass = "col-sm-12 ew-bottom";

			// Pager
			public Pager Pager {
				get {
					_pager ??= new PrevNextPager(StartGroup, DisplayGroups, TotalGroups, PageSizes, GroupRange, AutoHidePager, AutoHidePageSizeSelector, !DashboardReport);
					return _pager;
				}
			}
			#pragma warning disable 168, 219

			/// <summary>
			/// Page run
			/// </summary>
			/// <returns>Page result</returns>
			public async Task<IActionResult> Run() { // DN

				// Header
				Header(Config.Cache);

				// User profile
				Profile = new UserProfile();

				// Security
				if (!await SetupApiRequest()) {
					Security ??= CreateSecurity(); // DN
				}
				CurrentAction = Param("action"); // Set up current action

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

				// Set up table class
				if (IsExport("word") || IsExport("excel") || IsExport("pdf"))
					ReportTableClass = "ew-table";
				else
					ReportTableClass = "table ew-table ew-report-table";
				FirstGroupField = BranchId; // DN

				// Set field visibility for detail fields
				LeadId.SetVisibility();
				_Name.SetVisibility();

				// Set up groups per page dynamically
				SetupDisplayGroups();

				// Set up Breadcrumb
				if (!IsExport())
					SetupBreadcrumb();

				// Load custom filters
				Page_FilterLoad();

				// Extended filter
				string extendedFilter = "";

				// No filter
				FilterOptions["savecurrentfilter"].Visible = false;
				FilterOptions["deletefilter"].Visible = false;

				// Call Page Selecting event
				Page_Selecting(ref SearchWhere);

				// Search options
				SetupSearchOptions();

				// Set up search panel class
				if (!Empty(SearchWhere))
					SearchPanelClass = AppendClass(SearchPanelClass, "show");

				// Get sort
				Sort = GetSort();

				// Update filter
				AddFilter(ref Filter, SearchWhere);

				// Get total group count
				string sql = BuildReportSql(SqlSelectGroup, SqlWhere, SqlGroupBy, SqlHaving, "", Filter, "");
				TotalGroups = await TryGetRecordCount(sql); // DN
				if (DisplayGroups <= 0 || DrillDown || DashboardReport) // Display all groups
					DisplayGroups = TotalGroups;
				StartGroup = 1;

				// Show header
				ShowHeader = (TotalGroups > 0);

				// Set up start position if not export all
				if (ExportAll && IsExport())
					DisplayGroups = TotalGroups;
				else
					SetupStartGroup();

				// Set no record found message
				if (TotalGroups == 0) {
						if (SearchWhere == "0=101") {
							WarningMessage = Language.Phrase("EnterSearchCriteria");
						} else {
							WarningMessage = Language.Phrase("NoRecord");
						}
				}

				// Hide all options if export/dashboard//hide options
				if (IsExport() || DashboardReport || HideOptions)
					ExportOptions.HideAllOptions();

				// Hide search/filter options if export/drilldown/dashboard/hide options
				if (IsExport() || DrillDown || DashboardReport || HideOptions) {
					SearchOptions.HideAllOptions();
					FilterOptions.HideAllOptions();
				}

				// Get group records
				if (TotalGroups > 0) {
					string groupSort = UpdateSortFields(SqlOrderByGroup, Sort, 2); // Get grouping field only // DN
					sql = BuildReportSql(SqlSelectGroup, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderByGroup, Filter, groupSort);
					GroupRecords = await GetRecords(sql, StartGroup, DisplayGroups); // DN
					LoadGroupRowValues();
					GroupCount = 1;
				}

				// Setup field count
				SetupFieldCount();

				// Set the last group to display if not export all
				if (ExportAll && IsExport()) {
					StopGroup = TotalGroups;
				} else {
					StopGroup = StartGroup + DisplayGroups - 1;
				}

				// Stop group <= total number of groups
				if (StopGroup > TotalGroups)
					StopGroup = TotalGroups;
				RecordCount = 0;
				RecordIndex = 0;

				// Result
				return PageResult();
			}
			#pragma warning restore 168, 219

			// Get records // DN
			public async Task<List<Dictionary<string, object>>> GetRecords(string sql, int start = 1, int grps = int.MaxValue) {
				List<Dictionary<string, object>> records = await Connection.GetRowsAsync(sql);
				int idx = start - 1;
				return records?.GetRange(idx, Math.Min(grps, records.Count - idx));
			}

			// Load group row values
			public void LoadGroupRowValues() {
				int cnt = GroupRecords.Count;
				if (GroupCount < cnt)
					FirstGroupField.GroupValue = GroupRecords[GroupCount].FirstOrDefault().Value;
				else
					FirstGroupField.GroupValue = "";
			}

			// Load row values
			public void LoadRowValues(Dictionary<string, object> record) {
				if (RecordIndex == 1) { // Load first row data
					Dictionary<string, object> data = new Dictionary<string, object>();
					data["LeadId"] = record["LeadId"];
					data["_Name"] = record["Name"];
					data["State"] = record["State"];
					data["LeadStatusId"] = record["LeadStatusId"];
					data["BranchId"] = record["BranchId"];
					data["UserId"] = record["UserId"];
					data["FirstName"] = record["FirstName"];
					data["LastName"] = record["LastName"];
					data["BlobUrl"] = record["BlobUrl"];
					data["EmailAddress"] = record["EmailAddress"];
					data["PhoneNumber"] = record["PhoneNumber"];
					Rows.Add(data);
				}
					LeadId.SetDbValue(record["LeadId"]);
					_Name.SetDbValue(record["Name"]);
					State.SetDbValue(record["State"]);
					LeadStatusId.SetDbValue(record["LeadStatusId"]);
					BranchId.SetDbValue(GroupValue(BranchId, record["BranchId"]));
					UserId.SetDbValue(record["UserId"]);
					FirstName.SetDbValue(record["FirstName"]);
					LastName.SetDbValue(record["LastName"]);
					BlobUrl.SetDbValue(record["BlobUrl"]);
					EmailAddress.SetDbValue(record["EmailAddress"]);
					PhoneNumber.SetDbValue(record["PhoneNumber"]);
			}
			#pragma warning disable 168

			// Render row
			public async Task RenderRow() {
				if (RowType == Config.RowTypeTotal && RowTotalSubType == Config.RowTotalFooter && RowTotalType == Config.RowTotalPage) { // Get Page total
					List<Dictionary<string, object>> records = null;

					// Build detail SQL
					FirstGroupField.GetDistinctValues(GroupRecords);
					string where = DetailFilterSql(FirstGroupField, SqlFirstGroupField, FirstGroupField.DistinctValues, DbId);
					if (!Empty(Filter))
						AddFilter(ref where, Filter);
					string sql = BuildReportSql(SqlSelect, SqlWhere, SqlGroupBy, SqlHaving, SqlOrderBy, where, Sort);
					records = await GetRecords(sql);
					PageTotalCount = records?.Count ?? 0;
				} else if (RowType == Config.RowTypeTotal && RowTotalSubType == Config.RowTotalFooter && RowTotalType == Config.RowTotalGrand) { // Get Grand total
					bool hasCount = false;
					bool hasSummary = false;

					// Get total count from SQL directly
					string sql = BuildReportSql(SqlSelectCount, SqlWhere, SqlGroupBy, SqlHaving, "", Filter, "");
					var total = await Connection.ExecuteScalarAsync(sql); // DN
					if (total != null) { // DN
						TotalCount = ConvertToInt(total);
						hasCount = true;
					} else {
						TotalCount = 0;
					}
					hasSummary = true;

					// Accumulate grand summary from detail records
					if (!hasCount || !hasSummary) {
						sql = BuildReportSql(SqlSelect, SqlWhere, SqlGroupBy, SqlHaving, "", Filter, "");
						DetailRecords = await GetRecords(sql);
					}
				}

				// Call Row_Rendering event
				Row_Rendering();

				// BranchId
				// LeadStatusId
				// LeadId
				// Name

				if (RowType == Config.RowTypeSearch) { // Search row
				} else if (RowType == Config.RowTypeTotal && !(RowTotalType == Config.RowTotalGroup && RowTotalSubType == Config.RowTotalHeader)) { // Summary row
					PrependClass(RowAttrs["class"], (RowTotalType == Config.RowTotalPage || RowTotalType == Config.RowTotalGrand) ? "ew-rpt-grp-aggregate" : ""); // Set up row class
					if (RowTotalType == Config.RowTotalGroup)
						RowAttrs["data-group"] = Convert.ToString(BranchId.GroupValue); // Set up group attribute
					if (RowTotalType == Config.RowTotalGroup && RowGroupLevel >= 2)
						RowAttrs["data-group-2"] = Convert.ToString(LeadStatusId.GroupValue); // Set up group attribute 2

					// BranchId
					curVal = Convert.ToString(BranchId.GroupValue);
					if (!Empty(curVal)) {
						BranchId.GroupViewValue = BranchId.LookupCacheOption(curVal);
						if (BranchId.GroupViewValue == null) { // Lookup from database
							filterWrk = "[Id]" + SearchString("=", curVal.Trim(), Config.DataTypeNumber, "");
							sqlWrk = BranchId.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(listwrk[1]);
								BranchId.GroupViewValue = BranchId.DisplayValue(listwrk);
							} else {
								BranchId.GroupViewValue = BranchId.GroupValue;
							}
						}
					} else {
						BranchId.GroupViewValue = System.DBNull.Value;
					}
					BranchId.CellCssClass = (RowGroupLevel == 1 ? "ew-rpt-grp-summary-1" : "ew-rpt-grp-field-1");
					BranchId.ViewCustomAttributes = "";
					BranchId.GroupViewValue = DisplayGroupValue(BranchId, BranchId.GroupViewValue);

					// LeadStatusId
					curVal = Convert.ToString(LeadStatusId.GroupValue);
					if (!Empty(curVal)) {
						LeadStatusId.GroupViewValue = LeadStatusId.LookupCacheOption(curVal);
						if (LeadStatusId.GroupViewValue == null) { // Lookup from database
							filterWrk = "[Id]" + SearchString("=", curVal.Trim(), Config.DataTypeNumber, "");
							sqlWrk = LeadStatusId.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(listwrk[1]);
								LeadStatusId.GroupViewValue = LeadStatusId.DisplayValue(listwrk);
							} else {
								LeadStatusId.GroupViewValue = LeadStatusId.GroupValue;
							}
						}
					} else {
						LeadStatusId.GroupViewValue = System.DBNull.Value;
					}
					LeadStatusId.CellCssClass = (RowGroupLevel == 2 ? "ew-rpt-grp-summary-2" : "ew-rpt-grp-field-2");
					LeadStatusId.ViewCustomAttributes = "";
					LeadStatusId.GroupViewValue = DisplayGroupValue(LeadStatusId, LeadStatusId.GroupViewValue);

					// BranchId
					BranchId.HrefValue = "";

					// LeadStatusId
					LeadStatusId.HrefValue = "";

					// LeadId
					LeadId.HrefValue = "";

					// Name
					_Name.HrefValue = "";
				} else {
					if (RowTotalType == Config.RowTotalGroup && RowTotalSubType == Config.RowTotalHeader) {
						RowAttrs["data-group"] = Convert.ToString(BranchId.GroupValue); // Set up group attribute
						if (RowGroupLevel >= 2)
							RowAttrs["data-group-2"] = Convert.ToString(LeadStatusId.GroupValue); // Set up group attribute 2
					} else {
						RowAttrs["data-group"] = Convert.ToString(BranchId.GroupValue); // Set up group attribute
						RowAttrs["data-group-2"] = Convert.ToString(LeadStatusId.GroupValue); // Set up group attribute 2
					}

					// BranchId
					curVal = Convert.ToString(BranchId.GroupValue);
					if (!Empty(curVal)) {
						BranchId.GroupViewValue = BranchId.LookupCacheOption(curVal);
						if (BranchId.GroupViewValue == null) { // Lookup from database
							filterWrk = "[Id]" + SearchString("=", curVal.Trim(), Config.DataTypeNumber, "");
							sqlWrk = BranchId.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(listwrk[1]);
								BranchId.GroupViewValue = BranchId.DisplayValue(listwrk);
							} else {
								BranchId.GroupViewValue = BranchId.GroupValue;
							}
						}
					} else {
						BranchId.GroupViewValue = System.DBNull.Value;
					}
					BranchId.CellCssClass = "ew-rpt-grp-field-1";
					BranchId.ViewCustomAttributes = "";
					BranchId.GroupViewValue = DisplayGroupValue(BranchId, BranchId.GroupViewValue);
					if (!BranchId.LevelBreak)
						BranchId.GroupViewValue = "&nbsp;";
					else
						BranchId.LevelBreak = false;

					// LeadStatusId
					curVal = Convert.ToString(LeadStatusId.GroupValue);
					if (!Empty(curVal)) {
						LeadStatusId.GroupViewValue = LeadStatusId.LookupCacheOption(curVal);
						if (LeadStatusId.GroupViewValue == null) { // Lookup from database
							filterWrk = "[Id]" + SearchString("=", curVal.Trim(), Config.DataTypeNumber, "");
							sqlWrk = LeadStatusId.Lookup.GetSql(false, filterWrk, null, this);
							rswrk = await Connection.GetRowsAsync(sqlWrk);
							if (rswrk != null && rswrk.Count > 0) { // Lookup values found
								var listwrk = rswrk[0].Values.ToList();
								listwrk[1] = Convert.ToString(listwrk[1]);
								LeadStatusId.GroupViewValue = LeadStatusId.DisplayValue(listwrk);
							} else {
								LeadStatusId.GroupViewValue = LeadStatusId.GroupValue;
							}
						}
					} else {
						LeadStatusId.GroupViewValue = System.DBNull.Value;
					}
					LeadStatusId.CellCssClass = "ew-rpt-grp-field-2";
					LeadStatusId.ViewCustomAttributes = "";
					LeadStatusId.GroupViewValue = DisplayGroupValue(LeadStatusId, LeadStatusId.GroupViewValue);
					if (!LeadStatusId.LevelBreak)
						LeadStatusId.GroupViewValue = "&nbsp;";
					else
						LeadStatusId.LevelBreak = false;

					// LeadId
					LeadId.ViewValue = LeadId.CurrentValue;
					LeadId.CellCssClass = (RecordCount % 2 != 1 ? "ew-table-alt-row" : "ew-table-row");
					LeadId.ViewCustomAttributes = "";

					// Name
					_Name.ViewValue = Convert.ToString(_Name.CurrentValue); // DN
					_Name.CellCssClass = (RecordCount % 2 != 1 ? "ew-table-alt-row" : "ew-table-row");
					_Name.ViewCustomAttributes = "";

					// BranchId
					BranchId.HrefValue = "";
					BranchId.TooltipValue = "";

					// LeadStatusId
					LeadStatusId.HrefValue = "";
					LeadStatusId.TooltipValue = "";

					// LeadId
					LeadId.HrefValue = "";
					LeadId.TooltipValue = "";

					// Name
					_Name.HrefValue = "";
					_Name.TooltipValue = "";
				}

				// Call Cell_Rendered event
				object viewValue; // DN
				if (RowType == Config.RowTypeTotal) { // Summary row

					// BranchId
					Cell_Rendered(BranchId, BranchId.GroupViewValue, ref BranchId.GroupViewValue, BranchId.ViewAttrs, BranchId.CellAttrs, ref BranchId.HrefValue, BranchId.LinkAttrs);

					// LeadStatusId
					Cell_Rendered(LeadStatusId, LeadStatusId.GroupViewValue, ref LeadStatusId.GroupViewValue, LeadStatusId.ViewAttrs, LeadStatusId.CellAttrs, ref LeadStatusId.HrefValue, LeadStatusId.LinkAttrs);
				} else {

					// BranchId
					Cell_Rendered(BranchId, BranchId.CurrentValue, ref BranchId.ViewValue, BranchId.ViewAttrs, BranchId.CellAttrs, ref BranchId.HrefValue, BranchId.LinkAttrs);

					// LeadStatusId
					Cell_Rendered(LeadStatusId, LeadStatusId.CurrentValue, ref LeadStatusId.ViewValue, LeadStatusId.ViewAttrs, LeadStatusId.CellAttrs, ref LeadStatusId.HrefValue, LeadStatusId.LinkAttrs);

					// LeadId
					Cell_Rendered(LeadId, LeadId.CurrentValue, ref LeadId.ViewValue, LeadId.ViewAttrs, LeadId.CellAttrs, ref LeadId.HrefValue, LeadId.LinkAttrs);

					// Name
					Cell_Rendered(_Name, _Name.CurrentValue, ref _Name.ViewValue, _Name.ViewAttrs, _Name.CellAttrs, ref _Name.HrefValue, _Name.LinkAttrs);
				}

				// Call Row_Rendered event
				Row_Rendered();

				// Setup field count
				SetupFieldCount();
			}
			#pragma warning restore 168

			// Group count
			private Dictionary<string, int> _groupCounts = new Dictionary<string, int>();

			// Get group count
			public int GetGroupCount(params int[] args) {
				string key = String.Join("_", args.Select(arg => arg.ToString()));
				if (Empty(key)) {
					return -1;
				} else if (key == "0") { // Number of first level groups
					int i = 1;
					while (_groupCounts.ContainsKey(i.ToString()))
						i++;
					return i - 1;
				}
				return _groupCounts.TryGetValue(key, out int cnt) ? cnt : -1;
			}

			// Set group count
			public void SetGroupCount(int value, params int[] args) {
				string key = String.Join("_", args.Select(arg => arg.ToString()));
				if (Empty(key))
					return;
				_groupCounts[key] = value;
			}

			// Setup field count
			public void SetupFieldCount() {
				GroupColumnCount = 0;
				SubGroupColumnCount = 0;
				DetailColumnCount = 0;
				if (BranchId.Visible)
					GroupColumnCount++;
				if (LeadStatusId.Visible) {
					GroupColumnCount++;
					SubGroupColumnCount++;
				}
				if (LeadId.Visible)
					DetailColumnCount++;
				if (_Name.Visible)
					DetailColumnCount++;
			}

			// Save data to memory cache
			public void SetCache<T>(string key, T value, int span) => Cache.Set<T>(key, value, new MemoryCacheEntryOptions()
				.SetSlidingExpiration(TimeSpan.FromMilliseconds(span))); // Keep in cache for this time, reset time if accessed

			// Gete data from memory cache
			public void GetCache<T>(string key) => Cache.Get<T>(key);

			// Get export HTML tag
			protected string GetExportTag(string type, bool custom = false) {
				return type.ToLower() switch {
					"excel" => "<a class=\"ew-export-link ew-excel\" title=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) +
						"\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToExcel", true)) +
						"\" href=\"#\" onclick=\"ew.exportWithCharts(event, '" + ExportExcelUrl + "', '" + Session.SessionId + "');\">" +
						Language.Phrase("ExportToExcel") + "</a>",
					"word" => "<a class=\"ew-export-link ew-word\" title=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) +
						"\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToWord", true)) +
						"\" href=\"#\" onclick=\"ew.exportWithCharts(event, '" + ExportWordUrl + "', '" + Session.SessionId + "');\">" +
						Language.Phrase("ExportToWord") + "</a>",
					"pdf" => "<a class=\"ew-export-link ew-pdf\" title=\"" + HtmlEncode(Language.Phrase("ExportToPDF", true)) +
						"\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToPDF", true)) +
						"\" href=\"#\" onclick=\"ew.exportWithCharts(event, '" + ExportPdfUrl + "', '" + Session.SessionId + "');\">" +
						Language.Phrase("ExportToPDF") + "</a>",
					"email" => "<a class=\"ew-export-link ew-email\" title=\"" + HtmlEncode(Language.Phrase("ExportToEmail", true)) +
						"\" data-caption=\"" + HtmlEncode(Language.Phrase("ExportToEmail", true)) +
						"\" id=\"emf_Active_Leads_By_Branch\" href=\"#\" onclick=\"ew.emailDialogShow({ lnk: 'emf_Active_Leads_By_Branch', hdr: ew.language.phrase('ExportToEmailText'), url: '" + PageUrl + "export=email', exportid: '" + Session.SessionId + "', el: this }); false;\">" +
						Language.Phrase("ExportToEmail") + "</a>",
					"print" => "<a href=\"" + ExportPrintUrl + "\" class=\"ew-export-link ew-print\" title=\"" + HtmlEncode(Language.Phrase("PrinterFriendlyText")) + "\" data-caption=\"" + HtmlEncode(Language.Phrase("PrinterFriendlyText")) + "\">" + Language.Phrase("PrinterFriendly") + "</a>",
					_ => ""
				};
			}

			// Set up export options
			protected void SetupExportOptions() {
				ListOption item;

				// Printer friendly
				item = ExportOptions.Add("print");
				item.Body = GetExportTag("print");
				item.Visible = false;

				// Export to Excel
				item = ExportOptions.Add("excel");
				item.Body = GetExportTag("excel");
				item.Visible = false;

				// Export to Word
				item = ExportOptions.Add("word");
				item.Body = GetExportTag("word");
				item.Visible = false;

				// Export to PDF
				item = ExportOptions.Add("pdf");
				item.Body = GetExportTag("pdf");
				item.Visible = false;

				// Export to Email
				item = ExportOptions.Add("email");
				item.Body = GetExportTag("email");
				item.Visible = false;

				// Drop down button for export
				ExportOptions.UseButtonGroup = true;
				ExportOptions.UseDropDownButton = false;
				if (ExportOptions.UseButtonGroup && IsMobile())
					ExportOptions.UseDropDownButton = true;
				ExportOptions.DropDownButtonPhrase = Language.Phrase("ButtonExport");

				// Add group option item
				item = ExportOptions.Add(ExportOptions.GroupOptionName);
				item.Body = "";
				item.Visible = false;

				// Hide options for export
				if (IsExport())
					ExportOptions.HideAllOptions();
			}

			// Set up search options
			protected void SetupSearchOptions() {
				ListOption item;
				SearchOptions = new ListOptions();
				SearchOptions.Tag = "div";
				SearchOptions.TagClassName = "ew-search-option";

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
				breadcrumb.Add("summary", TableVar, url, "", TableVar, true);
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

			// Set up other options
			public void SetupOtherOptions() {

				// Filter button
				var item = FilterOptions.Add("savecurrentfilter");
				item.Body = "<a class=\"ew-save-filter\" data-form=\"fsummary\" href=\"#\">" + Language.Phrase("SaveCurrentFilter") + "</a>";
				item.Visible = true;
				item = FilterOptions.Add("deletefilter");
				item.Body = "<a class=\"ew-delete-filter\" data-form=\"fsummary\" href=\"#\">" + Language.Phrase("DeleteFilter") + "</a>";
				item.Visible = false;
				FilterOptions.UseDropDownButton = true;
				FilterOptions.UseButtonGroup = !FilterOptions.UseDropDownButton;
				FilterOptions.DropDownButtonPhrase = Language.Phrase("Filters");

				// Add group option item
				item = FilterOptions.Add(FilterOptions.GroupOptionName);
				item.Body = "";
				item.Visible = false;
			}

			// Set up starting group
			public void SetupStartGroup() {

				// Exit if no groups
				if (DisplayGroups == 0)
					return;
				string startGrp = Param(Config.TableStartRec);
				string pageNo = Param("pageno");

				// Check for a 'start' parameter
				if (!Empty(startGrp) &&
					IsNumeric(startGrp)) {
					StartGroup = ConvertToInt(startGrp);
					SessionStartGroup = StartGroup;
				} else if (!Empty(pageNo)) {
					if (IsNumeric(pageNo)) {
						int nPageNo = ConvertToInt(pageNo);
						StartGroup = (nPageNo - 1) * DisplayGroups + 1;
						if (StartGroup <= 0) {
							StartGroup = 1;
						} else if (StartGroup >= (TotalGroups - 1) / DisplayGroups * DisplayGroups + 1) {
							StartGroup = (TotalGroups - 1) / DisplayGroups * DisplayGroups + 1;
						}
						SessionStartGroup = StartGroup;
					} else {
						StartGroup = SessionStartGroup;
					}
				} else {
					StartGroup = SessionStartGroup;
				}

				// Check if correct start group counter
				if (StartGroup <= 0) {
					StartGroup = 1; // Reset start group counter
					SessionStartGroup = StartGroup;
				} else if (StartGroup > TotalGroups) { // Avoid starting group > total groups
					StartGroup = (TotalGroups - 1) / DisplayGroups * DisplayGroups + 1; // Point to last page first group
					SessionStartGroup = StartGroup;
				} else if ((StartGroup - 1) % DisplayGroups != 0) {
					StartGroup = (StartGroup - 1) / DisplayGroups * DisplayGroups + 1; // Point to page boundary
					SessionStartGroup = StartGroup;
				}
			}

			// Reset pager
			public void ResetPager() {
				StartGroup = 1;
				SessionStartGroup = StartGroup;
			}

			// Set up number of groups displayed per page
			public void SetupDisplayGroups() {
				string wrk = Param(Config.TableRecordsPerPage);
				if (!Empty(wrk)) {
					if (IsNumeric(wrk)) {
						DisplayGroups = ConvertToInt(wrk);
					} else {
						if (SameText(wrk, "all")) { // Display all records
							DisplayGroups = -1; // All records
						} else {
							DisplayGroups = 3; // Non-numeric, load default
						}
					}
					GroupPerPage = DisplayGroups; // Save to session

					// Reset start position (reset command)
					StartGroup = 1;
					SessionStartGroup = StartGroup;
				} else {
					if (GroupPerPage == -1 || GroupPerPage > 0) { // DN
						DisplayGroups = GroupPerPage; // Restore from session
					} else {
						DisplayGroups = 3; // Load default
					}
				}
			}

			// Get sort parameters based on sort links clicked
			public string GetSort() {
				if (DrillDown)
					return "";
				bool resetSort = Param("cmd") == "resetsort";
				string orderBy = Param("order"), orderType = Param("ordertype");

				// Check for a resetsort command
				if (resetSort) {
					OrderBy = "";
					SessionStartGroup = 1;
					LeadId.Sort = "";
					_Name.Sort = "";
					LeadStatusId.Sort = "";
					BranchId.Sort = "";

				// Check for an Order parameter
				} else if (!Empty(orderBy)) {
					CurrentOrder = orderBy;
					CurrentOrderType = orderType;
					UpdateSort(LeadId); // LeadId
					UpdateSort(_Name); // Name
					UpdateSort(LeadStatusId); // LeadStatusId
					UpdateSort(BranchId); // BranchId
					OrderBy = SortSql;
					SessionStartGroup = 1;
				}
				return OrderBy;
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

			// Page Breaking event
			public void Page_Breaking(ref bool brk, ref string content) {

				// Example:
				//	brk = false; // Skip page break, or
				//	content = "<div style=\"page-break-after:always;\">&nbsp;</div>"; // Modify page break content

			}

			// Load Filters event
			public void Page_FilterLoad() {

				// Enter your code here
				// Example: Register/Unregister Custom Extended Filter
				//	RegisterFilter(<Field>, "StartsWithA", "Starts With A", "GetStartsWithAFilter"); // With function, or
				//	RegisterFilter(<Field>, "StartsWithA", "Starts With A"); // No function, use Page_Filtering event
				//	UnregisterFilter(<Field>, "StartsWithA");

			}

			// Page Selecting event
			public void Page_Selecting(ref string filter) {

				// Enter your code here
			}

			// Page Filter Validated event
			public void Page_FilterValidated() {

				// Example:
				//MyField1.SearchValue = "your search criteria"; // Search value

			}

			// Page Filtering event
			public void Page_Filtering(ReportField fld, ref string filter, string typ, string opr, string val, string cond, string opr2, string val2) {

				// Note: ALWAYS CHECK THE FILTER TYPE (typ)! Example:
				//	if (typ == "dropdown" && fld.Name == "MyField") // Dropdown filter
				//		filter = "..."; // Modify the filter
				//	if (typ == "extended" && fld.Name == "MyField") // Extended filter
				//		filter = "..."; // Modify the filter
				//	if (typ == "popup" && fld.Name == "MyField") // Popup filter
				//		filter = "..."; // Modify the filter
				//	if (typ == "custom" && opr == "..." && fld.Name == "MyField") // Custom filter, opr is the custom filter ID
				//		filter = "..."; // Modify the filter

			}

			// Cell Rendered event
			public void Cell_Rendered(ReportField field, object currentValue, ref object viewValue, Attributes viewAttrs, Attributes CellAttrs, ref object hrefValue, Attributes linkAttrs) {

				//ViewValue = "xxx";
				//ViewAttrs["style"] = "xxx";

			}

			// Form Custom Validate event
			public virtual bool Form_CustomValidate(ref string customError) {

				//Return error message in customError
				return true;
			}
		} // End page class
	} // End Partial class
} // End namespace