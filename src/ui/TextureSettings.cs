using ImGuiNET;


namespace LearnOpenTK
{
    public class TextureSettings : UIComponent
    {
        private int selectedIndex = 0;
        private readonly string[] options = Enum.GetNames(typeof(TextureFilter));

        public override void Render()
        {
            base.Render();

            ImGui.Text("Texture Filter Settings");

            if(ImGui.Combo("Texture filter", ref selectedIndex, options, options.Length ))
            {
                TextureFilter filter = (TextureFilter)selectedIndex;

                Texture.ApplyTextureFilter(filter);
                Console.WriteLine(selectedIndex);
            }

        }

    }
}
