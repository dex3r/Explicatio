#version 330 

const int CHUNK_SIZE = 6;

layout(triangles) in;
layout(triangle_strip, max_vertices = 144) out;

uniform mat4 projectionMatrix; 
uniform mat4 modelMatrix; 

uniform int[CHUNK_SIZE * CHUNK_SIZE] chunkBlocks;

out vec2 varying_uv;

void main() 
{
	mat4 r = projectionMatrix * modelMatrix;
	vec2 v1 = vec2(0.0, 0.5);
	vec2 v2 = vec2(0.5, 1);
	vec2 v3 = vec2(0.5, 0);
	vec2 v4 = vec2(1, 0.5);
	float x;
	float y;
	for(int i = 0; i < CHUNK_SIZE; i++)
	{
		for(int j = 0; j < CHUNK_SIZE; j++)
		{
		if(chunkBlocks[i * CHUNK_SIZE + j] == 0)
		{
			x = (j - i) * 1.939;
		    y = (j + i) * 0.969;
			//x = i;
			//y = j;

			gl_Position = r * vec4(-1.0 + x, 0.0 + y, 0.0, 1.0); 
			varying_uv = v1;
			EmitVertex();
			gl_Position = r * vec4(1.0 + x, 1.0 + y, 0.0, 1.0); 
			varying_uv = v2;
			EmitVertex();
			gl_Position = r * vec4(1.0 + x, -1.0 + y, 0.0, 1.0); 
			varying_uv = v3;
			EmitVertex();
			gl_Position = r * vec4(3.0 + x, 0.0 + y, 0.0, 1.0); 
			varying_uv = v4;
			EmitVertex();
			EndPrimitive();
			}
		}
	}
}