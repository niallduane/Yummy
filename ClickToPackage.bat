set PATH=%PATH%;%WINDIR%\Microsoft.Net\Framework\v4.0.30319
MSBuild Yummy.Common\Yummy.Common.csproj /p:Configuration=Release
nuget.exe pack Yummy.Common\Yummy.Common.csproj -Prop Configuration=Release