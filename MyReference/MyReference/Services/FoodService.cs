using System.Collections;
using System.Text.Json.Serialization;

namespace MyReference.Services;

public class FoodService : ContentPage
{
	public FoodService()
	{
	
	}

    public async Task SetFoodJson()
    {
        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FoodServer", "foodData.json");
        using FileStream filestream = File.Create(filePath);

        await JsonSerializer.SerializeAsync(filestream, Globals.MyStaticList);
        await filestream.DisposeAsync();

    }

    public async Task<List<Food>> GetFood()
    {
        List<Food> food;

        string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "FoodServer", "foodData.json");

        using var stream = File.Open(filePath, FileMode.Open);
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        food = JsonSerializer.Deserialize<List<Food>>(contents);

        return food;
    }
}

