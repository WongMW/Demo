using Aptify.Framework.DataServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
{
    public partial class EnhancedFirmSearchResults : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        private static int firmSearchPageSize = 20;

        private static string spGetCountryByName__cai => "spGetCountryByName__cai";
        private static string spGetTopicCodeByParentID__c => "spGetTopicCodeByParentID__c";
        private static string spEnhancedListingFullSearch__cai => "spEnhancedListingFullSearch__cai";

        private enum SearchType
        {
            Firm,
            Members,
            MemberInPractice
        }

        private SearchType CurrentSearchType
        {
            get
            {
                var t = Request.QueryString["type"];
                if (!String.IsNullOrEmpty(t) && t.ToLower().Equals("firm"))
                {
                    return SearchType.Firm;
                }
                else if (!String.IsNullOrEmpty(t) && t.ToLower().Equals("members"))
                {
                    return SearchType.Members;
                }
                else if (!String.IsNullOrEmpty(t) && t.ToLower().Equals("member-in-practice"))
                {
                    return SearchType.MemberInPractice;
                }

                return SearchType.Firm; // default
            }
        }

        public struct FirmSearchParams
        {
            public int countryId;
            public String city;
            public String firmName;
            public List<String> sectors;
            public List<String> specialisms;
            public List<int> authorisations;
            public int currentPage;
            private int singleFirmID;
            private List<int> listOfFirmsToSelect;
            private string dbConnection;

            public int SingleFirmID
            {
                get
                {
                    return singleFirmID;
                }
                set
                {
                    singleFirmID = value;
                }
            }
            public List<int> ListOfFirmsToSelect
            {
                get
                {
                    return listOfFirmsToSelect;
                }
                set
                {
                    listOfFirmsToSelect = value;
                }
            }

            public FirmSearchParams(int firmID)
            {
                this.dbConnection = SoftwareDesign.Helper.GetAptifyEntitiesConnectionString();
                this.currentPage = 1;
                this.countryId = -1;
                this.city = String.Empty;
                this.firmName = String.Empty;
                this.sectors = new List<string>();
                this.specialisms = new List<string>();
                this.authorisations = new List<int>();
                this.singleFirmID = firmID;
                this.listOfFirmsToSelect = new List<int>();
            }
            public FirmSearchParams(String countryId, String city, String firmName, String sectors, String specialisms, String authorisations, String currentPage)
            {
                this.dbConnection = SoftwareDesign.Helper.GetAptifyEntitiesConnectionString();
                this.singleFirmID = -1;
                this.listOfFirmsToSelect = new List<int>();
                // checking if at least one param is supplied
                if (!String.IsNullOrEmpty(countryId) ||
                    !String.IsNullOrEmpty(city) ||
                    !String.IsNullOrEmpty(firmName) ||
                    !String.IsNullOrEmpty(sectors) ||
                    !String.IsNullOrEmpty(specialisms) ||
                    !String.IsNullOrEmpty(authorisations) ||
                    !String.IsNullOrEmpty(currentPage))
                {
                    if (!String.IsNullOrEmpty(countryId))
                    {
                        // lets try to parse country id
                        this.countryId = -1;
                        if (!int.TryParse(countryId, out this.countryId))
                        {
                            this.countryId = -1;
                        }
                    }
                    else this.countryId = -1;

                    if (!String.IsNullOrEmpty(city))
                    {
                        this.city = city;
                    }
                    else this.city = String.Empty;

                    if (!String.IsNullOrEmpty(firmName))
                    {
                        this.firmName = firmName;
                    }
                    else this.firmName = String.Empty;

                    this.sectors = FirmSearchParams.ConvertStringToListOfStrings(sectors);
                    this.specialisms = FirmSearchParams.ConvertStringToListOfStrings(specialisms);
                    this.authorisations = ConvertStringToListOfIntegers(authorisations);

                    if (!String.IsNullOrEmpty(currentPage))
                    {
                        // lets try to parse country id
                        int currPage = 1;
                        if (int.TryParse(currentPage, out currPage))
                        {
                            this.currentPage = currPage;
                        }
                        else this.currentPage = 1;
                    }
                    else
                    {
                        this.currentPage = 1;
                    }
                } else
                {
                    this.currentPage = -1;
                    this.countryId = -1;
                    this.city = String.Empty;
                    this.firmName = String.Empty;
                    this.sectors = new List<string>();
                    this.specialisms = new List<string>();
                    this.authorisations = new List<int>();
                }
            }

            public static List<String> ConvertStringToListOfStrings(String commaSeparatedList)
            {
                List<String> data = new List<String>();

                if (!String.IsNullOrEmpty(commaSeparatedList))
                {
                    var parts = commaSeparatedList.Split(',');
                    foreach (var p in parts)
                    {
                        if (!String.IsNullOrEmpty(p.Trim()))
                        {
                            data.Add(p.Trim());
                        }
                    }
                }

                return data;
            }

            public static List<int> ConvertStringToListOfIntegers(String commaSeparatedList)
            {
                List<int> data = new List<int>();

                if (!String.IsNullOrEmpty(commaSeparatedList))
                {
                    var parts = commaSeparatedList.Split(',');
                    foreach (var p in parts)
                    {
                        int val = -1;
                        if (int.TryParse(p, out val))
                        {
                            data.Add(val);
                        }
                    }
                }

                return data;
            }

            public bool HasOneValue()
            {
                return countryId > 0 ||
                    !String.IsNullOrEmpty(city) ||
                    !String.IsNullOrEmpty(firmName) ||
                    (sectors != null && sectors.Count > 0) ||
                    (specialisms != null && specialisms.Count > 0) ||
                    (authorisations != null && authorisations.Count > 0);
            }

            private String GetSelectColumns()
            {
                return "FirmDirectoryLookupTableDaily__cai.*, " +
                    "TradingNames__c.TradingName, " +
                    "vwCompanies.CountryCodeID, vwCompanies.WebSite, vwCompanies.MainPhone, vwCompanies.County, vwCompanies.ParentID, " +
                    "EnhancedListingEditForm__cai.Specialisms, EnhancedListingEditForm__cai.IndustrySector, " +
                    "EnhancedListingEditForm__cai.FirmName, " +
                    "EnhancedListingEditForm__cai.NoOfEmployees, " +
                    "EnhancedListingEditForm__cai.NoOfPartners, " +
                    "EnhancedListingEditForm__cai.FirmDescription, " +
                    "EnhancedListingEditForm__cai.LocationURL, " +
                    "EnhancedListingEditForm__cai.LogoURL ";
            }

            private DataTable BuildSearchQuery(String columnQuery, bool dropPagination = false, bool selectOnlyTopEnhancedListing = false)
            {
                String enhancedListingTopJoinQuery = "";
                String defaultWhereQuery = this.BuildWherePartOfQuery();
                if (selectOnlyTopEnhancedListing)
                {
                    enhancedListingTopJoinQuery = "JOIN (select EnhancedListingAdmin__cai.FirmId from EnhancedListingAdmin__cai where EnhancedListingAdmin__cai.ApproveStatus = 1 AND GETDATE() < EnhancedListingAdmin__cai.ApprovedEndDate GROUP BY EnhancedListingAdmin__cai.FirmId) as enhancedListing ON enhancedListing.FirmId = FirmDirectoryLookupTableDaily__cai.FId ";
                }

                DataTable cdt = new DataTable();
                using (SqlConnection con = new SqlConnection(dbConnection))
                {
                    SqlCommand cmd = new SqlCommand(spEnhancedListingFullSearch__cai, con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@columnQuery", columnQuery);
                    cmd.Parameters.AddWithValue("@finalQueryPart",
                        (selectOnlyTopEnhancedListing ? enhancedListingTopJoinQuery : "") +
                        defaultWhereQuery +
                        (selectOnlyTopEnhancedListing ? (String.IsNullOrEmpty(defaultWhereQuery) ? "WHERE" : "AND") + " EnhancedListingEditForm__cai.IsActive = 1 AND EnhancedListingEditForm__cai.IsDraft = 0 " : "") +
                        (dropPagination && selectOnlyTopEnhancedListing ? "  ORDER BY NEWID() " : "") +
                        (!dropPagination ? "ORDER BY FirmDirectoryLookupTableDaily__cai.FirmName " : "") +
                        (!dropPagination ? (
                        "OFFSET " +
                        ((this.currentPage - 1) * firmSearchPageSize) + " ROWS FETCH NEXT " + firmSearchPageSize + " ROWS ONLY"
                        ) : "")
                    );
                    cmd.Connection = con;
                    con.Open();

                    using (SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd))
                    {
                        dataAdapter.SelectCommand = cmd;
                        dataAdapter.Fill(cdt);
                    }
                }

                return cdt;
            }

            public DataTable BuildSearchQuery()
            {
                return this.BuildSearchQuery(this.GetSelectColumns());
            }

            public DataTable BuildCountQuery()
            {
                return this.BuildSearchQuery("count(*) as TotalRecords ", true);
            }


            public DataTable BuildSearchQueryTopListing()
            {
                return this.BuildSearchQuery(this.GetSelectColumns(), true, true);
            }

            public DataTable BuildNameFIdSelectQuery()
            {
                return this.BuildSearchQuery(" FirmDirectoryLookupTableDaily__cai.FId, FirmDirectoryLookupTableDaily__cai.FirmName ", true);
            }

            private String BuildWherePartOfQuery()
            {
                String query = String.Empty;

                if(this.countryId > 0)
                {
                    if(!String.IsNullOrEmpty(query))
                    {
                        query += " AND ";
                    }

                    query += "vwCompanies.CountryCodeID = " + countryId;
                }

                if (!String.IsNullOrEmpty(this.city))
                {
                    if (!String.IsNullOrEmpty(query))
                    {
                        query += " AND ";
                    }

                    query += "(" + 
                        "LOWER(FirmDirectoryLookupTableDaily__cai.City) like '%" + this.city.Replace("'", "''") + "%'"+
                        " OR " +
                        "LOWER(vwCompanies.County) LIKE '%" + this.city.Replace("'", "''") + "%'" +
                    ")";
                }

                if (!String.IsNullOrEmpty(this.firmName))
                {
                    if (!String.IsNullOrEmpty(query))
                    {
                        query += " AND ";
                    }

                    query += "(" + 
                        "LOWER(FirmDirectoryLookupTableDaily__cai.FirmName) like '%" + this.firmName.Replace("'", "''") + "%'" + 
                        " OR " +
                        "LOWER(TradingNames__c.TradingName) like '%" + this.firmName.Replace("'", "''") + "%'" +
                        " OR " +
                        "LOWER(CompanyAlias__c.Name) like '%" + this.firmName.Replace("'", "''") + "%'" +
                    ")";
                }

                if (this.authorisations != null && this.authorisations.Contains((int)AuthorisationTypes.AUD))
                {
                    if (!String.IsNullOrEmpty(query))
                    {
                        query += " AND ";
                    }

                    query += "FirmDirectoryLookupTableDaily__cai.AUD = 'AUD'";
                }

                if (this.authorisations != null && this.authorisations.Contains((int)AuthorisationTypes.DPB))
                {
                    if (!String.IsNullOrEmpty(query))
                    {
                        query += " AND ";
                    }

                    query += "FirmDirectoryLookupTableDaily__cai.DPB = 'DPB'";
                }

                if (this.authorisations != null && this.authorisations.Contains((int)AuthorisationTypes.IB))
                {
                    if (!String.IsNullOrEmpty(query))
                    {
                        query += " AND ";
                    }

                    query += "FirmDirectoryLookupTableDaily__cai.IB = 'IB'";
                }

                if (this.authorisations != null && this.authorisations.Contains((int)AuthorisationTypes.ATOL))
                {
                    if (!String.IsNullOrEmpty(query))
                    {
                        query += " AND ";
                    }

                    query += "FirmDirectoryLookupTableDaily__cai.ATOL = 'ATOL'";
                }
                
                // building sectors check
                if(this.sectors != null && this.sectors.Count() > 0)
                {
                    var sectorsQuery = BuildComplexQueryFromListForColumn(this.sectors, "IndustrySector");

                    if (!String.IsNullOrEmpty(sectorsQuery))
                    {
                        if (!String.IsNullOrEmpty(query))
                        {
                            query += " AND ";
                        }

                        query += "(" + sectorsQuery + ")";
                    }
                }
                // ---
                // building specialisms check
                if (this.specialisms != null && this.specialisms.Count() > 0)
                {
                    var specialismsQuery = BuildComplexQueryFromListForColumn(this.specialisms, "Specialisms");

                    if (!String.IsNullOrEmpty(specialismsQuery))
                    {
                        if (!String.IsNullOrEmpty(query))
                        {
                            query += " AND ";
                        }

                        query += "(" + specialismsQuery + ")";
                    }
                }
                // ---

                // single company ID search
                if(SingleFirmID > 0)
                {
                    if (!String.IsNullOrEmpty(query))
                    {
                        query += " AND ";
                    }

                    query += "FirmDirectoryLookupTableDaily__cai.FId = " + SingleFirmID;
                }
                if(ListOfFirmsToSelect.Count > 0)
                {
                    if (!String.IsNullOrEmpty(query))
                    {
                        query += " AND ";
                    }

                    query += "FirmDirectoryLookupTableDaily__cai.FId IN (" + String.Join<int>(",", ListOfFirmsToSelect) + ")";
                }
                // ---

                if (!String.IsNullOrEmpty(query))
                {
                    query = " WHERE " + query;
                }

                return query;
            }

            private String BuildComplexQueryFromListForColumn(List<String> items, String columnName)
            {
                var query = String.Empty;
                var checkOperator = " OR ";
                foreach (var item in items)
                {
                    var itemCleaned = item.Replace("'", "''").ToLower();
                    var column = "LOWER(" + columnName + ")";
                    var likeStr = column + " LIKE '{0}'";
                    var sQ = String.Format(likeStr, itemCleaned) + " OR " +
                        String.Format(likeStr, "%," + itemCleaned) + " OR " +
                        String.Format(likeStr, "" + itemCleaned + ",%") + " OR " +
                        String.Format(likeStr, "%," + itemCleaned + ",%") + " OR " +
                        // same stuff but with spaces before each value
                        String.Format(likeStr, "%, " + itemCleaned) + " OR " +
                        String.Format(likeStr, "" + itemCleaned + " ,%") + " OR " +
                        String.Format(likeStr, "%, " + itemCleaned + " ,%") + " OR " +
                        String.Format(likeStr, "%," + itemCleaned + " ,%") + " OR " +
                        String.Format(likeStr, "%, " + itemCleaned + ",%");

                    if (!String.IsNullOrEmpty(query))
                    {
                        query += checkOperator;
                    }

                    query += "(" + sQ + ")";
                }

                return query;
            }

            public String GetQueryString(int page = -1)
            {
                String query = String.Empty;
                
                if (this.countryId > 0)
                {
                    if (!String.IsNullOrEmpty(query)) { query += "&"; }
                    query += "country=" + this.countryId;
                }

                if (!String.IsNullOrEmpty(this.city))
                {
                    if (!String.IsNullOrEmpty(query)) { query += "&"; }
                    query += "city=" + this.city;
                }

                if (!String.IsNullOrEmpty(this.firmName))
                {
                    if (!String.IsNullOrEmpty(query)) { query += "&"; }
                    query += "firmName=" + this.firmName;
                }

                if (this.sectors != null && this.sectors.Count > 0)
                {
                    if (!String.IsNullOrEmpty(query)) { query += "&"; }
                    query += "sectors=" + string.Join(",", this.sectors);
                }

                if (this.specialisms != null && this.specialisms.Count > 0)
                {
                    if (!String.IsNullOrEmpty(query)) { query += "&"; }
                    query += "specialisms=" + string.Join(",", this.specialisms);
                }

                if (this.authorisations != null && this.authorisations.Count > 0)
                {
                    if (!String.IsNullOrEmpty(query)) { query += "&"; }
                    query += "authorisations=" + string.Join(",", this.authorisations);
                }

                if(page != -1)
                {
                    if (!String.IsNullOrEmpty(query)) { query += "&"; }
                    query += "page=" + page;
                }

                return query;
            }
        }

        private FirmSearchParams? CurrentFirmSearchParams
        {
            get
            {
                var country = Request.QueryString["country"];
                var city = Request.QueryString["city"];
                var firmName = Request.QueryString["firmName"];
                var sectors = Request.QueryString["sectors"];
                var specialisms = Request.QueryString["specialisms"];
                var authorisations = Request.QueryString["authorisations"];
                var currentPage = Request.QueryString["page"];

                var prms = new FirmSearchParams(country, city, firmName, sectors, specialisms, authorisations, currentPage);

                return prms;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.EnableViewState = true;

                // checking if type of search exists
                if (CurrentSearchType == SearchType.Firm)
                {
                    // binding dropdowns
                    this.BindCountryList();
                    this.BindIndustrySectors();
                    this.BindSpecialisms();
                    this.BindAuthorisations();

                    if (CurrentFirmSearchParams.HasValue && !String.IsNullOrEmpty(CurrentFirmSearchParams.Value.firmName))
                    {
                        idFirmName.Text = CurrentFirmSearchParams.Value.firmName;
                    }
                    if (CurrentFirmSearchParams.HasValue && !String.IsNullOrEmpty(CurrentFirmSearchParams.Value.city))
                    {
                        idFirmCity.Text = CurrentFirmSearchParams.Value.city;
                    }

                    this.PerformFirmSearch();
                }
            }
        }

        private void PerformFirmSearch()
        {
            if(CurrentFirmSearchParams.HasValue && CurrentFirmSearchParams.Value.HasOneValue())
            {
                pnlFirmSearchResults.Visible = true;

                // lets perform search for general list of firms
                var dt = CurrentFirmSearchParams.Value.BuildSearchQuery();
                if (dt.Rows.Count > 0)
                {
                    var data = new List<EnhancedListingObj>();
                    foreach(DataRow row in dt.Rows)
                    {
                        int listingParentId = -1;
                        int.TryParse(row["ParentID"].ToString(), out listingParentId);

                        data.Add(new EnhancedListingObj()
                        {
                            Title = row["FirmName"] != null ? row["FirmName"].ToString() : String.Empty,
                            City = row["City"] != null && !String.IsNullOrEmpty(row["City"].ToString()) ? row["City"].ToString() : (row["County"] != null ? row["County"].ToString() : ""),
                            Phone = dt.Columns.Contains("ContactNo") && row["ContactNo"] != null ? row["ContactNo"].ToString() : String.Empty,
                            Url = row["WebSite"] != null ? row["WebSite"].ToString() : String.Empty,
                            ID = row["FId"] != null ? row["FId"].ToString() : String.Empty,
                            ParentID = listingParentId
                        });
                    }

                    FillDataFromParentFirms(data);

                    firmGeneralSearchRepeater.DataSource = data;
                    firmGeneralSearchRepeater.DataBind();

                    // binding pagination
                    // previous disabled current next
                    dt = CurrentFirmSearchParams.Value.BuildCountQuery();
                    if(dt.Rows.Count > 0)
                    {
                        DataRow dtr = dt.Rows[0];
                        int totalRecords = int.Parse(dtr["TotalRecords"].ToString());
                        // we will be generating:
                        // Previous 1 ... 4 5 6 ... lastPage Next
                        // rule is to generate first, last and three pages from current page
                        FirmSearchParams prms = CurrentFirmSearchParams.Value;
                        List<Pagination> pagination = new List<Pagination>();
                        var lastPageNumber = (int)Math.Ceiling(((double)totalRecords) / firmSearchPageSize);
                        var previous = new Pagination()
                        {
                            CssClass = "previous" + (prms.currentPage == 1 ? " disabled" : ""),
                            Text = "Previous",
                            Page = (prms.currentPage > 1 ? (prms.currentPage - 1).ToString() : "0"),
                            Url = (prms.currentPage > 1 ? "?" + prms.GetQueryString(prms.currentPage - 1) : "#")
                        };
                        var firstPage = new Pagination()
                        {
                            CssClass = (prms.currentPage == 1 ? "current" : ""),
                            Text = "1",
                            Page = "1",
                            Url = "?" + prms.GetQueryString(1)
                        };
                        var lastPage = new Pagination()
                        {
                            CssClass = (prms.currentPage == lastPageNumber ? "current" : ""),
                            Text = lastPageNumber.ToString(),
                            Page = lastPageNumber.ToString(),
                            Url = "?" + prms.GetQueryString(lastPageNumber)
                        };
                        var next = new Pagination()
                        {
                            CssClass = "next" + (prms.currentPage == lastPageNumber ? " disabled" : ""),
                            Text = "Next",
                            Page = (prms.currentPage < lastPageNumber ? (prms.currentPage + 1).ToString() : "0"),
                            Url = (prms.currentPage < lastPageNumber ? "?" + prms.GetQueryString(prms.currentPage + 1) : "#")
                        };

                        List<int> pagesToAdd = new List<int>();

                        // checking if we are at the begining
                        if (prms.currentPage - 3 <= 1)
                        {
                            if(lastPageNumber > 2)
                            {
                                pagesToAdd.Add(2);
                            }
                            if (lastPageNumber > 3)
                            {
                                pagesToAdd.Add(3);
                            }
                            if (lastPageNumber > 4)
                            {
                                pagesToAdd.Add(4);
                            }
                            if (lastPageNumber > 5)
                            {
                                pagesToAdd.Add(5);
                            }
                            if(lastPageNumber >= 7)
                            {
                                pagesToAdd.Add(-1);
                            }
                        }
                        // checking if we are at the end
                        else if (prms.currentPage >= lastPageNumber - 3)
                        {
                            if (lastPageNumber >= 7)
                            {
                                pagesToAdd.Add(-1);
                            }
                            if(lastPageNumber - 4 > 1)
                            {
                                pagesToAdd.Add(lastPageNumber - 4);
                            }
                            if (lastPageNumber - 3 > 1)
                            {
                                pagesToAdd.Add(lastPageNumber - 3);
                            }
                            if (lastPageNumber - 2 > 1)
                            {
                                pagesToAdd.Add(lastPageNumber - 2);
                            }
                            if (lastPageNumber - 1 > 1)
                            {
                                pagesToAdd.Add(lastPageNumber - 1);
                            }
                        } else
                        {
                            pagesToAdd.Add(-1);
                            pagesToAdd.Add(prms.currentPage - 1);
                            pagesToAdd.Add(prms.currentPage);
                            pagesToAdd.Add(prms.currentPage + 1);
                            pagesToAdd.Add(-1);
                        }

                        if(prms.currentPage != 1)
                        {
                            pagination.Add(previous);
                        }

                        pagination.Add(firstPage);

                        foreach (var p in pagesToAdd)
                        {
                            pagination.Add(new Pagination()
                            {
                                CssClass = prms.currentPage == p ? "current" : "",
                                Text = p == -1 ? "..." : p.ToString(),
                                Page = p.ToString(),
                                Url = (p == -1 ? "#" : "?" + prms.GetQueryString(p))
                            });
                        }

                        if(lastPageNumber != 1)
                        {
                            pagination.Add(lastPage);
                        }
                        if(lastPageNumber != prms.currentPage)
                        {
                            pagination.Add(next);
                        }

                        // checking if pagination has more than 1 element
                        if(pagination.Count > 1)
                        {
                            firmSearchPagination.DataSource = pagination;
                        } else
                        {
                            firmSearchPagination.DataSource = new List<Pagination>();
                        }

                        firmSearchPagination.DataBind();
                    }
                }
                else
                {
                    // no results found
                    pnlFirmSearchResults.Visible = false;
                }

                // checking if search panel is visible
                if(pnlFirmSearchResults.Visible)
                {
                    dt = null;

                    // lets bind enhanced search results
                    if(CurrentFirmSearchParams.Value.currentPage > 1)
                    {
                        // lets check if we have performed search previously and have results of top search in cookies
                        if(caiPremiumResults.CurrentSearchListing.Count == 0)
                        {
                            dt = CurrentFirmSearchParams.Value.BuildSearchQueryTopListing();
                        }
                    } else
                    {
                        dt = CurrentFirmSearchParams.Value.BuildSearchQueryTopListing();
                    }

                    if (dt != null)
                    {
                        if (dt.Rows.Count > 0)
                        {
                            var data = new List<EnhancedListingObj>();
                            var parentIds = new List<int>();
                            foreach (DataRow row in dt.Rows)
                            {
                                int listingParentId = -1;
                                int.TryParse(row["ParentID"].ToString(), out listingParentId);

                                data.Add(new EnhancedListingObj()
                                {
                                    Image = row["LogoURL"] != null ? row["LogoURL"].ToString() : String.Empty,
                                    Title = row["FirmName"] != null ? row["FirmName"].ToString() : String.Empty,
                                    City = row["City"] != null && !String.IsNullOrEmpty(row["City"].ToString()) ? row["City"].ToString() : (row["County"] != null ? row["County"].ToString() : ""),
                                    Phone = dt.Columns.Contains("ContactNo") && row["ContactNo"] != null ? row["ContactNo"].ToString() : String.Empty,
                                    Url = row["WebSite"] != null ? row["WebSite"].ToString() : String.Empty,
                                    ID = row["FId"] != null ? row["FId"].ToString() : String.Empty,
                                    ParentID = listingParentId
                                });

                                if (data.Count == 3)
                                {
                                    break;
                                }
                            }
                            
                            FillDataFromParentFirms(data, true);

                            caiPremiumResults.Results = data;
                        }
                    } else
                    {
                        if (caiPremiumResults.CurrentSearchListing.Count > 0)
                        {
                            caiPremiumResults.Results = caiPremiumResults.CurrentSearchListing;
                        }
                    }

                    caiPremiumResults.LoadSearchResults();
                    // ----
                }
            }
        }

        private void FillDataFromParentFirms(List<EnhancedListingObj> data, bool ehancedListingSearch = false)
        {
            List<int> parentIds = new List<int>();

            foreach(var obj in data)
            {
                if (obj.ParentID > 0 && !parentIds.Contains(obj.ParentID))
                {
                    parentIds.Add(obj.ParentID);
                }
            }

            if (parentIds.Count > 0)
            {
                var prmsParents = new FirmSearchParams(0);
                prmsParents.SingleFirmID = 0;
                prmsParents.ListOfFirmsToSelect = parentIds;
                DataTable dt2 = null;

                if(ehancedListingSearch)
                {
                    dt2 = prmsParents.BuildSearchQueryTopListing();
                } else
                {
                    dt2 = prmsParents.BuildSearchQuery();
                }
                
                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        // find all firms with this parent id
                        var kids = data.FindAll(a => a.ParentID == int.Parse(row2["FId"].ToString()));
                        foreach (var kidObj in kids)
                        {
                            if (String.IsNullOrEmpty(kidObj.Url) && !String.IsNullOrEmpty(row2["WebSite"].ToString()))
                            {
                                kidObj.Url = row2["WebSite"].ToString();
                            }
                            if (ehancedListingSearch && String.IsNullOrEmpty(kidObj.Image) && !String.IsNullOrEmpty(row2["LogoURL"].ToString()))
                            {
                                kidObj.Image = row2["LogoURL"].ToString();
                            }
                            // setting parent URL
                            var pObj = new EnhancedListingObj()
                            {
                                Image = row2["LogoURL"] != null ? row2["LogoURL"].ToString() : String.Empty,
                                Title = row2["FirmName"] != null ? row2["FirmName"].ToString() : String.Empty,
                                City = row2["City"] != null ? row2["City"].ToString() : (row2["County"] != null ? row2["County"].ToString() : ""),
                                Phone = dt2.Columns.Contains("ContactNo") && row2["ContactNo"] != null ? row2["ContactNo"].ToString() : String.Empty,
                                Url = row2["WebSite"] != null ? row2["WebSite"].ToString() : String.Empty,
                                ID = row2["FId"] != null ? row2["FId"].ToString() : String.Empty
                            };
                            kidObj.SinglePageUrl = pObj.SinglePageUrl;
                        }
                    }
                }
            }
        }

        private void BindCountryList()
        {
            var sql = $"{AptifyApplication.GetEntityBaseDatabase("Country")}..{spGetCountryByName__cai}";

            var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, new IDataParameter[0]);

            if (dt.Rows.Count > 0)
            {
                // lets add additional please select country to the list
                var pleaseSelectRow = dt.NewRow();
                pleaseSelectRow["CountryId"] = -1;
                pleaseSelectRow["CountryName"] = "Country";
                dt.Rows.InsertAt(pleaseSelectRow, 0);
                // -----
                List<DropdownItem> data = new List<DropdownItem>();
                foreach(DataRow row in dt.Rows)
                {
                    data.Add(new DropdownItem()
                    {
                        ID = Int32.Parse(row["CountryId"].ToString()),
                        Name = row["CountryName"].ToString()
                    });
                }
                // bind dropdown
                cmbCountrySelect.DataSource = data;
                cmbCountrySelect.DataBind();
				


				idMemCountry.DataSource = data;
				idMemCountry.DataBind();



				foreach (ListItem item in cmbCountrySelect.Items)
                {
                    if (CurrentFirmSearchParams.HasValue && CurrentFirmSearchParams.Value.countryId.ToString().Equals(item.Value.ToString()))
                    {
                        item.Selected = true;
                    }
                }
            }
        }
        private void BindIndustrySectors()
        {
            BindListBox(cmbIndustrySectors, "enhancedListingIndustryTopicCodeID", 1376, CurrentFirmSearchParams.HasValue ? CurrentFirmSearchParams.Value.sectors : null);
        }
        private void BindSpecialisms()
        {
            BindListBox(cmbSpecialisms, "enhancedListingSpecialismsTopicCodeID", 1368, CurrentFirmSearchParams.HasValue ? CurrentFirmSearchParams.Value.specialisms : null);
        }
        private void BindAuthorisations()
        {
            var data = new List<DropdownItem>();
            data.Add(new DropdownItem()
            {
                ID = (int)AuthorisationTypes.ATOL,
                Name = "ATOL"
            });
            data.Add(new DropdownItem()
            {
                ID = (int)AuthorisationTypes.AUD,
                Name = "Auditor"
            });
            data.Add(new DropdownItem()
            {
                ID = (int)AuthorisationTypes.DPB,
                Name = "Designated Professional Body"
            });
            data.Add(new DropdownItem()
            {
                ID = (int)AuthorisationTypes.IB,
                Name = "Investment Business"
            });

            cmbAuthorisations.DataSource = data;
            cmbAuthorisations.DataBind();

            if (CurrentFirmSearchParams.HasValue 
                && CurrentFirmSearchParams.Value.authorisations != null 
                && CurrentFirmSearchParams.Value.authorisations.Count > 0)
            {
                foreach (ListItem item in cmbAuthorisations.Items)
                {
                    if (CurrentFirmSearchParams.Value.authorisations.Contains(int.Parse(item.Value)))
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        private void BindListBox(ListBox listBox, String key, int defaultValueForKey, List<String> preselection)
        {
            String _keyVal = ConfigurationManager.AppSettings[key];
            if(!String.IsNullOrEmpty(_keyVal))
            {
                Int32.TryParse(_keyVal, out defaultValueForKey);
            }

            var parameters = new List<IDataParameter>();
            var sql = $"{AptifyApplication.GetEntityBaseDatabase("TopicCode")}..{spGetTopicCodeByParentID__c}";
            parameters.Add(DataAction.GetDataParameter("@ParentID", SqlDbType.Int, defaultValueForKey));
            parameters.Add(DataAction.GetDataParameter("@OrganizationID", SqlDbType.Int, -1));

            var dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());
            var data = new List<DropdownItem>();
            foreach(DataRow row in dt.Rows)
            {
                data.Add(new DropdownItem()
                {
                    ID = Int32.Parse(row["ID"].ToString()),
                    Name = row["Name"].ToString()
                });
            }
            listBox.DataSource = data;
            listBox.DataBind();
            
            if(preselection != null && preselection.Count > 0)
            {
                foreach (ListItem item in listBox.Items)
                {
                    if (preselection.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        public class DropdownItem
        {
            public int ID { get; set; }
            public String Name { get; set; }
        }

        public enum AuthorisationTypes
        {
            AUD = 1,
            DPB = 2,
            IB = 3,
            ATOL = 4
        }

        public class Pagination
        {
            public String CssClass { get; set; }
            public String Page { get; set; }
            public String Text { get; set; }
            public String Url { get; set; }
        }
    }
}
