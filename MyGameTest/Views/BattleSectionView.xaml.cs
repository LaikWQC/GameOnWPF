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

namespace MyGameTest.Views
{
    /// <summary>
    /// Логика взаимодействия для BattleSectionView.xaml
    /// </summary>
    public partial class BattleSectionView : UserControl
    {
        public BattleSectionView()
        {
            InitializeComponent();
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                EnemiesTable.Unselect();
                HeroesTable.Unselect();
            }
        }
    }
}
