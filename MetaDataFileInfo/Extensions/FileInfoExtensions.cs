namespace MetaDataFileInfo.Extensions
{
  using System.IO;
  using Classes;
  using Interfaces;
  using Microsoft.WindowsAPICodePack.Shell;

  /// <summary>
  ///   The FileInfoExtensions class.
  /// </summary>
  public static class FileInfoExtensions
  {
    /// <summary>
    ///   Gets the meta data info from the file info.
    /// </summary>
    /// <param name="fileInfo">The file info to use.</param>
    /// <returns>The meta file info.</returns>
    public static IMetaFileInfo MetaInfo(this FileInfo fileInfo)
    {
      return new MetaFileInfo(fileInfo);
    }

    /// <summary>
    ///   Gets the ShellInfo from the file info.
    /// </summary>
    /// <param name="fileInfo">The file info to use.</param>
    /// <returns>The ShellFile.</returns>
    internal static ShellFile GetShellFile(this FileInfo fileInfo)
    {
      return ShellFile.FromFilePath(fileInfo.FullName);
    }
  }
}