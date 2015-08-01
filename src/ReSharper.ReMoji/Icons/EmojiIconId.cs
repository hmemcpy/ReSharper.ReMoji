using System;
using JetBrains.UI.Icons;

namespace ReSharper.ReMoji.Icons
{
    class EmojiIconId : IconId
    {
        public Emoji.Emoji Emoji { get; }

        public EmojiIconId(Emoji.Emoji emoji)
        {
            Emoji = emoji;
        }

        public override string ToString()
        {
            return $"EmojiIconId({Emoji.Name})";
        }

        public override int CompareTo(IconId otherRaw)
        {
            EmojiIconId imageSourceIconId = otherRaw as EmojiIconId;
            if (imageSourceIconId == null)
                return string.Compare(GetType().Name, otherRaw.GetType().Name, StringComparison.Ordinal);
            return string.Compare(Emoji.Name, imageSourceIconId.Emoji.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            var imageSourceIconId = obj as EmojiIconId;
            if (imageSourceIconId == null)
                return false;
            return Emoji == imageSourceIconId.Emoji;
        }

        public override int GetHashCode()
        {
            return Emoji.GetHashCode();
        }
    }
}