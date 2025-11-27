using System;

public class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public int Balance { get; set; } = 1000; // стартовий баланс

    public DateTime RegistrationDate { get; set; } = DateTime.Now; // дата реєстрації

    public int Slot1Plays { get; set; } = 0;  // скільки разів запускали слот 1
    public int Slot2Plays { get; set; } = 0;  // скільки разів запускали слот 2
    public int Slot3Plays { get; set; } = 0;  // скільки разів запускали слот 3
}
