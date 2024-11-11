        using Microsoft.EntityFrameworkCore;
        using FarmacyAPI.Models;

        namespace FarmacyAPI.Data
        {
            public class ApplicationDbContext : DbContext
            {
                public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
                    : base(options) { }


                public DbSet<Animal> Animal { get; set; }
                public DbSet<Animals> Animals { get; set; }
                public DbSet<User> User { get; set; }
                public DbSet<Report> Report { get; set; }

                public DbSet<HealthCounts> HealthCounts { get; set; }
                public DbSet<Models.Task> Task { get; set; }

                public DbSet<Questionaire> Questionaire { get; set; }

                public DbSet<TaskAllocation> TaskAllocation { get; set; }
                public DbSet<TaskAllo> TaskAllo { get; set; }

                public DbSet<Kraal> Kraal { get; set; }


                public DbSet<ToDoList> ToDoList { get; set; }

                public DbSet<MedAllo> MedAllo { get; set; }

                public DbSet<Budget> Budget { get; set; }
                public DbSet<Medication> Medication { get; set; }

                public DbSet<VetVisits> VetVisits { get; set; }

                public DbSet<AllocateMed> AllocateMed { get; set; }





        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Call the base method first
            base.OnModelCreating(modelBuilder);

            // Configure Animal entity
            modelBuilder.Entity<Animal>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Species).IsRequired();
                entity.Property(e => e.Gender).IsRequired();
                entity.Property(e => e.Breed).IsRequired();
                entity.Property(e => e.Colour).IsRequired();
                entity.Property(e => e.A_Weight).IsRequired();
                entity.Property(e => e.Height).IsRequired();
                entity.Property(e => e.HealthStatus).IsRequired();
                entity.Property(e => e.DateAcquired).IsRequired();
                entity.Property(e => e.Age).IsRequired();
                entity.Property(e => e.Kraal).IsRequired();
                entity.Property(e => e.AppetiteStatus).IsRequired();
                entity.Property(e => e.New_Weight).IsRequired();
                entity.Property(e => e.Name).IsRequired();
            });

            // Configure Animal entity
            modelBuilder.Entity<Animals>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Species).IsRequired();
                entity.Property(e => e.Gender).IsRequired();
                entity.Property(e => e.HealthStatus).IsRequired();
                entity.Property(e => e.A_Weight).IsRequired();
                entity.Property(e => e.Height).IsRequired();
                entity.Property(e => e.Age).IsRequired();
                entity.Property(e => e.DateAcquired).IsRequired();
               
            });

            // Configure Animal entity
            modelBuilder.Entity<Report>(entity =>
            {
                entity.HasKey(e => e.ReportId);
                entity.Property(e => e.DateGenerated).IsRequired();
                entity.Property(e => e.HealthyCount).IsRequired();
                entity.Property(e => e.ExposedCount).IsRequired();
                entity.Property(e => e.HighRiskCount).IsRequired();
                entity.Property(e => e.Decision).IsRequired();
                entity.Property(e => e.AnimalId).IsRequired();

                entity.HasOne<Animal>().WithMany().HasForeignKey(e => e.AnimalId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            }); 
            //configure questionaire
            modelBuilder.Entity<Questionaire>(entity =>
                {
                    entity.HasKey(e => e.Id);
                    entity.Property(e => e.DateAcquired).IsRequired();
                    entity.Property(e => e.Q_One).IsRequired();
                    entity.Property(e => e.Q_Two).IsRequired();
                    entity.Property(e => e.Q_Three).IsRequired();
                    entity.Property(e => e.Q_Four).IsRequired();
                    entity.Property(e => e.Q_Five).IsRequired();
                    entity.Property(e => e.Q_Six).IsRequired();
                    entity.Property(e => e.Q_Seven).IsRequired();
                    entity.Property(e => e.Q_Eight).IsRequired();
                    entity.Property(e => e.Q_Nine).IsRequired();
                    entity.Property(e => e.Q_Ten).IsRequired();
                    entity.Property(e => e.U_ID).IsRequired();
                    entity.Property(e => e.Cid).IsRequired();

                    entity.HasOne<Animal>().WithOne().HasForeignKey<Questionaire>(e => e.Cid).IsRequired().OnDelete(DeleteBehavior.Cascade); 
                });

            // Configure Task entity
            modelBuilder.Entity<Models.Task>(entity =>
                    {
                        entity.HasKey(e => e.Id);
                        entity.Property(e => e.Task_Name).IsRequired();
                        entity.Property(e => e.Description).IsRequired();
                        entity.Property(e => e.Category).IsRequired();
                    });

            modelBuilder.Entity<Models.HealthCounts>(entity =>
            {
                entity.HasKey(e => e.key);
                entity.Property(e => e.HealthyCount).IsRequired();
                entity.Property(e => e.ExposedCount).IsRequired();
                entity.Property(e => e.HighRiskCount).IsRequired();
                entity.Property(e => e.fmd).IsRequired();

            });

            // Configure User entity
            modelBuilder.Entity<User>(entity =>
                    {
                        entity.HasKey(e => e.Id);
                        entity.Property(e => e.Name).IsRequired();
                        entity.Property(e => e.Surname).IsRequired();
                        entity.Property(e => e.Email).IsRequired();
                        entity.Property(e => e.Password).IsRequired();
                        entity.Property(e => e.UserType).IsRequired();
                        // Additional configurations as needed
                    });

                // Configure User entity
                    modelBuilder.Entity<LoginModel>(entity =>
                    {
                        entity.HasKey(e => e.Id);
                        entity.Property(e => e.Password).IsRequired();
                    });

                    // Configure TaskAllocation entity
                    modelBuilder.Entity<TaskAllocation>(entity =>
                    {
                        // Define composite primary key
                        entity.HasKey(e => new { e.Task_Id, e.User_Id }); 

                        // Define foreign key relationships
                        entity.HasOne(e => e.Task)
                            .WithMany() // Assuming one-to-many relationship
                            .HasForeignKey(e => e.Task_Id)
                            .OnDelete(DeleteBehavior.Restrict); // Specify delete behavior as needed

                        entity.HasOne(e => e.User)
                            .WithMany() // Assuming one-to-many relationship
                            .HasForeignKey(e => e.User_Id)
                            .OnDelete(DeleteBehavior.Restrict); // Specify delete behavior as needed

                        entity.Property(e => e.Status).IsRequired();
                        entity.Property(e => e.Duedate).IsRequired();
                    });

            // Configure TaskAllocation entity
            modelBuilder.Entity<TaskAllo>(entity =>
            {
                // Define composite primary key
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Duedate).IsRequired();
                entity.Property(e => e.AnimalId).IsRequired();
                entity.Property(e => e.Feed).IsRequired();

            });

            modelBuilder.Entity<Models.ToDoList>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Notes);
                entity.Property(e => e.Title);
                entity.Property(e => e.TaskDate);


            });

            modelBuilder.Entity<Models.Kraal>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.Sanitizer);
                entity.Property(e => e.Size);
                entity.Property(e => e.SizeSanitized);
                entity.Property(e => e.SizeUnsanitized);


            });

            modelBuilder.Entity<Models.Budget>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Category);
                entity.Property(e => e.Price);
                entity.Property(e => e.Month);


            });

            modelBuilder.Entity<MedAllo>(entity =>
            {
                // Define composite primary key
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.AnimalId).IsRequired();
                entity.Property(e => e.Feed).IsRequired();

            });

            modelBuilder.Entity<Models.Medication>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name);
                entity.Property(e => e.Dosage);
                entity.Property(e => e.Frequency);
                entity.Property(e => e.Category);
                entity.Property(e => e.Type);
                entity.Property(e => e.Cost);
                entity.Property(e => e.Quantity);

            });

            modelBuilder.Entity<Models.VetVisits>(entity =>
            {
                entity.HasKey(e => e.VisitID);
                entity.Property(e => e.VisitDate);
                entity.Property(e => e.AnimalID);
                entity.Property(e => e.Availability);
                entity.Property(e => e.Cost);

            });

            modelBuilder.Entity<AllocateMed>(entity =>
            {
                // Define composite primary key
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Status).IsRequired();
                entity.Property(e => e.Date).IsRequired();
                entity.Property(e => e.AnimalId).IsRequired();
                entity.Property(entity => entity.Med_Id).IsRequired();
                entity.Property(entity => entity.MedNotes);

            });
        }



            }
        }
