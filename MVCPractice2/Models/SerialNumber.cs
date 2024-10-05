using System.ComponentModel.DataAnnotations.Schema;

namespace MVCPractice2.Models
{
    public class SerialNumber
    {
        public int Id { get; set; }
        //giving an initial value of null
        public string Name { get; set; } = null;
        public int? ItemId { get; set; }
        [ForeignKey("ItemId")]

        //the ? means that it is nullable so that we dont have to connect it to
        //the Item model ASAP
        public Item? Item { get; set; }
    }
}
