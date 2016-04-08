sampler Scene: register(s1);
sampler LightMap: register(s2);

float4 PixelShaderFunction(float2 modCoordinate:TEXCOORD0) : COLOR0
{
	return tex2D(Scene,modCoordinate) + tex2D(LightMap,modCoordinate);
}

technique Technique1
{
    pass Pass1
    {
        PixelShader = compile ps_2_0 PixelShaderFunction();
    }
}
