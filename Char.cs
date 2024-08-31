namespace Plugin_UnicodeCharacterLookup {

  /// <summary>
  /// A Unicode character (UTF-16 Encoding)
  /// </summary>
  public class Char {

    /// <summary>
    /// The character (UTF-16 Encoding)
    /// </summary>
    public string character { get; set; } = "";

    /// <summary>
    /// The name of the character<br />
    /// (e.g., "Greek Capital Letter Epsilon with Tonos" for the character Έ (0x0388))
    /// </summary>
    public string description { get; set; } = "";
  }
}
