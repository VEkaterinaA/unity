using UnityEngine;
using Script.ExtensionMethods;
using Zenject;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Classes
    private MovePlayer _movePlayer;
    private CameraPosition _cameraPosition;
    private CreateWeapon _createWeapon;
    private MinimapCameraController _miniMapCameraController;
    //**********
    private ClassCameraViewEnum.CameraViewTypes cameraView;//enum
    //CameraView
    [Header("Camera View")]
    [SerializeField]
    private Camera FirstCameraPlayer;
    [SerializeField]
    private Camera SecondCameraPlayer;
    [SerializeField]
    private Camera ThirdCameraPlayer;
    //
    [Header("Start Position Bullet")]
    public WeaponStartPos weaponMarker;
    //
    [HideInInspector]
    public Image Aim;
    //
    private Rigidbody rigidmodyPlayer;

    [Inject]
    public void Construct(MovePlayer movePlayer, CameraPosition cameraPosition, CreateWeapon createWeapon, MinimapCameraController miniMapCameraController)
    {
        _movePlayer = movePlayer;
        _cameraPosition = cameraPosition;
        _createWeapon = createWeapon;
        _miniMapCameraController = miniMapCameraController;
    }

    private void Awake()
    {
        rigidmodyPlayer = GetComponent<Rigidbody>();
        cameraView = ClassCameraViewEnum.CameraViewTypes.Third;
        _miniMapCameraController.StartMiniMapCameraPosition(transform);
    }

    private async void FixedUpdate()
    {
        float xMov = Input.GetAxis("Horizontal");
        float zMov = Input.GetAxis("Vertical");
        //movement player
        if (xMov != 0 || zMov != 0)
        {
            _miniMapCameraController.ChangeMiniMapCameraPosition(transform);
            _movePlayer.PlayerMovement(xMov,zMov, transform.forward, rigidmodyPlayer);
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
            await _createWeapon.Create(weaponMarker);

        }
    }

    public void ChangeCameraView()
    {
      _cameraPosition.ChangeCameraPosition(
          cameraView = cameraView.Next(),
          FirstCameraPlayer,
          SecondCameraPlayer,
          ThirdCameraPlayer); 

    }
    

}
