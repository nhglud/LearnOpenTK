
using LearnOpenTK.src.ui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK
{
    public class MainPanel : UIPanel
    {
        public MainPanel(string title, Game game) : base(title, game) 
        {
            components.Add("LevelSelector", new LevelSelector(game));
            components.Add("TextureSettings", new TextureSettings());
            components.Add("PostProcessingSettings", new PostProcessingSettings(game));
            components.Add("EntityInspector", new EntityInspector(game));
            components.Add("Blood Shooter", new DecalShooter());


        }




    }
}
