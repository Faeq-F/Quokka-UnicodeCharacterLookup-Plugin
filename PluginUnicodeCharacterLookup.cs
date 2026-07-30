using Newtonsoft.Json;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.Collections.ObjectModel;
using System.IO;

namespace PluginUnicodeCharacterLookup
{

  /// <summary>
  /// A Unicode character
  /// </summary>
  public class UnicodeCharacter
  {
    /// <summary>
    /// The character itself
    /// </summary>
    public string Name { get; set; } = "";
    /// <summary>
    /// A description of the character
    /// </summary>
    public string Description { get; set; } = "";
  }

  /// <summary>
  /// The Unicode Character Lookup plugin
  /// </summary>
  public class UnicodeCharacterLookup : Plugin
  {

    private static readonly Lazy<PluginSettings> _lazyPluginSettings = new(() =>
    {
      string fileName = Path.Combine(Environment.CurrentDirectory, "PlugBoard", "PluginUnicodeCharacterLookup", "Plugin", "settings.json");
      return File.Exists(fileName)
        ? JsonConvert.DeserializeObject<PluginSettings>(File.ReadAllText(fileName)) ?? new PluginSettings()
        : new PluginSettings();
    });

    internal static PluginSettings PluginSettings => _lazyPluginSettings.Value;

    private static readonly Lazy<List<UnicodeCharacter>> _lazyCharacters = new(() =>
    {
      string fileName = Path.Combine(Environment.CurrentDirectory, "PlugBoard", "PluginUnicodeCharacterLookup", "Plugin", "index.json");
      return File.Exists(fileName)
        ? JsonConvert.DeserializeObject<List<UnicodeCharacter>>(File.ReadAllText(fileName)) ?? new List<UnicodeCharacter>()
        : new List<UnicodeCharacter>();
    });

    internal static List<UnicodeCharacter> Characters => _lazyCharacters.Value;

    /// <summary>
    /// Initializes a new instance of the <see cref="UnicodeCharacterLookup"/> class.
    /// </summary>
    public UnicodeCharacterLookup() { }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    public override string PluginName { get; set; } = "UnicodeCharacterLookup";

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="query"><inheritdoc/></param>
    /// <returns>
    /// An empty collection - Using the command signifier is the only way to get a result from this plugin,
    /// as to not needlessly send queries to the Unicode API
    /// </returns>
    public override Collection<ListItem> OnQueryChange(string query) { return new Collection<ListItem>(); }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns>
    /// The CharacterSignifier from plugin settings
    /// </returns>
    public override Collection<string> CommandSignifiers()
    {
      return new Collection<string>() { PluginSettings.CharacterSignifier };
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="command">The CharacterSignifier (Since there is only 1 signifier for this plugin), followed by the character being searched for</param>
    /// <returns>Collection of characters that possibly match what is being searched for</returns>
    public override Collection<ListItem> OnSignifier(string command)
    {
      command ??= "";
      command = command.Substring(PluginSettings.CharacterSignifier.Length);
      return FuzzySearch.Sort(command,
        new Collection<ListItem>(
          FuzzySearch.SearchAll(command,
            new Collection<string>(Characters.ConvertAll(x => x.Description)), PluginSettings.FuzzySearchThreshold)
            .Select(x => (ListItem)new CharItem(Characters[x.Index].Name, Characters[x.Index].Description))
            .ToList())
        );
    }
  }

}
