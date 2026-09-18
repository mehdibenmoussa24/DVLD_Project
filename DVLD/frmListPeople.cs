using DVLD.Business;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD
{
    public partial class frmListPeople : Form
    {

        private static DataTable _dtAllPeople = Person.GetAllPeople();


        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                                             "FirstName", "SecondName", "ThirdName", "LastName",
                                                                             "GenderCaption", "DateOfBirth", "CountryName", "Phone", "Email");

        private void _RefreshPeopleList()
        {
            _dtAllPeople = Person.GetAllPeople();

            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                                             "FirstName", "SecondName", "ThirdName", "LastName",
                                                                             "GenderCaption", "DateOfBirth", "CountryName", "Phone", "Email");
            dgvPeople.DataSource = _dtPeople;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }
        public frmListPeople()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmListPeople_Load(object sender, EventArgs e)
        {
            dgvPeople.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();

            if (dgvPeople.Rows.Count > 0)
            {
                dgvPeople.Columns[0].HeaderText = "Person ID";
                dgvPeople.Columns[0].Width = 110;

                dgvPeople.Columns[1].HeaderText = "National No";
                dgvPeople.Columns[1].Width = 120;


                dgvPeople.Columns[2].HeaderText = "First Name";
                dgvPeople.Columns[2].Width = 120;

                dgvPeople.Columns[3].HeaderText = "Second Name";
                dgvPeople.Columns[3].Width = 140;


                dgvPeople.Columns[4].HeaderText = "Third Name";
                dgvPeople.Columns[4].Width = 120;

                dgvPeople.Columns[5].HeaderText = "Last Name";
                dgvPeople.Columns[5].Width = 120;

                dgvPeople.Columns[6].HeaderText = "Gender";
                dgvPeople.Columns[6].Width = 120;

                dgvPeople.Columns[7].HeaderText = "Date Of Birth";
                dgvPeople.Columns[7].Width = 140;

                dgvPeople.Columns[8].HeaderText = "Nationality";
                dgvPeople.Columns[8].Width = 120;


                dgvPeople.Columns[9].HeaderText = "Phone";
                dgvPeople.Columns[9].Width = 120;


                dgvPeople.Columns[10].HeaderText = "Email";
                dgvPeople.Columns[10].Width = 170;
            }
        }

   

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = string.Empty;

            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;
                case "National No":
                    FilterColumn = "NationalNo";
                    break;
                case "First Name":
                    FilterColumn = "FirstName";
                    break;
                case "Second Name":
                    FilterColumn = "SecondName";
                    break;
                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;
                case "Last Name":
                    FilterColumn = "LastName";
                    break;
                case "Nationality":
                    FilterColumn = "CountryName";
                    break;
                case "Gender":
                    FilterColumn = "GenderCaption";
                    break;
                case "Phone":
                    FilterColumn = "Phone";
                    break;
                case "Email":
                    FilterColumn = "Email";
                    break;
                default:
                    FilterColumn = "None";
                    break;
            }

            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
                return;
            }
            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dgvPeople.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");

            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
            }
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            //we allow number incase person id is selected.
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Comming Soon","Soon",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
