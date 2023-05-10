using CommunityToolkit.Mvvm.Messaging;
using MyChatGPT.Models;

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
}