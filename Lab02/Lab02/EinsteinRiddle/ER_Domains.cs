using System.Collections.Generic;

namespace Lab02.EinsteinRiddle
{
    enum Nationality : int
    {
        English = 1,
        Spaniard = 2,
        Ukrainian = 3,
        Norwegian = 4,
        Japanese = 5
    }

    enum HauseColor : int
    {
        Red = 1,
        Green = 2,
        Ivory = 3 ,
        Yellow = 4,
        Blue = 5
    }

    enum Pet : int
    {
        Dog = 1,
        Snails = 2, 
        Fox = 3,
        Horse = 4,
        Zebra = 5
    }

    enum Drink : int
    {
        Coffee = 1,
        Tea = 2,
        Milk = 3,
        Orange_Juice = 4,
        Water = 5
    }

    enum Cigarette: int
    {
        Old_Gold = 1,
        Kools = 2,
        Chesterfields = 3,
        Lucky_Strike = 4,
        Parliaments = 5
    }

    enum HauseNumber: int
    {
        Pierwszy = 1,
        Drugi = 2,
        Trzeci = 3,
        Czarty = 4,
        Piąty = 5
    }

    class ER_Domains
    {
        /*
        public List<HauseNumber> HouseNumbers { get; set; }
        public List<Person> People { get; set; }
        public List<HauseColor> HauseColors { get; set; }
        public List<Pet> Pets { get; set; }
        public List<Drink> Drinks { get; set; }
        public List<Smoke> Smokes { get; set; }*/

        public List<int> Domains { get; set; }
        public ER_Domains()
        {
            /* HouseNumbers = new List<HauseNumber>() { HauseNumber.Pierwszy, HauseNumber.Drugi, HauseNumber.Trzeci, HauseNumber.Czarty, HauseNumber.Piąty };
             People = new List<Person>() { Person.English, Person.Japanese, Person.Norwegian, Person.Spaniard, Person.Ukrainian };
             HauseColors = new List<HauseColor>() { HauseColor.Blue, HauseColor.Green, HauseColor.Ivory, HauseColor.Red, HauseColor.Yellow };
             Pets = new List<Pet>() { Pet.Dog, Pet.Fox, Pet.Horse, Pet.Snails, Pet.Zebra };
             Drinks = new List<Drink>() { Drink.Coffee, Drink.Milk, Drink.Orange_Juice, Drink.Tea, Drink.Water };
             Smokes = new List<Smoke>() { Smoke.Chesterfields, Smoke.Kools, Smoke.Lucky_Strike, Smoke.Old_Gold, Smoke.Parliaments };
         */
            Domains = new List<int>();
            }
    }
}
