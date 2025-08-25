using goo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImGuiNET;
using OpenTK.Windowing.Common;


namespace LearnOpenTK
{
    public  class UIManager
    {
        private ImGuiController uiController;
        private Game game;


        public UIManager(Game game)
        {
            //this.uiController = uiController;
            this.game = game;

            ImGui.CreateContext();
            ImGui.GetIO().Fonts.AddFontDefault();

            uiController = new ImGuiController(game.ClientSize.X, game.ClientSize.Y);

        }

        public void Update(FrameEventArgs e)
        {
            uiController.Update(game, (float)e.Time);

            ImGui.Begin("Select Level");
            if (ImGui.Button("Level 1"))
                game.ChangeLevel(new FirstLevel(game));

            if (ImGui.Button("Level 2"))
                game.ChangeLevel(new LevelTwo(game));


            ImGui.End();



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
