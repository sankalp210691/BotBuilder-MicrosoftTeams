// <auto-generated /> Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Bot.Connector.Teams.Models
{
    using System.Linq;

    /// <summary>
    /// Describes a tenant
    /// </summary>
    public partial class TenantInfo
    {
        /// <summary>
        /// Initializes a new instance of the TenantInfo class.
        /// </summary>
        public TenantInfo() { }

        /// <summary>
        /// Initializes a new instance of the TenantInfo class.
        /// </summary>
        /// <param name="id">Unique identifier representing a tenant</param>
        public TenantInfo(string id = default(string))
        {
            Id = id;
        }

        /// <summary>
        /// Gets or sets unique identifier representing a tenant
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

    }
}
