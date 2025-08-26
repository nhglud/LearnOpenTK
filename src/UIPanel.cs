using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK
{
    public class UIPanel
    {
        private string title;

        private Dictionary<string, UIComponent> components;

        public UIPanel() 
        { 
            


        }

        public void DrawPanel()
        {

            ImGui.Begin(title);

            foreach (var component in components.Values)
            {
                component.Render();
            }

            ImGui.End();

        }



    }
}
