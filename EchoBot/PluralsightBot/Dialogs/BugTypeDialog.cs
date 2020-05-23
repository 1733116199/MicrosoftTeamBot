using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using PluralsightBot.Helpers;
using PluralsightBot.Services;

namespace PluralsightBot.Dialogs
{
    public class BugTypeDialog : ComponentDialog
    {
        #region Variables
        private readonly BotServices _botServices;
        private readonly BotStateService _botStateService;

        #endregion
        public BugTypeDialog(string dialogId, BotStateService botStateService, BotServices botServices) : base(dialogId)
        {
            this._botStateService = botStateService;
            this._botServices = botServices;
            InitializeWaterfallDialog();
        }

        private void InitializeWaterfallDialog()
        {
            var waterfallSteps = new WaterfallStep[]
            {
                InitialStepAsync,
                FinalStepAsync
            };

            AddDialog(new WaterfallDialog($"{nameof(BugTypeDialog)}.mainFlow", waterfallSteps));

            InitialDialogId = $"{nameof(BugTypeDialog)}.mainFlow";
        }

        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var result = await _botServices.Dispatch.RecognizeAsync(stepContext.Context, cancellationToken);
            var entities = result.Entities["$instance"]["BugType"];
            Console.WriteLine(entities);
            // var entities = luis.Entities;

            foreach (var entity in entities)
            {
                string sr = (string) entity["text"];
                if (Common.BugTypes.Any(s => s.Equals(sr, StringComparison.OrdinalIgnoreCase)))
                {
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text(String.Format("Yes! {0} is a Bug Type!", sr)), cancellationToken);
                }
                else
                {
                    await stepContext.Context.SendActivityAsync(MessageFactory.Text(String.Format("No! {0} is not a Bug Type!", sr)), cancellationToken);
                }
            }

            return await stepContext.NextAsync(null, cancellationToken);
        }

        private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.EndDialogAsync(null, cancellationToken);
        }

    }
}