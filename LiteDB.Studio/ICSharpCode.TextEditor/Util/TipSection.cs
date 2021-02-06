// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="none" email=""/>
//     <version>$Revision$</version>
// </file>

using System;
using System.Diagnostics;
using System.Drawing;

namespace LiteDB.Studio.ICSharpCode.TextEditor.Util
{
    internal abstract class TipSection
    {
        private SizeF tipMaxSize;
        private SizeF tipRequiredSize;

        protected TipSection(Graphics graphics)
        {
            Graphics = graphics;
        }

        protected Graphics Graphics { get; }

        protected SizeF AllocatedSize { get; private set; }

        protected SizeF MaximumSize => tipMaxSize;

        public abstract void Draw(PointF location);

        public SizeF GetRequiredSize()
        {
            return tipRequiredSize;
        }

        public void SetAllocatedSize(SizeF allocatedSize)
        {
            Debug.Assert(allocatedSize.Width >= tipRequiredSize.Width &&
                         allocatedSize.Height >= tipRequiredSize.Height);

            AllocatedSize = allocatedSize;
            OnAllocatedSizeChanged();
        }

        public void SetMaximumSize(SizeF maximumSize)
        {
            tipMaxSize = maximumSize;
            OnMaximumSizeChanged();
        }

        protected virtual void OnAllocatedSizeChanged()
        {
        }

        protected virtual void OnMaximumSizeChanged()
        {
        }

        protected void SetRequiredSize(SizeF requiredSize)
        {
            requiredSize.Width = Math.Max(0, requiredSize.Width);
            requiredSize.Height = Math.Max(0, requiredSize.Height);
            requiredSize.Width = Math.Min(tipMaxSize.Width, requiredSize.Width);
            requiredSize.Height = Math.Min(tipMaxSize.Height, requiredSize.Height);

            tipRequiredSize = requiredSize;
        }
    }
}