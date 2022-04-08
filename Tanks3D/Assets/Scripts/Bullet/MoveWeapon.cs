using UnityEngine;
using Zenject;

public class MoveWeapon : MonoBehaviour
{

    private float speed = 5f;
    private Rigidbody RigidbodyWeapon;

    [HideInInspector]
    public Damage _damage;

    [Inject]
    void Construct(Damage damage)
    {
        _damage = damage;
    }
    private void Start()
    {
        RigidbodyWeapon = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (RigidbodyWeapon.position.y > 0)
            RigidbodyWeapon.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Health HealthPerson = collision.transform.GetComponent<Health>();
        if (HealthPerson != null)
        {
            Debug.Log(_damage.DamageBullet);
            HealthPerson.HittingInPerson(_damage.DamageBullet,collision.gameObject,collision.transform.tag);
            Destroy(gameObject);
        }
    }
}

