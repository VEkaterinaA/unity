using Assets.Levels.Scripts;
using Assets.Levels.Scripts.Persons;
using Assets.Levels.Scripts.Player;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    [SerializeField]
    private Text ScoreText;

    private float BestScore;

    public static int AmountEnemy = 0;
    public override void InstallBindings()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        BindInstallerBindings();

        BindEnemyFactory();

        BindMinimapCameraController();

        BindPerson();

        BindWeapon();

        BindPlayer();

        BindGameMenu();
    }

    private void BindGameMenu()
    {
        Container
            .Bind<GameMenuController>()
            .AsSingle();
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
        LoadDataGame();

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

    private void BindPerson()
    {
        Container
    .Bind<HealthPerson>()
    .AsSingle();

    }
    private void LoadDataGame()
    {
        BestScore = PlayerPrefs.GetFloat("Score");

        Container
    .Bind<ScoreText>()
    .AsSingle()
    .WithArguments(ScoreText, BestScore);


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
    private void BindWeapon()
    {
        Container
    .Bind<Weapon>()
    .AsSingle();
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
            AmountEnemy++;
        }

    }

}