namespace Plugin_UnicodeCharacterLookup {

  /// <summary>
  /// A Unicode character
  /// </summary>
  public class Result {
    /// <summary>
    /// The character
    /// </summary>
    public string character { get; set; } = "";
    /// <summary>
    /// The character's description
    /// </summary>
    public string name { get; set; } = "";
    /// <summary>
    /// not used
    /// </summary>
    public string description { get; set; } = "";
    /// <summary>
    /// The Unicode code-point for the character
    /// </summary>
    public string codepoint { get; set; } = "";
    /// <summary>
    /// The character URI encoded
    /// </summary>
    public string uriEncoded { get; set; } = "";
    /// <summary>
    /// How close in % the character is to the query
    /// </summary>
    public double score { get; set; } = 0;
  }

  /// <summary>
  /// The response the API provides
  /// </summary>
  public class ApiResponse {
    /// <summary>
    /// the API endpoint used
    /// </summary>
    public string url { get; set; } = "";
    /// <summary>
    /// the character searched for
    /// </summary>
    public string query { get; set; } = "";
    /// <summary>
    /// whether or not there are more characters that match the query
    /// </summary>
    public bool hasMore { get; set; } = false;
    /// <summary>
    /// Current page (not used since a page has ItemLimit of characters)
    /// </summary>
    public int currentPage { get; set; }
    /// <summary>
    /// The next page (not used since a page has ItemLimit of characters)
    /// </summary>
    public int nextPage { get; set; }
    /// <summary>
    /// total number of characters that match the query
    /// </summary>
    public int totalResults { get; set; }
    /// <summary>
    /// The characters (on the first page) that match the query 
    /// </summary>
    public List<Result> results { get; set; } = new();
  }


}
