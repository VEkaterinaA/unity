using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

class MainInstaller : MonoInstaller, IInitializable
{
    [SerializeField]
    private GameObject PrefabPlayer;
    [SerializeField]
    private Transform StartPoint;
    [SerializeField]
    private Transform scene;
    [SerializeField]
    private EnemyStartPos[] EnemyMarkers;
    [SerializeField]
    private Image imageHealth, imageAim;
    [SerializeField]
    private Camera MiniMapCamera;
    public override void InstallBindings()
    {
        BindInstallerBindings();

        BindEnemyFactory();

        BindWeaponFactory();

        BindMinimapCameraController();

        BindPlayer();


    }

    private void BindInstallerBindings()
    {
        Container
            .BindInterfacesTo<MainInstaller>()
            .FromInstance(this)
            .AsSingle();
    }
    private void BindPlayer()
    {
        Container
            .Bind<HealthBar>()
            .AsSingle()
            .WithArguments(imageHealth);

        Container
            .Bind<MovePlayer>()
            .AsSingle();

        Container
            .Bind<CameraPosition>()
            .AsSingle();

        PlayerController playerController = Container
            .InstantiatePrefabForComponent<PlayerController>(
            PrefabPlayer, StartPoint.position, Quaternion.identity, scene);

        Container
        .Bind<PlayerController>()
            .FromInstance(playerController)
            .AsSingle();

        playerController.Aim = imageAim;
    }

    private void BindMinimapCameraController()
    {
        Container
            .Bind<MinimapCameraController>()
            .AsSingle()
            .WithArguments(MiniMapCamera);
    }

    private void BindEnemyFactory()
    {
        Container
            .Bind<IFactory<EnemyStartPos>>()
            .To<EnemyFactory>()
            .AsSingle();
    }
    private void BindWeaponFactory()
    {
        BindCharacterWeapon();

        Container
            .Bind<IFactory<WeaponStartPos>>()
            .To<WeaponFactory>()
            .AsSingle();

        Container
            .Bind<CreateWeapon>()
            .AsSingle();
    }
    private void BindCharacterWeapon()
    {
        Container
            .Bind<Damage>()
            .AsTransient();
    }

    public async void Initialize()
    {
       await LoadEnemy();
    }

    public async Task LoadEnemy()
    {
        var enemyFactory = Container.Resolve<IFactory<EnemyStartPos>>();

        await enemyFactory.Load();
        foreach (var marker in EnemyMarkers)
        {
            await enemyFactory.Create(marker, marker.transform.position);
        }

    }

}