namespace MyReference.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        public string title; // Titre de la vue modèle

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy; // Indicateur d'activité
        public bool IsNotBusy => !IsBusy; // Propriété calculée qui indique si le modèle n'est pas occupé

        public BaseViewModel()
        {

        }
    }
}
