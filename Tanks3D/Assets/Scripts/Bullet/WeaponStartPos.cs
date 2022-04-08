
using UnityEngine;

public class WeaponStartPos : MonoBehaviour
{
    public WeaponType weaponType;
    public Transform weaponStartPosition;
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, 0.02f);
        Gizmos.color = Color.white;
    }

}
