using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    class ScoreText
    {
        private Text _ScorePlayer;
        public float _Score;
        public ScoreText(Text ScorePlayer, float BestScore)
        {
            _ScorePlayer = ScorePlayer;
            _Score = BestScore;

            _ScorePlayer.text = "Score: " + _Score;
        }

        public void ChangeScorePlayer(float heaithEnemy)
        {
            _Score += heaithEnemy;

            _ScorePlayer.text = "Score: " + _Score;


        }
    }
}
