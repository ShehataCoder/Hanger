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
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
            loadGrid();
        }



        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-8I5RMJS;Initial Catalog=FoodDlivery;Integrated Security=True;Encrypt=false");

        public void loadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * From customer ", conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            datagrid2.ItemsSource = dt.DefaultView;
        }

        public void cleardata()
        {
            Name_txt2.Clear();
            Id_int2.Clear();
            Address_txt2.Clear();
            phone.Clear();
            Searc_id2.Clear();
        }


        public bool isValid()
        {
            if (Name_txt2.Text == string.Empty || Id_int2.Text == string.Empty || phone.Text == string.Empty || Address_txt2.Text == string.Empty)
            {
                MessageBox.Show("Name is Requred , Faild");
                return false;
            }
            return true;
        }

        private void AddBtn_Click2(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO customer VALUES (@Name,@ID,@Address,@Salary)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", Name_txt2.Text);
                    cmd.Parameters.AddWithValue("@ID", Id_int2.Text);
                    cmd.Parameters.AddWithValue("@Address", Address_txt2.Text);
                    cmd.Parameters.AddWithValue("@Salary", phone.Text);
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
            finally 
            {
                conn.Close();
            }
        }

        private void ClearBtn_Click2(object sender, RoutedEventArgs e)
        {
            cleardata();
        }

        private void DeletBtn_Click2(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM customer WHERE ID = " + Searc_id2.Text + " ", conn);
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

        private void DataGrid_SelectionChanged_2(object sender, SelectionChangedEventArgs e)
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
