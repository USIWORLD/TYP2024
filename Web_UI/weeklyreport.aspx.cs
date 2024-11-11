using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Farmacy_App.MedicationCost;

namespace Farmacy_App
{
    public partial class weeklyreport : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;


        protected async void Page_Load(object sender, EventArgs e)
        {
            if (first)
            {
                string address = "http://localhost:7041/api/Farmacy/GetAnimals";

                await PopulateAnimalsAsync();
                await PopulateAnimatotalAsync(address);
                await PopulateAnimalsSantotalAsync();
                await PopulateAnimalsMEdtotalAsync();
                first = false;
            }

        }

        protected void btnQuestionnaires_Click(object sender, EventArgs e)
        {
            Response.Redirect("Questionnaire.aspx",false);
        }


        private async Task PopulateAnimalsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetCounts");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<HealthCounts>(responseBody);

                //lblH.Text = animals.HealthyCount.ToString();
                //lblE.Text = animals.ExposedCount.ToString();
                //lblHR.Text = animals.HighRiskCount.ToString();
                //lblFMD.Text = animals.fmd.ToString();

                //if (animals.HighRiskCount > animals.HealthyCount  || animals.HighRiskCount> animals.ExposedCount)
                //{
                //    decision.Text = "Call a vet and Quarantine Animals";
                //}else if(animals.ExposedCount > animals.HealthyCount)
                //{
                //    decision.Text = "Quarantine Animals";
                //}else
                //{
                //    decision.Text = "All conditions are good";
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
    
        public class HealthCounts
        {
           
            public int key { get; set; }
            public int HealthyCount { get; set; }

          
            public int ExposedCount { get; set; }
          
            public int HighRiskCount { get; set; }

            public int fmd { get; set; }
        }

        private async Task PopulateAnimatotalAsync(string url)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

                string animalIds = string.Join(",", animals.Select(a => a.Id.ToString()));
                string healthStatuses = string.Join(",", animals.Select(a => a.HealthStatus));
                string animalNames = string.Join(",", animals.Select(a => a.Name));

                AnimalIds.Value = animalIds;
                HealthStatus.Value = healthStatuses;
                AnimalNames.Value = animalNames;


                foreach (var animal in animals)
                {

                    if (animal.New_Weight == 0)
                    {
                        animal.New_Weight = animal.A_Weight;
                    }

                    animal.Actual_Cost = animal.New_Weight * 0.3M * 10.0M;

                    if (animal.Actual_Cost == 0)
                    {
                        animal.Actual_Cost = animal.Cost;

                    }
                    else
                    {
                        animal.Actual_Cost = animal.New_Weight * 0.3M * 10.0M;
                    }

                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(animal.Id.ToString()));
                    newRow.Cells.Add(CreateCell(animal.A_Weight.ToString()));
                    newRow.Cells.Add(CreateCell(animal.HayNeeded.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Cost.ToString("C")));
                    newRow.Cells.Add(CreateCell(animal.New_Weight.ToString("0")));
                    newRow.Cells.Add(CreateCell(animal.UpdateHayNeeded.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Actual_Cost.ToString("C")));
                    newRow.Cells.Add(CreateCell(animal.varience.ToString("C")));

                    TableCell varience = newRow.Cells[newRow.Cells.Count - 1];

                    if (animal.varience < 0)
                    {
                        varience.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        varience.ForeColor = System.Drawing.Color.Green;
                    }

                    newRow.Cells.Add(varience);

                    
                }
                decimal totalcost = animals.Sum(animal => animal.Actual_Cost);
                decimal allc = animals.Sum(animal => animal.Cost);
                decimal varrence = allc - totalcost;

                Cost.Text = totalcost.ToString("0.00");
                hvar.Text = varrence.ToString("0.00");
                costAllo.Text = allc.ToString("0.00");


                if (varrence < 0)
                {
                    hvar.ForeColor = System.Drawing.Color.Red;

                }
                else
                {
                    hvar.ForeColor = System.Drawing.Color.Green;
                }


            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                err.InnerText = $"Error: {ex.Message}";
            }
        }

        private TableCell CreateCell(string text)
        {
            return new TableCell { Text = text };
        }

        public class Animal
        {
            public int Id { get; set; }

            public string Name { get; set; }
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
            public string AppetiteStatus { get; set; }
            public decimal New_Weight { get; set; }
            public decimal HayNeeded => A_Weight * 0.3M;
            public decimal UpdateHayNeeded => New_Weight * 0.3M - HayNeeded;
            public decimal Cost => HayNeeded * 10.0M;

            public decimal Actual_Cost;
            public decimal varience => Cost - Actual_Cost;
        }



        protected async void ddlWeight_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlWeight.SelectedValue;
            string url = "";

            switch (selectedValue)
            {
                case "0-100":
                    url = "http://localhost:7041/api/Farmacy/GetWeight100";
                    break;
                case "100-200":
                    url = "http://localhost:7041/api/Farmacy/GetWeight200";
                    break;
            }

            await PopulateAnimatotalAsync(url);
        }

        protected async void ddlAge_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = ddlAge.SelectedValue;
            string url = "";

            switch (selectedValue)
            {
                case "0-10":
                    url = "http://localhost:7041/api/Farmacy/GetAge10";
                    break;
                case "10-20":
                    url = "http://localhost:7041/api/Farmacy/GetAge20";
                    break;
                case "20-30":
                    url = "http://localhost:7041/api/Farmacy/GetAge30";
                    break;
            }

            await PopulateAnimatotalAsync(url);
        }

        protected async void ddlGender_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedValue = Gender.SelectedValue;
            string url = "";

            switch (selectedValue)
            {
                case "Male":
                    url = "http://localhost:7041/api/Farmacy/GetMaleAnimals";
                    break;
                case "Female":
                    url = "http://localhost:7041/api/Farmacy/GetFemaleAnimals";
                    break;
            }

            await PopulateAnimatotalAsync(url);
        }

     
        private async Task PopulateAnimalsMEdtotalAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetMedications");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Medication>>(responseBody);
                HttpResponseMessage responseC = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAllocatedMeds");
                responseC.EnsureSuccessStatusCode();

                string responseBodyC = await responseC.Content.ReadAsStringAsync();
                var med = JsonConvert.DeserializeObject<List<MedAllo>>(responseBodyC);

                foreach (var item in animals)
                {
                    item.Allocation = med.Where(alloc => alloc.Med_Id == item.Id).ToList();
                }

                foreach (var animal in animals)
                {
                    var animalIds = (animal.Allocation != null && animal.Allocation.Any())
                             ? string.Join(" ", animal.Allocation.Select(a => a.AnimalId))
                             : "No Allocation";

                    var arrayAnimal = animalIds.Split(' ');


                    if (animal.Allocation == null || !animal.Allocation.Any())
                    {
                        animal.var = 0;
                        animal.EndCost = animal.Cost;
                        animal.var = 0;
                        animal.QuantityEdit = 0;


                        if (animal.Quantity == 0)
                        {

                            animal.EndCost = 0;
                            animal.var = animal.Cost;

                        }

                    }
                    else
                    {
                        if (arrayAnimal.Count() == 1)
                        {
                            animal.var = 0;
                            animal.QuantityEdit = animal.Quantity;
                            animal.EndCost = animal.Cost;

                        }
                        else if (arrayAnimal.Count() == 2)
                        {
                            animal.QuantityEdit = 2;
                            animal.EndCost = animal.Cost * animal.QuantityEdit;
                            animal.var = animal.Cost - animal.EndCost;


                        }
                        else
                        {
                            animal.var = 0;
                            animal.EndCost = animal.Cost;

                        }

                    }



                    //ASP Table
                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(animal.Name.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Dosage.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Cost.ToString()));
                    newRow.Cells.Add(CreateCell(animalIds.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Quantity.ToString()));

                    newRow.Cells.Add(CreateCell(animal.QuantityEdit.ToString()));
                    newRow.Cells.Add(CreateCell(animal.EndCost.ToString()));
                    newRow.Cells.Add(CreateCell(animal.var.ToString()));

                    TableCell varience = newRow.Cells[newRow.Cells.Count - 1];

                    if (animal.var == 0 || animal.EndCost == 0)
                    {
                        varience.ForeColor = System.Drawing.Color.Green;
                    }
                    else
                    {
                        varience.ForeColor = System.Drawing.Color.Red;
                    }

                }


                decimal totalcost = animals.Sum(animal => animal.EndCost);
                decimal varrence = 1200 - totalcost;

                medcost.Text = totalcost.ToString();
                medvar.Text = varrence.ToString();

                if (varrence < 0)
                {
                    medvar.ForeColor = System.Drawing.Color.Red;

                }
                else
                {
                    medvar.ForeColor = System.Drawing.Color.Green;
                }

            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                err.InnerText = $"Error: {ex.Message}";
            }

        }
              private async Task PopulateAnimalsSantotalAsync()
            {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animals>>(responseBody);
                HashSet<int> count = new HashSet<int>();



                foreach (var animal in animals)
                {
                    if (count.Contains(animal.Kraal))
                    {
                        continue;
                    }

                    count.Add(animal.Kraal);

                    if (animal.HealthStatus == "HighRisk")
                    {
                        animal.SanitizerUsed = animal.SanitizerAllocated;

                    }
                    else if (animal.HealthStatus == "Healthy")
                    {
                        animal.SanitizerUsed = animal.SanitizerAllocated;

                    }
                    else if (animal.HealthStatus == "FMD Confirmed")
                    {
                        animal.SanitizerUsed = animal.SanitizerAllocated;

                    }
                    else if (animal.HealthStatus == "Healthy")
                    {
                        animal.SanitizerUsed = animal.SanitizerAllocated;

                    }
                    else
                    {
                        animal.SanitizerUsed = animal.SanitizerAllocated;

                    }

                    if (animal.CostUsed > 45)
                    {
                        animal.frequency = "1 week";
                    }
                    else
                    {
                        animal.frequency = "2 weeks";
                    }

                    if (animal.Kraal == 4 || animal.Kraal == 5)
                    {
                        animal.KraalSize = 300;
                    }
                    else
                    {
                        animal.KraalSize = 150;

                    }

                    if (animal.KraalSize == 300)
                    {
                        animal.SanitizerAllocated = 300;
                        animal.SanitizerUsed = 300;

                    }
                    else if (animal.Kraal == 1)
                    {
                        animal.SanitizerAllocated = 150;
                        animal.SanitizerUsed = 100;
                    }
                    else
                    {
                        animal.SanitizerAllocated = 150;
                        animal.SanitizerUsed = 150;
                    }
                

                    decimal totalcost = animals.Sum(Animal => Animal.CostUsed);
                    decimal varrence = 225 - totalcost;

                  

                    TableRow newRow = new TableRow();
                    newRow.Cells.Add(CreateCell(animal.Kraal.ToString()));
                    newRow.Cells.Add(CreateCell(animal.SanitizerAllocated.ToString()));
                    newRow.Cells.Add(CreateCell(animal.Cost.ToString()));
                    newRow.Cells.Add(CreateCell(animal.SanitizerAllocated.ToString()));
                    newRow.Cells.Add(CreateCell(animal.CostUsed.ToString()));
                    newRow.Cells.Add(CreateCell(animal.frequency));
                    newRow.Cells.Add(CreateCell(animal.Varience.ToString()));


                    SanCost.Text = totalcost.ToString("0.00");
                    sanvar.Text = varrence.ToString("0.00");

                    if (varrence < 0)
                    {
                        sanvar.ForeColor = System.Drawing.Color.Red;

                    }
                    else
                    {
                        sanvar.ForeColor = System.Drawing.Color.Green;
                    }


                }

                
            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"Error: {ex.Message}";
            }
            catch (Exception ex)
            {
                err.InnerText = $"Error: {ex.Message}";
            }
        }


        public class Animals
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
            public string AppetiteStatus { get; set; }
            public int KraalSize { get; set; }
            public decimal Cost => KraalSize * 0.3M;
            public int SanitizerUsed { get; set; }
            public string frequency { get; set; }

            public decimal CostUsed => SanitizerUsed * 0.3M;
            public decimal Varience => Cost - CostUsed;

            public int SanitizerAllocated { get; set; }

        }

        public class MedAllo
        {
            public int Id { get; set; }

            public int Med_Id { get; set; }


            public string User_Id { get; set; }
            [Required]
            public string Status { get; set; }

            [Required]
            public DateTime Date { get; set; }


            public string AnimalId { get; set; }

        }

        public class Medication
        {

            public int Id { get; set; }


            public string Name { get; set; }

            public string Dosage { get; set; }



            public string Frequency { get; set; }


            public string Category { get; set; }

            public int Cost { get; set; }

            public int EndCost { get; set; }

            public int QuantityEdit { get; set; }

            public int Quantity { get; set; }


            public string Type { get; set; }

            public int var { get; set; }



            public string AnimalId { get; set; }

            public List<MedAllo> Allocation { get; set; }


        }


    }
}