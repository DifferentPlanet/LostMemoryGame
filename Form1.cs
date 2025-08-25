using System.Security.Cryptography.X509Certificates;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows;
using System;
using System.Windows.Forms;
using System.ComponentModel;


namespace Game1
{
    //

    public partial class Form1 : Form
    {
        Character character = new Character();
        WhereToSpendDeneg whereToSpendDeneg = new();
        Work characterWork = new Work();

        int hungerValue = 80;
        int sleepValue = 80;
        int healthValue = 80;
        public Form1()
        {
            InitializeComponent();
        }

        public Color ColorCheck(int value)
        {
            Color color;
            if (value >= 75)
                color = Color.ForestGreen;
            else if (value < 75 && value >= 35)
                color = Color.Chocolate;
            else
                color = Color.DarkRed;
            return color;
        }

        public Color ColorCheckBonus(int value)
        {
            Color color;
            if (value > 0)
                color = Color.ForestGreen;
            else if (value < 0)
                color = Color.DarkRed;
            else
                color = Color.Black;
            return color;
        }
        public string BonusCheck(int value)
        {
            string result;
            if (value > 0)
                result = "+ ";
            else if (value < 0)
                result = " ";
            else
                result = "";
            return result;
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            labelPerks.Text = character.GenerateCharacterPerks();
            labelChar.Text = character.GenerateCharisma().ToString();
            labelPower.Text = character.GeneratePower().ToString();
            labelLogic.Text = character.GenerateLogic().ToString();
            character.money = 0;
            labelMoney.Text = character.money.ToString();

            labelEstate.Text = whereToSpendDeneg.EstateList(0);
            character.estate = whereToSpendDeneg.EstateList(0);
            character.vehicle = whereToSpendDeneg.vehicleArray[0];

            character.daysSpent = 1;
            labelTime.Text = "Day " + character.daysSpent.ToString();

            character.remembersCounter = 0; button6.Enabled = false;
            button8.Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            labelPerks.Text = character.GenerateCharacterPerks();
            labelChar.Text = character.GenerateCharisma().ToString();
            labelPower.Text = character.GeneratePower().ToString();
            labelLogic.Text = character.GenerateLogic().ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            panel2.Enabled = true;
            panel3.Enabled = true;
            tabControl1.Enabled = true;

            button2.Enabled = false;

            character.perkBad = labelPerks.Text.ToString().Split(' ')[1];
            character.perkGood = labelPerks.Text.ToString().Split(' ')[0];
            character.perkNeutral = labelPerks.Text.ToString().Split(' ')[2];
            character.charisma = int.Parse(labelChar.Text.ToString());
            character.power = int.Parse(labelPower.Text.ToString());
            character.logic = int.Parse(labelLogic.Text.ToString());

            character.hoursAvalible = double.Parse(labelHours.Text.ToString());
            character.isWorking = false;
            button7.Enabled = false;

            for (int i = 0; i < whereToSpendDeneg.foodShop.Length; i++)
            {
                listBox1.Items.Add(whereToSpendDeneg.foodShop[i]);
            }
            for (int i = 0; i < whereToSpendDeneg.restaurant.Length; i++)
            {
                listBox2.Items.Add(whereToSpendDeneg.restaurant[i]);
            }
            for (int i = 0; i < whereToSpendDeneg.freeFood.Length; i++)
            {
                listBox3.Items.Add(whereToSpendDeneg.freeFood[i]);
            }
            for (int i = 0; i < whereToSpendDeneg.vehicleArray.Length; i++)
            {
                listBox4.Items.Add(whereToSpendDeneg.vehicleArray[i]);
            }
            for (int i = 0; i < whereToSpendDeneg.estateArray.Length; i++)
            {
                listBox6.Items.Add(whereToSpendDeneg.estateArray[i]);
            }
            for (int i = 0; i < whereToSpendDeneg.itemsToBuy.Length; i++)
            {
                listBox7.Items.Add(whereToSpendDeneg.itemsToBuy[i]);
            }
            listBox1.SelectedIndex = 0;
            listBox2.SelectedIndex = 0;
            listBox3.SelectedIndex = 0;
            listBox4.SelectedIndex = 0;
            listBox6.SelectedIndex = 0;
            listBox7.SelectedIndex = 0;

            if (character.perkBad == "Agressive")
            {
                button9.Visible = true;
                character.robberCounter = 0;
                character.robberReq = character.RobRequiredDays();
            }
            if (character.perkGood == "Kind")
                button8.Visible = false;
            else
                button8.Visible = true;

            character.charismaBonus = 0; character.powerBonus = 0; character.logicBonus = 0;
            character.gameOver = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            character.daysSpent = character.DaysCounter(character.daysSpent);
            labelTime.Text = "Day " + character.daysSpent.ToString();
            character.hoursAvalible = 16;
            labelHours.Text = "16";

            int step = 20;
            hungerValue = character.NeedsReduce(hungerValue, step);
            labelHungerValue.Text = hungerValue.ToString();
            labelHungerValue.ForeColor = ColorCheck(hungerValue);

            if (hungerValue == 0)
            {
                MessageBox.Show("Died from hunger");
                buttonNextDay.Enabled = false;
                tabControl1.SelectTab(2);
                character.gameOver = true;
                return;
            }
            else
            {
                sleepValue = whereToSpendDeneg.EstateSleep(character.estate);   //sleep
                labelSleepValue.Text = sleepValue.ToString();
                labelSleepValue.ForeColor = ColorCheck(sleepValue);

                character.money = character.money - whereToSpendDeneg.EstateCostPerDay(whereToSpendDeneg.EstateListIndex(character.estate));   //estate fee

                int healthInc = 6;

                if (character.estate != "None")
                {
                    healthInc = 3;
                    if (character.money <= 0)
                    {
                        character.money = 0;
                        character.estate = whereToSpendDeneg.EstateList(0);
                        labelEstate.Text = character.estate;

                        panel11.Enabled = false;
                        button13.Enabled = false;
                        character.itemsBought.Clear();
                        richTextBox2.Text = "";
                    }
                }

                if (character.power >= 85)
                    healthInc = healthInc / 3;
                else if (character.power < 85 && character.power >= 50)
                    healthInc = healthInc / 2;

                healthValue = character.NeedsReduce(healthValue, healthInc);
                labelHealthValue.Text = healthValue.ToString();
                labelHealthValue.ForeColor = ColorCheck(healthValue);
                if (healthValue == 0)
                {
                    MessageBox.Show("Died");
                    buttonNextDay.Enabled = false;
                    tabControl1.SelectTab(2);
                    character.gameOver = true;
                    return;
                }
                else
                {
                    if ((healthValue - healthInc) < healthInc)
                        MessageBox.Show("Your health is low");
                }

                labelMoney.Text = character.money.ToString();

                if ((hungerValue - step) <= 0)
                    MessageBox.Show("Eat something soon! You can die from hunger next turn!");
            }

            if (!character.isWorking)
                button4.Enabled = true;
            else
            {
                button12.Enabled = true;
                button7.Enabled = true;

                characterWork.absenseCounter++;
                if (characterWork.absenseCounter == 2)
                {
                    characterWork.daysReqPromo = 0;
                    MessageBox.Show("Are you working? If you continue like this, you will be fired.");
                }
                else if (characterWork.absenseCounter == 3)
                {
                    MessageBox.Show("You're fired.");
                    character.isWorking = false;
                    characterWork.currentJob = null;
                    character.daysOff = 0;
                    characterWork.absenseCounter = 0;
                    characterWork.daysReqPromo = 0;

                    button7.Enabled = false; button10.Enabled = false; button12.Enabled = false; button11.Enabled = false;
                    listBox5.Items.Clear();
                    panel8.Visible = false; label25.Visible = false;
                    labelPosition.Text = "-";
                    labelSalary.Text = "-";
                    labelWH.Text = "-";
                    labelReqForProm.Text = "-";
                }
            }
            if (whereToSpendDeneg.EstateListIndex(character.estate) > 1)
                button4.Enabled = false;
            button5.Enabled = true;
            button8.Enabled = true;
            if (character.remembersCounter > 0)
                button6.Enabled = true;

            if (character.perkBad == "Agressive")
                button9.Enabled = true;
            if (character.daysOff >= 5)
            {
                character.daysOff = 0;
                button10.Enabled = true;
            }
            label25.Visible = false; panel8.Visible = false;

            listBox5.Items.Clear();
            button11.Enabled = false;

            character.charismaBonus = 0; character.powerBonus = 0; character.logicBonus = 0;
            labelCharBonus.Visible = false; labelPowerBonus.Visible = false; labelLogicBonus.Visible = false;

            //remember smth
            if ((character.daysSpent % 5 == 0) && (character.daysSpent <= 100))
            {
                button15.Enabled = true;
            }
        }

        private void buttonCheatsEnter_Click(object sender, EventArgs e)
        {
            string pas = textBox1.Text.ToString();
            if (pas == "password")
            {
                buttonCheat.Text = "Enter";
                buttonCheat.Enabled = true;
                textBox2.Enabled = true;
                label8.Text = "Cheats list\n\nrosebud                 adds 1000" +
                           "\nmotherlode           adds 50000" +
                           "\nreal_estate_lux      adds a home" +
                           "\nsetDaysSpent_X    sets spent days (1-99)";
                label8.Visible = true;
            }
        }

        private void buttonCheat_Click(object sender, EventArgs e)
        {
            string input = textBox2.Text.ToString();
            Cheats cheat = new Cheats();
            if (cheat.TryMoneyCheat(input))
            {
                character.money += cheat.MoneyCheat(input);
                labelMoney.Text = character.money.ToString();
            }
            else if (cheat.TryEstateCheat(input))
            {
                character.estate += cheat.EstateCheat(input);
                labelEstate.Text = character.estate.ToString();
            }
            else if (cheat.DaysSpentCheat(character, input) != character.daysSpent)
            {
                character.daysSpent = cheat.DaysSpentCheat(character, input);
                labelTime.Text = "Day " + character.daysSpent.ToString();
            }

            textBox2.Text = "";
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelSupermarket.Text = "Cost: " + whereToSpendDeneg.foodCostSupermarket(listBox1.SelectedIndex).ToString();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            labelRestaurant.Text = "Cost: " + whereToSpendDeneg.foodCostRestaurant(listBox2.SelectedIndex).ToString();
        }

        private void listBox6_SelectedIndexChanged(object sender, EventArgs e)
        {
            label17.Text = "Cost: " + whereToSpendDeneg.EstateCost(listBox6.SelectedIndex).ToString();
            label16.Text = "+ fee per day: " + whereToSpendDeneg.EstateCostPerDay(listBox6.SelectedIndex).ToString();
        }

        private void buttonSupermarket_Click(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.TimeAvalible(character, 1))
            {
                int cost = whereToSpendDeneg.foodCostSupermarket(listBox1.SelectedIndex);
                if (whereToSpendDeneg.CanAfford(character.money, cost))
                {
                    if (character.perkNeutral == "Vegetarian" && (listBox1.SelectedIndex % 3 != 0 || listBox1.SelectedIndex == 0))
                    {
                        MessageBox.Show("It's not vegetarian.");
                    }
                    else
                    {
                        character.money -= cost;
                        labelMoney.Text = character.money.ToString();
                        hungerValue = character.NeedsIncrease(hungerValue, whereToSpendDeneg.foodHungerSupermarket(listBox1.SelectedIndex));
                        labelHungerValue.Text = hungerValue.ToString();
                        labelHungerValue.ForeColor = ColorCheck(hungerValue);
                        character.hoursAvalible = whereToSpendDeneg.HoursReduce(character, 1);
                        labelHours.Text = character.hoursAvalible.ToString();
                    }
                }
                else
                    MessageBox.Show("You can't afford it!");
            }
            else
                MessageBox.Show("No more time left for today. Try tomorrow.");
        }

        private void buttonRestaurant_Click(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.TimeAvalible(character, 2))
            {
                int cost = whereToSpendDeneg.foodCostRestaurant(listBox2.SelectedIndex);
                if (whereToSpendDeneg.CanAfford(character.money, cost))
                {
                    if (character.perkNeutral == "Vegetarian" && (listBox2.SelectedIndex % 3 != 0 || listBox2.SelectedIndex == 0))
                    {
                        MessageBox.Show("It's not vegetarian.");
                    }
                    else
                    {
                        character.money -= cost;
                        labelMoney.Text = character.money.ToString();

                        hungerValue = character.NeedsIncrease(hungerValue, whereToSpendDeneg.foodHungerRestaurant(listBox2.SelectedIndex));
                        labelHungerValue.Text = hungerValue.ToString();
                        labelHungerValue.ForeColor = ColorCheck(hungerValue);

                        healthValue = character.NeedsIncrease(healthValue, whereToSpendDeneg.foodHealthRestaurant(listBox2.SelectedIndex));
                        labelHealthValue.Text = healthValue.ToString();
                        labelHealthValue.ForeColor = ColorCheck(healthValue);

                        character.hoursAvalible = whereToSpendDeneg.HoursReduce(character, 2);
                        labelHours.Text = character.hoursAvalible.ToString();
                    }
                }
                else
                    MessageBox.Show("You can't afford it!");
            }
            else
                MessageBox.Show("No more time left for today. Try tomorrow.");
        }

        private void buttonTake_Click(object sender, EventArgs e)
        {
            if (character.perkNeutral == "Vegetarian" && (listBox3.SelectedIndex % 3 != 0 || listBox3.SelectedIndex == 0))
            {
                MessageBox.Show("It's not vegetarian.");
            }
            else
            {
                int step = whereToSpendDeneg.foodHealthFree(listBox3.SelectedIndex);
                healthValue = character.NeedsReduce(healthValue, step);
                labelHealthValue.Text = healthValue.ToString();
                labelHealthValue.ForeColor = ColorCheck(healthValue);

                if (healthValue == 0)
                {
                    MessageBox.Show("Died");
                    buttonNextDay.Enabled = false;
                    tabControl1.SelectTab(2);
                    character.gameOver = true;
                    return;
                }
                else
                {
                    hungerValue = character.NeedsIncrease(hungerValue, whereToSpendDeneg.foodHungerFree(listBox3.SelectedIndex));
                    labelHungerValue.Text = hungerValue.ToString();
                    labelHungerValue.ForeColor = ColorCheck(hungerValue);

                    if ((healthValue - step) < step)
                        MessageBox.Show("Be careful, it can be dangerous");
                }
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.EstateList(listBox6.SelectedIndex) == character.estate)
            {
                MessageBox.Show("It's already yours, please choose another one.");
                return;
            }

            int cost = whereToSpendDeneg.EstateCost(listBox6.SelectedIndex) + whereToSpendDeneg.EstateCostPerDay(listBox6.SelectedIndex);
            if (whereToSpendDeneg.CanAfford(character.money, cost))
            {
                character.money -= cost;
                labelMoney.Text = character.money.ToString();
                character.estate = whereToSpendDeneg.EstateList(listBox6.SelectedIndex);
                labelEstate.Text = character.estate.ToString();
                if (whereToSpendDeneg.EstateListIndex(character.estate) > 1)
                    button4.Enabled = false;
                if (whereToSpendDeneg.EstateListIndex(character.estate) >= 1)
                {
                    panel11.Enabled = true;
                    button13.Enabled = true;
                }
                button5.Enabled = false;
            }
            else
                MessageBox.Show("You can't afford it!");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (sleepValue >= 30)
            {
                if (whereToSpendDeneg.TimeAvalible(character, 3))
                {
                    if (whereToSpendDeneg.EstateListIndex(character.estate) <= 1)
                    {
                        int moneyGot = character.Panhandling(character.charisma);
                        if (character.perkGood == "Honest" || character.perkGood == "Sociable" || character.perkBad == "Liar")
                            moneyGot += 5;
                        else if (character.perkBad == "Shy")
                            moneyGot -= 5;
                        character.money += moneyGot;
                        labelMoney.Text = character.money.ToString();
                        character.hoursAvalible = whereToSpendDeneg.HoursReduce(character, 3);
                        labelHours.Text = character.hoursAvalible.ToString();
                        sleepValue -= 30;
                        labelSleepValue.Text = sleepValue.ToString();
                        labelSleepValue.ForeColor = ColorCheck(sleepValue);
                        button4.Enabled = false;
                    }
                }
                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You don't have enough energy. Try tomorrow.");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (sleepValue >= 30)
            {
                if (whereToSpendDeneg.TimeAvalible(character, 4))
                {
                    if (character.robberCounter < character.robberReq - 2)
                    {
                        character.robberCounter++;
                    }
                    else if (character.robberCounter == character.robberReq - 2)
                    {
                        character.robberCounter++;
                        button9.Text = "Rob";
                    }
                    else if (character.robberCounter == character.robberReq - 1)
                    {
                        character.robberCounter = 0;
                        character.robberReq = character.RobRequiredDays();
                        button9.Text = "Rob preparing";

                        int moneyRobbed = character.Rob(character.power);
                        if (moneyRobbed > 0)
                        {
                            character.money += moneyRobbed;
                            labelMoney.Text = character.money.ToString();
                        }
                        else
                        {
                            MessageBox.Show("You've been caught!");
                            if (healthValue <= 25)
                                healthValue = 1;
                            else
                                healthValue = character.NeedsReduce(healthValue, 25);
                            labelHealthValue.Text = healthValue.ToString();
                            labelHealthValue.ForeColor = ColorCheck(healthValue);
                        }
                    }
                    character.hoursAvalible = whereToSpendDeneg.HoursReduce(character, 4);
                    labelHours.Text = character.hoursAvalible.ToString();
                    sleepValue -= 30;
                    labelSleepValue.Text = sleepValue.ToString();
                    labelSleepValue.ForeColor = ColorCheck(sleepValue);
                    button9.Enabled = false;
                }
                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You don't have enough energy. Try tomorrow.");

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (sleepValue >= 30)
            {
                if (whereToSpendDeneg.TimeAvalible(character, 2))
                {
                    int moneyStolen = character.Steal(character.logic);
                    if (moneyStolen > 0)
                    {
                        character.money += moneyStolen;
                        labelMoney.Text = character.money.ToString();
                    }
                    else
                    {
                        MessageBox.Show("You've been caught!");
                        if (healthValue <= 10)
                            healthValue = 1;
                        else
                            healthValue = character.NeedsReduce(healthValue, 10);
                        labelHealthValue.Text = healthValue.ToString();
                    }

                    labelMoney.Text = character.money.ToString();
                    character.hoursAvalible = whereToSpendDeneg.HoursReduce(character, 2);
                    labelHours.Text = character.hoursAvalible.ToString();
                    sleepValue -= 30;
                    labelSleepValue.Text = sleepValue.ToString();
                    labelSleepValue.ForeColor = ColorCheck(sleepValue);
                    button8.Enabled = false;
                }

                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You don't have enough energy. Try tomorrow.");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (sleepValue >= 45)
            {
                if (character.hoursAvalible >= characterWork.ReturnWorkingHours(characterWork.currentJob))
                {
                    character.daysOff++; button10.Enabled = false;
                    button7.Enabled = false;
                    character.money += characterWork.ReturnSalary(characterWork.currentJob);
                    labelMoney.Text = character.money.ToString();
                    character.hoursAvalible -= characterWork.ReturnWorkingHours(characterWork.currentJob);
                    labelHours.Text = character.hoursAvalible.ToString();
                    sleepValue -= 45;
                    labelSleepValue.Text = sleepValue.ToString();
                    labelSleepValue.ForeColor = ColorCheck(sleepValue);

                    characterWork.absenseCounter = 0;
                    characterWork.daysReqPromo++;
                }
                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You don't have enough energy. Try tomorrow.");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label25.Visible = true;
            panel8.Visible = true;
            button6.Enabled = false;

            characterWork.avalibleJobs = characterWork.ReturnAvalibleJobs(characterWork.currentJob);
            for (int i = 0; i < characterWork.avalibleJobs.Count; i++)
            {
                listBox5.Items.Add(characterWork.avalibleJobs[i]);
            }
        }

        private void listBox5_SelectedIndexChanged(object sender, EventArgs e)
        {
            button11.Enabled = true;
            labelApplyRequirement.Text = characterWork.ReturnRequirement(listBox5.SelectedItem.ToString());
        }

        private void button10_Click(object sender, EventArgs e)
        {
            button10.Enabled = false; button7.Enabled = false;
            character.daysOff = 0;
            characterWork.absenseCounter--;
            character.money += characterWork.ReturnSalary(characterWork.currentJob);
            labelMoney.Text = character.money.ToString();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Are you sure?", "", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                character.isWorking = false;
                characterWork.currentJob = null;
                character.daysOff = 0;
                characterWork.absenseCounter = 0;

                button7.Enabled = false; button10.Enabled = false; button12.Enabled = false; button11.Enabled = false;
                listBox5.Items.Clear();
                panel8.Visible = false; label25.Visible = false;
                labelPosition.Text = "-";
                labelSalary.Text = "-";
                labelWH.Text = "-";
                labelReqForProm.Text = "-";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (characterWork.RequirementCheck(character, listBox5.SelectedItem.ToString()))
            {
                if (characterWork.CheckTraits(character, listBox5.SelectedItem.ToString()))
                {
                    if (!character.isWorking || characterWork.daysReqPromo >= characterWork.ReturnDaysReqPromo(listBox5.SelectedItem.ToString()))
                    {
                        if (labelApplyRequirement.Text.Contains("money"))
                        {
                            character.money -= int.Parse(labelApplyRequirement.Text.Split(' ')[2]);
                            labelMoney.Text = character.money.ToString();
                        }
                        button11.Enabled = false;
                        character.isWorking = true;
                        characterWork.currentJob = listBox5.SelectedItem.ToString();
                        character.daysOff = 0;
                        characterWork.avalibleJobs = characterWork.ReturnAvalibleJobs(characterWork.currentJob);

                        listBox5.Items.Clear();
                        for (int i = 0; i < characterWork.avalibleJobs.Count; i++)
                        {
                            listBox5.Items.Add(characterWork.avalibleJobs[i]);
                        }

                        characterWork.absenseCounter = 0;
                        characterWork.daysReqPromo = 0;

                        labelPosition.Text = characterWork.currentJob;
                        labelSalary.Text = characterWork.ReturnSalary(characterWork.currentJob).ToString();
                        labelWH.Text = characterWork.ReturnWorkingHours(characterWork.currentJob).ToString();
                        labelReqForProm.Text = characterWork.ReturnRequirementPromo(characterWork.currentJob);

                        labelApplyRequirement.Text = "Requirement:";
                    }
                    else
                        MessageBox.Show("Hmm... You're not ready yet, work harder. Maybe next time.");
                }
                else
                    MessageBox.Show("You can't work on this position. Maybe this job just isn't yours?");
            }
            else
                MessageBox.Show("Check the requirements!");
        }

        private void panel8_MouseClick(object sender, MouseEventArgs e)
        {
            panel8.Visible = false;
            label25.Visible = false;
            listBox5.Items.Clear();
            labelApplyRequirement.Text = "Requirement:";
            button6.Enabled = true;
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (character.daysSpent < 100)
            {
                string add = "";
                if (character.remembersCounter == 0)
                {
                    button6.Enabled = true;
                    if (character.perkNeutral == "Pragmatic")
                    {
                        character.money += 50;
                        labelMoney.Text = character.money.ToString();
                        add = "\nP. S. Also you found some money in your pocket.";
                    }
                }

                if (character.remembersCounter == 1)
                {
                    if (character.perkGood == "Optimistic")
                    {
                        character.money += 1;
                        labelMoney.Text = character.money.ToString();
                        add = "\nP. S. Also you found a coin on the street.";
                    }
                }

                MessageBox.Show(character.RememberReturn(character, character.remembersCounter) + add);
                richTextBox1.Text += labelTime.Text + "\n" +
                                     character.RememberReturn(character, character.remembersCounter) + add + "\n";
                add = "";
                character.remembersCounter++;
            }
            else if (character.daysSpent >= 100)
            {
                character.remembersCounter = 20;
                MessageBox.Show(character.RememberReturn(character, character.remembersCounter));
                richTextBox1.Text += labelTime.Text + "\n" +
                                     character.RememberReturn(character, character.remembersCounter) + "\n";
                string strory = character.LastMemory();
                MessageBox.Show(strory);
                richTextBox1.Text += strory;
                var result = MessageBox.Show("Would you like to come back to your life?", "", MessageBoxButtons.YesNo);
                if (result == DialogResult.Yes)
                {
                    //finishTheGame
                    MessageBox.Show("Thank you for these " + character.daysSpent.ToString() + " days. Good luck, have fun!");
                    tabControl1.SelectTab(2);
                    character.gameOver = true;
                }
            }
            button15.Enabled = false;
        }

        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            label23.Text = "Cost: " + whereToSpendDeneg.vehicleCost(listBox4.SelectedIndex);
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.vehicleArray[listBox4.SelectedIndex] == character.vehicle)
            {
                MessageBox.Show("It's already yours, please choose another one.");
                return;
            }

            int cost = whereToSpendDeneg.vehicleCost(listBox4.SelectedIndex);
            if (whereToSpendDeneg.CanAfford(character.money, cost))
            {
                character.money -= cost;
                labelMoney.Text = character.money.ToString();
                character.vehicle = whereToSpendDeneg.vehicleArray[listBox4.SelectedIndex];
                labelVehicle.Text = character.vehicle.ToString();
            }
            else
                MessageBox.Show("You can't afford it!");
        }

        private void label24_Click(object sender, EventArgs e)
        {

        }

        private void listBox7_SelectedIndexChanged(object sender, EventArgs e)
        {
            label32.Text = "Cost: " + whereToSpendDeneg.ItemsCost(listBox7.SelectedIndex).ToString();
            if (character.estate != "None")
                button13.Enabled = true;
            else
                button13.Enabled = false;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (character.itemsBought.Contains(listBox7.SelectedItem.ToString()))
            {
                MessageBox.Show("It's already yours, please choose another one.");
                return;
            }

            int cost = whereToSpendDeneg.ItemsCost(listBox7.SelectedIndex);
            if (whereToSpendDeneg.CanAfford(character.money, cost))
            {
                character.money -= cost;
                labelMoney.Text = character.money.ToString();
                character.itemsBought.Add(listBox7.SelectedItem.ToString());

                richTextBox2.Text += listBox7.SelectedItem.ToString() + "\n";
            }
            else
                MessageBox.Show("You can't afford it!");
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {
            if (character.itemsBought.Contains("Shower gel")) button17.Enabled = true;
            else button17.Enabled = false;
            if (character.itemsBought.Contains("Yoga mat")) button20.Enabled = true;
            else button20.Enabled = false;
            if (character.itemsBought.Contains("PC") && character.itemsBought.Contains("Game")) button18.Enabled = true;
            else button18.Enabled = false;
            if (character.itemsBought.Contains("Mirrow")) button21.Enabled = true;
            else button21.Enabled = false;
            if (character.itemsBought.Contains("TV")) button22.Enabled = true;
            else button22.Enabled = false;
            if (character.itemsBought.Contains("Chess")) button19.Enabled = true;
            else button19.Enabled = false;
            if (character.itemsBought.Contains("Running machine")) button23.Enabled = true;
            else button23.Enabled = false;
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.TimeAvalible(character, 2))
            {
                character.hoursAvalible = whereToSpendDeneg.HoursReduce(character, 2);
                labelHours.Text = character.hoursAvalible.ToString();
                sleepValue = character.NeedsIncrease(sleepValue, 30);
                labelSleepValue.Text = sleepValue.ToString();
                labelSleepValue.ForeColor = ColorCheck(sleepValue);
            }
            else
                MessageBox.Show("No more time left for today. Try tomorrow.");
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (character.hoursAvalible != 0)
            {
                character.itemsBought.Remove("Shower gel");
                richTextBox2.Text = "";
                foreach (var item in character.itemsBought)
                    richTextBox2.Text += item.ToString() + "\n";
                character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 1);
                labelHours.Text = character.hoursAvalible.ToString();

                character.charismaBonus += 5;
                if (BonusCheck(character.charismaBonus) != "")
                {
                    labelCharBonus.Text = BonusCheck(character.charismaBonus) + character.charismaBonus.ToString();
                    labelCharBonus.ForeColor = ColorCheckBonus(character.charismaBonus);
                    labelCharBonus.Visible = true;
                }
                else labelCharBonus.Visible = false;
            }
            else
                MessageBox.Show("No more time left for today. Try tomorrow.");
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (character.hoursAvalible != 0)
            {
                character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 2);
                labelHours.Text = character.hoursAvalible.ToString();

                character.powerBonus += 5;
                if (BonusCheck(character.powerBonus) != "")
                {
                    labelPowerBonus.Text = BonusCheck(character.powerBonus) + character.powerBonus.ToString();
                    labelPowerBonus.ForeColor = ColorCheckBonus(character.powerBonus);
                    labelPowerBonus.Visible = true;
                }
                else labelPowerBonus.Visible = false;
            }
            else
                MessageBox.Show("No more time left for today. Try tomorrow.");
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (character.hoursAvalible != 0)
            {
                character.itemsBought.Remove("Game");
                richTextBox2.Text = "";
                foreach (var item in character.itemsBought)
                    richTextBox2.Text += item.ToString() + "\n";
                character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 2);
                labelHours.Text = character.hoursAvalible.ToString();

                character.logicBonus += 5;
                if (BonusCheck(character.logicBonus) != "")
                {
                    labelLogicBonus.Text = BonusCheck(character.logicBonus) + character.logicBonus.ToString();
                    labelLogicBonus.ForeColor = ColorCheckBonus(character.logicBonus);
                    labelLogicBonus.Visible = true;
                }
                else labelLogicBonus.Visible = false;

                character.powerBonus -= 2;
                if (BonusCheck(character.powerBonus) != "")
                {
                    labelPowerBonus.Text = BonusCheck(character.powerBonus) + character.powerBonus.ToString();
                    labelPowerBonus.ForeColor = ColorCheckBonus(character.powerBonus);
                    labelPowerBonus.Visible = true;
                }
                else labelPowerBonus.Visible = false;
            }
            else
                MessageBox.Show("No more time left for today. Try tomorrow.");
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (character.hoursAvalible != 0)
            {
                character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 1);
                labelHours.Text = character.hoursAvalible.ToString();

                character.charismaBonus += 7;
                if (BonusCheck(character.charismaBonus) != "")
                {
                    labelCharBonus.Text = BonusCheck(character.charismaBonus) + character.charismaBonus.ToString();
                    labelCharBonus.ForeColor = ColorCheckBonus(character.charismaBonus);
                    labelCharBonus.Visible = true;
                }
                else labelCharBonus.Visible = false;
            }
            else
                MessageBox.Show("No more time left for today. Try tomorrow.");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (character.hoursAvalible != 0)
            {
                character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 1);
                labelHours.Text = character.hoursAvalible.ToString();

                character.powerBonus += 3;
                if (BonusCheck(character.powerBonus) != "")
                {
                    labelPowerBonus.Text = BonusCheck(character.powerBonus) + character.powerBonus.ToString();
                    labelPowerBonus.ForeColor = ColorCheckBonus(character.powerBonus);
                    labelPowerBonus.Visible = true;
                }
                else labelPowerBonus.Visible = false;
            }
            else
                MessageBox.Show("No more time left for today. Try tomorrow.");
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (character.hoursAvalible != 0)
            {
                character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 1);
                labelHours.Text = character.hoursAvalible.ToString();

                character.logicBonus += 3;
                if (BonusCheck(character.logicBonus) != "")
                {
                    labelLogicBonus.Text = BonusCheck(character.logicBonus) + character.logicBonus.ToString();
                    labelLogicBonus.ForeColor = ColorCheckBonus(character.logicBonus);
                    labelLogicBonus.Visible = true;
                }
                else labelLogicBonus.Visible = false;
            }
            else
                MessageBox.Show("No more time left for today. Try tomorrow.");
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (character.hoursAvalible != 0)
            {
                character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 2);
                labelHours.Text = character.hoursAvalible.ToString();

                character.logicBonus -= 5;
                if (BonusCheck(character.logicBonus) != "")
                {
                    labelLogicBonus.Text = BonusCheck(character.logicBonus) + character.logicBonus.ToString();
                    labelLogicBonus.ForeColor = ColorCheckBonus(character.logicBonus);
                    labelLogicBonus.Visible = true;
                }
                else labelLogicBonus.Visible = false;
            }
            else
                MessageBox.Show("No more time left for today. Try tomorrow.");
        }
        private void button24_MouseMove(object sender, MouseEventArgs e)
        {
            label36.Visible = true;
        }
        private void button24_MouseLeave(object sender, EventArgs e)
        {
            label36.Visible = false;
        }
        private void button25_MouseMove(object sender, MouseEventArgs e)
        {
            label38.Visible = true;
        }
        private void button25_MouseHover(object sender, EventArgs e)
        {

        }
        private void button25_MouseLeave(object sender, EventArgs e)
        {
            label38.Visible = false;
        }
        private void button27_MouseMove(object sender, MouseEventArgs e)
        {
            label39.Visible = true;
        }
        private void button27_MouseLeave(object sender, EventArgs e)
        {
            label39.Visible = false;
        }
        private void button26_MouseMove(object sender, MouseEventArgs e)
        {
            label40.Visible = true;
        }
        private void button26_MouseLeave(object sender, EventArgs e)
        {
            label40.Visible = false;
        }
        private void button31_MouseMove(object sender, MouseEventArgs e)
        {
            label41.Visible = true;
        }
        private void button31_MouseLeave(object sender, EventArgs e)
        {
            label41.Visible = false;
        }
        private void button30_MouseMove(object sender, MouseEventArgs e)
        {
            label42.Visible = true;
        }
        private void button30_MouseLeave(object sender, EventArgs e)
        {
            label42.Visible = false;
        }
        private void button29_MouseMove(object sender, MouseEventArgs e)
        {
            label43.Visible = true;
        }
        private void button29_MouseLeave(object sender, EventArgs e)
        {
            label43.Visible = false;
        }
        private void button28_MouseMove(object sender, MouseEventArgs e)
        {
            label44.Visible = true;
        }
        private void button28_MouseLeave(object sender, EventArgs e)
        {
            label44.Visible = false;
        }
        private void button32_MouseMove(object sender, MouseEventArgs e)
        {
            label45.Visible = true;
        }
        private void button32_MouseLeave(object sender, EventArgs e)
        {
            label45.Visible = false;
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.CanAfford(character.money, 50))
            {
                if (character.hoursAvalible >= 2)
                {
                    character.money -= 50;
                    labelMoney.Text = character.money.ToString();

                    character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 2);
                    labelHours.Text = character.hoursAvalible.ToString();

                    character.logic = character.NeedsReduce(character.logic, 2);
                    labelLogic.Text = character.logic.ToString();
                }
                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You can't afford it!");
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.CanAfford(character.money, 75))
            {
                if (character.hoursAvalible >= 2)
                {
                    character.money -= 75;
                    labelMoney.Text = character.money.ToString();

                    character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 2);
                    labelHours.Text = character.hoursAvalible.ToString();

                    character.power = character.NeedsIncrease(character.power, 3);
                    labelPower.Text = character.power.ToString();
                }
                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You can't afford it!");
        }

        private void button26_Click(object sender, EventArgs e)
        {

            if (whereToSpendDeneg.CanAfford(character.money, 50))
            {
                if (character.hoursAvalible >= 2)
                {
                    character.money -= 50;
                    labelMoney.Text = character.money.ToString();

                    character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 2);
                    labelHours.Text = character.hoursAvalible.ToString();

                    character.logic = character.NeedsReduce(character.logic, 1);
                    labelLogic.Text = character.logic.ToString();
                    character.charisma = character.NeedsIncrease(character.charisma, 3);
                    labelChar.Text = character.charisma.ToString();
                }
                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You can't afford it!");
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.CanAfford(character.money, 30))
            {
                if (character.hoursAvalible >= 2)
                {
                    character.money -= 30;
                    labelMoney.Text = character.money.ToString();

                    character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 2);
                    labelHours.Text = character.hoursAvalible.ToString();

                    character.logic = character.NeedsIncrease(character.logic, 1);
                    labelLogic.Text = character.logic.ToString();
                }
                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You can't afford it!");
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.CanAfford(character.money, 50))
            {
                if (character.hoursAvalible >= 2)
                {
                    character.money -= 50;
                    labelMoney.Text = character.money.ToString();

                    character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 2);
                    labelHours.Text = character.hoursAvalible.ToString();

                    character.logic = character.NeedsIncrease(character.logic, 2);
                    labelLogic.Text = character.logic.ToString();
                }
                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You can't afford it!");
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.CanAfford(character.money, 1000))
            {
                if (character.hoursAvalible >= 5)
                {
                    character.money -= 1000;
                    labelMoney.Text = character.money.ToString();

                    character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 5);
                    labelHours.Text = character.hoursAvalible.ToString();

                    character.logic = character.NeedsIncrease(character.logic, 10);
                    labelLogic.Text = character.logic.ToString();
                }
                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You can't afford it!");
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.CanAfford(character.money, 50))
            {
                if (character.hoursAvalible >= 3)
                {
                    character.money -= 50;
                    labelMoney.Text = character.money.ToString();

                    character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 3);
                    labelHours.Text = character.hoursAvalible.ToString();

                    healthValue = character.NeedsIncrease(healthValue, 25);
                    labelHealthValue.Text = healthValue.ToString();
                }
                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You can't afford it!");
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (character.hoursAvalible >= 4)
            {
                character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 4);
                labelHours.Text = character.hoursAvalible.ToString();

                character.power = character.NeedsIncrease(character.power, 1);
                labelPower.Text = character.power.ToString();
            }
            else
                MessageBox.Show("No more time left for today. Try tomorrow.");
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (character.hoursAvalible >= 5)
            {
                character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 5);
                labelHours.Text = character.hoursAvalible.ToString();

                character.logic = character.NeedsIncrease(character.logic, 1);
                labelLogic.Text = character.logic.ToString();
                character.charisma = character.NeedsIncrease(character.charisma, 1);
                labelChar.Text = character.charisma.ToString();
            }
            else
                MessageBox.Show("No more time left for today. Try tomorrow.");
        }

        private void button33_MouseMove(object sender, MouseEventArgs e)
        {
            label46.Visible = true;
        }
        private void button33_MouseLeave(object sender, EventArgs e)
        {
            label46.Visible = false;
        }
        private void button34_MouseMove(object sender, MouseEventArgs e)
        {
            label47.Visible = true;
        }
        private void button34_MouseLeave(object sender, EventArgs e)
        {
            label47.Visible = false;
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.CanAfford(character.money, 5000))
            {
                if (character.hoursAvalible >= 16)
                {
                    character.money -= 5000;
                    labelMoney.Text = character.money.ToString();

                    character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 16);
                    labelHours.Text = character.hoursAvalible.ToString();

                    healthValue = character.NeedsIncrease(healthValue, 100);
                    hungerValue = character.NeedsIncrease(hungerValue, 100);
                    labelHealthValue.Text = healthValue.ToString();
                    labelHealthValue.ForeColor = ColorCheck(healthValue);
                    labelHungerValue.Text = hungerValue.ToString();
                    labelHungerValue.ForeColor = ColorCheck(hungerValue);
                }
                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You can't afford it!");
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (whereToSpendDeneg.CanAfford(character.money, 200))
            {
                if (character.hoursAvalible >= 1)
                {
                    character.money -= 200;
                    labelMoney.Text = character.money.ToString();

                    character.hoursAvalible = whereToSpendDeneg.HoursReduceWOVehicle(character, 1);
                    labelHours.Text = character.hoursAvalible.ToString();

                    character.logic -= 5;
                    character.powerBonus += 5;
                    labelLogic.Text = character.logic.ToString();
                    if (BonusCheck(character.powerBonus) != "")
                    {
                        labelPowerBonus.Text = BonusCheck(character.powerBonus) + character.powerBonus.ToString();
                        labelPowerBonus.ForeColor = ColorCheckBonus(character.powerBonus);
                        labelPowerBonus.Visible = true;
                    }
                    else labelPowerBonus.Visible = false;
                    sleepValue = character.NeedsIncrease(sleepValue, 30);
                    labelSleepValue.Text = sleepValue.ToString();
                    labelSleepValue.ForeColor = ColorCheck(healthValue);
                }
                else
                    MessageBox.Show("No more time left for today. Try tomorrow.");
            }
            else
                MessageBox.Show("You can't afford it!");
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            if (character.gameOver)
            {
                e.Cancel = true;
            }
        }
    }
}
        