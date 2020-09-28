using Aptify.Framework.DataServices;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using static SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedFirmSearchResults;

namespace SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch
{
    public partial class EnhancedFirmDetails : Aptify.Framework.Web.eBusiness.BaseUserControlAdvanced
    {
        private static string spGetFirmPrincipalRoles__cai = "spGetFirmPrincipalRoles__cai";
        private static string spGetFirmDetailsByFirmId__cai = "spGetFirmDetailsByFirmId__cai";
        private static string spGetFirmSubOffices__cai = "spGetFirmSubOffices__cai";
        private static string spGetTradingNamesByCompanyId__cai = "spGetTradingNamesByCompanyId__cai";

        protected void Page_Load(object sender, EventArgs e)
        {
            // checking if in design mode, skip the rest of the code
            if(this.IsDesignMode())
            {
                return;
            }

            if (!IsPostBack)
            {
                String noFirmUrl = "/";
                int fId = -1;
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["enhancedListingSearchFirmPageURL"]))
                {
                    noFirmUrl = ConfigurationManager.AppSettings["enhancedListingSearchFirmPageURL"];
                    if (!noFirmUrl.StartsWith("/"))
                    {
                        noFirmUrl = "/" + noFirmUrl;
                    }
                }

                // check if we have valid firm
                var firm = Request.QueryString["firm"];
                var validFirm = false;
                if (!String.IsNullOrEmpty(firm))
                {
                    // checking if last part of firm is ID
                    var parts = firm.Split('-');
                    if (parts.Length > 1 && int.TryParse(parts.Last(), out fId))
                    {
                        // lets verify that provided firm actually exists
                        FirmSearchParams prms = new FirmSearchParams(fId);
                        // lets check if we have a single record based on search params
                        var dt = prms.BuildSearchQuery();
                        if (dt.Rows.Count > 0)
                        {
                            DataRow row = dt.Rows[0];

                            // lets verify if has Parent ID
                            if(dt.Columns.Contains("ParentID") && row["ParentID"] != null)
                            {
                                int listingParentId = -1;
                                int.TryParse(row["ParentID"].ToString(), out listingParentId);
                                if (listingParentId > 0)
                                {
                                    // lets find parent
                                    var p_prms = new FirmSearchParams(listingParentId);
                                    var p_dt = p_prms.BuildSearchQuery();
                                    if (p_dt.Rows.Count > 0)
                                    {
                                        var p_row = p_dt.Rows[0];
                                        var p_listingObj = new EnhancedListingObj()
                                        {
                                            Title = p_row["FirmName"] != null ? p_row["FirmName"].ToString() : String.Empty,
                                            City = p_row["City"] != null && !String.IsNullOrEmpty(row["City"].ToString()) ? p_row["City"].ToString() : (p_row["County"] != null ? p_row["County"].ToString() : ""),
                                            Phone = p_dt.Columns.Contains("ContactNo") && p_row["ContactNo"] != null ? p_row["ContactNo"].ToString() : String.Empty,
                                            Url = p_row["WebSite"] != null ? p_row["WebSite"].ToString() : String.Empty,
                                            ID = p_row["FId"] != null ? p_row["FId"].ToString() : String.Empty
                                        };

                                        Response.Redirect(p_listingObj.SinglePageUrl);
                                        return;
                                    }
                                }
                            }
                            // -----

                            // lets verify if enhanced feature
                            dt = prms.BuildSearchQueryTopListing();
                            bool isEnhancedListing = dt.Rows.Count > 0;
                            if (isEnhancedListing)
                            {
                                row = dt.Rows[0];
                            }
                            // ----

                            // lets verify that firm key is same as the one used on search page
                            var listingObj = new EnhancedListingObj()
                            {
                                Title = row["FirmName"] != null ? row["FirmName"].ToString() : String.Empty,
                                City = row["City"] != null && !String.IsNullOrEmpty(row["City"].ToString()) ? row["City"].ToString() : (row["County"] != null ? row["County"].ToString() : ""),
                                Phone = dt.Columns.Contains("ContactNo") && row["ContactNo"] != null ? row["ContactNo"].ToString() : String.Empty,
                                Url = row["WebSite"] != null ? row["WebSite"].ToString() : (row["ParentWebSite"] != null ? row["ParentWebSite"].ToString() : String.Empty),
                                ID = row["FId"] != null ? row["FId"].ToString() : String.Empty
                            };

                            if (listingObj.SinglePageUrl.EndsWith(firm))
                            {
                                Page.Title = listingObj.Title;

                                firmName.InnerHtml = row["FirmName"].ToString();

                                // retrieve trading names for company
                                var parameters = new List<IDataParameter>();
                                var sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetTradingNamesByCompanyId__cai}";
                                parameters.Add(DataAction.GetDataParameter("@cid", SqlDbType.Int, fId));
                                dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());
                                String tradingNamesTxt = String.Empty;
                                if (dt != null && dt.Rows.Count > 0)
                                {
                                    foreach(DataRow tdRow in dt.Rows)
                                    {
                                        if(!String.IsNullOrEmpty(tradingNamesTxt))
                                        {
                                            tradingNamesTxt += "<br/>";
                                        }
                                        tradingNamesTxt += tdRow["TradingName"].ToString();
                                    }
                                }
                                // ---

                                firmTradingName.InnerHtml = tradingNamesTxt;
                                if (String.IsNullOrEmpty(firmTradingName.InnerText))
                                {
                                    firmTradingName.Visible = false;
                                }

                                if (isEnhancedListing && row["FirmDescription"] != null && !String.IsNullOrEmpty(row["FirmDescription"].ToString()))
                                {
                                    firmDescription.Visible = true;
                                    firmDescription.InnerHtml = row["FirmDescription"].ToString();
                                }
                                else
                                {
                                    firmDescription.Visible = false;
                                }

                                // Firm Details
                                parameters = new List<IDataParameter>();
                                parameters.Add(DataAction.GetDataParameter("@id", SqlDbType.Int, fId));
                                sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetFirmDetailsByFirmId__cai}";
                                dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());
                                DataRow firmRow = null;
                                if (dt.Rows.Count > 0)
                                {
                                    DataRow r = dt.Rows[0];
                                    firmRow = r;
                                    //line1, line2, line3, line4, city, county, postcode, country
                                    primaryAddess.InnerText = AddressToString(r);

                                    String phoneNumber = r["areacode"].ToString() + r.Field<string>("phoneno");
                                    if (String.IsNullOrEmpty(phoneNumber) || String.IsNullOrEmpty(phoneNumber.Trim()))
                                    {
                                        phoneHolder.Visible = false;
                                    }
                                    else
                                    {
                                        phoneHolder.Visible = true;
                                        primaryPhone.InnerText = phoneNumber;
                                    }
                                    String faxNumber = r["faxno"].ToString();
                                    if (String.IsNullOrEmpty(faxNumber) || String.IsNullOrEmpty(faxNumber.Trim()))
                                    {
                                        faxHolder.Visible = false;
                                    }
                                    else
                                    {
                                        faxHolder.Visible = true;
                                        primaryFax.InnerText = faxNumber;
                                    }
                                    String mainemail = r["mainemail"].ToString();
                                    if (String.IsNullOrEmpty(mainemail) || String.IsNullOrEmpty(mainemail.Trim()))
                                    {
                                        emailHolder.Visible = false;
                                    }
                                    else
                                    {
                                        emailHolder.Visible = true;
                                        primaryEmail.InnerText = mainemail;
                                        primaryEmail.Attributes["href"] = "mailto:" + mainemail;
                                    }

                                    if (String.IsNullOrEmpty(row["WebSite"].ToString()) || String.IsNullOrEmpty(row["WebSite"].ToString()))
                                    {
                                        websiteHolder.Visible = false;
                                    }
                                    else
                                    {
                                        websiteHolder.Visible = true;
                                        primaryWebsite.InnerText = row["WebSite"].ToString();
                                        primaryWebsite.Attributes["href"] = listingObj.Url;
                                    }
                                }

                                // checking Authorisation flags
                                String atolStr = row["ATOL"] != null ? row["ATOL"].ToString() : "";
                                String audStr = row["AUD"] != null ? row["AUD"].ToString() : "";
                                String dpbStr = row["DPB"] != null ? row["DPB"].ToString() : "";
                                String ibStr = row["IB"] != null ? row["IB"].ToString() : "";

                                String authorisations = "";
                                if (!string.IsNullOrEmpty(audStr) && audStr.Equals("AUD"))
                                {
                                    if (!String.IsNullOrEmpty(authorisations))
                                    {
                                        authorisations += ", ";
                                    }
                                    authorisations += "Auditor";
                                }
                                if (!string.IsNullOrEmpty(dpbStr) && dpbStr.Equals("DPB"))
                                {
                                    if (!String.IsNullOrEmpty(authorisations))
                                    {
                                        authorisations += ", ";
                                    }
                                    authorisations += "Designated Professional Body";
                                }
                                if (!string.IsNullOrEmpty(ibStr) && ibStr.Equals("IB"))
                                {
                                    if (!String.IsNullOrEmpty(authorisations))
                                    {
                                        authorisations += ", ";
                                    }
                                    authorisations += "Investment Business";

                                    if(firmRow != null)
                                    {
                                        if(!String.IsNullOrEmpty(firmRow["product"].ToString()) && !firmRow["product"].ToString().Equals("NO"))
                                        {
                                            authorisations += " IIA-" + firmRow["product"].ToString();
                                        }
                                    }
                                }
                                if (!string.IsNullOrEmpty(atolStr) && atolStr.Equals("ATOL"))
                                {
                                    if (!String.IsNullOrEmpty(authorisations))
                                    {
                                        authorisations += ", ";
                                    }
                                    authorisations += "ATOL";
                                }
                                if (String.IsNullOrEmpty(authorisations))
                                {
                                    firmAuthorisationsHolder.Visible = false;
                                }
                                else
                                {
                                    firmAuthorisations.InnerText = authorisations;
                                }
                                // --------

                                if (isEnhancedListing && row["NoOfEmployees"] != null && !String.IsNullOrEmpty(row["NoOfEmployees"].ToString()))
                                {
                                    firmEmployeesHolder.Visible = true;
                                    firmEmployees.InnerText = row["NoOfEmployees"].ToString();
                                }
                                else
                                {
                                    firmEmployeesHolder.Visible = false;
                                }

                                if (isEnhancedListing && row["NoOfPartners"] != null && !String.IsNullOrEmpty(row["NoOfPartners"].ToString()))
                                {
                                    firmPartnersHolder.Visible = true;
                                    firmPartners.InnerText = row["NoOfPartners"].ToString();
                                }
                                else
                                {
                                    firmPartnersHolder.Visible = false;
                                }

                                if (isEnhancedListing && row["LogoURL"] != null && !String.IsNullOrEmpty(row["LogoURL"].ToString()))
                                {
                                    firmLogo.Visible = true;
                                    firmLogo.Attributes["src"] = row["LogoURL"].ToString();
                                }
                                else
                                {
                                    firmLogo.Visible = false;
                                }

                                // Principals
                                parameters = new List<IDataParameter>();
                                parameters.Add(DataAction.GetDataParameter("@id", SqlDbType.Int, fId));
                                sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetFirmPrincipalRoles__cai}";
                                dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());
                                var principles = new List<String>();
                                foreach (DataRow r in dt.Rows)
                                {
                                    principles.Add(r["pname"].ToString());
                                }
                                if (principles.Count > 0)
                                {
                                    frmPrincipalsRepeaterHolder.Visible = true;
                                    frmPrincipalsRepeater.DataSource = principles;
                                    frmPrincipalsRepeater.DataBind();
                                }
                                else
                                {
                                    frmPrincipalsRepeaterHolder.Visible = false;
                                }
                                // ------

                                // checking if google map link exists
                                String locationURL = row["LocationURL"].ToString();
                                if (!isEnhancedListing || String.IsNullOrEmpty(locationURL))
                                {
                                    primaryMapLinkHolder.Visible = false;
                                }
                                else
                                {
                                    primaryMapLinkHolder.Visible = true;
                                    primaryMapLink.Attributes["href"] = locationURL;
                                }

                                if (!isEnhancedListing || String.IsNullOrEmpty(row["Specialisms"].ToString()))
                                {
                                    primarySpecialismHolder.Visible = false;
                                }
                                else
                                {
                                    primarySpecialismHolder.Visible = true;
                                    String specialisms = row["Specialisms"].ToString();
                                    specialisms = specialisms.Replace(",", ", ");
                                    specialisms = specialisms.Replace("   ", " ").Replace("  ", " ");
                                    primarySpecialism.InnerText = specialisms;
                                }

                                if (!isEnhancedListing || String.IsNullOrEmpty(row["IndustrySector"].ToString()))
                                {
                                    primarySectorHolder.Visible = false;
                                }
                                else
                                {
                                    primarySectorHolder.Visible = true;
                                    String industrySector = row["IndustrySector"].ToString();
                                    industrySector = industrySector.Replace(",", ", ");
                                    industrySector = industrySector.Replace("   ", " ").Replace("  ", " ");
                                    primarySector.InnerText = industrySector;
                                }
                                // ------

                                // TODO - Sub-offices Firms
                                parameters = new List<IDataParameter>();
                                parameters.Add(DataAction.GetDataParameter("@id", SqlDbType.Int, fId));
                                sql = $"{AptifyApplication.GetEntityBaseDatabase("Firm")}..{spGetFirmSubOffices__cai}";
                                dt = DataAction.GetDataTableParametrized(sql, CommandType.StoredProcedure, parameters.ToArray());
                                if (dt.Rows.Count > 0)
                                {
                                    // lets retrieve values if enhanced listing on any sub offices
                                    List<int> listOfSubOffices = new List<int>();
                                    foreach (DataRow r in dt.Rows)
                                    {
                                        listOfSubOffices.Add(int.Parse(r["id"].ToString()));
                                    }

                                    prms.SingleFirmID = 0;
                                    prms.ListOfFirmsToSelect = listOfSubOffices;

                                    var dt2 = prms.BuildSearchQueryTopListing();
                                    List<SubOfficeDisplay> subOffices = new List<SubOfficeDisplay>();
                                    foreach (DataRow standard_row in dt.Rows)
                                    {
                                        var subOffice = new SubOfficeDisplay();
                                        subOffice.standard_row = standard_row;
                                        foreach (DataRow premium_row in dt2.Rows)
                                        {
                                            if(standard_row["id"].ToString().Equals(premium_row["FId"].ToString()))
                                            {
                                                subOffice.premium_row = premium_row;
                                                break;
                                            }
                                        }

                                        subOffices.Add(subOffice);
                                    }

                                    subOfficeRepeater.Visible = true;
                                    subOfficeRepeater.DataSource = subOffices;
                                    subOfficeRepeater.DataBind();
                                }
                                else
                                {
                                    subOfficeRepeater.Visible = false;
                                }
                                // ------

                                validFirm = true;
                            }
                            // ----
                        }
                    }
                }

                if (!validFirm)
                {
                    Response.Redirect(noFirmUrl);
                    return;
                }
            }
        }

        private String AddressToString(DataRow row)
        {
            return AddressToString(
                row["line1"].ToString(),
                row["line2"].ToString(),
                row["line3"].ToString(),
                row["line4"].ToString(),
                row["city"].ToString(),
                row["county"].ToString(),
                row["postcode"].ToString(),
                row["country"].ToString()
            );
        }
        private String AddressToString(String line1, String line2, String line3, String line4, String city, String county, String postcode, String country)
        {
            String addr = String.Empty;
            addr = AddCommaToStringIfNotNull(addr, line1);
            addr = AddCommaToStringIfNotNull(addr, line2);
            addr = AddCommaToStringIfNotNull(addr, line3);
            addr = AddCommaToStringIfNotNull(addr, line4);
            addr = AddCommaToStringIfNotNull(addr, city);
            addr = AddCommaToStringIfNotNull(addr, county);
            addr = AddCommaToStringIfNotNull(addr, postcode);
            addr = AddCommaToStringIfNotNull(addr, country);

            if (!String.IsNullOrEmpty(addr))
            {
                addr += ".";
            }

            return addr;
        }

        private String AddCommaToStringIfNotNull(String original, String toAdd)
        {
            return String.IsNullOrEmpty(toAdd.Trim()) ? original : original + (!String.IsNullOrEmpty(original) ? ", " : "") + toAdd.Trim();
        }

        protected void subOfficeRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                SubOfficeDisplay row = (SubOfficeDisplay)e.Item.DataItem;
                bool isEnhancedListing = row.premium_row != null;

                //line1, line2, line3, line4, city, county, postcode, country
                ((HtmlGenericControl)e.Item.FindControl("primaryAddess")).InnerText = AddressToString(row.standard_row);

                String phoneNumber = row.standard_row["areacode"].ToString() + row.standard_row.Field<string>("phoneno");
                if (String.IsNullOrEmpty(phoneNumber) || String.IsNullOrEmpty(phoneNumber.Trim()))
                {
                    ((HtmlGenericControl)e.Item.FindControl("phoneHolder")).Visible = false;
                }
                else
                {
                    ((HtmlGenericControl)e.Item.FindControl("phoneHolder")).Visible = true;
                    ((HtmlGenericControl)e.Item.FindControl("primaryPhone")).InnerText = phoneNumber;
                }

                String faxNumber = row.standard_row["faxno"].ToString();
                if (String.IsNullOrEmpty(faxNumber) || String.IsNullOrEmpty(faxNumber.Trim()))
                {
                    ((HtmlGenericControl)e.Item.FindControl("faxHolder")).Visible = false;
                }
                else
                {
                    ((HtmlGenericControl)e.Item.FindControl("faxHolder")).Visible = true;
                    ((HtmlGenericControl)e.Item.FindControl("primaryFax")).InnerText = faxNumber;
                }

                String mainemail = row.standard_row["mainemail"].ToString();
                if (String.IsNullOrEmpty(mainemail) || String.IsNullOrEmpty(mainemail.Trim()))
                {
                    ((HtmlGenericControl)e.Item.FindControl("emailHolder")).Visible = false;
                }
                else
                {
                    ((HtmlGenericControl)e.Item.FindControl("emailHolder")).Visible = true;
                    ((HtmlAnchor)e.Item.FindControl("primaryEmail")).InnerText = mainemail;
                    ((HtmlAnchor)e.Item.FindControl("primaryEmail")).Attributes["href"] = "mailto:" + mainemail;
                }

                if (String.IsNullOrEmpty(row.standard_row["website"].ToString()) || String.IsNullOrEmpty(row.standard_row["website"].ToString().Trim()))
                {
                    ((HtmlGenericControl)e.Item.FindControl("websiteHolder")).Visible = false;
                }
                else
                {
                    var listingObj = new EnhancedListingObj()
                    {
                        Url = row.standard_row["website"] != null ? row.standard_row["website"].ToString() : String.Empty
                    };

                    ((HtmlGenericControl)e.Item.FindControl("websiteHolder")).Visible = true;
                    ((HtmlAnchor)e.Item.FindControl("primaryWebsite")).InnerText = row.standard_row["WebSite"].ToString();
                    ((HtmlAnchor)e.Item.FindControl("primaryWebsite")).Attributes["href"] = listingObj.Url;
                }
                
                // checking if google map link exists
                String locationURL = String.Empty;
                if (!isEnhancedListing || String.IsNullOrEmpty(locationURL = row.premium_row["LocationURL"].ToString()))
                {
                    ((HtmlGenericControl)e.Item.FindControl("primaryMapLinkHolder")).Visible = false;
                }
                else
                {
                    ((HtmlGenericControl)e.Item.FindControl("primaryMapLinkHolder")).Visible = true;
                    ((HtmlAnchor)e.Item.FindControl("primaryMapLink")).Attributes["href"] = locationURL;
                }

                if (!isEnhancedListing || String.IsNullOrEmpty(row.premium_row["Specialisms"].ToString()))
                {
                    ((HtmlGenericControl)e.Item.FindControl("primarySpecialismHolder")).Visible = false;
                }
                else
                {
                    ((HtmlGenericControl)e.Item.FindControl("primarySpecialismHolder")).Visible = true;
                    String specialisms = row.premium_row["Specialisms"].ToString();
                    specialisms = specialisms.Replace(",", ", ");
                    specialisms = specialisms.Replace("   ", " ").Replace("  ", " ");
                    ((HtmlGenericControl)e.Item.FindControl("primarySpecialism")).InnerText = specialisms;
                }

                if (!isEnhancedListing || String.IsNullOrEmpty(row.premium_row["IndustrySector"].ToString()))
                {
                    ((HtmlGenericControl)e.Item.FindControl("primarySectorHolder")).Visible = false;
                }
                else
                {
                    ((HtmlGenericControl)e.Item.FindControl("primarySectorHolder")).Visible = true;
                    String industrySector = row.premium_row["IndustrySector"].ToString();
                    industrySector = industrySector.Replace(",", ", ");
                    industrySector = industrySector.Replace("   ", " ").Replace("  ", " ");
                    ((HtmlGenericControl)e.Item.FindControl("primarySector")).InnerText = industrySector;
                }
            }
        }

        public struct SubOfficeDisplay
        {
            public DataRow standard_row;
            public DataRow premium_row;
        }
    }
}
