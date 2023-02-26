using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using CalorieRestAPI.Controllers;
using CalorieRestAPIMySQL.Interfaces;
using CalorieRestAPIMySQL.Models;
using CalorieRestAPIMySQL.Repositories;
using CalorieRestAPIMySQL.Data;
using CalorieRestAPI;


namespace CalorieWebApp.Pages.Calories
{
    public class DeleteModel : PageModel
    {
        public CalorieClass Calorie { get; private set; }

        public string ErrorMessage { get; private set; }

        public string SuccessMessage { get; private set; }

        public async void OnGet(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://localhost:7248");

                var response = await client.GetAsync("Calorie/" +id);
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    Calorie = Newtonsoft.Json.JsonConvert.DeserializeObject<CalorieClass>(content);
                }
                else
                {
                    ErrorMessage = "Error retrieving calorie";
                }
            }
        }

        public async void OnPost(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new System.Uri("https://localhost:7248");

                var response = await client.DeleteAsync("/Calorie/" + id);
                if (response.IsSuccessStatusCode)
                {
                    SuccessMessage = "Calorie successfully deleted";
                }
                else
                {
                    ErrorMessage = "Error deleting calorie";
                }
            }
        }
    }
}


