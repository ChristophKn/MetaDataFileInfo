## MetaDataFileInfo

This library can read / write the MetaData on a file.

### Usage

````
string filePath = "PathToYourFile";
````

#### With Extension ( namespace: MetaDataFileInfo.Extensions)
````
System.IO.FileInfo fileInfo = new System.IO.FileInfo(filePath);
MetaFileInfo metaData = fileInfo.MetaInfo();
````

#### With Class ( namespace: MetaDataFileInfo.Classes)
````
//// With the plain file path.
MetaFileInfo metaData = new MetaFileInfo(filePath);

//// OR with the file info.
MetaFileInfo metaData = new MetaFileInfo(new FileInfo(filePath));
````

#### Accessing the properties
````
//// The Return type of the indexer is oftype MetaDataFileInfo.Classes.Property
//// But this type can be converted to the following types:
//// string
//// int
//// bool
var propertyValue = metaData["KeyForTheMetaData"];

//// Otherwise the value can be accessed by using this
var propertyValue = metaData["KeyForTheMetaData"].Value;
````