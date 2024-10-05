namespace MVCPractice2.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; } = null;
        
        //declaring Items as a List so EF Core automatically
        //sees it as a One to Many relationship
        public List<Item>? Items { get; set; }
    }
}
