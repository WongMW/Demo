<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EnhancedFirmDetails.ascx.cs" Inherits="SitefinityWebApp.UserControls.CAI_Custom_Controls.FirmsSearch.EnhancedFirmDetails" %>
<%-- bootstrap  --%>
<!-- Latest compiled and minified CSS -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" integrity="sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u" crossorigin="anonymous"/>
<!-- Optional theme -->
<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap-theme.min.css" integrity="sha384-rHyoN1iRsVXV4nD0JutlnGaslCJuC7uwjduW9SVrLvRYooPp2bWYgmgJQIXwl/Sp" crossorigin="anonymous"/>
<!-- Override bootstrap css for corporate styles -->
<link href="../../../CSS/bootstrap-override.min.css" rel="stylesheet" />
<!-- Multiselect dropdown files -->
<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/jquery.sumoselect/3.0.2/sumoselect.min.css">
<script src="https://cdnjs.cloudflare.com/ajax/libs/jquery.sumoselect/3.0.2/jquery.sumoselect.min.js"></script>
<div class="raDiv" style="overflow: visible;">
    <asp:UpdateProgress ID="updateProcessingIndicator" runat="server" DisplayAfter="0" AssociatedUpdatePanelID="PanelEnhancedFirmDetails">
        <ProgressTemplate>
            <div class="dvProcessing">
                <div class="loading-bg">
                    <img src="/Images/CAITheme/bx_loader.gif" />
                    <span>LOADING...<br /><br />Please do not leave or close this window while payment is processing.</span>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>
</div>
<asp:UpdatePanel ID="PanelEnhancedFirmDetails" runat="server" >
    <ContentTemplate>
        <div class="container-fluid firm-details">
            <div class="row">
                <div class="col-xs-12 col-md-9">
                    <h1 id="firmName" runat="server" class="schema-firm-name">PWC Price waterhouse Coopers</h1>
                    <h2 id="firmTradingName" runat="server" class="schema-firm-trade-name">N/A</h2><%-- Don't show this field if it's empty/if no trading name exists --%>
                    <%-- START ENHANCED FEATURE ONLY BELOW --%>
                    <p id="firmDescription" runat="server" class="schema-firm-description">N/A</p>
                    <%-- END ENHANCED FEATURE ONLY ABOVE --%>
                    <div runat="server" id="firmAuthorisationsHolder">
                        <span class="grey-subtitle">Authorisations:</span>
                        <span class="subtitle" id="firmAuthorisations" runat="server">N/A</span>
                    </div>
                    <%-- START ENHANCED FEATURE ONLY BELOW --%>
                    <div runat="server" id="firmEmployeesHolder">
                        <span class="grey-subtitle">No. of employees:</span>
                        <span id="firmEmployees" runat="server" class="subtitle schema-num-employees">N/A</span>
                    </div>
                    <div runat="server" id="firmPartnersHolder">
                        <span class="grey-subtitle">No. of partners:</span>
                        <span class="subtitle" id="firmPartners" runat="server">N/A</span>
                    </div>
                    <%-- END ENHANCED FEATURE ONLY ABOVE --%>
                </div>
                <div class="col-xs-12 col-md-3 center">
                    <%-- START ENHANCED FEATURE ONLY BELOW --%>
                    <img src="/sf_images/default-source/all-logos-images/external-logos/member-benefits-logos/aa-logo.jpg" runat="server" id="firmLogo" class="firm-logo schema-firm-logo"/>
                    <%-- END ENHANCED FEATURE ONLY ABOVE --%>
                </div>
                <div class="col-xs-12 col-md-9" id="frmPrincipalsRepeaterHolder" runat="server">
                    <div class="firm-principals">
                        <div class="trigger-title">
                            <h3>View principals<span class="plus"></span></h3>
                        </div>
                        <ul class="trigger-section">
                            <%-- REPEATER START Put this in repeater to list all principals of firm --%>
                            <asp:Repeater ID="frmPrincipalsRepeater" runat="server">
                                <ItemTemplate>
                                    <li class="schema-employee-name"><%# Container.DataItem.ToString() %></li>
                                </ItemTemplate>
                            </asp:Repeater>
                            <%-- REPEATER END Put this in repeater to list all principals of firm --%>
                        </ul>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12 col-md-9">
                    <h2>Primary firm details</h2>
                    <%-- Primary details --%>
                    <div class="primary-firm-details">
                        <div class="firm-details-item">
                            <span class="grey-subtitle">Address:</span>
                            <span id="primaryAddess" runat="server" class="subtitle schema-firm-primary-address">N/A</span>
                            <span class="firms-details-map-link" id="primaryMapLinkHolder" runat="server">
                                <%-- START ENHANCED FEATURE ONLY BELOW --%>
                                <a href="https://goo.gl/maps/jPLNiBqHEWx" target="_blank" runat="server" id="primaryMapLink" class="firm-map-link schema-firm-primary-map">See Google maps</a>
                                <%-- END ENHANCED FEATURE ONLY ABOVE --%>
                            </span>
                            <div id="phoneHolder" runat="server">
                                <span class="grey-subtitle">Phone:</span>
                                <span id="primaryPhone" runat="server" class="subtitle schema-firm-primary-telephone">N/A</span>
                            </div>
                            <div id="faxHolder" runat="server">
                                <span class="grey-subtitle">Fax:</span>
                                <span id="primaryFax" runat="server" class="subtitle schema-firm-primary-fax">N/A</span>
                            </div>
                            <div id="emailHolder" runat="server">
                                <span class="grey-subtitle">Email:</span>
                                <span class="subtitle">
                                    <a href="mailto:email@email.com" id="primaryEmail" runat="server" class="schema-firm-primary-email">N/A</a>
                                </span>
                            </div>
                            <div id="websiteHolder" runat="server">
                                <span class="grey-subtitle">Website:</span>
                                <span class="subtitle">
                                    <a href="http://www.google.com" target="_blank" runat="server" id="primaryWebsite" class="schema-firm-primary-url">N/A</a>
                                </span>
                            </div>
                            <%-- START ENHANCED FEATURE ONLY --%>
                            <div runat="server" id="primarySpecialismHolder">
                                <span class="grey-subtitle">Specialisms:</span>
                                <span id="primarySpecialism" runat="server" class="subtitle schema-firm-primary-specialism">N/A</span>
                            </div>
                            <div runat="server" id="primarySectorHolder">
                                <span class="grey-subtitle">Sectors:</span>
                                <span class="subtitle" id="primarySector" runat="server">N/A</span>
                            </div>
                            <%-- END ENHANCED FEATURE ONLY --%>
                        </div>
                    </div>
                    <asp:Repeater runat="server" ID="subOfficeRepeater" OnItemDataBound="subOfficeRepeater_ItemDataBound">
                        <HeaderTemplate>
                            <h2>Other office details</h2>
                            <%-- Sub-office details --%>
                            <div class="sub-office-details">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <div class="firm-details-item">
                                <span class="grey-subtitle">Address:</span>
                                <span id="primaryAddess" runat="server" class="subtitle schema-firm-secondary-address">N/A</span>
                                <span class="firms-details-map-link" id="primaryMapLinkHolder" runat="server">
                                    <%-- START ENHANCED FEATURE ONLY BELOW --%>
                                    <a href="https://goo.gl/maps/jPLNiBqHEWx" target="_blank" runat="server" id="primaryMapLink" class="firm-map-link schema-firm-secondary-map">See Google maps</a>
                                    <%-- END ENHANCED FEATURE ONLY ABOVE --%>
                                </span>
                                <div id="phoneHolder" runat="server">
                                    <span class="grey-subtitle">Phone:</span>
                                    <span id="primaryPhone" runat="server" class="subtitle schema-firm-secondary-telephone">N/A</span>
                                </div>
                                <div id="faxHolder" runat="server">
                                    <span class="grey-subtitle">Fax:</span>
                                    <span id="primaryFax" runat="server" class="subtitle schema-firm-secondary-fax">N/A</span>
                                </div>
                                <div id="emailHolder" runat="server">
                                    <span class="grey-subtitle">Email:</span>
                                    <span class="subtitle">
                                        <a href="mailto:email@email.com" id="primaryEmail" runat="server" class="schema-firm-secondary-email">N/A</a>
                                    </span>
                                </div>
                                <div id="websiteHolder" runat="server">
                                    <span class="grey-subtitle">Website:</span>
                                    <span class="subtitle">
                                        <a href="http://www.google.com" target="_blank" runat="server" id="primaryWebsite" class="schema-firm-secondary-url">N/A</a>
                                    </span>
                                </div>
                                <%-- START ENHANCED FEATURE ONLY --%>
                                <div runat="server" id="primarySpecialismHolder">
                                    <span class="grey-subtitle">Specialisms:</span>
                                    <span id="primarySpecialism" runat="server" class="subtitle schema-firm-secondary-specialism">N/A</span>
                                </div>
                                <div runat="server" id="primarySectorHolder">
                                    <span class="grey-subtitle">Sectors:</span>
                                    <span class="subtitle" id="primarySector" runat="server">N/A</span>
                                </div>
                                <%-- END ENHANCED FEATURE ONLY --%>
                            </div>
                        </ItemTemplate>
                        <FooterTemplate>
                            </div>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <div class="row">
                <div class="col-xs-12">
                    <a href="javascript:history.back(1)" class="simple-backbtn">Back to listings</a>
                </div>
            </div>
        </div>
    </ContentTemplate>
</asp:UpdatePanel>
