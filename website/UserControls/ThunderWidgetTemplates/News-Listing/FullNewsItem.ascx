﻿<%@ Control Language="C#" %>
<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.ContentUI" Assembly="Telerik.Sitefinity" %>
<%@ Register TagPrefix="sf" Namespace="Telerik.Sitefinity.Web.UI.PublicControls.BrowseAndEdit"
    Assembly="Telerik.Sitefinity" %>
<%@ Register Assembly="Telerik.Sitefinity" Namespace="Telerik.Sitefinity.Modules.Comments.Web.UI.Frontend" TagPrefix="comments" %>
<%@ Import Namespace="Telerik.Sitefinity.Web.UI" %>
<%@ Import Namespace="Telerik.Sitefinity.Modules.Comments" %>

<sf:SitefinityLabel id="title" runat="server" WrapperTagName="div" HideIfNoText="true" HideIfNoTextMode="Server" /> 
<telerik:RadListView ID="DetailsView" ItemPlaceholderID="ItemContainer" AllowPaging="False"
    runat="server" EnableEmbeddedSkins="false" EnableEmbeddedBaseStylesheet="false">
    <layouttemplate>
            <%-- <div class="sfnewsLinksWrp">
                <sf:MasterViewHyperLink class="sfnewsBack sfback" Text="<%$ Resources:NewsResources, AllNews %>" runat="server" />
            </div> --%>
            <asp:PlaceHolder ID="ItemContainer" runat="server"/>
    </layouttemplate>
    <itemtemplate>
       <div class="sfnewsDetails sfdetails" data-sf-provider='<%# Eval("Provider.Name")%>'  data-sf-id='<%# Eval("Id")%>' data-sf-type="Telerik.Sitefinity.News.Model.NewsItem">
        <h1 class="sfnewsTitle sftitle" data-sf-field="Title" data-sf-ftype="ShortText">
            <asp:Literal Text='<%# Eval("Title") %>' runat="server" />
        </h1>
        <comments:CommentsAverageRatingControl runat="server" 
            ThreadKey='<%# ControlUtilities.GetLocalizedKey(Eval("Id"), null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(Container.DataItem.GetType().FullName)) %>' 
            NavigateUrl="#commentsWidget" AllowComments='<%# Eval("AllowComments") %>' 
            DisplayMode="FullText" 
            ThreadType='<%# Container.DataItem.GetType().FullName %>'/>
        <div class="sfnewsAuthorAndDate sfmetainfo">
            <asp:Literal Text="<%$ Resources:Labels, By %>" runat="server" /> 
            <sf:PersonProfileView runat="server" /> | <sf:FieldListView ID="PublicationDate" runat="server" 
                Format="{PublicationDate.ToLocal():MMM dd, yyyy}" />
            <comments:CommentsCountControl runat="server" 
                ThreadKey='<%# ControlUtilities.GetLocalizedKey(Eval("Id"), null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(Container.DataItem.GetType().FullName)) %>' 
                NavigateUrl="#commentsWidget" AllowComments='<%# Eval("AllowComments") %>' 
                DisplayMode="ShortText" 
                ThreadType='<%# Container.DataItem.GetType().FullName %>'/>

        </div>
        <sf:FieldListView ID="summary" runat="server" Text="{0}" Properties="Summary" WrapperTagName="div" WrapperTagCssClass="sfnewsSummary sfsummary"  EditableFieldType="ShortText"/> 
        <div class="sfnewsContent sfcontent" data-sf-field="Content" data-sf-ftype="LongText">
            <asp:Literal Text='<%# Eval("Content") %>' runat="server" />
        </div>
           <asp:PlaceHolder ID="socialOptionsContainer" runat="server">
           </asp:PlaceHolder>
           <comments:CommentsWidget runat="server"
               ThreadKey='<%# ControlUtilities.GetLocalizedKey(Eval("Id"), null, CommentsBehaviorUtilities.GetLocalizedKeySuffix(Container.DataItem.GetType().FullName)) %>' 
               AllowComments='<%# Eval("AllowComments") %>' ThreadTitle='<%# Eval("Title") %>' ThreadType='<%# Container.DataItem.GetType().FullName %>' 
               GroupKey='<%# ControlUtilities.GetUniqueProviderKey("Telerik.Sitefinity.Modules.News.NewsManager", Eval("Provider.Name").ToString()) %>'
               DataSource='<%# Eval("Provider.Name")%>' />
       </div>
    </itemtemplate>
</telerik:RadListView>

