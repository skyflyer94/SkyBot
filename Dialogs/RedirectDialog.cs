namespace MultiDialogsBot.Dialogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.FormFlow;
    using Microsoft.Bot.Connector;

    [Serializable]
    public class RedirectDialog : IDialog<object>
    {
        public async Task StartAsync(IDialogContext context)
        {
            //await context.PostAsync("Welcome to FAQ!");
            ShowOptions(context);
        }

        private enum OptionList { Space = 1, Appshare };

        private void ShowOptions(IDialogContext context)
        {
            PromptDialog.Choice(context, this.OnOptionSelected, Enum.GetNames(typeof(OptionList)).ToList(), "Choose a category?", "Not a valid option", 3);
        }

        private async Task OnOptionSelected(IDialogContext context, IAwaitable<string> result)
        {
            try
            {
                string optionSelected = await result;
                OptionList opt;
                Enum.TryParse(optionSelected, out opt);
                if(optionSelected.ToLower().Contains("back")|| optionSelected.ToLower().Contains("cancel"))
                {
                    context.Fail(new FormCanceledException("exit from faq", null));
                }
                switch (opt)
                {
                    case OptionList.Space:
                        await context.PostAsync($"Ask a Question =)");
                        context.Call(new QnADialog("Space","103b9ce0-a863-4112-8c38-25d35f900148", "69e1cbed729b4fa8ac00cf5bd5570381"), this.ResumeAfterOptionDialog);
                        break;

                    case OptionList.Appshare:
                        await context.PostAsync($"Ask a Question =)");
                        context.Call(new QnADialog("Appshare","1f539b39-c180-491a-a407-ff9a7303529e", "69e1cbed729b4fa8ac00cf5bd5570381"), this.ResumeAfterOptionDialog);
                        break;
                    default:

                        break;
                }
            }
            catch (TooManyAttemptsException ex)
            {
                await context.PostAsync($"Ooops! Too many attemps :(. But don't worry, I'm handling that exception and you can try again!");

                context.Wait(this.MessageReceivedAsync);
            }
        }

        public virtual async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        {
            var message = await result;

            if (message.Text.ToLower().Contains("back"))
            {
                context.Fail(new FormCanceledException("exit", null));
            }else if (message.Text.ToLower().Contains("show"))
            {
                context.PostAsync($"This is Category Dialog");
            }
            else
            {
                this.ShowOptions(context);
            }
        }

        private async Task ResumeAfterOptionDialog(IDialogContext context, IAwaitable<object> result)
        {
            try
            {
                var message = await result;
            }
            catch (Exception ex)
            {
                await context.PostAsync($"Back to FAQ category.");
            }
            finally
            {
                context.Wait(this.MessageReceivedAsync);
            }
        }
    }
}