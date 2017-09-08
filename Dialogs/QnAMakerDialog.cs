using System;
using System.Threading.Tasks;

//using Microsoft.Bot.Builder.Azure;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.FormFlow;
using Microsoft.Bot.Connector;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using System.Text;
using System.Diagnostics;
using QnAMakerDialog;

// For more information about this template visit http://aka.ms/azurebots-csharp-qnamaker
namespace MultiDialogsBot.Dialogs
{
    [Serializable]
    [QnAMakerService("69e1cbed729b4fa8ac00cf5bd5570381", "8d239e7e-307d-4263-bb60-711373732ab8")]
    public class QnADialog : QnAMakerDialog<object> {

        public override async Task NoMatchHandler(IDialogContext context, string originalQueryText)
        {
            if (originalQueryText.ToLower().Contains("back"))
            {
                context.Fail(new FormCanceledException("exit from faq", null));
            }
            else
            {
                await context.PostAsync($"Sorry, I couldn't find an answer for '{originalQueryText}'.");
                context.Wait(MessageReceived);
            }
        }

        [QnAMakerResponseHandler(50)]
        public async Task LowScoreHandler(IDialogContext context, string originalQueryText, QnAMakerResult result)
        {
            await context.PostAsync($"I found an answer that might help...{result.Answer}.");
            context.Wait(MessageReceived);
        }
    }
}