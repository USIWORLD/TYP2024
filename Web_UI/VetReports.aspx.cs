using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using static Farmacy_App.VetDashboard;
using System.Net;

namespace Farmacy_App
{
    public partial class VetReports : System.Web.UI.Page
    {
        HttpClient client = new HttpClient();
        bool first = true;

        protected async void Page_Load(object sender, EventArgs e)
        {
            if (first)
            {
                string VisitDate = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(VisitDate))
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                    RegisterAsyncTask(new PageAsyncTask(() => PopulateVisitAsync(VisitDate)));
                }

                string address = "http://localhost:7041/api/Farmacy/GetAnimals";
                //string VisitDate = "2024/08/29";
                //await PopulateVisitAsync(VisitDate);
                await PopulateMedAsync();
                await PopulateAnimalsAsync();
                await PopulateHealthyAnimalsAsync();
                await PopulateFMDAnimalsAsync();
                await PopulateAnimatotalAsync(address);
                first = false;
            }


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

        //protected async void ddlKraal_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string selectedValue = ddlKraal.SelectedValue;

        //    try
        //    {
        //        HttpResponseMessage response = await client.GetAsync("https://localhost:7042/api/Farmacy/GetAnimals");
        //        response.EnsureSuccessStatusCode();

        //        string responseBody = await response.Content.ReadAsStringAsync();
        //        var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);
        //        List<Animal> allAnim1 = new List<Animal>();
        //        List<Animal> allAnim2 = new List<Animal>();
        //        List<Animal> allAnim3 = new List<Animal>();
        //        List<Animal> allAnim4 = new List<Animal>();
        //        string animalIds;
        //        string healthStatuses;
        //        string animalNames;

        //        foreach (var animal in animals)
        //        {
        //            if (animal.Kraal.Equals(1))
        //            {
        //                allAnim1.Add(animal);
        //            }
        //            else if (animal.Kraal.Equals(2))
        //            {
        //                allAnim2.Add(animal);
        //            }
        //            else if (animal.Kraal.Equals(3))
        //            {
        //                allAnim3.Add(animal);
        //            }
        //            else if (animal.Kraal.Equals(4))
        //            {
        //                allAnim4.Add(animal);
        //            }
        //        }

        //        switch (selectedValue)
        //        {
        //            case "1":
        //                animalIds = string.Join(",", allAnim1.Select(a => a.Id.ToString()));
        //                healthStatuses = string.Join(",", allAnim1.Select(a => a.HealthStatus));
        //                animalNames = string.Join(",", allAnim1.Select(a => a.Name));

        //                AnimalIds.Value = animalIds;
        //                HealthStatus.Value = healthStatuses;
        //                AnimalNames.Value = animalNames;
        //                break;
        //            case "2":
        //                animalIds = string.Join(",", allAnim2.Select(a => a.Id.ToString()));
        //                healthStatuses = string.Join(",", allAnim2.Select(a => a.HealthStatus));
        //                animalNames = string.Join(",", allAnim2.Select(a => a.Name));

        //                AnimalIds.Value = animalIds;
        //                HealthStatus.Value = healthStatuses;
        //                AnimalNames.Value = animalNames;
        //                break;
        //            case "3":
        //                animalIds = string.Join(",", allAnim3.Select(a => a.Id.ToString()));
        //                healthStatuses = string.Join(",", allAnim3.Select(a => a.HealthStatus));
        //                animalNames = string.Join(",", allAnim3.Select(a => a.Name));

        //                AnimalIds.Value = animalIds;
        //                HealthStatus.Value = healthStatuses;
        //                AnimalNames.Value = animalNames;
        //                break;
        //            case "4":
        //                animalIds = string.Join(",", allAnim4.Select(a => a.Id.ToString()));
        //                healthStatuses = string.Join(",", allAnim4.Select(a => a.HealthStatus));
        //                animalNames = string.Join(",", allAnim4.Select(a => a.Name));

        //                AnimalIds.Value = animalIds;
        //                HealthStatus.Value = healthStatuses;
        //                AnimalNames.Value = animalNames;
        //                break;
        //        }
        //    }
        //    catch (HttpRequestException ex)
        //    {
        //        err.InnerText = $"Error : {ex.Message}";
        //    }
        //    catch (Exception ex)
        //    {
        //        err.InnerText = $"Error : {ex.Message}";
        //    }
        //}

        protected async void ddlWeight_SelectedIndexChanged(object sender, EventArgs e)
        {

            string selectedValue = ddlWeight.SelectedValue;
            string url = "";

            switch (selectedValue)
            {
                case "0-100":
                    url = "https://localhost:7042/api/Farmacy/GetWeight100";
                    break;
                case "100-200":
                    url = "https://localhost:7042/api/Farmacy/GetWeight200";
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
                        url = "https://localhost:7042/api/Farmacy/GetAge10";
                        break;
                    case "10-20":
                        url = "https://localhost:7042/api/Farmacy/GetAge20";
                        break;
                    case "20-30":
                        url = "https://localhost:7042/api/Farmacy/GetAge30";
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
                    url = "https://localhost:7042/api/Farmacy/GetMaleAnimals";
                    break;
                case "Female":
                    url = "https://localhost:7042/api/Farmacy/GetFemaleAnimals";
                    break;
            }

            await PopulateAnimatotalAsync(url);
        }

        protected async Task PopulateHealthyAnimalsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetHealthyAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

                lblRecovered.Text = animals.Count.ToString();
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

        private async Task PopulateMedAsync()
        {
            try
            {

                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);
                foreach (var allo in animals)
                {
                    if (allo.HealthStatus.Equals("FMD Confirmed"))
                    {
                        TableRow newRow = new TableRow();
                        newRow.Cells.Add(CreateCell(allo.Name.ToString()));

                        Button btnReview = new Button
                        {
                            Text = "Review",
                            CssClass = "btn btn-primary",
                            CommandArgument = allo.Id.ToString(),
                            Width = 100,
                        };

                        btnReview.Click += btnReview_Command_Click;

                        TableCell cell = new TableCell();


                        cell.Controls.Add(btnReview);


                        newRow.Cells.Add(cell);


                        medTable.Rows.Add(newRow);
                    }
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

        private async Task PopulateAnimalsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);
                List<Animal> list = new List<Animal>();
                foreach (var animal in animals)
                {
                    if (animal.HealthStatus.Equals("HighRisk"))
                    {
                        TableRow newRow = new TableRow();
                        newRow.Cells.Add(CreateCell(animal.Id.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Name));
                        newRow.Cells.Add(CreateCell(animal.Gender.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Breed.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Colour.ToString()));
                        newRow.Cells.Add(CreateCell(animal.A_Weight.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Height.ToString()));
                        newRow.Cells.Add(CreateCell(animal.HealthStatus.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Age.ToString()));
                        newRow.Cells.Add(CreateCell(animal.Kraal.ToString()));


                        Button btnEdit = new Button
                        {
                            Text = "Examine",
                            CssClass = "btn btn-primary",
                            CommandArgument = animal.Id.ToString(),
                            Width = 100,

                        };


                        btnEdit.Click += btnEdit_Command_Click;

                        TableCell cell = new TableCell();


                        cell.Controls.Add(btnEdit);


                        newRow.Cells.Add(cell);


                        aTable.Rows.Add(newRow);
                    }
                    if(animal.HealthStatus.Equals("FMD Confirmed"))
                    {
                        list.Add(animal);
                    }

                    lblFMD.Text = list.Count.ToString();
                }

                //Total Animals
                lblTotal.Text = animals.Count.ToString();

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

        private async Task PopulateFMDAnimalsAsync()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetFMDAnimals");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<List<Animal>>(responseBody);

                int count = animals.Count();
                lblFMD.Text = count.ToString();
            }
            catch (HttpRequestException ex)
            {
                error.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                error.InnerText = $"Error : {ex.Message}";
            }

        }

        private async Task PopulateVisitAsync(string visitDate)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("http://localhost:7041/api/Farmacy/GetVisits");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var visits = JsonConvert.DeserializeObject<List<VetVisits>>(responseBody);


                VisitContent.InnerHtml = "<h6 style='color: #0043a6;'><h6>";
                lblVisitDate.Text = visitDate;
                foreach (var visit in visits)
                {
                    if (visit.VisitDate.Date.ToString("yyyy/MM/dd") == visitDate)
                    {

                    }
                    else
                    {
                        VisitContent.InnerHtml += "<h6 style='color: #0043a6;'>" + visit.VisitDate.Date.ToString("yyyy/MM/dd") + "</h6>";
                        VisitContent.InnerHtml += "<br />";
                    }



                }

            }
            catch (HttpRequestException ex)
            {
                error.InnerText = $"Error : {ex.Message}";
            }
            catch (Exception ex)
            {
                error.InnerText = $"Error : {ex.Message}";
            }

        }

        protected void btnEdit_Command_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnEdit = (Button)sender;
                int id = Convert.ToInt32(btnEdit.CommandArgument);
                Response.Redirect($"ConductReport.aspx?id={id}", false);


            }
            catch (Exception ex)
            {

                err.InnerText = $"Error: {ex.Message}";

            }
        }

        protected void btnReview_Command_Click(object sender, EventArgs e)
        {
            try
            {
                Button btnReview = (Button)sender;
                int id = Convert.ToInt32(btnReview.CommandArgument);
                Response.Redirect($"AnimalProfile.aspx?id={id}", false);
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

        public class VetVisits
        {
            public int Visitkey { get; set; }

            public DateTime VisitDate { get; set; }

            public string AnimalID { get; set; }

            public string Availability { get; set; }

            public decimal Cost { get; set; }

        }

        public class Medication
        {
            public int Id { get; set; }

            public string Name { get; set; }

            public string Dosage { get; set; }

            public string Frequency { get; set; }

            public string Category { get; set; }

            public string Type { get; set; }

            public decimal Cost { get; set; }

            public int Inventory { get; set; }

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

        public class HealthCounts
        {

            public int key { get; set; }
            public int HealthyCount { get; set; }


            public int ExposedCount { get; set; }

            public int HighRiskCount { get; set; }

            public int fmd { get; set; }
        }
    }
}