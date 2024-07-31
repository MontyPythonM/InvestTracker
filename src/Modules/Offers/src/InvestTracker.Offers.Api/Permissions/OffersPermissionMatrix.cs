using System.Reflection;
using InvestTracker.Shared.Abstractions.Authorization;

namespace InvestTracker.Offers.Api.Permissions;

internal sealed class OffersPermissionMatrix : IModulePermissionMatrix
{
    public string GetModuleName() => Assembly.GetExecutingAssembly().GetName().Name!;

    public ISet<Permission> Permissions { get; } = new HashSet<Permission>()
    {
        new(SystemSubscription.Advisor, OffersPermission.CreateOffer.ToString()),
        new(SystemSubscription.Advisor, OffersPermission.UpdateOffer.ToString()),
        new(SystemSubscription.Advisor, OffersPermission.DeleteOffer.ToString()),
        new(SystemSubscription.Advisor, OffersPermission.GetUserInvitations.ToString()),
        new(SystemSubscription.Advisor, OffersPermission.ConfirmCollaborationInvitation.ToString()),
        new(SystemSubscription.Advisor, OffersPermission.RejectCollaborationInvitation.ToString()),
        new(SystemSubscription.Advisor, OffersPermission.GetUserCollaborations.ToString()),
        new(SystemSubscription.Advisor, OffersPermission.GetUserCollaboration.ToString()),
        new(SystemSubscription.Advisor, OffersPermission.CancelOwnCollaboration.ToString()),
        new(SystemSubscription.Advisor, OffersPermission.GetAdvisorDetails.ToString()),
        new(SystemSubscription.Advisor, OffersPermission.UpdateAdvisorDetails.ToString()),
        
        new(SystemSubscription.StandardInvestor, OffersPermission.GetUserInvitations.ToString()),
        new(SystemSubscription.StandardInvestor, OffersPermission.SendCollaborationInvitation.ToString()),
        new(SystemSubscription.StandardInvestor, OffersPermission.GetUserCollaborations.ToString()),
        new(SystemSubscription.StandardInvestor, OffersPermission.GetUserCollaboration.ToString()),
        new(SystemSubscription.StandardInvestor, OffersPermission.CancelOwnCollaboration.ToString()),

        new(SystemSubscription.ProfessionalInvestor, OffersPermission.GetUserInvitations.ToString()),
        new(SystemSubscription.ProfessionalInvestor, OffersPermission.SendCollaborationInvitation.ToString()),
        new(SystemSubscription.ProfessionalInvestor, OffersPermission.GetUserCollaborations.ToString()),
        new(SystemSubscription.ProfessionalInvestor, OffersPermission.GetUserCollaboration.ToString()),
        new(SystemSubscription.ProfessionalInvestor, OffersPermission.CancelOwnCollaboration.ToString()),
        
        new(SystemRole.BusinessAdministrator, OffersPermission.UpdateOffer.ToString()),
        new(SystemRole.BusinessAdministrator, OffersPermission.DeleteOffer.ToString()),
        new(SystemRole.BusinessAdministrator, OffersPermission.GetUserCollaboration.ToString()),
        new(SystemRole.BusinessAdministrator, OffersPermission.GetAdvisorDetails.ToString()),
        new(SystemRole.BusinessAdministrator, OffersPermission.UpdateAdvisorDetails.ToString()),
        
        new(SystemRole.SystemAdministrator, OffersPermission.UpdateOffer.ToString()),
        new(SystemRole.SystemAdministrator, OffersPermission.DeleteOffer.ToString()),
        new(SystemRole.SystemAdministrator, OffersPermission.ConfirmCollaborationInvitation.ToString()),
        new(SystemRole.SystemAdministrator, OffersPermission.RejectCollaborationInvitation.ToString()),
        new(SystemRole.SystemAdministrator, OffersPermission.GetUserCollaboration.ToString()),
        new(SystemRole.SystemAdministrator, OffersPermission.CancelSelectedCollaboration.ToString()),
        new(SystemRole.SystemAdministrator, OffersPermission.GetAdvisorDetails.ToString()),
        new(SystemRole.SystemAdministrator, OffersPermission.UpdateAdvisorDetails.ToString()),
    };
}