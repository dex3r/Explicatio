#version 330 

uniform mat4 projectionMatrix; 
uniform mat4 modelMatrix; 
uniform mat4 viewMatrix;
uniform vec3 inColor;

layout (location = 0) in vec2 inPosition;  

smooth out vec3 theColor; 

void main() 
{ 
   gl_Position = projectionMatrix * modelMatrix * viewMatrix * vec4(inPosition, 0.0, 1.0); 
   theColor = inColor; 
}