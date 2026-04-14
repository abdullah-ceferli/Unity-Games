using SQLite;

public class UserAccount
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [Unique] // Prevents two people from having the same name
    public string Username { get; set; }

    public string Password { get; set; } // In a real game, you should hash this!
    public int Level { get; set; }
}