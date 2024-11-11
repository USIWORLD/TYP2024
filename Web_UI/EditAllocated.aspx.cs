using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace Farmacy_App
{
    public partial class EditAllocated : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;
        string animID = "";
        private List<int> selectAnimalId = new List<int>();
        protected async void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                string animalIdString = Request.QueryString["id"];
                animID = animalIdString;
                if (!string.IsNullOrEmpty(animalIdString) && int.TryParse(animalIdString, out int animalId))
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri("https://localhost:7042/api/Farmacy/");
                    RegisterAsyncTask(new PageAsyncTask(() => UpdateTasks(animalId)));
                }

                //LoadTasks();
                LoadWorkers();
                await PopulateMedAsync();
                first = false;

                date.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            else
            {
                selectAnimalId = GetAnimalIDs();

                await PopulateMedAsync();

            }
        }


        //public async void LoadTasks()
        //{
        //    var response = await client.GetAsync("https://localhost:7042/api/Farmacy/GetMedications");
        //    response.EnsureSuccessStatusCode();

        //    var responseBody = await response.Content.ReadAsStringAsync();
        //    var tasks = JsonConvert.DeserializeObject<List<Medication>>(responseBody);

        //    ddlId.DataSource = tasks;
        //    ddlId.DataTextField = "Name";
        //    ddlId.DataValueField = "Id";
        //    ddlId.DataBind();
        //}

        public async void LoadWorkers()
        {
            var response = await client.GetAsync("https://localhost:7042/api/Farmacy/GetWorkers");
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();
            var tasks = JsonConvert.DeserializeObject<List<Worker>>(responseBody);
            //var worker = JsonConvert.DeserializeObject<Worker>(responseBody);

            foreach (var worker in tasks)
            {
                if (worker.Name == "John")
                {
                    ddlname.Text = worker.Name;
                }
            }

        }

        private async Task PopulateMedAsync()
        {
            try
            {

                HttpResponseMessage response = await client.GetAsync("https://localhost:7042/api/Farmacy/GetMedications");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Medication>>(responseBody);

                foreach (var animal in animals)
                {
                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(animal.Id.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Name.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Quantity.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Cost.ToString()));



                    TableCell cell = new TableCell();
                    CheckBox list = new CheckBox
                    {
                        ID = "list_" + animal.Id,
                        Checked = selectAnimalId.Contains(animal.Id),
                    };




                    cell.Controls.Add(list);



                    newRow.Cells.Add(cell);


                    aTable.Rows.Add(newRow);
                }



            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                err.InnerText = $"Error : {ex.Message}";
            }
        }

        private async Task PopulateAnimalsAsync(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7042/api/Farmacy/GetAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

                animalTag.Text = id.ToString();


                //foreach (var animal in animals)
                //{
                //    if (animal.HealthStatus.Equals("HighRisk") || animal.HealthStatus.Equals("FMD Confirmed"))
                //    {

                //        TableRow newRow = new TableRow();
                //        newRow.Cells.Add(CreateCell(animal.Id.ToString()));
                //        newRow.Cells.Add(CreateCell(animal.HealthStatus.ToString()));


                //        TableCell cell = new TableCell();
                //        CheckBox list = new CheckBox
                //        {
                //            ID = "list_" + animal.Id,
                //            Checked = selectAnimalId.Contains(animal.Id),
                //        };



                //        cell.Controls.Add(list);
                //        newRow.Cells.Add(cell);
                //        ATable.Rows.Add(newRow);

                //    }
                //}
            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                err.InnerText = $"Error : {ex.Message}";
            }

        }

        private async Task UpdateTasks(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"GetOneAlloMed/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                var animal = JsonConvert.DeserializeObject<AllocateMed>(responseBody);

                if (animal != null)
                {
                    id = animal.Id;
                    animalTag.Text = animal.AnimalId;
                    allocatedMeds.Text = animal.Med_Id;
                    ddlname.Text = animal.User_Id;
                    date.Text = animal.Date.ToString("yyyy/MM/dd");
                    txtMedNotes.Value = animal.MedNotes;
                }


            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                err.InnerText = $"Error : {ex.Message}";
            }

        }

        private TableCell CreateCell(string text)
        {
            return new TableCell { Text = text };
        }
        protected async void AllocateTask_Click(object sender, EventArgs e)
        {
            DateTime time = DateTime.Now;
            var animTag = animalTag.Text;
            selectAnimalId = GetAnimalIDs();
            var selectedString = string.Join(" ", selectAnimalId);
            var allo = new
            {
                Med_Id = selectedString,
                User_Id = "John@gmail.com",
                Status = "Not Started",
                Date = time,
                AnimalId = animalTag.Text,
                MedNotes = txtMedNotes.Value,
            };

            var response = await AllocateAsync(allo);
            var responsebody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {

                err.InnerText = $"Medication: {selectedString} has been assigned for Animals Id's {animTag}";

            }
            else
            {
                err.InnerText = $"{responsebody}";
            }
        }


        private List<int> GetAnimalIDs()
        {
            List<int> list = new List<int>();


            foreach (TableRow row in aTable.Rows)
            {
                foreach (TableCell cell in row.Cells)
                {
                    foreach (Control control in cell.Controls)
                    {
                        if (control is CheckBox checkBox && checkBox.Checked)
                        {
                            //string checkBoxId = checkBox.ID; 
                            string animalIdString = checkBox.ID.Replace("list_", "");

                            if (int.TryParse(animalIdString, out int animalId))
                            {
                                list.Add(animalId);
                            }

                        }
                    }
                }
            }

            return list;
        }


        private async Task<HttpResponseMessage> AllocateAsync(object user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync("https://localhost:7042/api/Farmacy/AllocateAllMeds", content);
        }



        public class Worker
        {
            public string Id { get; set; }

            public string Name { get; set; }

            public string Surname { get; set; }

            public string Email { get; set; }

            public string Password { get; set; }
            public int UserType { get; set; }
        }

        public class Medication
        {
            public int Id { get; set; }


            public string Name { get; set; }


            public string Dosage { get; set; }



            public string Frequency { get; set; }


            public string Category { get; set; }

            public string Type { get; set; }

            public string Quantity { get; set; }

            public decimal Cost { get; set; }

        }

        public class Animal
        {
            public int Id { get; set; }


            public string Species { get; set; }


            public string Gender { get; set; }



            public string Breed { get; set; }


            public string Colour { get; set; }


            public decimal A_Weight { get; set; }


            public decimal Height { get; set; }


            public string HealthStatus { get; set; }


            public DateTime DateAcquired { get; set; }

            public int Age { get; set; }

            public int Kraal { get; set; }
        }

        public class AllocateMed
        {

            public int Id { get; set; }
            public string Med_Id { get; set; }
            public string User_Id { get; set; }
            public string Status { get; set; }
            public DateTime Date { get; set; }
            public string AnimalId { get; set; }
            public string MedNotes { get; set; }
        }



    }
}