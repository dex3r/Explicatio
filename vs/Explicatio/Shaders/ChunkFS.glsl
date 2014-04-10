#version 330 

uniform sampler2D base_texture;

in vec2 varying_uv;

out vec4 outputColor; 

void main() 
{ 
   outputColor = texture2D(base_texture, varying_uv);
   //if ( outputColor.a <= 0.1f )
   //{
   // discard;
   //}
}