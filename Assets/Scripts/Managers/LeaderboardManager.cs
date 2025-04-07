using Dan.Models;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace ldjam_hellevator
{
    public class LeaderboardManager : MonoBehaviour
    {
        [FormerlySerializedAs("leaderBoardAPI")] [SerializeField]
        private LeaderBoardChannel leaderBoardChannel;

        [SerializeField]
        private TextMeshProUGUI scoresTextField;

        [SerializeField] private int maxEntries = 5;

        [SerializeField]
        private TextMeshProUGUI errorMessage;
        [SerializeField]
        private GameObject leaderBoard;
        
        [SerializeField]
        private ScoreManagerChannel scoreManagerChannel;

        private void Start()
        {
            MaybeFetchEntries();
            if (leaderBoardChannel.LeaderBoardFailed)
            {
                OnLBFailed();
            }
        }

        private void MaybeFetchEntries()
        {
            var currentEntries = leaderBoardChannel.GetCurrentEntries();
            if (currentEntries.Length == 0) {
                leaderBoardChannel.FetchLeaderBoard(OnLeaderBoardLoaded, OnLBFailed);
            } else {
                OnLeaderBoardLoaded(currentEntries);
            }

        }

        public void UploadLeaderBoardEntry(TMP_InputField playerName)
        {
            var score = scoreManagerChannel.GetScore();
            var text = playerName.text.Trim().Normalize();
            score = score > 0 ? score : 0;
            leaderBoardChannel.UploadLeaderBoardEntry(text, score, OnLeaderBoardLoaded, OnLBFailed);
        }

        private void EmptyBoard()
        {
            scoresTextField.text = "";
        }
	
        private void OnLBFailed(string msg = "")
        {
            errorMessage.text = msg;
            errorMessage.gameObject.SetActive(true);
            leaderBoard.SetActive(false);
        }

        private void OnLeaderBoardLoaded(Entry[] entry)
        {
            if (leaderBoardChannel.LeaderBoardFailed) return;
            EmptyBoard();
            var numEntries = Mathf.Min(entry.Length, maxEntries);
            for (int i = 0; i < numEntries; ++i)
            {
                var text = $"{i + 1}. {entry[i].Username} - {entry[i].Score}";
                scoresTextField.text += text + "\n";
            }
        } 
    }
}
