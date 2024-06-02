using static DiceBoard.Enums;

namespace DiceBoard
{
    internal class Side
    {
        public Side(SideNumber sideNumber)
        {
            Number = (int)sideNumber;
            Image = $"Resources/Images/dice_{Number}_icon.png";
        }

        public Side(Side side)
        {
            Number = side.Number;
            Image = side.Image;
        }

        public int Number { get; }
        public string Image { get; }

        //public int Rotation { get; }???    }
    }
}
