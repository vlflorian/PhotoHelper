# PhotoHelper cli tool
This is just a simple cli tool that I created to help me manage my photos.
There's 2 tools at the moment:

### rawcleanup
 I shoot JPG and RAW. After copying the images to my laptop, I go through the JPGs and delete those I don't like. 
 The rawcleanup tool will also delete the corresponding RAW files.

This is faster than going through the RAW files + as a Fuji shooter, most times I just use the JPGs anyway, even though I want to keep the RAW files just in case. 

### border-tool
This adds white borders to images so they have an 1:1 aspect ratio. Useful for posting to Instagram.

## Usage examples
`$ photohelper add-border -d . --border-size 2`

`$ photohelper rawcleanup -v -d . --rawExtension RAF`   