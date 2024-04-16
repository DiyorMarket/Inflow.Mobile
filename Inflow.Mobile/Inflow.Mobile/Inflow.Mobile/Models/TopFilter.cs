namespace Inflow.Mobile.Models
{
    public class TopFilter
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public TopFilter(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
