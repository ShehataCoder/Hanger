using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;
using System.Windows.Forms;
using System.Windows.Controls;


namespace test_System_2
{
    

    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }



        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-8I5RMJS;Initial Catalog=FoodDlivery;Integrated Security=True;Encrypt=false");

            if (username.Text == "" || password.Text == "")
            {
                System.Windows.MessageBox.Show("Please fill all blank fields", "Error Message");
            }
            else
            {
                try
                {
                    connect.Open();

                    String selectData = "SELECT * FROM users WHERE username = @username AND password = @password";

                    using (SqlCommand cmd = new SqlCommand(selectData, connect))
                    {
                        cmd.Parameters.AddWithValue("@username", username.Text.Trim());
                        cmd.Parameters.AddWithValue("@password", password.Text.Trim());
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable table = new DataTable();
                        adapter.Fill(table);

                        if (table.Rows.Count >= 1)
                        {
                            System.Windows.MessageBox.Show("Login Successfully!", "Information Message");

                            Window2 mForm = new Window2();
                            mForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            System.Windows.MessageBox.Show("Incorrect Username/Password", "Error Message");

                        }
                    }

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error connecting Database: " + ex, "Error Message");

                }
                finally
                {
                    connect.Close();
                }


            }
        }

    }
}
