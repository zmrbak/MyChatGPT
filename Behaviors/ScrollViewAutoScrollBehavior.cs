using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChatGPT.Behaviors
{
    public class ScrollViewAutoScrollBehavior : Behavior<StackLayout>
    {
        protected override void OnAttachedTo(StackLayout bindable)
        {
            bindable.SizeChanged += Content_SizeChanged;
            base.OnAttachedTo(bindable);
        }

        private void Content_SizeChanged(object sender, EventArgs e)
        {
            if (sender is StackLayout view)
            {
                if (view.Parent is ScrollView scrollView)
                {
                    scrollView.ScrollToAsync(scrollView.X, view.Height, false);
                }
            }
        }

        protected override void OnDetachingFrom(StackLayout bindable)
        {
            bindable.SizeChanged -= Content_SizeChanged;
            base.OnDetachingFrom(bindable);
        }
    }
}
