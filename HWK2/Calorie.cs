namespace CalorieRestAPI
{
    public class Calorie
    {
        // Private static field to keep track of the next available ID for a new Calorie object
        private static int nextId = 1;

        // Constructor that accepts parameters for all properties of the Calorie class
        public Calorie(int id, string name, int quantity, string unit, int value)
        {
            // Assign passed in values to properties

            Id = id;
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Value = value;
        }

        // Public properties for the ID, name, quantity, unit, and value of the Calorie object
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public string Unit { get; set; }

        public int Value { get; set; }
    }
}
