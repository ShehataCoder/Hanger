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
using System.Data.Entity.Core.Metadata.Edm;

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
        string pay;
        string tea, bur,fr,ic,co,order;
        int total=0;

        public void loadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * From ord ", conn);
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
            
        }


        public bool isValid()
        {
            if (Id_costomer.Text == string.Empty || Id_Dlivery.Text == string.Empty || Id_Dlivery.Text == string.Empty )
            {
                MessageBox.Show("this items are Requred , Faild");
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
                    if(cach.IsChecked == true)
                    {
                         pay = "Cach";
                    }
                    else if(Wallet.IsChecked == true)
                    {
                        pay = "Wallet";
                    }
                    else if (visa.IsChecked == true)
                    {
                        pay = "Visa";
                    }

                    if(coco.IsChecked == true)
                    {
                        bur = "Burger";
                        total +=70;
                    }
                    else
                    {
                        bur = "";
                    }
                    if(teas.IsChecked == true) 
                    {
                        tea = "Tea";
                        total +=8;
                    }
                    else
                    {
                        tea = "";
                    }
                    if (ice.IsChecked == true)
                    {
                        ic = "iceCream";
                        total +=20;
                    }
                    else
                    {
                        ic = "";
                    }
                    if (fries.IsChecked == true)
                    {
                        fr = "Frech Fries";
                        total +=12;
                    }
                    else
                    {
                        fr = "";
                    }
                    if (cola.IsChecked == true)
                    {
                        co = "Cola";
                        total +=10;
                    }
                    else
                    {
                        co = "";
                    }
                    order = co +" "+tea+" "+ic+" "+bur+" "+fr;
                    string x = total + " $";
                    string query = "INSERT INTO ord (Ncustomer,Ndlivery,IDdliery,item,Payment,Total) VALUES ( '" + Id_order.Text + "' ,'" + Id_costomer.Text + "' , '" + Id_Dlivery.Text +  "','" + order + "' , '" + pay + "' , '" + x + "' )";
                    SqlCommand cmd = new SqlCommand( query , conn);
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

        private void ClearBtn_Click3(object sender, RoutedEventArgs e)
        {
            cleardata();
        }

        private void DeletBtn_Click3(object sender, RoutedEventArgs e)
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("DELETE FROM ord WHERE IDdliery = " + Searc_id3.Text + " ", conn);
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
            Window1 mForm = new Window1();
            mForm.Show();
            this.Hide();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
