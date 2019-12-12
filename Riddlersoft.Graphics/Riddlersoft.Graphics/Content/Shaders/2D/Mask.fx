#include "Macros.fxh"

DECLARE_TEXTURE(Texture, 0);
DECLARE_TEXTURE(Mask, 1);

bool Enabled = true;

float4 MainPS(float4 pos : SV_POSITION, float4 color1 : COLOR0, float2 texCoord : TEXCOORD0) : SV_TARGET0
{
    float4 pixelColor = SAMPLE_TEXTURE(Texture, texCoord);
    float4 maskColor = SAMPLE_TEXTURE(Mask, texCoord);

    if (Enabled)
    {
        return maskColor * min(maskColor.a, pixelColor.a);
    }
        return pixelColor;
    }

TECHNIQUE(main, MainPS);