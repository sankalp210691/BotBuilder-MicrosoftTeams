// <auto-generated /> Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.

namespace Microsoft.Bot.Connector.Teams.Models
{
    using System.Linq;

    /// <summary>
    /// A list card
    /// </summary>
    public partial class ListCard
    {
        /// <summary>
        /// Initializes a new instance of the ListCard class.
        /// </summary>
        public ListCard() { }

        /// <summary>
        /// Initializes a new instance of the ListCard class.
        /// </summary>
        /// <param name="title">Title of the card</param>
        /// <param name="items">Array of items</param>
        /// <param name="buttons">Set of actions applicable to the current
        /// card</param>
        public ListCard(string title = default(string), System.Collections.Generic.IList<ListItemBase> items = default(System.Collections.Generic.IList<ListItemBase>), System.Collections.Generic.IList<CardAction> buttons = default(System.Collections.Generic.IList<CardAction>))
        {
            Title = title;
            Items = items;
            Buttons = buttons;
        }

        /// <summary>
        /// Gets or sets title of the card
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets array of items
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "items")]
        public System.Collections.Generic.IList<ListItemBase> Items { get; set; }

        /// <summary>
        /// Gets or sets set of actions applicable to the current card
        /// </summary>
        [Newtonsoft.Json.JsonProperty(PropertyName = "buttons")]
        public System.Collections.Generic.IList<CardAction> Buttons { get; set; }

    }
}
