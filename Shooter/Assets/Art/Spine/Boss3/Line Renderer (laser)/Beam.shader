// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Beam"
{
	Properties
	{
		_MainTex ("Sprite Texture", 2D) = "white" {}
		_Color ("Tint", Color) = (1,1,1,1)
		_TextureColumns ("Texture Columns", float) = 1
		_TextureRows ("Texture Rows", float) = 1
		_AnimationSpeed ("Animation Speed", float) = 1
		_XTiling ("X Tiling", float) = 1
		_XScrolling ("X Scrolling", float) = 0
//		_AnimationTime ("Animation Time", float) = 1
		[MaterialToggle] PixelSnap ("Pixel snap", Float) = 0
	}

	SubShader
	{
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

		Pass
		{
		CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile _ PIXELSNAP_ON
			#pragma shader_feature ETC1_EXTERNAL_ALPHA
			#include "UnityCG.cginc"
			
			struct appdata_t
			{
				float4 vertex   : POSITION;
				float4 color    : COLOR;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f
			{
				float4 vertex   : SV_POSITION;
				fixed4 color    : COLOR;
				float2 texcoord  : TEXCOORD0;
			};
			
			fixed4 _Color;
			uniform float4 _MainTex_ST;

			v2f vert(appdata_t IN)
			{
				v2f OUT;
				OUT.vertex = UnityObjectToClipPos(IN.vertex);
				OUT.texcoord = TRANSFORM_TEX(IN.texcoord, _MainTex);
//				OUT.texcoord = IN.texcoord;
				OUT.color = IN.color * _Color;
				#ifdef PIXELSNAP_ON
				OUT.vertex = UnityPixelSnap (OUT.vertex);
				#endif

				return OUT;
			}

			sampler2D _MainTex;
			sampler2D _AlphaTex;
			uniform float _TextureColumns;
			uniform float _TextureRows;
			uniform float _AnimationSpeed;
			uniform float _AnimationTime;
			uniform float _XTiling;
			uniform float _XScrolling;

			fixed4 SampleSpriteTexture (float2 uv)
			{
				fixed4 color = tex2D (_MainTex, uv.gr);

				#if ETC1_EXTERNAL_ALPHA
				// get the color from an external texture (usecase: Alpha support for ETC1 on android)
				color.a = tex2D (_AlphaTex, uv.gr).r;
				#endif //ETC1_EXTERNAL_ALPHA

				return color;
			}

			fixed4 frag(v2f IN) : SV_Target
			{
				float2 spriteUV;
				float time = floor(_Time.y * _AnimationSpeed);

				float texWidth = (1.0 / _TextureColumns);
				float texHeight = (1.0 / _TextureRows);

				float xPos = floor(time) % _TextureColumns; 
				float yPos = floor(time / _TextureColumns) % _TextureRows;

				float uu = (xPos * texWidth) + (texWidth * IN.texcoord.x);
				float vv = (yPos * texHeight) + (texHeight * IN.texcoord.y);
			
				spriteUV = float2(uu * _XTiling + _Time.x * _XScrolling, vv);

				fixed4 c = SampleSpriteTexture (spriteUV) * IN.color;

				c.rgb *= c.a;

				xPos += 0; 
				yPos += 1;

				uu = (xPos * texWidth) + (texWidth * IN.texcoord.x) ;
				vv = (yPos * texHeight) + (texHeight * IN.texcoord.y);
			
				spriteUV = float2(uu * _XTiling + _Time.x * _XScrolling, vv);

				fixed4 d = SampleSpriteTexture (spriteUV) * IN.color;
				d.rgb *= d.a;

//				return c;
				xPos = frac(_Time.y * _AnimationSpeed); 
				return lerp(c,d,xPos);
			}
		ENDCG
		}
	}
}
