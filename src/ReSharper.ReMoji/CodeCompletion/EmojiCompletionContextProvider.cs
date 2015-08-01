using JetBrains.DocumentModel;
using JetBrains.ReSharper.Feature.Services.CodeCompletion;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.CompletionInDocComments;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Impl;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Infrastructure;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.CSharp.Tree;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;

namespace ReSharper.ReMoji.CodeCompletion
{
    [IntellisensePart]
    class EmojiCompletionContextProvider : CodeCompletionContextProviderBase
    {
        public override bool IsApplicable(CodeCompletionContext context)
        {
            return context.File is ICSharpFile;
        }

        public override ISpecificCodeCompletionContext GetCompletionContext(CodeCompletionContext context)
        {
            TreeOffset startOffset = context.SelectedTreeRange.StartOffset;
            ITokenNode tokenNode = GetTokenNode(context);
            if (tokenNode == null)
                return null;

            int offset = tokenNode.GetTreeStartOffset().Offset;
            int start = startOffset.Offset - offset;
            if (start <= 2)
                return null;

            string text = tokenNode.GetText();
            if (start > text.Length)
                return null;

            string commentText = text.Substring(2, start - 2);
            int emojiStart = commentText.LastIndexOf(':');
            if (emojiStart < 0)
                return null;
            for (int index = emojiStart + 1; index < commentText.Length; ++index)
            {
                if ((index != emojiStart + 1 || !IsEmojiChar(commentText[index])) && (index <= emojiStart + 1 || !IsEmojiChar(commentText[index])))
                    return null;
            }
            DocumentRange documentRange = context.File.GetDocumentRange(new TreeTextRange(new TreeOffset(offset + emojiStart + 2), new TreeOffset(offset + start)));
            if (!documentRange.IsValid())
                return null;
            return new ContextInDocComment(context, documentRange, new TextLookupRanges(documentRange.TextRange, documentRange.TextRange));

        }

        internal static bool IsEmojiChar(char c)
        {
            if (char.IsLetterOrDigit(c))
                return true;

            if (c == '_' || c == '+' || c == '-' || c == ':')
                return true;

            return false;
        }

        private ITokenNode GetTokenNode(CodeCompletionContext context)
        {
            if (!(context.File is ICSharpFile))
                return null;

            if (!context.SelectedTreeRange.IsValid() || context.SelectedTreeRange.Length > 0)
                return null;

            if (AutomaticCodeCompletionStrategyEx.MatchTokenType(null, context.File, context.TextControl,
                token => token == CSharpTokenType.END_OF_LINE_COMMENT && token.IsComment))
            {
                var tokenNode = context.File.FindTokenAt(context.Document, context.TextControl.Caret.Offset() - 1) as ITokenNode;
                if (tokenNode is IDocCommentNode) return null;
                return tokenNode;
            }

            return null;
        }
    }
}