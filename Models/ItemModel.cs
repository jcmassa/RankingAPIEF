using System.ComponentModel.DataAnnotations.Schema;

namespace RankingAPI.Models
{
    [Table("Item")]
    public class ItemModel
    {
        [Column("Id")]
        public int id { get; set; }
        [Column("titulo")]
        public string titulo { get; set; } = String.Empty;
        [Column("imageId")]
        public int ImageId { get; set; }
        [Column("ranking")]
        public int ranking { get; set; }
        [Column("itemType")]
        public int itemType { get; set; }
        [Column("tierId")]
        public int tierId { get; set; }
    }
}
