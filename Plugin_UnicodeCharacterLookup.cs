
using Newtonsoft.Json;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.IO;
using System.Net;

namespace Plugin_UnicodeCharacterLookup {

  /// <summary>
  /// The Unicode Character Lookup plugin
  /// </summary>
  public class UnicodeCharacterLookup : Plugin {

    internal List<CharItem> AllChars = new();
    private static PluginSettings pluginSettings = new();
    internal static PluginSettings PluginSettings { get => pluginSettings; set => pluginSettings = value; }

    /// <summary>
    /// Loads plugin settings
    /// </summary>
    public UnicodeCharacterLookup() {
      string fileName = Environment.CurrentDirectory + "\\PlugBoard\\Plugin_UnicodeCharacterLookup\\Plugin\\settings.json";
      PluginSettings = JsonConvert.DeserializeObject<PluginSettings>(File.ReadAllText(fileName))!;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override string PluggerName { get; set; } = "UnicodeCharacterLookup";

    private List<ListItem> parseCharacters(string obj) {
      List<ListItem> characters = new List<ListItem>();
      ApiResponse response = JsonConvert.DeserializeObject<ApiResponse>(obj)!;
      foreach (Result ch in response.results) {
        characters.Add(new CharItem(ch.character, ch.codepoint + " | " + ch.name));
      }
      return characters;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="query"><inheritdoc/></param>
    /// <returns>
    /// An empty list - Using the command signifier is the only way to get a result from this plugin,
    /// as to not needlessly send queries to the Unicode API
    /// </returns>
    public override List<ListItem> OnQueryChange(string query) { return new List<ListItem>(); }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns>
    /// The CharacterSignifier from plugin settings
    /// </returns>
    public override List<string> CommandSignifiers() {
      return new List<string>() { PluginSettings.CharacterSignifier };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="command">The CharacterSignifier (Since there is only 1 signifier for this plugin), followed by the character being searched for</param>
    /// <returns>List of characters that possibly match what is being searched for</returns>
    public override List<ListItem> OnSignifier(string command) {
      command = command.Substring(PluginSettings.CharacterSignifier.Length);
      try {
        WebRequest request = WebRequest.CreateHttp("https://unicode-api.aaronluna.dev/v1/characters/search?name=" + command + "&min_score=" + PluginSettings.FuzzySearchThreshold + "&per_page=" + PluginSettings.ItemLimit);
        request.ContentType = "application/json";
        string characters;
        var response = (HttpWebResponse) request.GetResponse();
        using (var sr = new StreamReader(response.GetResponseStream())) {
          characters = sr.ReadToEnd();
        }
        return parseCharacters(characters);
      } catch (Exception) {
        return new List<ListItem>();
      }
    }
  }

}
