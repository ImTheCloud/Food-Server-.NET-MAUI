using System.Text.Json;
namespace MyApp.Services;
public class StudentService
{
    List<StudentModel> students;
    public StudentService()
    { }
    public async Task<List<StudentModel>> GetStudents()
    {
        using var stream = await FileSystem.OpenAppPackageFileAsync("Student.json");
        using var reader = new StreamReader(stream);
        var contents = await reader.ReadToEndAsync();
        students = JsonSerializer.Deserialize<List<StudentModel>>(contents); 
        
        return students;

    }
}