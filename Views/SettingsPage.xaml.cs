using MyChatGPT.Models;

namespace MyChatGPT.Views;


public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        this.Loaded += SettingsPage_Loaded;
    }

    private void SettingsPage_Loaded(object sender, EventArgs e)
    {
        this.openAiServer.Text = GlobalSettings.OpenAiServer;
        this.openAiKey.Text = GlobalSettings.OpenAiKey;
        this.userPassword.Text = GlobalSettings.UserPassword;
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        GlobalSettings.OpenAiServer = this.openAiServer.Text;
        GlobalSettings.OpenAiKey = this.openAiKey.Text;
        GlobalSettings.UserPassword = this.userPassword.Text;
        GlobalSettings.SaveOpenAiSettings();

        await DisplayAlert("信息", "保存配置成功！", "OK");
        await Shell.Current.GoToAsync("//ChatPage");
    }

    private async void Button_Clicked_1(object sender, EventArgs e)
    {
        SettingsPage_Loaded(sender, e);
        await Shell.Current.GoToAsync("//ChatPage");
    }
}