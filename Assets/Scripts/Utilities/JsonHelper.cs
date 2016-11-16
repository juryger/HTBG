using System;
using UnityEngine;

/// <summary>
/// Used for deserializing JSON collections.
/// </summary>
public class JsonHelper
{
    /* json string variable contains a list of persons serialized as JSON, i.e. {"id":1, "name":"Robert"},{"id":2, name:"John"}
    json = "[" + json + "]";
    var pList = JsonHelper.getJsonArray<Person>(json);
    */

    /// <summary>
    /// Deserialize collection from JSON string.
    /// </summary>
    /// <typeparam name="T">type of object in collection</typeparam>
    /// <param name="json">json representation of object collection</param>
    /// <returns>deserialized object collection</returns>
    public static T[] getJsonArray<T>(string json)
    {
        if (string.IsNullOrEmpty(json))
            return null;

        Wrapper<T> result = null;
        string newJson = "{ \"array\": " + json + "}";
        result = JsonUtility.FromJson<Wrapper<T>>(newJson);

        return result != null ? result.array : null;
    }

    [Serializable]
    private class Wrapper<T>
    {
        public T[] array;
    }
}