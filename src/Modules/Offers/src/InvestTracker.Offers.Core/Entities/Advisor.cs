﻿namespace InvestTracker.Offers.Core.Entities;

public class Advisor
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? Bio { get; set; }
    public string? CompanyName { get; set; }
    public byte[]? Avatar { get; set; }
    public virtual ICollection<Offer> Offers { get; set; } = new List<Offer>();
    public virtual ICollection<Collaboration> Collaborations { get; set; } = new List<Collaboration>();
    
    public Advisor()
    {
    }

    public Advisor(Guid id, string fullName, string email)
    {
        Id = id;
        FullName = fullName;
        Email = email;
    }
}