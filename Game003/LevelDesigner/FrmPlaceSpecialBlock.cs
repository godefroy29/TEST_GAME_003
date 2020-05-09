using GameDll;
using System;
using System.Windows.Forms;

namespace Maparameter
{
    public partial class FrmPlaceSpecialBlock : Form
    {
        public AreaEvent res = new AreaEvent();
        private int x, y;

        

        public FrmPlaceSpecialBlock(int x, int y)
        {
            InitializeComponent();
            this.x = x;
            this.y = y;
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            res = new AreaEvent();
            Close();
        }

        private void FrmPlaceSpecialBlock_Load(object sender, EventArgs e)
        {

        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            try
            {
                res = new AreaEvent(cboType.SelectedItem.ToString().ToUpper(), CboDir.SelectedItem.ToString().ToUpper(), txtName.Text.Trim(' '), x, y);
                Close();
            }
            catch (Exception)
            {
                
            }
            
        }
    }
}
