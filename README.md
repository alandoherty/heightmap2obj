heightmap2obj
=============

A minimal Heightmap to Wavefront (.obj) converter

Usage
=============
heightmap2obj
=============

A minimal Heightmap to Wavefront (.obj) converter

Usage
=============

heightmap2obj [heightmap] [plane scale]

The heightmap can use any image format supported by your .NET framework version, the plane scale is the distance between each converted heightmap pixel on the X and Y axis.

Notes
=============

- This is not ready, nor fully working, and lacks any support for texturing
- Does not optimize object files in any way
- The height scale is taken by the value of red on the colour, divided by two

License
=============

It's MIT, and designed more as code to learn from, than code to be used in any projects, I don't have any problems with this however.

Example
=============

![Heightmap Image](https://dl.dropboxusercontent.com/u/16266581/terrain03.png "Model Input")

![Heightmap Exported](https://dl.dropboxusercontent.com/u/16266581/terrain01.png "Model Output")
