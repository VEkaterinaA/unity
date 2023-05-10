using Assets.Levels.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Levels.Scripts.Persons
{
    class HealthPerson
    {
        private HealthBar _healthBar;
        private ScoreText _scoreText;
        public HealthPerson(HealthBar healthBar, ScoreText scoreText)
        {
            _healthBar = healthBar;
            _scoreText = scoreText;
        }

        public void ChangeHealthEnemy(Health health, float damage)
        {
            health.healthPerson -= damage;
            Debug.Log(health.healthPerson);
            if (health.healthPerson <= 0)
            {
                _scoreText.ChangeScorePlayer(health.startHealthPerson);
            }
        }
        public void ChangeHealthPlayer(Health health, float damage)
        {
            health.healthPerson -= damage;
            Debug.Log(health.healthPerson);

            if (health.healthPerson >= 0)
            {
                HealthBarPlayer(health.healthPerson, health.startHealthPerson);
            }
            else
            {
                Restart();
            }
        }
        private void Restart()
        {
            _healthBar.ChangeFill(0);
            Debug.Log("Reloading scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
        private void HealthBarPlayer(float HealthPerson, float StartHealthPlayer)
        {
            float healthPersonPercent = (HealthPerson * 100) / StartHealthPlayer;
            float fill = healthPersonPercent / 100;

            if (fill > 0)
            {
                _healthBar.ChangeFill(fill);
            }
            else
            {
                Restart();
            }
        }

    }
}
