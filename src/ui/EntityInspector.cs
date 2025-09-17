using ImGuiNET;
using System.Numerics;


namespace LearnOpenTK
{
    public class EntityInspector : UIComponent
    {

        private Game game;


        public EntityInspector(Game game)
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();


            int index = 0;
            foreach (var entity in game.currentLevel.entities)
            {
                if (ImGui.TreeNode($"{entity.name} ##entity{index}"))
                {
                    DrawTransform(entity);


                    if(entity.HasComponent(typeof(Light)))
                    {
                        var light = entity.GetComponent<Light>();

                        if(ImGui.TreeNode($"Light ##{light.GetHashCode()}"))
                        {
                            ImGui.Text("Light yo");

                            ImGui.TreePop();
                        }

                    }

                    //DrawComponents(entity);

                    ImGui.TreePop();
                }
                index++;
            }


            if (ImGui.Button("Add Entity"))
            {
                var entity = new Entity();
                entity.transform = new Transform(OpenTK.Mathematics.Vector3.Zero, OpenTK.Mathematics.Vector3.Zero, OpenTK.Mathematics.Vector3.One);
            }

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

    }


}
