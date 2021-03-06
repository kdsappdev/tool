﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Northwoods.Go;

namespace MDT.Tools.Server.Monitor.Plugin
{
    [Serializable]
    public class AnimatedLink : GoLink
    {
        public string Text { get; set; }

        public AnimatedLink()
        {
            this.Reshapable = false;
            this.HighlightWhenSelected = true;
            this.HighlightPenColor = Color.Chartreuse;
            this.HighlightPenWidth = 5;
            this.PenColor = Color.White;
            this.PenWidth = 2;
            this.BrushColor = Color.White;
            this.ToArrow = true;
        }

        public override void Paint(Graphics g, GoView view)
        {
            base.Paint(g, view);
            GoStroke s = this;
            if (mySeg >= s.PointsCount - 1)
                mySeg = 0;
            PointF a = s.GetPoint(mySeg);
            PointF b = s.GetPoint(mySeg + 1);
            float len = (float)Math.Sqrt((b.X - a.X) * (b.X - a.X) + (b.Y - a.Y) * (b.Y - a.Y));
            float x = b.X;
            float y = b.Y;
            if (myDist >= len)
            {
                mySeg++;
                myDist = 0;
            }
            else if (len >= 1)
            {
                x = a.X + (b.X - a.X) * myDist / len;
                y = a.Y + (b.Y - a.Y) * myDist / len;
            }
            GoShape.DrawEllipse(g, view, null, Brushes.Red, x - 3, y - 3, 7, 7);
        }
        public void Step()
        {
            myDist += 3;
            this.InvalidateViews();
        }

        [NonSerialized]
        private int mySeg = 0;
        [NonSerialized]
        private float myDist = 0;
    }
}
