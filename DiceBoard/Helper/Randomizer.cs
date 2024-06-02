namespace DiceBoard.Helper
{
    public class Randomizer
    {
        private static Random rand = new Random();

        public static int Get(int max)
        {
            return Get(0, max);
        }

        public static int Get(int min, int max)
        {
            return rand.Next(min, max);
        }
    }
}
