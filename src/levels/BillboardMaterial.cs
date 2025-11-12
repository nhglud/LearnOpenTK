using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK.src.levels
{
    public class BillboardMaterial : Material
    {
        private Texture billboardTexture;

        public BillboardMaterial(Texture billboardTexture) : base() {

            this.billboardTexture = billboardTexture;

            shader = new Shader(
               AssetManager.path + "src/shaders/billboard.vert",
               AssetManager.path + "src/shaders/billboard.geom",
               AssetManager.path + "src/shaders/billboard.frag");
        }


        public override void Use(Matrix4 model)
        {
            base.Use(model);

            shader.SetInt("billboardTexture", 0);
            billboardTexture.Use();
        }

    }
}
