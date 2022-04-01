using Assets.Scripts;
using UnityEngine;
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
    private EnemyMarker[] EnemyMarkers;

    public override void InstallBindings()
    {
        BindInstallerBindings();
        
        BindEnemyFactory();

        BindWeaponFactory();

        BindPlayer();

        BindGameController();

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
        .Bind<MovePlayer>()
        .AsSingle();

        Container
            .Bind<CameraPosition>()
            .AsSingle();
        
        Container
           .Bind<HealthBar>()
           .AsSingle();

        PlayerController playerController = Container
            .InstantiatePrefabForComponent<PlayerController>(
            PrefabPlayer, StartPoint.position, Quaternion.identity, scene);

        Container
        .Bind<PlayerController>()
            .FromInstance(playerController)
            .AsSingle();
    }

    private void BindGameController()
    {
        Container
            .Bind<GameController>()
            .AsSingle();
    }

    private void BindEnemyFactory()
    {
        Container
            .Bind<IFactory<EnemyMarker>>()
            .To<EnemyFactory>()
            .AsSingle();
    }
    private void BindWeaponFactory()
    {
        Container
            .Bind<IFactory<WeaponMarker>>()
            .To<WeaponFactory>()
            .AsSingle();

        Container
            .Bind<CreateWeapon>()
            .AsSingle();
    }

    public void Initialize()
    {
        var enemyFactory = Container.Resolve<IFactory<EnemyMarker>>();

        enemyFactory.Load();
        foreach (var marker in EnemyMarkers)
        {
            enemyFactory.Create(marker, marker.transform.position);
        }
    }


}