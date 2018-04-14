namespace BTLCsharp.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Audio")]
    public partial class Audio
    {
        [Key]
        public int idAudio { get; set; }

        public int? idUser { get; set; }

        [StringLength(50)]
        public string Category { get; set; }

        [StringLength(50)]
        public string audioName { get; set; }

        [Column("meta-AudioName")]
        [StringLength(50)]
        public string meta_AudioName { get; set; }

        public int? level { get; set; }

        [StringLength(1000)]
        public string content { get; set; }

        [StringLength(100)]
        public string urlAudio { get; set; }

        [StringLength(500)]
        public string extraInfo { get; set; }

        public DateTime? addedDate { get; set; }

        public int? views { get; set; }

        public virtual User User { get; set; }
    }
}
