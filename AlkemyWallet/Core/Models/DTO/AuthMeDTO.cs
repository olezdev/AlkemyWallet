﻿namespace AlkemyWallet.Core.Models.DTO;

public class AuthMeDTO
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public int? Points { get; set; }
    public string Role { get; set; }
}
