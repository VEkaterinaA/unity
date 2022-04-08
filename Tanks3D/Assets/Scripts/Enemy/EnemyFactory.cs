using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class EnemyFactory : IFactory<EnemyStartPos>
{
    public string PrefabName => "Enemy";
    public Object PrefabObject { get; set; }

    private readonly DiContainer _diContainer;

    public EnemyFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }
    public async Task Load()
    {
        PrefabObject = Resources.Load(PrefabName);
    }
    public async Task Create(EnemyStartPos enemyMarker, Vector3 StartPos)
    {
                _diContainer.InstantiatePrefab(PrefabObject, StartPos, Quaternion.identity, null);
    }
}

