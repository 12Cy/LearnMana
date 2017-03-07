using LearnMonoGame;
using LearnMonoGame.Components;
using LearnMonoGame.Manager;
using LearnMonoGame.Summoneds;
using LearnMonoGame.Tools;
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
            Kill,
            Debug
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
            if (!BoolClass.InGameLevel)
                return;
            HandleElement();

            switch (curr)
            {
                case EElement.Spawn:
                    UpdateSpawn();
                    break;
                case EElement.Kill:
                    UpdateKill();
                    break;
                case EElement.Debug:
                    UpdateDebug();
                    break;
                default:
                    break;
            }

        }

        #region Update


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

        private void UpdateDebug()
        {
            Action act = () =>
            {
                txbMouse.Text = "Mouse: \t\t\t\t" +  xIn.StrMousePosition();
                txbCamera.Text = "Camera: \t\t\t\t" + _MapStuff.Instance.camera.StrBounds();
                txbBool.Text = BoolClass.StrBool();
            };

            txbMouse.Dispatcher.Invoke(act);
        }

        #endregion

        private void HandleElement()
        {
            Action act;
            if (curr != prev)
            {
                CollapsedAll();
                switch (curr)
                {
                    case EElement.Spawn:
                        act = () =>
                        {
                            stpList.Visibility = Visibility.Visible;
                            stpSpawn.Visibility = Visibility.Visible;
                            
                        };

                        stpList.Dispatcher.Invoke(act);
                        break;
                    case EElement.Debug:
                        act = () =>
                        {
                            stpDebug.Visibility = Visibility.Visible;

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

        private void btnDebug_Click(object sender, RoutedEventArgs e)
        {
            curr = EElement.Debug;
        }

        private void CollapsedAll()
        {
            Action act = () =>
            {
                stpList.Visibility = Visibility.Collapsed;
                stpSpawn.Visibility = Visibility.Collapsed;
                stpDebug.Visibility = Visibility.Collapsed;
            };

            stpList.Dispatcher.Invoke(act);            
        }
    }
}
