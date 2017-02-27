using LearnMonoGame;
using LearnMonoGame.Components;
using LearnMonoGame.Summoneds;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using static LearnMonoGame.Events;

namespace Debug
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





        Task game;

        bool activated = false;

        EElement curr = EElement.Spawn, prev = EElement.Spawn;

        public MainWindow()
        {
            InitializeComponent();

            SetEvent(UpdateDebugConsole);

            game = Task.Factory.StartNew(() => { Program.Main(); });

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
            string str = "";
            Action strAct = () =>
            {
                str = (cmbList.SelectedItem as TextBlock).Text;
                Action txtAct = () => txbList.Text = MonsterManager.Instance.GetList(str);
                txbList.Dispatcher.Invoke(txtAct);
            };
            cmbList.Dispatcher.Invoke(strAct);

        }

        private void UpdateSpawn()
        {
            UpdateList();

            if (activated)
            {
                string str = "";
                Action strAct = () =>
                {
                    str = (cmbSpawn.SelectedItem as TextBlock).Text;
                    MonsterManager.Instance.SpawnCharacterAtMousePosition(str);
                };
                cmbSpawn.Dispatcher.Invoke(strAct);
            }

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
                        Action act = () =>
                        {
                            stpList.Visibility = Visibility.Visible;
                            stpSpawn.Visibility = Visibility.Visible;
                        };

                        stpList.Dispatcher.Invoke(act);
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
            Action act = () =>
            {
                stpList.Visibility = Visibility.Collapsed;
                stpSpawn.Visibility = Visibility.Collapsed;
            };

            stpList.Dispatcher.Invoke(act);

            Action act2 = () =>
            {
                stpSpawn.Visibility = Visibility.Collapsed;
            };
            
        }
    }
}
