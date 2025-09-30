using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK.src.ui
{
    public class DecalShooter : UIComponent
    {

       

        public override void Render()
        {
            base.Render();

            if(ImGui.Button("Blood!"))
            {

                Decal decal = new Decal();

                decal.direction = Camera.main.front;
                decal.position = Camera.main.transform.position;

                LitMaterial.decals.Add(decal);

            }


        }

    }




}
