Yummy
=====

a library of common functionality that has been used throughout all my projects. 
This project is mainly for my own benefit when building .Net Solutions. It was also used to get familiar with NuGet.

##Yummy.Common
1. Attributes contains custom dataannotations. i.e. WordCount which ensures a string has a specified number of words.
2. Encryption contains <ul><li>XDocument which inherits from System.Linq.XML.XDocument and adds an Encrypted node to the XML that uses the XML contents to generate a SHA1 hash.</li>
<li>String extensions that encrypt the given string using MD5 and SHA1.</li>
</ul>
3. Extensions contains a range of functions for the likes of DateTime, string and others.
4. SocialMedia.Facebook.Client inherits from FacebookClient from the Facebook Nuget Package and adds functionality related to facebook.
5. Image Handler gives a website the ability for the user to upload an Image file. Keep the Original file and create different image sizes
based on the original file.

  <ImageHandlerSettings SourceDirectory="~/Uploads/" Default="Thumbnail">
	<ImageSetting>
    <add Name="Compressed" MaxHeight="700" MaxWidth="700" Quality="90" />
    <add Name="Thumbnail" MaxHeight="200" MaxWidth="200" Quality="90" />
	</ImageSetting>
  </ImageHandlerSettings>

  To request an image use the url: ~/Image.axd?f={image filename}&s={ImageSetting to use (optional)}
  for example: ~/Image.axd?f=test.jpg&s=thumbnail

###ClickToPackage.bat
creates a nuget package based on the Yummy.Common Project.
The Package is currently hosted in the Nuget Gallery.