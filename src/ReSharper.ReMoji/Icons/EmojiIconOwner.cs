using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using JetBrains.Application;
using JetBrains.UI.Icons;
using JetBrains.Util;

namespace ReSharper.ReMoji.Icons
{
    [ShellComponent]
    class EmojiIconOwner : IIconIdOwner
    {
        public ImageSource TryGetImage(IconId iconId, IconTheme theme, IThemedIconManagerPerThemeCache cache, OnError onerror)
        {
            var emojiIconId = iconId as EmojiIconId;

            var image = emojiIconId?.Emoji.Bitmap();
            return image;
        }

        public Type IconIdType => typeof (EmojiIconId);
    }
}