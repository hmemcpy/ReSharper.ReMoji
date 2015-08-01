using System.Collections.Generic;
using System.Linq;
using Emoji;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure.LookupItems;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure.LookupItems.Impl;
using JetBrains.ReSharper.Feature.Services.CSharp.CodeCompletion.Infrastructure;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using ReSharper.ReMoji.Icons;

namespace ReSharper.ReMoji.CodeCompletion
{
    [Language(typeof(CSharpLanguage))]
    class EmojiCompletionProvider : ItemsProviderOfSpecificContext<CSharpCodeCompletionContext>
    {
        private readonly IEnumerable<TextLookupItem> lookupItems;

        public EmojiCompletionProvider(IEmojiStore emojiStore)
        {
            emojiStore = emojiStore ?? new EmojiStore();
            lookupItems = emojiStore.Emojis().Select(e => new TextLookupItem(":" + e.Name + ":", new EmojiIconId(e), true));
        }

        protected override bool IsAvailable(CSharpCodeCompletionContext context)
        {
            if (context.NodeInFile is ICSharpCommentNode)
                return true;

            return base.IsAvailable(context);
        }

        protected override bool AddLookupItems(CSharpCodeCompletionContext context, GroupedItemsCollector collector)
        {
            foreach (var lookupItem in lookupItems)
            {
                lookupItem.InitializeRanges(context.CompletionRanges, context.BasicContext);
                collector.Add(lookupItem);
            }

            return true;
        }
    }
}