namespace MVCPractice2.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = null;

        //connecting to the 3rd table or helpermodel
        //plural naming when using a List
        public List<UserItem> UserItems { get; set; }
    }
}
