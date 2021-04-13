using System;
using System.Collections.Generic;

namespace Lab02.EinsteinRiddle
{
    class ER_Constraint : Constraint<HauseAssignment, ER_Domains>
    {
        public Nationality? Nationality { get; set; }
        public HauseColor? LeftHauseColor { get; set; }
        public HauseColor? RightHauseColor { get; set; }
        public int HauseDistance { get; set; }
        public Pet? Pet { get; set; }
        public Drink? Drink { get; set; }
        public Cigarette? Smoke { get; set; }
        public HauseNumber? HauseNumber { get; set; }
        public int Type { get; set; }

        public ER_Constraint(Nationality person, HauseColor hauseColor, Pet pet, Drink drink, Cigarette smoke, HauseNumber hauseNumber)
            : base(new List<HauseAssignment>() { new HauseAssignment(person, hauseColor, pet, drink, smoke, hauseNumber) })
        {
            Nationality = person;
            LeftHauseColor = hauseColor;
            Pet = pet;
            Drink = drink;
            Smoke = smoke;
            HauseNumber = hauseNumber;
        }

        public ER_Constraint() : base(new List<HauseAssignment>() { new HauseAssignment() })
        {
            Nationality = null;
            LeftHauseColor = null;
            Pet = null;
            Drink = null;
            Smoke = null;
            HauseNumber = null;
            Type = -1;
        }

        public ER_Constraint(Nationality person, HauseColor hauseColor)
            : base(new List<HauseAssignment>() { new HauseAssignment(person, hauseColor) })
        {
            Nationality = person;
            LeftHauseColor = hauseColor;
            Pet = null;
            Drink = null;
            Smoke = null;
            HauseNumber = null;
            Type = 0;
        }

        public ER_Constraint(Nationality person, Pet pet)
            : base(new List<HauseAssignment>() { new HauseAssignment(person, pet) })
        {
            Nationality = person;
            LeftHauseColor = null;
            Pet = pet;
            Drink = null;
            Smoke = null;
            HauseNumber = null;
            Type = 1;
        }

        public ER_Constraint(Nationality person, Drink drink)
         : base(new List<HauseAssignment>() { new HauseAssignment(person, drink) })
        {
            Nationality = person;
            LeftHauseColor = null;
            Pet = null;
            Drink = drink;
            Smoke = null;
            HauseNumber = null;
            Type = 2;
        }

        public ER_Constraint(HauseColor leftHause, HauseColor rightHause)
              : base(new List<HauseAssignment>() { new HauseAssignment(leftHause), new HauseAssignment(rightHause) })
        {
            Nationality = null;
            LeftHauseColor = leftHause;
            RightHauseColor = rightHause;
            Pet = null;
            Drink = null;
            Smoke = null;
            HauseNumber = null;
            HauseDistance = 1;
            Type = 3;
        }

        public ER_Constraint(HauseColor hauseColor, Drink drink)
          : base(new List<HauseAssignment>() { new HauseAssignment(hauseColor, drink) })
        {
            Nationality = null;
            LeftHauseColor = hauseColor;
            Pet = null;
            Drink = drink;
            Smoke = null;
            HauseNumber = null;
            Type = 4;
        }

        public ER_Constraint(Cigarette cigarette, Pet pet)
            : base(new List<HauseAssignment>() { new HauseAssignment(cigarette, pet) })
        {
            Nationality = null;
            LeftHauseColor = null;
            Pet = pet;
            Drink = null;
            Smoke = cigarette;
            HauseNumber = null;
            Type = 5;
        }

        public ER_Constraint(HauseColor color, Cigarette cigarette)
            : base(new List<HauseAssignment>() { new HauseAssignment(color, cigarette) })
        {
            Nationality = null;
            LeftHauseColor = color;
            Pet = null;
            Drink = null;
            Smoke = cigarette;
            HauseNumber = null;
            Type = 6;
        }

        public ER_Constraint(HauseNumber hauseNumber, Drink drink)
            : base(new List<HauseAssignment>() { new HauseAssignment(hauseNumber, drink) })
        {
            Nationality = null;
            LeftHauseColor = null;
            Pet = null;
            Drink = drink;
            Smoke = null;
            HauseNumber = hauseNumber;
            Type = 7;
        }

        public ER_Constraint(HauseNumber hauseNumber, Nationality nationality)
             : base(new List<HauseAssignment>() { new HauseAssignment(hauseNumber, nationality) })
        {
            Nationality = nationality;
            LeftHauseColor = null;
            Pet = null;
            Drink = null;
            Smoke = null;
            HauseNumber = hauseNumber;
            Type = 8;
        }

        public ER_Constraint(Cigarette cigarette, Drink drink)
         : base(new List<HauseAssignment>() { new HauseAssignment(cigarette, drink) })
        {
            Nationality = null;
            LeftHauseColor = null;
            Pet = null;
            Drink = drink;
            Smoke = cigarette;
            HauseNumber = null;
            Type = 10;

        }

        public ER_Constraint(Nationality nationality, Cigarette cigarette)
            : base(new List<HauseAssignment>() { new HauseAssignment(nationality, cigarette) })
        {
            Nationality = nationality;
            LeftHauseColor = null;
            Pet = null;
            Drink = null;
            Smoke = cigarette;
            HauseNumber = null;
            Type = 11;

        }

        public ER_Constraint(bool nextTo, Nationality nationality, HauseColor color)
                 : base(new List<HauseAssignment>() { new HauseAssignment(nationality), new HauseAssignment(color) })
        {
            HauseDistance = 1;
            Nationality = nationality;
            LeftHauseColor = color;
            HauseDistance = 1;
            Type = 12;
        }

        public ER_Constraint(bool nextTo, Cigarette cigarette, Drink drink)
          : base(new List<HauseAssignment>() { new HauseAssignment(cigarette), new HauseAssignment(drink) })
        {
            HauseDistance = 1;
            Nationality = null;
            LeftHauseColor = null;
            HauseDistance = 1;
            Type = 13;
        }

        public ER_Constraint(HauseNumber hauseNumber, HauseColor color)
            : base(new List<HauseAssignment>() { new HauseAssignment(hauseNumber, color) })
        {
            Nationality = null;
            LeftHauseColor = color;
            Pet = null;
            Drink = null;
            Smoke = null;
            HauseNumber = hauseNumber;
            Type = 14;
        }

        public ER_Constraint(bool nextTo, HauseColor hauseColor, Pet pet)
         : base(new List<HauseAssignment>() { new HauseAssignment(hauseColor, pet) })
        {
            Nationality = null;
            LeftHauseColor = hauseColor;
            Pet = pet;
            Drink = null;
            Smoke = null;
            HauseNumber = null;
            HauseDistance = 1;
            Type = 15;
        }

        public ER_Constraint(bool nextTo, HauseColor leftColor, HauseColor rightColor)
                : base(new List<HauseAssignment>() { new HauseAssignment(leftColor), new HauseAssignment(rightColor) })
        {
            LeftHauseColor = leftColor;
            RightHauseColor = rightColor;
            HauseDistance = 1;
            Type = 16;
        }

        private bool NextTo(HauseAssignment h1, HauseAssignment h2)
        {
            if (Math.Abs(h1.HauseNumber - h2.HauseNumber) == 1) return true;
            else return false;
        }

       /* public bool isSatisfied(HauseAssignment assign, Nationality nationality)
        {
            switch (Type)
            {
                case 0:
                    if (nationality.Equals(Nationality) == assign.HauseColor.Equals(LeftHauseColor)) return true;
                    break;
                case 1:
                    if (nationality.Equals(Nationality) == assign.Pet.Equals(Pet)) return true;
                    break;
                case 2:
                    if (assign.Nationality.Equals(Nationality) == assign.Drink.Equals(Drink)) return true;
                    break;
                case 8:
                    if (nationality.Equals(Nationality) == assign.HauseNumber.Equals(HauseNumber)) return true;
                    break;
                case 11:
                    if (nationality.Equals(Nationality) && assign.Cigarette.Equals(Smoke)) return true;
                    break;
                case 12:
                    for (int i = 0; i < EinsteinHelper.Counter - 1; ++i)
                    {
                        if ((EinsteinHelper.vars[i].nationality.equals(n) && EinsteinHelper.vars[i + 1].color.equals(c))
                                || (EinsteinBackTrack.vars[i].color.equals(c) && EinsteinBackTrack.vars[i + 1].nationality.equals(n))) return true;
                    }
                    break;
            }
            return false;
        }*/

        public override bool IsSatisfied(Dictionary<HauseAssignment, ER_Domains> set)
        {
            throw new NotImplementedException();
        }
    }
}