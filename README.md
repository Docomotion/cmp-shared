# cmp-shared
Component for shared solutions for FDC, DC and RT

***
### How to manually publish project to github packages

1. **Update version at ${project-name}.nuspec file** <br/><br/>
2. **Build project via MSBuild. For example:** <br/>```msbuild ${project-name}.csproj -property:Configuration=Release -property:Platform=AnyCPU -verbosity:m```<br/><br/>
3. **Pack project, example:** <br/>```nuget pack ${project-name}.csproj -Properties Configuration=Release```<br/><br/>
4.**Push project, example:** <br/>```nuget push ${project-name}.${project-version}.nupkg -ApiKey ${github-token (PAT)} -Source "https://nuget.pkg.github.com/${OwnerOrOrganization}/index.json"```