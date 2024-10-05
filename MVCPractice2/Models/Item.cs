using System.ComponentModel.DataAnnotations.Schema;

namespace MVCPractice2.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        //decraling foreign key and the model
        public int? SerialNumberId { get; set; }
        public SerialNumber SerialNumber { get; set; }

        //manually setting the foreign key and nullable property
        //nullable is optional, however doing so will ignore the compile warnings
        public int? CategoryId { get; set; }
        [ForeignKey("CategoryId")]

        public Category? Category { get; set; }


        //reference to the useritem model using one to many
        //plural naming when using a List
        public List<UserItem> UserItems { get; set; }
    }
}
