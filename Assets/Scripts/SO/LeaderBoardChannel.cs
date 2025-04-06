using UnityEngine;
using Dan.Main;
using Dan.Models;
using System;
using Unity.VisualScripting;

namespace ldjam_hellevator
{
    [CreateAssetMenu(fileName = "LeaderBoardChannel", menuName = "Scriptable Objects/LeaderBoardChannel", order = 1)]
    public class LeaderBoardChannel : ScriptableObject
    {
        private string lbPubKey = "57cb01de3a1b37958cb52f6719a8c65375b8297dc0e9edede312e5b43c1d91bb";

        private Entry[] entries = null;
        private string lastUserName = null;

        public string LastUserName
        {
            private set => lastUserName = value;
            get => lastUserName != null ? lastUserName : "";
        }

        [field: SerializeField] public bool LeaderBoardFailed { get; private set; } = false;

        public void FetchLeaderBoard()
        {
            LeaderboardCreator.GetLeaderboard(lbPubKey, (_entries) =>
            {
                entries = _entries;
                UpdatePlayerName();
                LeaderBoardFailed = false;
            }, (_) =>
            {
                LeaderBoardFailed = true;
                throw new System.Exception("Failed to fetch leaderboard");
            });
        }

        public void FetchLeaderBoard(Action<Entry[]> Callback, Action<string> ErrorCallback)
        {
            if (LeaderBoardFailed)
            {
                ErrorCallback("");
                return;
            }

            LeaderboardCreator.GetLeaderboard(lbPubKey, (_entries) =>
            {
                entries = _entries;
                Callback(entries);
                UpdatePlayerName();
                LeaderBoardFailed = false;
            }, ErrorCallback);
        }

        private void UpdatePlayerName()
        {
            if (LeaderBoardFailed) return;
            LeaderboardCreator.GetPersonalEntry(lbPubKey, (entry) => { LastUserName = entry.Username; });
        }

        public Entry[] GetCurrentEntries()
        {
            if (entries == null)
            {
                return new Entry[0];
            }

            return entries;
        }

        public void UploadLeaderBoardEntry(string playerName, int playerScore, Action<Entry[]> Callback,
            Action<string> ErrorCallback = null)
        {
            if (LeaderBoardFailed)
            {
                ErrorCallback?.Invoke("");
                return;
            }

            if (!string.IsNullOrWhiteSpace(playerName))
            {
                LastUserName = playerName;
            }

            LeaderboardCreator.UploadNewEntry(lbPubKey, LastUserName, playerScore,
                (_) => FetchLeaderBoard(Callback, ErrorCallback));
        }
    }
}