#version 150
uniform sampler2D MyTexture0;
uniform sampler2D MyTexture1;
uniform sampler2D MyTexture2;
uniform sampler2D MyTexture3;
 
void main(void)
{
 if (gl_TexCoord[0].s < 0.25){
  gl_FragColor = texture2D( MyTexture0, gl_TexCoord[0].st );  
  gl_FragColor[1] = gl_FragColor[1] * 0.90;
 }
else if (gl_TexCoord[0].s < 0.5) {
  gl_FragColor = texture2D( MyTexture1, gl_TexCoord[0].st );  
  gl_FragColor[0] = gl_FragColor[0] * 0.90;
 }
else if (gl_TexCoord[0].s < 0.75) {
  gl_FragColor = texture2D( MyTexture2, gl_TexCoord[0].st );  
  gl_FragColor[2] = gl_FragColor[2] * 0.90;
 }
else {
  gl_FragColor = texture2D( MyTexture3, gl_TexCoord[0].st );  
 }
}