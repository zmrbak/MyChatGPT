using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyChatGPT.Models
{
    public partial class Chat : ObservableObject
    {
        [ObservableProperty]
        public string question;
        [ObservableProperty]
        public string answer;
    }
}
