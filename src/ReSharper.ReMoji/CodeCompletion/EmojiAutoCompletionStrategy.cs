using JetBrains.Application.Settings;
using JetBrains.ProjectModel;
using JetBrains.ReSharper.Feature.Services.CodeCompletion;
using JetBrains.ReSharper.Feature.Services.CodeCompletion.Settings;
using JetBrains.ReSharper.Psi;
using JetBrains.ReSharper.Psi.CSharp;
using JetBrains.ReSharper.Psi.CSharp.Parsing;
using JetBrains.ReSharper.Psi.Tree;
using JetBrains.TextControl;

namespace ReSharper.ReMoji.CodeCompletion
{
    [SolutionComponent]
    public class EmojiAutoCompletionStrategy : IAutomaticCodeCompletionStrategy
    {
        public AutopopupType IsEnabledInSettings(IContextBoundSettingsStore settingsStore, ITextControl textControl)
        {
            return AutopopupType.SoftAutopopup;
        }

        public bool AcceptTyping(char c, ITextControl textControl, IContextBoundSettingsStore boundSettingsStore)
        {
            if (c == ':') return true;

            if (EmojiCompletionContextProvider.IsEmojiChar(c))
                return this.MatchText(textControl, 1, text => text[0] == ':');

            return false;
        }

        public bool ProcessSubsequentTyping(char c, ITextControl textControl)
        {
            return EmojiCompletionContextProvider.IsEmojiChar(c);
        }

        public bool AcceptsFile(IFile file, ITextControl textControl)
        {
            return this.MatchTokenType(file, textControl, token => token == CSharpTokenType.END_OF_LINE_COMMENT);
        }

        public PsiLanguageType Language => CSharpLanguage.Instance;
        public bool ForceHideCompletion => false;
    }
}