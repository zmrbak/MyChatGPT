using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChatGPT.EventTriggers
{
    /// <summary>
    /// 为文本框设置一个旋转的动画
    /// </summary>
    public class LabelRotateAnimation : TriggerAction<Label>
    {
        protected override async void Invoke(Label label)
        {
            //if (label == null && label.Text == null) return;
            if (label.Text.Length == 0) label.Text = "☆";

            while (label.IsVisible == true)
            {
                await label.RelRotateTo(360, 1000);
            }
        }
    }
}
