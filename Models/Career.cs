using Microsoft.Identity.Client;

namespace BaltaDataAccess.Models
{
    public class Career
    {
        public Career()
        {
            CarrerItems = new List<CareerItem>();
        }
        
    public Guid Id { get; set; }
    public string Title { get; set; }

    // uma lista e uma propriedade
    public IList<CareerItem> CarrerItems { get; set; }

    }
}
    
