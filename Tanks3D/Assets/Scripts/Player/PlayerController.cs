using UnityEngine;
using Script.ExtensionMethods;
using Zenject;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Classes
    public MovePlayer motor;
    private CameraPosition cameraPosition;
    private CreateWeapon createWeapon;
    //**********
    public ClassCameraViewEnum.CameraViewTypes cameraView;//enum
    //CameraView
    [Header("CameraView")]
    [SerializeField]
    private Camera FirstCameraPlayer;
    [SerializeField]
    private Camera SecondCameraPlayer;
    [SerializeField]
    private Camera ThirdCameraPlayer;
    //**********
    private Rigidbody rigidbodyPlayer;
    //
    public WeaponMarker weaponMarker;
    //
    [SerializeField]
    private Image Aim;

    [Inject]
    public void Construct(MovePlayer movePlayer, CameraPosition cameraPosition, CreateWeapon weapon)
    {
        motor = movePlayer;
        this.cameraPosition = cameraPosition;
        createWeapon = weapon;
    }
    private void Start()
    {
        rigidbodyPlayer = gameObject.GetComponent<Rigidbody>();
        cameraView = ClassCameraViewEnum.CameraViewTypes.Third;
    }
    private void FixedUpdate()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");
        //movement player
        if (xMov != 0 || zMov != 0)
        {
            motor.PlayerMovement(xMov,zMov,rigidbodyPlayer, transform.forward);
        }
        //change camera view
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeCameraView();
            if (cameraView == ClassCameraViewEnum.CameraViewTypes.First)
            {
                Aim.enabled = true;
            }
            else Aim.enabled = false;
        }
        //shoot
        if (Input.GetKeyDown(KeyCode.Space))
        {
            createWeapon.Create(weaponMarker);

        }
    }
    internal void ChangeCameraView()
    {
      cameraPosition.ChangeCameraPosition(
          cameraView = cameraView.Next(),
          FirstCameraPlayer,
          SecondCameraPlayer,
          ThirdCameraPlayer); 

    }
    

}
