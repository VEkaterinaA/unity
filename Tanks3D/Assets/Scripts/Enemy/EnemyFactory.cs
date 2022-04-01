using UnityEngine;
using Zenject;

public class EnemyFactory : IFactory<EnemyMarker>
{
    public string PrefabName => "Enemy";
    public Object PrefabObject { get; set; }

    private readonly DiContainer _diContainer;

    public EnemyFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }
    public void Load()
    {
        PrefabObject = Resources.Load(PrefabName);
    }
    public void Create(EnemyMarker enemyMarker, Vector3 StartPos)
    {
                _diContainer.InstantiatePrefab(PrefabObject, StartPos, Quaternion.identity, null);
    }
}

