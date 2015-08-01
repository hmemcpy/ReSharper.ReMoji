using JetBrains.Annotations;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure;

namespace ReSharper.ReMoji.CodeCompletion
{
    public class EmojiCompletionContext : SpecificCodeCompletionContext
    {
        public EmojiCompletionContext([NotNull] CodeCompletionContext context) : base(context)
        {
        }

        public override string ContextId => nameof(EmojiCompletionContext);
    }
}