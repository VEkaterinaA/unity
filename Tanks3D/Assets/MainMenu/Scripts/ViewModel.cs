using System.Collections.ObjectModel;
using Zenject;

namespace Assets.MainMenu.Scripts
{
    internal class ViewModel : PropertyChangedClass
    {
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

        public ViewModel()
        {

            ListModel = new ObservableCollection<Model>()
            {
                new Model{ Name = "Start", NameWindowOpen="LevelOne"},
                new Model{ Name = "Option", NameWindowOpen="Option"},
                new Model{ Name = "Exit", NameWindowOpen="Exit"}
            };
        }

    }
}
