# SvgPathExtractor
Niche utility to extract path tags out of svg files.

Simple file text reader specific to .svg files. It uses a pattern matching to capture the path tag, and the value of the d attribute.
It outputs files as a json file (which is sort of unnecessary) for ease of readability and separation of wanted result.

## How to use
* build the application or download the release version
* (optional) place the tool into the folder where the .svg files are located
* run the application!
* (optional) output will be created at the source of the application. If this behaviour is unwanted, manually create **OR** select an existing path to where it should be output.
