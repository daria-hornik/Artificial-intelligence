namespace Lab02.EinsteinRiddle
{
    class HauseAssignment
    {
        public HauseColor HauseColor { get; set; }
        public Pet Pet { get; set; }
        public Drink Drink { get; set; }
        public HauseNumber HauseNumber { get; set; }
        public HauseColor LeftHause { get; }
        public Cigarette Cigarette { get; }
        public Nationality Nationality { get; }

        public HauseAssignment(Nationality person, HauseColor hauseColor, Pet pet, Drink drink, Cigarette smoke, HauseNumber hauseNumber)
        {
            Nationality = person;
            HauseColor = hauseColor;
            Pet = pet;
            Drink = drink;
            Cigarette = smoke;
            HauseNumber = hauseNumber;
        }

        public HauseAssignment()
        {
        }

        public HauseAssignment(Nationality person, HauseColor hauseColor)
        {
            Nationality = person;
            HauseColor = hauseColor;
        }

        public HauseAssignment(Nationality person, Pet pet)
        {
            Nationality = person;
            Pet = pet;
        }

        public HauseAssignment(Nationality person, Drink drink)
        {
            Nationality = person;
            Drink = drink;
        }

        public HauseAssignment(HauseColor leftHause)
        {
            LeftHause = leftHause;
        }

        public HauseAssignment(HauseColor leftHause, Cigarette cigarette) : this(leftHause)
        {
            Cigarette = cigarette;
        }

        public HauseAssignment(Cigarette cigarette, Pet pet)
        {
            Cigarette = cigarette;
            Pet = pet;
        }

        public HauseAssignment(Cigarette cigarette, Drink drink)
        {
            Cigarette = cigarette;
            Drink = drink;
        }

        public HauseAssignment(HauseNumber hauseNumber, Drink drink)
        {
            HauseNumber = hauseNumber;
            Drink = drink;
        }

        public HauseAssignment(HauseNumber hauseNumber, Nationality nationality)
        {
            HauseNumber = hauseNumber;
            Nationality = nationality;
        }

        public HauseAssignment(HauseNumber hauseNumber, Cigarette cigarette, Pet pet)
        {
            HauseNumber = hauseNumber;
            Cigarette = cigarette;
            Pet = pet;
        }

        public HauseAssignment(Nationality nationality)
        {
            Nationality = nationality;
        }

        public HauseAssignment(HauseColor leftHause, Drink drink) : this(leftHause)
        {
        }

        public HauseAssignment(Pet pet)
        {
            Pet = pet;
        }

        public HauseAssignment(Cigarette cigarette)
        {
            Cigarette = cigarette;
        }

        public HauseAssignment(Nationality nationality, Cigarette cigarette) : this(nationality)
        {
        }

        public HauseAssignment(Drink drink)
        {
            Drink = drink;
        }

        public HauseAssignment(HauseNumber hauseNumber, HauseColor color)
        {
            HauseNumber = hauseNumber;
            LeftHause = color;
        }

        public HauseAssignment(HauseColor leftHause, Pet pet) : this(leftHause)
        {
        }
    }
}
