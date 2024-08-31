/// <summary>
/// All plugin specific settings
/// </summary>
public class PluginSettings {

  /// <summary>
  /// The command signifier used to obtain the denotation of a word (defaults to "define ")<br />
  /// Using this signifier is the only way to get a result from this plugin, as to not needlessly send queries to the dictionary API
  /// </summary>
  public string DictionarySignifier { get; set; } = "define ";
}