using System;
namespace HWK4.Models
{
    /// <summary>
    /// Represents the calorie analysis data for a set of calories.
    /// </summary>
	public class CalorieAnalysis
	{
        /// <summary>
        /// Gets or sets the analysis string that summarizes the calorie data 
        /// </summary>
        public string Analysis { get; set; }

        /// <summary>
        /// gets or sets a dictionary that contains the calorie analysis data, with keys representing the different aspects of the analysis
        /// </summary>
        public Dictionary<string, int> AnalysisData { get; set; }
    }
}

