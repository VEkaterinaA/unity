using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ProjectContextScript : MonoInstaller
{
    public override void InstallBindings()
    {
        Container
               .Bind<GlobalData>()
               .AsSingle()
               .NonLazy();
    }
    }
