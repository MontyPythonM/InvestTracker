using InvestTracker.Shared.Infrastructure.Types;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace InvestTracker.InvestmentStrategies.Infrastructure.Persistence.Converters;

internal sealed class GuidCollectionValueConverter : ValueConverter<ICollection<Guid>, string>
{
    public GuidCollectionValueConverter() 
        : base(collection => SerializeCollection(collection), json => DeserializeCollection(json))
    {
    }

    private static string SerializeCollection(ICollection<Guid> collection)
    {
        if (collection.Any())
        {
            var elements = collection.Select(element => element.ToString()).ToArray();
            return JsonConvert.SerializeObject(elements);
        }
        
        return string.Empty;
    }

    private static ICollection<Guid> DeserializeCollection(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
        {
            return new List<Guid>();
        }

        var deserializedResults = JsonConvert.DeserializeObject<ICollection<Guid>>(json);

        if (deserializedResults is null || !deserializedResults.Any())
        {
            return new List<Guid>();
        }

        return deserializedResults;
        
        //return deserializedResults.Select(element => element.ToGuid()).ToList();
    }
}