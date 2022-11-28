namespace Client.DataBase.Models;

public class Account : DomainObject
{
    public User AccountHolder { get; set; }
    public ICollection<FilesInfo> FilesInfo { get; set; }
}
