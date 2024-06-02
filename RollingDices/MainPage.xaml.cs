using DiceBoard;
using DiceBoard.Helper;

namespace RollingDices
{
    public partial class MainPage : ContentPage
    {
        private Board board;
        private int rows = 3;
        private int cols = 3;
        private int width = 100;
        private int height = 100;
        private int expectedNum;

        private Grid boardGrid;
        private Label currentSum;
        private Label expectedSum;
        private Button shuffleButton;

        public MainPage()
        {
            InitializeComponent();

            ConfigureBoard();
            ConfigureCurrentSum();
            ConfigureExpectedSum();
            ConfigureShuffleButton();

            ConfigureSwipeGestures();
            ConfigureTapGestures();

            SetBoard(true);
            SetCurrentSum();
            SetExpectedSum();

            Content = ConfigureMainGrid();
        }

        private void CheckWin()
        {
            if (expectedNum == board.GetSum())
                ShowWin();

            SetBoard();
            SetCurrentSum();
        }

        private void ShowWin()
        {
            boardGrid.GestureRecognizers.Clear();
            expectedSum.Text = "You Won!!!";
        }

        #region Configure
        private void ConfigureCurrentSum()
        {
            currentSum = new Label
            {
                Padding = new Thickness(0, 20, 0, 0),
                FontAttributes = FontAttributes.Bold,
                FontSize = 28,
                TextColor = Colors.Blue,
                HorizontalTextAlignment = TextAlignment.Center,
            };
        }

        private void ConfigureExpectedSum()
        {
            expectedSum = new Label
            {
                FontAttributes = FontAttributes.Bold,
                FontSize = 64,
                TextColor = Colors.Green,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
            };
        }

        private void ConfigureShuffleButton()
        {
            shuffleButton = new Button
            {
                Text = "Shuffle",
                FontSize = 24,
            };
        }

        private void ConfigureBoard()
        {
            boardGrid = new Grid
            {
                RowSpacing = 15,
                ColumnSpacing = 15,
            };

            for (int row = 0; row < rows; row++)
                boardGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(height) });

            for (int col = 0; col < cols; col++)
                boardGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(width, GridUnitType.Star) });

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    Image image = new Image
                    {
                        HeightRequest = height,
                        WidthRequest = width,
                    };
                    boardGrid.Add(image, col, row);
                }
            }
        }

        private Grid ConfigureMainGrid()
        {
            Grid mainGrid = new Grid
            {
                Padding = new Thickness(30),
                BackgroundColor = Colors.LightGray,
            };

            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100, GridUnitType.Auto) });
            mainGrid.Add(boardGrid, 0, 0);
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Auto) });
            mainGrid.Add(currentSum, 0, 1);
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(100, GridUnitType.Star) });
            mainGrid.Add(expectedSum, 0, 2);
            mainGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Auto) });
            mainGrid.Add(shuffleButton, 0, 3);

            return mainGrid;
        }

        private void ConfigureSwipeGestures()
        {
            if(boardGrid.GestureRecognizers.Any())
                return;

            SwipeGestureRecognizer leftSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Left };
            leftSwipeGesture.Swiped += OnSwipedLeft;
            SwipeGestureRecognizer rightSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Right };
            rightSwipeGesture.Swiped += OnSwipedRight;
            SwipeGestureRecognizer upSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Up };
            upSwipeGesture.Swiped += OnSwipedUp;
            SwipeGestureRecognizer downSwipeGesture = new SwipeGestureRecognizer { Direction = SwipeDirection.Down };
            downSwipeGesture.Swiped += OnSwipedDown;

            boardGrid.GestureRecognizers.Add(leftSwipeGesture);
            boardGrid.GestureRecognizers.Add(rightSwipeGesture);
            boardGrid.GestureRecognizers.Add(upSwipeGesture);
            boardGrid.GestureRecognizers.Add(downSwipeGesture);
        }

        private void ConfigureTapGestures()
        {
            TapGestureRecognizer shuffleTap = new TapGestureRecognizer();
            shuffleTap.Tapped += OnShuffleTapped;

            shuffleButton.GestureRecognizers.Add(shuffleTap);
        }
        #endregion

        #region Set Data
        private void SetBoard(bool newBoard = false)
        {
            if(newBoard)
                board = new Board(rows, cols);

            var faces = board.GetFaces();
            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    var image = boardGrid.Children[row * cols + col] as Image;
                    image!.Source = faces[row, col];
                }
            }
        }

        private void SetCurrentSum()
        {
            currentSum.Text = $"Current Sum is {board.GetSum()}";
        }

        private void SetExpectedSum()
        {
            var minValue = rows * cols - 1;
            var maxValue = 6 * minValue + 1;
            do
            {
                expectedNum = Randomizer.Get(minValue, maxValue);
            }
            while (expectedNum == board.GetSum());
            expectedSum.Text = expectedNum.ToString();
        }
        #endregion

        #region Events
        private void OnSwipedLeft(object? sender, SwipedEventArgs e)
        {
            board.MoveLeft();
            CheckWin();
        }

        private void OnSwipedRight(object? sender, SwipedEventArgs e)
        {
            board.MoveRight();
            CheckWin();
        }

        private void OnSwipedUp(object? sender, SwipedEventArgs e)
        {
            board.MoveUp();
            CheckWin();
        }

        private void OnSwipedDown(object? sender, SwipedEventArgs e)
        {
            board.MoveDown();
            CheckWin();
        }

        private void OnShuffleTapped(object? sender, TappedEventArgs e)
        {
            SetBoard(true);
            ConfigureSwipeGestures();
            SetCurrentSum();
            SetExpectedSum();
        }
        #endregion
    }
}
