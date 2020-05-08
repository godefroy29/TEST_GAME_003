using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Maparameter
{
    public partial class FrmPlaceSpecialBlock : Form
    {
        public Tuple<String, String> res = new Tuple<String, String>("", "");
        private int x, y;

        public FrmPlaceSpecialBlock(int x, int y)
        {
            InitializeComponent();
            this.x = x;
            this.y = y;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            res = new Tuple<String, String>("","");
            Close();
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            res = new Tuple<String,String>(cboType.SelectedItem.ToString(),txtName.Text.Trim(' '));
            Close();
        }
    }
}
