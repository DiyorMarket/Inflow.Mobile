namespace Inflow.Mobile.Models
{
    public class TopFilter
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Title
        {
            get => Name.Substring(0, 10) + "...";
        }
        public double Width
        {
            get => Name.Length > 10 ? 100 : 50;
        }

        public TopFilter(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
