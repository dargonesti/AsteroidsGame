namespace AsteroidsPrototype.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Microsoft.Xna.Framework;
    using DisplaySpring;
    using VAlign = DisplaySpring.Item.VerticalAlignmentType;
    using HAlign = DisplaySpring.Item.HorizontalAlignmentType;
    using Layout = DisplaySpring.Frame.LayoutType;
    using AsteroidsPrototype.GameManager;

    /// <summary>
    /// Menu de chois de level
    /// </summary>
    class ChooseLevelMenu : Menu
    {
        ScrollList sl;
        GameManager manager;

        /// <summary>
        /// Sample Button Scroll List Menu
        /// </summary>
        public ChooseLevelMenu(MultiController controllers, List<Controller> allControllers, Rectangle bounds, GameManager pmanager)
            : base(controllers, bounds)
        {
            manager = pmanager;
            BaseFrame.Layout = Layout.None;

            //Create a title, and scale the text so it is a little bigger
            new Label(BaseFrame, "List of levels") 
            { 
                Scale = new Vector2(2f, 2f),
                  FontColor = Color.Gold ,
                  VerticalAlignment = VAlign.Top
            };

            sl = new ScrollList(BaseFrame, 10)
            {
                Direction = ScrollList.Orientation.Horizontal,
                LayoutStretch = 5,
                ScrollPosition = ScrollList.ScrollBarPosition.Left, //left means top for horizontal orientation
                HorizontalAlignment = HAlign.Center
            };

            createScrollList(sl);
           /* ScrollList tsl = createScrollList(sl); tsl.Scale *= 2f;
            tsl = createScrollList(sl); tsl.Scale *= 0.5f;
            tsl = createScrollList(sl);
            tsl = createScrollList(sl); tsl.Scale *= 2f;
            */
            Reset();
        }

        ScrollList createScrollList(Item parent)
        {
            ScrollList sl = new ScrollList(parent);
            sl.ViewCount = 10;

            for (int i = 0; i < manager._lstLevels.Count; i++)
            {
                int noLevel = i;
                Button btn = new Button(sl, App.Button, App.menuButtonHighlighted, manager._lstLevels[noLevel].GetName() /* "Level " + (noLevel + 1)*/) { Scale = new Vector2(1f, 1f) };
                btn.OnA = delegate() { manager.nivCourant = manager._lstLevels[noLevel];
                this.Close();
                };
             }
            return sl;
        }

        /// <summary>
        /// The reset button will provide a way to set focus to a button when changing
        /// to and from sub menus. It is best to override and implement this function
        /// </summary>
        public override void Reset()
        {
            base.Reset(sl);
        }
    }
}

