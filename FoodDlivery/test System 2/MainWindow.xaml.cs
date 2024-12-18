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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            loadGrid();
            
        }

        SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-8I5RMJS;Initial Catalog=FoodDlivery;Integrated Security=True;Encrypt=false");

        private void DataGrid_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        public void loadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * From dlivery " , conn);
            DataTable dt = new DataTable();
            conn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            conn.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }

        public void cleardata()
        {
            Name_txt.Clear();
            Id_int.Clear();
            Address_txt.Clear();
            salary.Clear();
            Searc_id.Clear();
        }
        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            cleardata();
        }

        public bool isValid()
        {
            if(Name_txt.Text==string.Empty || Id_int.Text == string.Empty || salary.Text==string.Empty || Address_txt.Text==string.Empty )
            {
                MessageBox.Show("Name is Requred , Faild");
                return false;
            }
            return true;
        }



        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
             try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO dlivery VALUES (@Name,@Id,@Address,@Salary)", conn);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Name", Name_txt.Text);
                    cmd.Parameters.AddWithValue("@Id", Id_int.Text);
                    cmd.Parameters.AddWithValue("@Address", Address_txt.Text);
                    cmd.Parameters.AddWithValue("@Salary", salary.Text);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    loadGrid();
                    MessageBox.Show("Successful register ", "Save", MessageBoxButton.OK);
                    cleardata();

                }
            }
            catch(SqlException ex )
            {
                MessageBox.Show(ex.Message);
            }
            finally 
            {
                conn.Close(); 
            }
        }

        private void DeletBtn_Click(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM dlivery WHERE id = "+Searc_id.Text+" ",conn);
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
                conn.Close() ;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window2 mForm = new Window2();
            mForm.Show();
            this.Hide();
        }
    }
}
