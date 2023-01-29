using Microsoft.AspNetCore.Mvc;

namespace CalorieRestAPI
{
    // Class that holds the total, average, min, and max calories
    public class CalorieAnalysis
    {
        public double totalCalories { get; set; }
        public double averageCalories { get; set; }
        public double minCalories { get; set; }
        public double maxCalories { get; set; }

        // Constructor for CalorieAnalysis class
        public CalorieAnalysis(double totalCalories, double averageCalories, double minCalories, double maxCalories)
        {
            this.totalCalories = totalCalories;
            this.averageCalories = averageCalories;
            this.minCalories = minCalories;
            this.maxCalories = maxCalories;
        }
    }
    // Repository class for managing Calorie objects
    public class CalorieRepository
    {
        // Singleton instance of the CalorieRepository class
        private static CalorieRepository instance;
        private List<Calorie> calories;

        // Private constructor for CalorieRepository class
        private CalorieRepository()
        {
            calories = new List<Calorie>();

            // Read the CSV file and add the data to the 'calories' list
            using (var reader = new StreamReader("calorie.csv"))
            {
                // Skip the header row
                reader.ReadLine();

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    int id = int.Parse(values[0]);
                    string name = values[1];
                    int quantity = int.Parse(values[2]); ;
                    string unit = values[3];
                   int value = int.Parse(values[4]);

                    calories.Add(new Calorie(id, name, quantity, unit, value));
                }
            }
        }

        // Method to return the singleton instance of the CalorieRepository class
        public static CalorieRepository getInstance()
        {
            if (instance == null)
            {
                instance = new CalorieRepository();
            }

            return instance;
        }

        // Method to return all Calorie objects in the repository
        public List<Calorie> getCalories()
        {
            return calories;
        }

        // Method to return a specific Calorie object based on its ID
        public Calorie GetCalorie(int id)
        {
            Calorie calorie = null;


            // Iterate through the list of Calorie objects and find the one with a matching ID
            foreach (Calorie c in calories)
            {
                if (id == c.Id)
                {
                    calorie = c;
                    break;
                }
            }

            return calorie;
        }

        // Method to add a new Calorie object to the repository
        public bool addCalorie(Calorie calorie)
        {
            bool isAdded = true;

            foreach (Calorie c in calories)
            {
                if (c.Id == calorie.Id)
                {
                    isAdded = false;
                    break;
                }
            }

            if (isAdded)
            {
                calories.Add(calorie);

            }

            return isAdded;
        }

        // This method updates a Calorie object in the calories list by its id
        public bool editCalorie(int id, Calorie updated)
        {
            bool isEdited = false;

            foreach (Calorie c in calories)
            {
                // Check if the id of the current Calorie object matches the id passed to the method
                if (c.Id == id)
                {
                    c.Name = updated.Name;
                    c.Quantity = updated.Quantity;
                    c.Unit = updated.Unit;
                    c.Value = updated.Value;
                    isEdited = true;
                    break;
                }
            }

            return isEdited;
        }

        // This method deletes a Calorie object from the calories list by its id
        public bool deleteCalorie(int id)
        {
            Calorie delete = null;
            foreach (Calorie c in calories)
            {
                if (id == c.Id)
                {
                    delete = c;
                    break;
                }
            }
            if (delete != null)
            {
                calories.Remove(delete);
                return true;
            }
            else
            {
                return false;
            }
        }



        // This method calculates the total value of all the calories in the list
        public double calculateTotalCalories()
        {
            double total = 0;
            foreach (Calorie c in calories)
            {
                total += c.Value;
            }
            return total;
        }

        // This method calculates and returns an analysis of the calories data
        public CalorieAnalysis AnalyzeData()
        {
            double total = calculateTotalCalories();
            double average = total / calories.Count;
            double minCalories = calories.Min(c => c.Value);
            double maxCalories = calories.Max(c => c.Value);
            return new CalorieAnalysis(total, average, minCalories, maxCalories);
        }
    }
}

