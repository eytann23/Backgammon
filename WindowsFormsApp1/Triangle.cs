using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackgammonWorld.Properties;

namespace BackgammonWorld
{


   
    class Triangle
    {
        public bool IsBlack { get; set; }
        public int PiecesAmount { get; set; }
        public FlowLayoutPanel Container { get; set; }


        public void InitializeTriangle(int amount, bool isBlack)
        {

            for (int i=1;i<=amount;i++)
                Add(isBlack);
        }

        PictureBox getCheckerPictureBox(bool isBlackTurn)
        {
            PictureBox newPiece = new PictureBox();
            newPiece.Image = isBlackTurn ? Resources.black4 : Resources.white3;
            newPiece.Width = 55;
            newPiece.Height = 55;
            newPiece.Margin = new Padding(1, 0, 0, 0);
            newPiece.Anchor = AnchorStyles.None;
            newPiece.SizeMode = PictureBoxSizeMode.StretchImage;
            newPiece.Tag = this.Container.TabIndex;
            newPiece.Enabled = false;
            return newPiece;
        }
        public void Add(bool isBlackTurn)
        {
            PictureBox newPiece = getCheckerPictureBox(isBlackTurn);
            const int blackOutsideStock = 0, whiteOutsideStock = 25;
            
            if (this.Container.TabIndex == blackOutsideStock || this.Container.TabIndex == whiteOutsideStock)
            {
                AddToSideStock(isBlackTurn);
                PiecesAmount++;
                return;
            }

            this.IsBlack = isBlackTurn;

            PiecesAmount++;
            if (PiecesAmount <= 5)
            {
                this.Container.Controls.Add(newPiece);
            }
            else
            {
                foreach (Control c in this.Container.Controls)
                {
                    if (c.GetType() == typeof(Label))
                    {
                        c.Text = this.PiecesAmount + "";
                        return;
                    }      
                }
                Label moreThanFiveLabel = new Label
                {
                    Text = PiecesAmount + "",
                    TextAlign = ContentAlignment.TopCenter,
                    Dock = DockStyle.Fill,
                    Margin = new Padding(0, 0, 0, 2),
                    ForeColor =Color.Black,
                    Font = new Font("Serif", 18, FontStyle.Bold)    
                };
                this.Container.Controls.Add(moreThanFiveLabel);
            }
        }

        public void Remove()
        {
            if (PiecesAmount > 0)
                PiecesAmount--;
            else
                return;
            
            if (PiecesAmount>5)
            {
                foreach (Control c in this.Container.Controls)
                    if (c.GetType() == typeof(Label))
                    {
                        c.Text = this.PiecesAmount + "";
                        return;
                    }
            }   
            
            if (PiecesAmount < 5)
                this.Container.Controls.RemoveAt(0);
            if (PiecesAmount == 5)
                this.Container.Controls.RemoveAt(5);
            if (PiecesAmount == 0)
                IsBlack = false;

        }
        private void AddToSideStock(bool isBlackTurn)
        {
            PictureBox newPiece = new PictureBox();
            newPiece.Image = isBlackTurn ? Resources.Black_out : Resources.White_out;
            newPiece.Width = 85;
            newPiece.Height = 19;
            newPiece.Margin = new Padding(0,2,0,0);
            newPiece.Anchor = AnchorStyles.None;
            newPiece.SizeMode = PictureBoxSizeMode.StretchImage;
            newPiece.Tag = this.Container.TabIndex;
            newPiece.Enabled = false;

            this.Container.Controls.Add(newPiece);
        }
        public bool IsTriangleValidAsSource(bool isBlackTurn)
        {
            if (this.IsBlack == isBlackTurn && this.PiecesAmount > 0)
                return true;
            else
                return false;
        }
        public bool IsTriangleValidAsDestination(bool isBlackTurn)
        {
            if (this.IsBlack == isBlackTurn || this.PiecesAmount == 0)
                return true;
            else if ((this.IsBlack != isBlackTurn) && (this.PiecesAmount == 1))
                return true;
            else
                return false;
        }
    }

}
