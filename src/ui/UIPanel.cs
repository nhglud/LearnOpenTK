using ImGuiNET;


namespace LearnOpenTK
{
    public class UIPanel
    {
        private string title;

        protected Dictionary<string, UIComponent> components;

        private Game game;

        public UIPanel(string title, Game game) 
        {
            this.title = title;
            this.game = game;
            components = new Dictionary<string, UIComponent>();

            

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
