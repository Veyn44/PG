using PG.Presentation.UI;
using Raylib_cs;

namespace PG.App.Bootstrap;

public sealed class GameApplication
{
    public void Run()
    {
        InitializeWindow();

        try
        {
            RunMainLoop();
        }
        finally
        {
            Raylib.CloseWindow();
        }
    }

    private static void InitializeWindow()
    {
        Raylib.SetConfigFlags(ConfigFlags.ResizableWindow);
        Raylib.InitWindow(1280, 720, "PG");
        Raylib.SetTargetFPS(60);
    }

    private static void RunMainLoop()
    {
        var mainMenu = new MainMenu();

        while (!Raylib.WindowShouldClose() && !mainMenu.ExitRequested)
        {
            mainMenu.Update();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(18, 20, 26, 255));
            mainMenu.Draw();
            Raylib.EndDrawing();
        }
    }
}
