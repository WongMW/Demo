<%@ Control Language="VB" AutoEventWireup="false" CodeFile="~/UserControls/Aptify_Surveys/QuestionTree.ascx.vb" Inherits="Aptify.Framework.Web.eBusiness.Surveys.QuestionTreeControl" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness.Knowledge.Controls" Assembly="AptifyKnowledgeWebControls" %>
<%@ Register TagPrefix="cc2" Namespace="Aptify.Framework.Web.eBusiness" Assembly="AptifyEBusinessUser" %>

<div class="content-container clearfix">
    <script language="javascript" type="text/javascript">
	    window.history.forward(1);
    </script>
	<table id="Table1" runat="server" >
		<tr>
			<td >		
				<cc2:QuestionTreeControl id="ctlQuestionTree" runat="server" />
			</td>
		</tr>
	</table>
    <cc2:User ID="User1" runat="server" />
</div>