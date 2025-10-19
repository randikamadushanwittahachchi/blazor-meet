namespace ClientLibrary.Helper.Constracts;

public interface ISerialization<T> where T : class
{
    string? SerializationModelObject(T modelObject);
    T? DeserializationJsonString(string jsonString);
    List<T>? DeserializationJsonStringList(string jsonString);
}
