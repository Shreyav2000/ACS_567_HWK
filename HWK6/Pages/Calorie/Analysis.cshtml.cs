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
namespace CalorieWebApp.Pages.Calories;
using HWK4.Models;

/// <summary>
/// Represents a Razor Page for displaying calorie analysis.
/// </summary>
public class AnalysisModel : PageModel
{
    public CalorieAnalysis DataAnalysis = new();
    /// <summary>
    /// Performs a HTTP Get call
    /// </summary>
    public async void OnGet()
    {
        using (var client = new HttpClient())
        {
            client.BaseAddress = new Uri("https://localhost:7248");
            //HTTP GET
            var responseTask = client.GetAsync("Calorie/analysis");
            responseTask.Wait();

            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = await result.Content.ReadAsStringAsync();
                DataAnalysis = JsonConvert.DeserializeObject<CalorieAnalysis>(readTask);
            }
        }


    }
}


