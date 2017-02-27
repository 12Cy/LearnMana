using LearnMonoGame;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LearnMonoGame.Summoneds;

namespace DebugConsole
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        enum EElement
        {
            Spawn,
            Kill
        }

        bool activated = false;

        EElement curr = EElement.Spawn, prev = EElement.Spawn;

        public MainWindow()
        {
            InitializeComponent();

            Program.Main();

        }

        private void UpdateDebugConsole()
        {

            HandleElement();

            switch (curr)
            {
                case EElement.Spawn:
                    UpdateSpawn();
                    break;
                case EElement.Kill:
                    UpdateKill();
                    break;
                default:
                    break;
            }

        }

        private void UpdateList()
        {
            string str = (cmbList.SelectedItem as TextBlock).Text;

            Console.WriteLine(str);

            txbList.Text = MonsterManager.Instance.GetList(str);
        }

        private void UpdateSpawn()
        {
            UpdateList();
        }

        private void UpdateKill()
        {
            UpdateList();
        }

        private void HandleElement()
        {
            if (curr != prev)
            {
                CollapsedAll();
                switch (curr)
                {
                    case EElement.Spawn:
                        stpList.Visibility = Visibility.Visible;
                        stpSpawn.Visibility = Visibility.Visible;
                        break;
                    default:
                        break;
                }

                prev = curr;

            }
        }

        private void btnSpawn_Click(object sender, RoutedEventArgs e)
        {
            curr = EElement.Spawn;
        }

        private void btnKill_Click(object sender, RoutedEventArgs e)
        {
            curr = EElement.Kill;
        }

        private void btnActivate_Click(object sender, RoutedEventArgs e)
        {
            activated = !activated;
            if (activated)
                btnActivate.Background = new SolidColorBrush(Colors.Green);
            else
                btnActivate.Background = new SolidColorBrush(Colors.Red);
        }

        private void CollapsedAll()
        {
            stpList.Visibility = Visibility.Collapsed;
            stpSpawn.Visibility = Visibility.Collapsed;
        }
    }


}
