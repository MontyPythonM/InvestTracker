using InvestTracker.Notifications.Core.Dto;
using InvestTracker.Notifications.Core.Entities.Base;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Exceptions;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Abstractions.Context;

namespace InvestTracker.Notifications.Core.Services.Receivers;

public class ReceiverService : IReceiverService
{
    private readonly IReceiverRepository _receiverRepository;
    private readonly IRequestContext _requestContext;

    public ReceiverService(IReceiverRepository receiverRepository, IRequestContext requestContext)
    {
        _receiverRepository = receiverRepository;
        _requestContext = requestContext;
    }

    public async Task<PersonalSettingsDto> GetPersonalSettingsAsync(CancellationToken token)
    {
        var currentUserId = _requestContext.Identity.UserId;
        var receiver = await _receiverRepository.GetAsync(currentUserId, null, true, token);

        if (receiver is null)
        {
            throw new ReceiverNotFoundException(currentUserId);
        }

        return new PersonalSettingsDto
        {
            EnableNotifications = receiver.PersonalSettings.EnableNotifications,
            EnableEmails = receiver.PersonalSettings.EnableEmails,
            InvestmentStrategiesActivity = receiver.PersonalSettings.InvestmentStrategiesActivity,
            PortfoliosActivity = receiver.PersonalSettings.PortfoliosActivity,
            AssetActivity = receiver.PersonalSettings.AssetActivity,
            ExistingCollaborationsActivity = receiver.PersonalSettings.ExistingCollaborationsActivity,
            NewCollaborationsActivity = receiver.PersonalSettings.NewCollaborationsActivity
        };
    }

    public async Task<IEnumerable<ReceiverDto>> GetReceiversAsync(RecipientGroup recipientGroup, CancellationToken token)
    {
        var receivers = await _receiverRepository.GetAsync(recipientGroup, null, true, token);

        return receivers.Select(receiver => new ReceiverDto
        {
            Id = receiver.Id,
            FullName = receiver.FullName,
            Email = receiver.Email,
            PhoneNumber = receiver.PhoneNumber,
            Role = receiver.Role,
            Subscription = receiver.Subscription,
            PersonalSettings = MapToDto(receiver.PersonalSettings)
        });
    }

    public async Task SetPersonalSettingsAsync(SetPersonalSettingsDto dto, CancellationToken token)
    {
        var currentUserId = _requestContext.Identity.UserId;
        var receiver = await _receiverRepository.GetAsync(currentUserId, null, false, token);
        
        if (receiver is null)
        {
            throw new ReceiverNotFoundException(currentUserId);
        }

        receiver.PersonalSettings.EnableNotifications = dto.EnableNotifications;
        receiver.PersonalSettings.EnableEmails = dto.EnableEmails;
        receiver.PersonalSettings.InvestmentStrategiesActivity = dto.InvestmentStrategiesActivity;
        receiver.PersonalSettings.PortfoliosActivity = dto.PortfoliosActivity;
        receiver.PersonalSettings.AssetActivity = dto.AssetActivity;
        receiver.PersonalSettings.ExistingCollaborationsActivity = dto.ExistingCollaborationsActivity;
        receiver.PersonalSettings.NewCollaborationsActivity = dto.NewCollaborationsActivity;
        receiver.PersonalSettings.AdministratorsActivity = SystemRole.IsAdministrator(receiver.Role) && dto.AdministratorsActivity;
        
        await _receiverRepository.UpdateAsync(receiver, token);
    }
    
    private static PersonalSettingsDto MapToDto(NotificationSettings settings) => new()
    {
        EnableNotifications = settings.EnableNotifications,
        EnableEmails = settings.EnableEmails,
        InvestmentStrategiesActivity = settings.InvestmentStrategiesActivity,
        PortfoliosActivity = settings.PortfoliosActivity,
        AssetActivity = settings.AssetActivity,
        ExistingCollaborationsActivity = settings.ExistingCollaborationsActivity,
        NewCollaborationsActivity = settings.NewCollaborationsActivity,
        AdministratorsActivity = settings.AdministratorsActivity
    };
}