#version 330 

smooth in vec3 theColor; 
out vec4 outputColor; 

void main() 
{ 
   outputColor = vec4(theColor, 1.0); 
}

//#version 130

//precision highp float;

//uniform vec3 lightColor;

//const vec3 ambient = vec3(0.1, 0.1, 0.1);
//const vec3 lightVecNormalized = normalize(vec3(0.5, 0.5, 2.0));

//in vec3 normal;

//out vec4 out_frag_color;

//void main(void)
//{
//  float diffuse = clamp(dot(lightVecNormalized, normalize(normal)), 0.0, 1.0);
//  //out_frag_color = vec4(ambient + diffuse * lightColor, 1.0);
//  out_frag_color = vec4(lightColor, 1.0);
//  //out_frag_color = vec4(vec3(0.8, 0.0, 0.0), 1.0);
//}
