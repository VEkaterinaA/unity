using UnityEngine;
using Zenject;

public class GameController : MonoBehaviour
{
    //Classes
    private PlayerController playerController;
    //
    [SerializeField]
    private Camera MinimapCamera;

    [Inject]
    void Construct(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    private void Update()
    {
        if (Input.GetAxis("Vertical") != 0)
            MinimapCamera.transform.position = new Vector3(playerController.transform.position.x, 15f, playerController.transform.position.z);

        if (Input.GetAxis("Horizontal") != 0)
            MinimapCamera.transform.rotation = Quaternion.Euler(90, 0, playerController.transform.rotation.z);
        

    }
}
