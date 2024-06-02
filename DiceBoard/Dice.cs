using DiceBoard.Helper;
using static DiceBoard.Enums;

namespace DiceBoard
{
    internal class Dice
    {
        private Random rand = new Random();
        private int maxRollCount = 4;

        private Side face;
        private Side rear;
        private Side left;
        private Side right;
        private Side top;
        private Side bottom;

        public Side Face => face;

        public Dice(bool shuffle = false)
        {
            face = new Side(SideNumber.One);
            rear = new Side(SideNumber.Six);
            left = new Side(SideNumber.Three);
            right = new Side(SideNumber.Four);
            top = new Side(SideNumber.Two);
            bottom = new Side(SideNumber.Five);

            if (shuffle)
                Shuffle();
        }

        public Dice(Dice dice)
        {
            face = dice.face;
            rear = dice.rear;
            left = dice.left;
            right = dice.right;
            top = dice.top;
            bottom = dice.bottom;
        }

        public void Shuffle()
        {
            Shuffle(Randomizer.Get(maxRollCount), RollRight, RollLeft);
            Shuffle(Randomizer.Get(maxRollCount), RollDown, RollUp);
        }

        private void Shuffle(int iterationCount, Action action1, Action action2)
        {
            for (int iterator = 0; iterator < Math.Abs(iterationCount); iterator++)
            {
                if (iterationCount > 0)
                    action1();
                else
                    action2();
            }
        }

        public void RollUp()
        {
            var temp = new Side(face);
            face = new Side(bottom);
            bottom = new Side(rear);
            rear = new Side(top);
            top = temp;
        }

        public void RollDown()
        {
            var temp = new Side(face);
            face = new Side(top);
            top = new Side(rear);
            rear = new Side(bottom);
            bottom = temp;
        }

        public void RollLeft()
        {
            var temp = new Side(face);
            face = new Side(right);
            right = new Side(rear);
            rear = new Side(left);
            left = temp;
        }

        public void RollRight()
        {
            var temp = new Side(face);
            face = new Side(left);
            left = new Side(rear);
            rear = new Side(right);
            right = temp;
        }

        public void SetEmpty()
        {
            face = new Side(SideNumber.Zero);
            rear = new Side(SideNumber.Zero);
            left = new Side(SideNumber.Zero);
            right = new Side(SideNumber.Zero);
            top = new Side(SideNumber.Zero);
            bottom = new Side(SideNumber.Zero);
        }
    }
}
