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
            int num1 = startOffset.Offset - offset;
            if (num1 <= 2)
                return null;
            string text = tokenNode.GetText();
            if (num1 > text.Length)
                return null;
            string str = text.Substring(2, num1 - 2);
            int num2 = str.LastIndexOf(':');
            if (num2 < 0)
                return null;
            for (int index = num2 + 1; index < str.Length; ++index)
            {
                if ((index != num2 + 1 || !char.IsLetter(str[index])) && (index <= num2 + 1 || !char.IsLetterOrDigit(str[index])))
                    return null;
            }
            DocumentRange documentRange = context.File.GetDocumentRange(new TreeTextRange(new TreeOffset(offset + num2 + 2), new TreeOffset(offset + num1)));
            if (!documentRange.IsValid())
                return null;
            return new ContextInDocComment(context, documentRange, new TextLookupRanges(documentRange.TextRange, documentRange.TextRange));

        }

        private ITokenNode GetTokenNode(CodeCompletionContext context)
        {
            if (!(context.File is ICSharpFile))
                return null;

            if (!context.SelectedTreeRange.IsValid() || context.SelectedTreeRange.Length > 0)
                return null;

            if (AutomaticCodeCompletionStrategyEx.MatchTokenType(null, context.File, context.TextControl,
                token => token == CSharpTokenType.END_OF_LINE_COMMENT))
            {
                return context.File.FindTokenAt(context.Document, context.TextControl.Caret.Offset() - 1) as ITokenNode;
            }

            return null;
        }
    }
}