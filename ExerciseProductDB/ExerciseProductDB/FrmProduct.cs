using ExerciseProductDB.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExerciseProductDB
{
    public partial class FrmProduct : Form
    {
        public FrmProduct()
        {
            InitializeComponent();
        }

        private void FrmProduct_Load(object sender, EventArgs e)
        {
            DataGridViewCheckBoxColumn select = new DataGridViewCheckBoxColumn();
            select.Name = "selectColumn";
            select.HeaderText = "Lua chon";
            //select.ValueType = typeof(bool);
            dgvMain.Columns.Add(select);

            dgvMain.DataSource = ProductDAO.getListProduct();

            DataGridViewButtonColumn update = new DataGridViewButtonColumn();
            dgvMain.Columns.Add(update);
            update.HeaderText = "Update";
            update.Text = "Update";
            update.Name = "btnupdate";
            update.UseColumnTextForButtonValue = true;
            DataGridViewButtonColumn delete = new DataGridViewButtonColumn();
            dgvMain.Columns.Add(delete);
            delete.HeaderText = "Delete";
            delete.Text = "Delete";
            delete.Name = "btndelete";
            delete.UseColumnTextForButtonValue = true;

            //btnDel.Enabled = false;
        }

        private void dgvMain_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvMain_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvMain.CurrentRow.Index;
            if (dgvMain.Rows[i].Cells[6].Selected)
            {
                ProductDAO.Delete(dgvMain.Rows[i].Cells[1].Value.ToString());
                dgvMain.DataSource = ProductDAO.getListProduct();
            }
            if (dgvMain.Rows[i].Cells[5].Selected)
            {
                
                FrmUpdate up = new FrmUpdate(dgvMain.Rows[i].Cells[1].Value.ToString());
                this.Hide();
                up.ShowDialog();
                this.Close();
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           dgvMain.DataSource= ProductDAO.getListProductbyName(txtSearch.Text);

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            this.Hide();
            FrmAddProduct add = new FrmAddProduct();
            add.ShowDialog();
            this.Close();
        }

        private void btnDel_Click(object sender, EventArgs e)
        {
            for (int j = 0; j < dgvMain.Rows.Count; j++)
            {
                if (dgvMain.Rows[j].Cells[0].Value != null)
                {
                    
                    //btnDel.Enabled = true;
                    ProductDAO.Delete(dgvMain.Rows[j].Cells[1].Value.ToString());

                }
                
            }
            dgvMain.DataSource = ProductDAO.getListProductbyName(txtSearch.Text);
        }
    }
}
