using System.Globalization;

namespace MyApp.Model;

public class StudentModel
{
    public StudentModel(int id, String Surname, String Name, String Email, String Foto)
    {
        this.Id = id;
        this.Surname = Surname;
        this.Name = Name;
        this.Email = Email;
        this.Foto = Foto;

    }

    public StudentModel()
    { 
    }

    public int Id { get; set; }
    public string Surname { get; set; }

    public string Name { get; set; }

    public string Email { get; set; }

    public string Foto { get; set; }

}