namespace BazaSamochod
{
    class Motorcycle : Vechicle
    {
        public Motorcycle(string numberId, 
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
