using System;
using System.Threading.Tasks;

//using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Builder.Luis;
using Microsoft.Bot.Builder.Luis.Models;
using Microsoft.Bot.Connector;
using Microsoft.Bot.Connector;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;

// For more information about this template visit http://aka.ms/azurebots-csharp-qnamaker
namespace MultiDialogsBot.Dialogs
{
    [Serializable]
    [LuisModel("89e36123-823b-413a-8754-c838f378f758", "96647e513e3a43abad18c99343e8c124")]
    public class LuisDialog : LuisDialog<object>
    {
        private string name;

        [LuisIntent("")]
        [LuisIntent("None")]
        public async Task None(IDialogContext context, LuisResult result)
        {
            string message = $"Sorry, I did not understand '{result.Query}'. Type 'help' if you need assistance.";

            await context.PostAsync(message);

            context.Wait(this.MessageReceived);
        }

        [LuisIntent("Camera")]
        public async Task Camera(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Hi! We are analyzed 'Camera' from your message: '{message.Text}'...");

            
        }

        [LuisIntent("Emotion")]
        public async Task Emotion(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Hi! We are analyzed 'Emotion' from your message: '{message.Text}'...");


        }

        [LuisIntent("Greet")]
        public async Task Greet(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Hi! We are analyzed 'Greet' from your message: '{message.Text}'...");


        }

        [LuisIntent("Weather")]
        public async Task Search(IDialogContext context, IAwaitable<IMessageActivity> activity, LuisResult result)
        {
            var message = await activity;
            await context.PostAsync($"Hi! We are analyzed 'Weather from your message: '{message.Text}'...");


        }
    }
}