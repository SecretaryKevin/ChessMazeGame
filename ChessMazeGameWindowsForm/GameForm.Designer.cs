// In GameForm.Designer.cs
using System.ComponentModel;

namespace ChessMazeGameWindowsForm;

partial class GameForm
{
    private System.Windows.Forms.Panel BoardPanel;
    private System.Windows.Forms.Button undoButton;
    private System.Windows.Forms.TextBox moveHistoryTextBox;
    private System.Windows.Forms.Button backButton;
    private System.Windows.Forms.Label moveHistoryLabel;
    private System.Windows.Forms.Label moveCountLabel;

    private IContainer components = null;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        this.undoButton = new System.Windows.Forms.Button();
        this.BoardPanel = new System.Windows.Forms.Panel();
        this.components = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize = new System.Drawing.Size(1280, 720);
        this.Text = "Chess Maze Game";
        this.moveHistoryTextBox = new System.Windows.Forms.TextBox();
        this.backButton = new System.Windows.Forms.Button();

        this.moveHistoryLabel = new System.Windows.Forms.Label();
        this.moveHistoryLabel.Text = "Move History";
        this.moveHistoryLabel.Location = new System.Drawing.Point(1070, 10);
        this.moveHistoryLabel.Size = new System.Drawing.Size(200, 50);
        this.moveHistoryLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.moveHistoryLabel.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);

        this.moveCountLabel = new System.Windows.Forms.Label();
        this.moveCountLabel.Text = "Move Count: 0";
        this.moveCountLabel.Location = new System.Drawing.Point(1070, 620);
        this.moveCountLabel.Size = new System.Drawing.Size(200, 50);
        this.moveCountLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
        this.moveCountLabel.Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold);

        this.undoButton.Location = new System.Drawing.Point(1070, 660);
        this.undoButton.Name = "undoButton";
        this.undoButton.Size = new System.Drawing.Size(200, 50);
        this.undoButton.TabIndex = 1;
        this.undoButton.Text = "Undo";
        this.undoButton.UseVisualStyleBackColor = true;
        this.undoButton.Click += new System.EventHandler(this.UndoButton_Click);

        this.backButton = new System.Windows.Forms.Button();
        this.backButton.Text = "Back";
        this.backButton.Location = new System.Drawing.Point(10, 10);
        this.backButton.Size = new System.Drawing.Size(200, 50);
        this.backButton.Click += new System.EventHandler(this.BackButton_Click);

        this.moveHistoryTextBox = new System.Windows.Forms.TextBox();
        this.moveHistoryTextBox.Multiline = true;
        this.moveHistoryTextBox.ReadOnly = true;
        this.moveHistoryTextBox.BackColor = System.Drawing.Color.White;
        this.moveHistoryTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
        this.moveHistoryTextBox.Location = new System.Drawing.Point(1070, 70);
        this.moveHistoryTextBox.Size = new System.Drawing.Size(200, 540);

        // Set Anchor properties
        this.moveHistoryTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.moveHistoryLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.moveCountLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        this.undoButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        this.backButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;
        this.BoardPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

        this.Controls.Add(this.moveHistoryTextBox);
        this.Controls.Add(this.BoardPanel);
        this.Controls.Add(this.undoButton);
        this.Controls.Add(this.backButton);
        this.Controls.Add(this.moveHistoryLabel);
        this.Controls.Add(this.moveCountLabel);
    }
}