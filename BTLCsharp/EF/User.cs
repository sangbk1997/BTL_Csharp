namespace BTLCsharp.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            Audios = new HashSet<Audio>();
            Categories = new HashSet<Category>();
        }

        public int id { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [Column("meta-username")]
        [StringLength(50)]
        public string meta_username { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(50)]
        public string password { get; set; }

        public int? age { get; set; }

        [StringLength(50)]
        public string address { get; set; }

        public DateTime? joinDate { get; set; }

        public int? modeaccess { get; set; }

        [StringLength(500)]
        public string extraInfo { get; set; }

        public int? score { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Audio> Audios { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Category> Categories { get; set; }
    }
}