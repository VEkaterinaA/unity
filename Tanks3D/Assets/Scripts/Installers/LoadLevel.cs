using UnityEngine;
using Zenject;

namespace Assets.Scripts.Installers
{
    public class LoadLevel : MonoBehaviour
    {
        [SerializeField]
        private Camera Minimap;

        private PlayerController playerController;

        [Inject]
        void Constructor(PlayerController playerController)
        {
            this.playerController = playerController;
        }
        void Start()
        {
            Minimap.transform.SetParent(playerController.transform);
            Minimap.transform.localPosition = new Vector3(0,15,0);
        }

    }
}
