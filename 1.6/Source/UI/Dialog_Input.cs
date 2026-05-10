using RimWorld;
using System;
using UnityEngine;
using Verse;

namespace SpecialSauce.UI
{
    public class Dialog_Input<T> : Window
    {
        private readonly string prompt;
        private string input;
        private readonly Action<T> confirmAction;
        private readonly Func<string, T> parser;
        private readonly Func<T, AcceptanceReport> validator;

        public Dialog_Input(string prompt, Action<T> confirmAction, Func<string, T> parser, T defaultValue, Func<T, AcceptanceReport> validator = null)
        {
            this.prompt = prompt;
            this.confirmAction = confirmAction;
            this.parser = parser;
            this.validator = validator;
            input = defaultValue.ToString();
            doCloseX = true;
            forcePause = true;
            closeOnAccept = false;
            closeOnClickedOutside = true;
            absorbInputAroundWindow = true;
        }

        public override Vector2 InitialSize => new Vector2(480f, 175f);

        public override void DoWindowContents(Rect inRect)
        {
            Rect promptRect = new Rect(inRect);
            Text.Font = GameFont.Medium;
            promptRect.height = Text.LineHeight + 10f;
            Widgets.Label(promptRect, prompt);
            Text.Font = GameFont.Small;

            GUI.SetNextControlName("InputField");
            Rect textRect = new Rect(0f, promptRect.yMax, inRect.width, 35f);
            input = Widgets.TextField(textRect, input);
            Verse.UI.FocusControl("InputField", this);

            Rect buttonRect = new Rect(15f, inRect.height - 35f - 10f, inRect.width - 15f - 15f, 35f);
            if (Widgets.ButtonText(buttonRect, "OK"))
            {
                T result = parser(input);
                AcceptanceReport acceptanceReport = validator != null ? validator(result) : AcceptanceReport.WasAccepted;
                if (!acceptanceReport.Accepted)
                {
                    if (acceptanceReport.Reason.NullOrEmpty())
                    {
                        Messages.Message("SpecialSauce_InputIsInvalid".Translate(), MessageTypeDefOf.RejectInput, false);
                        return;
                    }
                    Messages.Message(acceptanceReport.Reason, MessageTypeDefOf.RejectInput, false);
                    return;
                }
                else
                {
                    Find.WindowStack.TryRemove(this);
                    confirmAction(result);
                }
            }
        }
    }
}
