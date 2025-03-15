namespace Shortner;

public record ShortenRequest(string Url);

public record Shorten2(int age, string Url) : ShortenRequest(Url)
{
}
