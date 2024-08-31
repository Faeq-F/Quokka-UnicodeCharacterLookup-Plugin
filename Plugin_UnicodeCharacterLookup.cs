
using Newtonsoft.Json;
using Quokka.ListItems;
using Quokka.PluginArch;
using System.IO;
using WinCopies.Util;

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

    /// <summary>
    /// <inheritdoc/>
    /// Loads the UnicodeChars.json file
    /// </summary>
    public override void OnAppStartup() {
      string fileName = Environment.CurrentDirectory + "\\PlugBoard\\Plugin_UnicodeCharacterLookup\\Plugin\\UnicodeChars.json";
      List<Char> chars = JsonConvert.DeserializeObject<List<Char>>(File.ReadAllText(fileName))!;
      foreach (Char i in chars) AllChars.Add(new CharItem(i));
    }

    private List<ListItem> ProduceItems(string query) {
      List<ListItem> IdentifiedChars = new();
      //filtering apps
      foreach (CharItem ch in AllChars) {
        if (
            ch.Name.Contains(query, StringComparison.OrdinalIgnoreCase)
            || ( FuzzySearch.LD(ch.Description, query) < PluginSettings.FuzzySearchThreshold )
        ) {
          IdentifiedChars.Add(ch);
        }
      }
      return IdentifiedChars;
    }

    public override List<ListItem> OnQueryChange(string query) {
      return ProduceItems(query);
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <returns>The AllCharsSpecialCommand from plugin settings</returns>
    public override List<String> SpecialCommands() {
      List<String> SpecialCommand = new() { PluginSettings.AllCharsSpecialCommand };
      return SpecialCommand;
    }

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="command">The AllCharsSpecialCommand from plugin settings (Since there is only 1 special command for this plugin)</param>
    /// <returns>All Unicode characters</returns>
    public override List<ListItem> OnSpecialCommand(string command) {
      return new(AllChars);
    }

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
      return ProduceItems(command.Substring(PluginSettings.CharacterSignifier.Length));
    }
  }

}
