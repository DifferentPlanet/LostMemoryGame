using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    internal class WhereToSpendDeneg
    {
        public string[] foodShop = new string[] { "Ham sandwich", "Fried chicken", "Hamburger", "Vegetarian pizza",
                                                  "Ham pizza", "Pork noodle", "Banana", "Sushi",
                                                  "Meat chips", "Fries" , "Caesar", "Salmon Caesar",
                                                  "Apple"};
        public string[] restaurant = new string[] { "McDonald's", "KFC", "Royal Restaurant", "Vegans' Place" };
        public string[] freeFood = new string[] { "Leftovers", "Pigeon", "Fried rat", "Berries" };

        public string[] estateArray = new string[] { "None", "Hostel", "Small flat", "Big flat", "Family house", "Luxury house" };
        public string[] vehicleArray = new string[] { "None", "Bicycle", "Daewoo Lanos", "Ford Focus", "Range Rover", "Lamborghini" };

        public string[] itemsToBuy = new string[] { "Shower gel", "PC", "Game", "Yoga mat", "TV",
                                                    "Chess", "Mirrow", "Running machine" };

        public bool CanAfford(int money, int cost)
        {
            if ((money < cost) || (money - cost < 0))
                return false;
            else
                return true;
        }
        public bool TimeAvalible(Character character, double value)
        {
            if (character.hoursAvalible - (value * ReturnVehicleHour(character.vehicle)) >= 0)
                return true;
            else return false;
        }
        public double HoursReduce(Character character, double value)
        {
            return Math.Round(character.hoursAvalible - (value * ReturnVehicleHour(character.vehicle)), 2);
        }
        public double HoursReduceWOVehicle(Character character, double value)
        {
            double result = Math.Round(character.hoursAvalible - (value ), 2);
            if (result < 0)
                result = 0;
            return result;
        }

        public int vehicleCost(int selectedIndex)
        {
            int[] cost = new int[] { 0, 150, 500, 750, 5000, 25000 };
            return cost[selectedIndex];
        }
        public double ReturnVehicleHour(string vehicle)
        {
            double[] vehicleHours = new double[] { 1, 0.95, 0.8, 0.75, 0.5, 0.2 };
            double result = 1;
            for (int i = 0; i < vehicleArray.Length; i++)
            {
                if (vehicle == vehicleArray[i])
                {
                    result = vehicleHours[i];
                    break;
                }
            }
            return result;
        }

        public int foodCostSupermarket(int index)
        {
            int[] cost = new int[] { 2, 3, 2, 4, 4, 5, 1, 5, 2, 3, 3, 4, 1 };
            return cost[index];
        }
        public int foodCostRestaurant(int index)
        {
            int[] cost = new int[] { 10, 12, 50, 30 };
            return cost[index];
        }
        public int foodHungerSupermarket(int index)
        {
            int[] cost = new int[] { 15, 20, 15, 15, 15, 30, 5, 15, 5, 10, 15, 20, 5 };
            return cost[index];
        }
        public int foodHungerRestaurant(int index)
        {
            int[] hunger = new int[] { 50, 50, 70, 70 };
            return hunger[index];
        }
        public int foodHungerFree(int index)
        {
            int[] hunger = new int[] { 10, 30, 15, 10 };
            return hunger[index];
        }
        public int foodHealthRestaurant(int index)
        {
            int[] health = new int[] { 10, 10, 20, 20 };
            return health[index];
        }
        public int foodHealthFree(int index)
        {
            int[] health = new int[] { 10, 10, 10, 5 };
            return health[index];
        }

        public string EstateList(int i)
        {
            return estateArray[i];
        }
        public int EstateListIndex(string s)
        {
            int index = 0;
            for (int i = 0; i < estateArray.Length; i++)
            {
                if (s == estateArray[i]) { index = i; break; }
            }
            return index;
        }

        public int EstateSleep(string s)
        {
            int[] sleep = new int[] { 75, 80, 85, 90, 95, 100 };
            for (int i = 0; i < estateArray.Length; i++)
            {
                if (s == estateArray[i])
                {
                    return sleep[i];
                }
            }
            return 75;
        }

        public int EstateCost(int index)
        {
            int[] cost = new int[] { 0, 0, 200, 1000, 10000, 25000 };
            return cost[index];
        }

        public int EstateCostPerDay(int index)
        {
            int[] cost = new int[] { 0, 10, 20, 75, 200, 1000 };
            return cost[index];
        }

        public int ItemsCost(int index)
        {
            int[] cost = new int[] { 15, 200, 35, 50, 100, 90, 150, 500 };
            return cost[index];
        }

    }
}
