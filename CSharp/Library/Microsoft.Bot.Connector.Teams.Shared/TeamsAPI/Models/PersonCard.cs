// <auto-generated /> Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Bot.Connector.Teams.Models
{
    using System.Linq;

    /// <summary>
    /// Card representing a person.
    /// </summary>
    public partial class PersonCard
    {
        /// <summary>
        /// Initializes a new instance of the PersonCard class.
        /// </summary>
        public PersonCard() { }

        /// <summary>
        /// Initializes a new instance of the PersonCard class.
        /// </summary>
        /// <param name="upn">UPN of the user</param>
        /// <param name="text">Text for the card</param>
        /// <param name="images">Array of images</param>
        /// <param name="buttons">Set of actions applicable to the current
        /// card</param>
        public PersonCard(string upn = default(string), string text = default(string), System.Collections.Generic.IList<CardImage> images = default(System.Collections.Generic.IList<CardImage>), System.Collections.Generic.IList<CardAction> buttons = default(System.Collections.Generic.IList<CardAction>), CardAction tap = default(CardAction))
        {
            Upn = upn;
            Text = text;
            Images = images;
            Buttons = buttons;
            Tap = tap;
        }

        /// <summary>
        /// Gets or sets UPN of the user
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "upn")]
        public string Upn { get; set; }

        /// <summary>
        /// Gets or sets text for the card
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets array of images
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "images")]
        public System.Collections.Generic.IList<CardImage> Images { get; set; }

        /// <summary>
        /// Gets or sets set of actions applicable to the current card
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "buttons")]
        public System.Collections.Generic.IList<CardAction> Buttons { get; set; }

        /// <summary>
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "tap")]
        public CardAction Tap { get; set; }

    }
}
