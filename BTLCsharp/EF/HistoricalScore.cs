namespace BTLCsharp.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HistoricalScore")]
    public partial class HistoricalScore
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int idUser { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(50)]
        public string dayLearned { get; set; }

        public int? score { get; set; }

        public int? seqDay { get; set; }

        public virtual User User { get; set; }
    }
}
