using ImGuiNET;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnOpenTK
{
    public class PostProcessingSettings : UIComponent
    {

        private bool applyFilter = false;
        private bool applyBnwFilter = false;
        private bool applyToonFilter = false;


        private PostProcessingFilter negativeFilter;
        private PostProcessingFilter bnwFilter;
        private PostProcessingFilter toonFilter;


        private Game game;


        public PostProcessingSettings(Game game) : base() 
        { 
            this.game = game;
        }

        public override void Render()
        {
            base.Render();

            ImGui.Checkbox("Negative filter", ref applyFilter);

            if (applyFilter)
            {
                if (negativeFilter == null)
                    negativeFilter = new PostProcessingFilter(AssetManager.GetShader("negative_filter"), game.ClientSize.X, game.ClientSize.Y);

                game.postProcessor.AddFilter("negative", negativeFilter);
            }
            else
            {
                game.postProcessor.RemoveFilter("negative");
            }


            ImGui.Checkbox("bnw filter", ref applyBnwFilter);
            if (applyBnwFilter)
            {
                if (bnwFilter == null)
                    bnwFilter = new PostProcessingFilter(AssetManager.GetShader("bnw_filter"), game.ClientSize.X, game.ClientSize.Y);

                game.postProcessor.AddFilter("bnw", bnwFilter);
            }
            else
            {
                game.postProcessor.RemoveFilter("bnw");

            }


            ImGui.Checkbox("toon filter", ref applyToonFilter);
            if (applyToonFilter)
            {
                if (toonFilter == null)
                    toonFilter = new PostProcessingFilter(AssetManager.GetShader("toon_filter"), game.ClientSize.X, game.ClientSize.Y);

                game.postProcessor.AddFilter("toon", toonFilter);
            }
            else
            {
                game.postProcessor.RemoveFilter("toon");

            }



        }


    }
}
