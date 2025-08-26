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
    public class LevelTwo : Level
    {
        private LightingSystem lightingSystem;
        private RenderingSystem renderingSystem;
        private UpdateSystem updateSystem;


        public LevelTwo(Game game) : base(game)
        { 

        }



        public override void LoadLevel()
        {
            base.LoadLevel();

            Entity monke = new Entity();

            monke.transform = new Transform(Vector3.Zero, Vector3.Zero, Vector3.One);
            monke.AddComponent(AssetManager.GetMesh("monke"));
            monke.AddComponent(new Renderer(AssetManager.GetMaterial("container2_mat")));

            Entity torus = new Entity();

            torus.transform = new Transform(3f * Vector3.UnitX, Vector3.Zero, Vector3.One);
            torus.AddComponent(AssetManager.GetMesh("torus"));
            torus.AddComponent(new Renderer(AssetManager.GetMaterial("container2_mat")));



            Entity floor = new Entity();

            floor.transform = new Transform(-1.5f * Vector3.UnitY, Vector3.Zero, new Vector3(8f, 1f, 8f));
            floor.AddComponent(AssetManager.GetMesh("quad"));
            floor.AddComponent(new Renderer(AssetManager.GetMaterial("container2_mat")));

            //Entity lightEntity = new Entity();
            //lightEntity.transform = new Transform(new Vector3(6.5f, 3.0f, 2.0f), Vector3.Zero, 0.2f * Vector3.One);

            //lightEntity.AddComponent(AssetManager.GetMesh("cube"));
            //lightEntity.AddComponent(new Renderer(new UnlitMaterial(Vector3.One)));
            //lightEntity.AddComponent(new PointLight(Color4.White));


            Entity spotLightEntity = new Entity();
            spotLightEntity.transform = new Transform(1.5f * Vector3.UnitY, Vector3.Zero,  0.2f * Vector3.One);

            //spotLightEntity.AddComponent(new SpotLight(Vector3.UnitX, (float)Math.Cos(10.0), (float)Math.Cos(15.0), -Vector3.UnitY));
            spotLightEntity.AddComponent(new SpotLight(Color4.Red, 10.0f, 15.0f, -Vector3.UnitY));

            spotLightEntity.AddComponent(AssetManager.GetMesh("cube"));
            spotLightEntity.AddComponent(new Renderer(new UnlitMaterial(Vector3.One)));

            Entity player = new Entity();
            player.transform = new Transform(new Vector3(0.0f, 0.0f, 3.0f), new Vector3(0.0f), new Vector3(1.0f));
            player.AddComponent(new Camera());
            player.AddComponent(new CharacterController(game.KeyboardState, game.MouseState));

            entities.Add(monke);
            entities.Add(floor);
            entities.Add(player);
            entities.Add(torus);

            //entities.Add(lightEntity);
            entities.Add(spotLightEntity);

            lightingSystem = new LightingSystem(entities);
            renderingSystem = new RenderingSystem(entities);
            updateSystem = new UpdateSystem(entities);

        }

        public override void UpdateLevel(FrameEventArgs e)
        {
            base.UpdateLevel(e);

            updateSystem.Update(e);

        }

        public override void RenderLevel(FrameEventArgs e)
        {
            base.RenderLevel(e);

            Camera.main.UpdateUBO(game.ClientSize.X, game.ClientSize.Y);
            
            lightingSystem.Update();
            renderingSystem.Render();
        
        }




    }
}
