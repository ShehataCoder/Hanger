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
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;

namespace test_System_2
{
    /// <summary>
    /// Interaction logic for Window3.xaml
    /// </summary>
    public partial class Window4 : Window
    {
        public Window4()
        {
            InitializeComponent();
            loadGrid();
        }



        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-8I5RMJS;Initial Catalog=FoodDlivery;Integrated Security=True;Encrypt=false");

        public void loadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * From Orders ", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            datagrid3.ItemsSource = dt.DefaultView;
        }

        public void cleardata()
        {
            
            Id_order.Clear();
            Id_Dlivery.Clear();
            Id_costomer.Clear();
            item.Clear();
        }


        public bool isValid()
        {
            if (Id_costomer.Text == string.Empty || Id_Dlivery.Text == string.Empty || Id_Dlivery.Text == string.Empty || item.Text == string.Empty)
            {
                MessageBox.Show("Name is Requred , Faild");
                return false;
            }
            return true;
        }

        private void AddBtn_Click3(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Orders VALUES (@id_order,@id_customer,@id_dlivery,@item)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_order", Id_order.Text);
                    cmd.Parameters.AddWithValue("@id_customer", Id_Dlivery.Text);
                    cmd.Parameters.AddWithValue("@id_dlivery", Id_costomer.Text);
                    cmd.Parameters.AddWithValue("@item", item.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    loadGrid();
                    MessageBox.Show("Successful register ", "Save", MessageBoxButton.OK);
                    cleardata();

                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearBtn_Click3(object sender, RoutedEventArgs e)
        {
            cleardata();
        }

        private void DeletBtn_Click3(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM Orders WHERE id_order = " + Searc_id3.Text + " ", conn);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Record has been deleted", "Delet", MessageBoxButton.OK);
                conn.Close();
                cleardata();
                loadGrid();
                conn.Close();
            }
            catch (SqlException ex)
            {
                MessageBox.Show("Not Deleted" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void DataGrid_SelectionChanged_3(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 mForm = new Window2();
            mForm.Show();
            this.Hide();
        }
    }
}
