﻿using InvestTracker.InvestmentStrategies.Application.Portfolios.Dto;
using InvestTracker.InvestmentStrategies.Domain.Portfolios.ValueObjects.Types;
using InvestTracker.Shared.Abstractions.Queries;

namespace InvestTracker.InvestmentStrategies.Application.Portfolios.Queries;

public record GetPortfolio(PortfolioId PortfolioId) : IQuery<PortfolioDetailsDto>;