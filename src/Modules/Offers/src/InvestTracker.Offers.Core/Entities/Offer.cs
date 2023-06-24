namespace InvestTracker.Offers.Core.Entities;

public class Offer
{
    public Guid Id { get; private set; }
    public string Title { get; private set; } = string.Empty;
    public string? Description { get; private set; }
    public decimal? Price { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    public Advisor Advisor { get; private set; }
    public Guid AdvisorId { get; private set; }
    public ICollection<OfferTag> Tags { get; private set; } = new List<OfferTag>();

    private Offer()
    {
        // for EF
    }

    public Offer(Guid id, string title, string? description, decimal? price, DateTime now, 
        Advisor advisor, IEnumerable<string> tags)
    {
        Id = id;
        Title = title;
        Description = description;
        Price = price;
        CreatedAt = now;
        UpdatedAt = null;
        Advisor = advisor;
        AddTags(CreateTags(tags));
    }

    public void Update(string title, string? description, decimal? price, DateTime now, IEnumerable<string> tags)
    {
        Title = title;
        Description = description;
        Price = price;
        UpdatedAt = now;
        AddTags(CreateTags(tags));
    }
    
    private IEnumerable<OfferTag> CreateTags(IEnumerable<string> tagValues)
    {
        return tagValues.Select(value => new OfferTag
        {
            Id = Guid.NewGuid(),
            Value = value
        });
    }

    private void AddTags(IEnumerable<OfferTag> tags)
    {
        Tags.Clear();
        tags.ToList().ForEach(tag => Tags.Add(tag));
    }
}