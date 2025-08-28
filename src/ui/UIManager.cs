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

        private bool applyFilter = false;
        private bool applyBnwFilter = false;


        private PostProcessingFilter negativeFilter;
        private PostProcessingFilter bnwFilter;



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

            // LEVEL SELECT WINDOW

            ImGui.Begin("Select Level");
            if (ImGui.Button("Level 1"))
                game.ChangeLevel(new FirstLevel(game));

            if (ImGui.Button("Level 2"))
                game.ChangeLevel(new LevelTwo(game));


            if (ImGui.Button("Level 3"))
                game.ChangeLevel(new LevelThree(game));

            ImGui.End();

            ImGui.Begin("Post processing");

            ImGui.Checkbox("Negative filter", ref applyFilter);

            if (applyFilter)
            {
                if (negativeFilter == null)
                    negativeFilter = new PostProcessingFilter(AssetManager.GetShader("negative_filter"), game.ClientSize.X, game.ClientSize.Y);

                game.postProcessor.AddFilter("negative", negativeFilter);
            }
            else
            {
                game.postProcessor.RemoveFilter("negative");
            }


            ImGui.Checkbox("bnw filter", ref applyBnwFilter);
            if (applyBnwFilter)
            {
                if (bnwFilter == null)
                    bnwFilter = new PostProcessingFilter(AssetManager.GetShader("bnw_filter"), game.ClientSize.X, game.ClientSize.Y);

                game.postProcessor.AddFilter("bnw", bnwFilter);
            }
            else
            {
                game.postProcessor.RemoveFilter("bnw");

            }


            ImGui.End();


            ImGui.Begin("Entity Inspector");
            int index = 0;
            foreach (var entity in game.currentLevel.entities)
            {
                if (ImGui.TreeNode($"Entity {index}##entity{index}"))
                {
                    DrawTransform(entity);


                    DrawComponents(entity);

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


        private void DrawTransform(Entity entity)
        {
            if (ImGui.TreeNode($"{entity.transform.GetType().Name}##{entity.transform.GetHashCode()}"))
            {
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

                ImGui.TreePop();
            }


        }

        private void DrawComponents(Entity entity)
        {
            foreach (var component in entity.components.Values)
            {
                if (ImGui.TreeNode($"{component.GetType().Name}##{component.GetHashCode()}"))
                {
                    
                    foreach (var field in component.GetType().GetFields())
                    {

                        if (field.FieldType == typeof(float))
                        {
                            float val = (float)field.GetValue(component);

                            if (ImGui.DragFloat(field.Name, ref val))
                            {
                                field.SetValue(component, val);
                            }

                        }
                        else if (field.FieldType == typeof(OpenTK.Mathematics.Vector3))
                        {
                            var val = (OpenTK.Mathematics.Vector3)field.GetValue(component);

                            Vector3 vector = new Vector3(val.X, val.Y, val.Z);
                            if (ImGui.DragFloat3(field.Name, ref vector))
                            {
                                field.SetValue(component, new OpenTK.Mathematics.Vector3(vector.X, vector.Y, vector.Z));
                            }

                        }
                        else if (field.FieldType == typeof(OpenTK.Mathematics.Color4))
                        {
                            var val = (OpenTK.Mathematics.Color4)field.GetValue(component);

                            Vector4 vector = new Vector4(val.R, val.G, val.B, val.A);
                            if (ImGui.ColorEdit4(field.Name, ref vector))
                            {
                                field.SetValue(component, new OpenTK.Mathematics.Color4(vector.X, vector.Y, vector.Z, vector.W));
                            }

                        }

                    }

                    ImGui.TreePop();
                }
            }
        }
    }
}
