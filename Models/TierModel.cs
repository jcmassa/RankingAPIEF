using System.ComponentModel.DataAnnotations.Schema;

namespace RankingAPI.Models
{
    [Table("Tiers")]
    public class TierModel
    {
        [Column("Id")]
        public int Id { get; set; }
        [Column("rowName")]
        public string RowName { get; set; } = String.Empty;
        [Column("rowNumber")]
        public int RowNumber { get; set; }
        [Column("numCells")]
        public int NumCells { get; set; }
    }
}
