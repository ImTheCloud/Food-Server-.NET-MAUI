using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyReference.Services
{

    public partial class DeviceOrientationServices
    {

        public DeviceOrientationServices() { }
        // Constructeur de la classe DeviceOrientationServices

        public partial void ConfigureScanner();
        // Méthode partiellement définie pour configurer le scanner

        public class QueueBuffer : Queue
        {
            // Classe interne QueueBuffer dérivée de Queue

            public event EventHandler Changed;
            // Événement Changed qui se déclenche lorsque la file est modifiée

            protected virtual void OnChanged()
            {
                // Méthode protégée appelée lorsque la file est modifiée
                if (Changed != null)
                    Changed(this, EventArgs.Empty);
                // Vérifie si l'événement a des abonnés et le déclenche avec les arguments appropriés
            }

            public override void Enqueue(object item)
            {
                // Redéfinition de la méthode Enqueue pour ajouter un élément à la file
                base.Enqueue(item);
                // Appelle la méthode Enqueue de la classe de base (Queue) pour ajouter l'élément à la file
                OnChanged();
                // Appelle la méthode OnChanged pour signaler que la file a été modifiée
            }
        }
    }
}
