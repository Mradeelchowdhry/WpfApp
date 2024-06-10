using Microsoft.Data.SqlClient;
using System.Data;
using System.Windows;

namespace ADEEL_WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection con = new SqlConnection("Data Source=.;Initial Catalog=adeel;User ID=sa;Password=aptech;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");

        public MainWindow()
        {
            InitializeComponent();
            loadData();
        }

        private void loadData()
        {
            SqlCommand getData = new SqlCommand("Select * from students", con);
            DataTable dt = new DataTable();
            con.Open();

            SqlDataReader dataReader = getData.ExecuteReader();
            dt.Load(dataReader);
            studentGrid.ItemsSource = dt.DefaultView;
            con.Close();

        }

        private void ClearData()
        {
            uname.Clear();
            email.Clear();
            age.Clear();
            cellno.Clear();
            city.Clear();
            sid.Clear();
        }

        private void ClearBtn_Click(object sender, RoutedEventArgs e)
        {
            ClearData();
        }

        private bool IsValid()
        {
            if (uname.Text == string.Empty)
            {
                MessageBox.Show("Name connot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (email.Text == string.Empty)
            {
                MessageBox.Show("Email connot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (age.Text == string.Empty)
            {
                MessageBox.Show("Age connot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (cellno.Text == string.Empty)
            {
                MessageBox.Show("Cell No connot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (city.Text == string.Empty)
            {
                MessageBox.Show("City connot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            else
            {
                return true;
            }

        }

        private void AddStudent(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                SqlCommand addStud = new SqlCommand("Insert into students values (@fname,@email,@age,@cell,@city)", con);
                con.Open();
                addStud.CommandType = CommandType.Text;

                addStud.Parameters.AddWithValue("@fname", uname.Text);
                addStud.Parameters.AddWithValue("@email", email.Text);
                addStud.Parameters.AddWithValue("@age", age.Text);
                addStud.Parameters.AddWithValue("@cell", cellno.Text);
                addStud.Parameters.AddWithValue("@city", city.Text);

                addStud.ExecuteNonQuery();
                con.Close();
                loadData();
                ClearData();
            }
        }

        private void dltStudent(object sender, RoutedEventArgs e)
        {
            if (sid.Text != string.Empty)
            {
                SqlCommand deleteStd = new SqlCommand("Delete From students Where Id = @sid", con);
                con.Open();
                deleteStd.CommandType = CommandType.Text;
                deleteStd.Parameters.AddWithValue("@sid", sid.Text);
                deleteStd.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Students Deleted Succesfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                loadData();
                ClearData();
            }

            else
            {
                MessageBox.Show("Students Id Is Required To Delete A Record", "Can't Delete Students", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void GetStudDetails(object sender, RoutedEventArgs e)
        {
            if (sid.Text != string.Empty)
            {
                SqlCommand getStud = new SqlCommand("SELECT * FROM students WHERE id = @sid", con);
                con.Open();
                getStud.CommandType = CommandType.Text;
                getStud.Parameters.AddWithValue("@sid", sid.Text);
                SqlDataReader reader = getStud.ExecuteReader();

                if (reader.Read())
                {
                    uname.Text = reader["fullname"].ToString();
                    age.Text = reader["age"].ToString();
                    email.Text = reader["email"].ToString();
                    cellno.Text = reader["cellno"].ToString();
                    city.Text = reader["city"].ToString();

                }
                else
                {
                    MessageBox.Show("Invalid Id", "Invalid Id", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                con.Close();
            }
            else
            {
                MessageBox.Show("Student Id Is Requird To Update A Record", "Can't Update Student", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void UpdateStudent(object sender, RoutedEventArgs e)
        {
            if (IsValid())
            {
                SqlCommand UpdateStudentDet = new SqlCommand("Update students Set fullname=@fname, email=@email, age=@age, cellno=@cell, city=@city WHERE id = @sid", con);
                con.Open();
                UpdateStudentDet.CommandType = CommandType.Text;

                UpdateStudentDet.Parameters.AddWithValue("@fname", uname.Text);
                UpdateStudentDet.Parameters.AddWithValue("@email", email.Text);
                UpdateStudentDet.Parameters.AddWithValue("@age", age.Text);
                UpdateStudentDet.Parameters.AddWithValue("@cell", cellno.Text);
                UpdateStudentDet.Parameters.AddWithValue("@city", city.Text);
                UpdateStudentDet.Parameters.AddWithValue("@sid", sid.Text);

                UpdateStudentDet.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("Student Updated Successfully", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                loadData();
                ClearData();
            }
        }

    }
}

