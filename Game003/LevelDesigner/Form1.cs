using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using GameDll;
using Microsoft.VisualBasic;

namespace Maparameter
{
    public partial class Form1 : Form
    {

        Image pic;
        Bitmap DrawArea;
        Graphics g;

        AreaLevelList listLevelBlock;
        AreaEventList listEventBlock;

        string filePath;

        public Form1()
        {
            InitializeComponent();
            listEventBlock = new AreaEventList(GameVars.BLOCK);
            listLevelBlock = new AreaLevelList(GameVars.BLOCK);
        }

        private void btnOpenPic_Click(object sender, EventArgs e)
        {
            OfdPic.ShowDialog();
            filePath = OfdPic.FileName;
            txtPath.Text = filePath;
            if (File.Exists(filePath))
            {
                pic = Image.FromFile(filePath);
                PbPic.Size = pic.Size;
                PbPic.BackgroundImage = pic;
                DrawArea = new Bitmap(pic.Size.Width, pic.Size.Height);
                PbPic.Image = DrawArea;
            }
        }

        private void BtnWriteLevel_Click(object sender, EventArgs e)
        {
            GameFuncs.WriteLevels(listLevelBlock, filePath.Substring(0, filePath.Length - filePath.Split('.').Last().Length - 1) + "_level.csv");
        }

        private void BtnWriteEvent_Click(object sender, EventArgs e)
        {
            GameFuncs.WriteEvents(listEventBlock, filePath.Substring(0, filePath.Length - filePath.Split('.').Last().Length - 1) + "_event.csv");
        }

        private void PbPic_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.MouseEventArgs me = (System.Windows.Forms.MouseEventArgs)e;
            if (Keyboard.IsKeyDown(Key.LeftCtrl) | Keyboard.IsKeyDown(Key.RightCtrl))
            {
                if (me.Button == MouseButtons.Left)
                    ZoneSpecial(me);
            }
            else
            {
                if (me.Button == MouseButtons.Left)
                    ZoneLevel(me);
            }

        }

        private void BtnReset_Click(object sender, EventArgs e)
        {
            listEventBlock = new AreaEventList(GameVars.BLOCK);
            listLevelBlock = new AreaLevelList(GameVars.BLOCK);
            DrawArea = new Bitmap(pic.Size.Width, pic.Size.Height);
            PbPic.Image = DrawArea;
        }

        private void BtnRedraw_Click(object sender, EventArgs e)
        {

        }


        #region "BLOCK LEVEL"

        private void ZoneLevel(System.Windows.Forms.MouseEventArgs me)
        {
            int blockSize = (int)NudBlockSize.Value;
            int xVal = (int)(me.Location.X / blockSize);
            int yVal = (int)(me.Location.Y / blockSize);
            Pen mypen;
            AreaLevel z = listLevelBlock.GetAreaLevel(xVal, yVal);
            switch (z.ZPos)
            {
                case GameEnums.AreaLevel_ZPos.GROUND:
                    listLevelBlock.EditZPos(GameEnums.AreaLevel_ZPos.PLAYER, xVal, yVal);
                    mypen = new Pen(Color.Red, 2f);
                    break;

                case GameEnums.AreaLevel_ZPos.PLAYER:
                    listLevelBlock.EditZPos(GameEnums.AreaLevel_ZPos.SKY, xVal, yVal);
                    mypen = new Pen(Color.Yellow, 2f);
                    break;

                case GameEnums.AreaLevel_ZPos.SKY:
                    listLevelBlock.EditZPos(GameEnums.AreaLevel_ZPos.GROUND, xVal, yVal);
                    mypen = new Pen(Color.Black, 2f);
                    break;

                case GameEnums.AreaLevel_ZPos.NONE:
                default:
                    listLevelBlock.EditZPos(GameEnums.AreaLevel_ZPos.PLAYER, xVal, yVal);
                    mypen = new Pen(Color.Red, 2f);
                    break;
            }
            
            g = Graphics.FromImage(DrawArea);
            g.DrawRectangle(mypen, xVal * blockSize, yVal * blockSize, blockSize, blockSize);
            PbPic.Image = DrawArea;
            g.Dispose();
        }

        #endregion  

        #region "BLOCK EVENT"
        private void ZoneSpecial(System.Windows.Forms.MouseEventArgs me)
        {

            int blockSize = (int)NudBlockSize.Value;
            int xVal = (int)(me.Location.X / blockSize);
            int yVal = (int)(me.Location.Y / blockSize);

            FrmPlaceSpecialBlock frmPlaceSpecialBlock = new FrmPlaceSpecialBlock(xVal, yVal);
            frmPlaceSpecialBlock.ShowDialog();
            AreaEvent res = frmPlaceSpecialBlock.res;

            if (res.AType == GameEnums.AreaEvent_Type.NONE)
                return;

            List<AreaEvent> oldOnes = listEventBlock.GetAreaEvent(res.AType, GameEnums.Direction.NONE, "", res.XVal, res.YVal);
            if (oldOnes.Count() > 0)
                listEventBlock.eventsSource.Remove(oldOnes.First());
            listEventBlock.eventsSource.Add(res);

            Pen mypen = new Pen(Color.Orange, 2f);
            g = Graphics.FromImage(DrawArea);
            g.DrawLine(mypen, xVal * blockSize, yVal * blockSize, xVal * blockSize + blockSize, yVal * blockSize + blockSize);
            g.DrawLine(mypen, xVal * blockSize + blockSize, yVal * blockSize, xVal * blockSize, yVal * blockSize + blockSize);
            PbPic.Image = DrawArea;
            g.Dispose();
        }


        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
