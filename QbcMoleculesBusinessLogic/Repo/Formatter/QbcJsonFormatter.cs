using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QbcMoleculesBusinessLogic.Repo.Formatter
{
    public class QbcJsonFormatter : IQbcFormatter
    {
        #region private properties
        private JsonSerializerOptions Options { get; }

        #endregion



        public QbcJsonFormatter()
        {

            this.Options = new JsonSerializerOptions
            {
                IgnoreReadOnlyProperties = true,
                ReadCommentHandling = JsonCommentHandling.Skip,
                WriteIndented = true
            };
        }


        public async Task<TMsg?> DeserializeObjectAsync<TMsg>(byte[] msg) where TMsg : class?
        {
            TMsg? retval = default;
            using (MemoryStream str = new(msg))
            {
                retval = await JsonSerializer.DeserializeAsync<TMsg>(str, Options);
            }
            return retval;
        }

        public async Task<object?> DeserializeObjectAsync(byte[] msg, Type targetType)
        {
            object? retval;
            using (MemoryStream str = new(msg))
            {
                retval = await JsonSerializer.DeserializeAsync(str, targetType, Options);
            }
            return retval;
        }

        public TMsg? DeserializeObjectFromString<TMsg>(string msg) where TMsg : class?
        {
            TMsg? retval = default;
            if ( !string.IsNullOrEmpty(msg))
            {
                retval = JsonSerializer.Deserialize<TMsg>(msg, Options);
            }            
            return retval;
        }

        public object? DeserializeObjectFromString(string msg, Type targetType)
        {
            return JsonSerializer.Deserialize(msg, targetType, this.Options);
        }

        public byte[] SerializeObject<TMsg>(TMsg? message) where TMsg : class?
                                => JsonSerializer.SerializeToUtf8Bytes(message, this.Options);

        public byte[] SerializeObject(Type type, object message)
                                => JsonSerializer.SerializeToUtf8Bytes(message, type, this.Options);

        public byte[] SerializeObject(object message)
                            => JsonSerializer.SerializeToUtf8Bytes(message, message.GetType(), this.Options);

        public string SerializeObjectToString<TMsg>(TMsg? message) where TMsg : class?
                            => JsonSerializer.Serialize(message, this.Options);

        public string SerializeObjectToString(Type type, object message)
                            => JsonSerializer.Serialize(message, type, this.Options);

        public string SerializeObjectToString(object message)
                            => JsonSerializer.Serialize(message, message.GetType(), this.Options);
    }
}
