using UnityEngine;
using UnityEngine.EventSystems;

public class StartGame : MonoBehaviour
{
    public GameObject Menu;
    public Animation PlayerAnimation;
    public CannonControl PlayerMove;
    public GameObject GameController;
    public GameObject Shell;
    private void Update()
    {
        if (Input.GetKey(KeyCode.Space) || (Input.touchCount > 0) & !EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                Menu.SetActive(false);
                PlayerAnimation.enabled = true;
                Invoke("EnabledObject",1f);
            }
        }
    }
    private void EnabledObject()
    {
        PlayerMove.enabled = true;
        GameController.SetActive(true);
        Shell.SetActive(true);

    }


}
