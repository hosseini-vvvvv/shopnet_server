namespace API.Entities
{
    public class CategoryTwo
    {
        public int id { get; set; }
        public string Name { get; set; }
        public ICollection<CategoryThree> CategoryThree { get; } = new List<CategoryThree>();

    }
}