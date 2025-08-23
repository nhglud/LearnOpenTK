//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

using Assimp;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;


namespace LearnOpenTK
{
    internal class LevelTwo : Level
    {
        public LevelTwo(Game game) : base(game)
        { 


        }

        LightingSystem lightingSystem;

        public override void LoadLevel()
        {
            base.LoadLevel();

            Entity monke = new Entity();

            monke.transform = new Transform(Vector3.Zero, Vector3.Zero, Vector3.One);
            monke.AddComponent<Mesh>(AssetManager.GetMesh("monke"));
            monke.AddComponent<Renderer>(new Renderer(AssetManager.GetMaterial("container2_mat")));


            Entity lightEntity = new Entity();
            lightEntity.transform = new Transform(new Vector3(0.5f, 3.0f, 2.0f), Vector3.Zero, 0.2f * Vector3.One);

            lightEntity.AddComponent(AssetManager.GetMesh("cube"));
            lightEntity.AddComponent(new Renderer(new UnlitMaterial(Vector3.One)));
            lightEntity.AddComponent(new PointLight(Vector3.One));


            Entity player = new Entity();
            player.transform = new Transform(new Vector3(0.0f, 0.0f, 3.0f), new Vector3(0.0f), new Vector3(1.0f));
            player.AddComponent(new Camera());
            player.AddComponent(new CharacterController(game.KeyboardState, game.MouseState));

            entities.Add(monke);
            entities.Add(player);
            entities.Add(lightEntity);

            lightingSystem = new LightingSystem(new List<PointLight>() { lightEntity.GetComponent<PointLight>() });

        }

        public override void UpdateLevel(FrameEventArgs e)
        {
            base.UpdateLevel(e);
            
            CharacterController.UpdateComponents(e);
        }

        public override void RenderLevel(FrameEventArgs e)
        {
            base.RenderLevel(e);

            Camera.main.UpdateUBO(game.ClientSize.X, game.ClientSize.Y);
            
            lightingSystem.Update();
            Renderer.UpdateRenderers();
        
        }




    }
}
