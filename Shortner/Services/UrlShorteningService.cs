using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Shortner;

public class UrlShorteningService(UrlDbContext context)
{
    public const int NumberOfCharsInShortLink = 7;
    private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
    private readonly Random _random = new();
    public async Task<string> GenerateUniqueCode()
    {
        var codeChars = new Char[NumberOfCharsInShortLink];
        while (true)
        {
            for (int i = 0; i < NumberOfCharsInShortLink; i++)
            {
                int randomIndex = _random.Next(Alphabet.Length - 1);
                codeChars[i] = Alphabet[randomIndex];
            }
            var code = new string(codeChars);
            if (!await context.ShortendUrls.AnyAsync(x => x.Code == code))
                return code;
        }
    }
}
