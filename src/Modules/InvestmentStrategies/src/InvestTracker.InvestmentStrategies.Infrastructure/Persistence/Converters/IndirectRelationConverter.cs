using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Converters;

internal sealed class IndirectRelationConverter<T> : ValueConverter<IEnumerable<T>, string> 
    where T : struct
{
    public IndirectRelationConverter() 
        : base(collection => SerializeCollection(collection), json => DeserializeCollection(json))
    {
    }

    private static string SerializeCollection(IEnumerable<T> collection)
        => !collection.Any() ? 
            string.Empty : 
            JsonConvert.SerializeObject(collection);

    private static IEnumerable<T> DeserializeCollection(string json)
        => string.IsNullOrWhiteSpace(json) ? 
            new HashSet<T>() : 
            JsonConvert.DeserializeObject<ISet<T>>(json)!;
}