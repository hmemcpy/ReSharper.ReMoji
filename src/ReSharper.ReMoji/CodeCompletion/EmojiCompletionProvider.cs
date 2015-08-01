using JetBrains.ReSharper.Feature.Services.CodeCompletion;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;

namespace ReSharper.ReMoji.CodeCompletion
{
    [Language(typeof(CSharpLanguage))]
    public class EmojiCompletionProvider : ItemsProviderOfSpecificContext<EmojiCompletionContext>
    {
        protected override bool IsAvailable(EmojiCompletionContext context)
        {
            if (context.BasicContext.CodeCompletionType != CodeCompletionType.BasicCompletion)
            {
                
            }

            return base.IsAvailable(context);
        }
    }
}