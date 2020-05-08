using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Input;
using Microsoft.VisualBasic;

namespace Maparameter
{
    public partial class Form1 : Form
    {

        Image pic;
        Bitmap DrawArea;
        Graphics g;

        List<Tuple<String, String, Point>> listSpecialBlock;
        List<Tuple<int, Point>> listLevelBlock;

        string filePath;

        public Form1()
        {
            InitializeComponent();
            listSpecialBlock = new List<Tuple<String, String, Point>>();
            listLevelBlock = new List<Tuple<int, Point>>();
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

        private void BtnWrite_Click(object sender, EventArgs e)
        {
            WriteLevels();
        }

        private void BtnSpecial_Click(object sender, EventArgs e)
        {
            WriteSpecialBlock();
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
            listSpecialBlock = new List<Tuple<string, string, Point>>();
            listLevelBlock = new List<Tuple<int, Point>>();
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
            Point p = new Point(xVal, yVal);

            Tuple<int, Point> z = GetLevelBlock(p);
            Pen mypen;
            switch (z.Item1)
            {
                case 0:
                    listLevelBlock.Remove(z);
                    listLevelBlock.Add(new Tuple<int, Point>(1, p));
                    mypen = new Pen(Color.Red, 2f);
                    break;

                case 1:
                    listLevelBlock.Remove(z);
                    listLevelBlock.Add(new Tuple<int, Point>(2, p));
                    mypen = new Pen(Color.Yellow, 2f);
                    break;

                case 2:
                    listLevelBlock.Remove(z);
                    listLevelBlock.Add(new Tuple<int, Point>(0, p));
                    mypen = new Pen(Color.Black, 2f);
                    break;

                case -1:
                    listLevelBlock.Add(new Tuple<int, Point>(1, p));
                    mypen = new Pen(Color.Red, 2f);
                    break;
                    
                default:
                    return;
            }
            
            g = Graphics.FromImage(DrawArea);
            g.DrawRectangle(mypen, xVal * blockSize, yVal * blockSize, blockSize, blockSize);
            PbPic.Image = DrawArea;
            g.Dispose();
        }

        private Tuple<int, Point> GetLevelBlock(Point p)
        {
            Tuple<int, Point> t;
            for (int c = 0; c <= 2; c++)
            {
                t = new Tuple<int, Point>(c, p);
                if (listLevelBlock.Contains(t))
                    return t;
            }
            return new Tuple<int, Point>(-1, p);
        }

        private void WriteLevels()
        {
            string picCsvLevels = filePath.Substring(0, filePath.Length - filePath.Split('.').Last().Length - 1) + "_levels.csv";
            List<string> levelsData = new List<string>
            {
                "Level;X;Y",
            };
            foreach (Tuple<int, Point> t in listLevelBlock)
            {
                levelsData.Add(t.Item1.ToString() + ";" + t.Item2.X.ToString() + ";" + t.Item2.Y.ToString());
            }
            File.WriteAllLines(picCsvLevels, levelsData);
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
            Tuple<String, String, Point> res = new Tuple<String, String, Point>(frmPlaceSpecialBlock.res.Item1, frmPlaceSpecialBlock.res.Item2, new Point(xVal, yVal));

            if (res.Item1 == "" | res.Item2 == "")
                return;

            Tuple<String, String, Point> oldOne = GetSpecialBlockInfo(xVal, yVal);
            if (oldOne.Item1 != "")
            {
                if (MessageBox.Show("Le block est déjà défini, voulez-vous l'écraser ?", "block déjà défini") == DialogResult.Yes)
                    listSpecialBlock.Remove(oldOne);
            }

            listSpecialBlock.Add(res);

            Pen mypen = new Pen(Color.Orange, 2f);
            g = Graphics.FromImage(DrawArea);
            g.DrawLine(mypen, xVal * blockSize, yVal * blockSize, xVal * blockSize + blockSize, yVal * blockSize + blockSize);
            g.DrawLine(mypen, xVal * blockSize + blockSize, yVal * blockSize, xVal * blockSize, yVal * blockSize + blockSize);
            PbPic.Image = DrawArea;
            g.Dispose();
        }

        private Tuple<string, string, Point> GetSpecialBlockInfo(int xVal, int yVal)
        {
            Point p = new Point(xVal, yVal);
            foreach (Tuple<string, string, Point> t in listSpecialBlock)
            {
                if (t.Item3 == p)
                    return t;
            }
            return new Tuple<string, string, Point>("", "", p);
        }

        private void WriteSpecialBlock()
        {
            string picCsvSpecial = filePath.Substring(0, filePath.Length - filePath.Split('.').Last().Length - 1) + "_events.csv";
            List<string> specialData = new List<string>
            {
                "Type;Name;X;Y",
            };
            foreach (Tuple<String, String, Point> t in listSpecialBlock)
            {
                specialData.Add(t.Item1 + ";" + t.Item2 + ";" + t.Item3.X.ToString() + ";" + t.Item3.Y.ToString());
            }
            File.WriteAllLines(picCsvSpecial, specialData);

        }

        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
