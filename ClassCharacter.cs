using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    internal class Character
    {
        public string perkGood, perkBad, perkNeutral;
        public int charisma, power, logic;
        public int daysSpent;
        public double hoursAvalible;
        public string estate, vehicle;
        public int money;
        public bool isWorking; public int daysOff;
        public int robberCounter, robberReq;
        public int remembersCounter;
        public List<string> itemsBought = new List<string>();
        public int charismaBonus, powerBonus, logicBonus;
        public bool gameOver;

        public string GenerateCharacterPerks()
        {
            string[] perksGood = new string[] { "Honest", "Kind", "Optimistic", "Sociable" };
            string[] perksBad = new string[] { "Liar", "Agressive", "Pessimistic", "Shy"};
            string[] perksNeutral = new string[] { "Thoughtful", "Vegetarian", "Pragmatic", "Creative" };

            Random rnd = new Random();

            int intGood = rnd.Next(0, 4); int intBad = rnd.Next(0, 4);
            string perkGood = perksGood[intGood];
            while (intBad == intGood)
            {
                intBad = rnd.Next(0, 4);
            }
            string perkBad = perksBad[intBad];
            string perkNeutral = perksNeutral[rnd.Next(0, 4)];

            return perkGood + " " + perkBad + " " + perkNeutral;
        }

        public int GenerateCharisma()
        {
            Random rnd = new Random();
            return rnd.Next(20, 101);
        }
        public int GeneratePower()
        {
            Random rnd = new Random();
            return rnd.Next(20, 101);
        }
        public int GenerateLogic()
        {
            Random rnd = new Random();
            return rnd.Next(20, 101);
        }

        public int DaysCounter(int days)
        {
            days = days + 1;
            return days;
        }

        public int NeedsReduce(int needs, int value)
        {
            needs = needs - value;
            if (needs < 0)
                needs = 0;
            return needs;
        }
        public int NeedsIncrease(int needs, int value)
        {
            needs = needs + value;
            if (needs > 100)
                needs = 100;
            return needs;
        }

        public int RobRequiredDays()
        {
            Random rnd = new Random();
            return rnd.Next(4, 9);
        }

        public int Rob(int power)
        {
            Random rnd = new Random();
            int chance;
            if (power < 75)
                chance = rnd.Next(0, power);
            else
                chance = rnd.Next(26, 101);
            if (chance > 25)
            {
                return rnd.Next(10, 235);
            }
            else return 0;
        }

        public int Steal(int logic)
        {
            Random rnd = new Random();
            int chance;
            if (logic < 70)
                chance = rnd.Next(0, logic);
            else
                chance = rnd.Next(15, 101);
            if (chance > 25)
            {
                return rnd.Next(15, 25);
            }
            else return 0;
        }

        public int Panhandling(int charisma)
        {
            Random rnd = new Random();
            if (charisma >= 85)
                return rnd.Next(20, 26);
            else if (charisma < 85 && charisma >= 65)
                return rnd.Next(12, 18);
            else return rnd.Next(5, 11);
        }

        string[] memoryNeutral = new string[] { "Hmm... It was really hard, but it seems like you remember your name. Now you can find a job. Congratulations!",
                                                "You saw a black cat in the morning. It looked strangely familiar...",  //1
                                                "You had a dream tonight. It was a dream about your school... Where were you studying?",  //2
                                                "You were trying so hard, but... No, nothing for today.",  //3
                                                "Familiar house... You saw it yesterday. What does it mean?",  //4
                                                "Maybe you should buy a shower gel with mint? Its smell reminds you something...",  //5
                                                "You were trying so hard, but... No, nothing for today.",  //6
                                                "When you were a teen, you were obsessed with two things: cars and writing poems about love. You don't need silly poems anymore, maybe you should think about buying a car?",
                                                "Maybe you should play chess to improve your logic?",  //8
                                                "You were trying so hard, but... No, nothing for today.",  //9
                                                "Obviously, you had a running machine some time ago. Maybe you should buy it?",  //10
                                                "You woke up this morning and realized that you want to go to a restaurant.",  //11
                                                "You saw a kid on the street. Do you know this kid?",  //12
                                                "Do you like watching TV?",  //13
                                                "You were trying so hard, but... No, nothing for today.",  //14
                                                "You saw this kid again... His face...",  //15
                                                "\"Darling...\", - that's how someone called you. Who? Where? When?",  //16
                                                "You were trying so hard, but... No, nothing for today.",  //17
                                                "You found a golden ring in your back pocket. Is it yours?",  //18
                                                "You were trying so hard, but... No, nothing for today.",  //19
                                                "Wait. You remember all your life..."};
        string[] memoryOpt = new string[] { "Wow! It was not so easy, but it seems like you remember your name. Now you finally can find a job. Congratulations!",
                                            "You saw a beautiful black cat in the morning. They say it could be a good sign!",  //1
                                            "Wow, you had such a nice dream tonight! It was a dream about your school. So interesting!",  //2
                                            "Nothing for today. Let's try next time! <3",  //3
                                            "You saw very familiar house yesterday. But where? You forgot. Never mind, let's try once more!",  //4
                                            "Maybe you should buy a shower gel with mint? Smells good, tastes good! You're sure you've already had it some time ago.",  //5
                                            "Nothing for today. Let's try next time! :)",  //6
                                            "When you were a teen, you were obsessed with two things: cars and writing poems about love. You don't care about love anymore, and what about cars?",
                                            "Suddenly you want to play chess... It can improve your logic!",  //8
                                            "Nothing for today. Let's try next time! =^.^=",  //9
                                            "Obviously, you had a running machine some time ago. You loved it so much!",  //10
                                            "You want to go to a restaurant. Now! :)",  //11
                                            "You saw a cute kid on the street. Maybe you know this kid?",  //12
                                            "You watched some TV show a year ago and you loved it. Maybe you should buy a TV?",  //13
                                            "Nothing for today. Let's try next time! <3",  //14
                                            "You saw this kid again... His face...",  //15
                                            "\"Darling...\", - that's how someone called you. Who? Where? When?",  //16
                                            "Nothing for today. Let's try next time! =^.^=",  //17
                                            "You found a golden ring in your back pocket. Are you married?",  //18
                                            "Nothing for today. Let's try next time! :)",  //19
                                            "Wait. You remember all your life!"};
        string[] memoryPes = new string[] { "Meh... Okay, you remember (what for..?) your name. Now you can find a job.",  //0
                                            "You saw a black cat in the morning. Maybe it's a bad sign?", //1
                                            "You had a nightmare tonight. It was a dream about your school... No, you don't want to even think about it...",
                                            "Meh... Nothing.",  //3
                                            "You. Just. Remember. NOTHING.",  //4
                                            "Mint shower gel... Why do you ever think about it? Useless...", //5
                                            "Meh... Nothing. Again.",  //6
                                            "When you were a teen, you were obsessed with two things: cars and writing poems about love. Do you need any of these now?",
                                            "You feel stupid. Chess could help...",  //8
                                            "Nothing. Again. :(",  //9
                                            "Obviously, you had a running machine some time ago. You're so fat... Let's do some sport.",
                                            "You don't like any food... Maybe you shoud try something different?", //11
                                            "You saw a kid on the street. You could recognise this kid if you had a bit more time.",  //12
                                            "Do you like watching TV? Some TV shows can be interesting...",  //13
                                            "--justnothingfortoday--",  //14
                                            "You saw this kid again... His face...",  //15
                                            "\"Darling...\", - that's how someone called you. Who? Where? When?",  //16
                                            "No memories for today. :(",  //17
                                            "You found a golden ring in your back pocket. Did you steal it?",  //18
                                            "Meh... No, not today.",  //19
                                            "Oh no... You remember all your life..."};
        public string RememberReturn(Character character, int counter)
        {
            if (character.daysSpent >= 100)
                counter = memoryNeutral.Length - 1;

            if (character.perkGood == "Optimistic")
                return memoryOpt[counter];
            else if (character.perkBad == "Pessimistic")
                return memoryPes[counter];
            else
                return memoryNeutral[counter];
        }

        public string LastMemory()
        {
            string[] FirstPart = new string[] { "You were a doctor. You had a nice family.", "You had no job, but you had a family.",
                                                "You were drug-addicted. Your family hated you.", "You were a soldier. You had a family.",
                                                "You were a politician. You family never understood you.", "You were a criminal person. Your family was afraid of you."};
            string[] MiddlePart = new string[] { "You have 3 children, your parthner is a realy nice person.", "You have one child and your parthner left you.",
                                                 "You were infertile, but somehow your wife gave birth to your child.", "You had 2 children. Your parthner passed away, you were depressed."};
            string[] LastPart = new string[] { "you just forgot everything. You had a disease.", "you were attacked by someone and forgot everything.",
                                               "you fell out of the window.", "you drunk too much one day. That's why you forgot everything.",
                                               "you got into a car accident.", "was a war, and you forgot everything because of your emotional state.",
                                               "you realyzed, that your life was too boring to exist. That's why you forgot everything."};

            Random rnd = new Random();
            int first = rnd.Next(0, FirstPart.Length);
            int middle = rnd.Next(0, MiddlePart.Length);
            int last = rnd.Next(0, LastPart.Length);

            string story = FirstPart[first] + " About your family... " + MiddlePart[middle] + " But then " + LastPart[last];
            return story;
        }
    }
}
