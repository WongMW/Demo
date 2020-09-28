<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Custom__c/ProdCategories__c.ascx.vb"
    Inherits="Aptify.Framework.Web.eBusiness.ProductCatalog.ProdCategories__c" %>
<%@ Register TagPrefix="uc1" TagName="RecommentedProduct" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Product_Catalog/RelatedProductsGrid.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FeaturedProduct" Src="~/UserControls/SoftwareDesign_Aptify/SD_Aptify__c/FeaturedProducts.ascx" %>
<%@ Register TagPrefix="uc1" TagName="FindProduct" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_Custom__c/FindProduct__c.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ProdCategoryBar" Src="~/UserControls/SoftwareDesign_Aptify/Aptify_General/ProdCategoryBar.ascx" %>
<h1 runat="server" id="lblHeader">
    ICE Store
</h1>
 <%--dilip issue 12719 Can we change the image at top right 19/1/2012--%>
  <div>
    <asp:Image ID="ImgSideImage" Visible="false" runat="server" Width="160px" Height="140px" ImageUrl="" />
       </div>
<%--dilip issue 12719 Can we change the image at top right 19/1/2012--%>
   
<div class="sidebar-cat-nav">
    <a id="<%= ClientID %>" class="sfNavToggle">&#9776;</a>
</div>
<div id="outerDiv" class="OuterDiv  ">
    <h6 runat="server" id="lblPageHeaderText" class="main-title">
    </h6>
    <div id="ProductCategoryDiv" class="ProductCategoryDiv sfShown">
        <div class="BrowseProduct">
            <h6 runat="server" id="lblProdCatHeader" class="BrowseProduct">
            </h6>
            <div class="ProdCategory" >
             <uc1:ProdCategoryBar ID="ProdCategoryBar" runat="server" />
            </div>
        </div>
    </div>
    <div class="banner-image">
    <asp:Image ID="Image1" runat="server"  ImageUrl="" ImageAlign="Right" />
       </div>
    <div class="aptify-welcome-message">
        <h6 runat="server" id="lblWelcomeText" class="mainTitle" Visible="false">
        </h6>
	<h2>Welcome to our online store</h2>
        <br />
        <div class="sf_cols">
            <div class="sf_colsOut sf_2cols_1_67">
                <div id="baseTemplatePlaceholder_content_ctl00_ctl03_ctl03_C014_Col00" class="sf_colsIn sf_2cols_1in_67">
                    <div style="margin-top:10px;">
                        <uc1:FindProduct ID="FindProduct" HeaderText="{Find Product}" ShowHeaderIfEmpty="False" runat="server" />
                    </div>
                </div>
            </div>
            <div class="sf_colsOut sf_2cols_2_33">
                <div id="baseTemplatePlaceholder_content_ctl00_ctl03_ctl03_C014_Col01" class="sf_colsIn sf_2cols_2in_33">
                    <div class="button-block style-1">
                        <a style="text-decoration: none;" class="btn-full-width btn tooltip" href="/Event-Calendar" alt="Use the event calendar to find events by dates">CPD and events calendar</a>
                    </div>
                </div>
            </div>
        </div>
        <br />
        <div>
            <uc1:FeaturedProduct ID="FeaturedProducts" HeaderText="{Featured Product}" ShowHeaderIfEmpty="False"
                runat="server" />
        </div>
    </div>
    <div class="div">
    </div>
</div>
  <script type="text/javascript">
      (function ($) {
          $('.sfNavToggle').click(function () {
              $('.ProductCategoryDiv').toggleClass("sfShown");
          });
          
      })(jQuery);
</script>

<style>
    .RadMenu .rmHorizontal .rmSlide:before {
        content:" ";
        background-image: url("/Images/CAITheme/chartered_accounts_page_templates-08.png");
        background-position: -204px -55px !important;
        height: 16px;
        width: 16px;
        display: inline-block;
        position: absolute;
        z-index: 10;
        right: 20px;
        top: -30px;
    }


</style>