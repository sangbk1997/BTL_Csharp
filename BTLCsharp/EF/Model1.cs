namespace BTLCsharp.EF
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model1 : DbContext
    {
        public Model1()
            : base("name=Model1")
        {
        }

        public virtual DbSet<Audio> Audios { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(e => e.Audios)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.idUser);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Categories)
                .WithOptional(e => e.User)
                .HasForeignKey(e => e.idUser);
        }
    }
}
