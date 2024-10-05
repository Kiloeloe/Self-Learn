namespace MVCPractice2.Models
{
    public class UserItem
    {

        //reffering each foreign keys
        public int ItemId { get; set; }
        public Item Item { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
