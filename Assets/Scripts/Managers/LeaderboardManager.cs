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
        private TextMeshProUGUI ScoresPrefab;
        [SerializeField]
        private TextMeshProUGUI NamesPrefab;

        private GameObject _namesHolder;
        private GameObject _scoresHolder;

        [SerializeField]
        private GameObject _errorMessage;
        [SerializeField]
        private GameObject _leaderBoard;
        
        [SerializeField]
        private ScoreManagerChannel scoreManagerChannel;

        private void Awake()
        {
            _scoresHolder = GameObject.FindWithTag("LeaderBoardScore");
            _namesHolder = GameObject.FindWithTag("LeaderBoardName");
            
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
                Debug.Log("Entries are non zero");
                OnLeaderBoardLoaded(currentEntries);
            }

        }

        public void UploadLeaderBoardEntry(TMP_InputField playerName)
        {
            int score = scoreManagerChannel.GetScore();
            string text = playerName.text.Trim().Normalize();
            leaderBoardChannel.UploadLeaderBoardEntry(text, score, OnLeaderBoardLoaded, OnLBFailed);
        }

        private void EmptyBoard()
        {
            foreach (Transform child in _namesHolder.transform)
            {
                Destroy(child.gameObject);
            }
            foreach (Transform child in _scoresHolder.transform)
            {
                Destroy(child.gameObject);
            }
        }
	
        private void OnLBFailed(string _ = "")
        {
            _errorMessage.SetActive(true);
            _leaderBoard.SetActive(false);
        }

        private void OnLeaderBoardLoaded(Dan.Models.Entry[] entry)
        {
            if (leaderBoardChannel.LeaderBoardFailed) return;
            EmptyBoard();
            for (int i = 0; i < entry.Length; ++i)
            {
                var newName = Instantiate(NamesPrefab, _namesHolder.transform);
                newName.GetComponent<TextMeshProUGUI>().text = entry[i].Username;
                var newScore = Instantiate(ScoresPrefab, _scoresHolder.transform);
                newScore.GetComponent<TextMeshProUGUI>().text = entry[i].Score.ToString();
            }
        } 
    }
}
