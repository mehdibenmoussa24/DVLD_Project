using System;
using DVLD.Business;
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
    public partial class ctrlUserCard : UserControl
    {
        private int _UserID;
        private User _User;

        public int UserID
        {
            get { return _UserID; }
        }
        public ctrlUserCard()
        {
            InitializeComponent();
        }



        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _User = User.FindByUserID(_UserID);
            if (_User == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            _FillUserInfo();
        }
        private void _FillUserInfo()
        {
            ctrlPersonCard1.LoadPersonData(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName.ToString();
            if(_User.IsActive == true)
            {
                lblIsActive.Text = "Yes";
                lblIsActive.ForeColor = Color.Green;
            }
            else
            {
                lblIsActive.Text = "No";
                lblIsActive.ForeColor = Color.Red;
            }
        }

        private void _ResetPersonInfo()
        {

            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "?????";
            lblUserName.Text = "?????";
            lblIsActive.Text = "?????";
        }
    }
}
