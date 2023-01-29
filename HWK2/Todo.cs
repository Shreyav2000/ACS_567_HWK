namespace CalorieRestAPI
{
    public class Calorie
    {
        private static int nextId = 1;

        public Calorie(int id, string name, int quantity, string unit, int value)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Unit = unit;
            Value = value;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public string Unit { get; set; }

        public int Value { get; set; }
    }
}
