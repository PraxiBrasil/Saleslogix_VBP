<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CopyUserProfile.ascx.cs" Inherits="CopyUserProfile" %>

<%@ Register Assembly="Sage.SalesLogix.Web.Controls" Namespace="Sage.SalesLogix.Web.Controls"
    TagPrefix="SalesLogix" %>
<%@ Register Assembly="Sage.SalesLogix.HighLevelTypes" Namespace="Sage.SalesLogix.HighLevelTypes"
    TagPrefix="SalesLogix" %>
<%@ Register Assembly="Sage.Platform.WebPortal" Namespace="Sage.Platform.WebPortal.SmartParts"
    TagPrefix="SalesLogix" %>
<%@ Register TagName="CopyProfileParameter" TagPrefix="UserCtl" Src="~/SmartParts/User/CopyProfileParameters.ascx" %>

<style type="text/css">
    #outerDiv { margin: 10px; }
</style>

<div style="display:none">
    <asp:Panel ID="AddUsers_RTools" runat="server">
        <SalesLogix:PageLink ID="lnkCopyUserHelp" runat="server" LinkType="HelpFileName"
            ToolTip="<%$ resources: Portal, Help_ToolTip %>" Target="Help" NavigateUrl="Copying_Profile_Information.htm" ImageUrl="~/ImageResource.axd?scope=global&type=Global_Images&key=Help_16x16">&nbsp;&nbsp;&nbsp;&nbsp;</SalesLogix:PageLink>
    </asp:Panel>    
</div>

<div id="outerDiv">
<UserCtl:CopyProfileParameter ID="copyProfileParameter" runat="server" />   

<p>&nbsp;</p>
<asp:Label ID="lblTargets" runat="server" Text="<%$ resources: targetUserGridCaption %>"></asp:Label>
<b><asp:Label ID="lblTargetUser" runat="server"></asp:Label></b>


<p>&nbsp;</p>

<table style="width: 100%">
    <tr>
        <td align="right">
            <asp:Button ID="btnOk" runat="server" Text="<%$ resources: OkButton.Caption %>" CssClass="slxbutton"  />
            <asp:Button ID="btnCancel" runat="server" CssClass="slxbutton" Text="<%$ resources: CancelButton.Caption %>" />
        </td>
    </tr>
</table>

</div>

