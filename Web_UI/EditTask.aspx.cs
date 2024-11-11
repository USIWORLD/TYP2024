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
    public partial class EditTask : System.Web.UI.Page
    {
        HttpClient client;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string taskIdString = Request.QueryString["id"];

                if (!string.IsNullOrEmpty(taskIdString) && int.TryParse(taskIdString, out int taskId))
                {
                    client = new HttpClient();
                    client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                    RegisterAsyncTask(new PageAsyncTask(() => UpdateTasks(taskId)));
                }

            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string taskIdString = Request.QueryString["id"];

            if (!string.IsNullOrEmpty(taskIdString) && int.TryParse(taskIdString, out int taskId))
            {
                client = new HttpClient();
                client.BaseAddress = new Uri("http://localhost:7041/api/Farmacy/");
                RegisterAsyncTask(new PageAsyncTask(() => SaveData(taskId)));
            }
        }

        private async Task UpdateTasks(int id)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"GetTask/{id}");
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();

                string[] data = responseBody.Split(new[] { " + " }, StringSplitOptions.None);

                if (data.Length >= 3)
                {

                    string name = data[0].Trim();
                    string Description = data[1].Trim();
                    string category = data[2].Trim();

                    TaskName.Text = name;
                    description.Text = Description;
                    ddlCategory.SelectedValue = category;
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

        protected async Task SaveData(int id)
        {
            try
            {
                var animal = new
                {
                    Id = id,
                    Task_Name = TaskName.Text,
                    Description = description.Text,
                    Category = ddlCategory.SelectedValue,

                };



                var json = JsonConvert.SerializeObject(animal);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"EditTask/{id}", content);
                response.EnsureSuccessStatusCode();

                string responseBody = await response.Content.ReadAsStringAsync();
                var animals = JsonConvert.DeserializeObject<task>(responseBody);


            }
            catch (Exception ex)
            {
                error.InnerText = $"An error occurred: {ex.Message}";
            }

        }

        public class task
        {

            public int Id { get; set; }


            public string Task_Name { get; set; }


            public string Description { get; set; }


            public string Category { get; set; }

        }
    }
}