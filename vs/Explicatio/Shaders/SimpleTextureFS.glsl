#version 330 

varying vec2 varying_uv;

uniform sampler2D base_texture;

out vec4 fragment_colour;

void main(void)
{
    fragment_colour = texture2D(base_texture, varying_uv);
}