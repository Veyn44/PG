using Raylib_cs;

namespace PG.Presentation.UI;

public sealed class MainMenu
{
    private enum Page
    {
        Main,
        Options,
        DeveloperMode
    }

    private Page _currentPage = Page.Main;
    private string _notice = string.Empty;

    public bool ExitRequested { get; private set; }

    public void Update()
    {
        if (ExitRequested)
        {
            return;
        }

        if (Raylib.IsKeyPressed(KeyboardKey.Escape))
        {
            if (_currentPage == Page.DeveloperMode)
            {
                _currentPage = Page.Options;
            }
            else if (_currentPage == Page.Options)
            {
                _currentPage = Page.Main;
            }
        }

        if (!Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            return;
        }

        var mousePosition = Raylib.GetMousePosition();

        switch (_currentPage)
        {
            case Page.Main:
                HandleMainMenuClick(mousePosition);
                break;

            case Page.Options:
                HandleOptionsClick(mousePosition);
                break;

            case Page.DeveloperMode:
                if (Raylib.CheckCollisionPointRec(mousePosition, BackButton()))
                {
                    _currentPage = Page.Options;
                }
                break;
        }
    }

    public void Draw()
    {
        switch (_currentPage)
        {
            case Page.Main:
                DrawMainMenu();
                break;

            case Page.Options:
                DrawOptions();
                break;

            case Page.DeveloperMode:
                DrawDeveloperMode();
                break;
        }
    }

    private void HandleMainMenuClick(Vector2 mousePosition)
    {
        if (Raylib.CheckCollisionPointRec(mousePosition, MainButton(0)))
        {
            _notice = "New Game is not implemented yet.";
        }
        else if (Raylib.CheckCollisionPointRec(mousePosition, MainButton(1)))
        {
            _notice = "Load Game is not implemented yet.";
        }
        else if (Raylib.CheckCollisionPointRec(mousePosition, MainButton(2)))
        {
            _notice = string.Empty;
            _currentPage = Page.Options;
        }
        else if (Raylib.CheckCollisionPointRec(mousePosition, MainButton(3)))
        {
            ExitRequested = true;
        }
    }

    private void HandleOptionsClick(Vector2 mousePosition)
    {
        if (Raylib.CheckCollisionPointRec(mousePosition, DeveloperModeButton()))
        {
            _currentPage = Page.DeveloperMode;
        }
        else if (Raylib.CheckCollisionPointRec(mousePosition, BackButton()))
        {
            _currentPage = Page.Main;
        }
    }

    private void DrawMainMenu()
    {
        DrawCard(new Rectangle(0, 0, Raylib.GetScreenWidth(), Raylib.GetScreenHeight()));

        DrawCenteredText("PLACEHOLDER GAME", 150, 42, Color.White);
        DrawCenteredText("Main Menu", 205, 20, MutedText);

        string[] labels = ["New Game", "Load Game", "Options", "Exit"];

        for (var i = 0; i < labels.Length; i++)
        {
            DrawButton(MainButton(i), labels[i]);
        }

        if (!string.IsNullOrEmpty(_notice))
        {
            DrawCenteredText(_notice, Raylib.GetScreenHeight() - 70, 16, MutedText);
        }
    }

    private void DrawOptions()
    {
        var centerX = Raylib.GetScreenWidth() / 2f;
        var centerY = Raylib.GetScreenHeight() / 2f;
        var panel = new Rectangle(centerX - 340, centerY - 270, 680, 540);

        DrawCard(panel);
        DrawCenteredText("Options", (int)panel.Y + 28, 34, Color.White);
        DrawCenteredText("General settings will be added here.", (int)panel.Y + 82, 17, MutedText);

        var settingsCard = new Rectangle(panel.X + 35, panel.Y + 125, panel.Width - 70, 100);
        DrawCard(settingsCard);
        DrawTextAt("Settings", settingsCard.X + 22, settingsCard.Y + 18, 20, Color.White);
        DrawTextAt("Display, audio and gameplay settings will be added here.",
            settingsCard.X + 22, settingsCard.Y + 53, 16, MutedText);

        var developerCard = new Rectangle(panel.X + 35, panel.Y + 245, panel.Width - 70, 170);
        DrawCard(developerCard);
        DrawTextAt("Developer Mode", developerCard.X + 22, developerCard.Y + 18, 20, Color.White);
        DrawTextAt("Development and inspection tools.", developerCard.X + 22,
            developerCard.Y + 52, 16, MutedText);
        DrawButton(DeveloperModeButton(), "Open Developer Mode");

        DrawButton(BackButton(), "Back");
    }

    private void DrawDeveloperMode()
    {
        var centerX = Raylib.GetScreenWidth() / 2f;
        var centerY = Raylib.GetScreenHeight() / 2f;
        var panel = new Rectangle(centerX - 300, centerY - 190, 600, 380);

        DrawCard(panel);
        DrawCenteredText("Developer Mode", (int)panel.Y + 55, 32, Color.White);
        DrawCenteredText("Developer tools are not implemented yet.",
            (int)panel.Y + 145, 18, MutedText);
        DrawButton(BackButton(), "Back to Options");
    }

    private static Rectangle MainButton(int index)
    {
        const float buttonWidth = 320;
        const float buttonHeight = 52;
        const float spacing = 14;

        var x = (Raylib.GetScreenWidth() - buttonWidth) / 2f;
        var firstY = Raylib.GetScreenHeight() / 2f - 75;

        return new Rectangle(x, firstY + index * (buttonHeight + spacing),
            buttonWidth, buttonHeight);
    }

    private static Rectangle DeveloperModeButton()
    {
        var centerX = Raylib.GetScreenWidth() / 2f;
        var centerY = Raylib.GetScreenHeight() / 2f;

        return new Rectangle(centerX - 135, centerY + 80, 270, 42);
    }

    private static Rectangle BackButton()
    {
        var centerX = Raylib.GetScreenWidth() / 2f;
        var centerY = Raylib.GetScreenHeight() / 2f;

        return new Rectangle(centerX - 110, centerY + 205, 220, 44);
    }

    private static void DrawCard(Rectangle rectangle)
    {
        Raylib.DrawRectangleRec(rectangle, new Color(25, 27, 34, 245));
        Raylib.DrawRectangleLinesEx(rectangle, 1, new Color(75, 79, 91, 255));
    }

    private static void DrawButton(Rectangle rectangle, string label)
    {
        var hovered = Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), rectangle);
        var background = hovered
            ? new Color(75, 91, 112, 255)
            : new Color(48, 53, 64, 255);

        Raylib.DrawRectangleRec(rectangle, background);
        Raylib.DrawRectangleLinesEx(rectangle, 1, new Color(105, 112, 126, 255));

        var textWidth = Raylib.MeasureText(label, 20);
        var textX = (int)(rectangle.X + (rectangle.Width - textWidth) / 2);
        var textY = (int)(rectangle.Y + (rectangle.Height - 20) / 2);

        Raylib.DrawText(label, textX, textY, 20, Color.White);
    }

    private static void DrawCenteredText(string text, int y, int fontSize, Color color)
    {
        var textWidth = Raylib.MeasureText(text, fontSize);
        var x = (Raylib.GetScreenWidth() - textWidth) / 2;

        Raylib.DrawText(text, x, y, fontSize, color);
    }

    private static void DrawTextAt(string text, float x, float y, int fontSize, Color color)
    {
        Raylib.DrawText(text, (int)x, (int)y, fontSize, color);
    }

    private static Color MutedText => new(170, 176, 188, 255);
}
