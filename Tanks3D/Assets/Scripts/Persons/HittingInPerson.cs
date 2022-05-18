using Assets.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Persons
{
    class HittingInPerson
    {
        private HealthBar _healthBar;
        private ScoreText _scoreText;
        public HittingInPerson(HealthBar healthBar, ScoreText scoreText)
        {
            _healthBar = healthBar;
            _scoreText = scoreText;
        }

        public bool _HittingInPerson(Health health,float Damage, string tag)
        {
            health.HealthPerson -= Damage;

            if (tag == "Player")
            {
                if (health.HealthPerson >= 0)
                    HealthBarPlayer(health.HealthPerson, health.StartHealthPlayer);
                return false;
            }
            if (health.HealthPerson < 0 || health.HealthPerson == 0)
            {
                _scoreText.ChangeScorePlayer(health.StartHealthPlayer);
                return true;
            }
            return false;
        }

        void HealthBarPlayer(float HealthPerson, float StartHealthPlayer)
        {
            float x = (HealthPerson * 100) / StartHealthPlayer;
            float fill = x / 100;
            if (fill > 0)
            {
                _healthBar.ChangeFill(fill);
            }
            else
            {
                _healthBar.ChangeFill(0);
                Debug.Log("Reloading scene");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }
}
