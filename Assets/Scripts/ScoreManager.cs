using UnityEngine.Events;

namespace DefaultNamespace
{
    public class ScoreManager : Singleton<ScoreManager>, ILogger
    {
        public string Prefix => "分數管理器：";
        
        public int Score;
        public bool IsWin;

        public UnityEvent<int> OnScoreChanged = new UnityEvent<int>();

        public void SetIsWin(int winNeedScore)
        {
            IsWin = Score >= winNeedScore;
            string winStatus = IsWin ? "Win" : "Lose";
            Logger.Log(this, $"Game {winStatus}!!!");
        }

        public void InitScore()
        {
            SetScore(0);
        }

        public void AddScore(int add)
        {
            SetScore(Score + add);
        }

        private void SetScore(int score)
        {
            Score = score;
            OnScoreChanged.Invoke(score);
            Logger.Log(this, $"Current Score {Score}");
        }

        
    }
}