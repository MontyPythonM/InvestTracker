using InvestTracker.Shared.Abstractions.Context;

namespace InvestTracker.Shared.Infrastructure.Context;

internal interface IContextFactory
{
    IContext Create();
}