namespace BaltaDataAccess.Models
{
    public class CareerItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        // objeto do tipo complexo
        public Course Course { get; set; }
    }
}