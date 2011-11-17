using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Telerik.WebControls;
using Sage.Platform.WebPortal.SmartParts;
using Sage.Platform.Application.UI;
using Sage.Entity.Interfaces;
using System.IO;
using Sage.Platform.Application;
using Sage.Platform.Application.UI.Web;
using Sage.Platform.Application.UI.Web.Threading;
using System.Threading;
using Sage.Platform.Orm;
using Sage.Platform.WebPortal.Services;
using Sage.SalesLogix.Security;
using Sage.SalesLogix.Client.GroupBuilder;


public partial class ReplaceTeamMember : Sage.Platform.WebPortal.SmartParts.SmartPartInfoProvider
{


    private string _mode = string.Empty;
    
    

    /// <summary>
    /// Gets the smart part info.
    /// </summary>
    /// <param name="smartPartInfoType">Type of the smart part info.</param>
    /// <returns></returns>
    public override ISmartPartInfo GetSmartPartInfo(Type smartPartInfoType)
    {
            ToolsSmartPartInfo tinfo = new ToolsSmartPartInfo();

            foreach (Control c in ReplaceTeamMember_RTools.Controls)
            {
                tinfo.RightTools.Add(c);
            }
            tinfo.Title = GetTitle();
           return tinfo;          
    }

    
    /// <summary>
    /// Raises the <see cref="E:PreRender"/> event.
    /// </summary>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected override void OnPreRender(EventArgs e)
    {

        LoadView();

    }

    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);

       
    }
   

    /// <summary>
    /// Handles the Click event of the ok button control.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
    protected void OK_Click(object sender, EventArgs e)
    {

        ReplaceMemberOnAllTeam();
               
        DialogService.CloseEventHappened(this, null);
    }
    
    private void ReplaceMemberOnAllTeam()
    {

        IOwner selectedMember = null;
        string selectedMemberId = string.Empty;
        IOwner replaceMember = null;
        string replaceMemberId = string.Empty;

        if (lueSelectedMember.LookupResultValue != null)
        {
            selectedMemberId = lueSelectedMember.LookupResultValue.ToString();
            selectedMember = Sage.Platform.EntityFactory.GetById<IOwner>(selectedMemberId);

        }

        if (lueReplaceMember.LookupResultValue != null)
        {
            replaceMemberId = lueReplaceMember.LookupResultValue.ToString();
            replaceMember = Sage.Platform.EntityFactory.GetById<IOwner>(replaceMemberId);

        }


        if ((selectedMember != null) && (replaceMember != null))
        {
            
            IList<ITeam> teams = Sage.SalesLogix.Team.Rules.GetTeams();
            foreach (ITeam team in teams)
            {

                if (team.ContainsMember(selectedMember))
                {
                    IOwnerJoin member = Sage.Platform.EntityFactory.GetByCompositeId(typeof(IOwnerJoin), new string[] { "ParentOwnerId", "ChildOwnerId" }, new object[] { team.Id, selectedMember.Id }) as IOwnerJoin;
                    team.RemoveMember(member);
                    // keep the security profile from the original member
                    team.AddMemberWithSecurityProfile(replaceMember, member.OwnerSecurityProfile);                   
                }

            }
            RedirectTo(replaceMember);   
            
        }
    }
    private void RedirectTo(IOwner replaceMember)
    {
        string Id = string.Empty;
        if (replaceMember.Type == OwnerType.User)
        {
            Id = GetUserId(replaceMember);
            Response.Redirect(string.Format("~/{0}.aspx?entityId={1}", "User", Id), false);
        }
        else if (replaceMember.Type == OwnerType.Department)
        {
            Id = GetDepartmentId(replaceMember);
            Response.Redirect(string.Format("~/{0}.aspx?entityId={1}", "Department", Id), false);
        }
        else if (replaceMember.Type == OwnerType.Team)
        {
            Id = GetTeamId(replaceMember);
            Response.Redirect(string.Format("~/{0}.aspx?entityId={1}", "Team", Id), false);
        }
    }
    private string GetUserId(IOwner owner)
    {
      return owner.User.Id.ToString();

    }
    private string GetTeamId(IOwner owner)
    {
        return owner.Id.ToString();

    }
    private string GetDepartmentId(IOwner owner)
    {
        return owner.Id.ToString();

    }
    private IList<string> GetTargets()
    {

        IList<string> ids = null;
        if (DialogService.DialogParameters.ContainsKey("selectedIds"))
        {

            ids = DialogService.DialogParameters["selectedIds"] as IList<string>;
        }
        return ids;
    }

    private Type GetContext()
    {
        Type context = null;
       
        if (DialogService.DialogParameters.ContainsKey("context"))
        {

            context = DialogService.DialogParameters["context"] as Type;
        }
               
        return context;
    }

    private string GetTitle()
    {
        string desc = GetLocalResourceObject("Title_Replace.Caption").ToString();
       
        Type context = GetContext();
        if (context == typeof(IUser))
        {
            desc = GetLocalResourceObject("Title_Replace_User.Caption").ToString();
        }
        else if (context == typeof(ITeam))
        {
            desc = GetLocalResourceObject("Title_Replace_Team.Caption").ToString();
        }
        else if (context == typeof(IDepartment))
        {
            desc = GetLocalResourceObject("Title_Replace_Depart.Caption").ToString();
        }
        desc = GetLocalResourceObject("Title_Replace.Caption").ToString();
        return desc;

    }
    private string GetSelectLabel()
    {
        string desc = GetLocalResourceObject("lueSelectedMember.Caption").ToString();
        Type context = GetContext();
        if (context == typeof(IUser))
        {
            desc = GetLocalResourceObject("lueSelectedMemberUser.Caption").ToString();
        }
        else if (context == typeof(ITeam))
        {
            desc = GetLocalResourceObject("lueSelectedMemberTeam.Caption").ToString();
        }
        else if (context == typeof(IDepartment))
        {
            desc = GetLocalResourceObject("lueSelectedMemberDepart.Caption").ToString();
        }

        return desc;

    }
    private string GetDescription()
    {
        string desc = GetLocalResourceObject("Replace_Description").ToString();
        
        return desc;

    }
    private string GetSelectedOwnerId()
    {
        IList<string> ids = GetTargets();
        string ownerId = string.Empty;
        Type context = GetContext();
        if (context == typeof(IUser))
        {
            IUser  user = Sage.Platform.EntityFactory.GetById<IUser>(ids[0]);
            if ((user.Type == UserType.Template))
            {
                throw new ApplicationException(GetLocalResourceObject("InvalidUserContextMSG").ToString());
            }
            else
            {
                ownerId = user.DefaultOwner.Id.ToString(); 
            }

        }
        else if (context == typeof(ITeam))
        {
            ITeam team = Sage.Platform.EntityFactory.GetById<ITeam>(ids[0]);
            ownerId = team.Owner.Id.ToString(); 
        }
        else if (context == typeof(IDepartment))
        {
            IDepartment depart = Sage.Platform.EntityFactory.GetById<IDepartment>(ids[0]);
            ownerId = depart.Owner.Id.ToString(); 
        }

        return ownerId;

    }
    protected void CANCEL_Click(object sender, EventArgs e)
    {
        //Response.Redirect(string.Format("~/{0}.aspx?", "Team"), false);    
    }

    protected override void OnWireEventHandlers()
    {
        base.OnWireEventHandlers();
         btnCancel.Click += new EventHandler(DialogService.CloseEventHappened);

    }

    private void LoadView()
    {
       
        if (DialogService.DialogParameters.Count > 0 )
        {

            if (DialogService.DialogParameters.ContainsKey("selectedIds"))
            {

                lueSelectedMember.LookupResultValue = GetSelectedOwnerId();
                lblSelectedMember.Text = GetSelectLabel();
                //lblDescription.Text = GetDescription();

            }

        }

        if ((lueReplaceMember.LookupResultValue == null) || (lueReplaceMember.LookupResultValue.ToString() == string.Empty))
        {
            btnOk.Enabled = false;
        }
        else 
        {
            btnOk.Enabled = true;
            lueReplaceMember.LookupResultValue = lueReplaceMember.LookupResultValue;
        }
        
    }  
    
}
