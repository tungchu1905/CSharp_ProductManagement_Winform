using ProductManagements.DAO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
namespace ProductManagements
{
    public partial class Form1 : Form
    {
      
        private bool status = false;
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            // bo sung cot vao datagridview

            DataGridViewCheckBoxColumn select = new DataGridViewCheckBoxColumn();

            select.Name = "selectColumn";
            select.HeaderText = "Lua chon";
            select.ValueType = typeof(bool);
            dgvCategory.Columns.Add(select);

            RefreshDgvCategory();
                // disable cac thuoc tinh
            txtCatId.Enabled = false;
            txtCatName.Enabled = false;
            txtDescription.Enabled = false;
            btnDelete.Enabled = false;
            btnSave.Enabled = false;
            status = false;
        }

        private void RefreshDgvCategory()
        {
            dgvCategory.DataSource = null;
            dgvCategory.DataSource = Category.getCategories();
            dgvCategory.Columns[1].HeaderText = "Ma Danh Muc";
            dgvCategory.Columns[2].HeaderText = "Ten Danh Muc";
            dgvCategory.Columns[3].HeaderText = "Mo Ta Chi Tiet";
        }
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            status = true;
            txtCatId.Enabled = true;
            txtCatId.Text = "";
            txtCatId.Focus();
            txtCatName.Enabled = true;
            txtCatName.Text = "";
            txtDescription.Enabled = true;
            txtDescription.Text = "";
            btnSave.Enabled = true;
        }
        //private bool valid()
        //{
        //    if (!Regex.IsMatch(txtCatId.Text.Trim(), (@"^(C)\d{4}$"))) {
        //        MessageBox.Show("ID sai format", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        
        //    }
        //    if (txtCatName.Text.Trim() == "")
        //    {
        //        MessageBox.Show("Ten khong duoc de trong", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return false;
        //    }
        //    return true;
        //}
        private void btnSave_Click(object sender, EventArgs e)
        {
            string catid = txtCatId.Text.Trim();
            string catname = txtCatName.Text.Trim();
            string catdes = txtDescription.Text.Trim();
            // to chuc du lieu kieu ArrayList<>;
            ArrayList arrayList = new ArrayList() {catid,catname,catdes };


            Regex regex = new Regex(@"^(C)\d{4}$");
            if (!regex.IsMatch(catid))
            {
                MessageBox.Show("ID sai format", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //check them id co ton tai hay khong
            else if (catname == "")
            {
                MessageBox.Show("Ten khong duoc de trong", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (status == true)
                {
                    if(Category.addCategory(arrayList) > 0)
                    {
                        // them moi
                        MessageBox.Show("THem moi thanh cong", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                     }
                }
                else
                {
                   if (Category.updateCategory(arrayList) > 0)
                    {
                        // update
                        MessageBox.Show("Update thanh cong", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                RefreshDgvCategory();

            }
        }

        private void dgvCategory_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int i;
            i = dgvCategory.CurrentCell.RowIndex;
            txtCatId.Text = dgvCategory.Rows[i].Cells[1].Value.ToString();
            txtCatName.Text = dgvCategory.Rows[i].Cells[2].Value.ToString();
            txtDescription.Text = dgvCategory.Rows[i].Cells[3].Value.ToString();
            txtCatId.Enabled = false;
            txtCatName.Enabled = true;
            txtDescription.Enabled = true;
            btnDelete.Enabled = true;
            btnSave.Enabled = true;
            status = false;
            //if(dgvCategory.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
            //{
            //    txtCatId.Text = dgvCategory.Rows[e.RowIndex].Cells[1].Value.ToString();
            //    // ....
            //}
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
           

         
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string catid = txtCatId.Text.Trim();
            string catname = txtCatName.Text.Trim();
            string catdes = txtDescription.Text.Trim();
            ArrayList arrayList = new ArrayList() { catid, catname, catdes };
            if (Category.deleteCategory(arrayList) > 0)
            {
                MessageBox.Show("Xoa thanh cong", "thong bao", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            RefreshDgvCategory();
            

        }
    }
}
