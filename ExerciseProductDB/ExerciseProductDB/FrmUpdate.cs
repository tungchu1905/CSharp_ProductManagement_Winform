using ExerciseProductDB.DAO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ExerciseProductDB
{
    public partial class FrmUpdate : Form
    {
        private string id;
        public FrmUpdate(string v)
        {
            InitializeComponent();
            id = v;
        }
        
        private void FrmUpdate_Load(object sender, EventArgs e)
        {
            cbCate.DataSource = null;
            cbCate.DataSource = ProductDAO.getCate();
            cbCate.DisplayMember = "CategoryName";
            cbCate.ValueMember = "CategoryId";
            txtId.Text = id;
            txtId.Enabled = false;
            ArrayList list = new ArrayList() { "", "", "", "", "", "", "", ""};
            ProductDAO.LoadDataById(list, id);
            txtName.Text = list[1].ToString();
            cbCate.SelectedValue = list[2].ToString();
            txtUni.Text = list[3].ToString();
            txtPrice.Text = list[4].ToString();
            txtQuanti.Text = list[5].ToString();
            if (list[6].ToString().Equals("True"))
            {
                checkbDis.Checked = true;
            }
            datetime.Value = Convert.ToDateTime( list[7].ToString());
        }
        bool checkDis()
        {
            if(checkbDis.Checked == true)
            {
                return true;
            }
            return false;
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(txtName.Text == "")
            {
                MessageBox.Show("Your name is empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
            }
            else
            {
                ProductDAO.Update(txtName.Text, cbCate.SelectedValue.ToString(), txtUni.Text, txtPrice.Text, txtQuanti.Text, checkDis(), datetime.Value, txtId.Text);
                DialogResult dialogResult = MessageBox.Show("Update Successfully. Do you want to back to main !", "", MessageBoxButtons.YesNo);
                switch (dialogResult)
                {
                    case DialogResult.Yes:
                        this.Hide();
                        FrmProduct main = new FrmProduct();
                        main.ShowDialog();
                        this.Close();
                        break;
                    case DialogResult.No:
                        break;
                }
            }
            
            //thong bao
        }

        private void FrmUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            FrmProduct main = new FrmProduct();
            main.ShowDialog();
            this.Close();
        }
    }
}
