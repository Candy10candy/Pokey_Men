using Pokey_Men.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        private readonly BackgroundWorker worker = new BackgroundWorker();

        public Fighter you;

        public string type = "";
        public int choice = 0;
        public int healTime = 1;
        public bool Flee = false;

        public Window1()
        {
            InitializeComponent();
            worker.DoWork += worker_DoWork;
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
            worker.WorkerReportsProgress = true;
            worker.ProgressChanged += worker_ProgressChanged;
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //display health bars
            EnemyHPBar.Value = you.opponent.Health;
            YourHPBar.Value = you.Health;
            YourHP.Content = YourHPBar.Value;
            EnemyHP.Content = EnemyHPBar.Value;
            EnergyLabel.Content = you.energy;
            EnergyBar.Value = you.energy;

            Attack1.IsEnabled = you.energy >= 6;
            Attack2.IsEnabled = you.energy >= 3;
            Attack3.IsEnabled = you.energy >= 1;
            Attack4.IsEnabled = you.energy >= 0;

            if (you.Health <= 0 && you.opponent.Health <= 0)
            {
                Window2 win = new Window2();
                win.Show();
                Close();
            }
            else if (you.Health <= 0)
            {
                Window3 lose = new Window3();
                lose.Show();
                Close();
            }
            else if (you.opponent.Health <= 0)
            {
                Window2 win = new Window2();
                win.Show();
                Close();
            }
            else
            {
                EnemyHPBar.Value = you.opponent.Health;
                YourHPBar.Value = you.Health;
            }

        }
        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            

            while (you.Health > 0 && you.opponent.Health > 0 && !Flee)
            {
                choice = 0;

                while (choice == 0)
                {
                  
                }

                worker.ReportProgress(0);

                you.energy += 2;
                you.CompTurn();
                
            }
            
        }

        private void worker_RunWorkerCompleted(object sender,
                                                   RunWorkerCompletedEventArgs e)
        {
            //display health bars
            EnemyHPBar.Value = you.opponent.Health;
            YourHPBar.Value = you.Health;
            YourHP.Content = YourHPBar.Value;
            EnemyHP.Content = EnemyHPBar.Value;

        }
        public void makeDamp()
        {

            you = new Fighter(52, 183);
            type = "damp";
            EnemyHPBar.Value = you.opponent.Health;
            YourHPBar.Value = you.Health;
            EnemyHPBar.Maximum = you.opponent.Health;
            YourHPBar.Maximum = you.Health;
            YourHP.Content = YourHPBar.Value;
            EnemyHP.Content = EnemyHPBar.Value;
            EnergyLabel.Content = you.energy;
            EnergyBar.Maximum = you.energy;
            EnergyBar.Minimum = 0;
            EnergyBar.Value = you.energy;


            YouSprite.Source = new BitmapImage(new Uri("water.png", UriKind.Relative));
            EnemySprite.Source = new BitmapImage(new Uri("leaf.png", UriKind.Relative));


            YourName.Content = "Ocean Man";
            EnemyName.Content = "Literally Just A Leaf";


            worker.RunWorkerAsync();

        }

        public void makeBoom()
        {

            you = new Fighter(61, 156);
            type = "boom";
            EnemyHPBar.Value = you.opponent.Health;
            YourHPBar.Value = you.Health;
            EnemyHPBar.Maximum = you.opponent.Health;
            YourHPBar.Maximum = you.Health;
            YourHP.Content = YourHPBar.Value;
            EnemyHP.Content = EnemyHPBar.Value;
            EnergyLabel.Content = you.energy;
            EnergyBar.Maximum = you.energy;
            EnergyBar.Minimum = 0;
            EnergyBar.Value = you.energy;

            YourName.Content = "Sparky Sparky Boom Man";
            EnemyName.Content = "Ocean Man";

            YouSprite.Source = new BitmapImage(new Uri("fire.png", UriKind.Relative));
            EnemySprite.Source = new BitmapImage(new Uri("water.png", UriKind.Relative));

            worker.RunWorkerAsync();

        }

        public void makeLeaf()
        {

            you = new Fighter(55, 165);
            type = "leaf";
            EnemyHPBar.Value = you.opponent.Health;
            YourHPBar.Value = you.Health;
            EnemyHPBar.Maximum = you.opponent.Health;
            EnemyHPBar.Minimum = 0;
            YourHPBar.Maximum = you.Health;
            YourHPBar.Minimum = 0;
            YourHP.Content = YourHPBar.Value;
            EnemyHP.Content = EnemyHPBar.Value;
            EnergyLabel.Content = you.energy;
            EnergyBar.Maximum = you.energy;
            EnergyBar.Minimum = 0;
            EnergyBar.Value = you.energy;

            YouSprite.Source = new BitmapImage(new Uri("leaf.png", UriKind.Relative));
            EnemySprite.Source = new BitmapImage(new Uri("fire.png", UriKind.Relative));

            YourName.Content = "Literally Just A Leaf";
            EnemyName.Content = "Sparky Sparky Boom Man";


            worker.RunWorkerAsync();

        }

        private void Attack1_Click(object sender, RoutedEventArgs e)
        {
            choice = 1;
            you.a1();
        }

        private void Attack2_Click(object sender, RoutedEventArgs e)
        {
            you.a2();
            choice = 2;
        }

        private void Attack3_Click(object sender, RoutedEventArgs e)
        {
            you.a3();
            choice = 3;
        }

        private void Attack4_Click(object sender, RoutedEventArgs e)
        {
            you.a4();
            choice = 4;
        }

        private void Heal_Click(object sender, RoutedEventArgs e)
        {
            
            you.Heal(25);
            choice = 5;
            healTime++;
            if (healTime > 2)
            {
                Heal.IsEnabled = false;
            }
            you.energy -= 2;
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

