/// <summary>
/// All plugin specific settings
/// </summary>
public class PluginSettings {
  /// <summary>
  /// The command signifier used to obtain Unicode characters (defaults to "char ")<br />
  /// Using this signifier is the only way to get a result from this plugin, as to not needlessly send queries to the Unicode API
  /// </summary>
  public string CharacterSignifier { get; set; } = "char ";
  /// <summary>
  ///   The threshold for when to consider a character's description
  ///   is similar enough to the query for it to be displayed 
  ///   (defaults to 90). The larger the number, the more similar it needs to be
  /// </summary>
  public int FuzzySearchThreshold { get; set; } = 90;
}