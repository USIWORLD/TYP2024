using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;

namespace WebFarmApplication
{
    public partial class Login : System.Web.UI.Page
    {
        private static readonly HttpClient client = new HttpClient();

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async void LoginButton_Click(object sender, EventArgs e)
        {
            var pass = Secrecy.HashPassword(password.Text);
            var id = username.Text;

            var logindata = new
            {
                Id = id,
                Password = pass
            };

            try
            {
                HttpResponseMessage response = await LoginUserAsync(logindata);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    // Use the strongly-typed LoginResponse class for deserialization
                    var result = JsonConvert.DeserializeObject<LoginResponse>(responseBody);

                    if (result != null && result.userType != 0)  // Ensure userType is not 0 or null
                    {
                        int userType = result.userType;

                        switch (userType)
                        {
                            case 1:
                                // Admin user
                                Session["userID"] = username.Text;
                                Response.Redirect("Dashboard.aspx");
                                break;

                            case 2:
                                // Worker user
                                Session["userID"] = username.Text;
                                Response.Redirect("WorkerDashboard.aspx");
                                break;

                            case 3:
                                // Vet user
                                Session["userID"] = username.Text;
                                Response.Redirect("VetDashboard.aspx");
                                break;

                            default:
                                // Handle other user types if necessary
                                err.InnerText = "Unexpected user type.";
                                break;
                        }
                    }
                    else
                    {
                        // Handle the case where the result is null or userType is not present
                        err.InnerText = "Result is null or missing userType.";
                    }
                }
                else
                {
                    // Invalid credentials
                    err.InnerText = "Incorrect username or password.";
                }
            }
            catch (Exception ex)
            {
                // Display any exceptions encountered
                err.InnerText = $"Error: {ex.Message}";
            }
        }

        private async Task<HttpResponseMessage> LoginUserAsync(object user)
        {
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync("http://localhost:7041/api/Farmacy/login", content);
        }

        // Define the class that matches the structure of your JSON response
        public class LoginResponse
        {
            public int userType { get; set; }  // User type (e.g., Admin, Worker, Vet, etc.)
            public string message { get; set; }  // Optional message from API
        }
    }
}
