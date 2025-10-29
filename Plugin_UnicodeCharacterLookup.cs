
using Newtonsoft.Json;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.IO;

namespace Plugin_UnicodeCharacterLookup {

  /// <summary>
  /// A Unicode character
  /// </summary>
  public class UnicodeCharacter {
    /// <summary>
    /// The character itself
    /// </summary>
    public string name { get; set; } = "";
    /// <summary>
    /// A description of the character
    /// </summary>
    public string description { get; set; } = "";
  }

  /// <summary>
  /// The Unicode Character Lookup plugin
  /// </summary>
  public class UnicodeCharacterLookup : Plugin {

    private static PluginSettings pluginSettings = new();
    internal static PluginSettings PluginSettings { get => pluginSettings; set => pluginSettings = value; }

    private static List<UnicodeCharacter> characters = new();
    internal static List<UnicodeCharacter> Characters { get => characters; set => characters = value; }

    /// <summary>
    /// Loads plugin settings
    /// </summary>
    public UnicodeCharacterLookup() {
      string fileName = Environment.CurrentDirectory + "\\PlugBoard\\Plugin_UnicodeCharacterLookup\\Plugin\\settings.json";
      PluginSettings = JsonConvert.DeserializeObject<PluginSettings>(File.ReadAllText(fileName))!;
      fileName = Environment.CurrentDirectory + "\\PlugBoard\\Plugin_UnicodeCharacterLookup\\Plugin\\index.json";
      Characters = JsonConvert.DeserializeObject<List<UnicodeCharacter>>(File.ReadAllText(fileName))!;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override string PluggerName { get; set; } = "UnicodeCharacterLookup";

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
      return FuzzySearch.sort(command, FuzzySearch.searchAll(command, Characters.Select(x => x.description).ToList(), PluginSettings.FuzzySearchThreshold).Select(x => (ListItem) new CharItem(Characters[x.Index].name, Characters[x.Index].description)).ToList()).ToList();
    }
  }

}
