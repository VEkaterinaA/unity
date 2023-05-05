using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Assets.MainMenu.Scripts
{
    public class ViewModel : MonoBehaviour, INotifyPropertyChanged
    {
        private string pathUI = "Assets/MainMenu/MainMenuUI";

        private Model selectedModel;

        public ObservableCollection<Model> ListModel { get; set; }
        public Model SelectedModel
        {
            get { return selectedModel; }
            set
            {
                selectedModel = value;
                OnPropertyChanged("SelectedModel");
            }
        }
        private void Awake()
        {
            VisualTreeAsset ButtonTemplate = EditorGUIUtility.Load(pathUI + "/ButtonTemplate.uxml") as VisualTreeAsset;
            var UiDocument = GetComponent<UIDocument>();

            ViewModelBind(UiDocument.rootVisualElement, ButtonTemplate);
        }
        public void ViewModelBind(VisualElement root, VisualTreeAsset ButtonTemplate)
        {
            ListModel = new ObservableCollection<Model>()
            {
                new Model{ Name = "Start", NameWindowOpen="Level1"},
                new Model{ Name = "Option", NameWindowOpen="Option"},
                new Model{ Name = "Exit", NameWindowOpen="Exit"}
            };

            var listView = root.Q<ListView>();
            listView.itemsSource = ListModel;



            Func<VisualElement> makeItem = () => ButtonTemplate.Instantiate();

            listView.makeItem = makeItem;

            Action<VisualElement, int> bindItem = (element, i) =>
            {
                var button = element.Q<Button>();
                button.text = ListModel[i].Name;

                if (ListModel[i].NameWindowOpen == "Exit")
                {
                    button.clicked += () => Application.Quit();
                }
                else
                    button.clicked += () => SceneManager.LoadScene(ListModel[i].NameWindowOpen, LoadSceneMode.Single);
            };

            listView.bindItem = bindItem;

        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
