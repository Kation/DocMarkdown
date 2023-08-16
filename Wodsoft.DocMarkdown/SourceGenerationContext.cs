using System.Reflection;
using System.Text.Json.Serialization;

//[assembly: AssemblyMetadata("IsTrimmable", "True")]
namespace Wodsoft.DocMarkdown
{
    [JsonSourceGenerationOptions(WriteIndented = false, PropertyNamingPolicy = JsonKnownNamingPolicy.Unspecified)]
    [JsonSerializable(typeof(DocConfig))]
    [JsonSerializable(typeof(DocLanguage))]
    [JsonSerializable(typeof(DocLanguage[]))]
    [JsonSerializable(typeof(DocVersion))]
    [JsonSerializable(typeof(DocVersion[]))]
    [JsonSerializable(typeof(NavConfig))]
    [JsonSerializable(typeof(Dictionary<string, NavConfig>))]
    [JsonSerializable(typeof(string))]
    [JsonSerializable(typeof(bool))]
    internal partial class SourceGenerationContext : JsonSerializerContext
    {
    }
}
