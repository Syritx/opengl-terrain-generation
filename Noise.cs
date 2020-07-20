using System;
using OpenTK;

namespace terrain_generation
{
    public class Noise
    {
        static float[] heightMaps;

        public static Vector3[] GenerateHeightMaps(int layers, int length, float intensity, int min, int max)
        {
            int area = length * length;
            int index = 0;

            Vector3[] vertices = new Vector3[area];
            heightMaps = new float[area];

            for (int layer = 0; layer < layers; layer++) {
                for (int h = 0; h < area; h++) {
                    if (min > 0) min *= -1;

                    Random rMin = new Random();
                    Random rMax = new Random();
                    int newMin = rMin.Next(min, -1);
                    int newMax = rMax.Next(1, max);

                    Random randomIncrease = new Random();
                    float newHeight = randomIncrease.Next(newMin, newMax);
                    heightMaps[h] += (newHeight*intensity);
                }
            }

            for (int x = 0; x < length; x++) {
                for (int z = 0; z < length; z++) {
                    vertices[index] = new Vector3((x-length/2)*5, heightMaps[index], (z-length/2)*5);
                    index++;
                }
            }

            return vertices;
        }
    }
}
