using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class CreateWeapon
{

    private readonly DiContainer _diContainer;
    public CreateWeapon(DiContainer diContainer)
    {
        _diContainer = diContainer;
    }

    public async Task Create(WeaponStartPos marker)
    {
        var weaponFactory = _diContainer.Resolve<IFactory<WeaponStartPos>>();

       await weaponFactory.Load();
       await weaponFactory.Create(marker,marker.weaponStartPosition.position);
    }
}

