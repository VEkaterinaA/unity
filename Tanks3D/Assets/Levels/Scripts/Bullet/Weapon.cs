using Assets.Levels.Scripts.Persons;
using Assets.Levels.Scripts.Player;
using UnityEngine;

public class Weapon
{
    private HittingInPerson _hittingInPerson;

    private float maxDistance = 500;


    Weapon(HittingInPerson hittingInPerson)
    {
        _hittingInPerson = hittingInPerson;
    }
    public GameObject Shoot(Camera CameraPlayer, float damage)
    {
        RaycastHit hit;

        bool raycast = Physics.Raycast(CameraPlayer.transform.position, CameraPlayer.transform.forward, out hit, maxDistance);

        if (raycast)
        {

            if (hit.collider.tag == "Enemy")
            {
                Health health = hit.collider.transform.GetComponent<EnemyAI>().health;
                bool DeathPerson = _hittingInPerson._HittingInPerson(health, damage, "Enemy");

                if (DeathPerson)
                {
                    MainInstaller.AmountEnemy--;
                    if (MainInstaller.AmountEnemy < 1)
                    {
                        GameObject CanvasWin = GameObject.Find("/Scene/Win");
                        CanvasWin.transform.GetChild(0).gameObject.SetActive(true);
                    }
                    return hit.collider.gameObject;
                }
            }
            else if (hit.collider.transform.tag == "Player")
            {
                Health health = hit.collider.transform.GetComponent<PlayerController>().health;
                bool DeathPerson = _hittingInPerson._HittingInPerson(health, damage, "Player");

                if (DeathPerson)
                {
                    return hit.collider.gameObject;
                }
            }
        }
        return null;
    }
}
