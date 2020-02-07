#include "Macros2D.fxh" 
 
DECLARE_TEXTURE(Texture, 0);
//sampler s0 : register(s0);
//DECLARE_TEXTURE(Mask, 1);
//sampler s1 : register(s1);
 
//bool Enabled = true;

float4 MainPS(float4 pos : SV_POSITION, float4 color1 : COLOR0, float2 texCoord : TEXCOORD0) : SV_TARGET0
{
    float4 pixelColor = SAMPLE_TEXTURE(Texture, texCoord);
 //   float4 maskColor = SAMPLE_TEXTURE(Mask, texCoord);

    //if (Enabled)
    {
    //  	return maskColor * min(pixelColor.a, maskColor.a);
	   
    }

    return pixelColor;
}
 
TECHNIQUE(main, MainPS);