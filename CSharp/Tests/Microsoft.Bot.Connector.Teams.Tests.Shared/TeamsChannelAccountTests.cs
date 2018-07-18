﻿// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license.
//
// Microsoft Bot Framework: http://botframework.com
// Microsoft Teams: https://dev.office.com/microsoft-teams
//
// Bot Builder SDK GitHub:
// https://github.com/Microsoft/BotBuilder
//
// Bot Builder SDK Extensions for Teams
// https://github.com/OfficeDev/BotBuilder-MicrosoftTeams
//
// Copyright (c) Microsoft Corporation
// All rights reserved.
//
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

namespace Microsoft.Bot.Connector.Teams.Tests.Shared
{
    using System.IO;

    using Microsoft.Bot.Connector.Teams.Models;
    using VisualStudio.TestTools.UnitTesting;
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// TeamsChannelAccount tests
    /// </summary>
    [TestClass]
    public class TeamsChannelAccountTests
    {
        /// <summary>
        /// Parse out the AAD Object Id from a conversationUpdate event.
        /// </summary>
        [TestMethod]
        public void AsTeamsChannelAccount_ParsesAADIdCorrectlyFromConversationUpdate()
        {
            Activity sampleActivity = JsonConvert.DeserializeObject<Activity>(File.ReadAllText(@"Jsons\SampleActivityConversationUpdateWithAADObjectId.json"));

            ChannelAccount user = sampleActivity.From;
            TeamsChannelAccount teamsUser = user.AsTeamsChannelAccount();

            Assert.IsNotNull(teamsUser);
            Assert.IsNotNull(teamsUser.ObjectId);
        }

        /// <summary>
        /// Parse out the AAD Object Id from a message event.
        /// </summary>
        [TestMethod]
        public void AsTeamsChannelAccount_ParsesAADIdCorrectlyFromMessage()
        {
            Activity sampleActivity = JsonConvert.DeserializeObject<Activity>(File.ReadAllText(@"Jsons\SampleActivityMessageWithAADObjectId.json"));

            ChannelAccount user = sampleActivity.From;
            TeamsChannelAccount teamsUser = user.AsTeamsChannelAccount();

            Assert.IsNotNull(teamsUser);
            Assert.IsNotNull(teamsUser.ObjectId);
        }

        /// <summary>
        /// Parse out the AAD Object Id from a roster response.
        /// </summary>
        [TestMethod]
        public void AsTeamsChannelAccount_ParsesAADIdCorrectlyFromRoster()
        {
            TeamsChannelAccount[] memberList = JsonConvert.DeserializeObject<TeamsChannelAccount[]>(File.ReadAllText(@"Jsons\SampleResponseGetTeamsConversationMembers.json"));

            Assert.IsTrue(memberList.Count() == 2);
            Assert.IsFalse(memberList.Any(member => string.IsNullOrEmpty(member.ObjectId)));
            Assert.IsFalse(memberList.Any(member => string.IsNullOrEmpty(member.UserPrincipalName)));
            Assert.IsFalse(memberList.Any(member => string.IsNullOrEmpty(member.Id)));
            Assert.IsFalse(memberList.Any(member => string.IsNullOrEmpty(member.Email)));
            Assert.IsFalse(memberList.Any(member => string.IsNullOrEmpty(member.Name)));
        }
    }
}
