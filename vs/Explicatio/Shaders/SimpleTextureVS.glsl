#version 330 

uniform mat4 projectionMatrix; 
uniform mat4 modelViewMatrix; 

layout (location = 0) in vec2 inPosition; 
layout (location = 1) in vec2 uv;

varying out vec2 varying_uv;

void main() 
{ 
   gl_Position = projectionMatrix * modelViewMatrix * vec4(inPosition, 0.0, 1.0); 
   varying_uv = uv;
}