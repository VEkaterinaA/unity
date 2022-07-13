using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ListMenu : MonoBehaviour
{
    private Button Button_Template;


    private void Start()
    {
        Button_Template = transform.GetChild(0).GetComponent<Button>();
        ListButtonMenu();
    }

    void ListButtonMenu()
    {
        AddRowMenu("Start game", StartGame_Click);
        AddRowMenu("Exit", ExitGame_Click);

        Destroy(Button_Template.gameObject);
    }
    void AddRowMenu(string text, UnityEngine.Events.UnityAction ClickButton_method)
    {
        Button AddButton = Instantiate(Button_Template, transform);
        AddButton.transform.SetSiblingIndex(0);
        AddButton.transform.GetChild(0).GetComponent<Text>().text = text;
        AddButton.onClick.AddListener(ClickButton_method);

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


