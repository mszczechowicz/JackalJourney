Shader "Custom/DizableZWrite"
{
	SubShader{
		Tags{
			"RenderType" = "Opaque"
			}
		
		Pass{
			ZWrite Off
		}
	}
}