# Rendering-Mandelbrot
WPF application which role is to render tha Mandelbrot's fractal.The curve of the fractal is rendered via tha following formula
<code> f<sub>c</sub>(z)=z<sup>2</sup>+c </code> .
I have used unsafe code in order to manage bits(pixels) of the image. 
There is optimization using parallel processes over the iteration in width of the image.
