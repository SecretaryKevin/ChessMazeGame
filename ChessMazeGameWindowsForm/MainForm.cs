using System;
using System.Windows.Forms;

namespace ChessMazeGameWindowsForm
{
    public partial class MainForm : Form
    {
        private MainMenu mainMenu;
        private GameForm gameForm;

        public MainForm()
        {
            InitializeComponent();
            InitializeForms();
        }

        private void InitializeForms()
        {
            // Initialize MainMenu
            mainMenu = new MainMenu { Name = "mainMenu" };
            mainMenu.Dock = DockStyle.Fill;
            mainMenu.StartGame += MainMenu_StartGame;
            Controls.Add(mainMenu);

            // Initialize GameForm
            gameForm = new GameForm { Name = "gameForm" };
            gameForm.TopLevel = false;
            gameForm.FormBorderStyle = FormBorderStyle.None;
            gameForm.Dock = DockStyle.Fill;
            gameForm.Visible = false;
            Controls.Add(gameForm);
        }

        private void MainMenu_StartGame(object sender, EventArgs e)
        {
            mainMenu.Visible = false;
            gameForm.Visible = true;
            gameForm.Show();
        }
    }
}