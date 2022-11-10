using Assets.Levels.Scripts.ExtensionMethods;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Assets.Levels.Scripts.Player
{

    public class PlayerController : MonoBehaviour
    {
        //Classes
        private MovePlayer _movePlayer;
        private CameraPosition _cameraPosition;
        private MinimapCameraController _miniMapCameraController;
        private Weapon _weapon;
        [HideInInspector]
        public Health health;
        [HideInInspector]
        public Damage damage = new Damage();
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

        private Camera CurrentCameraPlayer;
        [Inject]
        public void Construct(MovePlayer movePlayer, CameraPosition cameraPosition, MinimapCameraController miniMapCameraController, Weapon weapon)
        {
            _movePlayer = movePlayer;
            _cameraPosition = cameraPosition;
            _miniMapCameraController = miniMapCameraController;
            _weapon = weapon;
        }

        private void Awake()
        {
            rigidmodyPlayer = GetComponent<Rigidbody>();
            cameraView = ClassCameraViewEnum.CameraViewTypes.Third;
            _miniMapCameraController.StartMiniMapCameraPosition(transform);
            CurrentCameraPlayer = ThirdCameraPlayer;
            damage.DamageBullet = 10f;
        }

        private void FixedUpdate()
        {
            float xMov = Input.GetAxis("Horizontal");
            float zMov = Input.GetAxis("Vertical");
            //movement player
            if (xMov != 0 || zMov != 0)
            {
                _miniMapCameraController.ChangeMiniMapCameraPosition(transform);
                _movePlayer.PlayerMovement(xMov, zMov, transform.forward, rigidmodyPlayer);
            }
        }

        private void Update()
        {
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
            if (Input.GetMouseButtonUp(0))
            {
                _weapon.Shoot(CurrentCameraPlayer,damage.DamageBullet);
            }

        }
        public void ChangeCameraView()
        {
            _cameraPosition.ChangeCameraPosition(
                cameraView = cameraView.Next(),
                FirstCameraPlayer,
                SecondCameraPlayer,
                ThirdCameraPlayer,
                ref CurrentCameraPlayer
                );

        }


    }
}