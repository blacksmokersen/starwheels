using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

namespace SW.Matchmaking.Friends
{
    public class FriendGroupDisplayer : MonoBehaviour
    {
        // CALLBACKS

        protected Callback<LobbyChatUpdate_t> LobbyChatUpdatedCallback;

        // PUBLIC

        public void AddNewFriendEntry()
        {

        }

        public void RemovedFriendEntry()
        {

        }

        // PRIVATE

        private void OnLobbyChatUpdated(LobbyChatUpdate_t result)
        {
            if (result.m_rgfChatMemberStateChange == 0) // A user status has changed (quit, joined, etc.)
            {

            }
        }
    }
}
