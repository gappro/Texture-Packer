# Texture-Packer

<p align="center"><img width="210" height="210" src="https://github.com/gappro/Texture-Packer/assets/50177367/c5fcb07a-4976-4220-8aad-d5450ab0b4be"></img></p>

Simple MVVM C# WPF Application Texture Packer capable of handling various Resolutions, Image Types and Texture atlases.

## How it works

It takes x number of files and calculates their dimensions in the texture atlas based on AtlasResolution/FileNumber unless a Horizontal limit lower than the file number is applied, in which case the limit will override the ecuation to AtlasResolution/Limiter. Via the texture size in the atlas all textures are resized in width and height.

Override function works as such that if the number of files are higher to the number of files that fit inside an atlas it creates a folder with various atlases.

*Miles Morales is NOT included in distributed version

```
File Name sets the output file name and in case of Override they will be set in a folder of the same name containing FileName+index files
Resolution of atlas sets output file dimensions, will always be a factor of two
Horizontal Limit sets a maximum number of textures per row/column
Override offers capability of creating diverse texture atlases of the same size if the images input are more than a singular atlas can handle
```

<p align="center"><img src="https://github.com/gappro/Texture-Packer/assets/50177367/92559136-9b14-4055-9c5f-28b5a8ce13be"></img></p>

```
Image Extensions
```
<p align="center"><img src="https://github.com/gappro/Texture-Packer/assets/50177367/0e4be3e0-3545-498b-b0a5-cfaa842f7fed"></img></p>

```
Non Overrided output example
```
<p align="center"><img src="https://github.com/gappro/Texture-Packer/assets/50177367/a5f5a8b0-9790-4430-9228-d7143453964d"></img></p>

```
Overrided outputs example
```
<p align="center"><img src="https://github.com/gappro/Texture-Packer/assets/50177367/34043dd5-0662-4cea-9475-4067b917f2f6"/><img src="https://github.com/gappro/Texture-Packer/assets/50177367/5e3b831e-0744-4bfc-ad90-c3b5197504d6"/></p>

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
