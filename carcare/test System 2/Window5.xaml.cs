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
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace test_System_2
{
    /// <summary>
    /// Interaction logic for Window5.xaml
    /// </summary>
    public partial class Window5 : Window
    {
        public Window5()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1();
            window1.Show();
            this.Hide();

        }

        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (t1.Text == "" || t2.Text == "" || t3.Text == "" || t2.Text != t3.Text)
            {
                System.Windows.MessageBox.Show("Please fill all blank fields or Check password and re password ", "Error Message");
            }
            else
            {
                FoodDliveryEntities foodDliveryEntities = new FoodDliveryEntities();
                user ut = new user();
                ut.username = t1.Text;
                ut.password = t2.Text;
                foodDliveryEntities.users.Add(ut);
                foodDliveryEntities.SaveChanges();
                t1.Clear();
                t2.Clear();
                t3.Clear();

            }

        }
    }
}
