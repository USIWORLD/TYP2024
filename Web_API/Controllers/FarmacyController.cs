using FarmacyAPI.Models;
using Microsoft.AspNetCore.Http;
        using Microsoft.AspNetCore.Mvc;
        using Microsoft.EntityFrameworkCore;

        namespace FarmacyAPI.Controllers
        {
    [Route("api/[controller]")]
    [ApiController]

    public class FarmacyController : ControllerBase
    {
        public readonly FarmacyService _service;

        public FarmacyController(FarmacyService service)
        {
            _service = service;
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] Models.User user)
        {
            bool IsRegistered = _service.RegisterUser(user.Id, user.Name, user.Surname, user.Email, user.Password, user.UserType);

            if (IsRegistered)
            {
                return Ok(new { Message = "User registered successfully." });
            }
            else
            {
                return BadRequest(new { Message = "User already exists." });
            }

        }

        [HttpPost("login")]
        public IActionResult LoginUser([FromBody] Models.LoginModel user)
        {
            int userType = _service.LoginUser(user.Id, user.Password);

            if (userType != 0)
            {
                return Ok(new { UserType = userType });

            }
            else
            {
                return Unauthorized(new { Message = "Invalid credentials." });
            }

        }

        [HttpPost("RegisterAnimal")]
        public IActionResult RegisterAnimal([FromBody] Models.Animal animal)
        {
            bool isAdded = _service.isAdded(animal.Name,animal.Species, animal.Gender, animal.Breed, animal.Colour, animal.A_Weight, animal.Height, animal.HealthStatus, animal.DateAcquired, animal.Age, animal.Kraal, animal.AppetiteStatus,animal.New_Weight);

            if (isAdded)
            {
                return Ok(new { Message = "Animal added successfully." });
            }
            else
            {
                return BadRequest(new { Message = "Animal already exists." });
            }
        }




        //for animals table
        [HttpPost("RegisterAnimals")]
        public IActionResult RegisterAnimals([FromBody] Models.Animal animal)
        {
            bool isAdded = _service.Added(animal.Species, animal.Gender, animal.Breed, animal.Colour, animal.A_Weight, animal.Height, animal.HealthStatus, animal.DateAcquired, animal.Age);

            if (isAdded)
            {
                return Ok(new { Message = "Animal added successfully." });
            }
            else
            {
                return BadRequest(new { Message = "Animal already exists." });
            }
        }

        [HttpPost("AddTask")]

        public IActionResult AddTask([FromBody] Models.Task task)
        {
            bool addTask = _service.AddTask(task.Task_Name, task.Description, task.Category);

            if (addTask)
            {
                return Ok(new { Message = " Task added successfully" });

            }
            else
            {
                return BadRequest(new { Message = "Task already exists" });
            }

        }

        [HttpPost("AddBudget")]

        public IActionResult AddBudget([FromBody] Models.Budget task)
        {
            bool addTask = _service.AddBudget(task.Category, task.Price, task.Month);

            if (addTask)
            {
                return Ok(new { Message = "Budget added successfully" });

            }
            else
            {
                return BadRequest(new { Message = "Budget already exists" });
            }

        }

        [HttpPost("AllocateTask")]
        public IActionResult AllocateTask([FromBody] Models.TaskAllo task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors if any
            }

            bool aTask = _service.AllocateTask(task.Task_Id, task.User_Id, task.Status, task.Duedate, task.AnimalId, task.Feed);

            if (aTask)
            {
                return Ok(new { Message = " Task allocated successfully" });

            }
            else
            {
                return BadRequest(new { Message = "error/s occured" });
            }

        }

        [HttpPost("AddMedication")]
        public IActionResult AddMed([FromBody] Models.Medication med)
        {
            bool addMed = _service.AddMeds(med.Name, med.Dosage, med.Frequency, med.Category, med.Type, med.Cost,med.Quantity);

            if (addMed)
            {
                return Ok(new { Message = " Medication added successfully" });

            }
            else
            {
                return BadRequest(new { Message = "Medication already exists" });
            }

        }

        [HttpPost("AddKraal")]
        public IActionResult AddKraal([FromBody] Models.Kraal med)
        {
            bool addMed = _service.AddKraal(med.Name, med.Size, med.SizeSanitized, med.SizeUnsanitized, med.Sanitizer);

            if (addMed)
            {
                return Ok(new { Message = " Kraal added successfully" });

            }
            else
            {
                return BadRequest(new { Message = "Kraal already exists" });
            }

        }


        [HttpPut("EditMedication/{id}")]

        public IActionResult EditMeds(int id, [FromBody] Models.Medication task)
        {
            bool edit = _service.EditMeds(task.Id, task.Name, task.Dosage, task.Frequency, task.Category, task.Type, task.Cost,task.Quantity);

            if (edit)
            {
                return Ok(new { Message = " Medication edited successfully" });

            }
            else
            {
                return BadRequest(new { Message = "Medication not found" });
            }

        }

        [HttpPut("EditKraal/{id}")]

        public IActionResult EditKraal(int id, [FromBody] Models.Kraal task)
        {
            bool edit = _service.EditKraal(task.Id, task.Name, task.Size, task.Sanitizer, task.SizeSanitized, task.SizeUnsanitized);

            if (edit)
            {
                return Ok(new { Message = " Kraal edited successfully" });

            }
            else
            {
                return BadRequest(new { Message = "Kraal not found" });
            }

        }

        //New Allocated Meds
        [HttpGet("GetAlloMeds")]
        public ActionResult<List<Models.AllocateMed>> GetAlloMeds()
        {
            var tasks = _service.GetAlloMeds();
            return Ok(tasks);
        }

        //GetSingleAllocation
        [HttpGet("GetOneAlloMed/{id}")]
        public ActionResult<string> GetSingleMed(int id)
        {
            var allocations = _service.GetSingleAlloMed(id);

            if (allocations == null)
            {
                return NotFound(allocations);
            }
            else
            {
                return Ok(allocations);
            }

        }


        [HttpGet("GetMed/{id}")]
        public ActionResult<string> GetMed(int id)
        {
            var med = _service.GetMed(id);

            if (med == null)
            {
                return NotFound(med);
            }
            else
            {
                return Ok(med);
            }

        }

        [HttpGet("GetMedications")]
        public ActionResult<List<Medication>> GetMedications()
        {
            var worker = _service.GetMeds();
            return Ok(worker);
        }

        [HttpGet("GetKraals")]
        public ActionResult<List<Kraal>> GetKraals()
        {
            var worker = _service.GetKraals();
            return Ok(worker);
        }

        //Allocated Med
        [HttpGet("GetAllocatedMed")]
        public ActionResult<List<Models.Medication>> GetMeds(int id)
        {
            var tasks = _service.GetMed(id);
            return Ok(tasks);
        }

        //allocated meds
        [HttpGet("GetAllocatedMeds")]
        public ActionResult<List<Models.MedAllo>> GetAllocatedMed()
        {
            var tasks = _service.getAllocatedMed();
            return Ok(tasks);
        }



        //Get animals
        [HttpGet("GetAnimals")]
        public ActionResult<List<Animal>> GetAnimals()
        {
            var animals = _service.GetAnimal();
            return Ok(animals);
        }

        //Get Healthy animals
        [HttpGet("GetHealthyAnimals")]
        public ActionResult<List<Animal>> GetHealthyAnimals()
        {
            var animals = _service.GetHealthyAnimals();
            return Ok(animals);
        }

        //Get Exposed animals
        [HttpGet("GetExposedAnimals")]
        public ActionResult<List<Animal>> GetExposedAnimals()
        {
            var animals = _service.GetExposedAnimals();
            return Ok(animals);
        }

        //Get HighRisk animals
        [HttpGet("GetHighRiskAnimals")]
        public ActionResult<List<Animal>> GetHighRiskAnimals()
        {
            var animals = _service.GetHighRiskAnimals();
            return Ok(animals);
        }

        //Get Male animals
        [HttpGet("GetMaleAnimals")]
        public ActionResult<List<Animal>> GetMaleAnimals()
        {
            var animals = _service.GetMaleAnimals();
            return Ok(animals);
        }

        //Get Female animals
        [HttpGet("GetFemaleAnimals")]
        public ActionResult<List<Animal>> GetFemaleAnimals()
        {   
            var animals = _service.GetFemaleAnimals();
            return Ok(animals);
        }

        [HttpGet("GetWeight100")]
        public ActionResult<List<Animal>> GetWeight100()
        {
            var animals = _service.GetWeight100();
            return Ok(animals);
        }

        [HttpGet("GetWeight200")]
        public ActionResult<List<Animal>> GetWeight200()
        {
            var animals = _service.GetWeight100();
            return Ok(animals);
        }

        [HttpGet("GetAge10")]
        public ActionResult<List<Animal>> GetAge10()
        {
            var animals = _service.GetAge10();
            return Ok(animals);
        }

        [HttpGet("GetAge20")]
        public ActionResult<List<Animal>> GetAge20()
        {
            var animals = _service.GetAge20();
            return Ok(animals);
        }

        [HttpGet("GetAge30")]
        public ActionResult<List<Animal>> GetAge30()
        {
            var animals = _service.GetAge20();
            return Ok(animals);
        }

        //Get Report
        [HttpGet("GetCounts")]
        public ActionResult<List<HealthCounts>> GetReports()
        {

            var animal = _service.GetAnimal();
            var counts = new HealthCounts
            {
                HealthyCount = animal.Count(a => a.HealthStatus == "Healthy"),
                ExposedCount = animal.Count(a => a.HealthStatus == "Exposed"),
                HighRiskCount = animal.Count(a => a.HealthStatus == "HighRisk"),
                fmd = animal.Count(a => a.HealthStatus == "FMD Confirmed")

            };

            return Ok(counts);
        }

        //GetAnimals
        [HttpGet("GetAnimal/{id}")]
        public ActionResult<string> GetAnimal(int id)
        {
            var animals = _service.GetAnimals(id);

            if (animals == null)
            {
                return NotFound(animals);
            }
            else
            {
                return Ok(animals);
            }

        }

        [HttpPost("AllocateMeds")]
        public IActionResult AllocateMeds([FromBody] Models.MedAllo task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors if any
            }

            bool aTask = _service.AllocateMed(task.Med_Id, task.User_Id, task.Status, task.Date, task.AnimalId);

            if (aTask)
            {
                return Ok(new { Message = " Medication allocated successfully" });

            }
            else
            {
                return BadRequest(new { Message = "error/s occured" });
            }

        }

        [HttpPost("AllocateAllMeds")]
        public IActionResult AllocateAllMeds([FromBody] Models.AllocateMed task)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Return validation errors if any
            }

            bool aTask = _service.AllocateAllMeds(task.Med_Id, task.User_Id, task.Status, task.Date, task.AnimalId, task.MedNotes);

            if (aTask)
            {
                return Ok(new { Message = " Medication allocated successfully" });

            }
            else
            {
                return BadRequest(new { Message = "error/s occured" });
            }

        }

        [HttpPut("EditAnimal/{id}")]
        public IActionResult EditAnimal(int id, [FromBody] Models.Animal animal)
        {
            bool isAdded = _service.EditAnimal(animal);

            if (isAdded)
            {
                return Ok(new { Message = "Animal edited successfully." });
            }
            else

            {
                return BadRequest(new { Message = "Animal not found." });
            }
        }

        [HttpGet("GetTasks")]
        public ActionResult<List<Models.Task>> GetTasks()
        {
            var tasks = _service.GetTasks();
            return Ok(tasks);
        }

        //GeTask
        [HttpGet("GetTask/{id}")]
        public ActionResult<string> GetTask(int id)
        {
            var task = _service.GetTask(id);

            if (task == null)
            {
                return NotFound(task);
            }
            else
            {
              
                return Ok(task);
            }

        }

        [HttpPut("EditTask/{id}")]

        public IActionResult EditTask(int id ,[FromBody] Models.Task task)
        {
            bool edit = _service.EditTask(task.Id,task.Task_Name, task.Description, task.Category);

            if (edit)
            {
                return Ok(new { Message = " Task edited successfully" });

            }
            else
            {
                return BadRequest(new { Message = "Task not found" });
            }

        }

        [HttpPut("EditBudget/{id}")]

        public IActionResult EditBudget(int id, [FromBody] Models.Budget task)
        {
            bool edit = _service.EditBudget(task.Id, task.Category, task.Price, task.Month);

            if (edit)
            {
                return Ok(new { Message = "Budget edited successfully" });

            }
            else
            {
                return BadRequest(new { Message = "Budget not found" });
            }

        }

        [HttpPost("AddVisit")]

        public IActionResult AddVisit([FromBody] Models.VetVisits visit)
        {
            bool addVisit = _service.AddVisit(visit.VisitDate, visit.AnimalID, visit.Availability,visit.Cost);

            if (addVisit)
            {
                return Ok(new { Message = " Vet Visit added successfully" });

            }
            else
            {
                return BadRequest(new { Message = "Vet Visit already exists" });
            }

        }

        [HttpGet("GetVisits")]
        public ActionResult<List<VetVisits>> GetVisits()
        {
            var Visits = _service.getVisits();
            return Ok(Visits);
        }

        [HttpPut("EditVisit/{id}")]

        public IActionResult EditVisit(int id, [FromBody] Models.VetVisits visits)
        {
            bool edit = _service.EditVisit(visits.VisitID, visits.VisitDate, visits.AnimalID, visits.Availability,visits.Cost);

            if (edit)
            {
                return Ok(new { Message = " Visit edited successfully" });

            }
            else
            {
                return BadRequest(new { Message = "Visit not found" });
            }

        }

        [HttpGet("GetAVisit")]
        public ActionResult<List<Models.VetVisits>> GetAVisit(int id)
        {
            var visit = _service.GetTaskAllocation(id);
            return Ok(visit);
        }

        [HttpGet("GetAllocatedTask")]
        public ActionResult<List<Models.Task>> GetTasks(int id)
        {
            var tasks = _service.GetTaskAllocation(id);
            return Ok(tasks);
        }

        //allocated tasks
        [HttpGet("GetAllocatedTasks")]
        public ActionResult<List<Models.TaskAllo>> GetAllocatedTask()
        {
            var tasks = _service.getAllocated();
            return Ok(tasks);
        }

        [HttpGet("GetWorkers")]
        public ActionResult<List<User>> GetWorkers()
        {
            var worker = _service.GetWorkers();
            return Ok(worker);
        }

        [HttpGet("GetBudgets")]
        public ActionResult<List<Budget>> GetBudgets()
        {
            var worker = _service.GetBudgets();
            return Ok(worker);
        }

        //GetWroker
        [HttpGet("GetWorker/{id}")]
        public ActionResult<string> GetWorker(string id)
        {
            var worker = _service.GetWorker(id);

            if (worker == null)
            {
                return NotFound(worker);
            }
            else
            {

                return Ok(worker);
            }

        }

        [HttpPut("EditWorker/{id}")]

        public IActionResult EditWorker(string id, [FromBody] Models.User worker)
        {
            bool edit = _service.EditWorker(worker.Id, worker.Name, worker.Surname, worker.Email);

            if (edit)
            {
                return Ok(new { Message = " Worker edited successfully" });

            }
            else
            {
                return BadRequest(new { Message = "Worker not found" });
            }

        }

        [HttpGet("GetsAnimal")]
        public ActionResult<string> GetsAnimal(int id)
        {
            var animal = _service.GetsAnimal(id);

            if (animal == null)
            {
                return NotFound(animal);
            }
            else
            {
                return Ok(animal);
            }

        }


        [HttpPost("questionaire")]
        public IActionResult QuestionaireFunction([FromBody] Models.Questionaire quest)
        {
            bool recorded = _service.QuestionaireFunction(quest.DateAcquired, quest.Q_One, quest.Q_Two, quest.Q_Three, quest.Q_Four, quest.Q_Five, quest.Q_Six, quest.Q_Seven, quest.Q_Eight, quest.Q_Nine, quest.Q_Ten, quest.U_ID, quest.Cid);

            if (recorded)
            {
                return Ok(new { Message = "Questionaire successfully logged." });
            }
            else
            {
                return BadRequest(new { Message = "Incorrect format for answers" });
            }

        }

        [HttpGet("GetQuestionnaires")]
        public ActionResult<List<Questionaire>> GetQuestionnaires()
        {
            var worker = _service.GetQuestionnaires();
            return Ok(worker);
        }

        [HttpPut("updateTaskStatus")]
        public async Task<IActionResult> UpdateTaskStatus([FromBody] Models.TaskAllo task)
        {
           await _service.UpdateTaskStatus(task.Id, task.Status);
            return Ok();
        }

        //updating the animal health
        [HttpPut("updateAnimalHealthStatus")]
        public async Task<IActionResult> updateAnimalHealthStatus([FromBody] Models.Animals animal)
        {
            await _service.UpdateAnimalHealth(animal.Id, animal.HealthStatus);
            return Ok();
        }

        [HttpPut("updateAnimalHealthyStatus")]
        public async Task<IActionResult> updateAnimalHealthyStatus([FromBody] Models.Animal animal)
        {
            await _service.UpdateAnimalHealthy(animal.Id, animal.HealthStatus, animal.Kraal);   
            return Ok();
        }

        // to-do list code

        [HttpGet("GetTodoList")]
        public ActionResult<string> GetTodoList()
        {
            var notes = _service.getToDoList();
            return Ok(notes);
        }

        [HttpPut("UpdateTodoList")]
        public async Task<IActionResult> UpdateTodoList([FromBody] Models.ToDoList note)
        {
            await _service.addNote(note.Notes, note.Title,note.TaskDate);
            return Ok();
        }

    }

    }

        
