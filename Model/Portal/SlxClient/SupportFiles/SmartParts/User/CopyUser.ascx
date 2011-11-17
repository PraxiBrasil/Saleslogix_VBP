<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CopyUser.ascx.cs" Inherits="CopyUser" %>

<%@ Register Assembly="Sage.SalesLogix.Web.Controls" Namespace="Sage.SalesLogix.Web.Controls"
    TagPrefix="SalesLogix" %>
<%@ Register Assembly="Sage.SalesLogix.HighLevelTypes" Namespace="Sage.SalesLogix.HighLevelTypes"
    TagPrefix="SalesLogix" %>
<%@ Register Assembly="Sage.Platform.WebPortal" Namespace="Sage.Platform.WebPortal.SmartParts"
    TagPrefix="SalesLogix" %>
<%@ Register TagName="CopyProfileParameter" TagPrefix="UserCtl" Src="~/SmartParts/User/CopyProfileParameters.ascx" %>

<style type="text/css">
    #outerDiv { margin: 10px; }
    table { width: 100%; }
</style>

<div style="display:none">
    <asp:Panel ID="AddUsers_RTools" runat="server">
        <SalesLogix:PageLink ID="lnkCopyUserHelp" runat="server" LinkType="HelpFileName"
            ToolTip="<%$ resources: Portal, Help_ToolTip %>" Target="Help" NavigateUrl="Creating_a_Copy_of_a_User.htm" ImageUrl="~/ImageResource.axd?scope=global&type=Global_Images&key=Help_16x16">&nbsp;&nbsp;&nbsp;&nbsp;</SalesLogix:PageLink>
    </asp:Panel>    
</div>

<div id="outerDiv">

<div>
    <div>
        <asp:Label id="lblStatus" runat="server" Text="<%$ resources: ConfirmCopyMessage %>"></asp:Label>
    </div>
    <p>&nbsp;</p>
    <div style="float: right">
        <asp:Button ID="btnCopy" runat="server" CssClass="slxbutton" Text="<%$ resources: OkButton.Caption %>" />
        <asp:Button ID="btnCancel" runat="server" CssClass="slxbutton" Text="<%$ resources: CancelButton.Caption %>" />
    </div>
</div>

</div>
