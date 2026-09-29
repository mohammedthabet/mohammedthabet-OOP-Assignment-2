public class Solution
{
    public int MaxVowels(string s, int k)
    {
        int currentVowels = 0;

        // First window
        for (int i = 0; i < k; i++)
        {
            if (IsVowel(s[i]))
                currentVowels++;
        }

        int maxVowels = currentVowels;

        // Slide the window
        for (int right = k; right < s.Length; right++)
        {
            int left = right - k;

            // Character leaving the window
            if (IsVowel(s[left]))
                currentVowels--;

            // Character entering the window
            if (IsVowel(s[right]))
                currentVowels++;

            maxVowels = Math.Max(
                maxVowels,
                currentVowels);

            // Cannot do better than k vowels
            if (maxVowels == k)
                return k;
        }

        return maxVowels;
    }

    private bool IsVowel(char c)
    {
        return c == 'a' ||
               c == 'e' ||
               c == 'i' ||
               c == 'o' ||
               c == 'u';
    }
}