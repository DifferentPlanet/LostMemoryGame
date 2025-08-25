using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    internal class Work
    {
        public string currentJob;
        public List<string> avalibleJobs;
        public int absenseCounter, daysReqPromo;

        private string[] Positions = new string[] { "Loader", "Fruit Seller", "Trade Manager", "Brocker", "CEO",            //0 1 2 3 4
                                                    "Agitator", "Party Activist", "Major", "Prime-minister", "President",   //5 6 7 8 9
                                                    "Cleaner", "Assistant", "Researcher", "Professor", "Scientist"};        //10 11 12 13 14
        private string[] Spheres = new string[] { "Business", "Politics", "Science" };

        public List<string> ReturnAvalibleJobs(string currentJob)
        {
            List<string> jobsList = new List<string>();

            for (int i = 0; i < Spheres.Length; i++)
            {
                jobsList.Add(Positions[i * 5] + " (" + ReturnSphere(Positions[i * 5]) + ")");
            }

            if (currentJob != null)
            {
                string sphere = ReturnSphere(currentJob.Split(" (")[0]);
                for (int i = 0; i < jobsList.Count; i++)
                {
                    if (jobsList[i].Contains(sphere))
                        jobsList.Remove(jobsList[i]);
                }
                for (int i = 0; i < Positions.Length - 1; i++)
                {
                    if (i != 4 && i != 9)
                    {
                        if (ReturnPosition(currentJob) == Positions[i])
                        {
                            jobsList.Add(Positions[i + 1] + " (" + ReturnSphere(Positions[i]) + ")");
                            break;
                        }
                    }
                }
                jobsList.Reverse();
            }
            return jobsList;
        }

        public string ReturnRequirement (string selectedValue)
        {
            string result = "Requirement: ";
            int[] requirementsPoint = new int[] { 75, 50, 60, 70, 95,
                                                  50, 70, 5000, 7500, 10000,
                                                  40, 70, 80, 90, 95};
            string[] requirementsName = new string[] { "power", "charisma", "charisma", "charisma", "charisma",
                                                       "power", "power", "money", "money", "money",
                                                       "power", "logic", "logic", "logic", "logic"};
            for (int i = 0; i < Positions.Length; i++)
            {
                if (ReturnPosition(selectedValue) == Positions[i])
                {
                    result += requirementsName[i] + " " + requirementsPoint[i].ToString();
                }
            }
            return result;
        }

        public string ReturnRequirementPromo(string currentJob)
        {
            string result = "-";
            int[] requirementsPoint = new int[] { 75, 50, 60, 70, 95,
                                                  50, 70, 5000, 7500, 10000,
                                                  40, 70, 80, 90, 95};
            string[] requirementsName = new string[] { "power", "charisma", "charisma", "charisma", "charisma",
                                                       "power", "power", "money", "money", "money",
                                                       "power", "logic", "logic", "logic", "logic"};
            for (int i = 0; i < Positions.Length - 1; i++)
            {
                if (ReturnPosition(currentJob) == Positions[i] && (i != 4 && i != 9 && i != 14))
                {
                    result += requirementsName[i + 1] + " " + requirementsPoint[i + 1].ToString();
                }
            }
            return result;
        }

        public int ReturnDaysReqPromo(string selectedValue)
        {
            int result = 0;
            int[] requiredDays = new int[] { 0, 7, 7, 14, 21,
                                            0, 15, 5, 5, 7,
                                            0, 12, 7, 21, 30};
            for (int i = 0; i < Positions.Length - 1; i++)
            {
                if (ReturnPosition(selectedValue) == Positions[i] && (i != 4 && i != 9 && i != 14))
                {
                    result = requiredDays[i];
                }
            }
            return result;
        }

        public bool RequirementCheck(Character character, string selectedValue)
        {
            bool result = false;
            int[] requirementsPoint = new int[] { 75, 50, 60, 70, 95,
                                                  50, 70, 5000, 7500, 10000,
                                                  40, 70, 80, 90, 95};
            string[] requirementsName = new string[] { "power", "charisma", "charisma", "charisma", "charisma",
                                                       "power", "power", "money", "money", "money",
                                                       "power", "logic", "logic", "logic", "logic"};

            for (int i = 0; i < Positions.Length; i++)
            {
                if (ReturnPosition(selectedValue) == Positions[i])
                {
                    if (requirementsName[i] == "charisma")
                    {
                        result = (character.charisma + character.charismaBonus) >= requirementsPoint[i];
                        break;
                    }
                    else if (requirementsName[i] == "power")
                    {
                        result = (character.power + character.powerBonus) >= requirementsPoint[i];
                        break;
                    }
                    else if (requirementsName[i] == "logic")
                    {
                        result = (character.logic + character.logicBonus) >= requirementsPoint[i];
                        break;
                    }
                    else if (requirementsName[i] == "money")
                    {
                        result = character.money >= requirementsPoint[i];
                        break;
                    }
                }
            }
            return result;
        }

        public bool CheckTraits(Character character, string selectedValue)
        {
            bool result = true;
            if (character.perkNeutral != "Thoughtful")
            {
                if (ReturnPosition(selectedValue) == "Researcher")
                {
                    result = false;
                }
            }

            if (character.perkNeutral != "Creative")
            {
                if (ReturnPosition(selectedValue) == "Trade Manager")
                {
                    result = false;
                }
            }

            return result;
        }

        public int ReturnWorkingHours(string currentJob)
        {
            int result = 0;
            int[] workingHours = new int[] { 4, 6, 8, 10, 12,
                                             4, 4, 6, 8, 8,
                                             4, 6, 6, 8, 10};
            for (int i = 0; i < Positions.Length; i++)
            {
                if (ReturnPosition(currentJob) == Positions[i])
                {
                    result = workingHours[i]; break;
                }
            }
            return result;
        }

        public int ReturnSalary(string currentJob)
        {
            int result = 0;
            int[] salaryPerHour = new int[] { 15, 25, 50, 75, 150,
                                              25, 35, 85, 100, 110,
                                              10, 30, 50, 90, 150};
            int[] workingHours = new int[] { 4, 6, 8, 10, 12,
                                             4, 4, 6, 8, 8,
                                             4, 6, 6, 8, 10};
            for (int i = 0; i < Positions.Length; i++)
            {
                if (ReturnPosition(currentJob) == Positions[i])
                {
                    result = workingHours[i] * salaryPerHour[i]; break;
                }
            }
            return result;
        }

        public string ReturnPosition(string currentJob)
        {
            return currentJob.Split(" (")[0];
        }
        public string[] AddJobHistory(string position, string[] jobHistory)
        {
            List<string> jobs = new List<string>();
            for (int i = 0; i < jobHistory.Length; i++)
            {
                jobs.Add(jobHistory[i]);
            }
            jobs.Add(position);
            return jobs.ToArray();
        }
        public string ReturnSphere(string position)
        {
            string r = "";
            for (int i = 0; i < Positions.Length; i++)
            {
                if (position == Positions[i])
                {
                    if (i < 5)
                        r = Spheres[0];
                    else if (i >= 5 && i < 10)
                        r = Spheres[1];
                    else if (i >= 10 && i < 15)
                        r = Spheres[2];
                }
            }
            return r;
        }

        
    }
}
