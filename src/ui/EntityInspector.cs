using ImGuiNET;
using LearnOpenTK.src;
using LearnOpenTK.src.shaders;
using System.Numerics;


namespace LearnOpenTK
{
    public class EntityInspector : UIComponent
    {

        private Game game;

        private bool wireframeOn = false;
        private bool normalVizOn = false;

        private float heightScale = 60.0f;
        private float noiseScale = 1.0f;
        private float noiseThreshold = 0.5f;

        private float windSpeed = 0.5f;
        private float windForce = 0.5f;
        private float windScale = 1.0f;


        private TerrainMaterial? tmat = null;
        private TerrainBillboardMaterial? tbmat = null;

        public EntityInspector(Game game)
        {
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

            ImGui.Text("Debug Visualization");


            if (ImGui.Checkbox("Wireframe", ref wireframeOn))
            {

            }

            if (ImGui.Checkbox("Visualize Normals", ref normalVizOn))
            {

            }

            ImGui.Text("Entity Inspector");

            int index = 0;
            foreach (var entity in game.currentLevel.entities)
            {

                if (entity.HasComponent(typeof(Renderer)))
                {
                    ToggleDebugViz(entity.GetComponent<Renderer>());
                }

                if (ImGui.TreeNode($"{entity.name} ##entity{index}"))
                {
                    DrawTransform(entity);


                    if (entity.HasComponent(typeof(PointLight)))
                    {
                        DrawLight<PointLight>(entity);
                    }
                    else if (entity.HasComponent(typeof(SpotLight)))
                    {
                        DrawLight<SpotLight>(entity);

                    }
                    else if (entity.HasComponent(typeof(DirectionalLight)))
                    {
                        DrawLight<DirectionalLight>(entity);

                    }

                    if (entity.HasComponent(typeof(Renderer)))
                    {

                        var renderer = entity.GetComponent<Renderer>();

                        DrawTerrain(renderer);                   
                    }


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

        private void DrawLight<T>(Entity entity) where T : Light
        {
            var light = entity.GetComponent<T>();
            var color = new Vector3(light.lightColor.R, light.lightColor.G, light.lightColor.B);

            if (ImGui.TreeNode($"Light ##{light.GetHashCode()}"))
            {
                if (ImGui.ColorPicker3("Light color", ref color))
                {
                    light.lightColor.R = color.X;
                    light.lightColor.G = color.Y;
                    light.lightColor.B = color.Z;

                }

                ImGui.TreePop();
            }
        }

        private void ToggleDebugViz(Renderer renderer)
        {
            if (wireframeOn && !renderer.materials.Contains(AssetManager.GetMaterial("wireframe")))
            {
                renderer.AddMaterial(AssetManager.GetMaterial("wireframe"));

            }
            else if (!wireframeOn && renderer.materials.Contains(AssetManager.GetMaterial("wireframe")))
            {
                renderer.RemoveMaterial(AssetManager.GetMaterial("wireframe"));

            }

            if (normalVizOn && !renderer.materials.Contains(AssetManager.GetMaterial("normal")))
            {
                renderer.AddMaterial(AssetManager.GetMaterial("normal"));

            }
            else if (!normalVizOn && renderer.materials.Contains(AssetManager.GetMaterial("normal")))
            {
                renderer.RemoveMaterial(AssetManager.GetMaterial("normal"));

            }
        }

        private void DrawTerrain(Renderer renderer)
        {


            if (tmat == null)
            {
                foreach (var mat in renderer.materials)
                {
                    if (mat.GetType() == typeof(TerrainMaterial))
                    {
                        tmat = (TerrainMaterial)mat;
                        //heightScale = tmat.heightScale;
                        ImGui.Text("Terrain");

                    }
                    else if (mat.GetType() == typeof(TerrainBillboardMaterial))
                    {
                        tbmat = (TerrainBillboardMaterial)mat;
                    }

                }

            }

            if (tmat != null && tbmat != null)
            {
                if (ImGui.DragFloat("HeightScale", ref heightScale))
                {
                    tmat.heightScale = heightScale;
                    tbmat.heightScale = heightScale;

                }

                if (ImGui.SliderFloat("NoiseScale", ref noiseScale, 0, 100.0f))
                {

                    tbmat.noiseScale = noiseScale;

                }

                if (ImGui.SliderFloat("NoiseThreshold", ref noiseThreshold, 0, 1.0f))
                {

                    tbmat.noiseThreshold = noiseThreshold;

                }

                if (ImGui.SliderFloat("windSpeed", ref windSpeed, 0, 1.0f))
                {

                    tbmat.windSpeed = windSpeed;

                }


                if (ImGui.SliderFloat("windForce", ref windForce, 0, 1.0f))
                {

                    tbmat.windForce = windForce;

                }


                if (ImGui.SliderFloat("windScale", ref windScale, 0, 10.0f))
                {

                    tbmat.windScale = windScale;

                }

            }


        }


    }


}
