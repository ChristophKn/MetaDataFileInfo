namespace MetaDataFileInfo.Classes
{
  using System;
  using System.Linq;
  using System.Xml.Linq;
  using Extensions;

  /// <summary>
  ///   The XmlHelper class.
  /// </summary>
  internal class XmlHelper
  {
    /// <summary>
    ///   The used xml document.
    /// </summary>
    private XDocument xmlDocument;

    /// <summary>
    ///   Initializes a new instance of the XmlHelper class.
    /// </summary>
    public XmlHelper()
      : this(string.Empty)
    {
    }

    /// <summary>
    ///   Initializes a new instance of the XmlHelper class with the given xml.
    /// </summary>
    /// <param name="xml">The xml string.</param>
    public XmlHelper(string xml)
    {
      if (!xml.IsNullOrEmpty())
      {
        try
        {
          this.xmlDocument = XDocument.Parse(xml);
        }
        catch
        {
          //// Ignored.
        }
      }
    }

    /// <summary>
    ///   Gets a value indicating whether the xml is valid or not.
    /// </summary>
    public bool IsValid => this.xmlDocument != null;

    /// <summary>
    ///   Gets or sets the xml value.
    /// </summary>
    /// <param name="key">The key for the xml element.</param>
    /// <returns>The element value.</returns>
    public string this[string key]
    {
      get => this.GetElementByKey(key)?.Value ?? string.Empty;

      set
      {
        var element = this.GetElementByKey(key);
        if (element == null)
        {
          this.xmlDocument = this.xmlDocument?.Root == null ? new XDocument(new XElement("root")) : this.xmlDocument;

          element = new XElement(key);
          this.xmlDocument.Root.Add(element);
        }

        element.Value = value;
      }
    }

    /// <summary>
    ///   Gets the xml as string.
    /// </summary>
    /// <returns>The xml string.</returns>
    public override string ToString()
    {
      var xml = this.xmlDocument?.ToString() ?? string.Empty;
      xml = xml.Replace(Environment.NewLine, string.Empty);
      xml = xml.Replace("> <", string.Empty);

      return xml;
    }

    /// <summary>
    ///   Gets the element by its key.
    /// </summary>
    /// <param name="key">The key for the element.</param>
    /// <returns>The element with the given key or null.</returns>
    private XElement GetElementByKey(string key)
    {
      return this.xmlDocument?.Root?.Descendants().FirstOrDefault(element => element.Name.LocalName.ToLower() == key.ToLower());
    }
  }
}