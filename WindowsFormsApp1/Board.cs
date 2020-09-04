using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackgammonWorld.Properties;

namespace BackgammonWorld
{
    class Board
    {
        public Triangle[] triangles = new Triangle[26];
        public BeatedPlace BlackBeatedPlace=new BeatedPlace(true);
        public BeatedPlace WhiteBeatedPlace = new BeatedPlace(false);


        public Board()
        {
            initializeBottom();
            initializeTop();
            initializeTriangles();
            initializeSideStocks();
        }

        void initializeBottom()
        {
            int bottomXLoction = 745;

            for (int i = 1; i <= 12; i++)
            {
                if (i == 7)
                    bottomXLoction -= 47;

                Triangle t = new Triangle();
                t.Container = new FlowLayoutPanel();
                t.Container.FlowDirection = FlowDirection.BottomUp;
                t.Container.Location = new Point(bottomXLoction, 422);//434
                t.Container.Name = "triangle" + i;
                t.Container.Size = new Size(57, 300);
                t.Container.Visible = true;
                t.Container.BackColor = Color.Transparent;
                t.Container.Cursor = Cursors.Hand;
                t.Container.Enabled = false;
                t.Container.TabIndex = i;
                triangles[i] = t;
                bottomXLoction -= 61;
            }
        }
        void initializeTop()
        {
            int bottomXLoction = 24;

            for (int i = 13; i <= 24; i++)
            {
                if (i == 19)
                    bottomXLoction += 41;

                Triangle t = new Triangle();
                t.Container = new FlowLayoutPanel();
                t.Container.FlowDirection = FlowDirection.TopDown;
                t.Container.Location = new Point(bottomXLoction, 18);
                t.Container.Name = "triangle" + i;
                t.Container.Size = new Size(57, 300);
                t.Container.Visible = true;
                t.Container.BackColor = Color.Transparent;
                t.Container.Enabled = false;
                t.Container.TabIndex = i;
                t.Container.Cursor = Cursors.Hand;
                triangles[i] = t;
                bottomXLoction += 62;
            }
        }
        void initializeTriangles()
        {
            
            //White
            triangles[1].InitializeTriangle(2, false);
            triangles[12].InitializeTriangle(5, false);
            triangles[17].InitializeTriangle(3, false);
            triangles[19].InitializeTriangle(5, false);
            //Black
            
            triangles[24].InitializeTriangle(2, true);
            triangles[13].InitializeTriangle(5, true);
            triangles[8].InitializeTriangle(3, true);
            triangles[6].InitializeTriangle(5, true);
            
            /*
            triangles[5].InitializeTriangle(2, false);
            triangles[13].InitializeTriangle(1, false);
            triangles[19].InitializeTriangle(5, false);
            triangles[20].InitializeTriangle(3, false);

            //WhiteBeatedPlace.Add();
            
            triangles[1].InitializeTriangle(4, true);
            triangles[2].InitializeTriangle(6, true);
            triangles[3].InitializeTriangle(4, true);
            */

        }
        void initializeSideStocks()
        {
            Triangle sideStock = new Triangle();
            //White
            sideStock.IsBlack = false;
            sideStock.Container = new FlowLayoutPanel();
            sideStock.Container.FlowDirection = FlowDirection.BottomUp;
            sideStock.Container.Location = new Point(824, 17);
            sideStock.Container.Name = "triangle25";
            sideStock.Container.Size = new Size(86, 323);
            sideStock.Container.Visible = true;
            sideStock.Container.Cursor = Cursors.Hand;
            sideStock.Container.Enabled = false;
            sideStock.Container.BackColor = Color.Transparent;
            sideStock.Container.TabIndex = 25;
            triangles[25] = sideStock;

            //Black
            sideStock = new Triangle();
            sideStock.IsBlack = true;
            sideStock.Container = new FlowLayoutPanel();
            sideStock.Container.FlowDirection = FlowDirection.BottomUp;
            sideStock.Container.Location = new Point(824, 392);
            sideStock.Container.Name = "triangle0";
            sideStock.Container.Size = new Size(86, 324);
            sideStock.Container.Visible = true;
            sideStock.Container.Cursor = Cursors.Hand;
            sideStock.Container.Enabled = false;
            sideStock.Container.BackColor = Color.Transparent;
            sideStock.Container.TabIndex = 0;
            triangles[0] = sideStock;
        }


        
        public void DisableAllBoard()
        {
            foreach (Triangle triangle in triangles)
            {
                triangle.Container.Enabled = false;
            }
            BlackBeatedPlace.BeatedCheckersPlace.Enabled = false;
            WhiteBeatedPlace.BeatedCheckersPlace.Enabled = false;
        }
        public void BeatPiece (int triangleNumber)
        {
            Triangle selectedTriangle = triangles[triangleNumber];
            if (selectedTriangle.IsBlack)
                BlackBeatedPlace.Add();
            else
                WhiteBeatedPlace.Add();
            selectedTriangle.Remove();   
        }

    }


    class BeatedPlace
    {
        public bool isBlack { get; set; }
        public int Amount { get; set; }
        public FlowLayoutPanel BeatedCheckersPlace { get; set; }

        public BeatedPlace(bool isBlack)
        {
            this.isBlack = isBlack;
            if (isBlack)
            {
                BeatedCheckersPlace = new FlowLayoutPanel();
                BeatedCheckersPlace.FlowDirection = FlowDirection.BottomUp;
                BeatedCheckersPlace.Location = new Point(385, 275);
                BeatedCheckersPlace.Name = "blackBeatedPlace";
                BeatedCheckersPlace.Size = new Size(65, 82);
                BeatedCheckersPlace.Visible = true;
                BeatedCheckersPlace.Enabled = false;
                BeatedCheckersPlace.Cursor = Cursors.Hand;
                BeatedCheckersPlace.BackColor = Color.Transparent;
                BeatedCheckersPlace.TabIndex = 27;
            }
            else
            {
                BeatedCheckersPlace = new FlowLayoutPanel();
                BeatedCheckersPlace.FlowDirection = FlowDirection.BottomUp;
                BeatedCheckersPlace.Location = new Point(385, 353);
                BeatedCheckersPlace.Name = "whiteBeatedPlace";
                BeatedCheckersPlace.Size = new Size(65, 82);
                BeatedCheckersPlace.Visible = true;
                BeatedCheckersPlace.Enabled = false;
                BeatedCheckersPlace.Cursor = Cursors.Hand;
                BeatedCheckersPlace.BackColor = Color.Transparent;
                BeatedCheckersPlace.TabIndex = 28;
                
            }
        }

        PictureBox getCheckerPictureBox()
        {
            PictureBox newPiece = new PictureBox();
            newPiece.Image = isBlack ? Resources.black4 : Resources.white3;
            newPiece.Width = 55;
            newPiece.Height = 55;
            newPiece.Margin = new Padding(1, 0, 0, 0);
            newPiece.Anchor = AnchorStyles.None;
            newPiece.SizeMode = PictureBoxSizeMode.StretchImage;
            newPiece.Enabled = false;
            return newPiece;
        }
        public void Add()
        {
            PictureBox newPiece = getCheckerPictureBox();

            Amount++;
            if (Amount == 1)
                this.BeatedCheckersPlace.Controls.Add(newPiece);

            else
            {
                foreach (Control c in this.BeatedCheckersPlace.Controls)
                {
                    if (c.GetType() == typeof(Label))
                    {
                        c.Text = this.Amount + "";
                        return;
                    }
                }

                Label moreThanOneCheckerLabel = new Label
                {
                    Text = Amount + "",
                    TextAlign = ContentAlignment.TopCenter,
                    Dock = DockStyle.Fill,
                    ForeColor = Color.White,
                    Margin = new Padding(0),
                    Font = new Font("Serif", 10, FontStyle.Bold)

                };
                this.BeatedCheckersPlace.Controls.Add(moreThanOneCheckerLabel);
            }
        }

        public void Remove()
        {
            if (Amount > 0)
                Amount--;
            else
                return;

            if (Amount > 1)
            {
                foreach (Control c in this.BeatedCheckersPlace.Controls)
                    if (c.GetType() == typeof(Label))
                    {
                        c.Text = this.Amount + "";
                        return;
                    }
            }

            if (Amount == 0)
                this.BeatedCheckersPlace.Controls.RemoveAt(0);
            if (Amount == 1)
                this.BeatedCheckersPlace.Controls.RemoveAt(1);

        }
    }
}
