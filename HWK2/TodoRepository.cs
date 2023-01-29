using Microsoft.AspNetCore.Mvc;

namespace CalorieRestAPI
{
    public class CalorieAnalysis
    {
        public double totalCalories { get; set; }
        public double averageCalories { get; set; }
        public double minCalories { get; set; }
        public double maxCalories { get; set; }

        public CalorieAnalysis(double totalCalories, double averageCalories, double minCalories, double maxCalories)
        {
            this.totalCalories = totalCalories;
            this.averageCalories = averageCalories;
            this.minCalories = minCalories;
            this.maxCalories = maxCalories;
        }
    }
    public class CalorieRepository
    {
        private static CalorieRepository instance;
        private List<Calorie> calories;

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

        public static CalorieRepository getInstance()
        {
            if (instance == null)
            {
                instance = new CalorieRepository();
            }

            return instance;
        }

        public List<Calorie> getCalories()
        {
            return calories;
        }

        public Calorie GetCalorie(int id)
        {
            Calorie calorie = null;

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


        public bool editCalorie(int id, Calorie updated)
        {
            bool isEdited = false;

            foreach (Calorie c in calories)
            {
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




        public double calculateTotalCalories()
        {
            double total = 0;
            foreach (Calorie c in calories)
            {
                total += c.Value;
            }
            return total;
        }

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

