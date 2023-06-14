using System.Collections;
using System.Text.Json.Serialization;

namespace MyReference.Services
{
    
    public class FoodService : ContentPage
    {

        public FoodService()
        {
            // Constructeur de la classe FoodService
        }

        public async Task SetFoodJson() // ecrire les donnée dans le json
        {

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FoodServer", "foodData.json"); // Chemin du fichier de données JSON des aliments
            using FileStream filestream = File.Create(filePath);   // Utilisation d'un bloc 'using' pour créer un flux de fichier et l'ouvrir en mode création
            await JsonSerializer.SerializeAsync(filestream, Globals.MyStaticList);
            // Sérialisation asynchrone de la liste statique 'MyStaticList' en format JSON et écriture dans le flux de fichier
            await filestream.DisposeAsync();
        }

        public async Task<List<Food>> GetFood() // recupere les infos du json
        {
            List<Food> food;
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FoodServer", "foodData.json"); // Chemin du fichier de données JSON des aliments
            using var stream = File.Open(filePath, FileMode.Open);  // Utilisation d'un bloc 'using' pour ouvrir le fichier en mode lecture
            using var reader = new StreamReader(stream); // Utilisation d'un bloc 'using' pour créer un lecteur de flux de fichier
            var contents = await reader.ReadToEndAsync();  
            food = JsonSerializer.Deserialize<List<Food>>(contents); // Désérialisation du contenu JSON dans la liste 'food'
            return food;
         
        }
    }
}
