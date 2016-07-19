using System;

namespace BazaSamochod
{
    public enum Condition {Nowy,
                           Powypadkowy,
                           Bezwypadkowy};
    
    public enum Type { Samochód_Osobowy,
                       Motocykl,
                       Ciężarówka }
    public class Vechicle : IComparable<Vechicle>
    {
        public string Brand { get; set; }
        private string NumberID
        {
            get
            {
                return this.NumberID;
            }
            set
            {
                this.NumberID = value;
            }
        }
        public string Model { get; set; }
        public string Color { get; set; }
        public int YearOfProduction { get; set; }
        public Condition Condition { get; set; }
        public Type Type { get; set; }
        
        public Vechicle(string numberId, 
                        string brand, 
                        string model, 
                        int yearOfProduction, 
                        Condition condition, 
                        string color, 
                        Type type)
        {
            this.Type = type;
            this.NumberID = numberId;
            this.Brand = brand;
            this.Model = model;
            this.YearOfProduction = yearOfProduction;
            this.Condition = condition;
            this.Color = color;
        }
       
        public int CompareTo(Vechicle other)
        {
            if (this.NumberID == other.NumberID)
            {
               NumberID =NumberID+1;
            }
            return this.NumberID.CompareTo(other.NumberID);
        }
        public Vechicle()
        { }

    }
}