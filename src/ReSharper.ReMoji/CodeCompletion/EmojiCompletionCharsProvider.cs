using JetBrains.Application.Settings;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Settings;

namespace ReSharper.ReMoji.CodeCompletion
{
    [IntellisensePart]
    public class EmojiCompletionCharsProvider : CompletingCharsProviderBase<EmojiCompletionContext>
    {
        protected override CompletionAction IsCharacterAcceptable(char c, EmojiCompletionContext context, IContextBoundSettingsStore settingsStore)
        {
            if (c == ':') return CompletionAction.Accept;

            return base.IsCharacterAcceptable(c, context, settingsStore);
        }

        protected override bool IsApplicable(EmojiCompletionContext context)
        {
            return true;
        }
    }
}