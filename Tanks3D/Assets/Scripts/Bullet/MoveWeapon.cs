using Assets.Scripts.Persons;
using Assets.Scripts.Player;
using UnityEngine;
using Zenject;

public class MoveWeapon : MonoBehaviour
{

    private Rigidbody RigidbodyWeapon;
     
    [HideInInspector]
    public Damage _damage;
    [HideInInspector]
    private HittingInPerson _hittingInPerson;
    [Inject]
    void Construct(Damage damage, HittingInPerson hittingInPerson)
    {
        _damage = damage;
        _hittingInPerson = hittingInPerson;
    }
    private void Start()
    {
        RigidbodyWeapon = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (RigidbodyWeapon.position.y > 0)
            RigidbodyWeapon.AddForce(transform.forward, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (_hittingInPerson._HittingInPerson(collision.transform.GetComponent<PlayerController>().health, _damage.DamageBullet, collision.transform.tag))
            { 
                Destroy(collision.gameObject); 
            }
        }
        else if (collision.transform.tag == "Enemy")
        {
            if (_hittingInPerson._HittingInPerson(collision.transform.GetComponent<EnemyAI>().health, _damage.DamageBullet, collision.transform.tag))
            {
                Destroy(collision.gameObject);
            }

        }
        Destroy(gameObject);

    }
}

