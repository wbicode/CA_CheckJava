# Custom Action - Check Java

This WiX Custom Action enables it to check if Java is installed and find out its Platform (x86 or x64). This is achieved by checking the registry (and not the environment variable JAVA_HOME). <br />

The Platform will be stored in the Property "JRE_INSTALLED" ("32bit" or "64bit") and if no installation is found it's set to "0".

## Usage

Include the built **CA_CheckJava.CA.dll** from this project in your own project or use the published nuget (defined in CA_CheckJava/CA_CheckJava.nuspec). 

Reference the .dll in your .wxs file like so:

`<Binary Id="CA_CheckJava" SourceFile="pathToNugetPackages/CA_DirectoryChooser.X.X.X/lib/net45/CA_CheckJava.CA.dll" />`

* "pathToNugetPackages" could be $(var.SolutionDir)/packages

And now you can create your CustomAction: 

`<CustomAction Id="CheckJREInstalled" BinaryKey="CA_CheckJava" DllEntry="CheckJREInstalled" Execute="immediate" />`

After this Action the Property JRE_INSTALLED contains either "64bit" or "32bit" or "0" if no java installation is found. <br />

If both platforms are installed "64bit" is returned.

