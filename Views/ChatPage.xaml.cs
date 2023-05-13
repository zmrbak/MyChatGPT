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
    /// ���ɵ����ʣ����͵������
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
    /// ���˴𰸸��Ƶ������壬����������ʹ��
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>

    private async void TapGestureRecognizer_Tapped2(object sender, TappedEventArgs e)
    {
        await Clipboard.SetTextAsync((sender as Label).Text);
        await DisplayAlert("Alert", "�˴��Ѿ����Ƶ������壬��ճ�������������ʹ�á�", "OK");
    }
}