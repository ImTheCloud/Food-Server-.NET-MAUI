using System.Collections;
using System.Text.Json.Serialization;

namespace MyReference.Services;

public class FoodService : ContentPage
{
	public FoodService()
	{
	
	}

    public async Task<List<Food>> GetFood()
    {
        List<Food> food;

        using var stream = await FileSystem.OpenAppPackageFileAsync("foodData.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        food = JsonSerializer.Deserialize<List<Food>>(contents);

        return food;
    }
}

