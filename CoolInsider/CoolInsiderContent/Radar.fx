
sampler backgroundTexture : register(s0);
sampler mazeTexture       : register(s1);
float2 AvatarPosition;
float2 TreasurePosition;
float4 GemColor;

// Combines the Radar Image with the Background Image
float4 PixelShaderFunction( float2 coordinate:TEXCOORD0) : COLOR0
{
	float4 mazeColor  = tex2D(mazeTexture, coordinate);
	float4 backColor  = tex2D(backgroundTexture, coordinate);
	float4 output = float4(0,0,0,.5);
	
	if(mazeColor.a > .4){
		if(distance(AvatarPosition,coordinate) < .03){
				output = float4(1,0,0,1);
		}
		if(distance(TreasurePosition,coordinate) < .05){
				output = GemColor;
		}
	}
	else output = backColor;


    return output;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
