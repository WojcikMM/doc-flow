using Microsoft.EntityFrameworkCore;
using System;
using DocFlow.Core.Domain;

namespace DocFlow.Infrastructure.EntityFramework
{
    public class DataSeeder
    {
        public static readonly Guid TestUser1Id = new Guid("E4BF0C15-A506-44F8-AB1C-6CD76CE93D9A");

        private static readonly UserEntity User1 = new UserEntity(TestUser1Id.ToString(), "John Doe");

        private static readonly ApplicationEntity Application1 = new ApplicationEntity(new Guid("5660AF80-C763-4EFC-86FC-B3986FEA59D4"));

        private static readonly StatusEntity Status1 = new StatusEntity(new Guid("58297A8A-B5C3-4D1D-A587-07012190309C"), "Status 1");
        private static readonly StatusEntity Status2 = new StatusEntity(new Guid("EE080251-D307-4F28-B733-77B6D1572A56"), "Status 2");
        private static readonly StatusEntity Status3 = new StatusEntity(new Guid("EE080251-D307-4F28-B733-77B6D1572A51"), "Status 3");

        private static readonly TransactionEntity TransactionInitial1 = new TransactionEntity(new Guid("AD10BAE1-8C47-4610-8A64-2A61618E761E"), "Initial 1", "Initial Transaction 1", null, Status1.Id);
        private static readonly TransactionEntity TransactionInitial2 = new TransactionEntity(new Guid("AD10BAE1-8C47-4610-8A64-2A61618E761F"), "Initial 2", "Initial Transaction 2", null, Status2.Id);

        private static readonly TransactionEntity Transaction1 = new TransactionEntity(new Guid("AD10BAE1-8C47-4610-8A64-2A61618E761D"), "Transaction 1", "Description 1", Status1.Id, Status2.Id);
        private static readonly TransactionEntity Transaction2 = new TransactionEntity(new Guid("0DF10806-95B7-46C2-B777-01645AE94474"), "Transaction 2", "Description 2", Status1.Id, Status3.Id);


        public static void Seed(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StatusEntity>()
                .HasData(Status1, Status2, Status3);

            modelBuilder.Entity<TransactionEntity>()
                .HasData(Transaction1, Transaction2, TransactionInitial1, TransactionInitial2);


            modelBuilder.Entity<UserEntity>()
                .HasData(User1);

            modelBuilder.Entity<ApplicationEntity>()
                .HasData(Application1);

            modelBuilder.Entity<TransactionItem>()
                .HasData(TransactionItem.Create(TransactionInitial1, User1.Id, Application1.Id));

        }
    }
}
