using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Xml;

public static class UserManager
{
    public static List<User> Users = new List<User>();
    public static string FilePath = "users.json";

    public static void LoadUsers()
    {
        if (File.Exists(FilePath))
        {
            var json = File.ReadAllText(FilePath);
            Users = JsonConvert.DeserializeObject<List<User>>(json) ?? new List<User>();
        }
    }

    public static void SaveUsers()
    {
        var json = JsonConvert.SerializeObject(Users, Newtonsoft.Json.Formatting.Indented);

        File.WriteAllText(FilePath, json);
    }

    public static User FindUser(string username, string password)
    {
        return Users.Find(u => u.Username == username && u.Password == password);
    }

    public static bool IsUsernameTaken(string username)
    {
        return Users.Exists(u => u.Username == username);
    }
}
