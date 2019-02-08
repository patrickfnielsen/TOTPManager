using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace TOTPManager.Helpers
{
    public class TypeOnlyContractResolver<T> : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty property = base.CreateProperty(member, memberSerialization);
            property.ShouldSerialize = instance => property.DeclaringType == typeof(T);
            return property;
        }
    }
}
