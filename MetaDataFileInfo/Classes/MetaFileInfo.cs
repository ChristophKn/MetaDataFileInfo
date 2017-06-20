namespace MetaDataFileInfo.Classes
{
  using System;
  using System.Collections;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using Extensions;
  using Interfaces;

  /// <summary>
  ///   The MetaFileInfo class.
  /// </summary>
  public class MetaFileInfo : IMetaFileInfo
  {
    /// <summary>
    ///   The used file Info.
    /// </summary>
    private readonly FileInfo fileInfo;

    /// <summary>
    ///   The used lazy initialization for the properties.
    /// </summary>
    private readonly Lazy<IDictionary<string, Property>> properties;

    /// <summary>
    ///   Initializes a new instance of the MetaFileInfo class.
    /// </summary>
    public MetaFileInfo(FileInfo fileInfo)
    {
      this.fileInfo = fileInfo;
      this.properties = new Lazy<IDictionary<string, Property>>(this.GetProperties);
    }

    /// <summary>
    ///   Initializes a new instance of the MetaFileInfo class.
    /// </summary>
    /// <param name="filePath">The file path to use.</param>
    public MetaFileInfo(string filePath)
      : this(new FileInfo(filePath))
    {
    }

    /// <summary>
    ///   Gets the properties.
    /// </summary>
    private IDictionary<string, Property> Properties => this.properties?.Value;

    /// <summary>
    ///   Gets the enumerator containing the property key and the property itself.
    /// </summary>
    /// <returns>The property enumerator.</returns>
    public IEnumerator<KeyValuePair<string, Property>> GetEnumerator()
    {
      return this.Properties.GetEnumerator();
    }

    /// <summary>
    ///   Gets the enumerator for this meta file info.
    /// </summary>
    /// <returns>The enumerator.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return this.GetEnumerator();
    }

    /// <summary>
    ///   Gets or sets the meta file info.
    /// </summary>
    /// <param name="key">The key of the meta file info to get or set.</param>
    /// <returns>The value of the meta file info.</returns>
    public Property this[string key]
    {
      get
      {
        if (this.Properties.ContainsKey(key))
        {
          return this.Properties[key];
        }

        throw new ApplicationException($"Meta file info key \"{key}\" does not exist");
      }
      set
      {
        if (this.Properties.ContainsKey(key))
        {
          this.Properties[key].Value = value?.Value;
        }
        else
        {
          throw new ApplicationException($"Meta file info key \"{key}\" does not exist");
        }
      }
    }

    /// <summary>
    ///   Gets or sets the extended file info part if its a xml content.
    /// </summary>
    /// <param name="key">key of extended file info.</param>
    /// <param name="xmlElementName">xml element name,</param>
    /// <returns>xml element value.</returns>
    public string this[string key, string xmlElementName]
    {
      get => this[key][xmlElementName];

      set => this[key][xmlElementName] = value;
    }

    /// <summary>
    ///   Gets a value indicating whether the meta data are read only or not.
    /// </summary>
    public bool IsReadOnly
    {
      get
      {
        using (var shellFile = this.fileInfo.GetShellFile())
        {
          return shellFile.IsReadOnly();
        }
      }
    }

    /// <summary>
    ///   Gets a dictionary with all possible properties.
    /// </summary>
    /// <returns>Dictionary with the possible properties.</returns>
    private IDictionary<string, Property> GetProperties()
    {
      using (var shellFile = this.fileInfo.GetShellFile())
      {
        return shellFile.GetPropertyKeysWithName().ToDictionary(p => p.Key, p => new Property(this.fileInfo, p.Key), StringComparer.InvariantCultureIgnoreCase);
      }
    }
  }
}