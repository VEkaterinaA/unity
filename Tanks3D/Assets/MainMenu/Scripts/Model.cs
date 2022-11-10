namespace Assets.MainMenu.Scripts
{
    internal class Model : PropertyChangedClass
    {
        private string name;
        private string nameWindowOpen;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public string NameWindowOpen
        {
            get { return nameWindowOpen; }
            set
            {
                nameWindowOpen = value;
                OnPropertyChanged("NameWindowOpen");
            }
        }
    }
}
