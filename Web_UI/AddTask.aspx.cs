    
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
    public partial class AddTask : System.Web.UI.Page
    {
        private static readonly HttpClient client = new HttpClient();
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected async void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var task = new
                {
                    Task_Name = TaskName.Text,
                    Description = description.Text,
                    Category = ddlCategory.SelectedValue,
                };

                var response = await AddTaskAsync(task);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Response.Redirect("TaskManager.aspx");

                }

                else
                {

                    error.Text = $"Enter Valid Details {response}";
                }
            }
            catch (JsonSerializationException ex)
            {
                error.Text = $"Serialization error: {ex.Message}";
            }
            catch (Exception ex)
            {
                error.Text = $"An error occurred: {ex.Message}";
            }
        }

        private async Task<HttpResponseMessage> AddTaskAsync(object user)
        {
        
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync("http://localhost:7041/api/Farmacy/AddTask", content);
        }

    }
}