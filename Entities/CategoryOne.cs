namespace API.Entities
{
    public class CategoryOne
    {
        public int id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryTwo> CategoryTwo { get; } = new List<CategoryTwo>();
    }
}