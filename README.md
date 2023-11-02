<div align="center">
  <p align="center"><img width="210" height="210" src="https://github.com/gappro/Texture-Packer/assets/50177367/f1176a08-adfa-4b93-bb40-b05a3b13a1fd"></img></p>
<pre>
████████╗███████╗██╗  ██╗████████╗██╗   ██╗██████╗ ███████╗    ██████╗  █████╗  ██████╗██╗  ██╗███████╗██████╗ 
╚══██╔══╝██╔════╝╚██╗██╔╝╚══██╔══╝██║   ██║██╔══██╗██╔════╝    ██╔══██╗██╔══██╗██╔════╝██║ ██╔╝██╔════╝██╔══██╗
   ██║   █████╗   ╚███╔╝    ██║   ██║   ██║██████╔╝█████╗      ██████╔╝███████║██║     █████╔╝ █████╗  ██████╔╝
   ██║   ██╔══╝   ██╔██╗    ██║   ██║   ██║██╔══██╗██╔══╝      ██╔═══╝ ██╔══██║██║     ██╔═██╗ ██╔══╝  ██╔══██╗
   ██║   ███████╗██╔╝ ██╗   ██║   ╚██████╔╝██║  ██║███████╗    ██║     ██║  ██║╚██████╗██║  ██╗███████╗██║  ██║ 
  
---------------------------------------------------
desktop MVVM C# WPF app to pack multiple textures into one file
</pre>
</div>
Have you ever had too many textures? Want to use less memory? Try Texture Packer -- a WPF desktop program to package your endless files into one or many beautiful pieces of work.

## How it works

It takes x number of files and calculates their dimensions in the texture atlas based on AtlasResolution/FileNumber unless a Horizontal limit lower than the file number is applied, in which case the limit will override the ecuation to AtlasResolution/Limiter. Via the texture size in the atlas all textures are resized in width and height.

All resized images are drawn on the atlas pixel per pixel via Bitmaps.

Override function works as such that if the number of files are higher to the number of files that fit inside an atlas it creates a folder with various atlases.

*Miles Morales is NOT included in distributed version

```sh
File Name sets the output file name and in case of Override they will be set in a folder of the same name containing FileName+index files
Resolution of atlas sets output file dimensions, will always be a factor of two
Horizontal Limit sets a maximum number of textures per row/column
Override offers capability of creating diverse texture atlases of the same size if the images input are more than a singular atlas can handle
```

<p align="center"><img src="https://github.com/gappro/Texture-Packer/assets/50177367/c56cae3f-064b-4761-b9e4-b5e9ae82f210"></img></p>

```
Image Extensions
```
<p align="center"><img src="https://github.com/gappro/Texture-Packer/assets/50177367/1b80e3bc-1ef2-4168-a708-35cb11685801"></img></p>

```
Non Overrided output example
```
<p align="center"><img src="https://github.com/gappro/Texture-Packer/assets/50177367/c843417e-c951-4dcf-a78a-ac0c411a7fbb"></img></p>

```
Overrided outputs example
```
<p align="center"><img src="https://github.com/gappro/Texture-Packer/assets/50177367/e2e513f8-e1ba-4d52-82bf-5a5290017d15"/><img src="https://github.com/gappro/Texture-Packer/assets/50177367/cb737614-5140-4c60-b6d2-b5c60394ad1f"/></p>

## Meta

Gabriel – gabrielv.gonz@gmail.com

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details 

[https://github.com/gappro](https://github.com/gappro)
