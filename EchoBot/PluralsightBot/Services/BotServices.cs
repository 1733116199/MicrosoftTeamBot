using Microsoft.Bot.Builder.AI.Luis;
using Microsoft.Extensions.Configuration;
// using Microsoft.Bot.Builder.AI.LuisV3;
namespace PluralsightBot.Services
{
    public class BotServices
    {
        public BotServices(IConfiguration configuration)
        {
            // var luisApplication = new LuisApplication(
            //     configuration["LuisAppId"],
            //     configuration["LuisAPIKey"],
            //     "https://" + configuration["LuisAPIHostName"]);
            // // Set the recognizer options depending on which endpoint version you want to use.
            // // More details can be found in https://docs.microsoft.com/en-gb/azure/cognitive-services/luis/luis-migration-api-v3
            // var recognizerOptions = new LuisRecognizerOptionsV3(luisApplication)
            // {
            //     PredictionOptions = new Microsoft.Bot.Builder.AI.LuisV3.LuisPredictionOptions
            //     {
            //         IncludeAllIntents = true,
            //         IncludeInstanceData = true
            //     }
            // };

            // Dispatch = new LuisRecognizer(recognizerOptions);
        }

        public LuisRecognizer Dispatch { get; private set; }
    }
}