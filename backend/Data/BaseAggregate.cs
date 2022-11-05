using System.Globalization;

namespace backend.Data;

public class BaseAggregate
{
    public string? Id { get; set; }
    public int LastRevision { get; set; }
    public string? CreatedAt { get; set; }
    
    public void New()
    {
        this.Id = Guid.NewGuid().ToString();
        this.LastRevision = 1;
        this.CreatedAt = DateTime.Now.ToString(CultureInfo.GetCultureInfo("ru-RU"));
    }
}
