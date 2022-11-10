using Assets.Levels.Scripts;
using Assets.Levels.Scripts.Persons;
using Assets.Levels.Scripts.Player;
using Zenject;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    private float HealthPlayer = 100;

    public static int AmountEnemy=0;
    public override void InstallBindings()
    {
        Debug.Log(SceneManager.GetActiveScene().buildIndex);
        BindInstallerBindings();

        BindEnemyFactory();

        BindMinimapCameraController();

        BindWeaponFactory();

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
        playerController.health = new Health();
        playerController.health.StartHealthPlayer = HealthPlayer;
        playerController.health.HealthPerson = playerController.health.StartHealthPlayer;

    }

    private void BindPerson()
    {
        Container
    .Bind<HittingInPerson>()
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
    private void BindWeaponFactory()
    {
        BindPerson();


        BindCharacterWeapon();

        Container
            .Bind<Weapon>()
            .AsTransient();

        Container
            .Bind<IFactory<WeaponStartPos>>()
            .To<WeaponFactory>()
            .AsSingle();

        //Container
        //    .Bind<CreateWeapon>()
        //    .AsSingle();
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
            AmountEnemy++;
        }

    }

}