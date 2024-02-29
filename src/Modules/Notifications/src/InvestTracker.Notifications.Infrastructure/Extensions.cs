using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using InvestTracker.Notifications.Core.Entities;
using InvestTracker.Notifications.Core.Entities.Base;
using InvestTracker.Notifications.Core.Enums;
using InvestTracker.Notifications.Core.Interfaces;
using InvestTracker.Notifications.Infrastructure.Interfaces;
using InvestTracker.Notifications.Infrastructure.Options;
using InvestTracker.Notifications.Infrastructure.Persistence;
using InvestTracker.Notifications.Infrastructure.Persistence.Repositories;
using InvestTracker.Notifications.Infrastructure.Services.Emails;
using InvestTracker.Notifications.Infrastructure.Services.GlobalSettings;
using InvestTracker.Notifications.Infrastructure.Services.Notifications;
using InvestTracker.Notifications.Infrastructure.Services.Receivers;
using InvestTracker.Shared.Abstractions.Authorization;
using InvestTracker.Shared.Infrastructure;
using InvestTracker.Shared.Infrastructure.Postgres;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("InvestTracker.Notifications.Api")]
namespace InvestTracker.Notifications.Infrastructure;

internal static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        var notificationServiceOptions = services.GetOptions<NotificationServiceOptions>("Notifications:NotificationService");
        var emailSenderOptions = services.GetOptions<EmailSenderOptions>("Emails:Sender");

        services
            .AddSingleton(emailSenderOptions)
            .AddScoped<IEmailSender, SmtpEmailSender>()
            .AddScoped<IEmailPublisher, EmailPublisher>();

        services 
            .AddSingleton(notificationServiceOptions)
            .AddSingleton<INotificationSender, NotificationSender>()
            .AddHostedService(sp => (NotificationSender)sp.GetService<INotificationSender>()!)
            .AddScoped<INotificationPublisher, NotificationPublisher>();
        
        services
            .AddPostgres<NotificationsDbContext>()
            .AddScoped<IReceiverRepository, ReceiverRepository>()
            .AddScoped<IFilteredReceiverRepository, FilteredReceiverRepository>()
            .AddScoped<IReceiverService, ReceiverService>()
            .AddScoped<IGlobalSettingsRepository, GlobalSettingsRepository>()
            .AddScoped<IGlobalSettingsService, GlobalSettingsService>()
            .AddSignalR(s => s.EnableDetailedErrors = notificationServiceOptions.EnableDetailedErrorsSignalR);

        return services;
    }

    /// <summary>
    /// Filter query by selected GlobalSettings property
    /// </summary>
    /// <param name="query">processed query</param>
    /// <param name="filterBy">expression with NotificationSettings property by which the query will be filtered</param>
    /// <param name="globalSettings">current global settings</param>
    /// <returns>For true value of selected GlobalSettings property returns original query. Otherwise, it returns the query always with no results.</returns>
    internal static IQueryable<Receiver> FilterByGlobalSetting(this IQueryable<Receiver> query, 
        Expression<Func<NotificationSettings, bool>>? filterBy, GlobalSettings globalSettings)
    {
        if (filterBy is null) return query;
        
        var filterByPropertyName = ((MemberExpression)filterBy.Body).Member.Name;
        var properties =  typeof(NotificationSettings).GetProperties();
        
        foreach (var property in properties)
        {
            if (property.Name != filterByPropertyName)
            {
                continue;
            }
            
            var globalSettingsPropertyValue = property.GetValue(globalSettings);

            if (globalSettingsPropertyValue is not null)
            {
                return (bool)globalSettingsPropertyValue 
                    ? query
                    : query.Where(x => false);
            }
        }
        
        return query;
    }
    
    /// <summary>
    /// Filter query by selected PersonalSettings property
    /// </summary>
    /// <param name="query">processed query</param>
    /// <param name="filterBy">expression with NotificationSettings property by which the query will be filtered</param>
    /// <returns>Returns a query with an additional where clause with an expression containing condition with the FilterBy property selected</returns>
    internal static IQueryable<Receiver> FilterByPersonalSetting(this IQueryable<Receiver> query, 
        Expression<Func<NotificationSettings, bool>>? filterBy)
    {
        if (filterBy is null) return query;
        
        var filterByPropertyName = ((MemberExpression)filterBy.Body).Member.Name;
        var properties =  typeof(NotificationSettings).GetProperties();

        foreach (var property in properties)
        {
            if (property.Name != filterByPropertyName)
            {
                continue;
            }
            
            var parameter = Expression.Parameter(typeof(Receiver), "x");

            var equality = Expression.Equal(
                Expression.Property(Expression.Property(parameter, nameof(PersonalSettings)), property.Name), 
                Expression.Constant(true));
            
            return query.Where(Expression.Lambda<Func<Receiver, bool>>(equality, parameter));
        }

        return query;
    }
    
    internal static IQueryable<Receiver> FilterByRecipientGroup(this IQueryable<Receiver> query, RecipientGroup recipientGroup)
    {
        var filteredQuery = recipientGroup switch
        {
            RecipientGroup.None 
                => query.Where(r => false),
            RecipientGroup.StandardInvestors 
                => query.Where(r => r.Subscription == SystemSubscription.StandardInvestor),
            RecipientGroup.ProfessionalInvestors 
                => query.Where(r => r.Subscription == SystemSubscription.ProfessionalInvestor),
            RecipientGroup.Investors 
                => query.Where(r => r.Subscription == SystemSubscription.StandardInvestor || r.Subscription == SystemSubscription.ProfessionalInvestor),
            RecipientGroup.Advisors 
                => query.Where(r => r.Subscription == SystemSubscription.Advisor),
            RecipientGroup.Subscribers 
                => query.Where(r => r.Subscription == SystemSubscription.StandardInvestor || r.Subscription == SystemSubscription.ProfessionalInvestor || r.Subscription == SystemSubscription.Advisor),
            RecipientGroup.BusinessAdministrators 
                => query.Where(r => r.Role == SystemRole.BusinessAdministrator),
            RecipientGroup.SystemAdministrators 
                => query.Where(r => r.Role == SystemRole.SystemAdministrator),
            RecipientGroup.Administrators 
                => query.Where(r => r.Role == SystemRole.BusinessAdministrator || r.Role == SystemRole.SystemAdministrator),
            RecipientGroup.All => query,
            _ => throw new ArgumentOutOfRangeException(nameof(recipientGroup), recipientGroup, null)
        };
        
        return filteredQuery;
    }
}