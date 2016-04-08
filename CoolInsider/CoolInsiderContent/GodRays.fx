sampler blackedOut : register(s0);
float2 lightSource;
float sourceLightReach;
float4 sourceLightColor;

float exposure;

float density;
float weight;
float decay;

float4 CreateLight(float2 coordinate){
	float dist = distance(lightSource, coordinate);
	float decay = 1 - (dist/sourceLightReach);
	return sourceLightColor * pow(decay,2);
}


float4 PixelShaderFunction(float2 coordinate:TEXCOORD0) : COLOR0
{
   float4 lightColor = CreateLight(coordinate);
	float2 direction = (lightSource - coordinate);
	direction *= (1.0/10) * density;
	float4 color = tex2D(blackedOut,coordinate);
	float illuminationDecay = 1;
	for(int i = 0 ;i < 3; i++){
		coordinate += direction;
		float4 sampleColor = CreateLight(coordinate);
		sampleColor *= illuminationDecay * weight;
		color += sampleColor;
		illuminationDecay *= decay;
	}
	color.a = 1;
    return color;
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
