using ExerciseProductDB.DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ExerciseProductDB
{
    public partial class FrmAddProduct : Form
    {
        public FrmAddProduct()
        {
            InitializeComponent();
        }

        private void FrmAddProduct_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Hide();
            FrmProduct main = new FrmProduct();
            main.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
        bool checkDisconnected()
        {
            if (checkbDis.Checked)
            {
                return true;
            }
            return false;
        }
        bool checkInputId(string id)
        {
            Regex regex = new Regex(@"^(P)\d{4}$");
            if (!regex.IsMatch(id)) return false;
            return true;
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            //check dau vao 
            if (!checkInputId(txtId.Text))
            {
                MessageBox.Show("Wrong Format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtId.Focus();
            }
            else if(txtName.Text == "")
            {
                MessageBox.Show("Your name is empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtName.Focus();
            }
            else
            {
                ProductDAO.Add(txtId.Text, txtName.Text, cbCate.SelectedValue.ToString(), txtUni.Text, txtPrice.Text, txtQuanti.Text, checkDisconnected(), datetime.Value);
                DialogResult dialogResult = MessageBox.Show("Add Successfully. Do you want to back to main !", "", MessageBoxButtons.YesNo);
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
            
        // add xong hien thong bao da add thanh cong
        // muon quay lai main ko, yes ve, no o lai
        }

        private void FrmAddProduct_Load(object sender, EventArgs e)
        {
            cbCate.DataSource = null;
            cbCate.DataSource = ProductDAO.getCate();
            cbCate.DisplayMember = "CategoryName";
            cbCate.ValueMember = "CategoryId";

        }
    }
}
