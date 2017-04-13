﻿namespace Microsoft.Bot.Connector.Teams
{
    using System;
    using System.Collections.Generic;
    using Models;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Location at which AtMention should be added in text.
    /// </summary>
    public enum MentionTextLocation
    {
        /// <summary>
        /// Adds mention to start of text. Note this edits Text property.
        /// </summary>
        PrependText,

        /// <summary>
        /// Adds mention to end of text. Note this edits Text property.
        /// </summary>
        AppendText
    }

    /// <summary>
    /// Activity extensions.
    /// </summary>
    public static class ActivityExtensions
    {
        /// <summary>
        /// The members added event name.
        /// </summary>
        private const string MembersAddedEventName = "teamMemberAdded";

        /// <summary>
        /// The members removed event name.
        /// </summary>
        private const string MembersRemovedEventName = "teamMemberRemoved";

        /// <summary>
        /// The channel created event name.
        /// </summary>
        private const string ChannelCreatedEventName = "channelCreated";

        /// <summary>
        /// The channel deleted event name.
        /// </summary>
        private const string ChannelDeletedEventName = "channelDeleted";

        /// <summary>
        /// The channel renamed event name.
        /// </summary>
        private const string ChannelRenamedEventName = "channelRenamed";

        /// <summary>
        /// The team renamed event name.
        /// </summary>
        private const string TeamRenamedEventName = "teamRenamed";

        /// <summary>
        /// Adds the mention text to an existing activity.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <param name="mentionedUser">The mentioned user.</param>
        /// <param name="textLocation">Location at which AtMention text should be added to text.</param>
        /// <param name="mentionText">The mention text.</param>
        /// <returns>Activity with added mention.</returns>
        /// <exception cref="Rest.ValidationException">Either mentioned user name or mentionText must have a value</exception>
        public static Activity AddMentionToText(
            this Activity activity,
            ChannelAccount mentionedUser,
            MentionTextLocation textLocation = MentionTextLocation.PrependText,
            string mentionText = null)
        {
            if (mentionedUser == null || string.IsNullOrEmpty(mentionedUser.Id))
            {
                throw new ArgumentNullException("mentionedUser", "Mentioned user and user ID cannot be null");
            }

            if (string.IsNullOrEmpty(mentionedUser.Name) && string.IsNullOrEmpty(mentionText))
            {
                throw new ArgumentException("Either mentioned user name or mentionText must have a value");
            }

            string mentionEntityText = string.Format("<at>{0}</at>", mentionText == null ? mentionedUser.Name : mentionText);

            if (textLocation == MentionTextLocation.AppendText)
            {
                activity.Text = activity.Text + " " + mentionEntityText;
            }
            else
            {
                activity.Text = mentionEntityText + " " + activity.Text;
            }

            if (activity.Entities == null)
            {
                activity.Entities = new List<Entity>();
            }

            activity.Entities.Add(new Mention()
            {
                Text = mentionEntityText,
                Mentioned = mentionedUser
            });

            return activity;
        }

        /// <summary>Gets the conversation update data.</summary>
        /// <param name="activity">The activity.</param>
        /// <returns>Channel event data.</returns>
        /// <exception cref="Exception">
        /// Failed to process channel data in Activity
        /// or
        /// ChannelData missing in Activity
        /// </exception>
        public static TeamEventBase GetConversationUpdateData(this Activity activity)
        {
            if (activity.GetActivityType() != ActivityTypes.ConversationUpdate)
            {
                throw new ArgumentException("activity must be a ConversationUpdate");
            }

            if (activity.ChannelData != null)
            {
                TeamsChannelData channelData = activity.GetChannelData<TeamsChannelData>();

                if (!string.IsNullOrEmpty(channelData?.EventType))
                {
                    switch (channelData.EventType)
                    {
                        case MembersAddedEventName:
                            return new MembersAddedEvent
                            {
                                MembersAdded = activity.MembersAdded,
                                Team = channelData.Team,
                                Tenant = channelData.Tenant
                            };
                        case MembersRemovedEventName:
                            return new MembersRemovedEvent
                            {
                                MembersRemoved = activity.MembersRemoved,
                                Team = channelData.Team,
                                Tenant = channelData.Tenant
                            };
                        case ChannelCreatedEventName:
                            return new ChannelCreatedEvent
                            {
                                Channel = channelData.Channel,
                                Team = channelData.Team,
                                Tenant = channelData.Tenant
                            };
                        case ChannelDeletedEventName:
                            return new ChannelDeletedEvent
                            {
                                Channel = channelData.Channel,
                                Team = channelData.Team,
                                Tenant = channelData.Tenant
                            };
                        case ChannelRenamedEventName:
                            return new ChannelRenamedEvent
                            {
                                Channel = channelData.Channel,
                                Team = channelData.Team,
                                Tenant = channelData.Tenant
                            };
                        case TeamRenamedEventName:
                            return new TeamRenamedEvent
                            {
                                Tenant = channelData.Tenant,
                                Team = channelData.Team
                            };
                    }
                }

                throw new ArgumentException("Failed to process channel data in Activity");
            }
            else
            {
                throw new ArgumentNullException("Activity.ChannelData", "ChannelData missing in Activity");
            }
        }

        /// <summary>
        /// Gets the general channel for a team.
        /// </summary>
        /// <param name="activity">The activity.</param>
        /// <returns>Channel data for general channel.</returns>
        /// <exception cref="ArgumentException">Failed to process channel data in Activity</exception>
        /// <exception cref="ArgumentNullException">ChannelData missing in Activity</exception>
        public static ChannelInfo GetGeneralChannel(this Activity activity)
        {
            if (activity.ChannelData != null)
            {
                TeamsChannelData channelData = activity.GetChannelData<TeamsChannelData>();

                if (channelData != null && channelData.Team != null)
                {
                    return new ChannelInfo
                    {
                        Id = channelData.Team.Id,
                    };
                }

                throw new ArgumentException("Failed to process channel data in Activity. ChannelData is missing Team property.");
            }
            else
            {
                throw new ArgumentException("ChannelData missing in Activity");
            }
        }

        /// <summary>
        /// Creates a reply for the General channel of the team.
        /// </summary>
        /// <param name="activity">Incoming activity.</param>
        /// <param name="text">Reply text.</param>
        /// <param name="locale">Locale information.</param>
        /// <returns>New reply activity with General channel channel data.</returns>
        public static Activity CreateReplyToGeneralChannel(this Activity activity, string text = null, string locale = null)
        {
            TeamsChannelData channelData = activity.GetChannelData<TeamsChannelData>();
            Activity replyActivity = activity.CreateReply(text, locale);

            replyActivity.ChannelData = JObject.FromObject(new TeamsChannelData
            {
                Channel = activity.GetGeneralChannel(),
                Team = channelData.Team,
                Tenant = channelData.Tenant
            });

            return replyActivity;
        }

        /// <summary>Gets the tenant identifier.</summary>
        /// <param name="activity">The activity.</param>
        /// <returns>Tenant Id of the user who send the message.</returns>
        /// <exception cref="ArgumentException">Failed to process channel data in Activity</exception>
        /// <exception cref="ArgumentNullException">ChannelData missing in Activity</exception>
        public static string GetTenantId(this Activity activity)
        {
            if (activity.ChannelData != null)
            {
                TeamsChannelData channelData = activity.GetChannelData<TeamsChannelData>();

                if (!string.IsNullOrEmpty(channelData?.Tenant?.Id))
                {
                    return channelData.Tenant.Id;
                }

                throw new ArgumentException("Failed to process channel data in Activity");
            }
            else
            {
                throw new ArgumentNullException("ChannelData missing in Activity");
            }
        }
    }
}
