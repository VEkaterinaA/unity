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
              var NewEnemy =  _diContainer.InstantiatePrefab(PrefabObject, StartPos, Quaternion.identity, null);
       
        var EnemyAIScript = NewEnemy.GetComponent<EnemyAI>();

        switch (enemyMarker.enemyType)
        {
            case EnemyType.Tank:
                EnemyAIScript.health = new Health(30f);
                EnemyAIScript.damage = new Damage(10f);
                break;
        }


    }
}

