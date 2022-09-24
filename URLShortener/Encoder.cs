namespace URLShortener
{
    public static class Encoder
    {
        private const string Alphabet = "abcdefghijklmnopqrstuvwxyz0123456789";
        private static readonly int Base = Alphabet.Length;

        public static string Encode(string s)
        {
            int i = 0;

            int index = s.IndexOf('/') + 1; // удаляем из строки http://, https://, ftp://
            s = s.Remove(0, index + 1);

            foreach (var c in s)
            {
                if (Alphabet.IndexOf(c) > -1)
                {
                    i = (i * Base) + Alphabet.IndexOf(c);
                }
            }

            i = Math.Abs(i);

            if (i == 0) return Alphabet[0].ToString();

            string str = string.Empty;

            while (i > 0)
            {
                str += Alphabet[i % Base];
                i = i / Base;
            }

            string result = "sh.rt/" + string.Join(string.Empty, str.Reverse());

            return result;
        }
    }
}
