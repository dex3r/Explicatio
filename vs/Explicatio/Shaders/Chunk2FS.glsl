#version 330 

in vec2 varying_uv;

uniform sampler2D base_texture;

out vec4 fragment_colour;

void main(void)
{
	fragment_colour = texture2D(base_texture, varying_uv);
	if (fragment_colour.a <= 0.125f)
	//if (fragment_colour.r == 1 && fragment_colour.g == 0 && fragment_colour.b == 1)
	{
		discard;
	}
}