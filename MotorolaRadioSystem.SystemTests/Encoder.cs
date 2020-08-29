using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace MotorolaRadioSystem.SystemTests
{
    public class Encoder<T>
    {
        public StringContent JsonEncode(T data)
        {
            return new StringContent(JsonConvert.SerializeObject(data),
                Encoding.UTF8, "application/json");
        }
    }
}