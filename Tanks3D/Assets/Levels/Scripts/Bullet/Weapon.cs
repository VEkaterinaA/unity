using Assets.Levels.Scripts.Persons;
using Assets.Levels.Scripts.Player;
using UnityEngine;

public class Weapon
{
    private HealthPerson _healthPerson;

    private float maxDistance = 500;

    Weapon(HealthPerson healthPerson)
    {
        _healthPerson = healthPerson;
    }

    public void Shoot(PlayerController playerController)
    {
        RaycastHit hitPoint;
        var PlayerRayStart = playerController.weaponMarker.transform;

        bool raycast = Physics.Raycast(PlayerRayStart.position, PlayerRayStart.forward, out hitPoint, maxDistance);

        if (!raycast)
        {
            return;
        }
        var enemy = hitPoint.collider.GetComponent<EnemyAI>();

        if (enemy == null)
        {
            return;
        }
        Health health = enemy.health;

        if (health == null)
        {
            return;
        }
        _healthPerson.ChangeHealthEnemy(health, playerController.damage.DamageBullet);


    }
    public void Shoot(EnemyAI enemyAI)
    {
        RaycastHit hitPoint;
        var EnemyRayStart = enemyAI.weaponMarker.transform;

        bool raycast = Physics.Raycast(EnemyRayStart.position, EnemyRayStart.forward, out hitPoint, maxDistance);

        if (!raycast)
        {
            return;
        }
        var player = hitPoint.collider.GetComponent<PlayerController>();

        if (player == null)
        {
            return;
        }

        Health health = player.health;

        if (health == null)
        {
            return;
        }
        _healthPerson.ChangeHealthPlayer(health, enemyAI.damage.DamageBullet);
    }
}
