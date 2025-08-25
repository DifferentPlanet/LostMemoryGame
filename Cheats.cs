using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    internal class Cheats
    {
        public string[] cheatsMoney = new string[] { "rosebud", "motherlode" };
        public string[] cheatsEstate = new string[] { "real_estate_lux" };

        public bool TryMoneyCheat(string input)
        {
            bool flag = false;
            foreach (var item in cheatsMoney)
            {
                if (input == item)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        public bool TryEstateCheat(string input)
        {
            bool flag = false;
            foreach (var item in cheatsEstate)
            {
                if (input == item)
                {
                    flag = true;
                    break;
                }
            }
            return flag;
        }
        public int MoneyCheat(string input)
        {
            int sum = 0;
            if (input == cheatsMoney[0])
                sum = 1000;
            else if (input == cheatsMoney[1])
                sum = 50000;
            return sum;
        }
        public string EstateCheat(string input)
        {
            string estate = "None";
            if (input == cheatsEstate[0])
                estate = "Luxury house";
            return estate;
        }

        public int DaysSpentCheat(Character character, string input)
        {
            int days = character.daysSpent;
            string check = input.Split('_')[0];
            if (check == "setDaysSpent")
            {
                if (int.TryParse(input.Split('_')[1], out int result))
                {
                    if (result > days && result <= 99)
                        days = result;
                }
            }
            return days;
        }
    }
}
