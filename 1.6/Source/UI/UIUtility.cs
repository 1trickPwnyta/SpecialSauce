using UnityEngine;
using Verse;

namespace SpecialSauce.UI
{
    public static class UIUtility
    {
        private static void DrawImageTextButton(Rect rect, Texture2D image, string text)
        {
            Widgets.DrawButtonGraphic(rect);
            if (image != null)
            {
                GUI.DrawTexture(rect.LeftPartPixels(rect.height).ContractedBy(6f), image);
            }
            using (new TextBlock(TextAnchor.MiddleLeft)) Widgets.Label(rect.RightPartPixels(rect.width - rect.height).ContractedBy(3f), text);
        }

        public static bool ButtonImageText(Rect rect, Texture2D image, string text)
        {
            DrawImageTextButton(rect, image, text);
            return Widgets.ButtonInvisible(rect);
        }
    }
}
