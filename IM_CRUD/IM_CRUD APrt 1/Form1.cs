using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace IM_CRUD_APrt_1
{
    public partial class Form1 : Form
    {
        //Add Con
        SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=C:\Users\ACER\Desktop\Documents\Visual Studio 2013\Projects\IM_CRUD\IM_CRUD APrt 1\ServiceD.mdf;Integrated Security=True");
        SqlCommand com = new SqlCommand();

        public Form1()
        {
            InitializeComponent();
        }

        //Open Connection
        public void OpenConnetion()
        {
            if (con.State == ConnectionState.Closed)
                con.Open();
        }

        //Close Connection
        public void CloseConnetion()
        {
            if (con.State == ConnectionState.Open)
                con.Close();
        }


    //Query Execution
      public void executeQuery(string query)
        {
            try
            {
                OpenConnetion();
                com = new SqlCommand(query, con);

                if (com.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Query Executed");
                }

                else
                {
                    MessageBox.Show("Fail");
                }
            }
                catch (Exception ex)
                {
                     MessageBox.Show(ex.Message);
                }

             finally
                 {
                   CloseConnetion();
                 }
          }
      
       
      //Show Data in Gridview
      private void DisplayDatagrid()
      {
          OpenConnetion();
          com.CommandText = "SELECT * FROM  CatTable";
          com.ExecuteNonQuery();
          DataTable dt = new DataTable();
          SqlDataAdapter da = new SqlDataAdapter(com);
          da.Fill(dt);
          dataGridView1.DataSource = dt;
          CloseConnetion();
          updaterowcount();
          TotalPrice();
      }


        //Auto Display in Data Struture
        private void tableBindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.tableBindingSource.EndEdit();
            this.tableAdapterManager.UpdateAll(this.serviceDDataSet);

        }

        //for Datasource Gridview
        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'serviceDDataSet2.CatTable' table. You can move, or remove it, as needed.
            this.catTableTableAdapter1.Fill(this.serviceDDataSet2.CatTable);
            com.Connection = con;
            DisplayDatagrid();

        }


        //Total Compute inside datagridview
        private void TotalPrice()
        {
             int sum = 0;
             for (int i = 0; i < dataGridView1.Rows.Count; ++i)
             {
                sum += Convert.ToInt32(dataGridView1.Rows[i].Cells[4].Value);
             }
             totprice.Text = sum.ToString();
        }


        //Total Row COunt
        private void updaterowcount()
        {
            int numRows = dataGridView1.Rows.Count;

            label9.Text = numRows.ToString();

        }


        //Close Form
        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        //Clear Texboxt
        private void button4_Click(object sender, EventArgs e)
        {
            catIDTextBox.Clear();
            catNameTextBox.Clear();
            ageTextBox.Clear();
            breedTextBox.Clear();
            priceTextBox.Clear();
        }


        //Insert Record
        private void button1_Click(object sender, EventArgs e)
        {
            string insertQuery = " INSERT INTO CatTable VALUES ('" + catIDTextBox.Text + "', '" + catNameTextBox.Text + "', '" + ageTextBox.Text + "', '" + breedTextBox.Text + "', '" + priceTextBox.Text + "' )";
            executeQuery(insertQuery);
            DisplayDatagrid();
        }

         //Cell Content Click
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
           textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
           textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
           textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
           textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();

        }
        
        //Update
        private void button2_Click(object sender, EventArgs e)
        {
            string updateQuery = " UPDATE CatTable set CatName='" + catNameTextBox.Text + "', Age='" + ageTextBox.Text + "', Breed='" + breedTextBox.Text + "',Price='" + priceTextBox.Text + "' WHERE CatID='" + catIDTextBox.Text + "'";
            executeQuery(updateQuery);
            DisplayDatagrid();
        }

        //Delete
        private void button3_Click(object sender, EventArgs e)
        {
            string deleteQuery = " DELETE from CatTable WHERE CatID='" + catIDTextBox.Text + "'";
            executeQuery(deleteQuery);
            DisplayDatagrid();
        }
    }
}
