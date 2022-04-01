using UnityEngine;

public class MoveWeapon : MonoBehaviour
{

    private float speed = 5f;
    private Rigidbody rigidbody;
    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (rigidbody.position.y > 0)
            rigidbody.AddForce(transform.forward * speed, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        Health HealthPerson = collision.transform.GetComponent<Health>();
        if (HealthPerson != null)
        {
            float damage = GetComponent<Damage>().DamageBullet;
            HealthPerson.HittingInPerson(damage,collision.gameObject,collision.transform.tag);
            Destroy(gameObject);
        }
    }
}
