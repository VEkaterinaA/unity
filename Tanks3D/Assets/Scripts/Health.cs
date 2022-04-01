using UnityEngine;
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
        healthBar.ChangeFill(fill);
    }
}
