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
  ///   (defaults to 80). Uses the loose-matching rule from the Unicode API
  /// </summary>
  public int FuzzySearchThreshold { get; set; } = 80;
  /// <summary>
  /// The number of items to retrieve
  /// </summary>
  public int ItemLimit { get; set; } = 10;
}