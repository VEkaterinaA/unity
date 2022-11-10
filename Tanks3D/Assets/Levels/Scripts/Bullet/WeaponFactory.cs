using System.Threading.Tasks;
using UnityEngine;
using Zenject;

class WeaponFactory : IFactory<WeaponStartPos>
{
    public string PrefabName => "AP";
    public Object PrefabObject { get; set; }
    private readonly DiContainer _diContainer;

    public WeaponFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public async Task Create(WeaponStartPos weaponMarker, Vector3 StartPos)
    {
        var NewWeaponObject = _diContainer.InstantiatePrefab(PrefabObject, weaponMarker.transform);

        var NewWeaponMoveScript = NewWeaponObject.GetComponent<MoveWeapon>();
        

        NewWeaponMoveScript.transform.localPosition = Vector3.zero;
        NewWeaponMoveScript.transform.localRotation = Quaternion.identity;

        switch (weaponMarker.weaponType)
        {
            case WeaponType.TankRound:
                NewWeaponMoveScript._damage.DamageBullet = 10;
                break;
        }

    }

    public async Task Load()
    {
        PrefabObject = Resources.Load(PrefabName);
    }
}

