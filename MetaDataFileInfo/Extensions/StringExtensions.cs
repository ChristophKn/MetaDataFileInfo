namespace MetaDataFileInfo.Extensions
{
  /// <summary>
  ///   The StringExtensions class.
  /// </summary>
  public static class StringExtensions
  {
    /// <summary>
    /// Checks if the given string is null or empty.
    /// </summary>
    /// <param name="s">The string to check.</param>
    /// <returns>True if the value if null or empty, false if not.</returns>
    public static bool IsNullOrEmpty(this string s)
    {
      return string.IsNullOrEmpty(s);
    }
  }
}