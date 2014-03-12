#version 330 

const int CHUNK_SIZE = 6;

layout (location = 0) in int[CHUNK_SIZE * CHUNK_SIZE] inChunkBlocks;

out int[CHUNK_SIZE * CHUNK_SIZE] chunkBlocks;

void main() 
{
   chunkBlocks = inChunkBlocks;
}