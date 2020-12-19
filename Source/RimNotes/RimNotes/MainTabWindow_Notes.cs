using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RimWorld;
using UnityEngine;
using Verse;

using static RimNotes.Constants;

namespace RimNotes
{
    public enum SourceType
    {
        Colonists,
        Animals,
        Prisoners,
        Visitors,
        Hostiles
    }
    public class MainTabWindow_Notes : MainTabWindow
    {
        private SourceType _source = SourceType.Colonists;

        #region Constructors

        static MainTabWindow_Notes()
        {
            throw new NullReferenceException(" This is the static constructor");
        }
        public MainTabWindow_Notes()
        {
            Instance = this;
        }
        public SourceType Source
        {
            get => _source;
            private set
            {
                _source = value;
            }
        }
        #endregion Constructors
        public static MainTabWindow_Notes Instance { get; private set; }



        public override void DoWindowContents(Rect rect)
        {

            DoSourceSelectionButton(new Rect(rect.xMin, rect.yMin, 120f, 30f));
            base.DoWindowContents(rect);
        }

        public void DoSourceSelectionButton(Rect rect)
        {
            // apparently, font size going to tiny on fully zooming in is working as designed...
            Text.Font = GameFont.Small;
            if (Widgets.ButtonText(rect, Source.ToString().Translate()))
            {
                var options = new List<FloatMenuOption>();

                foreach (var sourceOption in Enum.GetValues(typeof(SourceType)).OfType<SourceType>())
                    if (sourceOption != Source)
                        options.Add(new FloatMenuOption(sourceOption.ToString().Translate(),
                            delegate { Source = sourceOption; }));

                Find.WindowStack.Add(new FloatMenu(options));
            }
        }
    }
}
