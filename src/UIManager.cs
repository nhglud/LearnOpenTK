using goo;
using ImGuiNET;
using OpenTK.Windowing.Common;
//using OpenTK.Mathematics;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using System.Numerics;

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

            //ImGui.ShowDemoWindow();

            // LEVEL SELECT WINDOW

            ImGui.Begin("Select Level");
            if (ImGui.Button("Level 1"))
                game.ChangeLevel(new FirstLevel(game));

            if (ImGui.Button("Level 2"))
                game.ChangeLevel(new LevelTwo(game));


            ImGui.End();


            ImGui.Begin("Scene Entities");
            int index = 0;
            foreach (var entity in game.currentLevel.entities)
            {
                if (ImGui.TreeNode($"Entity {index}##entity{index}"))
                {
                    ImGui.Text("Transform");

                    Vector3 pos = new Vector3(entity.transform.position.X, entity.transform.position.Y, entity.transform.position.Z);
                    Vector3 rot = new Vector3(entity.transform.rotation.X, entity.transform.rotation.Y, entity.transform.rotation.Z);
                    Vector3 scale = new Vector3(entity.transform.scale.X, entity.transform.scale.Y, entity.transform.scale.Z);

                    if (ImGui.DragFloat3("Position", ref pos))
                    {
                        entity.transform.position = new OpenTK.Mathematics.Vector3(pos.X, pos.Y, pos.Z);
                    }

                    if (ImGui.DragFloat3("Rotation", ref rot))
                    {
                        entity.transform.rotation = new OpenTK.Mathematics.Vector3(rot.X, rot.Y, rot.Z);
                    }

                    if (ImGui.DragFloat3("Scale", ref scale))
                    {
                        entity.transform.scale = new OpenTK.Mathematics.Vector3(scale.X, scale.Y, scale.Z);
                    }


                    // Components
                    foreach (var component in entity.components.Values)
                    {
                        if (ImGui.TreeNode($"{component.GetType().Name}##{component.GetHashCode()}"))
                        {
                            // Later you could add per-component editing here
                            ImGui.TreePop();
                        }
                    }

                    ImGui.TreePop();
                }
                index++;
            }

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
