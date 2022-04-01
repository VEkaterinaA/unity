using UnityEngine;
using Zenject;

class WeaponFactory : IFactory<WeaponMarker>
{
    public string PrefabName => "AP";
    public Object PrefabObject { get; set; }
    private readonly DiContainer _diContainer;

    public WeaponFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public void Create(WeaponMarker weaponMarker, Vector3 StartPos)
    {
        var NewWeapon = _diContainer.InstantiatePrefab(PrefabObject, weaponMarker.transform);

        NewWeapon.transform.localPosition = Vector3.zero;
        NewWeapon.transform.localRotation = Quaternion.identity;

        Damage damage = NewWeapon.transform.GetComponent<Damage>();
        switch (weaponMarker.weaponType)
        {
            case WeaponType.TankRound:
                damage.DamageBullet = 10;
                break;
        }

    }

    public void Load()
    {
        PrefabObject = Resources.Load(PrefabName);
    }
}

