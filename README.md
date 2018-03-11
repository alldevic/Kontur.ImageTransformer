# Kontur.ImageTransformer

## Problem
Get a HTTP request containing a picture in png format, the coordinates of the rectangular area and the name of the filter. You need to apply a filter to the picture, cut out a fragment corresponding to the specified area, and send in response.

## API
The service must support two requests:
```
POST /process/<filter>/<coords>
POST /process/<transform>/<coords>
```
Body: png image, <=100KB - 200 OK, >100KB - 400 Bad Request

`<filter>` — string 'grayscale', 'sepia' or 'threshold(x)', where x in `[0, 100]`.
Example of correct `<filter>`: threshold(30)
`<transform>` — string 'rotate-cw', 'rotate-ccw', 'flip-v' or 'flip-h'

If `<filter>` or `<transform>` does not match this format, you need to send an empty response with 400 Bad Request.

`<coords>` is a string of the form x, y, w, h, where x and y are integers specifying the coordinates of the upper-left corner of the rectangle, w and h are integers specifying the width and height of the rectangle.

Coordinates are counted from the upper left corner of the image (the upper left pixel has coordinates 0, 0). All 4 numbers can be negative, and each of them modulo does not exceed 2^31. If the rectangle goes beyond the boundary of the image, it must be trimmed along the border of the image.

An example of the correct `coords` line for a 100x100 image: -5, -30,200,50
In this case, the image area should be in the response 0,0,100,20.

If the intersection of the rectangle with the image is empty - you need to return an empty response with the code 204 No Content.
If `<coords>` has the wrong format - you need to send an empty response with 400 Bad Request.

If the service receives a request of a different format - you need to send an empty response with the code 400 Bad Request.

## Filters

Filters convert each pixel of the image according to the rules described. If the image has an alpha channel, it should remain unchanged.

All operations are performed in whole numbers, unless otherwise stated.
### grayscale
```C#
intensity = (oldR + oldG + oldB) / 3

R = intensity
G = intensity
B = intensity
```

### threshold(x)
```C#
intensity = (oldR + oldG + oldB) / 3

if intensity >= 255 * x / 100
    R = 255
    G = 255
    B = 255
else
    R = 0
    G = 0
    B = 0
```

### sepia
Uses single-precision math (float in C#). When assigning new values to color components, the fractional part is discarded if the number is greater than 255 - it is replaced by 255.
```C#
R = (oldR * .393) + (oldG * .769) + (oldB * .189)
G = (oldR * .349) + (oldG * .686) + (oldB * .168)
B = (oldR * .272) + (oldG * .534) + (oldB * .131)
```

## Transforms

### rotate-cw
Rotate сlockwise

### rotate-ccw
Rotate counter-clockwise

### flip-h
Reflection about the Y axis

### flip-v
Reflection about the X axis

## Structure of solution

ImageTransformer - base solution

Tests - unit testing of solution

Demo - simple client
