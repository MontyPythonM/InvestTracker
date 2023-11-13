using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Converters;

internal sealed class IndirectRelationValueConverter<T> : ValueConverter<ICollection<T>, string>
{
    public IndirectRelationValueConverter() 
        : base(collection => SerializeCollection(collection), json => DeserializeCollection(json))
    {
    }

    private static string SerializeCollection(ICollection<T> collection)
        => !collection.Any() ? 
            string.Empty : 
            JsonConvert.SerializeObject(collection);

    private static ICollection<T> DeserializeCollection(string json)
        => string.IsNullOrWhiteSpace(json) ? 
            new List<T>() : 
            JsonConvert.DeserializeObject<ICollection<T>>(json)!;
}