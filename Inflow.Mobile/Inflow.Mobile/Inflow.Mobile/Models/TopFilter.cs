namespace Inflow.Mobile.Models
{
    internal class TopFilter
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
