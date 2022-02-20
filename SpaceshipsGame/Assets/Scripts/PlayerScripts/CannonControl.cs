using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonControl : MonoBehaviour
{
    private const float MovementSpeed = 5f;

    [SerializeField]
    private Transform muzzle;

    [SerializeField]
    private AudioClip shooting;

    private float coolDownTime = 0.3f;

    [SerializeField]
    private Bullet bulletPrefab;

    private float shootTimer;
    [SerializeField]
    private GameObject AllBullet;

    [SerializeField]
    private GameObject LeftPoint;
    [SerializeField]
    private GameObject RigthPoint;

    private float HalfWidthScene = Screen.width / 2;


    private void Update()
    {
        //Перемещение игрока влево-вправо
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            {
                if (touch.position.x < HalfWidthScene)
                {
                    if((transform.position.x - Time.deltaTime * MovementSpeed)> LeftPoint.transform.position.x)
                    transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * MovementSpeed;
                }
                else
                {
                    if((transform.position.x + Time.deltaTime * MovementSpeed) < RigthPoint.transform.position.x)
                    transform.position += new Vector3(1, 0, 0) * Time.deltaTime * MovementSpeed;
                }
            }
        }

        //пуля
        shootTimer += Time.deltaTime;
        if (shootTimer > coolDownTime)
        {
            shootTimer = 0f;

           Bullet NewBullet = Instantiate(bulletPrefab, muzzle.position, Quaternion.identity);
            NewBullet.transform.SetParent(AllBullet.transform);
            GameManager.Instance.PlaySfx(shooting);
        }
    }

}
