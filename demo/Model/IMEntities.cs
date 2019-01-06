using demo;
using System.Data.Entity;

namespace IMModel
{
    public class IMEntities : DbContext
    {
        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public IMEntities() :
                base()
        {
            Configure();
        }

        public IMEntities(string nameOrConnectionString) :
                base(nameOrConnectionString)
        {
            Configure();
        }

        private void Configure()
        {
            Configuration.AutoDetectChangesEnabled = false;
            Configuration.LazyLoadingEnabled = true;
            Configuration.ProxyCreationEnabled = true;
            Configuration.ValidateOnSaveEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasMany(usr => usr.SentMessages)
                .WithRequired(msg => msg.Sender)
                .HasForeignKey(msg => msg.SenderId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(usr => usr.ReceivedMessages)
                .WithRequired(msg => msg.Receiver)
                .HasForeignKey(msg => msg.RecieverId)
                .WillCascadeOnDelete(false);
        }
    }
}
