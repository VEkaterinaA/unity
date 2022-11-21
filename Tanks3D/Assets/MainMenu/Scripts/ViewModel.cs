using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
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
            VisualTreeAsset template = EditorGUIUtility.Load(pathUI + "/ButtonTemplate.uxml") as VisualTreeAsset;
            var UiDocument = GetComponent<UIDocument>();

            ViewModelBind(UiDocument.rootVisualElement, template);
        }
        public void ViewModelBind(VisualElement root, VisualTreeAsset ButtonTemplate)
        {
            ListModel = new ObservableCollection<Model>()
            {
                new Model{ Name = "Start", NameWindowOpen="LevelOne"},
                new Model{ Name = "Option", NameWindowOpen="Option"},
                new Model{ Name = "Exit", NameWindowOpen="Exit"}
            };

            var listView = root.Q<ListView>();
            listView.itemsSource = ListModel;

            Func<VisualElement> makeItem = () => new Button();

            listView.makeItem = makeItem;

            Action<VisualElement, int> bindItem = (VisualElement element, int index) =>
            (element as Button).text = ListModel[index].Name;

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
