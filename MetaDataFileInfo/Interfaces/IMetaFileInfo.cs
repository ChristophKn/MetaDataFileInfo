namespace MetaDataFileInfo.Interfaces
{
  using System.Collections.Generic;
  using Classes;

  /// <summary>
  ///   The MetaFileInfo Interface.
  /// </summary>
  public interface IMetaFileInfo : IEnumerable<KeyValuePair<string, Property>>
  {
    /// <summary>
    ///   Gets a value indicating whether the meta data are read only or not.
    /// </summary>
    bool IsReadOnly { get; }

    /// <summary>
    ///   Gets or sets the meta data.
    /// </summary>
    /// <param name="key">The key of the meta data to get or set.</param>
    /// <returns>The value of the meta data.</returns>
    Property this[string key] { get; set; }

    /// <summary>
    ///   Gets or sets the extended file info part if its a xml content.
    /// </summary>
    /// <param name="key">key of extended file info.</param>
    /// <param name="xmlElementName">xml element name,</param>
    /// <returns>xml element value.</returns>
    string this[string key, string xmlElementName] { get; set; }
  }
}