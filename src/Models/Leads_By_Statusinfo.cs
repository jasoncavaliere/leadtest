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
		/// Leads_By_Status
		/// </summary>
		public static _Leads_By_Status Leads_By_Status {
			get => HttpData.GetOrCreate<_Leads_By_Status>("Leads_By_Status");
			set => HttpData["Leads_By_Status"] = value;
		}

		/// <summary>
		/// Table class for Leads By Status
		/// </summary>
		public class _Leads_By_Status: ReportTable {
			#pragma warning disable 414

			public bool ShowGroupHeaderAsRow = false;

			public bool ShowCompactSummaryFooter = true;
			#pragma warning restore 414

			public int RowCount = 0; // DN

			public bool UseSessionForListSql = true;

			public readonly ReportField<SqlDbType> LeadId;

			public readonly ReportField<SqlDbType> _Name;

			public readonly ReportField<SqlDbType> State;

			public readonly ReportField<SqlDbType> LeadStatusId;

			public readonly ReportField<SqlDbType> BranchId;

			public readonly ReportField<SqlDbType> UserId;

			public readonly ReportField<SqlDbType> FirstName;

			public readonly ReportField<SqlDbType> LastName;

			public readonly ReportField<SqlDbType> BlobUrl;

			public readonly ReportField<SqlDbType> EmailAddress;

			public readonly ReportField<SqlDbType> PhoneNumber;

			// Constructor
			public _Leads_By_Status() {

				// Language object // DN
				Language ??= new Lang();
				TableVar = "Leads_By_Status";
				Name = "Leads By Status";
				Type = "REPORT";

				// Update Table
				UpdateTable = "[dbo].[Leads]";
				ReportSourceTable = "_Leads"; // Report source table
				ReportSourceTableName = "Leads"; // Report source table name
				DbId = "DB"; // DN
				ExportAll = true;
				ExportPageBreakCount = 0; // Page break per every n record (report only)
				ExportPageOrientation = "portrait"; // Page orientation (PDF only)
				ExportPageSize = "a4"; // Page size (PDF only)
				ExportExcelPageOrientation = ""; // Page orientation (EPPlus only)
				ExportExcelPageSize = ""; // Page size (EPPlus only)
				ExportColumnWidths = new float[] {  }; // Column widths (PDF only) // DN
				UserIdAllowSecurity = 0; // User ID Allow

				// LeadId
				LeadId = new ReportField<SqlDbType> {
					TableVar = "Leads_By_Status",
					TableName = "Leads By Status",
					FieldVar = "x_LeadId",
					Name = "LeadId",
					Expression = "[LeadId]",
					BasicSearchExpression = "[LeadId]",
					Type = 72,
					DbType = SqlDbType.UniqueIdentifier,
					DateTimeFormat = -1,
					VirtualExpression = "[LeadId]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "HIDDEN",
					IsPrimaryKey = true, // Primary key field
					Nullable = false, // NOT NULL field
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectGUID"),
					SourceTableVar = "_Leads",
					IsUpload = false
				};
				LeadId.Init(this); // DN
				Fields.Add("LeadId", LeadId);

				// Name
				_Name = new ReportField<SqlDbType> {
					TableVar = "Leads_By_Status",
					TableName = "Leads By Status",
					FieldVar = "x__Name",
					Name = "Name",
					Expression = "[Name]",
					BasicSearchExpression = "[Name]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[Name]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					SourceTableVar = "_Leads",
					IsUpload = false
				};
				_Name.Init(this); // DN
				Fields.Add("Name", _Name);

				// State
				State = new ReportField<SqlDbType> {
					TableVar = "Leads_By_Status",
					TableName = "Leads By Status",
					FieldVar = "x_State",
					Name = "State",
					Expression = "[State]",
					BasicSearchExpression = "[State]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[State]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Nullable = false, // NOT NULL field
					Required = true, // Required field
					Sortable = true, // Allow sort
					SourceTableVar = "_Leads",
					IsUpload = false
				};
				State.Init(this); // DN
				Fields.Add("State", State);

				// LeadStatusId
				LeadStatusId = new ReportField<SqlDbType> {
					TableVar = "Leads_By_Status",
					TableName = "Leads By Status",
					FieldVar = "x_LeadStatusId",
					Name = "LeadStatusId",
					Expression = "[LeadStatusId]",
					BasicSearchExpression = "CAST([LeadStatusId] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[LeadStatusId]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "SELECT",
					GroupingFieldId = 1,
					ShowGroupHeaderAsRow = false,
					ShowCompactSummaryFooter = true,
					GroupByType = "",
					GroupInterval = "0",
					GroupSql = "",
					Sortable = true, // Allow sort
					UsePleaseSelect = true, // Use PleaseSelect by default
					PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
					DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
					SourceTableVar = "_Leads",
					IsUpload = false
				};
				LeadStatusId.Init(this); // DN
				LeadStatusId.Lookup = new Lookup<DbField>("LeadStatusId", "LeadStatus", false, "Id", new List<string> {"DisplayName", "", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "");
				Fields.Add("LeadStatusId", LeadStatusId);

				// BranchId
				BranchId = new ReportField<SqlDbType> {
					TableVar = "Leads_By_Status",
					TableName = "Leads By Status",
					FieldVar = "x_BranchId",
					Name = "BranchId",
					Expression = "[BranchId]",
					BasicSearchExpression = "CAST([BranchId] AS NVARCHAR)",
					Type = 3,
					DbType = SqlDbType.Int,
					DateTimeFormat = -1,
					VirtualExpression = "[BranchId]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "SELECT",
					Sortable = true, // Allow sort
					UsePleaseSelect = true, // Use PleaseSelect by default
					PleaseSelectText = Language.Phrase("PleaseSelect"), // PleaseSelect text
					DefaultErrorMessage = Language.Phrase("IncorrectInteger"),
					SourceTableVar = "_Leads",
					IsUpload = false
				};
				BranchId.Init(this); // DN
				BranchId.Lookup = new Lookup<DbField>("BranchId", "BankBranch", false, "Id", new List<string> {"Name", "", "", ""}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, new List<string> {}, "", "");
				Fields.Add("BranchId", BranchId);

				// UserId
				UserId = new ReportField<SqlDbType> {
					TableVar = "Leads_By_Status",
					TableName = "Leads By Status",
					FieldVar = "x_UserId",
					Name = "UserId",
					Expression = "[UserId]",
					BasicSearchExpression = "[UserId]",
					Type = 72,
					DbType = SqlDbType.UniqueIdentifier,
					DateTimeFormat = -1,
					VirtualExpression = "[UserId]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectGUID"),
					SourceTableVar = "_Leads",
					IsUpload = false
				};
				UserId.Init(this); // DN
				Fields.Add("UserId", UserId);

				// FirstName
				FirstName = new ReportField<SqlDbType> {
					TableVar = "Leads_By_Status",
					TableName = "Leads By Status",
					FieldVar = "x_FirstName",
					Name = "FirstName",
					Expression = "[FirstName]",
					BasicSearchExpression = "[FirstName]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[FirstName]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					SourceTableVar = "_Leads",
					IsUpload = false
				};
				FirstName.Init(this); // DN
				Fields.Add("FirstName", FirstName);

				// LastName
				LastName = new ReportField<SqlDbType> {
					TableVar = "Leads_By_Status",
					TableName = "Leads By Status",
					FieldVar = "x_LastName",
					Name = "LastName",
					Expression = "[LastName]",
					BasicSearchExpression = "[LastName]",
					Type = 130,
					DbType = SqlDbType.NChar,
					DateTimeFormat = -1,
					VirtualExpression = "[LastName]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					SourceTableVar = "_Leads",
					IsUpload = false
				};
				LastName.Init(this); // DN
				Fields.Add("LastName", LastName);

				// BlobUrl
				BlobUrl = new ReportField<SqlDbType> {
					TableVar = "Leads_By_Status",
					TableName = "Leads By Status",
					FieldVar = "x_BlobUrl",
					Name = "BlobUrl",
					Expression = "[BlobUrl]",
					BasicSearchExpression = "[BlobUrl]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[BlobUrl]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					SourceTableVar = "_Leads",
					IsUpload = false
				};
				BlobUrl.Init(this); // DN
				Fields.Add("BlobUrl", BlobUrl);

				// EmailAddress
				EmailAddress = new ReportField<SqlDbType> {
					TableVar = "Leads_By_Status",
					TableName = "Leads By Status",
					FieldVar = "x_EmailAddress",
					Name = "EmailAddress",
					Expression = "[EmailAddress]",
					BasicSearchExpression = "[EmailAddress]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[EmailAddress]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectEmail"),
					SourceTableVar = "_Leads",
					IsUpload = false
				};
				EmailAddress.Init(this); // DN
				Fields.Add("EmailAddress", EmailAddress);

				// PhoneNumber
				PhoneNumber = new ReportField<SqlDbType> {
					TableVar = "Leads_By_Status",
					TableName = "Leads By Status",
					FieldVar = "x_PhoneNumber",
					Name = "PhoneNumber",
					Expression = "[PhoneNumber]",
					BasicSearchExpression = "[PhoneNumber]",
					Type = 200,
					DbType = SqlDbType.VarChar,
					DateTimeFormat = -1,
					VirtualExpression = "[PhoneNumber]",
					IsVirtual = false,
					ForceSelection = false,
					SelectMultiple = false,
					VirtualSearch = false,
					ViewTag = "FORMATTED TEXT",
					HtmlTag = "TEXT",
					Sortable = true, // Allow sort
					DefaultErrorMessage = Language.Phrase("IncorrectPhone"),
					SourceTableVar = "_Leads",
					IsUpload = false
				};
				PhoneNumber.Init(this); // DN
				Fields.Add("PhoneNumber", PhoneNumber);
			}

			// Set Field Visibility
			public override bool GetFieldVisibility(string fldname) {
				var fld = FieldByName(fldname);
				return fld.Visible; // Returns original value
			}

			// Invoke method // DN
			public object Invoke(string name, object[] parameters = null) {
				MethodInfo mi = this.GetType().GetMethod(name);
				if (mi != null) {
					if (IsAsyncMethod(mi)) {
						return InvokeAsync(mi, parameters).GetAwaiter().GetResult();
					} else {
						return mi.Invoke(this, parameters);
					}
				}
				return null;
			}

			// Invoke async method // DN
			public async Task<object> InvokeAsync(MethodInfo mi, object[] parameters = null) {
				if (mi != null) {
					dynamic awaitable = mi.Invoke(this, parameters);
					await awaitable;
					return awaitable.GetAwaiter().GetResult();
				}
				return null;
			}
			#pragma warning disable 1998

			// Invoke async method // DN
			public async Task<object> InvokeAsync(string name, object[] parameters = null) => InvokeAsync(this.GetType().GetMethod(name), parameters);
			#pragma warning restore 1998

			// Check if Invoke async method // DN
			public bool IsAsyncMethod(MethodInfo mi) {
				if (mi != null) {
					Type attType = typeof(AsyncStateMachineAttribute);
					var attrib = (AsyncStateMachineAttribute)mi.GetCustomAttribute(attType);
					return (attrib != null);
				}
				return false;
			}

			// Check if Invoke async method // DN
			public bool IsAsyncMethod(string name) => IsAsyncMethod(this.GetType().GetMethod(name));
			#pragma warning disable 618

			// Connection
			public virtual DatabaseConnectionBase<SqlConnection, SqlCommand, SqlDataReader, SqlDbType> Connection => GetConnection(DbId);
			#pragma warning restore 618

			// Single column sort // DN
			public void UpdateSort(ReportField fld) {
				if (CurrentOrder == fld.Name) {
					string sortField = fld.Expression, lastSort = fld.Sort, currentSort = "";
					if (CurrentOrderType == "ASC" || CurrentOrderType == "DESC") {
						currentSort = CurrentOrderType;
					} else {
						currentSort = (lastSort == "ASC") ? "DESC" : "ASC";
					}
					fld.Sort = currentSort;
					if (fld.GroupingFieldId == 0)
						DetailOrderBy = sortField + " " + currentSort; // Save to Session
				} else {
					if (fld.GroupingFieldId == 0)
						fld.Sort = "";
				}
			}

			// Multiple column sort // DN
			public void UpdateSort(ReportField fld, bool ctrl) {
				if (CurrentOrder == fld.Name) {
					string sortField = fld.Expression, lastSort = fld.Sort, currentSort = "";
					if (CurrentOrderType == "ASC" || CurrentOrderType == "DESC") {
						currentSort = CurrentOrderType;
					} else {
						currentSort = (lastSort == "ASC") ? "DESC" : "ASC";
					}
					fld.Sort = currentSort;
					if (fld.GroupingFieldId == 0) {
						if (ctrl) {
							string orderBy = DetailOrderBy;
							if (orderBy.Contains(sortField + " " + lastSort)) {
								orderBy = orderBy.Replace(sortField + " " + lastSort, sortField + " " + currentSort);
							} else {
								if (!Empty(orderBy)) orderBy += ", ";
								orderBy += sortField + " " + currentSort;
							}
							DetailOrderBy = orderBy; // Save to Session
						} else {
							DetailOrderBy = sortField + " " + currentSort; // Save to Session
						}
					}
				} else {
					if (fld.GroupingFieldId == 0 && !ctrl)
						fld.Sort = "";
				}
			}

			// Get Sort SQL
			public string SortSql {
				get {
					string detailSortSql = DetailOrderBy; // Get ORDER BY for detail fields from session
					var groups = new List<string>();
					foreach (ReportField fld in Fields.Values) {
						if (!Empty(fld.Sort)) {
							string fldsql = fld.Expression;
							if (fld.GroupingFieldId > 0) {
								if (!Empty(fld.GroupSql))
									groups.Add(fld.GroupSql.Replace("%s", fldsql) + " " + fld.Sort);
								else
									groups.Add(fldsql + " " + fld.Sort);
							}
						}
					}
					if (!Empty(detailSortSql))
						groups.Add(detailSortSql);
					return String.Join(", ", groups);
				}
			}

			// Crosstab/Summary report private properties
			private string _sqlFirstGroupField = "";

			private string _sqlSelectGroup = "";

			private string _sqlOrderByGroup = "";

			public ReportField FirstGroupField; // DN

			// First Group Field
			public string SqlFirstGroupField {
				get => GetSqlFirstGroupField();
				set => _sqlFirstGroupField = value;
			}

			// Get First Group Field // DN
			public string GetSqlFirstGroupField(bool alias = false) {
				if (!Empty(_sqlFirstGroupField))
					return _sqlFirstGroupField;
				string expr = FirstGroupField.Expression;
				if (!Empty(FirstGroupField.GroupSql)) {
					expr = FirstGroupField.GroupSql.Replace("%s", FirstGroupField.Expression);
					if (alias)
						expr += " AS " + QuotedName(FirstGroupField.GroupName, DbId);
				}
				return expr;
			}

			// Select Group
			public string SqlSelectGroup {
				get => !Empty(_sqlSelectGroup) ? _sqlSelectGroup : "SELECT DISTINCT " + GetSqlFirstGroupField(true) + " FROM " + SqlFrom;
				set => _sqlSelectGroup = value;
			}

			// Order By Group
			public string SqlOrderByGroup {
				get => !Empty(_sqlOrderByGroup) ? _sqlOrderByGroup : SqlFirstGroupField + " ASC";
				set => _sqlOrderByGroup = value;
			}

			// Summary/Simple report private properties
			private string _sqlSelectAggregate = "";

			private string _sqlAggregatePrefix = "";

			private string _sqlAggregateSuffix = "";

			private string _sqlSelectCount = "";

			// Select Aggregate
			public string SqlSelectAggregate {
				get => !Empty(_sqlSelectAggregate) ? _sqlSelectAggregate : "SELECT * FROM " + SqlFrom;
				set => _sqlSelectAggregate = value;
			}

			// Aggregate Prefix
			public string SqlAggregatePrefix {
				get => !Empty(_sqlAggregatePrefix) ? _sqlAggregatePrefix : "";
				set => _sqlAggregatePrefix = value;
			}

			// Aggregate Suffix
			public string SqlAggregateSuffix {
				get => !Empty(_sqlAggregateSuffix) ? _sqlAggregateSuffix : "";
				set => _sqlAggregateSuffix = value;
			}

			// Select Count
			public string SqlSelectCount {
				get => !Empty(_sqlSelectCount) ? _sqlSelectCount : "SELECT COUNT(*) FROM " + SqlFrom;
				set => _sqlSelectCount = value;
			}
			#pragma warning disable 1998

			// Render for lookup
			public async Task RenderLookup() {
			}
			#pragma warning restore 1998

			// Table level SQL
			// FROM
			private string _sqlFrom = null;

			public string SqlFrom {
				get => _sqlFrom ?? "[dbo].[Leads]";
				set => _sqlFrom = value;
			}

			// SELECT
			private string _sqlSelect = null;

			public string SqlSelect { // Select
				get {
					if (!Empty(_sqlSelect))
						return _sqlSelect;
					string select = "*";
					if (!Empty(LeadStatusId.GroupSql)) {
						string expr = LeadStatusId.GroupSql.Replace("%s", LeadStatusId.Expression) + " AS " + QuotedName(LeadStatusId.GroupName, DbId);
						select += ", " + expr;
					}
					return "SELECT " + select + " FROM " + SqlFrom;
				}
				set {
					_sqlSelect = value;
				}
			}

			// WHERE // DN
			private string _sqlWhere = null;

			public string SqlWhere {
				get {
					string where = "";
					return _sqlWhere ?? where;
				}
				set {
					_sqlWhere = value;
				}
			}

			// Group By
			private string _sqlGroupBy = null;

			public string SqlGroupBy {
				get => _sqlGroupBy ?? "";
				set => _sqlGroupBy = value;
			}

			// Having
			private string _sqlHaving = null;

			public string SqlHaving {
				get => _sqlHaving ?? "";
				set => _sqlHaving = value;
			}

			// Order By
			private string _sqlOrderBy = null;

			public string SqlOrderBy {
				get => _sqlOrderBy ?? "";
				set => _sqlOrderBy = value;
			}

			// Apply User ID filters
			public string ApplyUserIDFilters(string filter) {
				return filter;
			}

			// Check if User ID security allows view all
			public bool UserIDAllow(string id = "") {
				int allow = Config.UserIdAllow;
				return id switch {
					"add" => ((allow & 1) == 1),
					"copy" => ((allow & 1) == 1),
					"gridadd" => ((allow & 1) == 1),
					"register" => ((allow & 1) == 1),
					"addopt" => ((allow & 1) == 1),
					"edit" => ((allow & 4) == 4),
					"gridedit" => ((allow & 4) == 4),
					"update" => ((allow & 4) == 4),
					"changepwd" => ((allow & 4) == 4),
					"forgotpwd" => ((allow & 4) == 4),
					"delete" => ((allow & 2) == 2),
					"view" => ((allow & 32) == 32),
					"search" => ((allow & 64) == 64),
					_ => ((allow & 8) == 8)
				};
			}

			// Get record count by reading data reader
			public async Task<int> GetRecordCount(string sql, dynamic c = null) { // use by Lookup // DN
				try {
					var cnt = 0;
					var conn = c ?? Connection;
					using var dr = await conn.OpenDataReaderAsync(sql);
					while (await dr.ReadAsync())
						cnt++;
					return cnt;
				} catch {
					if (Config.Debug)
						throw;
					return -1;
				}
			}

			// Try to get record count by SELECT COUNT(*)
			public async Task<int> TryGetRecordCount(string sql, dynamic c = null) {
				string orderBy = OrderBy;
				var conn = c ?? Connection;
				sql = Regex.Replace(sql, @"/\*BeginOrderBy\*/[\s\S]+/\*EndOrderBy\*/", "", RegexOptions.IgnoreCase).Trim(); // Remove ORDER BY clause (MSSQL)
				if (!string.IsNullOrEmpty(orderBy) && sql.EndsWith(orderBy))
					sql = sql.Substring(0, sql.Length - orderBy.Length); // Remove ORDER BY clause
				try {
					string sqlcnt;
					if ((new List<string> { "TABLE", "VIEW", "LINKTABLE" }).Contains(Type) && sql.StartsWith(SqlSelect)) { // Handle Custom Field
						sqlcnt = "SELECT COUNT(*) FROM " + SqlFrom + sql.Substring(SqlSelect.Length);
					} else {
						sqlcnt = "SELECT COUNT(*) FROM (" + sql + ") EW_COUNT_TABLE";
					}
					return Convert.ToInt32(await conn.ExecuteScalarAsync(sqlcnt));
				} catch {
					return await GetRecordCount(sql, c);
				}
			}

			// Record filter WHERE clause
			private string _sqlKeyFilter => "[LeadId] = '@LeadId@'";
			#pragma warning disable 168

			// Get record filter
			public string GetRecordFilter(Dictionary<string, object> row = null)
			{
				string keyFilter = _sqlKeyFilter;
				object val, result;
				val = !Empty(row) && row.TryGetValue("LeadId", out result) ? result : null;
				val ??= !Empty(LeadId.OldValue) ? LeadId.OldValue : LeadId.CurrentValue; // DN
				if (val == null)
					return "0=1"; // Invalid key
				else
					keyFilter = keyFilter.Replace("@LeadId@", AdjustSql(AdjustGuid(val, DbId), DbId)); // Replace key value
				return keyFilter;
			}
			#pragma warning restore 168

			// Return URL
			public string ReturnUrl {
				get {
					string name = Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl;

					// Get referer URL automatically
					if (!Empty(ReferUrl()) && ReferPage() != CurrentPageName() &&
						ReferPage() != "login") {// Referer not same page or login page
							Session[name] = ReferUrl(); // Save to Session
					}
					if (!Empty(Session[name])) {
						return Session.GetString(name);
					} else {
						return "";
					}
				}
				set {
					Session[Config.ProjectName + "_" + TableVar + "_" + Config.TableReturnUrl] = value;
				}
			}

			// Get modal caption
			public string GetModalCaption(string pageName) {
				if (SameString(pageName, ""))
					return Language.Phrase("View");
				else if (SameString(pageName, ""))
					return Language.Phrase("Edit");
				else if (SameString(pageName, ""))
					return Language.Phrase("Add");
				else
					return "";
			}

			// List URL
			public string ListUrl => "";

			// View URL
			public string ViewUrl => GetViewUrl();

			// View URL
			public string GetViewUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = KeyUrl("", UrlParm(parm));
				else
					url = KeyUrl("", UrlParm(Config.TableShowDetail + "="));
				return AddMasterUrl(url);
			}

			// Add URL
			public string AddUrl { get; set; } = "";

			// Add URL
			public string GetAddUrl(string parm = "") {
				string url = "";
				if (!Empty(parm))
					url = "?" + UrlParm(parm);
				else
					url = "";
				return AppPath(AddMasterUrl(url));
			}

			// Edit URL
			public string EditUrl => GetEditUrl();

			// Edit URL (with parameter)
			public string GetEditUrl(string parm = "") {
				string url = "";
				url = KeyUrl("", UrlParm(parm));
				return AppPath(AddMasterUrl(url)); // DN
			}

			// Inline edit URL
			public string InlineEditUrl =>
				AppPath(AddMasterUrl(KeyUrl(CurrentPageName(), UrlParm("action=edit")))); // DN

			// Copy URL
			public string CopyUrl => GetCopyUrl();

			// Copy URL
			public string GetCopyUrl(string parm = "") {
				string url = "";
				url = KeyUrl("", UrlParm(parm));
				return AppPath(AddMasterUrl(url)); // DN
			}

			// Inline copy URL
			public string InlineCopyUrl =>
				AppPath(AddMasterUrl(KeyUrl(CurrentPageName(), UrlParm("action=copy")))); // DN

			// Delete URL
			public string DeleteUrl =>
				AppPath(KeyUrl("", UrlParm())); // DN

			// Add master URL
			public string AddMasterUrl(string url) {
				return url;
			}

			// Get primary key as JSON
			public string KeyToJson() {
				string json = "";
				json += "LeadId:" + ConvertToJson(LeadId.CurrentValue, "string", true);
				return "{" + json + "}";
			}

			// Add key value to URL
			public string KeyUrl(string url, string parm = "") { // DN
				if (!IsDBNull(LeadId.CurrentValue)) {
					url += "/" + RawUrlEncode(Convert.ToString(LeadId.CurrentValue));
				} else {
					return "javascript:ew.alert(ew.language.phrase('InvalidRecord'));";
				}
				if (Empty(parm))
					return url;
				else
					return url + "?" + parm;
			}

			// Sort URL (already URL-encoded)
			public string SortUrl(DbField fld) {
				if (!Empty(CurrentAction) || !Empty(Export) ||
					DrillDown || DashboardReport ||
					(new List<int> {141, 201, 203, 128, 204, 205}).Contains(fld.Type)) { // Unsortable data type
					return "";
				} else if (fld.Sortable) {
					string urlParm = UrlParm("order=" + UrlEncode(fld.Name) + "&amp;ordertype=" + fld.ReverseSort());
					return AddMasterUrl(CurrentPageName() + "?" + urlParm);
				}
				return "";
			}
			#pragma warning disable 168

			// Get record keys
			public List<string> GetRecordKeys() {
				var result = new List<string>();
				StringValues sv;
				var keysList = new List<string>();
				if (Post("key_m[]", out sv) || Get("key_m[]", out sv)) { // DN
					keysList = sv.ToList();
				} else if (RouteValues.Count > 0 || Query.Count > 0 || Form.Count > 0) { // DN
					string key = "";
					string[] keyValues = null;
					object rv;
					if (IsApi() && RouteValues.TryGetValue("key", out object k))
						keyValues = k.ToString().Split('/');
					if (RouteValues.TryGetValue("LeadId", out rv)) { // LeadId
						key = Convert.ToString(rv);
					} else if (IsApi() && !Empty(keyValues)) {
						key = keyValues[0];
					} else {
						key = Param("LeadId");
					}
					keysList.Add(key);
				}

				// Check keys
				foreach (var keys in keysList) {
					result.Add(keys);
				}
				return result;
			}
			#pragma warning restore 168

			// Table filter
			private string _tableFilter = null;

			public string TableFilter {
				get => _tableFilter ?? "";
				set => _tableFilter = value;
			}
			#pragma warning disable 1998

			// Get file data
			public async Task<IActionResult> GetFileData(string fldparm, string key, bool resize, int width = -1, int height = -1) {
				if (width < 0)
					width = Config.ThumbnailDefaultWidth;
				if (height < 0)
					height = Config.ThumbnailDefaultHeight;

				// No binary fields
				return JsonBoolResult.FalseResult; // Incorrect key
			}
			#pragma warning restore 1998

			// Email Sending event
			public virtual bool Email_Sending(Email email, dynamic args) {

				//Log(email);
				return true;
			}

			// Lookup Selecting event
			public void Lookup_Selecting(DbField fld, ref string filter) {

				// Enter your code here
			}

			// Row Rendering event
			public void Row_Rendering() {

				// Enter your code here
			}

			// Row Rendered event
			public void Row_Rendered() {

				//VarDump(<FieldName>); // View field properties
			}

			// User ID Filtering event
			public void UserID_Filtering(ref string filter) {

				// Enter your code here
			}
		}
	} // End Partial class
} // End namespace