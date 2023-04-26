using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace LibrarySystem
{
    public partial class Form1 : Form
    {

        private OleDbConnection conn;
        public Form1()
        {
            InitializeComponent();
            conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=C:\\Users\\QQ107\\Documents\\LibSys_Crispe1.mdb");
        }

        private void loadDatagrid()
        {
            conn.Open();

            OleDbCommand com = new OleDbCommand("Select * from book order by accesion_number asc", conn);       
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            grid1.DataSource = tab;

            conn.Close();
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            txtno.Text = grid1.Rows[e.RowIndex].Cells["accession_number"].Value.ToString();
            txttitle.Text = grid1.Rows[e.RowIndex].Cells["title"].Value.ToString();
            txtauthor.Text = grid1.Rows[e.RowIndex].Cells["Author"].Value.ToString();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            conn.Open();
            string num = txtno.Text;

            DialogResult dr = MessageBox.Show("Are you sure you want to delete this? ", "Confirm Deletion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if(dr == DialogResult.Yes)
            {
                OleDbCommand com = new OleDbCommand("Delete from book where accession_number =  '" + num + "'", conn);
                
                
                com.ExecuteNonQuery();

                MessageBox.Show("Succesfully DELETED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("CANCELLED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            conn.Close();
            loadDatagrid();
        }



        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            conn.Open();
            string no;
            no = txtno.Text;

            OleDbCommand com = new OleDbCommand("Update book SET title = '" + txttitle.Text + "', author = '" + txtauthor.Text + "' where accession_number = '" + no + "'", conn);
          
            com.ExecuteNonQuery();

            MessageBox.Show("Succesfully UPDATED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            conn.Close();
            loadDatagrid(); 
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            conn.Open();

            OleDbCommand com = new OleDbCommand("Insert into book values ('" + txtno.Text + "', '" + txttitle.Text + "', '" + txtauthor.Text + "')", conn);
            
            com.ExecuteNonQuery();

            MessageBox.Show("Succesfully SAVED!", "info", MessageBoxButtons.OK, MessageBoxIcon.Information);

            conn.Close();
            loadDatagrid();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            conn.Open();

            OleDbCommand com = new OleDbCommand("Select * from book where title like '%" + txtSearch.Text + "%'",conn);
            com.ExecuteNonQuery();

            OleDbDataAdapter adap = new OleDbDataAdapter(com);
            DataTable tab = new DataTable();

            adap.Fill(tab);
            grid1.DataSource = tab;

            conn.Close();
        }
    }
}
