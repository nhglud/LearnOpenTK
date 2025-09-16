using goo;
using ImGuiNET;
using OpenTK.Windowing.Common;
using System.Numerics;

namespace LearnOpenTK
{
    public class UIManager
    {
        private ImGuiController uiController;
        private Game game;

        private MainPanel mainPanel;


        public UIManager(Game game)
        {
            this.game = game;

            ImGui.CreateContext();
            ImGui.GetIO().Fonts.AddFontDefault();

            uiController = new ImGuiController(game.ClientSize.X, game.ClientSize.Y);

            mainPanel = new MainPanel("Main Panel", this.game);

        }

        public void Update(FrameEventArgs e)
        {
            uiController.Update(game, (float)e.Time);

            mainPanel.DrawPanel();

        }

        public void Render()
        {
            uiController.Render();
            ImGuiController.CheckGLError("End of frame");

        }


        public void Resize(FramebufferResizeEventArgs e)
        {
            uiController.WindowResized(e.Width, e.Height);
        }

    }
}
