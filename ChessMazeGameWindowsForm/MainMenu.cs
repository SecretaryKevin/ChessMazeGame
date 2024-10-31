using System;
using System.Windows.Forms;

namespace ChessMazeGameWindowsForm
{
    public partial class MainMenu : UserControl
    {
        public event EventHandler StartGame;

        public MainMenu()
        {
            InitializeComponent();
            InitializeMainMenu();
        }

        private void InitializeMainMenu()
        {
            // Background image with checkerboard style
            this.BackgroundImage = Image.FromFile("../../../Images/backgroundBoard.png");
            this.BackgroundImageLayout = ImageLayout.Stretch;

            // Center the knight image, larger and with no background
            PictureBox knightImage = new PictureBox
            {
                Image = Image.FromFile("../../../Images/black-knight.png"),
                Size = new Size(150, 150), // Increased size
                SizeMode = PictureBoxSizeMode.StretchImage,
                BackColor = Color.Transparent, // No background
                Location = new Point((ClientSize.Width - 150) / 2, 150), // Lowered
                Anchor = AnchorStyles.Top
            };
            this.Controls.Add(knightImage);

            // Main Text "Chess Maze Game" centered without a background
            Label menuText = new Label
            {
                Font = new Font("Arial", 24, FontStyle.Bold),
                Text = "Chess Maze Game",
                AutoSize = true,
                ForeColor = Color.Black,
                BackColor = Color.Transparent, // No background
                TextAlign = ContentAlignment.MiddleCenter, // Center text alignment
                Location = new Point((ClientSize.Width - 200) / 2, knightImage.Bottom + 30) // Centered text
            };
            this.Controls.Add(menuText);

            // New Game button positioned below the title
            Button newGameButton = new Button
            {
                Text = "New Game",
                Size = new Size(100, 30),
                Location = new Point((ClientSize.Width - 100) / 2, menuText.Bottom + 30)
            };
            newGameButton.Click += (_, _) => StartGame?.Invoke(this, EventArgs.Empty);
            this.Controls.Add(newGameButton);

            // Exit button positioned below the New Game button
            Button exitButton = new Button
            {
                Text = "Exit",
                Size = new Size(100, 30),
                Location = new Point((ClientSize.Width - 100) / 2, newGameButton.Bottom + 10)
            };
            exitButton.Click += (_, _) => Application.Exit();
            this.Controls.Add(exitButton);
        }
    }
}