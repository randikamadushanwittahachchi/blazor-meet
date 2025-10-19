using ClientLibrary.Helper.Constracts;
using System.Text.Json;

namespace ClientLibrary.Helper.Implimentations;

public class Serialization<T> : ISerialization<T> where T : class
{
    public T? DeserializationJsonString(string jsonString)
        => JsonSerializer.Deserialize<T>(jsonString);

    public List<T>? DeserializationJsonStringList(string jsonString)
        => JsonSerializer.Deserialize<List<T>>(jsonString);

    public string? SerializationModelObject(T modelObject)
        => JsonSerializer.Serialize(modelObject);
}
