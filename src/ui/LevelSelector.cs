using ImGuiNET;
using LearnOpenTK.src.levels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK
{
    public class LevelSelector : UIComponent
    {

        private Game game;
        public LevelSelector(Game game) 
        { 
            this.game = game;
        }


        public override void Render()
        {
            base.Render();


            ImGui.Text("Select Scene");

            if (ImGui.Button("Level 1"))
                game.ChangeLevel(new FirstLevel(game));

            if (ImGui.Button("Level 2"))
                game.ChangeLevel(new LevelTwo(game));


            if (ImGui.Button("Level 3"))
                game.ChangeLevel(new LevelThree(game));

            if (ImGui.Button("Terrain"))
                game.ChangeLevel(new TerrainLevel1(game));

        }

    }
}
