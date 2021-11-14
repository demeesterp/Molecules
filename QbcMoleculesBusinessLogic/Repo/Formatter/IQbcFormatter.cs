using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Repo.Formatter
{
    public interface IQbcFormatter
    {
        /// <summary>
        /// Serialize object to byte array
        /// </summary>
        /// <typeparam name="TMsg">Object type to be serialized</typeparam>
        /// <param name="message">object to be serialized</param>
        /// <returns>Serialized byte array</returns>
        Byte[] SerializeObject<TMsg>(TMsg? message) where TMsg : class?;


        /// <summary>
        /// Serialize object to byte array give the type of the object as parameter
        /// </summary>
        /// <param name="type">Type of the object to be serialized</param>
        /// <param name="message">Message to be serialized</param>
        /// <returns></returns>
        Byte[] SerializeObject(Type type, object message);

        /// <summary>
        /// SerializeObject GencoEntity to Byte Array
        /// </summary>
        /// <param name="message">resource to be serialized</param>
        /// <returns>Serialized byte array</returns>
        Byte[] SerializeObject(object message);

        /// <summary>
        /// Serialize object to string
        /// </summary>
        /// <typeparam name="TMsg">Object type to be serialized</typeparam>
        /// <param name="message">resource to be serialized</param>
        /// <returns>Serialized object as string</returns>
        string SerializeObjectToString<TMsg>(TMsg? message) where TMsg : class?;

        /// <summary>
        /// Serialize object to string
        /// </summary>
        /// <param name="type">Object type to be serialized</param>
        /// <param name="message">message to be serialiezd</param>
        /// <returns>Serialized object as string</returns>
        string SerializeObjectToString(Type type, object message);

        /// <summary>
        /// Serialize object to string
        /// </summary>
        /// <param name="message">resource to be serialized</param>
        /// <returns>string with serialized object</returns>
        string SerializeObjectToString(object message);

        /// <summary>
        /// Deserialize object from message 
        /// </summary>
        /// <typeparam name="TMsg">Objecttype to deserialize to</typeparam>
        /// <param name="msg">binary message to be deserialized</param>
        /// <returns>Deserialized object</returns>
        Task<TMsg?> DeserializeObjectAsync<TMsg>(Byte[] msg) where TMsg : class?;

        /// <summary>
        ///  Deserialize object from binary message 
        /// </summary>
        /// <param name="msg">binary message to be deserialized</param>
        /// <param name="targetType">Final object type to instantiate from stream</param>
        /// <returns>Deserialize object</returns>
        Task<object?> DeserializeObjectAsync(Byte[] msg, Type targetType);

        /// <summary>
        /// Deserialize object from string
        /// </summary>
        /// <typeparam name="TMsg">Objecttype to deserialize to</typeparam>
        /// <param name="msg">string message to de deserialized</param>
        /// <returns>Deserialized object</returns>
        TMsg? DeserializeObjectFromString<TMsg>(string msg) where TMsg : class?;

        /// <summary>
        /// Deserialize object from string
        /// </summary>
        /// <param name="msg">string message to be deserialized</param>
        /// <param name="targetType">target type to deserialize to</param>
        /// <returns>deserialized object</returns>
        object? DeserializeObjectFromString(string msg, Type targetType);

    }
}
