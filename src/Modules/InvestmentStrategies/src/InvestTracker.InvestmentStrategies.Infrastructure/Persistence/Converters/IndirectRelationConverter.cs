using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Converters;

internal sealed class IndirectRelationConverter<T> : ValueConverter<ISet<T>, string> 
    where T : class
{
    public IndirectRelationConverter() 
        : base(collection => SerializeCollection(collection), json => DeserializeCollection(json))
    {
    }

    private static string SerializeCollection(ISet<T> collection)
        => collection.Count == 0 ? 
            string.Empty : 
            JsonConvert.SerializeObject(collection);

    private static ISet<T> DeserializeCollection(string json)
        => string.IsNullOrWhiteSpace(json) ? 
            new HashSet<T>() : 
            JsonConvert.DeserializeObject<ISet<T>>(json)!;
}