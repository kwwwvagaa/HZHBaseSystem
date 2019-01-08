using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JZ.Tools
{
    public class LimitPropsContractResolver : DefaultContractResolver
    {
        private string[] props = null;

        private bool retain;

        public LimitPropsContractResolver(string[] props, bool retain = true)
        {
            this.props = props;
            this.retain = retain;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            IList<JsonProperty> source = base.CreateProperties(type, memberSerialization);
            return source.Where(delegate(JsonProperty p)
            {
                bool result;
                if (this.retain)
                {
                    result = this.props.Contains(p.PropertyName);
                }
                else
                {
                    result = !this.props.Contains(p.PropertyName);
                }
                return result;
            }).ToList<JsonProperty>();
        }
    }
}
