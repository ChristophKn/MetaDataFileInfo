namespace MetaDataFileInfo.Interfaces
{
  using System;

  /// <summary>
  ///   The Property Interface.
  /// </summary>
  public interface IProperty
  {
    /// <summary>
    ///   Gets or sets the value of the Property.
    /// </summary>
    object Value { get; set; }

    /// <summary>
    ///   Gets the type of the Value.
    /// </summary>
    Type Type { get; }

    /// <summary>
    ///   Gets or sets the element value of the property if its an xml content.
    /// </summary>
    /// <param name="xmlElementName">The xml element name to use.</param>
    /// <returns>The xml element value.</returns>
    string this[string xmlElementName] { get; set; }
  }
}