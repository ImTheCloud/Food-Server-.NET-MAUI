using MongoDB.Bson.IO;
using System.Collections;
using System.Text.Json.Serialization;
using Windows.ApplicationModel;

namespace MyReference.Services;

public class MonkeyService : ContentPage
{
	public MonkeyService()
	{
	
	}

	public async Task<List<Monkey>> GetMonkeys()
    {
        List<Monkey> monkeys;

        using var stream = await FileSystem.OpenAppPackageFileAsync("monkeydata.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        monkeys = JsonSerializer.Deserialize<List<Monkey>>(contents);
        var resultlist = JsonConvert.DeserializeObject<Package[]>(contents);
        ArrayList monkeyslist = new ArrayList();
        monkeyslist.Add(resultlist.code);
        return monkeyslist;
    }
}