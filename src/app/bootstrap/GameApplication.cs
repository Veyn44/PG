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
        Raylib.InitWindow(1280, 720, "PG - zakladni okno");
        Raylib.SetWindowState(ConfigFlags.HiddenWindow);
        Raylib.ClearWindowState(ConfigFlags.HiddenWindow);
        Raylib.SetTargetFPS(60);
    }

    private static void RunMainLoop()
    {
        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.IsKeyPressed(KeyboardKey.F11))
            {
                Raylib.ToggleBorderlessWindowed();
            }

            Raylib.BeginDrawing();
            Raylib.ClearBackground(new Color(24, 24, 30, 255));
            Raylib.DrawText("PG - zakladni okno", 520, 340, 40, Color.White);
            Raylib.EndDrawing();
        }
    }
}
