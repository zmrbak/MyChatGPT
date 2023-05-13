using CommunityToolkit.Mvvm.Messaging;
using MyChatGPT.Models;
using MyChatGPT.ViewModel;

namespace MyChatGPT.Views;

public partial class ChatPage : ContentPage
{
    public ChatPage()
    {
        InitializeComponent();
        WeakReferenceMessenger.Default.Register<AlertMessager>(this, async (r, m) =>
        {
            await ShowHiMessage(m.Value);
        });
    }

    private async Task ShowHiMessage(string message)
    {
        await DisplayAlert("Alert", message, "OK");
    }

    /// <summary>
    /// 将旧的提问，发送到输入框
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private  void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
    {
        if(this.BindingContext is ChatViewModel chatViewModel)
        {
            chatViewModel.Question += (sender as Label).Text;
        }
    }
    /// <summary>
    /// 将此答案复制到剪贴板，供其它程序使用
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private async void TapGestureRecognizer_Tapped2(object sender, TappedEventArgs e)
    {
        await Clipboard.SetTextAsync((sender as Label).Text);
        await DisplayAlert("Alert", "此答案已经复制到剪贴板，可粘贴到其它软件中使用。", "OK");
    }
}