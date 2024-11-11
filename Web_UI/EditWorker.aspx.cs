using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace Farmacy_App
{
    public partial class EditWorker : System.Web.UI.Page
    {
        HttpClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string workerIdString = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(workerIdString))
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                    RegisterAsyncTask(new PageAsyncTask(() => UpdateWorker(workerIdString)));
                }

            }
        }

        protected void AddWorker_Click(object sender, EventArgs e)
        {
            string workerIdString = Request.QueryString["id"];

            if (!string.IsNullOrEmpty(workerIdString))
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                RegisterAsyncTask(new PageAsyncTask(() => SaveWorker(workerIdString)));
            }
        }


        protected async Task UpdateWorker(string workerId)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"GetWorker/{workerId}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                string[] data = responseBody.Split(' ');

                if (data.Length >= 3)
                {
                    string name = data[0];
                    string surname = data[1];
                    string email = data[2];

                    Names.Text = name;
                    Surnames.Text = surname;
                    Emails.Text = email;
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

        protected async Task SaveWorker(string id)
        {
            try
            {
                var worker = new
                {
                   Id = id,
                   Name = Names.Text,
                   Surname = Surnames.Text,  
                   Email = Emails.Text,
                   Password = "6216f8a75fd5bb3d5f22b6f9958cdede3fc086",
                   UserType = 2,

                };


                var json = JsonConvert.SerializeObject(worker);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                

                HttpResponseMessage response = await client.PutAsync($"EditWorker/{id}", content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();   
                var animals = JsonConvert.DeserializeObject<Worker>(responseBody);


            }
            catch (HttpRequestException ex)
            {
                err.InnerText = $"An error occurred: {ex.Message}";
            }
            catch (Exception ex)
            {
                err.InnerText = $"An error occurred: {ex.Message}";
            }

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


    
    }   
}