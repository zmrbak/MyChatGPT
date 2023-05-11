using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MyChatGPT.Models;
using OpenAI;
using OpenAI.Chat;
using System.Collections.ObjectModel;
using System.Security.Authentication;

namespace MyChatGPT.ViewModel
{
    public partial class ChatViewModel : ObservableObject
    {
        private bool questionChanged = true;
        private bool CanAnswerSubmit() => questionChanged;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string question = "hello";
        partial void OnQuestionChanged(string value)
        {
            questionChanged = true;
        }

        [RelayCommand(CanExecute = nameof(CanAnswerSubmit))]
        private async Task SubmitAsync()
        {
            if (Question.Length == 0) return;
            var chat = new Chat();
            chat.Question = Question.Trim();
            chat.Answer = "";
            Chats.Add(chat);

            Question = "";

            try
            {
                HttpClientHandler clientHandler = new HttpClientHandler();
                clientHandler.ServerCertificateCustomValidationCallback += (sender, cert, chain, sslPolicyErrors) => { return true; };
                clientHandler.SslProtocols = SslProtocols.None;
                var httpClient = new HttpClient(clientHandler);

                //var auth = new OpenAIAuthentication($"sess-aAbBcCdDeE123456789");
                //var settings = new OpenAIClientSettings(domain: "www.qqq.com");
                var auth = new OpenAIAuthentication(GlobalSettings.OpenAiKey);
                var settings = new OpenAIClientSettings(domain: GlobalSettings.OpenAiServer);
                var openAIClient = new OpenAIClient(auth, settings, httpClient);

                var messages = new List<Message>
                {
                    new Message(Role.System, "You are a helpful assistant."),
                };
                foreach (var item in Chats.TakeLast(6))
                {
                    messages.Add(new Message(Role.User, item.Question));
                    if (item.Answer != "")
                    {
                        messages.Add(new Message(Role.Assistant, item.Answer));
                    }
                }

                string oldAnswer = "";
                var chatRequest = new ChatRequest(messages);
                await openAIClient.ChatEndpoint.StreamCompletionAsync(chatRequest, result =>
                {
                    var firstChoice = result.FirstChoice.ToString();
                    oldAnswer += firstChoice;

                    //剔除重复
                    var len = oldAnswer.Length;
                    if ((len % 2 == 0) && (oldAnswer.Substring(0, len / 2) == oldAnswer.Substring(len / 2, len / 2)))
                    {
                        chat.Answer = oldAnswer.Substring(0, len / 2);
                    }
                    else
                    {
                        //chat.Answer = null;
                        chat.Answer += firstChoice;
                    }
                });
                questionChanged = false;
            }
            catch (Exception ex)
            {
                WeakReferenceMessenger.Default.Send(new AlertMessager(ex.Message));//发布消息
            }
        }

        [RelayCommand]
        private void ClearAll()
        {
            Chats.Clear();
            questionChanged = true;
        }

        [ObservableProperty]
        ObservableCollection<Chat> chats = new();
    }
}
