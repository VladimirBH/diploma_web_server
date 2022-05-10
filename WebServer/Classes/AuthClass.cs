using System.ComponentModel.DataAnnotations;

namespace WebServer.Classes;

public class AuthClass
{
    [Required(ErrorMessage = "Не указан логин")]
    public string login { set; get; }
    
    [Required(ErrorMessage = "Не указан пароль")]
    public string password { set; get; }
}