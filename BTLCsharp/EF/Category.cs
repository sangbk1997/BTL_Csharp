namespace BTLCsharp.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Category")]
    public partial class Category
    {
        [Key]
        public int idCategory { get; set; }

        [StringLength(500)]
        public string nameCategory { get; set; }

        [Column("meta-Category")]
        [StringLength(500)]
        public string meta_Category { get; set; }

        public int? idUser { get; set; }

        [StringLength(1000)]
        public string briefIntroduce { get; set; }

        [StringLength(100)]
        public string urlImg { get; set; }

        public DateTime? addedDate { get; set; }

        public virtual User User { get; set; }
    }
}
