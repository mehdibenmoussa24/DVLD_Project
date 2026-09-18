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
    public partial class frmAddUpdatePerson : Form
    {
        public frmAddUpdatePerson()
        {
            InitializeComponent();
        }

        private void frmAddUpdatePerson_Load(object sender, EventArgs e)
        {

        }

        private void TextBox_Enter(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Text.StartsWith("e.g."))
            {
                textBox.Clear();
            }
        }

    

    }
}
