sampler Scene : register(s0);
sampler radar : register(s1);

float2 lightSource;
float sourceLightReach;
float4 sourceLightColor;

float4 PixelShaderFunction(float2 coordinate:TEXCOORD0) : COLOR0
{
    float dist = distance(lightSource, coordinate);
	float range = (dist/sourceLightReach);
	float4 color = tex2D(Scene,coordinate);
	if(abs(dist) < sourceLightReach){
		color += sourceLightColor * pow(1-range,2);
	}
	return color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
