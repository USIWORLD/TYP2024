    using FarmacyAPI.Data;
using FarmacyAPI.Models;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.VisualBasic;
using System.Drawing;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FarmacyAPI
{
    public class FarmacyService
    {

        private readonly ApplicationDbContext _context;

        public FarmacyService()
        {

        }
        public FarmacyService(ApplicationDbContext context)
        {
            _context = context;
        }

       

        //Registering a user
        public bool RegisterUser(string id, string name, string surname, string email, string password, int userType)
        {
            var existingUser = _context.User.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                var newUser = new User
                {
                    Id = id,
                    Name = name,
                    Surname = surname,
                    Email = email,
                    Password = password,
                    UserType = userType,
                };


                _context.User.Add(newUser);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }

        }
        // User login method
        public int LoginUser(string id, string password)
        {

            var user = _context.User.FirstOrDefault(u => u. Id == id && u.Password == password);

            if (user != null)
            {

                return user.UserType;
            }
            else
            {
                Console.WriteLine($"User found: {user.Id}, UserType: {user.UserType}");
                return 0;
            }


        }

        //Adding animal
        public bool isAdded(string name,string species, string gender, string breed, string colour, decimal weight, decimal height, string Health_status, DateTime date, int age, int kraal, string appetiteStatus,decimal new_weight)
        {
            var animals = _context.Animal.FirstOrDefault(u =>
                    u.Name == name &&
                    u.Species == species &&
                    u.Gender == gender &&
                    u.Breed == breed &&
                    u.Colour == colour &&
                    u.A_Weight == weight &&
                    u.Height == height &&
                    u.HealthStatus == Health_status &&
                    u.DateAcquired == date &&
                    u.Age == age &&
                    u.Kraal == kraal &&
                    u.AppetiteStatus == appetiteStatus &&
                    u.New_Weight == new_weight

                );

            if (animals == null)
            {
                var Nanimal = new Animal
                {
                    Name = name,
                    Species = species,
                    Gender = gender,
                    Breed = breed,
                    Colour = colour,
                    A_Weight = weight,
                    Height = height,
                    HealthStatus = Health_status,
                    DateAcquired = date,
                    Age = age,
                    Kraal = kraal,
                    AppetiteStatus = appetiteStatus,
                    New_Weight = new_weight

                };

                _context.Animal.Add(Nanimal);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }

        }

        //Adding animal for app
        public bool Added(string species, string gender, string breed, string colour, decimal weight, decimal height, string Health_status, DateTime date, int age)
        {
            var animals = _context.Animals.FirstOrDefault(u =>
                     u.Species == species &&
                     u.Gender == gender &&
                     u.HealthStatus == Health_status &&
                     u.A_Weight == weight &&
                     u.Height == height &&
                     u.Age == age &&
                     u.DateAcquired == DateTime.Now

                 );

            var animal = _context.Animal.FirstOrDefault(u =>
                     u.Species == species &&
                     u.Gender == gender &&
                     u.HealthStatus == Health_status &&
                     u.Breed == breed &&
                     u.Colour == colour &&  
                     u.A_Weight == weight &&
                     u.Height == height &&
                     u.Age == age &&
                     u.DateAcquired == DateTime.Now

                 );

            if (animals == null && animal == null)
            {
                var newAnimals = new Animals
                {

                    Species = species,
                    Gender = gender,
                    HealthStatus = Health_status,
                    A_Weight = weight,
                    Height = height,
                    Age = age,
                    DateAcquired = DateTime.Now,
                };

                //for animal table
                var newAnimal = new Animal
                {

                    Species = species,
                    Gender = gender,
                    HealthStatus = Health_status,
                    Breed = breed,
                    Colour = colour,
                    A_Weight = weight,
                    Height = height,
                    Age = age,
                    DateAcquired = DateTime.Now,
                };

                _context.Animals.Add(newAnimals);
                _context.Animal.Add(newAnimal);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }

        //Adding a task
        public bool AddTask(string name, string description, string category)
        {
            var existingTask = _context.Task.FirstOrDefault(u =>
             u.Task_Name == name &&
             u.Description == description &&
             u.Category == category

            );


            if (existingTask == null)
            {
                var newTask = new Models.Task
                {
                    Task_Name = name,
                    Description = description,
                    Category = category
                    
                };


                _context.Task.Add(newTask);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }

        }


        //Adding a task
        public bool AddBudget(string category, decimal price, string month)
        {
            var existingTask = _context.Budget.FirstOrDefault(u =>
             u.Category == category &&
             u.Price == price &&
             u.Month == month

            );


            if (existingTask == null)
            {
                var newTask = new Models.Budget
                {
                    Category = category,
                    Price = price,
                    Month = month

                };


                _context.Budget.Add(newTask);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }

        }

        public bool AddKraal(string Name, int size, int sizesanitized,int sizeunsanitized,int sanitizer)
        {
            var existingTask = _context.Kraal.FirstOrDefault(u =>
             u.Name == Name &&
             u.Size == size &&
             u.SizeSanitized == sizesanitized &&
             u.SizeUnsanitized == sizeunsanitized &&
             u.Sanitizer == sanitizer

            );


            if (existingTask == null)
            {
                var newTask = new Models.Kraal
                {
                    Name = Name,
                    Size = size,
                    SizeSanitized = sizesanitized,
                    SizeUnsanitized = sizeunsanitized,
                    Sanitizer = sanitizer

                };


                _context.Kraal.Add(newTask);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }

        }

        public bool EditBudget(int id, string category, decimal price, string month)
        {
            var task = _context.Budget.FirstOrDefault(u => u.Id.Equals(id));


            if (task == null)
            {

                return false;

            }
            else
            {
                task.Category = category;
                task.Price = price;
                task.Month = month;

                _context.SaveChanges();

                return true;
            }

        }


        //Get Animals
        public List<Animal> GetAnimal()
        {
           return  _context.Animal.ToList();
        }

        public List<Kraal> GetKraals()
        {
            return _context.Kraal.ToList();
        }

        //GetHealthyAnimals
        public List<Animal> GetHealthyAnimals()
        {
            return _context.Animal.Where(u => u.HealthStatus == "Healthy").ToList();


        }

        public List<Budget> GetBudgets()
        {
            return _context.Budget.ToList();


        }

        //GetMaleAnimals
        public List<Animal> GetMaleAnimals()
        {

            return _context.Animal.Where(u => u.Gender == "Male").ToList();


        }

        public List<Animal> GetWeight100()
        {

            return _context.Animal.Where(u => u.A_Weight <= 100).ToList();


        }

        public List<Animal> GetWeight200()
        {

            return _context.Animal.Where(u => u.A_Weight > 100).ToList();


        }

        public List<Animal> GetAge10()
        {

            return _context.Animal.Where(u => u.Age <= 10).ToList();


        }

      

        public List<Animal> GetAge20()
        {

            return _context.Animal.Where(u => u.Age > 10 && u.Age <= 20).ToList();
        }

        public List<Animal> GetAge30()
        {

            return _context.Animal.Where(u => u.Age > 20 && u.Age <= 30).ToList();

        }

        //GetfemaleAnimals
        public List<Animal> GetFemaleAnimals()
        {

            return _context.Animal.Where(u => u.Gender == "Female").ToList();

        }

        //GetExposedAnimals
        public List<Animal> GetExposedAnimals()
        {

            return _context.Animal.Where(u => u.HealthStatus == "Exposed").ToList();

        }

        //GetHighRiskAnimals
        public List<Animal> GetHighRiskAnimals()
        {

            return _context.Animal.Where(u => u.HealthStatus == "HighRisk").ToList();

        }

        //GetReport
        public List<Report> GetReports()
        {
            return _context.Report.ToList();
        }

        //GetAnimals
        public string GetAnimals(int id)
        {
            var animal = _context.Animal.FirstOrDefault(u => u.Id == id);

            if (animal == null)
            {
                return "No animal found";
            }
            else
            {
                return $"{animal.Id} {animal.Species} {animal.Gender} {animal.Breed} {animal.Colour} {animal.A_Weight} {animal.Height} {animal.HealthStatus} {animal.DateAcquired} {animal.Age} {animal.Kraal} ";
            }

        }



        //Editing animal
        public bool EditAnimal(Models.Animal animal)
        {
            var existingAnimal = _context.Animal.Find(animal.Id);

            if (existingAnimal == null)
            {
                return false;
            }

            existingAnimal.Name = animal.Name;
            existingAnimal.Species = animal.Species;
            existingAnimal.Gender = animal.Gender;
            existingAnimal.Breed = animal.Breed;
            existingAnimal.Colour = animal.Colour;
            existingAnimal.A_Weight = animal.A_Weight;
            existingAnimal.Height = animal.Height;
            existingAnimal.HealthStatus = animal.HealthStatus;
            existingAnimal.DateAcquired = animal.DateAcquired;
            existingAnimal.Age = animal.Age;
            existingAnimal.Kraal = animal.Kraal;
            existingAnimal.AppetiteStatus = animal.AppetiteStatus;
            existingAnimal.New_Weight = animal.New_Weight;

            try
            {
                _context.SaveChanges(); // Save changes to the database
                return true;  // Success
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error updating animal: {ex.Message}");
                return false;
            }
        }


        //Get tasks
        public List<Models.Task> GetTasks() 
        { 
            return _context.Task.ToList();
        }

        //Get Task
        public string GetTask(int id)
        {
            var task = _context.Task.FirstOrDefault(u => u.Id == id);

            if (task == null)
            {
                return "No task found";
            }
            else
            {
                return $" {task.Task_Name} + {task.Description} + {task.Category}";
            }

        }


        //Editing task
        public bool EditTask(int id,string name, string description, string category)
        {
            var task = _context.Task.FirstOrDefault(u => u.Id.Equals(id));


            if (task == null)
            {

                return false;

            }
            else
            {
                task.Task_Name = name;
                task.Description = description;
                task.Category = category;

                _context.SaveChanges();

                return true;
            }

        }


        public bool EditKraal(int id,string name, int size, int sanitizer,int sanitized, int unsanitized)
        {
            var task = _context.Kraal.FirstOrDefault(u => u.Id.Equals(id));


            if (task == null)
            {

                return false;

            }
            else
            {
                task.Name = name;
                task.Size = size;
                task.Sanitizer = sanitizer;
                task.SizeSanitized = sanitized;
                task.SizeUnsanitized = unsanitized;

                _context.SaveChanges();

                return true;
            }

        }


        //for the animals table
        public Animal GetsAnimal(int id)
        {
            return _context.Animal.Where(u => u.Id == id).FirstOrDefault();

        }



        public bool AllocateTask(int task_Id, string user_Id, string status, DateTime duedate, string animalId, int feed)
        {
            var aTask = new TaskAllo
            {
                Task_Id = task_Id,
                User_Id = user_Id,
                Status = status,
                Duedate = duedate,
                AnimalId = animalId,
                Feed = feed
            };

            _context.TaskAllo.Add(aTask);
            _context.SaveChanges();
            return true;
        }



        public FarmacyAPI.Models.Task GetTaskAllocation(int id)
        {

            return _context.Task.Where(u => u.Id==id).FirstOrDefault();

        }

        //get all tasks assigned to a worker for that specific day

        //updating the status part of allocated tasks
        public async System.Threading.Tasks.Task UpdateTaskStatus(int id,string status)
        {
            var allocatedTask = _context.TaskAllo.FirstOrDefault(u => u.Id == id);
            if (allocatedTask != null)
            {
                allocatedTask.Status = status;
                allocatedTask.Duedate = allocatedTask.Duedate;
                await _context.SaveChangesAsync();
            }

        }
        public async System.Threading.Tasks.Task UpdateAnimalHealth(int id, string status)
        {
            //UpdateAnimalHealthy(id,status);
            var animal = _context.Animals.FirstOrDefault(u => u.Id == id);
            

            if (animal != null)
            {
                animal.Species = animal.Species;
                animal.Gender = animal.Gender;
                animal.HealthStatus = status;
                animal.A_Weight = animal.A_Weight;
                animal.Height = animal.Height;
                animal.Age = animal.Age;
                animal.DateAcquired = animal.DateAcquired;
                //for animal table
               
                await _context.SaveChangesAsync();
            }

        }

        public async System.Threading.Tasks.Task UpdateAnimalHealthy(int id, string status, int kraal)
        {
            var animals = _context.Animal.FirstOrDefault(u => u.Id == id);


            if (animals != null)
            {
                animals.Species = animals.Species;
                animals.Gender = animals.Gender;
                animals.HealthStatus = status;
                animals.Breed = animals.Breed;
                animals.Colour = animals.Colour;
                animals.A_Weight = animals.A_Weight;
                animals.Height = animals.Height;
                animals.Age = animals.Age;
                animals.DateAcquired = animals.DateAcquired;
                animals.Kraal = kraal;
                animals.Name = animals.Name;
                animals.New_Weight= animals.New_Weight;

                await _context.SaveChangesAsync();
            }

        }

        public List<Models.TaskAllo> getAllocated()
        {
            return _context.TaskAllo.ToList();
        }

        //add Visit
        public bool AddVisit(DateTime date, int animalID, string availability,decimal cost)
        {
            var existingVisit = _context.VetVisits.FirstOrDefault(u =>
             u.VisitDate == date &&
             u.AnimalID == animalID &&
             u.Availability == availability &&
             u.Cost == cost


            );


            if (existingVisit == null)
            {
                var newVisit = new Models.VetVisits
                {
                    VisitDate = date,
                    AnimalID = animalID,
                    Availability = availability,
                    Cost = cost

                };


                _context.VetVisits.Add(newVisit);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }

        }

        //getVisits
        public List<Models.VetVisits> getVisits()
        {
            return _context.VetVisits.ToList();
        }

        //get a specific visit
        public FarmacyAPI.Models.VetVisits GetAVisit(int id)
        {

            return _context.VetVisits.Where(u => u.VisitID == id).FirstOrDefault();

        }

        public List<User> GetWorkers()
        {
            return _context.User.Where(u => u.UserType == 2).ToList();

        }

        public string GetWorker(string id)
        {
            var worker = _context.User.FirstOrDefault(u => u.Id.Equals(id) && u.UserType == 2);

            if (worker == null)
            {
                return "No worker found";
            }
            else
            {
                return $"{worker.Name} {worker.Surname} {worker.Email}";
            }

        }

        //Editing task
        public bool EditWorker(string id, string name, string surname, string email)
        {
            var worker = _context.User.FirstOrDefault(u => u.Id.Equals(id));


            if (worker == null)
            {

                return false;

            }
            else
            {
                worker.Name = name;
                worker.Surname = surname;
                worker.Email = email;

                _context.SaveChanges();

                return true;
            }

        }

        //editing a visit
        public bool EditVisit(int id, DateTime visitDate, int AnimalId, string availability,decimal cost)
        {
            var visit = _context.VetVisits.FirstOrDefault(u => u.VisitID.Equals(id));


            if (visit == null)
            {

                return false;

            }
            else
            {
                visit.VisitDate = visitDate;
                visit.AnimalID = AnimalId;
                visit.Availability = availability;
                visit.Cost = cost;

                _context.SaveChanges();

                return true;
            }

        }


        //Adding a health report
        public bool QuestionaireFunction(DateTime dat, int q1, int q2, int q3, int q4, int q5, int q6, int q7, int q8, int q9, int q10, string uID, int cID)
        {
           
                var quest = new Questionaire
                {

                    DateAcquired = DateTime.Now,
                    Q_One = q1,
                    Q_Two = q2,
                    Q_Three = q3,
                    Q_Four = q4,
                    Q_Five = q5,
                    Q_Six = q6, 
                    Q_Seven = q7,
                    Q_Eight = q8,
                    Q_Nine = q9,
                    Q_Ten = q10,
                    U_ID = uID,
                    Cid = cID
                };


                _context.Questionaire.Add(quest);
                _context.SaveChanges();
                return true;

        }


        //Get Questionnaires
        public List<Models.Questionaire> GetQuestionnaires()
        {
            return _context.Questionaire.ToList();  
        }

        //for the ToDoList

        public List<Models.ToDoList> getToDoList()
        {
            return _context.ToDoList.ToList();
        }

        public async System.Threading.Tasks.Task addNote(string note, string title,DateOnly date)
        {
            var addNote = new ToDoList
            {
                Notes = note,
                Title = title,
                TaskDate = date
            };
            _context.ToDoList.Add(addNote);
            _context.SaveChanges(true);
        }

        public bool AllocateMed(int med_Id, string user_Id, string status, DateTime duedate, string animalId)
        {
            var aTask = new MedAllo
            {
                Med_Id = med_Id,
                User_Id = user_Id,
                Status = status,
                Date = duedate,
                AnimalId = animalId
            };

            _context.MedAllo.Add(aTask);
            _context.SaveChanges();
            return true;
        }

        public bool AllocateAllMeds(string med_Id, string user_Id, string status, DateTime duedate, string animalId, string medNotes)
        {
            // Split the med_Id string assuming it is space-separated
            var medIds = med_Id.Split(',');

            // Iterate over each med_Id
            foreach (var id in medIds)
            {
                // Create a new MedAllo object for each med_Id
                var aTask1 = new MedAllo
                {
                    Med_Id = Convert.ToInt32(id),  // Convert the split string to an integer
                    User_Id = user_Id,
                    Status = status,
                    Date = duedate,
                    AnimalId = animalId
                };

                // Add the MedAllo entry to the context
                _context.MedAllo.Add(aTask1);
            }

            // Now store the AllocateMed information (only once, not per med_Id)
            var aTask = new AllocateMed
            {
                Med_Id = med_Id,  // Keep this as the full string of IDs
                User_Id = user_Id,
                Status = status,
                Date = duedate,
                AnimalId = animalId,
                MedNotes = medNotes
            };

            _context.AllocateMed.Add(aTask);

            // Save changes to the context after all entries are added
            _context.SaveChanges();

            return true;
        }

        //public bool AllocateAllMeds(string med_Id, string user_Id, string status, DateTime duedate, string animalId, string medNotes)
        //{
        //    var aTask = new AllocateMed
        //    {
        //        Med_Id = med_Id,
        //        User_Id = user_Id,
        //        Status = status,
        //        Date = duedate,
        //        AnimalId = animalId,
        //        MedNotes = medNotes
        //    };

        //    var aTask1 = new MedAllo
        //    {
        //        Med_Id = Convert.ToInt32(med_Id),
        //        User_Id = user_Id,
        //        Status = status,
        //        Date = duedate,
        //        AnimalId = animalId
        //    };

        //    _context.MedAllo.Add(aTask1);
        //    _context.AllocateMed.Add(aTask);
        //    _context.SaveChanges();
        //    return true;
        //}

        public FarmacyAPI.Models.MedAllo GetMedAllocation(int id)
        {

            return _context.MedAllo.Where(u => u.Id == id).FirstOrDefault();

        }

        public async System.Threading.Tasks.Task UpdateMedStatus(int id, string status)
        {
            var allocatedTask = _context.MedAllo.FirstOrDefault(u => u.Id == id);

            if (allocatedTask != null)
            {
                allocatedTask.Status = status;
                allocatedTask.Date = allocatedTask.Date;
                await _context.SaveChangesAsync();
            }

        }

        public List<Models.MedAllo> getAllocatedMed()
        {
            return _context.MedAllo.ToList();
        }

        public bool AddMeds(string name, string dosage, string frequency, string category, string type, decimal cost,int quantity)
        {
            var Med = _context.Medication.FirstOrDefault(u =>

            u.Name == name &&
            u.Dosage == dosage &&
            u.Frequency == frequency &&
            u.Category == category &&
            u.Type == type &&
            u.Cost == cost &&
            u.Quantity == quantity
        
            );


            if (Med == null)
            {
                var med = new Medication
                {
                    Name = name,
                    Dosage = dosage,
                    Frequency = frequency,
                    Category = category,
                    Type = type,
                    Cost = cost,
                    Quantity = quantity,
                };


                _context.Medication.Add(med);
                _context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }

        }


        //Edit medication
        public bool EditMeds(int id, string name, string dosage, string frequency, string category, string type, decimal cost,int quantity)
        {
            var med = _context.Medication.FirstOrDefault(u => u.Id.Equals(id));

            if (med == null)
            {

                return false;

            }
            else
            {
                med.Name = name;
                med.Dosage = dosage;
                med.Frequency = frequency;
                med.Category = category;
                med.Type = type;
                med.Cost = cost;
                med.Quantity = quantity;

                _context.SaveChanges();

                return true;
            }

        }

        //Allocated Meds
        public List<Models.AllocateMed> GetAlloMeds()
        {
            return _context.AllocateMed.ToList();
        }

        //Get Specific Med Allocation
        public AllocateMed GetSingleAlloMed(int id)
        {
            return _context.AllocateMed.FirstOrDefault(u => u.Id == id);

        }

        //getMeds
        public List<Models.Medication> GetMeds()
        {
            return _context.Medication.ToList();
        }

        //Get Task
        public Medication GetMed(int id)
        {
            return _context.Medication.FirstOrDefault(u => u.Id == id);


        }


    }
}
