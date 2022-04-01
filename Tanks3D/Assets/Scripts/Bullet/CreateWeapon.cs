using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CreateWeapon
{

    private readonly DiContainer _diContainer;
    public CreateWeapon(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public void Create(WeaponMarker marker)
    {
        var weaponFactory = _diContainer.Resolve<IFactory<WeaponMarker>>();

        weaponFactory.Load();
        weaponFactory.Create(marker,marker.weaponStartPosition.position);
    }
}

