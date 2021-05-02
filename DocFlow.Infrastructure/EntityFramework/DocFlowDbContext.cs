using Microsoft.EntityFrameworkCore;
using DocFlow.Core.Domain;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;

namespace DocFlow.Infrastructure.EntityFramework
{
    public class DocFlowDbContext : ApiAuthorizationDbContext<UserEntity>
    {

        public DocFlowDbContext(DbContextOptions options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
              Database.EnsureCreated();
        }

        public DbSet<ApplicationEntity> Applications { get; set; }
        public DbSet<TransactionItem> TransactionItems { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<StatusEntity> Statuses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Primmary Keys
            modelBuilder.Entity<StatusEntity>().HasKey(status => status.Id);
            modelBuilder.Entity<TransactionEntity>().HasKey(transaction => transaction.Id);
            modelBuilder.Entity<UserEntity>().HasKey(user => user.Id);
            modelBuilder.Entity<ApplicationEntity>().HasKey(applicaiton => applicaiton.Id);

            // Relations

            // -- Transaction Model

            modelBuilder.Entity<TransactionEntity>()
                .HasOne(transaction => transaction.OutgoingStatus)
                .WithMany(status => status.IncomingTransactions)
                .HasForeignKey(transaction => transaction.OutgoingStatusId);

            modelBuilder.Entity<TransactionEntity>()
                .HasOne(transaction => transaction.IncomingStatus)
                .WithMany(status => status.AvailableTransactions)
                .HasForeignKey(transaction => transaction.IncomingStatusId);

            // -- Status Model

            modelBuilder.Entity<StatusEntity>()
                .HasMany(status => status.AvailableTransactions)
                .WithOne(transaction => transaction.IncomingStatus)
                .HasForeignKey(transaction => transaction.IncomingStatusId)
                .IsRequired(false);

            modelBuilder.Entity<StatusEntity>()
                .HasMany(status => status.IncomingTransactions)
                .WithOne(transaction => transaction.OutgoingStatus)
                .HasForeignKey(transaction => transaction.OutgoingStatusId)
                .IsRequired();


            // -- Transaction Item Model
            modelBuilder.Entity<TransactionItem>()
                .HasKey(transactionItem => transactionItem.Id);

            modelBuilder.Entity<TransactionItem>()
                .HasOne(transactionItem => transactionItem.Application)
                .WithMany(application => application.TransactionItems)
                .HasForeignKey(transactionItem => transactionItem.ApplicationId)
                .IsRequired();

            modelBuilder.Entity<TransactionItem>()
                .HasOne(transactionItem => transactionItem.Transaction)
                .WithMany(transaction => transaction.TransactionItems)
                .HasForeignKey(transactionItem => transactionItem.TransactionId)
                .IsRequired();

            modelBuilder.Entity<TransactionItem>()
                .HasOne(transactionItem => transactionItem.User)
                .WithMany(user => user.TransactionItems)
                .HasForeignKey(transactionitems => transactionitems.UserId)
                .IsRequired();

            // -- User Model
            modelBuilder.Entity<UserEntity>()
                .HasMany(user => user.AssignedApplications)
                .WithOne(application => application.User)
                .HasForeignKey(application => application.AssignedUserId);


            // Seed Data
             DataSeeder.Seed(modelBuilder);
        }
    }
}
