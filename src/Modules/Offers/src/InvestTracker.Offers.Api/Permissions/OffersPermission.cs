namespace InvestTracker.Offers.Api.Permissions;

public enum OffersPermission
{
    CreateOffer,
    UpdateOffer,
    DeleteOffer,
    
    GetUserInvitations,
    SendCollaborationInvitation,
    ConfirmCollaborationInvitation,
    RejectCollaborationInvitation,
    
    GetUserCollaborations,
    GetUserCollaboration,
    CancelOwnCollaboration,
    CancelSelectedCollaboration,
    
    GetAdvisorDetails,
    UpdateAdvisorDetails
}