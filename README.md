# LiteDB.Studio

A GUI tool for viewing and editing documents for LiteDB v5, **Linux-compatible source**.

![LiteDB Studio](https://images2.imgbox.com/64/40/WBbd8qDV_o.png)

This is a fork of [the original LiteDB.Studio](https://github.com/mbdavid/LiteDB.Studio) repository.

### Why?
I've seen a lot of request for LiteDB.Studio on Linux. So I took a quick look and found that it targets .NET Framework while not having any p/invokes to platform-specific libraries. I've cleaned up some missing resources, using ILRepack instead of ILMerge, and then tweaking the visuals (some controls were overlapping) and did a quick test. It runs nicely on my Ubuntu 20.04 amd64. No GUI rewrite, it depends on Mono's cross platform Windows.Forms implementation.

### Get LiteDB.Studio for Linux
See releases section of the repository (tested only on Ubuntu 20.04 amd64)

### Compiling from Source
You can easily compile this solution from source if you have Mono SDK (or .NET runtime if you are on Windows). The easiest way to build the solution is by using _MonoDevelop_ to just.. _build_.

### Bugs
Feel free to file any issues. I'll see if i can help with it, but basically there is no modifications from the original repository, aside of UI and packing.