using Assets.Levels.Scripts.Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

public class Health
{

    public float startHealthPerson;
    public float healthPerson;

    public Health(float startHealthPerson)
    {
        this.startHealthPerson = startHealthPerson;
        this.healthPerson = startHealthPerson;
    }
}
