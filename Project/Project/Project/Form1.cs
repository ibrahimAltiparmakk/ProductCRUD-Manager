using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;


namespace yazgem
{
    public partial class Form1 : Form
    {
        
        public string connectionString = "Server=DESKTOP-IGV354K\\SQLEXPRESS;Database=Yazgem;Integrated Security=True;";

        public Form1()
        {
            InitializeComponent();
            
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            DataGrid();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            

            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];
                string id = selectedRow.Cells["id"].Value.ToString(); 

                if (MessageBox.Show("Are you sure you want to delete this product?", "Delete Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    
                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM Products WHERE Id = @Id", con);
                        cmd.Parameters.AddWithValue("@Id", id);

                        int result = cmd.ExecuteNonQuery();
                        if (result > 0)
                        {
                            MessageBox.Show("Product deleted successfully.");
                            dataGridView1.Rows.RemoveAt(selectedRow.Index); // Datagrid'den satırı kaldır
                        }//if
                        else
                            MessageBox.Show("Product not found or could not be deleted.");                   
                    }//using
                }//if
            }//if
            else
                MessageBox.Show("Please select a product to delete.");
            
        }//void



        private void DataGrid()
        {
            
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();
                    string query = "SELECT * FROM Products"; 
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);// dt ye doldur
                            dataGridView1.DataSource = dt;
                        }//using
                    }//using
                }//try
                catch (Exception ex)
                {
                    MessageBox.Show("Database error: " + ex.Message);
                }//catch
            }//using
        }//void

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                Product objProductNew = new Product
                {
                    Id = selectedRow.Cells["Id"].Value.ToString(),
                    Name = selectedRow.Cells["Name"].Value.ToString(),
                    Description = selectedRow.Cells["Description"].Value.ToString(),
                    Price = selectedRow.Cells["Price1"].Value.ToString(),
                    Price2 = selectedRow.Cells["Price2"].Value.ToString()
                };//product

                Form2 form2 = new Form2(objProductNew);
                if (form2.ShowDialog() == DialogResult.OK)
                {
                    SaveProductToDatabase(form2.ProductData);

                    selectedRow.Cells["Name"].Value = form2.ProductData.Name;
                    selectedRow.Cells["Description"].Value = form2.ProductData.Description;
                    selectedRow.Cells["Price1"].Value = form2.ProductData.Price;
                    selectedRow.Cells["Price2"].Value = form2.ProductData.Price2;
                }//if
            }//if
        }//void



        private void btnInsert_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            if (form.ShowDialog() == DialogResult.OK)
            {
                int newId = SaveProductToDatabase(form.ProductData);
                if (newId > 0)
                {
                    DataTable dt = (DataTable)dataGridView1.DataSource;
                    DataRow newRow = dt.NewRow();
                    newRow["Id"] = newId;
                    newRow["Name"] = form.ProductData.Name;
                    newRow["Description"] = form.ProductData.Description;
                    newRow["Price1"] = form.ProductData.Price;
                    newRow["Price2"] = form.ProductData.Price2;
                    dt.Rows.Add(newRow);
                }//if
            }//if
        }//void


        private int SaveProductToDatabase(Product objProduct)
        {
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                SqlCommand cmd;

                if (String.IsNullOrEmpty(objProduct.Id))
                    cmd = new SqlCommand("INSERT INTO Products (Name, Description, Price1, Price2) VALUES (@Name, @Description, @Price1, @Price2); SELECT SCOPE_IDENTITY();", con);
                

                else
                {
                    cmd = new SqlCommand("UPDATE Products SET Name=@Name, Description=@Description, Price1=@Price1, Price2=@Price2 WHERE Id=@Id", con);
                    cmd.Parameters.AddWithValue("@Id", objProduct.Id);
                }//else

                
                cmd.Parameters.AddWithValue("@Name", objProduct.Name);
                cmd.Parameters.AddWithValue("@Description", objProduct.Description);
                cmd.Parameters.AddWithValue("@Price1", objProduct.Price);
                cmd.Parameters.AddWithValue("@Price2", objProduct.Price2);

                if (String.IsNullOrEmpty(objProduct.Id))
                {
                    var newId = cmd.ExecuteScalar();//ilk satır ilk sutun
                    return Convert.ToInt32(newId); 
                }//if
                else
                {
                    cmd.ExecuteNonQuery();
                    return Convert.ToInt32(objProduct.Id);  
                }//else
            }//using
        }//int

    }//class


}//namespace
