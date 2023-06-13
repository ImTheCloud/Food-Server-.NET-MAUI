namespace MyReference.ViewModel
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        public string title; // Titre de la vue mod�le

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy; // Indicateur d'activit�
        public bool IsNotBusy => !IsBusy; // Propri�t� calcul�e qui indique si le mod�le n'est pas occup�

        public BaseViewModel()
        {

        }
    }
}
