namespace BazaSamochod
{

    class Truck : Vechicle
    {
        public Truck(string numerId,
                     string brand, 
                     string model, 
                     int yearOfProduction, 
                     Condition condition, 
                     string color,
                     Type type)
        {
            this.Type = type;
            this.Brand = brand;
            this.Model = model;
            this.YearOfProduction = yearOfProduction;
            this.Condition = condition;
            this.Color = color;
        }

    }
}
