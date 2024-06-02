using DiceBoard.Helper;

namespace DiceBoard
{
    public class Board
    {
        private Position empty;
        private int rowCount;
        private int colCount;
        private Dice[,] diceArray;

        public Board(int rows, int cols)
        {
            rowCount = rows;
            colCount = cols;

            empty = new Position(Randomizer.Get(rowCount), Randomizer.Get(colCount));

            diceArray = new Dice[rowCount, colCount];

            for (int row = 0; row < rowCount; row++)
                for (int col = 0; col < colCount; col++)
                    diceArray[row, col] = new Dice(true);
            
            diceArray[empty.Row, empty.Col].SetEmpty();
        }

        public string[,] GetFaces()
        {
            var result = new string[rowCount, colCount];

            for (int row = 0; row < rowCount; row++)
                for (int col = 0; col < colCount; col++)
                    result[row,col] = diceArray[row, col].Face.Image;

            return result;
        }

        public int GetSum()
        {
            var result = 0;

            for (int row = 0; row < rowCount; row++)
                for (int col = 0; col < colCount; col++)
                    result += diceArray[row, col].Face.Number;

            return result;
        }

        public void MoveUp()
        {
            if (empty.Row == rowCount - 1)
                return;

            diceArray[empty.Row + 1, empty.Col].RollUp();
            diceArray[empty.Row, empty.Col] = new Dice(diceArray[empty.Row + 1, empty.Col]);
            diceArray[empty.Row + 1, empty.Col].SetEmpty();
            empty.Row++;
        }

        public void MoveDown()
        {
            if (empty.Row == 0)
                return;

            diceArray[empty.Row - 1, empty.Col].RollDown();
            diceArray[empty.Row, empty.Col] = new Dice(diceArray[empty.Row - 1, empty.Col]);
            diceArray[empty.Row - 1, empty.Col].SetEmpty();
            empty.Row--;
        }

        public void MoveLeft()
        {
            if (empty.Col == colCount - 1)
                return;

            diceArray[empty.Row, empty.Col + 1].RollLeft();
            diceArray[empty.Row, empty.Col] = new Dice(diceArray[empty.Row, empty.Col + 1]);
            diceArray[empty.Row, empty.Col + 1].SetEmpty();
            empty.Col++;
        }

        public void MoveRight()
        {
            if (empty.Col == 0)
                return;

            diceArray[empty.Row, empty.Col - 1].RollRight();
            diceArray[empty.Row, empty.Col] = new Dice(diceArray[empty.Row, empty.Col - 1]);
            diceArray[empty.Row, empty.Col - 1].SetEmpty();
            empty.Col--;
        }
    }
}
