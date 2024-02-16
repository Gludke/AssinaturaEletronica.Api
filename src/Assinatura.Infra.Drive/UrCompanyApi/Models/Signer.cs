namespace Assinatura.Infra.Drive.UrCompanyApi.Models;

public class Signer
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }

    public Signer(string name, string email)
    {
        Id = Guid.NewGuid();
        Name = name;
        Email = email;
    }

}
