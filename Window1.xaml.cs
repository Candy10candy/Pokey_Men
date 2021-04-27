using Pokey_Men.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Pokey_Men
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<Fighter> team = new List<Fighter>();
        List<Man> opponents = new List<Man>();

        private readonly BackgroundWorker worker = new BackgroundWorker();

        public int choice = 0;
        public int healTime = 1;
        public bool Flee = false;
        public int i = 0;
        public int j = 0;
        public bool turn = true;
        public int lastDamage = 0;
        
        
        public Window1()
        {
            InitializeComponent();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += worker_ProgressChanged;

            team.Add(new Fighter(52, 183));
            team.Add(new Fighter(61, 156));
            team.Add(new Fighter(87, 147));

            opponents.Add(new Man(52, 183));
            opponents.Add(new Man(61, 156));
            opponents.Add(new Man(87, 147)); 

            PickScreen.Items.Add(team[0]);
            PickScreen.Items.Add(team[1]);
            PickScreen.Items.Add(team[2]);

        }
        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //switch fighters
            if (team[i].Health <= 0 && i < 2)
            {
                i++;
                YourHPBar.Maximum = team[i].Health;
            }
            if (opponents[j].Health <= 0 && j < 2)
            { 
                j++;
                EnemyHPBar.Maximum = opponents[j].Health;
            }

            //update labels
            EnemyHPBar.Value = opponents[j].Health;
            YourHPBar.Value = team[i].Health;
            YourHP.Content = YourHPBar.Value;
            EnemyHP.Content = EnemyHPBar.Value;
            EnergyLabel.Content = team[i].energy;
            EnergyBar.Value = team[i].energy;

            if (lastDamage != 0)
            {
                DamageLabel.Content = "Dealt " + lastDamage + " damage!";
            }
            else
            {
                DamageLabel.Content = "";
            }

            //enable/disable moves
            Attack1.IsEnabled = team[i].energy >= 6 && turn;
            Attack2.IsEnabled = team[i].energy >= 3 && turn;
            Attack3.IsEnabled = team[i].energy >= 1 && turn;
            Attack4.IsEnabled = team[i].energy >= 0 && turn;
            Heal.IsEnabled = team[i].energy >= 2 && turn && healTime <= 2;
            FleeButton.IsEnabled = turn;

            //update fighter info
            PickScreen.Items.Clear();
            PickScreen.Items.Add(team[0]);
            PickScreen.Items.Add(team[1]);
            PickScreen.Items.Add(team[2]);

            
                //keep energy in bounds
                if (team[i].energy > 10)
                { team[i].energy = 10; }

                if (opponents[i].Energy > 10)
                { opponents[i].Energy = 10; }

                //switch names/sprites for fighters
                if (i == 1)
                {
                    YouSprite.Source = new BitmapImage(new Uri("leaf.png", UriKind.Relative));
                    YourName.Content = "Leaf Sprite";
                    Attack1.Content = "Vine Whip: 6 Energy";
                    Attack2.Content = "Needles: 3 Energy";
                    Attack3.Content = "Spore: 1 Energy";
                    Attack4.Content = "Pollen: 0 Energy";
                }
                if (i == 2)
                {
                    YouSprite.Source = new BitmapImage(new Uri("fire.png", UriKind.Relative));
                    YourName.Content = "Fire Sprite";
                    Attack1.Content = "Incinerate: 6 Energy";
                    Attack2.Content = "Burn: 3 Energy";
                    Attack3.Content = "Spark: 1 Energy";
                    Attack4.Content = "Ember: 0 Energy";
                }

                if (j == 1)
                {
                    EnemySprite.Source = new BitmapImage(new Uri("fire.png", UriKind.Relative));
                    EnemyName.Content = "Fire Sprite";
                }

                if (j == 2)
                {
                    EnemySprite.Source = new BitmapImage(new Uri("water.png", UriKind.Relative));
                    EnemyName.Content = "Water Sprite";
                }


          

        }

        private void worker_RunWorkerCompleted(object sender,
                                                    RunWorkerCompletedEventArgs e)
        {
            //check for win/loss
            if (opponents[2].Health <= 0)
            {
                Window2 win = new Window2();
                win.Show();
                Close();
            }
            else if (team[2].Health <= 0 && opponents[2].Health > 0)
            {
                Window3 lose = new Window3();
                lose.Show();
                Close();
            }

        }

        //alternate turns
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while ((team[0].Health > 0 || team[1].Health > 0 || team[2].Health > 0) && opponents[2].Health > 0 && !Flee)
            {
                turn = true;
                choice = 0;

                while (choice == 0)
                {
                  
                }

                turn = false;
                worker.ReportProgress(0);

                System.Threading.Thread.Sleep(600);

                CompTurn();

                System.Threading.Thread.Sleep(600);

                team[i].energy += 2;

                System.Threading.Thread.Sleep(750);
                lastDamage = 0;
                team[i].energy += 2;
                worker.ReportProgress(0);


            }
        }
        public void Start()
        {
            //set labels and progress
            EnemyHPBar.Value = opponents[j].Health;
            YourHPBar.Value = team[i].Health;
            EnemyHPBar.Maximum = opponents[j].Health;
            YourHPBar.Maximum = team[i].Health;
            YourHP.Content = YourHPBar.Value;
            EnemyHP.Content = EnemyHPBar.Value;
            EnergyLabel.Content = team[i].energy;
            EnergyBar.Maximum = team[i].energy;
            EnergyBar.Minimum = 0;
            EnergyBar.Value = team[i].energy;

            //set sprites and names
            YouSprite.Source = new BitmapImage(new Uri("water.png", UriKind.Relative));
            EnemySprite.Source = new BitmapImage(new Uri("leaf.png", UriKind.Relative));
            Potion1.Source = new BitmapImage(new Uri("potion.png", UriKind.Relative));
            Potion2.Source = new BitmapImage(new Uri("potion.png", UriKind.Relative));

            YourName.Content = "Water Sprite";
            EnemyName.Content = "Leaf Sprite";

            Attack1.Content = "Whirlpool: 6 Energy";
            Attack2.Content = "Soak: 3 Energy";
            Attack3.Content = "Splash: 1 Energy";
            Attack4.Content = "Bubble: 0 Energy";


            //start battle
            worker.RunWorkerAsync();

        }
        private void Attack1_Click(object sender, RoutedEventArgs e)
        {
            choice = 1;
            opponents[j].Hurt(team[i].Attack / 5);
            if (opponents[j].Health < 0)
            { opponents[j].Health = 0; }
            team[i].energy -= 6;
            lastDamage = (team[i].Attack / 5);
        }
        private void Attack2_Click(object sender, RoutedEventArgs e)
        {
            opponents[j].Hurt(team[i].Attack / 6);
            if (opponents[j].Health < 0)
            { opponents[j].Health = 0; }
            team[i].energy -= 3;
            choice = 2;
            lastDamage = (team[i].Attack / 6);
        }
        private void Attack3_Click(object sender, RoutedEventArgs e)
        {
            opponents[j].Hurt(team[i].Attack / 7);
            if (opponents[j].Health < 0)
            { opponents[j].Health = 0; }
            team[i].energy -= 1;
            choice = 3;
            lastDamage = (team[i].Attack / 7);
        }
        private void Attack4_Click(object sender, RoutedEventArgs e)
        {
            opponents[j].Hurt(team[i].Attack / 10);
            if (opponents[j].Health < 0)
            { opponents[j].Health = 0; }
            choice = 4;
            lastDamage = (team[i].Attack / 10);
        }
        private void Heal_Click(object sender, RoutedEventArgs e)
        {
            team[i].Heal(25);
            choice = 5;
            healTime++;

            if (healTime == 2)
            { Potion2.Source = null; }
            if (healTime == 3)
            { Potion1.Source = null; }

            team[i].energy -= 2;
            lastDamage = 0;
        }
        public void CompTurn()
        {
            if (opponents[j].Energy >= 6)
            {
                team[i].Health -= (opponents[j].Attack) / 5;
                opponents[j].Energy -= 4;
                lastDamage = opponents[j].Attack / 5;
            }
            else if (opponents[j].Energy >= 3)
            {
                team[i].Health -= (opponents[j].Attack) / 6;
                opponents[j].Energy -= 1;
                lastDamage = opponents[j].Attack / 6;
            }
            else if (opponents[j].Energy >= 1)
            {
                team[i].Health -= (opponents[j].Attack) / 7;
                opponents[j].Energy++;
                lastDamage = opponents[j].Attack / 7;
            }
            else
            {
                team[i].Health -= (opponents[j].Attack) / 10;
                opponents[j].Energy += 2;
                lastDamage = opponents[j].Attack / 10;
            }
            if (team[i].Health < 0)
            { team[i].Health = 0; }
        }

private void FleeButton_Click(object sender, RoutedEventArgs e)
        {
            Flee = true;
            Window4 fled = new Window4();
            fled.Show();
            Close();
        }
    }
}

