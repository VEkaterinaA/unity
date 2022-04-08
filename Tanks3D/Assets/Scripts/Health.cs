using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Health : MonoBehaviour
{
    public float HealthPerson;
    private float StartHealthPlayer;
    private HealthBar healthBar;
    
    [Inject]
    void Construct(HealthBar healthBar)
    {
        this.healthBar = healthBar;
    }
    private void Awake()
    {
        StartHealthPlayer = HealthPerson;
    }
    public void HittingInPerson(float Damage, Object person, string tag)
    {
        HealthPerson -= Damage;
        if (tag == "Player")
        {
            if (HealthPerson >= 0)
                HealthBarPlayer();

        }
        if (HealthPerson < 0 || HealthPerson == 0)
        {

            Destroy(person);
        }
    }

    void HealthBarPlayer()
    {
        float x = (HealthPerson * 100) / StartHealthPlayer;
        float fill = x / 100;
        if (fill > 0)
        {
            healthBar.ChangeFill(fill);
        }
        else
        {
            healthBar.ChangeFill(0);
            Debug.Log("Reloading scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
