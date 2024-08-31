/// <summary>
/// All plugin specific settings
/// </summary>
public class PluginSettings {
  /// <summary>
  /// The command signifier used to only show items from this plugin (defaults to "char ")<br />
  /// Using this signifier does not change the output of this plugin, it only
  /// ensures that no other plugins' results are included in the search window results list
  /// </summary>
  public string CharacterSignifier { get; set; } = "char ";
  /// <summary>
  /// The command to show all Unicode characters
  /// </summary>
  public string AllCharsSpecialCommand { get; set; } = "AllChars";
  /// <summary>
  ///   The threshold for when to consider a character's description
  ///   is similar enough to the query for it to be
  ///   displayed (defaults to 5). Currently uses the
  ///   Levenshtein distance; the larger the number, the
  ///   bigger the difference.
  /// </summary>
  public int FuzzySearchThreshold { get; set; } = 5;
}