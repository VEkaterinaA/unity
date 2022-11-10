using UnityEngine;
using UnityEngine.UIElements;

public class ListMenu : MonoBehaviour
{
    private void Awake()
    {
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Start").clicked += StartGame_Click;
        root.Q<Button>("Exit").clicked += ExitGame_Click;
    }

        void StartGame_Click()
    {
        SceneTransition.SwitchToScene(1);
    }
    void ExitGame_Click()
    {
        Application.Quit();
    }
}


